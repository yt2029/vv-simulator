using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMove : MonoBehaviour
{

    public float speed = 0.05f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.UpArrow)&& Input.GetKey("left shift"))//前進
        {
            transform.position += transform.forward * speed;
        }
        if (Input.GetKey(KeyCode.DownArrow)&& Input.GetKey("left shift"))//後退
        {
            transform.position -= transform.forward * speed;
        }
        if (Input.GetKey(KeyCode.RightArrow))//右移動
        {
            transform.position += transform.right * speed;
        }
        if (Input.GetKey(KeyCode.LeftArrow))//左移動
        {
            transform.position -= transform.right * speed;
        }
        if (Input.GetKey(KeyCode.UpArrow) && !Input.GetKey("left shift"))//上移動
        {
            transform.position += transform.up * speed;
        }
        if (Input.GetKey(KeyCode.DownArrow) && !Input.GetKey("left shift"))//左移動
        {
            transform.position -= transform.up * speed;
        }
    }
}
