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

    public UnityEngine.UI.Text result_dv_score;
    public UnityEngine.UI.Text result_duration_score;
    public UnityEngine.UI.Text result_relativeV_score;

    public UnityEngine.UI.Text Result2;
  public int camera_collision = 0;
  public GameObject Result;

  public static int pause = 0;
  public static float miss = 0f;

  public static float battery, iss, technical, capture;//各スコア
    public static float total_score_c_r;

  //float _Battery = 0;

    void Start()
  {

    Result.SetActive(false);
    pause = 0;

    miss = Collisioncanadarm.get_miss_buf();

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

    if(objectName == "HTV")
    {
      Result2.text = ("HTVに当てちゃダメだぞ！");
      miss += 1;
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
    iss = CameraMove.get_arm_move_time();

    technical = 1 - ( miss * 0.1f );
    capture = (battery+(3000 - iss)/10)*technical;
    battery_score.text = string.Format("{0:0.0} Ah", battery);
    iss_score.text = string.Format("{0:0.0} sec", iss);
    technical_score.text = string.Format("× {0:0.0} point", technical);


        result_dv_score.text = string.Format("{0:0.0} m/sec", Director.result_dv);
        result_duration_score.text = string.Format("{0:0.0} mins", Director.result_duration);
        result_relativeV_score.text = string.Format("{0:0.000} m/sec", Director.result_relative_v);

        total_score_c_r = capture + Director.result_score;

        capture_score.text = string.Format("{0:0.0} points", total_score_c_r);

    yield return new WaitForSeconds(1);
    SceneManager.LoadScene("TitleScene");
    //Result.text = "Clear!\nHTVキャプチャに成功！";
    //ChangeScene();
    //SceneManager.LoadScene("GameClear");
    //Debug.Log("Clear!");
    
    //yield break;

  }

  public static float score_battery()
  {
    return battery;
  }

  public static float score_iss()
  {
    return iss;
  }

  public static float score_tech()
  {
    return technical;
  }

  public static float score_capture()
  {
    return capture;
  }

  public static int Pause()
  {
    return pause;

  }

}
