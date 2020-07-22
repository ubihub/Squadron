using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RadarController : MonoBehaviour
{
    // Start is called before the first frame update
    RectTransform RadarRectTransform;
    void Start()
    {
        RadarRectTransform = this.gameObject.GetComponent<RectTransform>();
    }

    // Update is called once per frame
    void Update()
    {
        RadarRectTransform.Rotate(Vector3.back * Time.deltaTime * 90);
    }
}
