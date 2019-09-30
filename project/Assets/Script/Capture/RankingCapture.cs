using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//追加する！
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class RankingCapture : MonoBehaviour
{

    //オブジェクトと結びつける
    public InputField inputField;

    public static float _top_score, _second_score, _third_score;
    public static float _top_duration, _second_duration, _third_duration;
    float _top_dv, _second_dv, _third_dv;
    float _top_relative_v, _second_relative_v, _third_relative_v;
    string _top_name, _second_name, _third_name;
    int flag_register;
    GameObject object_register;

    void Start()
    {
        //Componentを扱えるようにする
        inputField = inputField.GetComponent<InputField>();
        object_register = GameObject.Find("CompleteRegister");
        object_register.SetActive(false);

        flag_register = 0;

        _top_score = PlayerPrefs.GetFloat("1st_SCORE", 0f);
        _top_duration = PlayerPrefs.GetFloat("1st_DURATION", 0f);
        _top_dv = PlayerPrefs.GetFloat("1st_DV", 0f);
        _top_relative_v = PlayerPrefs.GetFloat("1st_REL_V", 0f);
        _top_name = PlayerPrefs.GetString("1st_NAME", "NO NAME");

        _second_score = PlayerPrefs.GetFloat("2nd_SCORE", 0f);
        _second_duration = PlayerPrefs.GetFloat("2nd_DURATION", 0f);
        _second_dv = PlayerPrefs.GetFloat("2nd_DV", 0f);
        _second_relative_v = PlayerPrefs.GetFloat("2nd_REL_V", 0f);
        _second_name = PlayerPrefs.GetString("2nd_NAME", "NO NAME");

        _third_score = PlayerPrefs.GetFloat("3rd_SCORE", 0f);
        _third_duration = PlayerPrefs.GetFloat("3rd_DURATION", 0f);
        _third_dv = PlayerPrefs.GetFloat("3rd_DV", 0f);
        _third_relative_v = PlayerPrefs.GetFloat("3rd_REL_V", 0f);
        _third_name = PlayerPrefs.GetString("3rd_NAME", "NO NAME");

        GameObject.Find("1st_name").GetComponent<Text>().text = _top_name;
        GameObject.Find("2nd_name").GetComponent<Text>().text = _second_name;
        GameObject.Find("3rd_name").GetComponent<Text>().text = _third_name;
        GameObject.Find("1st_point").GetComponent<Text>().text = string.Format("{0:0.0}", _top_score);
        GameObject.Find("2nd_point").GetComponent<Text>().text = string.Format("{0:0.0}", _second_score);
        GameObject.Find("3rd_point").GetComponent<Text>().text = string.Format("{0:0.0}", _third_score);
        GameObject.Find("1st_sub_relativeV").GetComponent<Text>().text = string.Format("{0:0.000}", _top_relative_v);
        GameObject.Find("2nd_sub_relativeV").GetComponent<Text>().text = string.Format("{0:0.000}", _second_relative_v);
        GameObject.Find("3rd_sub_relativeV").GetComponent<Text>().text = string.Format("{0:0.000}", _third_relative_v);
        GameObject.Find("1st_sub_duration").GetComponent<Text>().text = string.Format("{0:0.0}", _top_duration);
        GameObject.Find("2nd_sub_duration").GetComponent<Text>().text = string.Format("{0:0.0}", _second_duration);
        GameObject.Find("3rd_sub_duration").GetComponent<Text>().text = string.Format("{0:0.0}", _third_duration);
        GameObject.Find("1st_sub_dv").GetComponent<Text>().text = string.Format("{0:0.0}", _top_dv);
        GameObject.Find("2nd_sub_dv").GetComponent<Text>().text = string.Format("{0:0.0}", _second_dv);
        GameObject.Find("3rd_sub_dv").GetComponent<Text>().text = string.Format("{0:0.0}", _third_dv);


        GameObject.Find("your_point").GetComponent<Text>().text = string.Format("{0:0.0}", Cameracollider.capture);
        GameObject.Find("your_dv").GetComponent<Text>().text = string.Format("{0:0.0}", Cameracollider.battery);
        GameObject.Find("your_duration").GetComponent<Text>().text = string.Format("{0:0.0}", Cameracollider.iss);
        GameObject.Find("your_relativeV").GetComponent<Text>().text = string.Format("{0:0.000}", Cameracollider.technical);
    }

    void Update()
    {
        //Detect when the Return key is pressed down
        if (Input.GetKeyDown(KeyCode.Return))
        {
            InputButtonRegister();
        }
    }

    public void InputButtonRegister()
    {

        if (flag_register == 0)
        {
            flag_register = 1;
            object_register.SetActive(true);

            if (Cameracollider.capture > _top_score)
            {
                PlayerPrefs.SetFloat("3rd_SCORE", _second_score);
                PlayerPrefs.Save();
                PlayerPrefs.SetFloat("3rd_DURATION", _second_duration);
                PlayerPrefs.Save();
                PlayerPrefs.SetFloat("3rd_DV", _second_dv);
                PlayerPrefs.Save();
                PlayerPrefs.SetFloat("3rd_REL_V", _second_relative_v);
                PlayerPrefs.Save();
                PlayerPrefs.SetString("3rd_NAME", _second_name);
                PlayerPrefs.Save();

                PlayerPrefs.SetFloat("2nd_SCORE", _top_score);
                PlayerPrefs.Save();
                PlayerPrefs.SetFloat("2nd_DURATION", _top_duration);
                PlayerPrefs.Save();
                PlayerPrefs.SetFloat("2nd_DV", _top_dv);
                PlayerPrefs.Save();
                PlayerPrefs.SetFloat("2nd_REL_V", _top_relative_v);
                PlayerPrefs.Save();
                PlayerPrefs.SetString("2nd_NAME", _top_name);
                PlayerPrefs.Save();

                PlayerPrefs.SetFloat("1st_SCORE", Cameracollider.capture);
                PlayerPrefs.Save();
                PlayerPrefs.SetFloat("1st_DURATION", Cameracollider.battery);
                PlayerPrefs.Save();
                PlayerPrefs.SetFloat("1st_DV", Cameracollider.iss);
                PlayerPrefs.Save();
                PlayerPrefs.SetFloat("1st_REL_V", Cameracollider.technical);
                PlayerPrefs.Save();
                if (inputField.text == "")
                {
                    PlayerPrefs.SetString("1st_NAME", "KOUNOTORI");
                    PlayerPrefs.Save();
                }
                else
                {
                    PlayerPrefs.SetString("1st_NAME", inputField.text);
                    PlayerPrefs.Save();
                }
            }
            else if (Cameracollider.capture > _second_score)
            {
                PlayerPrefs.SetFloat("3rd_SCORE", _second_score);
                PlayerPrefs.Save();
                PlayerPrefs.SetFloat("3rd_DURATION", _second_duration);
                PlayerPrefs.Save();
                PlayerPrefs.SetFloat("3rd_DV", _second_dv);
                PlayerPrefs.Save();
                PlayerPrefs.SetFloat("3rd_REL_V", _second_relative_v);
                PlayerPrefs.Save();
                PlayerPrefs.SetString("3rd_NAME", _second_name);
                PlayerPrefs.Save();

                PlayerPrefs.SetFloat("2nd_SCORE", Cameracollider.capture);
                PlayerPrefs.Save();
                PlayerPrefs.SetFloat("2nd_DURATION", Cameracollider.battery);
                PlayerPrefs.Save();
                PlayerPrefs.SetFloat("2nd_DV", Cameracollider.iss);
                PlayerPrefs.Save();
                PlayerPrefs.SetFloat("2nd_REL_V", Cameracollider.technical);
                if (inputField.text == "")
                {
                    PlayerPrefs.SetString("2nd_NAME", "KOUNOTORI");
                    PlayerPrefs.Save();
                }
                else
                {
                    PlayerPrefs.SetString("2nd_NAME", inputField.text);
                    PlayerPrefs.Save();
                }
            }
            else if (Cameracollider.capture > _third_score)
            {
                PlayerPrefs.SetFloat("3rd_SCORE", Cameracollider.capture);
                PlayerPrefs.Save();
                PlayerPrefs.SetFloat("3rd_DURATION", Cameracollider.battery);
                PlayerPrefs.Save();
                PlayerPrefs.SetFloat("3rd_DV", Cameracollider.iss);
                PlayerPrefs.Save();
                PlayerPrefs.SetFloat("3rd_REL_V", Cameracollider.technical);
                PlayerPrefs.Save();
                if (inputField.text == "")
                {
                    PlayerPrefs.SetString("3rd_NAME", "KOUNOTORI");
                    PlayerPrefs.Save();
                }
                else
                {
                    PlayerPrefs.SetString("3rd_NAME", inputField.text);
                    PlayerPrefs.Save();
                }
            }

            _top_score = PlayerPrefs.GetFloat("1st_SCORE", 0f);
            _top_duration = PlayerPrefs.GetFloat("1st_DURATION", 0f);
            _top_dv = PlayerPrefs.GetFloat("1st_DV", 0f);
            _top_relative_v = PlayerPrefs.GetFloat("1st_REL_V", 0f);
            _top_name = PlayerPrefs.GetString("1st_NAME", "NO NAME");

            _second_score = PlayerPrefs.GetFloat("2nd_SCORE", 0f);
            _second_duration = PlayerPrefs.GetFloat("2nd_DURATION", 0f);
            _second_dv = PlayerPrefs.GetFloat("2nd_DV", 0f);
            _second_relative_v = PlayerPrefs.GetFloat("2nd_REL_V", 0f);
            _second_name = PlayerPrefs.GetString("2nd_NAME", "NO NAME");

            _third_score = PlayerPrefs.GetFloat("3rd_SCORE", 0f);
            _third_duration = PlayerPrefs.GetFloat("3rd_DURATION", 0f);
            _third_dv = PlayerPrefs.GetFloat("3rd_DV", 0f);
            _third_relative_v = PlayerPrefs.GetFloat("3rd_REL_V", 0f);
            _third_name = PlayerPrefs.GetString("3rd_NAME", "NO NAME");

            GameObject.Find("1st_name").GetComponent<Text>().text = _top_name;
            GameObject.Find("2nd_name").GetComponent<Text>().text = _second_name;
            GameObject.Find("3rd_name").GetComponent<Text>().text = _third_name;
            GameObject.Find("1st_point").GetComponent<Text>().text = string.Format("{0:0.0}", _top_score);
            GameObject.Find("2nd_point").GetComponent<Text>().text = string.Format("{0:0.0}", _second_score);
            GameObject.Find("3rd_point").GetComponent<Text>().text = string.Format("{0:0.0}", _third_score);
            GameObject.Find("1st_sub_relativeV").GetComponent<Text>().text = string.Format("{0:0.000}", _top_relative_v);
            GameObject.Find("2nd_sub_relativeV").GetComponent<Text>().text = string.Format("{0:0.000}", _second_relative_v);
            GameObject.Find("3rd_sub_relativeV").GetComponent<Text>().text = string.Format("{0:0.000}", _third_relative_v);
            GameObject.Find("1st_sub_duration").GetComponent<Text>().text = string.Format("{0:0.0}", _top_duration);
            GameObject.Find("2nd_sub_duration").GetComponent<Text>().text = string.Format("{0:0.0}", _second_duration);
            GameObject.Find("3rd_sub_duration").GetComponent<Text>().text = string.Format("{0:0.0}", _third_duration);
            GameObject.Find("1st_sub_dv").GetComponent<Text>().text = string.Format("{0:0.0}", _top_dv);
            GameObject.Find("2nd_sub_dv").GetComponent<Text>().text = string.Format("{0:0.0}", _second_dv);
            GameObject.Find("3rd_sub_dv").GetComponent<Text>().text = string.Format("{0:0.0}", _third_dv);
        }
    }


    public void RankingShow()
    {
        _top_score = PlayerPrefs.GetFloat("1st_SCORE", 0f);
        _top_duration = PlayerPrefs.GetFloat("1st_DURATION", 0f);
        _top_dv = PlayerPrefs.GetFloat("1st_DV", 0f);
        _top_relative_v = PlayerPrefs.GetFloat("1st_REL_V", 0f);
        _top_name = PlayerPrefs.GetString("1st_NAME", "NO DATA");

        _second_score = PlayerPrefs.GetFloat("2nd_SCORE", 0f);
        _second_duration = PlayerPrefs.GetFloat("2nd_DURATION", 0f);
        _second_dv = PlayerPrefs.GetFloat("2nd_DV", 0f);
        _second_relative_v = PlayerPrefs.GetFloat("2nd_REL_V", 0f);
        _second_name = PlayerPrefs.GetString("2nd_NAME", "NO DATA");

        _third_score = PlayerPrefs.GetFloat("3rd_SCORE", 0f);
        _third_duration = PlayerPrefs.GetFloat("3rd_DURATION", 0f);
        _third_dv = PlayerPrefs.GetFloat("3rd_DV", 0f);
        _third_relative_v = PlayerPrefs.GetFloat("3rd_REL_V", 0f);
        _third_name = PlayerPrefs.GetString("3rd_NAME", "NO DATA");

    }


    public void title_scene()
    {

        GameMaster.game_scene = 0;
        Time.timeScale = 1.0f;
        SceneManager.LoadScene("TitleScene");
        //val_sim_speed = 1.0f;
        //Time.timeScale = val_sim_speed;
        //FadeManager.Instance.LoadScene("TitleScene", 0.3f);
    }


    public void mode_select_1()
    {
        GameMaster.game_scene = 1;
        SceneManager.LoadScene("GameRbar");
    }

    public void score_delete()
    {
        PlayerPrefs.DeleteAll();
        _top_score = PlayerPrefs.GetFloat("1st_SCORE", 0f);
        _top_duration = PlayerPrefs.GetFloat("1st_DURATION", 0f);
        _top_dv = PlayerPrefs.GetFloat("1st_DV", 0f);
        _top_relative_v = PlayerPrefs.GetFloat("1st_REL_V", 0f);
        _top_name = PlayerPrefs.GetString("1st_NAME", "NO DATA");

        _second_score = PlayerPrefs.GetFloat("2nd_SCORE", 0f);
        _second_duration = PlayerPrefs.GetFloat("2nd_DURATION", 0f);
        _second_dv = PlayerPrefs.GetFloat("2nd_DV", 0f);
        _second_relative_v = PlayerPrefs.GetFloat("2nd_REL_V", 0f);
        _second_name = PlayerPrefs.GetString("2nd_NAME", "NO DATA");

        _third_score = PlayerPrefs.GetFloat("3rd_SCORE", 0f);
        _third_duration = PlayerPrefs.GetFloat("3rd_DURATION", 0f);
        _third_dv = PlayerPrefs.GetFloat("3rd_DV", 0f);
        _third_relative_v = PlayerPrefs.GetFloat("3rd_REL_V", 0f);
        _third_name = PlayerPrefs.GetString("3rd_NAME", "NO DATA");

        GameObject.Find("1st_name").GetComponent<Text>().text = _top_name;
        GameObject.Find("2nd_name").GetComponent<Text>().text = _second_name;
        GameObject.Find("3rd_name").GetComponent<Text>().text = _third_name;
        GameObject.Find("1st_point").GetComponent<Text>().text = string.Format("{0:0.0}", _top_score);
        GameObject.Find("2nd_point").GetComponent<Text>().text = string.Format("{0:0.0}", _second_score);
        GameObject.Find("3rd_point").GetComponent<Text>().text = string.Format("{0:0.0}", _third_score);
        GameObject.Find("1st_sub_relativeV").GetComponent<Text>().text = string.Format("{0:0.000}", _top_relative_v);
        GameObject.Find("2nd_sub_relativeV").GetComponent<Text>().text = string.Format("{0:0.000}", _second_relative_v);
        GameObject.Find("3rd_sub_relativeV").GetComponent<Text>().text = string.Format("{0:0.000}", _third_relative_v);
        GameObject.Find("1st_sub_duration").GetComponent<Text>().text = string.Format("{0:0.0}", _top_duration);
        GameObject.Find("2nd_sub_duration").GetComponent<Text>().text = string.Format("{0:0.0}", _second_duration);
        GameObject.Find("3rd_sub_duration").GetComponent<Text>().text = string.Format("{0:0.0}", _third_duration);
        GameObject.Find("1st_sub_dv").GetComponent<Text>().text = string.Format("{0:0.0}", _top_dv);
        GameObject.Find("2nd_sub_dv").GetComponent<Text>().text = string.Format("{0:0.0}", _second_dv);
        GameObject.Find("3rd_sub_dv").GetComponent<Text>().text = string.Format("{0:0.0}", _third_dv);
    }


}