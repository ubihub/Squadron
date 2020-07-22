using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using HoloToolkit.Unity.InputModule;
using UnityEngine.EventSystems;
using ISelectHandler = UnityEngine.EventSystems.ISelectHandler;
using UnityEditor;
using System;
using System.Threading.Tasks;

namespace BGC.Annotation.Basic
{

    public class Annotation : MonoBehaviour, IInputClickHandler, IManipulationHandler, ISpeechHandler, IDeselectHandler, IEventSystemHandler, IMoveHandler, IPointerDownHandler, IPointerEnterHandler, IPointerExitHandler, IPointerUpHandler, ISelectHandler
    {

        string json = "";
        string tag = "annotation";
        static bool toggle = true;
        
        RaycastHit hitInfo;
        static GameObject clonedGameObject;
        static AnnotationEntity annotationEntity;
        static BgcAnnotation bgcAnnotation = BgcAnnotation.Instance;

        static public Stack undoStack = new Stack();
        static public Stack redoStack = new Stack();
        public enum AnnotationTypes
        {
            undefined = 0,
            spot = 1,
            polyline = 2,
            polygon = 3,
            free = 7,
            pointer = 8,
            done = 20,
            cancel = 21,
            undo = 22,
            redo = 23,
            last = 24
        }

        static public AnnotationTypes annotationType { get; set; } = 0;
        public AnnotationTypes tempAnnotationType { get; set; } = 0;
        static public int count { get; set; } = 0;
        static public string mode = "selection";


        public Annotation()
        {

        }

        static public void SetBGCAnnotation(BgcAnnotation parameter)
        {
            if (bgcAnnotation != null ) bgcAnnotation = BgcAnnotation.Instance;
            bgcAnnotation = parameter;
            bgcAnnotation.datetime = System.DateTime.UtcNow.ToString();
            bgcAnnotation.group = "mrgeo";
        }
        /*
                public static Annotation instance = null;

                public static Annotation Instance
                {
                    get
                    {
                        if (instance == null)
                        {
                            instance = new Annotation();
                        }
                        return instance;
                    }
                }


        */

 /*       public static async Task<int> Send()
        {
            Transmitter.Instance.GetAnnotationLabel(null);
            Transmitter.Instance.GetAnnotationLabel(null);
            int response = await Transmitter.Instance.GetAnnotationLabel(clonedGameObject);

            return 0;
        }
*/
        public void OnInputClicked(InputClickedEventData eventData)
        {
            try {
                Transmitter.Instance.SetLabel(clonedGameObject);
                Debug.Log(this.name + " : " + annotationType);
                Debug.Log(this.name + ".mode = " + mode + " : count = " + count);
                if (mode.Equals("selection") || count <= 1) { count = 2; return; }
                switch (annotationType)
                {
                    case AnnotationTypes.spot:
                        /*Task<int> task = Send();*/
                        Spot spot = Spot.Instance;
                        Debug.Log("Annotation.OnInputClicked.clonedGameObject.tag = " + clonedGameObject);
                        clonedGameObject = spot.InstantiateFromEventData(eventData);
                        clonedGameObject = Transmitter.Instance.SetGameObject(clonedGameObject);
                        //annotationEntity = spot.MakeAnnotationEntity(clonedGameObject);
                        break;
                    case AnnotationTypes.polyline:
                            Polyline polyline = Polyline.Instance;
                            break;
                    case AnnotationTypes.polygon:

                        break;
                    case AnnotationTypes.done:
                        Done();
                        break;
                    default: break;

                } // End of Switch
                //if (clonedGameObject != null && annotationEntity != null)
                   // Finalize(clonedGameObject, annotationEntity);
                //Finalize();

            }
            catch (Exception ex)
            {
                ExceptionHandler.Instance.GetException(ex);
            }
            //        throw new System.NotImplementedException();
        }

        public void Finalize(GameObject clonedGameObject, AnnotationEntity annotationEntity)
        {
            try { 
                bgcAnnotation.annotationEntities.Add(annotationEntity);
                string json = JSONParser.Instance.ToJSON(bgcAnnotation);
                Transmitter.Instance.Transmit(json);
            }
            catch (Exception ex)
            {
                ExceptionHandler.Instance.GetException(ex);
            }
        }

        public static void Finalize()
        {
            try {
                bgcAnnotation.annotationEntities.Clear();
                GameObject[] allAnnotations = UnityEngine.GameObject.FindGameObjectsWithTag("annotation");
                foreach (GameObject annotation in allAnnotations)
                {
                    if (annotation.name.Contains("spot"))
                    {
                        annotationEntity = Spot.Instance.MakeAnnotationEntity(annotation);
                    }
                    else if (annotation.name.Contains("polyline"))
                    {

                    }
                    else
                    {

                    }
                    bgcAnnotation.annotationEntities.Add(annotationEntity);
                }
                string json = JSONParser.Instance.ToJSON(bgcAnnotation);
                Transmitter.Instance.Transmit(json);
            }
            catch (Exception ex)
            {
                ExceptionHandler.Instance.GetException(ex);
            }
        }

        public void OnManipulationCanceled(ManipulationEventData eventData)
        {
            Debug.Log("Annotation.OnManipulationCanceled()");
            //Undo.PerformUndo();
            //        throw new System.NotImplementedException();
        }

        public void OnManipulationCompleted(ManipulationEventData eventData)
        {
            Debug.Log("Annotation.OnManipulationCompleted()");
            //if (toggle) { toggle = false; Undo.PerformUndo(); }
            //else { toggle = true; Undo.PerformRedo(); }
            //Finalize();
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
            InputManager.Instance.PushModalInputHandler(this.gameObject);
        }

        // Update is called once per frame
        void Update()
        {
           /* if (Input.GetMouseButtonDown(0))
            {
                Debug.Log("Mouse is down");

                RaycastHit hitInfo = new RaycastHit();
                bool hit = Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hitInfo);
                if (hit)
                {
                    Debug.Log("Hit " + hitInfo.transform.gameObject.name);
                    if (hitInfo.transform.gameObject.tag == "annotation")
                    {
                        Debug.Log("It's working!");
                    }
                    else
                    {
                        Debug.Log("nopz");
                    }
                }
                else
                {
                    Debug.Log("No hit");
                }
            }
            */
        }

        void OnMouseOver()
        {
            Debug.Log("Mouse is over GameObject.");
        }

        void OnMouseExit()
        {
            Debug.Log("Mouse is no longer on GameObject.");
        }

        //  
        public static void Done()
        {
            try {
                //Finalize();
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


        // 
        public static bool Undo()
        {
            if (undoStack.Count == 0) return true;
            else
            {
                GameObject instance = (GameObject) undoStack.Pop();
                instance.hideFlags = HideFlags.HideInHierarchy;
                redoStack.Push(instance);
                return false;
            }
        }


        //  
        public static bool Redo()
        {
            if (redoStack.Count == 0) return true;
            else
            {
                GameObject instance = (GameObject)redoStack.Pop();
                instance.hideFlags = HideFlags.None;
                undoStack.Push(instance);
                return false;
            }
        }

        public static bool CheckMode(string mode)
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

        public static bool SetMode(string newMode)
        {
            try
            {
                Debug.Log("Annotation.SetMode.mode = " + mode + " : newMode = " + newMode);
                if (mode.Equals("selection")) count = 1;
                
                if (! mode.Equals(newMode))
                {
                    mode = newMode;
                }
               
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

        /*        public GameObject InstantiateFromEventData(GameObject clonedGameObject, InputClickedEventData eventData);
                        {
                            GameObject clonedGameObject = new GameObject();
                            try
                            {

                            }
                            catch (Exception ex)
                            {
                                ExceptionHandler eh = ExceptionHandler.Instance;
                                eh.GetException(ex);
                            }
                            return clonedGameObject;
                        }
        */
                public AnnotationEntity MakeAnnotationEntity(GameObject instance)
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
                    GameObject clonedGameObject = new GameObject();
                    try
                    {

                    }
                    catch (Exception ex)
                    {
                        ExceptionHandler eh = ExceptionHandler.Instance;
                        eh.GetException(ex);
                    }
                    return clonedGameObject;
                }
    }
}