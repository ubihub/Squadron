using UnityEngine;
using System.Collections;

public class shooting : MonoBehaviour
{
    public float bulletSpeed = 10;
    public Rigidbody bullet;


    void Fire()
    {
        Rigidbody bulletClone = (Rigidbody)Instantiate(bullet, transform.position, transform.rotation);
        bulletClone.velocity = transform.forward * bulletSpeed;
    }

    void Update()
    {
        if (Input.GetButtonDown("Fire1"))
            Fire();
    }
}