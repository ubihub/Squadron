using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AirshipAds : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //transform.RotateAround(Vector3.zero, Vector3.zero, 5 * Time.deltaTime);
        transform.Rotate(Vector3.up * Time.deltaTime * 15);
    }
}
