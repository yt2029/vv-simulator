﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Bone2IKy : MonoBehaviour
{

    double theta1 = 0;//bone003のx軸計算角度。
    //double theta3 = 0;//bone003のz軸計算角度。
    double thetaans = 0;//Quaternionで動かす角度。
    //float theta2 = 0f;//bone004の計算角度。
    double x = 0;//エンドエフェクタ相対座標初期値:x
    double y = 0.56795;//エンドエフェクタ相対座標初期値:y
    double z = 0;
    double _z = 0;
    //double z = 0;//エンドエフェクタ相対座標初期値:z
    double L1 = 0.28398;//bone003のサイズ。
    double L2 = 0.28397;//bone004のサイズ。

    double theta1buf = 90;//初期角。
    //double theta3buf = -180;
    double yx = 0.0;//Atanの計算用に初期化。 
    //double zx = 0.0;//Atanの計算用に初期化。(Z軸側)

    double theta_bone2 = 0;
    public static double delta_theta = 0;

    double PIfloat = 3.1415;//PIをFloatにしようとしたやつ。float時代の名残が。。。
    double PIdeg = 0;

    public static double thetaBone2_z = 0;



    // Update is called once per frame
    void Start()
    {
        PIdeg = 180 / PIfloat;//rad->deg変換用係数。

        //アーム初期位置を設定。
        y = 0.56795 - 0.25;//エンドエフェクタ座標の指定。
        x = 0.1;
        yx = (y*100)/(x*100);
        //Debug.Log (GameObject.Find("bone.003").transform.position);
        //ここから逆運動の計算。
        double root1 = Math.Sqrt(x*x + y*y);//rootの計算。（Mathはdouble型!）
        theta1 = (x*x + y*y + L1*L1 - L2*L2)/(2*L1*root1);//Acos内の計算。
        theta1 = Math.Acos(theta1) ;//Acosの計算(rad)。
        theta1 = theta1 * PIdeg;//Acosの計算(deg)。
        double atan1 = Math.Atan2(y, x);//atanの事前計算(rad)。
        atan1 = atan1 * PIdeg;//atanの事前計算(deg)。
        theta1 = theta1 + atan1;//theta1の計算。
        //Debug.Log(theta1);
        thetaans = theta1buf - theta1;//Quaternionの角度。
        //Debug.Log(y);
        //ここからQuaternionによる回転。
        //Vector3 axis1 = Vector3.right;
        //Quaternion q1 = Quaternion.AngleAxis((float)thetaans, axis1);//求めたtheta1分回転。
        //this.transform.rotation = this.transform.rotation * q1;
        //theta1buf = theta1;//次のQuaternionに入れるためのBuf。

        theta_bone2 -= 270;

    }

    // Update is called once per frame
    void Update()
    {

        

        if (Input.GetKey(KeyCode.UpArrow) && !Input.GetKey("left shift") || ((Input.GetAxis("Vertical") > 0.1) && !Input.GetButton("Shift")))
        {//エンドエフェクタISS進行方向へ。
         //if(x*x+y*y <= 0.56495*0.56495){//発散防止のIF。
         ///////////////////////////////////////////
         //縦軸に曲げる。
         ///////////////////////////////////////////
            Vector3 axis1 = -Vector3.right;
            Quaternion q1 = Quaternion.AngleAxis(-0.05f, axis1);
            this.transform.rotation = q1 * this.transform.rotation;

            Quaternion quaternion = this.transform.rotation;

            thetaBone2_z = quaternion.eulerAngles.z;

            /* 
            x = x + 0.0015f;
            yx = (y*100)/(x*100);
            //Debug.Log (GameObject.Find("bone.003").transform.position);
            //ここから逆運動の計算。
            double root1 = Math.Sqrt(x*x + y*y);//rootの計算。（Mathはdouble型!）
            theta1 = (x*x + y*y + L1*L1 - L2*L2)/(2*L1*root1);//Acos内の計算。
            theta1 = Math.Acos(theta1) ;//Acosの計算(rad)。
            theta1 = theta1 * PIdeg;//Acosの計算(deg)。
            double atan1 = Math.Atan2(y, x);//atanの事前計算(rad)。
            atan1 = atan1 * PIdeg;//atanの事前計算(deg)。
            theta1 = (theta1 + atan1);//theta1の計算。
            //Debug.Log(theta1);
            thetaans = theta1buf - theta1;//Quaternionの角度。
            //Debug.Log("bone003 : " + theta1);
            //ここからQuaternionによる回転。
            Vector3 axis1 = Vector3.right;
            Quaternion q1 = Quaternion.AngleAxis((float)thetaans, axis1);//求めたtheta1分回転。
            this.transform.rotation = this.transform.rotation * q1;
            theta1buf = theta1;//次のQuaternionに入れるためのBuf。
            */

            //}
        }
        if (Input.GetKey(KeyCode.DownArrow) && !Input.GetKey("left shift") || ((Input.GetAxis("Vertical") < -0.1) && !Input.GetButton("Shift")))
        {//エンドエフェクタISS後退方向へ。
         //if(x*x+y*y <= 0.56495*0.56495){//発散防止のIF。

            Vector3 axis1 = -Vector3.right;
            Quaternion q1 = Quaternion.AngleAxis(0.05f, axis1);
            this.transform.rotation = q1 * this.transform.rotation;

            Quaternion quaternion = this.transform.rotation;

            thetaBone2_z = quaternion.eulerAngles.z;

            /* 
            x = x - 0.0015f;
            yx = (y*100)/(x*100);
            //Debug.Log (GameObject.Find("bone.003").transform.position);
            //ここから逆運動の計算。
            double root1 = Math.Sqrt(x*x + y*y);//rootの計算。（Mathはdouble型!）
            theta1 = (x*x + y*y + L1*L1 - L2*L2)/(2*L1*root1);//Acos内の計算。
            theta1 = Math.Acos(theta1) ;//Acosの計算(rad)。
            theta1 = theta1 * PIdeg;//Acosの計算(deg)。
            double atan1 = Math.Atan2(y, x);//atanの事前計算(rad)。
            atan1 = atan1 * PIdeg;//atanの事前計算(deg)。
            theta1 = (theta1 + atan1);//theta1の計算。
            //Debug.Log(theta1);
            thetaans = theta1buf - theta1;//Quaternionの角度。
            //Debug.Log("bone003 : " + theta1);
            //ここからQuaternionによる回転。
            Vector3 axis1 = Vector3.right;
            Quaternion q1 = Quaternion.AngleAxis((float)thetaans, axis1);//求めたtheta1分回転。
            this.transform.rotation = this.transform.rotation * q1;
            theta1buf = theta1 ;//次のQuaternionに入れるためのBuf。
            */
            //}
        }

        /*
        if (Input.GetKey(KeyCode.UpArrow) && !Input.GetKey("left shift") || ((Input.GetAxis("Vertical") > 0.1) && Input.GetButton("Shift")))
        {

            z = Math.Sqrt(Bone3IKxy.y * Bone3IKxy.y - Bone3IKxy.x * Bone3IKxy.x);
            _z = Math.Sqrt((Bone3IKxy.y - 0.001) * (Bone3IKxy.y - 0.001) - Bone3IKxy.x * Bone3IKxy.x);

            Quaternion quaternion = this.transform.rotation;
            delta_theta = Math.Atan2(Bone3IKxy.x, _z) - Math.Atan2(Bone3IKxy.x, z);
            delta_theta = delta_theta * PIdeg;



            Vector3 axis1 = -Vector3.right;
            Quaternion q1 = Quaternion.AngleAxis((float)delta_theta, axis1);
            this.transform.rotation = q1 * this.transform.rotation;


            thetaBone2_z = quaternion.eulerAngles.z;

        }
    
        
        if (Input.GetKey(KeyCode.UpArrow) && !Input.GetKey("left shift") || ((Input.GetAxis("Vertical") < -0.1) && Input.GetButton("Shift")))
        {
            z = Math.Sqrt(Bone3IKxy.y * Bone3IKxy.y - Bone3IKxy.x * Bone3IKxy.x);
            _z = Math.Sqrt((Bone3IKxy.y + 0.001) * (Bone3IKxy.y + 0.001) - Bone3IKxy.x * Bone3IKxy.x);

            Quaternion quaternion = this.transform.rotation;
            delta_theta = Math.Atan2(Bone3IKxy.x, z) - Math.Atan2(Bone3IKxy.x, _z);
            delta_theta = delta_theta * PIdeg;



            Vector3 axis1 = -Vector3.right;
            Quaternion q1 = Quaternion.AngleAxis(-(float)delta_theta, axis1);
            this.transform.rotation = q1 * this.transform.rotation;


            thetaBone2_z = quaternion.eulerAngles.z;

        }
        */
        

    }
        
}
