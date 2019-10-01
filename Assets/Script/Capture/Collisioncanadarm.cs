using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Collisioncanadarm : MonoBehaviour
{

  //List<GameObject> colList = new List<GameObject> ();

  public UnityEngine.UI.Text Result;

  public static float miss_buf = 0f;//アームをHTVに当てた回数を保存。

  void Start()
  {
    miss_buf = 0f;
  }

  // 当たった時に呼ばれる関数
  void OnCollisionEnter(Collision collision)
  {
     
    string objectName = collision.collider.gameObject.name;      
    
    if(objectName == "FRGF v4")
    {
      FadeManager.Instance.LoadScene("GameCapture2", 0.5f);

      //Result.text = ("3秒間維持しよう！");
      //Debug.Log("keep the position!"); // ログを表示する          
      //StartCoroutine("Time");

    }

    if(objectName == "HTV")
    {
      Result.text = ("HTVに当てちゃダメだぞ！");
      miss_buf += 1;
    }
  
  
  }

  public static float get_miss_buf()
  {

      return miss_buf;

  }
  /*void OnCollisionExit(Collision collision)
  {
    
    string objectName = collision.collider.gameObject.name;

    if(objectName == "FRGF v4")
    {

      Result.text = "もう一度!";
      //Debug.Log("Try again");
      //StopCoroutine("Time");

    }

  }*/

  /*IEnumerator Time()
  {

    
    
    Result.text = "Clear!\nHTVキャプチャに成功！";
    yield return new WaitForSeconds(3);
    //Debug.Log("Clear!");

  }*/

}
