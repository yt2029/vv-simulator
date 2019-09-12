using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoneSize : MonoBehaviour
{
    // Update is called once per frame

    

    void Update()
    {
        if(Input.GetKey(KeyCode.LeftArrow)){
            //Debug.Log(GameObject.Find("bone.003").transform.position);
            //Vector3 axis1 = Vector3.right;
            //Quaternion q1 = Quaternion.AngleAxis(1, axis1);
            //this.transform.rotation = this.transform.rotation * q1;
            Transform myTransform = this.transform;
            Vector3 worldPos = myTransform.position;
            float x = worldPos.x;
            float y = worldPos.y;
            float z = worldPos.z; 
            Debug.Log(x);
            Debug.Log(y);
            Debug.Log(z);
        }



    }
}
