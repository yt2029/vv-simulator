using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bone3 : MonoBehaviour
{

    // Update is called once per frame
    void Update()
    {
        //float x = 0f;
        if(Input.GetKey(KeyCode.LeftArrow)){
            //Debug.Log (GameObject.Find("bone.003").transform.position);
            Vector3 axis1 = Vector3.right;
            Quaternion q1 = Quaternion.AngleAxis(1, axis1);
            this.transform.rotation = this.transform.rotation * q1;
        }
        if(Input.GetKey(KeyCode.RightArrow)){
            //Debug.Log (Vector3.forward);
            Vector3 axis1 = Vector3.right;
            Quaternion q1 = Quaternion.AngleAxis(-1, axis1);
            this.transform.rotation = this.transform.rotation * q1;           
        }
    }
}
