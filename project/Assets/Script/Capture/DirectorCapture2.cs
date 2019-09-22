using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class DirectorCapture2 : MonoBehaviour
{
    Text timeText;
    GameObject sim_timer;
    public static float timer, timer_after_scale;

    Slider _slider;//Batteryゲージの操作

     float _Battery = 0;

    // Start is called before the first frame update
    void Start()
    {
        this.sim_timer = GameObject.Find("Sim_Time");
        timer = 0;
        //TimedeltaCommon = 0;
        timer_after_scale = 0;

       
        _slider = GameObject.Find("Frame/BatteryFrame/BatterySlider").GetComponent<Slider>();//sliderの取得
        _Battery = DirectorCapture.getBattery();

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
            _Battery = 1;
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
}
