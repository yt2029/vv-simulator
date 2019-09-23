using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Cameracollider : MonoBehaviour
{

  //List<GameObject> colList = new List<GameObject> ();

  public UnityEngine.UI.Text battery_score;
  public UnityEngine.UI.Text iss_score;
  public UnityEngine.UI.Text technical_score;
  public UnityEngine.UI.Text capture_score;

  public UnityEngine.UI.Text Result2;
  public int camera_collision = 0;
  public GameObject Result;

  public static int pause = 0;

  public static float battery, iss, technical, capture;//各スコア

  //float _Battery = 0;

  void Start()
  {

    Result.SetActive(false);
    pause = 0;

    //インスタンスの影響かこれはうまく行かない。
    //this.battery_score = GameObject.Find("Battery_score");
    //this.iss_score = GameObject.Find("Iss_score");
    //this.technical_score = GameObject.Find("Technical_score");
    //this.capture_score = GameObject.Find("Capture_score");

  }
  
  

  // 当たった時に呼ばれる関数
  void OnTriggerEnter(Collider collider)
  {
     
    string objectName = collider.GetComponent<Collider>().name;      
    
    if(objectName == "Body5")
    {

      Result2.text = ("キャプチャします。");
      //Debug.Log("keep the position!"); // ログを表示する          
      StartCoroutine("Clear");
      

    }
  
  
  }
  void OnTriggerExit(Collider collider)
  {
     
    string objectName = collider.GetComponent<Collider>().name;      
    
    if(objectName == "Body5")
    {

      Result2.text = ("もう１度アームを操作し、\nターゲットを円に収めてください。");
      //Debug.Log("keep the position!"); // ログを表示する          
      StopCoroutine("Clear");

    }
  
  
  }

  IEnumerator Clear()
  {

    yield return new WaitForSeconds(2);
    
    
    Result.SetActive(true);
    pause = 1;
    battery = DirectorCapture2.scoreBattery() * 1850;
    iss = 0;
    technical = 0;
    capture = (battery+iss)*technical;
    battery_score.text = string.Format("{0:0.0} Ah", battery);
    iss_score.text = string.Format("{0:0.0} Ah", iss);
    technical_score.text = string.Format("{0:0.0} point", technical);
    capture_score.text = string.Format("{0:0.0} points", capture);
    yield return new WaitForSeconds(1);
    SceneManager.LoadScene("TitleScene");
    //Result.text = "Clear!\nHTVキャプチャに成功！";
    //ChangeScene();
    //SceneManager.LoadScene("GameClear");
    //Debug.Log("Clear!");
    
    //yield break;

  }

  public static int Pause()
  {
    return pause;

  }

}
