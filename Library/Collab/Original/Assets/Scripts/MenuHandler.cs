using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using ISelectHandler = UnityEngine.EventSystems.ISelectHandler;
using BGC.Annotation.Basic;
using System;
using HoloToolkit.Unity.SpatialMapping;

namespace BGC.Annotation.Basic
{


    public class MenuHandler : MonoBehaviour, ISelectHandler
    {
        //Annotation annotation = Annotation.Instance;

        void Start()
        {
            SpatialMappingManager spatialMappingManager;
            spatialMappingManager = SpatialMappingManager.Instance;
            if ( spatialMappingManager != null ) spatialMappingManager.DrawVisualMeshes = false;

            //SpatialMappingManager.Instance.gameObject.SetActive(false);
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
                Debug.Log(this.name + " : " + tempAnnotationType );
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
                    case Annotation.AnnotationTypes.done:
                        Annotation.annotationType = Annotation.AnnotationTypes.done;
                        Annotation.SetMode("creation");
                        //gameObject.SendMessage("SetMode", "selection");
                        Annotation.Done();
                        //this.gameObject.GetComponent<Renderer>().
                        break;
                    case Annotation.AnnotationTypes.undo:
                        Debug.Log("Undo");
                        if ( Annotation.Undo() ) Transmitter.Instance.Recall("undo"); ;
                        break;
                    case Annotation.AnnotationTypes.redo:
                        Debug.Log("Redo");
                        if ( Annotation.Redo() ) Transmitter.Instance.Recall("redo"); ;
                        break;
                    case Annotation.AnnotationTypes.last:
                        Debug.Log("last");
                        Transmitter.Instance.Recall("last"); ;
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