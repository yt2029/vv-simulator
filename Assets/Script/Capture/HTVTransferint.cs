using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class HTVTransferint : MonoBehaviour
{

    double i = 0.0;
    double angle = 90;

    // Start is called before the first frame update
    void Start()
    {
        // transformを取得
        Transform myTransform = this.transform;

        // 座標を取得
        Vector3 pos = myTransform.position;


        pos.x -= Random.Range(-0.05f, +0.05f);
        pos.y -= Random.Range(-0.005f, +0.005f);
        myTransform.position = pos;
    }

    // Update is called once per frame
    void Update()
    {

       

    }
}
