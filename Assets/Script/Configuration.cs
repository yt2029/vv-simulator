using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Configuration : MonoBehaviour
{

    GameObject slider_time, slider_dv, toggle_orbital_line, slider_robo;
    GameObject info_val_c_time, info_val_c_dv, object_register, info_val_robo;
    int flag_register;


    // Start is called before the first frame update
    void Start()
    {

        //Componentを扱えるようにする
        object_register = GameObject.Find("CompleteRegister");
        object_register.SetActive(false);

        flag_register = 0;

        this.slider_time = GameObject.Find("slider_time");
        this.slider_dv = GameObject.Find("slider_dv");
        this.info_val_c_time = GameObject.Find("c_t");
        this.info_val_c_dv = GameObject.Find("c_dv");
        this.info_val_robo = GameObject.Find("c_arm_speed");
        this.toggle_orbital_line = GameObject.Find("OrbitalLineToggle");
        this.slider_robo = GameObject.Find("slider_arm_speed");

        GameMaster.c_time = PlayerPrefs.GetFloat("C_TIME", 1f);
        GameMaster.c_dv = PlayerPrefs.GetFloat("C_DV", 1f);
        GameMaster.c_arm_speed = PlayerPrefs.GetFloat("C_ARM_SPEED", 1f);
        GameMaster.flag_orbital_line = PlayerPrefs.GetInt("FLAG_ORBITAL_LINE", 1);

        this.slider_time.GetComponent<Slider>().value = Mathf.Log10(GameMaster.c_time);
        this.slider_dv.GetComponent<Slider>().value = Mathf.Log10(GameMaster.c_dv);
        this.slider_robo.GetComponent<Slider>().value = GameMaster.c_arm_speed;

        if (GameMaster.flag_orbital_line == 1)
        {
            this.toggle_orbital_line.GetComponent<Toggle>().isOn = true;
        }
        else if (GameMaster.flag_orbital_line == 0)
        {
            this.toggle_orbital_line.GetComponent<Toggle>().isOn = false;
        }
    }

    // Update is called once per frame
    void Update()
    {

        GameMaster.c_time = Mathf.Pow(10, this.slider_time.GetComponent<Slider>().value);
        GameMaster.c_dv = Mathf.Pow(10, this.slider_dv.GetComponent<Slider>().value);
        GameMaster.c_arm_speed = this.slider_robo.GetComponent<Slider>().value;

        this.info_val_c_time.GetComponent<Text>().text = string.Format("x{0:0.0}", GameMaster.c_time);
        this.info_val_c_dv.GetComponent<Text>().text = string.Format("x{0:0.0}", GameMaster.c_dv);
        this.info_val_robo.GetComponent<Text>().text = string.Format("x{0:0.0}", GameMaster.c_arm_speed);

    }


    public void title_scene()
    {

        Time.timeScale = 1.0f;
        SceneManager.LoadScene("TitleScene");
        //val_sim_speed = 1.0f;
        //Time.timeScale = val_sim_speed;
        //FadeManager.Instance.LoadScene("TitleScene", 0.3f);
    }


    public void config_reset()
    {

        PlayerPrefs.DeleteKey("C_TIME");
        PlayerPrefs.DeleteKey("C_DV");
        PlayerPrefs.DeleteKey("FLAG_ORBITAL_LINE");
        PlayerPrefs.DeleteKey("C_ARM_SPEED");
        this.slider_time.GetComponent<Slider>().value = 0f;
        this.slider_dv.GetComponent<Slider>().value = 0f;
        this.toggle_orbital_line.GetComponent<Toggle>().isOn = true;
        this.slider_robo.GetComponent<Slider>().value = 1f;
        flag_register = 0;
    }

    public void config_register()
    {
        PlayerPrefs.SetFloat("C_TIME", GameMaster.c_time);
        PlayerPrefs.Save();
        PlayerPrefs.SetFloat("C_DV", GameMaster.c_dv);
        PlayerPrefs.Save();
        PlayerPrefs.SetFloat("C_ARM_SPEED", GameMaster.c_arm_speed);
        PlayerPrefs.Save();

        if (this.toggle_orbital_line.GetComponent<Toggle>().isOn == false)
        {
            GameMaster.flag_orbital_line = 0;
        }
        else if (this.toggle_orbital_line.GetComponent<Toggle>().isOn == true)
        {
            GameMaster.flag_orbital_line = 1;
        }

        PlayerPrefs.SetInt("FLAG_ORBITAL_LINE", GameMaster.flag_orbital_line);
        PlayerPrefs.Save();

        if (flag_register == 0)
        {
            flag_register = 1;
            object_register.SetActive(true);
        }
    }
}
