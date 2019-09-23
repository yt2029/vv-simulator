using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Bone3IK : MonoBehaviour
{

    float theta1 = 0f;//bone003の計算角度。
    float thetaans = 0f;//Quaternionで動かす角度。
    //float theta2 = 0f;//bone004の計算角度。
    float x = 0f;//エンドエフェクタ相対座標初期値:x
    float y = 0.56795f;//エンドエフェクタ相対座標初期値:y
    //float z = 39.23206f;//エンドエフェクタ座標初期値:z
    float L1 = 0.28398f;//bone003のサイズ。
    float L2 = 0.28397f;//bone004のサイズ。

    float theta1buf = 90f;//初期角。
    double yx = 0.0;//Atanの計算用に初期化。 

    float PIfloat = 3.1415f;
    float PIdeg = 0f;

    void Start(){
        PIdeg = 180 / PIfloat;//rad->deg変換用係数。
    

    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKey(KeyCode.UpArrow)){//エンドエフェクタ上昇。
            if(y >= 0.0030){
                y = y - 0.0015f;
            
                yx = (y*100)/(x*100);
                //Debug.Log (GameObject.Find("bone.003").transform.position);
                //ここから逆運動の計算。
                float root1 = (float)Math.Sqrt(x*x + y*y);//rootの計算。（Mathはdouble型!）
                theta1 = (x*x + y*y + L1*L1 - L2*L2)/(2*L1*root1);//Acos内の計算。
                theta1 = (float)Math.Acos(theta1) ;//Acosの計算(rad)。
                theta1 = theta1 * PIdeg;//Acosの計算(deg)。
                float atan1 = (float)Math.Atan(yx);//atanの事前計算(rad)。
                atan1 = atan1 * PIdeg;//atanの事前計算(deg)。
                theta1 = theta1 + atan1;//theta1の計算。
                //Debug.Log(theta1);
                thetaans = theta1buf - theta1;//Quaternionの角度。
                Debug.Log(y);
                //ここからQuaternionによる回転。Ifで発散を防ぐ
                //if(theta1 <= 80){
                Vector3 axis1 = Vector3.right;
                Quaternion q1 = Quaternion.AngleAxis(thetaans, axis1);//求めたtheta1分回転。
                this.transform.rotation = this.transform.rotation * q1;
                //}
                theta1buf = theta1;//次のQuaternionに入れるためのBuf。
            }
        }
        if(Input.GetKey(KeyCode.DownArrow)){//エンドエフェクタ降下。
            if(y <= 0.56495){
                y = y + 0.0015f;
                yx = (y*100)/(x*100);
                //Debug.Log (GameObject.Find("bone.003").transform.position);
                //ここから逆運動の計算。
                float root1 = (float)Math.Sqrt(x*x + y*y);//rootの計算。（Mathはdouble型!）
                theta1 = (x*x + y*y + L1*L1 - L2*L2)/(2*L1*root1);//Acos内の計算。
                theta1 = (float)Math.Acos(theta1) ;//Acosの計算(rad)。
                theta1 = theta1 * PIdeg;//Acosの計算(deg)。
                float atan1 = (float)Math.Atan(yx);//atanの事前計算(rad)。
                atan1 = atan1 * PIdeg;//atanの事前計算(deg)。
                theta1 = theta1 + atan1;//theta1の計算。
                //Debug.Log(theta1);
                thetaans = theta1buf - theta1;//Quaternionの角度。
                Debug.Log(y);
                //ここからQuaternionによる回転。Ifで発散を防ぐ
                //if(theta1 >= 5){
                Vector3 axis1 = Vector3.right;
                Quaternion q1 = Quaternion.AngleAxis(thetaans, axis1);//求めたtheta1分回転。
                this.transform.rotation = this.transform.rotation * q1;
                //}
                theta1buf = theta1;//次のQuaternionに入れるためのBuf。      
            }
        }
        /*if(Input.GetKey(KeyCode.LeftArrow)){//エンドエフェクタISS進行方向へ。
            y = y - 0.0005f;
            yx = (y*100)/(x*100);
            //Debug.Log (GameObject.Find("bone.003").transform.position);
            //ここから逆運動の計算。
            float root1 = (float)Math.Sqrt(x*x + y*y);//rootの計算。（Mathはdouble型!）
            theta1 = (x*x + y*y + L1*L1 - L2*L2)/(2*L1*root1);//Acos内の計算。
            theta1 = (float)Math.Acos(theta1) ;//Acosの計算(rad)。
            theta1 = theta1 * PIdeg;//Acosの計算(deg)。
            float atan1 = (float)Math.Atan(yx);//atanの事前計算(rad)。
            atan1 = atan1 * PIdeg;//atanの事前計算(deg)。
            theta1 = theta1 + atan1;//theta1の計算。
            //Debug.Log(theta1);
            thetaans = theta1buf - theta1;//Quaternionの角度。
            Debug.Log(thetaans);
            //ここからQuaternionによる回転。Ifで発散を防ぐ
            //if(thetaans >= 0.468){
                Vector3 axis1 = Vector3.right;
                Quaternion q1 = Quaternion.AngleAxis(thetaans, axis1);//求めたtheta1分回転。
                this.transform.rotation = this.transform.rotation * q1;
            //}
            theta1buf = theta1;//次のQuaternionに入れるためのBuf。
        }*/
    }
}