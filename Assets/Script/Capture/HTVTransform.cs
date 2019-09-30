using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class HTVTransform : MonoBehaviour
{

    double i=0.0;
    double angle = 90;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update() 
    {
 
        // transformを取得
        Transform myTransform = this.transform;
 
        // 座標を取得
        Vector3 pos = myTransform.position;

        angle += 1;

        i = Math.Sin(angle*(Math.PI / 180));


        if(i > 0)
        {

            pos.x += 0.0001f;    // x座標へ0.01加算
            pos.y += 0.0005f;    // y座標へ0.01加算
            //pos.z += 0.01f;    // z座標へ0.01加算
 
            myTransform.position = pos;  // 座標を設定
        }
        if(i < 0)
        {
            pos.x -= 0.0001f;
            pos.y -= 0.0005f;
            myTransform.position = pos;
        }
        
    }
}
