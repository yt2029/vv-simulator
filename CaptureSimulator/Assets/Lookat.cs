using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lookat : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    public Transform LookTarget;

    void Update(){
        transform.LookAt(LookTarget);
        Quaternion q = Quaternion.Euler(90f, 0f, 0f); // クォータニオン q
        this.transform.rotation = this.transform.rotation * q; // 回転
        
    }
}
