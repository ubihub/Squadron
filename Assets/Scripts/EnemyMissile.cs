using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMissile : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject starCraft;
    public GameObject enemyMissile;
    Rigidbody gameObjectClone;
    public float gameObjectSpeed = 200.0f;
    public static bool first = true;
    Vector3 temp;
    Vector3 pos;
    AudioSource sound;
    void Start()
    {
        pos = transform.position;
        temp = starCraft.transform.position;
        sound = this.gameObject.GetComponent<AudioSource>();
        sound.Pause();

        //        sound.mute = true;
    }
    private void Awake()
    {

    }

    // Update is called once per frame
    void Update()
    {
        transform.position = Vector3.MoveTowards(transform.position, temp, gameObjectSpeed * Time.deltaTime);
        if (Vector3.Distance(temp, transform.position) < 1.0f)
        {
            //Debug.Log("Reached");
            transform.position = pos;
            Start();
            //Destroy(this.gameObject);
        }
        else if (Vector3.Distance(temp, transform.position) < 2000.0f)
        {
            sound.UnPause();
            //sound.mute = false;

        } 
    }
}
