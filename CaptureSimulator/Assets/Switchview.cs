using System.Collections;
using System.Collections.Generic;
using UnityEngine;
 
//https://mogi0506.com/unity-camera-change/

public class Switchview : MonoBehaviour {
 
    //カメラを格納する変数
    public Camera MainCamera;
    public Camera overview;
 
    //キャンバスを格納
    //public GameObject Canvas;
 
    // Use this for initialization
    void Start () {
 
        //初めはサブカメラをオフにしておく
        overview.enabled = false;
        MainCamera.enabled = true;
    }
     
    // Update is called once per frame
    void Update () {
         if (Input.GetKeyDown(KeyCode.Space)) {
            //もしサブカメラがオフだったら
            if (!overview.enabled)
            {
                //サブカメラをオンにして
                overview.enabled = true;
    
                //カメラをオフにする
                MainCamera.enabled = false;
    
                //キャンバスを映すカメラをサブカメラオブジェクトにする
                //Canvas.GetComponent<Canvas>().worldCamera = overview;
            }
            //もしサブカメラがオンだったら
            else
            {
                //サブカメラをオフにして
                overview.enabled = false;
    
                //カメラをオンにする
                MainCamera.enabled = true;
    
                //キャンバスを映すカメラをカメラオブジェクトにする
                //Canvas.GetComponent<Canvas>().worldCamera = MainCamera;
            }
        }
    }
 
    /*//ボタンを押した時の処理
    public void PushButton()
    {
        if (Input.GetKeyDown(KeyCode.Space)) {
            //もしサブカメラがオフだったら
            if (!overview.enabled)
            {
                //サブカメラをオンにして
                overview.enabled = true;
    
                //カメラをオフにする
                MainCamera.enabled = false;
    
                //キャンバスを映すカメラをサブカメラオブジェクトにする
                //Canvas.GetComponent<Canvas>().worldCamera = overview;
            }
            //もしサブカメラがオンだったら
            else
            {
                //サブカメラをオフにして
                overview.enabled = false;
    
                //カメラをオンにする
                MainCamera.enabled = true;
    
                //キャンバスを映すカメラをカメラオブジェクトにする
                //Canvas.GetComponent<Canvas>().worldCamera = MainCamera;
            }
        }
    }*/
}