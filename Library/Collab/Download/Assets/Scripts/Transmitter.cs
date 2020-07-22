using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Net.Http;
using System;
using System.Globalization;

namespace BGC.Annotation.Basic
{

    public class Transmitter : MonoBehaviour
    {
        static string protocol = "http://";
        static string host = "10.159.23.94";
        static string php = "/repeater/index.php";
        static string group = "mrgeo";
        static string user = "James";
        static float init_time = 1.0f;
        static float interval = 1.0f;
        static string previousInterval = "";
        static StringContent stringContent ;

        static string previousjson = "";
        private  Transmitter()
        {
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
                    interval = float.Parse(result, CultureInfo.InvariantCulture.NumberFormat);
                    Debug.Log("Transmitter.RequestInterval.interval = " + interval);
                    Pause();
                    Resume();
                    Debug.Log("Transmitter.RequestInterval.InvokeRepeating" + interval);
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
                CancelInvoke("Request");
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
                InvokeRepeating("Request", 1.0f, interval);
            }
            catch (Exception ex)
            {
                ExceptionHandler.Instance.GetException(ex);
            }
            finally
            {
            }
        }

        //        public void Loop() { StartCoroutine(CallRequest(3.0f)); }
        /*
                IEnumerator CallRequest(float waitTime)
                {
                    while (true)
                    {
                        yield return new WaitForSeconds(waitTime);

                        Request();
                    }
                }

        */

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


        async public void Request()
        {
            HttpClient client = new HttpClient();
            //HttpResponseMessage response = new HttpResponseMessage() ;
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
    }
}
