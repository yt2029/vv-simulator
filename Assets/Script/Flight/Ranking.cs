using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//追加する！
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Ranking : MonoBehaviour
{

    //オブジェクトと結びつける
    public InputField inputField;

    public static float top_score, second_score, third_score;
    public static float top_duration, second_duration, third_duration;
    float top_dv, second_dv, third_dv;
    float top_relative_v, second_relative_v, third_relative_v;
    string top_name, second_name, third_name;
    int flag_register;
    GameObject object_register;

    void Start()
    {
        //Componentを扱えるようにする
        inputField = inputField.GetComponent<InputField>();
        object_register = GameObject.Find("CompleteRegister");
        object_register.SetActive(false);

        flag_register = 0;

        if (GameMaster.game_scene == 1)
        {
            top_score = PlayerPrefs.GetFloat("1st_SCORE", 0f);
            top_duration = PlayerPrefs.GetFloat("1st_DURATION", 0f);
            top_dv = PlayerPrefs.GetFloat("1st_DV", 0f);
            top_relative_v = PlayerPrefs.GetFloat("1st_REL_V", 0f);
            top_name = PlayerPrefs.GetString("1st_NAME", "NO NAME");

            second_score = PlayerPrefs.GetFloat("2nd_SCORE", 0f);
            second_duration = PlayerPrefs.GetFloat("2nd_DURATION", 0f);
            second_dv = PlayerPrefs.GetFloat("2nd_DV", 0f);
            second_relative_v = PlayerPrefs.GetFloat("2nd_REL_V", 0f);
            second_name = PlayerPrefs.GetString("2nd_NAME", "NO NAME");

            third_score = PlayerPrefs.GetFloat("3rd_SCORE", 0f);
            third_duration = PlayerPrefs.GetFloat("3rd_DURATION", 0f);
            third_dv = PlayerPrefs.GetFloat("3rd_DV", 0f);
            third_relative_v = PlayerPrefs.GetFloat("3rd_REL_V", 0f);
            third_name = PlayerPrefs.GetString("3rd_NAME", "NO NAME");
        }
        else if (GameMaster.game_scene == 4)
        {
            top_score = PlayerPrefs.GetFloat("1st_SCORE_D", 0f);
            top_duration = PlayerPrefs.GetFloat("1st_DURATION_D", 0f);
            top_dv = PlayerPrefs.GetFloat("1st_DV_D", 0f);
            top_relative_v = PlayerPrefs.GetFloat("1st_REL_V_D", 0f);
            top_name = PlayerPrefs.GetString("1st_NAME_D", "NO NAME");

            second_score = PlayerPrefs.GetFloat("2nd_SCORE_D", 0f);
            second_duration = PlayerPrefs.GetFloat("2nd_DURATION_D", 0f);
            second_dv = PlayerPrefs.GetFloat("2nd_DV_D", 0f);
            second_relative_v = PlayerPrefs.GetFloat("2nd_REL_V_D", 0f);
            second_name = PlayerPrefs.GetString("2nd_NAME_D", "NO NAME");

            third_score = PlayerPrefs.GetFloat("3rd_SCORE_D", 0f);
            third_duration = PlayerPrefs.GetFloat("3rd_DURATION_D", 0f);
            third_dv = PlayerPrefs.GetFloat("3rd_DV_D", 0f);
            third_relative_v = PlayerPrefs.GetFloat("3rd_REL_V_D", 0f);
            third_name = PlayerPrefs.GetString("3rd_NAME_D", "NO NAME");
        }

        GameObject.Find("1st_name").GetComponent<Text>().text = top_name;
        GameObject.Find("2nd_name").GetComponent<Text>().text = second_name;
        GameObject.Find("3rd_name").GetComponent<Text>().text = third_name;
        GameObject.Find("1st_point").GetComponent<Text>().text = string.Format("{0:0.0}", top_score);
        GameObject.Find("2nd_point").GetComponent<Text>().text = string.Format("{0:0.0}", second_score);
        GameObject.Find("3rd_point").GetComponent<Text>().text = string.Format("{0:0.0}", third_score);
        GameObject.Find("1st_sub_relativeV").GetComponent<Text>().text = string.Format("{0:0.000}", top_relative_v);
        GameObject.Find("2nd_sub_relativeV").GetComponent<Text>().text = string.Format("{0:0.000}", second_relative_v);
        GameObject.Find("3rd_sub_relativeV").GetComponent<Text>().text = string.Format("{0:0.000}", third_relative_v);
        GameObject.Find("1st_sub_duration").GetComponent<Text>().text = string.Format("{0:0.0}", top_duration);
        GameObject.Find("2nd_sub_duration").GetComponent<Text>().text = string.Format("{0:0.0}", second_duration);
        GameObject.Find("3rd_sub_duration").GetComponent<Text>().text = string.Format("{0:0.0}", third_duration);
        GameObject.Find("1st_sub_dv").GetComponent<Text>().text = string.Format("{0:0.0}", top_dv);
        GameObject.Find("2nd_sub_dv").GetComponent<Text>().text = string.Format("{0:0.0}", second_dv);
        GameObject.Find("3rd_sub_dv").GetComponent<Text>().text = string.Format("{0:0.0}", third_dv);


        GameObject.Find("your_point").GetComponent<Text>().text = string.Format("{0:0.0}", Director.result_score);
        GameObject.Find("your_dv").GetComponent<Text>().text = string.Format("{0:0.0}", Director.result_dv);
        GameObject.Find("your_duration").GetComponent<Text>().text = string.Format("{0:0.0}", Director.result_duration);
        GameObject.Find("your_relativeV").GetComponent<Text>().text = string.Format("{0:0.000}", Director.result_relative_v);
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
        if (GameMaster.game_scene == 1)
        {
            if (flag_register == 0)
            {
                flag_register = 1;
                object_register.SetActive(true);

                if (Director.result_score > top_score)
                {

                    PlayerPrefs.SetFloat("3rd_SCORE", second_score);
                    PlayerPrefs.Save();
                    PlayerPrefs.SetFloat("3rd_DURATION", second_duration);
                    PlayerPrefs.Save();
                    PlayerPrefs.SetFloat("3rd_DV", second_dv);
                    PlayerPrefs.Save();
                    PlayerPrefs.SetFloat("3rd_REL_V", second_relative_v);
                    PlayerPrefs.Save();
                    PlayerPrefs.SetString("3rd_NAME", second_name);
                    PlayerPrefs.Save();

                    PlayerPrefs.SetFloat("2nd_SCORE", top_score);
                    PlayerPrefs.Save();
                    PlayerPrefs.SetFloat("2nd_DURATION", top_duration);
                    PlayerPrefs.Save();
                    PlayerPrefs.SetFloat("2nd_DV", top_dv);
                    PlayerPrefs.Save();
                    PlayerPrefs.SetFloat("2nd_REL_V", top_relative_v);
                    PlayerPrefs.Save();
                    PlayerPrefs.SetString("2nd_NAME", top_name);
                    PlayerPrefs.Save();

                    PlayerPrefs.SetFloat("1st_SCORE", Director.result_score);
                    PlayerPrefs.Save();
                    PlayerPrefs.SetFloat("1st_DURATION", Director.result_duration);
                    PlayerPrefs.Save();
                    PlayerPrefs.SetFloat("1st_DV", Director.result_dv);
                    PlayerPrefs.Save();
                    PlayerPrefs.SetFloat("1st_REL_V", Director.result_relative_v);
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
                else if (Director.result_score > second_score)
                {
                    PlayerPrefs.SetFloat("3rd_SCORE", second_score);
                    PlayerPrefs.Save();
                    PlayerPrefs.SetFloat("3rd_DURATION", second_duration);
                    PlayerPrefs.Save();
                    PlayerPrefs.SetFloat("3rd_DV", second_dv);
                    PlayerPrefs.Save();
                    PlayerPrefs.SetFloat("3rd_REL_V", second_relative_v);
                    PlayerPrefs.Save();
                    PlayerPrefs.SetString("3rd_NAME", second_name);
                    PlayerPrefs.Save();

                    PlayerPrefs.SetFloat("2nd_SCORE", Director.result_score);
                    PlayerPrefs.Save();
                    PlayerPrefs.SetFloat("2nd_DURATION", Director.result_duration);
                    PlayerPrefs.Save();
                    PlayerPrefs.SetFloat("2nd_DV", Director.result_dv);
                    PlayerPrefs.Save();
                    PlayerPrefs.SetFloat("2nd_REL_V", Director.result_relative_v);
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
                else if (Director.result_score > third_score)
                {
                    PlayerPrefs.SetFloat("3rd_SCORE", Director.result_score);
                    PlayerPrefs.Save();
                    PlayerPrefs.SetFloat("3rd_DURATION", Director.result_duration);
                    PlayerPrefs.Save();
                    PlayerPrefs.SetFloat("3rd_DV", Director.result_dv);
                    PlayerPrefs.Save();
                    PlayerPrefs.SetFloat("3rd_REL_V", Director.result_relative_v);
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

                top_score = PlayerPrefs.GetFloat("1st_SCORE", 0f);
                top_duration = PlayerPrefs.GetFloat("1st_DURATION", 0f);
                top_dv = PlayerPrefs.GetFloat("1st_DV", 0f);
                top_relative_v = PlayerPrefs.GetFloat("1st_REL_V", 0f);
                top_name = PlayerPrefs.GetString("1st_NAME", "NO NAME");

                second_score = PlayerPrefs.GetFloat("2nd_SCORE", 0f);
                second_duration = PlayerPrefs.GetFloat("2nd_DURATION", 0f);
                second_dv = PlayerPrefs.GetFloat("2nd_DV", 0f);
                second_relative_v = PlayerPrefs.GetFloat("2nd_REL_V", 0f);
                second_name = PlayerPrefs.GetString("2nd_NAME", "NO NAME");

                third_score = PlayerPrefs.GetFloat("3rd_SCORE", 0f);
                third_duration = PlayerPrefs.GetFloat("3rd_DURATION", 0f);
                third_dv = PlayerPrefs.GetFloat("3rd_DV", 0f);
                third_relative_v = PlayerPrefs.GetFloat("3rd_REL_V", 0f);
                third_name = PlayerPrefs.GetString("3rd_NAME", "NO NAME");

                GameObject.Find("1st_name").GetComponent<Text>().text = top_name;
                GameObject.Find("2nd_name").GetComponent<Text>().text = second_name;
                GameObject.Find("3rd_name").GetComponent<Text>().text = third_name;
                GameObject.Find("1st_point").GetComponent<Text>().text = string.Format("{0:0.0}", top_score);
                GameObject.Find("2nd_point").GetComponent<Text>().text = string.Format("{0:0.0}", second_score);
                GameObject.Find("3rd_point").GetComponent<Text>().text = string.Format("{0:0.0}", third_score);
                GameObject.Find("1st_sub_relativeV").GetComponent<Text>().text = string.Format("{0:0.000}", top_relative_v);
                GameObject.Find("2nd_sub_relativeV").GetComponent<Text>().text = string.Format("{0:0.000}", second_relative_v);
                GameObject.Find("3rd_sub_relativeV").GetComponent<Text>().text = string.Format("{0:0.000}", third_relative_v);
                GameObject.Find("1st_sub_duration").GetComponent<Text>().text = string.Format("{0:0.0}", top_duration);
                GameObject.Find("2nd_sub_duration").GetComponent<Text>().text = string.Format("{0:0.0}", second_duration);
                GameObject.Find("3rd_sub_duration").GetComponent<Text>().text = string.Format("{0:0.0}", third_duration);
                GameObject.Find("1st_sub_dv").GetComponent<Text>().text = string.Format("{0:0.0}", top_dv);
                GameObject.Find("2nd_sub_dv").GetComponent<Text>().text = string.Format("{0:0.0}", second_dv);
                GameObject.Find("3rd_sub_dv").GetComponent<Text>().text = string.Format("{0:0.0}", third_dv);
            }
        }
        else if (GameMaster.game_scene == 4)
        {
            if (flag_register == 0)
            {
                flag_register = 1;
                object_register.SetActive(true);

                if (Director.result_score > top_score)
                {

                    PlayerPrefs.SetFloat("3rd_SCORE_D", second_score);
                    PlayerPrefs.Save();
                    PlayerPrefs.SetFloat("3rd_DURATION_D", second_duration);
                    PlayerPrefs.Save();
                    PlayerPrefs.SetFloat("3rd_DV_D", second_dv);
                    PlayerPrefs.Save();
                    PlayerPrefs.SetFloat("3rd_REL_V_D", second_relative_v);
                    PlayerPrefs.Save();
                    PlayerPrefs.SetString("3rd_NAME_D", second_name);
                    PlayerPrefs.Save();

                    PlayerPrefs.SetFloat("2nd_SCORE_D", top_score);
                    PlayerPrefs.Save();
                    PlayerPrefs.SetFloat("2nd_DURATION_D", top_duration);
                    PlayerPrefs.Save();
                    PlayerPrefs.SetFloat("2nd_DV_D", top_dv);
                    PlayerPrefs.Save();
                    PlayerPrefs.SetFloat("2nd_REL_V_D", top_relative_v);
                    PlayerPrefs.Save();
                    PlayerPrefs.SetString("2nd_NAME_D", top_name);
                    PlayerPrefs.Save();

                    PlayerPrefs.SetFloat("1st_SCORE_D", Director.result_score);
                    PlayerPrefs.Save();
                    PlayerPrefs.SetFloat("1st_DURATION_D", Director.result_duration);
                    PlayerPrefs.Save();
                    PlayerPrefs.SetFloat("1st_DV_D", Director.result_dv);
                    PlayerPrefs.Save();
                    PlayerPrefs.SetFloat("1st_REL_V_D", Director.result_relative_v);
                    PlayerPrefs.Save();
                    if (inputField.text == "")
                    {
                        PlayerPrefs.SetString("1st_NAME_D", "KOUNOTORI");
                        PlayerPrefs.Save();
                    }
                    else
                    {
                        PlayerPrefs.SetString("1st_NAME_D", inputField.text);
                        PlayerPrefs.Save();
                    }
                }
                else if (Director.result_score > second_score)
                {
                    PlayerPrefs.SetFloat("3rd_SCORE_D", second_score);
                    PlayerPrefs.Save();
                    PlayerPrefs.SetFloat("3rd_DURATION_D", second_duration);
                    PlayerPrefs.Save();
                    PlayerPrefs.SetFloat("3rd_DV_D", second_dv);
                    PlayerPrefs.Save();
                    PlayerPrefs.SetFloat("3rd_REL_V_D", second_relative_v);
                    PlayerPrefs.Save();
                    PlayerPrefs.SetString("3rd_NAME_D", second_name);
                    PlayerPrefs.Save();

                    PlayerPrefs.SetFloat("2nd_SCORE_D", Director.result_score);
                    PlayerPrefs.Save();
                    PlayerPrefs.SetFloat("2nd_DURATION_D", Director.result_duration);
                    PlayerPrefs.Save();
                    PlayerPrefs.SetFloat("2nd_DV_D", Director.result_dv);
                    PlayerPrefs.Save();
                    PlayerPrefs.SetFloat("2nd_REL_V_D", Director.result_relative_v);
                    if (inputField.text == "")
                    {
                        PlayerPrefs.SetString("2nd_NAME_D", "KOUNOTORI");
                        PlayerPrefs.Save();
                    }
                    else
                    {
                        PlayerPrefs.SetString("2nd_NAME_D", inputField.text);
                        PlayerPrefs.Save();
                    }
                }
                else if (Director.result_score > third_score)
                {
                    PlayerPrefs.SetFloat("3rd_SCORE_D", Director.result_score);
                    PlayerPrefs.Save();
                    PlayerPrefs.SetFloat("3rd_DURATION_D", Director.result_duration);
                    PlayerPrefs.Save();
                    PlayerPrefs.SetFloat("3rd_DV_D", Director.result_dv);
                    PlayerPrefs.Save();
                    PlayerPrefs.SetFloat("3rd_REL_V_D", Director.result_relative_v);
                    PlayerPrefs.Save();
                    if (inputField.text == "")
                    {
                        PlayerPrefs.SetString("3rd_NAME_D", "KOUNOTORI");
                        PlayerPrefs.Save();
                    }
                    else
                    {
                        PlayerPrefs.SetString("3rd_NAME_D", inputField.text);
                        PlayerPrefs.Save();
                    }
                }

                top_score = PlayerPrefs.GetFloat("1st_SCORE_D", 0f);
                top_duration = PlayerPrefs.GetFloat("1st_DURATION_D", 0f);
                top_dv = PlayerPrefs.GetFloat("1st_DV_D", 0f);
                top_relative_v = PlayerPrefs.GetFloat("1st_REL_V_D", 0f);
                top_name = PlayerPrefs.GetString("1st_NAME_D", "NO NAME");

                second_score = PlayerPrefs.GetFloat("2nd_SCORE_D", 0f);
                second_duration = PlayerPrefs.GetFloat("2nd_DURATION_D", 0f);
                second_dv = PlayerPrefs.GetFloat("2nd_DV_D", 0f);
                second_relative_v = PlayerPrefs.GetFloat("2nd_REL_V_D", 0f);
                second_name = PlayerPrefs.GetString("2nd_NAME_D", "NO NAME");

                third_score = PlayerPrefs.GetFloat("3rd_SCORE_D", 0f);
                third_duration = PlayerPrefs.GetFloat("3rd_DURATION_D", 0f);
                third_dv = PlayerPrefs.GetFloat("3rd_DV_D", 0f);
                third_relative_v = PlayerPrefs.GetFloat("3rd_REL_V_D", 0f);
                third_name = PlayerPrefs.GetString("3rd_NAME_D", "NO NAME");

                GameObject.Find("1st_name").GetComponent<Text>().text = top_name;
                GameObject.Find("2nd_name").GetComponent<Text>().text = second_name;
                GameObject.Find("3rd_name").GetComponent<Text>().text = third_name;
                GameObject.Find("1st_point").GetComponent<Text>().text = string.Format("{0:0.0}", top_score);
                GameObject.Find("2nd_point").GetComponent<Text>().text = string.Format("{0:0.0}", second_score);
                GameObject.Find("3rd_point").GetComponent<Text>().text = string.Format("{0:0.0}", third_score);
                GameObject.Find("1st_sub_relativeV").GetComponent<Text>().text = string.Format("{0:0.000}", top_relative_v);
                GameObject.Find("2nd_sub_relativeV").GetComponent<Text>().text = string.Format("{0:0.000}", second_relative_v);
                GameObject.Find("3rd_sub_relativeV").GetComponent<Text>().text = string.Format("{0:0.000}", third_relative_v);
                GameObject.Find("1st_sub_duration").GetComponent<Text>().text = string.Format("{0:0.0}", top_duration);
                GameObject.Find("2nd_sub_duration").GetComponent<Text>().text = string.Format("{0:0.0}", second_duration);
                GameObject.Find("3rd_sub_duration").GetComponent<Text>().text = string.Format("{0:0.0}", third_duration);
                GameObject.Find("1st_sub_dv").GetComponent<Text>().text = string.Format("{0:0.0}", top_dv);
                GameObject.Find("2nd_sub_dv").GetComponent<Text>().text = string.Format("{0:0.0}", second_dv);
                GameObject.Find("3rd_sub_dv").GetComponent<Text>().text = string.Format("{0:0.0}", third_dv);
            }
        }
    }


    public void RankingShow()
    {
        if (GameMaster.game_scene == 1)
        {
            top_score = PlayerPrefs.GetFloat("1st_SCORE", 0f);
            top_duration = PlayerPrefs.GetFloat("1st_DURATION", 0f);
            top_dv = PlayerPrefs.GetFloat("1st_DV", 0f);
            top_relative_v = PlayerPrefs.GetFloat("1st_REL_V", 0f);
            top_name = PlayerPrefs.GetString("1st_NAME", "NO DATA");

            second_score = PlayerPrefs.GetFloat("2nd_SCORE", 0f);
            second_duration = PlayerPrefs.GetFloat("2nd_DURATION", 0f);
            second_dv = PlayerPrefs.GetFloat("2nd_DV", 0f);
            second_relative_v = PlayerPrefs.GetFloat("2nd_REL_V", 0f);
            second_name = PlayerPrefs.GetString("2nd_NAME", "NO DATA");

            third_score = PlayerPrefs.GetFloat("3rd_SCORE", 0f);
            third_duration = PlayerPrefs.GetFloat("3rd_DURATION", 0f);
            third_dv = PlayerPrefs.GetFloat("3rd_DV", 0f);
            third_relative_v = PlayerPrefs.GetFloat("3rd_REL_V", 0f);
            third_name = PlayerPrefs.GetString("3rd_NAME", "NO DATA");
        }
        else if (GameMaster.game_scene == 4)
        {
            top_score = PlayerPrefs.GetFloat("1st_SCORE_D", 0f);
            top_duration = PlayerPrefs.GetFloat("1st_DURATION_D", 0f);
            top_dv = PlayerPrefs.GetFloat("1st_DV_D", 0f);
            top_relative_v = PlayerPrefs.GetFloat("1st_REL_V_D", 0f);
            top_name = PlayerPrefs.GetString("1st_NAME_D", "NO DATA");

            second_score = PlayerPrefs.GetFloat("2nd_SCORE_D", 0f);
            second_duration = PlayerPrefs.GetFloat("2nd_DURATION_D", 0f);
            second_dv = PlayerPrefs.GetFloat("2nd_DV_D", 0f);
            second_relative_v = PlayerPrefs.GetFloat("2nd_REL_V_D", 0f);
            second_name = PlayerPrefs.GetString("2nd_NAME_D", "NO DATA");

            third_score = PlayerPrefs.GetFloat("3rd_SCORE_D", 0f);
            third_duration = PlayerPrefs.GetFloat("3rd_DURATION_D", 0f);
            third_dv = PlayerPrefs.GetFloat("3rd_DV_D", 0f);
            third_relative_v = PlayerPrefs.GetFloat("3rd_REL_V_D", 0f);
            third_name = PlayerPrefs.GetString("3rd_NAME_D", "NO DATA");
        }
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

        if (GameMaster.game_scene == 1)
        {
            GameMaster.game_scene = 1;
            SceneManager.LoadScene("GameRbar");
        }
        else if (GameMaster.game_scene == 4)
        {
            GameMaster.game_scene = 4;
            SceneManager.LoadScene("GameDocking");
        }
    }

    public void score_delete()
    {
        PlayerPrefs.DeleteAll();

        if (GameMaster.game_scene == 1)
        {
            top_score = PlayerPrefs.GetFloat("1st_SCORE", 0f);
            top_duration = PlayerPrefs.GetFloat("1st_DURATION", 0f);
            top_dv = PlayerPrefs.GetFloat("1st_DV", 0f);
            top_relative_v = PlayerPrefs.GetFloat("1st_REL_V", 0f);
            top_name = PlayerPrefs.GetString("1st_NAME", "NO DATA");

            second_score = PlayerPrefs.GetFloat("2nd_SCORE", 0f);
            second_duration = PlayerPrefs.GetFloat("2nd_DURATION", 0f);
            second_dv = PlayerPrefs.GetFloat("2nd_DV", 0f);
            second_relative_v = PlayerPrefs.GetFloat("2nd_REL_V", 0f);
            second_name = PlayerPrefs.GetString("2nd_NAME", "NO DATA");

            third_score = PlayerPrefs.GetFloat("3rd_SCORE", 0f);
            third_duration = PlayerPrefs.GetFloat("3rd_DURATION", 0f);
            third_dv = PlayerPrefs.GetFloat("3rd_DV", 0f);
            third_relative_v = PlayerPrefs.GetFloat("3rd_REL_V", 0f);
            third_name = PlayerPrefs.GetString("3rd_NAME", "NO DATA");
        }
        else if (GameMaster.game_scene == 4)
        {
            top_score = PlayerPrefs.GetFloat("1st_SCORE_D", 0f);
            top_duration = PlayerPrefs.GetFloat("1st_DURATION_D", 0f);
            top_dv = PlayerPrefs.GetFloat("1st_DV_D", 0f);
            top_relative_v = PlayerPrefs.GetFloat("1st_REL_V_D", 0f);
            top_name = PlayerPrefs.GetString("1st_NAME_D", "NO DATA");

            second_score = PlayerPrefs.GetFloat("2nd_SCORE_D", 0f);
            second_duration = PlayerPrefs.GetFloat("2nd_DURATION_D", 0f);
            second_dv = PlayerPrefs.GetFloat("2nd_DV_D", 0f);
            second_relative_v = PlayerPrefs.GetFloat("2nd_REL_V_D", 0f);
            second_name = PlayerPrefs.GetString("2nd_NAME_D", "NO DATA");

            third_score = PlayerPrefs.GetFloat("3rd_SCORE_D", 0f);
            third_duration = PlayerPrefs.GetFloat("3rd_DURATION_D", 0f);
            third_dv = PlayerPrefs.GetFloat("3rd_DV_D", 0f);
            third_relative_v = PlayerPrefs.GetFloat("3rd_REL_V_D", 0f);
            third_name = PlayerPrefs.GetString("3rd_NAME_D", "NO DATA");
        }

        GameObject.Find("1st_name").GetComponent<Text>().text = top_name;
        GameObject.Find("2nd_name").GetComponent<Text>().text = second_name;
        GameObject.Find("3rd_name").GetComponent<Text>().text = third_name;
        GameObject.Find("1st_point").GetComponent<Text>().text = string.Format("{0:0.0}", top_score);
        GameObject.Find("2nd_point").GetComponent<Text>().text = string.Format("{0:0.0}", second_score);
        GameObject.Find("3rd_point").GetComponent<Text>().text = string.Format("{0:0.0}", third_score);
        GameObject.Find("1st_sub_relativeV").GetComponent<Text>().text = string.Format("{0:0.000}", top_relative_v);
        GameObject.Find("2nd_sub_relativeV").GetComponent<Text>().text = string.Format("{0:0.000}", second_relative_v);
        GameObject.Find("3rd_sub_relativeV").GetComponent<Text>().text = string.Format("{0:0.000}", third_relative_v);
        GameObject.Find("1st_sub_duration").GetComponent<Text>().text = string.Format("{0:0.0}", top_duration);
        GameObject.Find("2nd_sub_duration").GetComponent<Text>().text = string.Format("{0:0.0}", second_duration);
        GameObject.Find("3rd_sub_duration").GetComponent<Text>().text = string.Format("{0:0.0}", third_duration);
        GameObject.Find("1st_sub_dv").GetComponent<Text>().text = string.Format("{0:0.0}", top_dv);
        GameObject.Find("2nd_sub_dv").GetComponent<Text>().text = string.Format("{0:0.0}", second_dv);
        GameObject.Find("3rd_sub_dv").GetComponent<Text>().text = string.Format("{0:0.0}", third_dv);
    }


}
