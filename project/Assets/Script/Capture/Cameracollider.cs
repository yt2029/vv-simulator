using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Cameracollider : MonoBehaviour
{

  //List<GameObject> colList = new List<GameObject> ();

  public UnityEngine.UI.Text Result2;

  // 当たった時に呼ばれる関数
  void OnTriggerEnter(Collider collider)
  {
     
    string objectName = collider.GetComponent<Collider>().name;      
    
    if(objectName == "Body5")
    {

      Result2.text = ("キャプチャします。");
      //Debug.Log("keep the position!"); // ログを表示する          
      StartCoroutine("Time");
      

    }
  
  
  }
  void OnTriggerExit(Collider collider)
  {
     
    string objectName = collider.GetComponent<Collider>().name;      
    
    if(objectName == "Body5")
    {

      Result2.text = ("もう１度アームを操作し、\nターゲットを円に収めてください。");
      //Debug.Log("keep the position!"); // ログを表示する          
      StopCoroutine("Time");

    }
  
  
  }

  IEnumerator Time()
  {

    yield return new WaitForSeconds(1);
    SceneManager.LoadScene("TitleScene");
    //Result.text = "Clear!\nHTVキャプチャに成功！";
    //ChangeScene();
    //SceneManager.LoadScene("GameClear");
    //Debug.Log("Clear!");

  }

}
