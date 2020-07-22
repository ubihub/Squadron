using System.Collections;
using System.Collections.Generic;
using HoloToolkit.Unity.InputModule;
using UnityEngine;
using UnityEngine.EventSystems;

// James look at the note I added at the bottom if you are trying to add exeptions righ now
namespace BGC.Annotation.Basic{
    /**
     * 
     * 
     * */
    public class Spot : Annotation
    {

        public GameObject spotSymbol;
        static public GameObject spotInstance;
        RaycastHit hitInfo;

        private Spot()
        {

        }

        public static Spot Instance { get { return Nested.instance; } }

        private class Nested
        {
            // Explicit static constructor to tell C# compiler
            // not to mark type as beforefieldinit
            static Nested()
            {
            }

            internal static readonly Spot instance = new Spot();
        }
        //just need this for the clone of the spot
        //return to parent after
        public GameObject InstantiateFromEventData(GameObject clonedGameObject, InputClickedEventData eventData)
        {
            if (Physics.Raycast(
                 Camera.main.transform.position,
                 Camera.main.transform.forward,
                 out hitInfo,
                 Mathf.Infinity,
                 Physics.DefaultRaycastLayers))
            {
                spotSymbol = Resources.Load("spotObject") as GameObject;
                Debug.Log("Spot : spotSymbol = " + spotSymbol);
                if (spotSymbol != null) { 
                    spotSymbol.tag = "annotation";
                    spotInstance = Instantiate(spotSymbol, hitInfo.point, Quaternion.identity);
                    spotInstance.name = "spot";
                }
            }
            return spotInstance;

        }

        public AnnotationEntity MakeAnnotationEntity(GameObject instance)
        {
            AnnotationEntity annotationEntity = new AnnotationEntity();
            annotationEntity.type = "spot";
            annotationEntity.instanceID = instance.GetInstanceID();
            Position position = new Position();
            position.x = instance.transform.position.x;
            position.y = instance.transform.position.y;
            position.z = instance.transform.position.z;
            annotationEntity.position = position;
            return annotationEntity;
            //throw new System.NotImplementedException();
        }

/*
        public void InstantiateFromPosition(Vector3 position)
        {
            {
                if (spotSymbol != null && position != null)
                Instantiate(spotSymbol, position, Quaternion.identity);

            }
        }
*/

        public AnnotationEntity InstantiateFromEntity(AnnotationEntity annotationEntity)
        {
            Vector3 position = new Vector3(annotationEntity.position.x, annotationEntity.position.y, annotationEntity.position.z);
            Quaternion quaternion = new Quaternion(annotationEntity.rotation.x, annotationEntity.rotation.y, annotationEntity.rotation.z, annotationEntity.rotation.w);
            spotSymbol = Resources.Load("spotObject") as GameObject;
            Debug.Log("Spot.InstantiateFromEntity.spotSymbol = " + spotSymbol);
            if ( spotSymbol != null)
            {
                spotSymbol.tag = "annotation";
                spotInstance = Instantiate(spotSymbol, position, quaternion);
                spotInstance.name = "spot";
                annotationEntity.instanceID = spotInstance.GetInstanceID();
            }
            return annotationEntity;
        }

        // Start is called before the first frame update
        void Start()
        {
            InputManager.Instance.PushModalInputHandler(this.gameObject);
        }

        // Update is called once per frame
        void Update()
        {
        }

        //Hi James
        //Sorry I added this because I could not test with the errors this cs file was giving us.
        public void OnDeselect(BaseEventData eventData)
        {
            throw new System.NotImplementedException();
        }

        public void OnSelect(BaseEventData eventData)
        {
            throw new System.NotImplementedException();
        }
    }
}