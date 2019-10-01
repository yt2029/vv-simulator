using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bone1 : MonoBehaviour
{

    // Update is called once per frame
    void Update()
    {
        
        if(Input.GetKey(KeyCode.UpArrow)&& !Input.GetKey("left shift")){
            //Debug.Log (Vector3.forward);
            Vector3 axis1 = -Vector3.forward;
            Quaternion q1 = Quaternion.AngleAxis(1, axis1);
            this.transform.rotation = q1 * this.transform.rotation;
        }
        if(Input.GetKey(KeyCode.DownArrow)&& !Input.GetKey("left shift")){
            //Debug.Log (Vector3.forward);
            Vector3 axis1 = -Vector3.forward;
            Quaternion q1 = Quaternion.AngleAxis(-1, axis1);
            this.transform.rotation = q1 * this.transform.rotation;           
        }
    }
}
