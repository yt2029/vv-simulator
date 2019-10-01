using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Bone3IKxy : MonoBehaviour
{

    double thetaBone2 = 0;//bone002で与えられる関節角。
    double theta1 = 0;//bone003の計算角度。
    double thetaans = 0;//Quaternionで動かす角度。
    double theta2 = 0;//bone004の計算角度。
    //double theta3 = 0;
    //double theta4 = 0;
    double x = 0;//エンドエフェクタ相対座標初期値:x
    double y = 0.56795;//エンドエフェクタ相対座標初期値:y
    double y_0 = 0;//縦並進用のy軸初期値。
    double y_offset = 0.00759;//縦並進用のy軸オフセット。   
    double y_bias = 0;//縦並進用のy軸バイアス。
    //double z = 0;//エンドエフェクタ相対座標初期値:z
    double L1 = 0.28398;//bone003のサイズ。
    double L2 = 0.28397;//bone004のサイズ。

    double theta1buf = 90;//初期角。
    double theta2buf = 0;//初期角。
    double thetaBone2buf = 0;
    //double theta3buf = -180;//初期角。
    //double theta4buf = 180;
    double yx = 0.0;//Atanの計算用に初期化。 
    //double zx = 0.0;
    double theta2sin = 0;
    double theta2cos = 0;
    //double theta4sin = 0;
    //double theta4cos = 0;

    double PIfloat = 3.1415;
    double PIdeg = 0;

    public static float arm_move_time_buf = 0f;//アーム操作時間の計算。

    void Start(){
        PIdeg = 180 / PIfloat;//rad->deg変換用係数。
        y = y - 0.35;
        yx = (y*100)/(x*100);
        //Debug.Log (GameObject.Find("bone.003").transform.position);
        //ここからtheta1逆運動の計算。
        double root1 = Math.Sqrt(x*x + y*y);//rootの計算。（Mathはdouble型!）
        theta1 = (x*x + y*y + L1*L1 - L2*L2)/(2*L1*root1);//Acos内の計算。
        theta1 = Math.Acos(theta1) ;//Acosの計算(rad)。
        theta1 = theta1 * PIdeg;//Acosの計算(deg)。
        double atan1 = Math.Atan2(y, x);//atanの事前計算(rad)。
        atan1 = atan1 * PIdeg;//atanの事前計算(deg)。
        theta1 = theta1 + atan1;//theta1の計算。
        //Debug.Log(theta1);
        //ここからtheta2の計算。
        theta2sin = System.Math.Sin(theta1/PIdeg);
        theta2cos = System.Math.Cos(theta1/PIdeg);
        theta2 = Math.Atan2((y-L1*theta2sin), (x-L1*theta2cos));
        theta2 = theta2 * PIdeg;
        theta2 = theta2 - theta1;
        thetaans = theta1buf - theta1;//Quaternionの角度(変位)。
        //thetaans = -thetaans;
        //Debug.Log(y);
        //ここからQuaternionによる回転。
        Vector3 axis1 = Vector3.right;
        Quaternion q1 = Quaternion.AngleAxis((float)thetaans, axis1);//求めたtheta1分回転。
        this.transform.rotation = this.transform.rotation * q1;
        theta1buf = theta1;//次のQuaternionに入れるためのBuf。
        theta2buf = theta2;

        arm_move_time_buf = 0;

    }

    // Update is called once per frame
    void Update()
    {
        if((Input.GetKey(KeyCode.UpArrow) && Input.GetKey("left shift"))||((Input.GetAxis("Vertical")>0.1)&& Input.GetButton("Shift"))){
            if(y >= 0.0030){//発散防止のIF。
                y = y - 0.0015f;
                yx = (y*100)/(x*100);
                //Debug.Log (GameObject.Find("bone.003").transform.position);
                //ここからtheta1逆運動の計算。
                double root1 = Math.Sqrt(x*x + y*y);//rootの計算。（Mathはdouble型!）
                theta1 = (x*x + y*y + L1*L1 - L2*L2)/(2*L1*root1);//Acos内の計算。
                theta1 = Math.Acos(theta1) ;//Acosの計算(rad)。
                theta1 = theta1 * PIdeg;//Acosの計算(deg)。
                double atan1 = Math.Atan2(y, x);//atanの事前計算(rad)。
                atan1 = atan1 * PIdeg;//atanの事前計算(deg)。
                theta1 = theta1 + atan1;//theta1の計算。
                //Debug.Log(theta1);
                //ここからtheta2の計算。
                theta2sin = System.Math.Sin(theta1/PIdeg);
                theta2cos = System.Math.Cos(theta1/PIdeg);
                theta2 = Math.Atan2((y-L1*theta2sin), (x-L1*theta2cos));
                theta2 = theta2 * PIdeg;
                theta2 = theta2 - theta1;
                thetaans = theta1buf - theta1;//Quaternionの角度(変位)。
                //thetaans = -thetaans;
                //Debug.Log(y);
                //ここからQuaternionによる回転。
                Vector3 axis1 = Vector3.right;
                Quaternion q1 = Quaternion.AngleAxis((float)thetaans, axis1);//求めたtheta1分回転。
                this.transform.rotation = this.transform.rotation * q1;
                theta1buf = theta1;//次のQuaternionに入れるためのBuf。
                theta2buf = theta2;

                arm_move_time_buf += 1;
            }
        }
        if(Input.GetKey(KeyCode.DownArrow) && Input.GetKey("left shift")||((Input.GetAxis("Vertical")<-0.1)&& Input.GetButton("Shift"))){
            if(x*x+y*y <= 0.56495*0.56495){//発散防止のIF。
                y = y + 0.0015f;
                yx = (y*100)/(x*100);
                //Debug.Log (GameObject.Find("bone.003").transform.position);
                //ここからtheta1逆運動の計算。
                double root1 = Math.Sqrt(x*x + y*y);//rootの計算。（Mathはdouble型!）
                theta1 = (x*x + y*y + L1*L1 - L2*L2)/(2*L1*root1);//Acos内の計算。
                theta1 = Math.Acos(theta1) ;//Acosの計算(rad)。
                theta1 = theta1 * PIdeg;//Acosの計算(deg)。
                double atan1 = Math.Atan2(y, x);//atanの事前計算(rad)。
                atan1 = atan1 * PIdeg;//atanの事前計算(deg)。
                theta1 = theta1 + atan1;//theta1の計算。
                //Debug.Log(theta1);
                //ここからtheta2の計算。
                theta2sin = System.Math.Sin(theta1/PIdeg);
                theta2cos = System.Math.Cos(theta1/PIdeg);
                theta2 = Math.Atan2((y-L1*theta2sin), (x-L1*theta2cos));
                theta2 = theta2 * PIdeg;
                theta2 = theta2 - theta1;
                thetaans = theta1buf - theta1;//Quaternionの角度(変位)。
                //thetaans = -thetaans;
                //Debug.Log(y);
                //ここからQuaternionによる回転。
                Vector3 axis1 = Vector3.right;
                Quaternion q1 = Quaternion.AngleAxis((float)thetaans, axis1);//求めたtheta1分回転。
                this.transform.rotation = this.transform.rotation * q1;
                theta1buf = theta1;//次のQuaternionに入れるためのBuf。  
                theta2buf = theta2;

                arm_move_time_buf += 1;    
            }
        }
        if(Input.GetKey(KeyCode.LeftArrow)||((Input.GetAxis("Vertical")<-0.1)&& !Input.GetButton("Shift"))){//エンドエフェクタISS進行方向へ。
            if(x*x+y*y <= 0.56495*0.56495){//発散防止のIF。
                x = x + 0.0015f;//アーム操作速度。
                yx = (y*100)/(x*100);
                //Debug.Log (GameObject.Find("bone.003").transform.position);
                //ここからtheta1逆運動の計算。
                double root1 = Math.Sqrt(x*x + y*y);//rootの計算。（Mathはdouble型!）
                theta1 = (x*x + y*y + L1*L1 - L2*L2)/(2*L1*root1);//Acos内の計算。
                theta1 = Math.Acos(theta1) ;//Acosの計算(rad)。
                theta1 = theta1 * PIdeg;//Acosの計算(deg)。
                double atan1 = Math.Atan2(y, x);//atanの事前計算(rad)。
                atan1 = atan1 * PIdeg;//atanの事前計算(deg)。
                theta1 = theta1 + atan1;//theta1の計算。
                //Debug.Log(theta1);
                //ここからtheta2の計算。
                theta2sin = System.Math.Sin(theta1/PIdeg);
                theta2cos = System.Math.Cos(theta1/PIdeg);
                theta2 = Math.Atan2((y-L1*theta2sin), (x-L1*theta2cos));
                theta2 = theta2 * PIdeg;
                theta2 = theta2 - theta1;
                thetaans = theta1buf - theta1;//Quaternionの角度(変位)。
                //thetaans = -thetaans;
                //Debug.Log("bone004 : " + x);
                //ここからQuaternionによる回転。
                Vector3 axis1 = Vector3.right;
                Quaternion q1 = Quaternion.AngleAxis((float)thetaans, axis1);//求めたtheta1分回転。
                this.transform.rotation = this.transform.rotation * q1;
                theta1buf = theta1;//次のQuaternionに入れるためのBuf。  
                theta2buf = theta2;    

                arm_move_time_buf += 1;
            }
        }
        if(Input.GetKey(KeyCode.RightArrow)||((Input.GetAxis("Vertical")>0.1)&& !Input.GetButton("Shift"))){//エンドエフェクタISS後退方向へ。
            if(x*x+y*y <= 0.56495*0.56495){//発散防止のIF。
                x = x - 0.0015f;//アーム操作速度。
                yx = (y*100)/(x*100);
                //Debug.Log (GameObject.Find("bone.003").transform.position);
                //ここからtheta1逆運動の計算。
                double root1 = Math.Sqrt(x*x + y*y);//rootの計算。（Mathはdouble型!）
                theta1 = (x*x + y*y + L1*L1 - L2*L2)/(2*L1*root1);//Acos内の計算。
                theta1 = Math.Acos(theta1) ;//Acosの計算(rad)。
                theta1 = theta1 * PIdeg;//Acosの計算(deg)。
                double atan1 = Math.Atan2(y,x);//atanの事前計算(rad)。
                atan1 = atan1 * PIdeg;//atanの事前計算(deg)。
                theta1 = theta1 + atan1;//theta1の計算。
                //Debug.Log(theta1);
                //ここからtheta2の計算。
                theta2sin = System.Math.Sin(theta1/PIdeg);
                theta2cos = System.Math.Cos(theta1/PIdeg);
                theta2 = Math.Atan2((y-L1*theta2sin), (x-L1*theta2cos));
                theta2 = theta2 * PIdeg;
                theta2 = theta2 - theta1;
                thetaans = theta1buf - theta1;//Quaternionの角度(変位)。
                //thetaans = -thetaans;
                //Debug.Log("bone004 : " + x);
                //ここからQuaternionによる回転。
                Vector3 axis1 = Vector3.right;
                Quaternion q1 = Quaternion.AngleAxis((float)thetaans, axis1);//求めたtheta1分回転。
                this.transform.rotation = this.transform.rotation * q1;
                theta1buf = theta1;//次のQuaternionに入れるためのBuf。  
                theta2buf = theta2;    
                //Debug.Log(theta1buf);
                //Debug.Log(theta2buf);

                arm_move_time_buf += 1;
            }
        }

        if((Input.GetKey(KeyCode.UpArrow)&& !Input.GetKey("left shift"))||((Input.GetAxis("Horizontal")>0.1))){//エンドエフェクタISS進行方向へ。
            if(x*x+y_bias*y_bias <= 0.56495*0.56495){//発散防止のIF。
                ///////////////////////////////////////////
                //Bone2の角度。
                ///////////////////////////////////////////
                thetaBone2 += 0.1;
                Debug.Log(thetaBone2);
                thetaBone2 = thetaBone2 * (Math.PI / 180);

                //y = L1 * System.Math.Sin((180-theta1buf)* (Math.PI / 180)) + L2 * System.Math.Cos(Math.Abs(-theta2buf+360-theta1buf)* (Math.PI / 180));

                y_bias = (y + y_offset) / System.Math.Cos(thetaBone2);
                //y -= y_offset;
                yx = (y_bias*100)/(x*100);
                //Debug.Log (GameObject.Find("bone.003").transform.position);
                ///////////////////////////////////////////
                //ここからtheta1逆運動の計算。
                ///////////////////////////////////////////
                double root1 = Math.Sqrt(x*x + y_bias*y_bias);//rootの計算。（Mathはdouble型!）
                theta1 = (x*x + y_bias*y_bias + L1*L1 - L2*L2)/(2*L1*root1);//Acos内の計算。
                theta1 = Math.Acos(theta1) ;//Acosの計算(rad)。
                theta1 = theta1 * PIdeg;//Acosの計算(deg)。
                double atan1 = Math.Atan2(y_bias, x);//atanの事前計算(rad)。
                atan1 = atan1 * PIdeg;//atanの事前計算(deg)。
                theta1 = theta1 + atan1;//theta1の計算。
                //Debug.Log(theta1);
                ///////////////////////////////////////////
                //ここからtheta2の計算。
                ///////////////////////////////////////////
                theta2sin = System.Math.Sin(theta1/PIdeg);
                theta2cos = System.Math.Cos(theta1/PIdeg);
                theta2 = Math.Atan2((y_bias-L1*theta2sin), (x-L1*theta2cos));
                theta2 = theta2 * PIdeg;
                theta2 = theta2 - theta1;
                thetaans = theta1buf - theta1;//Quaternionの角度(変位)。
                //thetaans = -thetaans;
                //Debug.Log(y);
                ///////////////////////////////////////////
                //ここからQuaternionによる回転。
                ///////////////////////////////////////////
                Vector3 axis1 = Vector3.right;
                Quaternion q1 = Quaternion.AngleAxis((float)thetaans, axis1);//求めたtheta1分回転。
                this.transform.rotation = this.transform.rotation * q1;
                theta1buf = theta1;//次のQuaternionに入れるためのBuf。  
                theta2buf = theta2;
                
                //y -= y_offset;
                thetaBone2 = thetaBone2 * (180 / Math.PI);
                //thetaBone2buf = thetaBone2;

                arm_move_time_buf += 1;
            }
        }

        if((Input.GetKey(KeyCode.DownArrow)&& !Input.GetKey("left shift"))||((Input.GetAxis("Horizontal")<-0.1))){//エンドエフェクタISS後退方向へ。
            if(x*x+y_bias*y_bias <= 0.56495*0.56495){//発散防止のIF。
                ///////////////////////////////////////////
                //Bone2の角度。
                ///////////////////////////////////////////
                thetaBone2 -= 0.1;
                thetaBone2 = thetaBone2 * (Math.PI / 180);
                //y = L1 * System.Math.Cos((180-theta1buf)* (Math.PI / 180)) + L2 * System.Math.Cos(Math.Abs(-theta2buf+360-theta1buf)* (Math.PI / 180));
                y_bias = (y + y_offset) / System.Math.Cos(thetaBone2);
                //y -= y_offset;
                yx = (y_bias*100)/(x*100);
                //Debug.Log (GameObject.Find("bone.003").transform.position);
                ///////////////////////////////////////////
                //ここからtheta1逆運動の計算。
                ///////////////////////////////////////////
                double root1 = Math.Sqrt(x*x + y_bias*y_bias);//rootの計算。（Mathはdouble型!）
                theta1 = (x*x + y_bias*y_bias + L1*L1 - L2*L2)/(2*L1*root1);//Acos内の計算。
                theta1 = Math.Acos(theta1) ;//Acosの計算(rad)。
                theta1 = theta1 * PIdeg;//Acosの計算(deg)。
                double atan1 = Math.Atan2(y_bias, x);//atanの事前計算(rad)。
                atan1 = atan1 * PIdeg;//atanの事前計算(deg)。
                theta1 = theta1 + atan1;//theta1の計算。
                //Debug.Log(theta1);
                ///////////////////////////////////////////
                //ここからtheta2の計算。
                ///////////////////////////////////////////
                theta2sin = System.Math.Sin(theta1/PIdeg);
                theta2cos = System.Math.Cos(theta1/PIdeg);
                theta2 = Math.Atan2((y_bias-L1*theta2sin), (x-L1*theta2cos));
                theta2 = theta2 * PIdeg;
                theta2 = theta2 - theta1;
                thetaans = theta1buf - theta1;//Quaternionの角度(変位)。
                //thetaans = -thetaans;
                //Debug.Log(y);
                ///////////////////////////////////////////
                //ここからQuaternionによる回転。
                ///////////////////////////////////////////
                Vector3 axis1 = Vector3.right;
                Quaternion q1 = Quaternion.AngleAxis((float)thetaans, axis1);//求めたtheta1分回転。
                this.transform.rotation = this.transform.rotation * q1;
                theta1buf = theta1;//次のQuaternionに入れるためのBuf。  
                theta2buf = theta2;

                //y -= y_offset;
                thetaBone2 = thetaBone2 * (180 / Math.PI);

                arm_move_time_buf += 1;
            }
        }

        //////////////////////////////////////////////////////
        //ジョイコン検証。
        //////////////////////////////////////////////////////
        /*if(Input.GetAxis("Horizontal")==1)
        {

            Debug.Log ("Horizontal");

        }
        if(Input.GetAxis("Vertical")==1)
        {

            Debug.Log ("Vertical");

        }*/


    }

    public static float get_arm_move_time_buf()
    {
        return arm_move_time_buf;
    }
}