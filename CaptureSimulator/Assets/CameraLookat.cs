using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraLookat : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    public Transform LookTarget;

    void Update(){
        transform.LookAt(LookTarget);
        
        
    }
}
