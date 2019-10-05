using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMove : MonoBehaviour
{

    public float speed = 0.001f;
    public static float arm_move_time = 0f;
    public static float miss_buf = 0;

    int collision_flag = 0;

    public UnityEngine.UI.Text Result;

    // Start is called before the first frame update
    void Start()
    {

        arm_move_time = Bone3IKxy.get_arm_move_time_buf();
        collision_flag = 0;


    }

    // Update is called once per frame
    void FixedUpdate()
    {

        collision_flag = Cameracollider.collision_flag;

        if (collision_flag == 0 && ((Input.GetAxis("Vertical") > 0.1) && Input.GetButton("Shift")))//前進
        {
            transform.position += transform.forward * speed * Mathf.Abs(Input.GetAxis("Vertical")) * GameMaster.c_arm_speed;
            arm_move_time += 1;
        }
        if (((Input.GetAxis("Vertical") < -0.1) && Input.GetButton("Shift"))
            || (collision_flag == 2 && ((Input.GetAxis("Vertical") < -0.1) && Input.GetButton("Shift"))))//後退
        {
            transform.position -= transform.forward * speed * Mathf.Abs(Input.GetAxis("Vertical") * GameMaster.c_arm_speed);
            arm_move_time += 1;
        }
        if ((collision_flag == 0 && ((Input.GetAxis("Horizontal") > 0.1)))
            || (collision_flag == 2 && ((Input.GetAxis("Horizontal") > 0.1))))//右移動
        {
            transform.position += transform.right * speed * Mathf.Abs(Input.GetAxis("Horizontal") * GameMaster.c_arm_speed);
            arm_move_time += 1;
        }
        if ((collision_flag == 0 && ((Input.GetAxis("Horizontal") < -0.1)))
            ||(collision_flag == 2 && ((Input.GetAxis("Horizontal") < -0.1))))//左移動
        {
            transform.position -= transform.right * speed * Mathf.Abs(Input.GetAxis("Horizontal") * GameMaster.c_arm_speed);
            arm_move_time += 1;
        }
        if ((collision_flag == 0 && ((Input.GetAxis("Vertical") > 0.1) && !Input.GetButton("Shift")))
            || (collision_flag == 2 && ((Input.GetAxis("Vertical") > 0.1) && !Input.GetButton("Shift"))))//上移動
        {
            transform.position += transform.up * speed * Mathf.Abs(Input.GetAxis("Vertical") * GameMaster.c_arm_speed);
            arm_move_time += 1;
        }
        if ((collision_flag == 0 && ((Input.GetAxis("Vertical") < -0.1) && !Input.GetButton("Shift")))
            || (collision_flag == 2 && ((Input.GetAxis("Vertical") < -0.1) && !Input.GetButton("Shift"))))//左移動
        {
            transform.position -= transform.up * speed * Mathf.Abs(Input.GetAxis("Vertical") * GameMaster.c_arm_speed);
            arm_move_time += 1;
        }
    }
    public static float get_arm_move_time()
    {
        return arm_move_time;

    }



}
