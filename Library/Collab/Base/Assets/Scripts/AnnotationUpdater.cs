using BGC.Annotation.Basic;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BGC.Annotation.Basic
{
    public class AnnotationUpdater : MonoBehaviour
    {
        public enum AnnotationTypes
        {
            undefined = 0,
            spot = 1,
            polyline = 2,
            polygon = 3,
            circle = 4,
            cube = 5,
            sphere = 6,
            free = 7,
            pointer = 8
        }

        static public AnnotationTypes annotationType { get; set; } = 0;
        /*
                var dict = new Dictionary<string, AnnotationTypes> {
                    { "Spot", AnnotationTypes.spot },
                };

                var op = dict[str];
                var op = (AnnotationTypes)Enum.Parse(typeof(AnnotationTypes), str.Replace(' ', '_'));
        */
        // Start is called before the first frame update
        void Start()
        {
            try
            {
                GameObject[] allAnnotations = UnityEngine.GameObject.FindGameObjectsWithTag("annotation");
                foreach (GameObject annotation in allAnnotations)
                {
                    Destroy(annotation);
                }
            }
            catch (Exception ex)
            {
                ExceptionHandler.Instance.GetException(ex);
            }

        }

        // Update is called once per frame
        void Update()
        {
            try {

            }
            catch (Exception ex)
            {
                ExceptionHandler.Instance.GetException(ex);
            }
        }

        public void RemakeAnnotations(BgcAnnotation bgcAnnotation)
        {
            try
            {
                AnnotationEntity annotationEntity = bgcAnnotation.annotationEntity;
                annotationType = (AnnotationTypes)Enum.Parse(typeof(AnnotationTypes), annotationEntity.type.ToLower());

                switch (annotationType)
                {
                    case AnnotationTypes.spot:

                        break;
                    case AnnotationTypes.polyline:

                        break;
                    case AnnotationTypes.polygon:

                        break;
                    case AnnotationTypes.circle:

                        break;
                    case AnnotationTypes.cube:

                        break;
                    case AnnotationTypes.sphere:

                        break;
                    case AnnotationTypes.free:

                        break;
                    default: break;

                }
            }
            catch (Exception ex)
            {
                ExceptionHandler.Instance.GetException(ex);
            }

        }
    }
}
