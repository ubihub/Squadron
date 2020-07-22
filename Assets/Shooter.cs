using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shooter : MonoBehaviour
{
    public GameObject my_Misslel;
    public Camera my_Cam;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.Space))
        {
            var missle = Instantiate(my_Misslel,my_Cam.transform.position,Quaternion.identity);
            missle.GetComponent<Rigidbody>().velocity = my_Cam.transform.forward * 100;

        }
    }
}
