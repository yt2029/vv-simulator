using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class DirectorCapture : MonoBehaviour
{
    Text timeText;
    Slider _slider;

    GameObject sim_timer;
    GameObject Message;

    public GameObject Failure;
    public static float timer, timer_after_scale;
    public static float _Battery = 1;

    // Start is called before the first frame update
    void Start()
    {
                
        this.Message = GameObject.Find("Message");
        
        ///////////////////////////////////////////////
        //時刻設定。
        this.sim_timer = GameObject.Find("Sim_Time");
        timer = 0;
        timer_after_scale = 0;
        ///////////////////////////////////////////////

        Failure.SetActive(false);

        _slider = GameObject.Find("BatteryFrame/BatterySlider").GetComponent<Slider>();//sliderの取得
    
        _Battery = 1;
    }

    

    // Update is called once per frame
    void Update()
    {


        // Time Controller
        //elappsedTime += Time.deltaTime; //[sec]
        timer += Time.deltaTime;
        timer_after_scale = timer * Param.Co.TIME_SCALE_COMMON;
        //timer_after_scale += Time.deltaTime * Param.Co.TIME_SCALE_COMMON;
        float timer_after_scale_offset = timer_after_scale + 60 * 60 * 10;
        // Time View
        int days = Mathf.FloorToInt(timer_after_scale_offset / 60F / 60F / 24F);
        int hours = Mathf.FloorToInt(timer_after_scale_offset / 60F / 60F - days * 24);
        int minutes = Mathf.FloorToInt(timer_after_scale_offset / 60f - hours * 60 - days * 24 * 60);
        int seconds = Mathf.FloorToInt(timer_after_scale_offset - minutes * 60 - hours * 60 * 60 - days * 24 * 60 * 60);
        string niceTime = string.Format("{0:00}:{1:00}:{2:00}", hours, minutes, seconds);
        this.sim_timer.GetComponent<Text>().text = "" + niceTime;

        _Battery -= 0.001f;
        if(_Battery < 0)
        {
            //Debug用。 
            //_Battery = 1;
            this.Message.GetComponent<Text>().text = "HTVのバッテリが尽きてしまった...\nバッテリ残量に気をつけよう。";
            Failure.SetActive(true);
        }

        _slider.value = _Battery;


    }

//バッテリの値を共通変数に格納。
    public static float getBattery()
    {
        return _Battery;

    }



    public void title_scene()
    {
        SceneManager.LoadScene("TitleScene");
    }

    public void reload_scene()
    {
 
        SceneManager.LoadScene("GameCapture");

    }
}
