using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace BGC.Annotation.Basic
{

    public class JSONParser : MonoBehaviour
    {
        BgcAnnotation bgcAnnotation = BgcAnnotation.instance;
        //Annotation annotation = Annotation.Instance;
        AnnotationEntity newAnnotationEntity;

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

        //        public string ToJSON(BgcAnnotation bgcAnnotation)
        public string ToJSON(Vector3 position)
        {
            string json = "";
            try
            {
                json = JsonUtility.ToJson(position, false);
                Debug.Log("JSONParser.ToJSON.json = " + json);
                //                                json = JsonUtility.ToJson(bgcAnnotation.annotationEntity.position);

            }
            catch (Exception ex)
            {
                ExceptionHandler eh = ExceptionHandler.Instance;
                eh.GetException(ex);
            }

            return json;
        }

        public string ToJSON(BgcAnnotation bgcAnnotation)
        {
            string json = "";
            try
            {
                json = JsonUtility.ToJson(bgcAnnotation);
                Debug.Log("JSONParser.ToJson.bgcAnnotation = " + json);

            }
            catch (Exception ex)
            {
                ExceptionHandler eh = ExceptionHandler.Instance;
                eh.GetException(ex);
            }

            return json;
        }


        public string ToJSON(GameObject go)
        {
            string json = "";
            try
            {
                json = JsonUtility.ToJson(go);
                Debug.Log("JSONParser.ToJSON.go = " + json);

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
            try
            {
                bgcAnnotation = JsonUtility.FromJson<BgcAnnotation>(json);
                Annotation.SetBGCAnnotation(bgcAnnotation);
                //Debug.Log("JSONParser.FromJSON.Count= "  + bgcAnnotation.annotationEntities.Count);
                foreach ( AnnotationEntity annotationEntity in bgcAnnotation.annotationEntities)
                {
                    Annotation.AnnotationTypes tempAnnotationType = (Annotation.AnnotationTypes)Enum.Parse(typeof(Annotation.AnnotationTypes), annotationEntity.type.ToLower());
                    //Debug.Log("JSONParser.FromJSON.annotation.tempAnnotationType = " + tempAnnotationType);

                    switch (tempAnnotationType)
                    {
                        case Annotation.AnnotationTypes.spot:
                            Annotation.annotationType = Annotation.AnnotationTypes.spot;
                            newAnnotationEntity = Spot.Instance.InstantiateFromEntity(annotationEntity);
                            if (newAnnotationEntity != annotationEntity)
                            {
                                bgcAnnotation.annotationEntities[bgcAnnotation.annotationEntities.FindIndex(ind => ind.Equals(annotationEntity))] = newAnnotationEntity;
                            }
                            break;
                        case Annotation.AnnotationTypes.polyline:
                            Annotation.annotationType = Annotation.AnnotationTypes.polyline;

                            break;
                        case Annotation.AnnotationTypes.polygon:

                            break;
                        case Annotation.AnnotationTypes.free:

                            break;
                        default: break;

                    }
                }
                //Vector3 position = JSONParser.Instance.JSON2Vector3(result);
                //                    Annotation annotation = Annotation.Instance;
                //                    annotation.Instantiate2(position);
            }
            catch (Exception ex)
            {
                ExceptionHandler eh = ExceptionHandler.Instance;
                eh.GetException(ex);
            }
            finally
            {
                bgcAnnotation = BgcAnnotation.Instance;
                Annotation.SetBGCAnnotation(bgcAnnotation);
            }

            return bgcAnnotation;
        }
    }
}