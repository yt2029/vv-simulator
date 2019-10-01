using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMove : MonoBehaviour
{

    public float speed = 0.05f;
    public static float arm_move_time = 0f;

    // Start is called before the first frame update
    void Start()
    {

        arm_move_time = Bone3IKxy.get_arm_move_time_buf();
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.UpArrow)&& Input.GetKey("left shift"))//前進
        {
            transform.position += transform.forward * speed;
            arm_move_time += 1;
        }
        if (Input.GetKey(KeyCode.DownArrow)&& Input.GetKey("left shift"))//後退
        {
            transform.position -= transform.forward * speed;
            arm_move_time += 1;
        }
        if (Input.GetKey(KeyCode.RightArrow))//右移動
        {
            transform.position += transform.right * speed;
            arm_move_time += 1;
        }
        if (Input.GetKey(KeyCode.LeftArrow))//左移動
        {
            transform.position -= transform.right * speed;
            arm_move_time += 1;
        }
        if (Input.GetKey(KeyCode.UpArrow) && !Input.GetKey("left shift"))//上移動
        {
            transform.position += transform.up * speed;
            arm_move_time += 1;
        }
        if (Input.GetKey(KeyCode.DownArrow) && !Input.GetKey("left shift"))//左移動
        {
            transform.position -= transform.up * speed;
            arm_move_time += 1;
        }
    }
    public static float get_arm_move_time()
    {
        return arm_move_time;

    }
}
