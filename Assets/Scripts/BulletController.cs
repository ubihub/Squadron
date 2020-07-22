using UnityEngine;
using System.Collections;

public class BulletController : MonoBehaviour
{

    // Use this for initialization
    void Start()
    {
        Destroy(this.gameObject, 20.0f);
    }

    public void Delete(float seconds)
    {
        Destroy(this.GetComponent<Rigidbody>(), 5.0f);
    }


    void OnCollisionEnter(Collision collision)
    {
        //Debug.Log("a collision occured : Missile" );

        Destroy(this.gameObject, 0.0f);
    }
}
