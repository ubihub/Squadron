using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace BGC.Annotation.Basic
{

    public class JSONParser : MonoBehaviour
    {
        public static JSONParser Instance { get { return Nested.instance; } }

        private class Nested
        {
            // Explicit static constructor to tell C# compiler
            // not to mark type as beforefieldinit
            static Nested()
            {
            }

            internal static readonly JSONParser instance = new JSONParser();
        }


        // Start is called before the first frame update
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {

        }

        public string ToJSON(BgcAnnotation bgcAnnotation)
        {
            string json = "";
            try
            {
                json = JsonUtility.ToJson(bgcAnnotation);
            }
            catch (Exception ex)
            {
                ExceptionHandler eh = ExceptionHandler.Instance;
                eh.GetException(ex);
            }

            return json;
        }

        public BgcAnnotation FromJSON(string json)
        {
            BgcAnnotation bgcAnnotation = new BgcAnnotation();
            try
            {
                bgcAnnotation = JsonUtility.FromJson< BgcAnnotation>(json);
            }
            catch (Exception ex)
            {
                ExceptionHandler eh = ExceptionHandler.Instance;
                eh.GetException(ex);
            }

            return bgcAnnotation;
        }

    }
}