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

        public GameObject spotObectPrefab;

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
        public GameObject InstantiateFromEventData(InputClickedEventData eventData)
        {
            RaycastHit hitInfo;
            
            if (Physics.Raycast(
                 Camera.main.transform.position,
                 Camera.main.transform.forward,
                 out hitInfo,
                 Mathf.Infinity,
                 Physics.DefaultRaycastLayers))
            {
                Instantiate(spotObectPrefab, new Vector3(hitInfo.transform.position.x, hitInfo.collider.transform.position.y, hitInfo.transform.position.z), Quaternion.identity);
                spotObectPrefab.transform.position = hitInfo.point;
            }
            return spotObectPrefab;

            // throw new System.NotImplementedException();
        }

        public AnnotationEntity MakeAnnotationEntity()
        {
            
            throw new System.NotImplementedException();
        }

        public GameObject InstantiateFromEntity(AnnotationEntity annotationEntity)
        {
            GameObject go;
            Vector3 position = new Vector3(annotationEntity.position.x, annotationEntity.position.y, annotationEntity.position.z);
            Quaternion quaternion = new Quaternion(annotationEntity.rotation.x, annotationEntity.rotation.y, annotationEntity.rotation.z, annotationEntity.rotation.w);
            {
                go =  Instantiate(spotObectPrefab, position, quaternion);
                go.tag = tag ;
                
             }
            return go;
        }

        // Start is called before the first frame update
        void Start()
        {

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