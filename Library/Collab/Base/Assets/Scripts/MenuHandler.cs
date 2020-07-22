using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using ISelectHandler = UnityEngine.EventSystems.ISelectHandler;
using BGC.Annotation.Basic;
using System;


namespace BGC.Annotation.Basic
{

    public class MenuHandler : MonoBehaviour, ISelectHandler
    {
        Annotation annotation;
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
            pointer = 8,
            done = 20,
            cancel = 21,
            undo = 22,
            redo = 23
        }

        static public AnnotationTypes annotationType { get; set; } = 0;
        // Start is called before the first frame update
        void Start()
        {
        }

        // Update is called once per frame
        void Update()
        {

        }

        public void OnSelect(BaseEventData eventData)
        {
            try
            {
                annotation = Annotation.Instance;
                Spot sp = Spot.Instance;
                annotationType = (AnnotationTypes)Enum.Parse(typeof(AnnotationTypes), this.gameObject.name.ToLower());

                switch (annotationType)
                {
                    case AnnotationTypes.spot:
                        annotation.annotationType = Annotation.AnnotationTypes.spot;
                        annotation.ChangeMode("make");
                        sp.annotationType = Annotation.AnnotationTypes.spot;
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