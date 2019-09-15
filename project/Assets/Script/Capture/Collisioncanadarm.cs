using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collisioncanadarm : MonoBehaviour
{

  //List<GameObject> colList = new List<GameObject> ();

  public UnityEngine.UI.Text Result;

  // 当たった時に呼ばれる関数
  void OnCollisionEnter(Collision collision)
  {
     
    string objectName = collision.collider.gameObject.name;      
    
    if(objectName == "FRGF v4")
    {

      Result.text = ("3秒間維持しよう！");
      //Debug.Log("keep the position!"); // ログを表示する          
      StartCoroutine("Time");

    }
  
  
  }
  void OnCollisionExit(Collision collision)
  {
    
    string objectName = collision.collider.gameObject.name;

    if(objectName == "FRGF v4")
    {

      Result.text = "もう一度!";
      //Debug.Log("Try again");
      StopCoroutine("Time");

    }

  }

  IEnumerator Time()
  {

    yield return new WaitForSeconds(3);
    Result.text = "Clear!\nHTVキャプチャに成功！";
    //Debug.Log("Clear!");

  }

}
