using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class DirectorCapture2 : MonoBehaviour
{
    Text timeText;
    Slider _slider;//Batteryゲージの操作

    GameObject sim_timer;
    GameObject Message;

    public GameObject Failure;
    //public static float timer, timer_after_scale;

    public static int getpause;
    public static float _Battery = 0;

    // Start is called before the first frame update
    void Start()
    {
        this.sim_timer = GameObject.Find("Sim_Time");
        this.Message = GameObject.Find("Message");
        

        //timer = 0;
        //TimedeltaCommon = 0;
        //timer_after_scale = 0;

      　Failure.SetActive(false);

        _slider = GameObject.Find("Frame/BatteryFrame/BatterySlider").GetComponent<Slider>();//sliderの取得
        _Battery = DirectorCapture.getBattery();

    }

    // Update is called once per frame
    void FixedUpdate()
    {

        // Time Controller
        //elappsedTime += Time.deltaTime; //[sec]
        Director.timer += Time.deltaTime;
        Director.timer_after_scale = Director.timer * Param.Co.TIME_SCALE_COMMON;
        Director.timer_after_scale_offset = Director.timer_after_scale + 60 * 60 * 10;

        // Time View
        int days = Mathf.FloorToInt(Director.timer_after_scale_offset / 60F / 60F / 24F);
        int hours = Mathf.FloorToInt(Director.timer_after_scale_offset / 60F / 60F - days * 24);
        int minutes = Mathf.FloorToInt(Director.timer_after_scale_offset / 60f - hours * 60 - days * 24 * 60);
        int seconds = Mathf.FloorToInt(Director.timer_after_scale_offset - minutes * 60 - hours * 60 * 60 - days * 24 * 60 * 60);
        string niceTime = string.Format("{0:00}:{1:00}:{2:00}", hours, minutes, seconds);
        this.sim_timer.GetComponent<Text>().text = "" + niceTime;

        getpause = Cameracollider.Pause();
        if(getpause == 1){
            Time.timeScale = 0;
        }

        _Battery -= 0.001f;
        if(_Battery < 0)
        {
            this.Message.GetComponent<Text>().text = "HTVのバッテリが尽きてしまった...\nバッテリ残量に気をつけよう。";
            Failure.SetActive(true);
        }

        _slider.value = _Battery;


    }

    public void title_scene()
    {
        SceneManager.LoadScene("TitleScene");
    }

    public void reload_scene()
    {
 
        SceneManager.LoadScene("GameCapture2");

    }

    public static float scoreBattery()
    {
        return _Battery;

    }

    public void ranking_scene()
    {

        Time.timeScale = 1.0f;
        SceneManager.LoadScene("ShowRankIntegrated");
        //val_sim_speed = 1.0f;
        //Time.timeScale = val_sim_speed;
        //FadeManager.Instance.LoadScene("TitleScene", 0.3f);
    }

}
