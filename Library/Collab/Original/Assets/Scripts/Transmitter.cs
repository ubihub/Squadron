using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Net.Http;
using System;
using System.Globalization;
using UnityEngine.Networking;
using System.Text;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using System.Threading;

namespace BGC.Annotation.Basic
{

    public class Transmitter : MonoBehaviour
    {
        static string protocol = "http://";
        static string host = "localhost";
        static string php = "/repeater/index.php";
        static string group = "mrgeo";
        static string user = "James";
        static float init_time = 1.0f;
        static float interval = 1.0f;
        static string previousInterval = "";
        static StringContent stringContent ;
        public static string previousLabel = "";
        public static  GameObject staticGameObject;
        public static  GameObject updatedGameObject;
        public static string previousjson = "";
        public static string label = "";

        private  Transmitter()
        {
            if (previousLabel.Equals("")) GetLabel();
            //            InvokeRepeating("Request", 1.0f, 3.0f);
            // StartCoroutine(CallRequest(3.0f));
        }

        public static Transmitter Instance { get { return Nested.instance; } }

        private class Nested
        {
            // Explicit static constructor to tell C# compiler
            // not to mark type as beforefieldinit
            static Nested()
            {
            }

            internal static readonly Transmitter instance = new Transmitter();
        }
        // Start is called before the first frame update
        void Start()
        {
            InvokeRepeating("RequestInterval", 1.0f, 600.00f);
            InvokeRepeating("GetAnnotationLabel", init_time, interval);
            InvokeRepeating("Check", init_time, interval);
        }

        // Update is called once per frame
        void Update()
        {

        }

        async public void RequestInterval()
        {
            HttpClient client = new HttpClient();
            string result = "";
            try
            {
                result = await client.GetStringAsync(protocol + host + "/repeater/request_control.php?group_id=mrgeo&action=request_interval&user_id=134562");
                if (!result.Equals(previousInterval))
                {
                    previousInterval = result;
                    char[] delimeter = { '.' };
                    string[] intervalString = result.Split(delimeter);
                    init_time = float.Parse(intervalString[0], CultureInfo.InvariantCulture.NumberFormat);
                    interval = float.Parse(intervalString[1], CultureInfo.InvariantCulture.NumberFormat);
                    interval = interval / init_time;
                    Debug.Log("Transmitter.RequestInterval.init_time = " + init_time);
                    Debug.Log("Transmitter.RequestInterval.interval = " + interval);
                    Pause();
                    Resume();
                }
            }
            catch (Exception ex)
            {
                ExceptionHandler.Instance.GetException(ex);
            }
            finally
            {
                client.Dispose();
            }
            client.Dispose();
        }

        async public void GetLabel()
        {
            string action = "request";
            string result = "";
            HttpClient client = new HttpClient();
            try { 
                    if (previousLabel.Equals(""))
                    {
                        result = await client.GetStringAsync(protocol + host + "/repeater/retrieve_label.php?group_id=" + group + "&action=" + action + "&user_id=" + user + "&json=");
                        previousLabel = result;
                    }
            }
            catch (Exception ex)
            {
                ExceptionHandler.Instance.GetException(ex);
            }
            client.Dispose();
        }


        async public void GetAnnotationLabel()
        {
            string action = "request";
            string result = "";
            HttpClient client = new HttpClient();
            TextMesh t = new TextMesh();
            GameObject g = staticGameObject;
            label = "";
            try
            {
                //do
                //{
                    result = await client.GetStringAsync(protocol + host + "/repeater/retrieve_label.php?group_id=" + group + "&action=" + action + "&user_id=" + user + "&json=");
                    //Debug.Log("Transmitter.GetAnnotationLabel.result = " + previousLabel + " : " + result);

                    if (staticGameObject != null && result != null && (!result.Equals(previousLabel)))
                    {
                        previousLabel = result;
                        if (staticGameObject.transform.childCount > 0) g = staticGameObject.transform.GetChild(0).gameObject;
                        if (g != null) t = g.transform.GetComponent<TextMesh>();
                        if (t != null) t.text = result;
                        staticGameObject.tag = "annotation";
                        label = result;
                        Annotation.Finalize();
                    Debug.Log("Transmitter.GetAnna.label = " + staticGameObject.tag + ": " + t.text);
                    Debug.Log("Transmitter.GetAnna.staticGameObject = " + staticGameObject);
                        //lock (this)
                        //{
                            //Debug.Log("Monitor.Pulse(this); Label : " + result);
                            //Monitor.Pulse(this);
                        //}
                    }
                
                    //await Task.Delay(2000);
               // } while (t == null);
            }
            catch (Exception ex)
            {
                ExceptionHandler.Instance.GetException(ex);
            }
            client.Dispose();
           // return result;
        }

        public GameObject SetGameObject(GameObject clonedGameObject)
        {
            //Resume();
            //Pause();
            if (previousLabel.Equals("")) GetLabel();
            staticGameObject = clonedGameObject;
            return staticGameObject;
        }

        async public void Check()
        {
            try { 
                if (!label.Equals(""))
                {
                    Annotation.Finalize();
                }
            }
            catch (Exception ex)
            {
                ExceptionHandler.Instance.GetException(ex);
            }

        }

        public void SetLabel(GameObject staticGameObject)
        {
            try {
                Debug.Log("Transmitter.SetLabel.staticGameObject = " + staticGameObject);
                if (staticGameObject != null) {
                    TextMesh t = new TextMesh();
                    GameObject g = staticGameObject;
                    if (staticGameObject.transform.childCount > 0) g = staticGameObject.transform.GetChild(0).gameObject;
                    if (g != null) t = g.transform.GetComponent<TextMesh>();
                    if (t != null) t.text = " ";
                    staticGameObject.tag = "annotation";
                    label = " ";
                    Annotation.Finalize();
                }
            }
            catch (Exception ex)
            {
                ExceptionHandler.Instance.GetException(ex);
            }

        }



        async public void Recall(string action)
        {
            HttpClient client = new HttpClient();
            string result = "";
            try
            {
                result = await client.GetStringAsync(protocol + host + "/repeater/recall.php?group_id=" + group + "&action=" + action + "&user_id=" + user);
                if (int.Parse(result) > 0)
                {

                }
            }
            catch (Exception ex)
            {
                ExceptionHandler.Instance.GetException(ex);
            }
            finally
            {
                client.Dispose();
            }
            client.Dispose();
        }


        public void Pause()
        {
            try
            {
                //CancelInvoke("Request");
                CancelInvoke("Loop");
            }
            catch (Exception ex)
            {
                ExceptionHandler.Instance.GetException(ex);
            }
            finally
            {
            }
        }


        public void Resume()
        {
            try
            {
                InvokeRepeating("Loop", init_time, interval);
                //InvokeRepeating("Request", init_time, interval);
                //StartCoroutine("Request", interval);
            }
            catch (Exception ex)
            {
                ExceptionHandler.Instance.GetException(ex);
            }
            finally
            {
            }
        }


        public void Loop()
        {
            try
            {
                Request();
                //GetAnnotationLabel();
                //Check();
//                GetAnnotationLabel(staticGameObject);
            }
            catch (Exception ex)
            {
                ExceptionHandler.Instance.GetException(ex);
            }
            finally
            {
            }
        }

        public void StartRequest(GameObject go)
        {
            try
            {
                staticGameObject = go;
//                InvokeRepeating("GetAnnotationLabel", init_time, interval);
            }
            catch (Exception ex)
            {
                ExceptionHandler.Instance.GetException(ex);
            }
            finally
            {
            }
        }


        public void Clear()
        {
            //Debug.Log(this.name + " :  Clear()");
            GameObject[] allAnnotations = UnityEngine.GameObject.FindGameObjectsWithTag("annotation");
            foreach (GameObject annotation in allAnnotations)
            {
                Destroy(annotation);
            }
            previousjson = "";
        }


        public void Collect()
        {
            Debug.Log("Transmitter.Collect()");
            GameObject[] allAnnotations = UnityEngine.GameObject.FindGameObjectsWithTag("annotation");
            foreach (GameObject annotation in allAnnotations)
            {
               // annotation.transform;
            }
            
        }

        /*
                async public void Request()
                {
                    HttpClient client = new HttpClient();
                    HttpResponseMessage response = new HttpResponseMessage() ;
                    string result = "" ;
                    try
                    {
                        result = await client.GetStringAsync(protocol + host + "/repeater/index.php?group_id=mrgeo&action=request&user_id=james&json=");

                        //stringContent = new StringContent("group_id=mrgeo&action=request&user_id=james&json=");

                        //response = await client.PostAsync( protocol + host + php, stringContent);
                        //result = await response.Content.ReadAsStringAsync();
                        Debug.Log(this.name + " : Request() - " +  result );
                        if ( ! result.Equals(previousjson))
                        {
                            previousjson = result;
                            Clear();
                            JSONParser.Instance.FromJSON(result);
                        }
                    }
                    catch (Exception ex)
                    {
                        ExceptionHandler.Instance.GetException(ex);
                        client.Dispose();
                    }
                    //response.Dispose();
                    client.Dispose();

                }

        */


        async public void Request()
        {
            HttpClient client = new HttpClient();
            HttpResponseMessage response = new HttpResponseMessage();
            string result = "";
            try
            {
                string parameters = "group_id=mrgeo&action=request&user_id=james&json=";
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                var stringContent = new StringContent(parameters, Encoding.UTF8, "application/x-www-form-urlencoded");

                int retry = 3;
                do
                {
                    response = await client.PostAsync(protocol + host + php, stringContent);
                    result = await response.Content.ReadAsStringAsync();
                    //Debug.Log(this.name + " : Request() - " + result);
                    if (result != null && result != "" && !result.Equals(previousjson))
                    {
                        previousjson = result;
                        Clear();
                        JSONParser.Instance.FromJSON(result);
                    }
                } while (!response.IsSuccessStatusCode && retry-- > 0);
                if (!response.IsSuccessStatusCode)
                {
                    response.EnsureSuccessStatusCode();
                }

            }
            catch (Exception ex)
            {
                ExceptionHandler.Instance.GetException(ex);
                client.Dispose();
            }
            finally
            {
                response.Dispose();
                client.Dispose();
            }

        }



        async public void Transmit(string json)
        {
            previousjson = json;
            HttpClient client = new HttpClient();
            HttpResponseMessage response = new HttpResponseMessage();
            string result = "";
            try
            {
                string parameters = "group_id=mrgeo&action=update&user_id=james&json=";
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                var stringContent = new StringContent(parameters + json, Encoding.UTF8, "application/x-www-form-urlencoded");

                int retry = 3;
                do
                {
                    response = await client.PostAsync(protocol + host + php , stringContent);
                    result = await response.Content.ReadAsStringAsync();
                    Debug.Log(" : Transmit().result = " + result);
                } while (!response.IsSuccessStatusCode && retry-- > 0);
                if (!response.IsSuccessStatusCode)
                {
                    response.EnsureSuccessStatusCode();
                }
            }
            catch (Exception ex)
            {
                ExceptionHandler.Instance.GetException(ex);
                client.Dispose();
            }
            response.Dispose();
            client.Dispose();

        }
/*
        async public void Transmit(string json)
        {
            Debug.Log(" : Transmit() - " + json);
            previousjson = json;
            HttpClient client = new HttpClient();
            HttpResponseMessage response;
            try
            {
                string result = await client.GetStringAsync(protocol + host + "/repeater/index.php?group_id=mrgeo&action=update&user_id=james&json=" + json);
                int retry = 1;
                do
                {
                    //stringContent = new StringContent("group_id=mrgeo&action=update&user_id=james&json=" + json);
                    //response = await client.PostAsync(protocol + host + php, stringContent);
                } while ( retry-- > 0 );
            }
            catch (Exception ex)
            {
                ExceptionHandler.Instance.GetException(ex);
                client.Dispose();
            }
            client.Dispose();

        }
        */
    }
}
