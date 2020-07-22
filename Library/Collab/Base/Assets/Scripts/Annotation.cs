using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using HoloToolkit.Unity.InputModule;
using UnityEngine.EventSystems;
using ISelectHandler = UnityEngine.EventSystems.ISelectHandler;
using UnityEditor;
using System;

namespace BGC.Annotation.Basic
{

    public class Annotation : MonoBehaviour, IInputClickHandler, IManipulationHandler, ISpeechHandler, IDeselectHandler, IEventSystemHandler, IMoveHandler, IPointerDownHandler, IPointerEnterHandler, IPointerExitHandler, IPointerUpHandler, ISelectHandler
    {

        string json = "";
        string mode = "open command";
        string tag = "annotation";
        RaycastHit hitInfo;
        GameObject go;

        static public Stack undoStack = new Stack();
        static public Stack redoStack = new Stack();
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

        public AnnotationTypes annotationType { get; set; } = 0;


        public Annotation()
        {

        }

        public static Annotation Instance { get
            { return Nested.instance; }
        }

        private class Nested
        {
            // Explicit static constructor to tell C# compiler
            // not to mark type as beforefieldinit
            static Nested()
            {
            }

            public static readonly Annotation instance = new Annotation();
        }

        public void OnInputClicked(InputClickedEventData eventData)
        {
            try { 
            switch (annotationType)
            {
                case AnnotationTypes.spot:
                    Spot spot = Spot.Instance;
                    go = spot.InstantiateFromEventData(eventData);
                    Undo.RegisterCreatedObjectUndo(go, "New Spot");
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
            //        throw new System.NotImplementedException();
        }

        public void OnManipulationCanceled(ManipulationEventData eventData)
        {
            //        throw new System.NotImplementedException();
        }

        public void OnManipulationCompleted(ManipulationEventData eventData)
        {
            //        throw new System.NotImplementedException();
        }

        public void OnManipulationStarted(ManipulationEventData eventData)
        {
            //        throw new System.NotImplementedException();
        }

        public void OnManipulationUpdated(ManipulationEventData eventData)
        {
            //        throw new System.NotImplementedException();
        }

        public void OnSpeechKeywordRecognized(SpeechEventData eventData)
        {
            //        throw new System.NotImplementedException();
        }

        // Start is called before the first frame update
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {

        }

        //  
        public void Done()
        {
            try { 
                undoStack.Clear();
                redoStack.Clear();
                annotationType = 0;
            }
            catch (Exception ex)
            {
                ExceptionHandler eh = ExceptionHandler.Instance;
                eh.GetException(ex);
            }
        }


        //  
        public void Cancel()
        {
            try { 
                undoStack.Clear();
                redoStack.Clear();
                annotationType = 0;
            }
            catch (Exception ex)
            {
                ExceptionHandler.Instance.GetException(ex);
            }
        }

        /*
                // 
                void Undo()
                {
                    redoStack.Push(undoStack.Pop());
                }


                //  
                void Redo()
                {
                    undoStack.Push(redoStack.Pop());
                }
        */
        public bool CheckMode(string mode)
        {
            try
            {
                if (mode == "open") return true;
                else return false;
            }
            catch (Exception ex)
            {
                ExceptionHandler eh = ExceptionHandler.Instance;
                eh.GetException(ex);
            }
            return false;
        }

        public bool ChangeMode(string mode)
        {
            try
            {
                this.mode = mode;
            }
            catch (Exception ex)
            {
                ExceptionHandler eh = ExceptionHandler.Instance;
                eh.GetException(ex);
            }
            return false;
        }

        public void OnMove(AxisEventData eventData)
        {
            //        throw new System.NotImplementedException();
        }

        public void OnPointerDown(PointerEventData eventData)
        {
            //        throw new System.NotImplementedException();
        }

        public void OnPointerEnter(PointerEventData eventData)
        {
            //        throw new System.NotImplementedException();
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            //        throw new System.NotImplementedException();
        }

        public void OnPointerUp(PointerEventData eventData)
        {
            //        throw new System.NotImplementedException();
        }

        public Coordinates GetCoordinates(Vector3 point)
        {
            Coordinates coordinates = new Coordinates();
            try
            {

            }
            catch (Exception ex)
            {
                ExceptionHandler.Instance.GetException(ex);
            }
            return coordinates;
        }

        public float GetDistance(Vector3 point1, Vector3 point2)
        {
            float distance = 0.0f;
            try
            {

            }
            catch (Exception ex)
            {
                ExceptionHandler.Instance.GetException(ex);
            }
            return distance;

        }

        public float GeAngle(Vector3 point1, Vector3 point2, Vector3 point3)
        {
            float angle = 0.0f;
            try
            {

            }
            catch (Exception ex)
            {
                ExceptionHandler.Instance.GetException(ex);
            }
            return angle;

        }


        public Coordinates GetCoordinates(Spot spot)
        {
            Coordinates coordinates = new Coordinates();
            try
            {

            }
            catch (Exception ex)
            {
                ExceptionHandler.Instance.GetException(ex);
            }
            return coordinates;
        }

        public float GetDistance(Spot spot1, Spot spot2)
        {
            float distance = 0.0f;
            try
            {

            }
            catch (Exception ex)
            {
                ExceptionHandler.Instance.GetException(ex);
            }
            return distance;

        }

        public float GeAngle(Spot spot1, Spot spot2, Spot spot3)
        {
            float angle = 0.0f;
            try
            {

            }
            catch (Exception ex)
            {
                ExceptionHandler.Instance.GetException(ex);
            }
            return angle;

        }

        public void OnDeselect(BaseEventData eventData)
        {

        }

        public void OnSelect(BaseEventData eventData)
        {

        }

        public  GameObject InstantiateFromEventData(InputClickedEventData eventData)
        {
            GameObject go = new GameObject();
            try
            {
 
            }
            catch (Exception ex)
            {
                ExceptionHandler eh = ExceptionHandler.Instance;
                eh.GetException(ex);
            }
            return go;
        }

        public  AnnotationEntity MakeAnnotationEntity()
        {
            AnnotationEntity ae = new AnnotationEntity();
            try
            {

            }
            catch (Exception ex)
            {
                ExceptionHandler eh = ExceptionHandler.Instance;
                eh.GetException(ex);
            }
            return ae;
        }

        public GameObject InstantiateFromEntity(AnnotationEntity annotationEntity)
        {
            GameObject go = new GameObject();
            try
            {

            }
            catch (Exception ex)
            {
                ExceptionHandler eh = ExceptionHandler.Instance;
                eh.GetException(ex);
            }
            return go;
        }
    }
}