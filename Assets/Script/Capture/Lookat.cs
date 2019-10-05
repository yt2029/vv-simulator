using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lookat : MonoBehaviour
{

    GameObject FRGF_pin;
    GameObject Camera;

    public Transform LookTarget;

    // Start is called before the first frame update
    void Start()
    {
        this.FRGF_pin = GameObject.Find("Target_pin");
        this.Camera = GameObject.Find("Camera");
    }


    void Update(){

        Vector3 Target_pin_position = FRGF_pin.transform.position;
        Target_pin_position.x = Camera.transform.position.x;
        Camera.transform.LookAt(Target_pin_position);
        
        /*
        transform.LookAt(LookTarget);
        Quaternion q = Quaternion.Euler(90f, 0f, 0f); // クォータニオン q
        this.transform.rotation = this.transform.rotation * q; // 回転
        */
        
    }
}
