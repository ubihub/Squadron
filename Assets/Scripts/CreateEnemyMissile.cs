using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreateEnemyMissile : MonoBehaviour
{
    public GameObject starCraft;
    public GameObject enemyMissile;
    public static bool first = true;
    static int index = 0;
    public float gameObjectSpeed = 200.0f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (index < 5)
        {
            //if (bulletClone != null && bullet != null) bulletClone.velocity = bullet.transform.forward * bulletSpeed;
            index++;
            return;
        }
        else { index = 0; }
        //Debug.Log("first");
        // Vector3 newPosition = new Vector3(gameObject.transform.position.x, gameObject.transform.position.y, gameObject.transform.position.z + 5);
        //Vector3 rot = starCraft.transform.rotation.eulerAngles;
        //rot = new Vector3(rot.x * 2, rot.y * 2, rot.z);

        Vector3 pos = enemyMissile.transform.position;
        Vector3 dir = starCraft.transform.position;
        //Vector3 zero = new Vector3(0, 0, 0);

        Vector3 targetDir = dir;
        //float angle = Vector3.Angle(pos, dir);
        //float angle2 = Vector3.Angle(targetDir, enemyMissile.transform.forward);
        //enemyMissile.transform.rotation = Quaternion.Euler(angle, angle, angle);
        Quaternion posRot = Quaternion.Euler(pos);

        Quaternion dirRot = Quaternion.Euler(targetDir.x, targetDir.y, targetDir.z);
        //transform.rotation = dirRot;
        //Debug.DrawLine(pos, dir, Color.red, Mathf.Infinity);
        //Debug.DrawRay(transform.position, dir, Color.red);
        //Vector3 v3 = (pos-dir).normalized;
        //this.gameObject.transform.rotation = Quaternion.FromToRotation(pos, dir);
        Vector3 rot = this.gameObject.transform.rotation.eulerAngles;
        //Quaternion bulletRot = Quaternion.Euler(v3);
        //Quaternion bulletRot = Quaternion.Euler(v3);
        //        Quaternion gameObjectRot = Quaternion.Euler(rot);
        //this.gameObject.transform.rotation = Quaternion.Inverse(this.gameObject.transform.rotation);
        //enemyMissile.transform.rotation = Quaternion.Inverse(enemyMissile.transform.rotation);
        //Instantiate(enemyMissile.GetComponent<Rigidbody>(), transform.position, posRot);
        //.transform.SetParent(gameObject.transform);
        //Rigidbody gameObjectClone = (Rigidbody)Instantiate(enemyMissile.GetComponent<Rigidbody>(), transform);
        //gameObjectClone.transform.SetParent(gameObject.transform);
        //gameObjectClone.velocity = gameObjectClone.transform.forward * gameObjectSpeed;
        //gameObjectClone.Destroy(gameObjectClone, 2.0f);
        //Destroy(gameObjectClone.GetComponent<Rigidbody>(), 3.0f);
        //Destroy(gameObjectClone, 2.0f);        
    }
}
