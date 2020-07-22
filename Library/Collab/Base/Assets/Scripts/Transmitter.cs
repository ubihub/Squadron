using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Net.Http;
using System;

namespace BGC.Annotation.Basic
{

    public class Transmitter : MonoBehaviour
    {
        string host = "localhost";
        
        string json = "";
        private Transmitter()
        {
            InvokeRepeating("Request", 1.0f, 3.0f);
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
            InvokeRepeating("Request", 1.0f, 3.0f);
        }

        // Update is called once per frame
        void Update()
        {

        }

        async public void Request()
        {
            HttpClient client = new HttpClient();
            try
            {
                string result = await client.GetStringAsync("http://localhost/repeater/index.php?group_id=mrgeo&action=request&user_id=james&json=");
            if (!result.Equals(json))
            {
                //            JSONParser jp = JSONParser.Instance;
                //            jp.parse(json);
            }
            }
            catch (Exception ex)
            {
                ExceptionHandler.Instance.GetException(ex);
            } 
            client.Dispose();
        }

        async public void Transmit(string json)
        {
            this.json = json;
            HttpClient client = new HttpClient();
            try{
                string result = await client.GetStringAsync("http://localhost/repeater/index.php?group_id=mrgeo&action=update&user_id=james&json=" + json);
                string re = result;
            }
            catch (Exception ex)
            {
                ExceptionHandler.Instance.GetException(ex);
            }
            client.Dispose();

        }
    }
}
