//// GameMaster.cs
//// Sceneの遷移をコントロールする。


using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameMaster : MonoBehaviour
{

    public static int game_scene;

    static public float c_time, c_dv;
    static public int flag_orbital_line;

    // Start is called before the first frame update
    void Start()
    {
        Time.timeScale = 1.0f;
        game_scene = 0;

        c_time = PlayerPrefs.GetFloat("C_TIME", 1f);
        c_dv = PlayerPrefs.GetFloat("C_DV", 1f);
        flag_orbital_line = PlayerPrefs.GetInt("FLAG_ORBITAL_LINE", 0);
    }

    // Update is called once per frame
    void Update()
    {
    }


    public void mode_select_1()
    {
        game_scene = 1;
        //SceneManager.LoadScene("GameRbar");
        FadeManager.Instance.LoadScene("GameRbar", 0.5f);
    }


    public void mode_select_2()
    {
        game_scene = 2;
        //SceneManager.LoadScene("GameProx");
        FadeManager.Instance.LoadScene("GameProx", 0.5f);
    }


    public void mode_select_3()
    {
        game_scene = 3;
        //SceneManager.LoadScene("GameCapture");
        FadeManager.Instance.LoadScene("GameCapture", 0.5f);
    }

    public void mode_select_4()
    {
        game_scene = 4;
        //SceneManager.LoadScene("GameCapture");
        FadeManager.Instance.LoadScene("GameDocking", 0.5f);
    }



    public void mode_select_9()
    {
        game_scene = 9;
        //SceneManager.LoadScene("GameProx");
        FadeManager.Instance.LoadScene("HowToPlay", 0.2f);
    }


    public void mode_select_config()
    {
        game_scene = 99;
        //SceneManager.LoadScene("GameProx");
        FadeManager.Instance.LoadScene("Config", 0.2f);
    }

    public void title_scene()
    {

        game_scene = 0;
        Time.timeScale = 1.0f;
        SceneManager.LoadScene("TitleScene");
        //val_sim_speed = 1.0f;
        //Time.timeScale = val_sim_speed;
        //FadeManager.Instance.LoadScene("TitleScene", 0.3f);
    }

}
