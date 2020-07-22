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
        //Annotation annotation = Annotation.Instance;

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

                Annotation.AnnotationTypes tempAnnotationType  = (Annotation.AnnotationTypes)Enum.Parse(typeof(Annotation.AnnotationTypes), this.gameObject.name.ToLower());
                Debug.Log(this.name + " : " + tempAnnotationType);
                Annotation.count = 0;
                switch (tempAnnotationType)
                {
                    case Annotation.AnnotationTypes.spot:
                        Annotation.annotationType = Annotation.AnnotationTypes.spot;
                        Annotation.count = 1;
                        Annotation.SetMode("creation");
                        //gameObject.SendMessage("SetMode", "drawing");
                        break;
                    case Annotation.AnnotationTypes.polyline:
                        Annotation.annotationType = Annotation.AnnotationTypes.polyline;
                        
                        break;
                    case Annotation.AnnotationTypes.polygon:

                        break;
                    case Annotation.AnnotationTypes.circle:

                        break;
                    case Annotation.AnnotationTypes.cube:

                        break;
                    case Annotation.AnnotationTypes.sphere:

                        break;
                    case Annotation.AnnotationTypes.done:
                        Annotation.annotationType = Annotation.AnnotationTypes.done;
                        Annotation.SetMode("selection");
                        //gameObject.SendMessage("SetMode", "selection");
                        Annotation.Done();
                        //this.gameObject.GetComponent<Renderer>().
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