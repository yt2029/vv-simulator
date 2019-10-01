//// Director.cs
//// ゲームシーン上のカメラの位置・得点計算・時刻計算・UIへの表示をコントロールする。


using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class Director : MonoBehaviour
{

    Text timeText;
    public static float timer, timer_after_scale;
    GameObject slider;
    GameObject sim_timer;
    GameObject info_val_x, info_val_y, info_val_z, info_val_vx, info_val_vy, info_val_vz, info_val_attitude, info_message, info_val_total_dv, game_status_capture_timer, game_status_hold_timer;
    GameObject info_val_vx_slider, info_val_vy_slider;
    GameObject game_status_result, game_status_failure;
    GameObject game_status_result_duration, game_status_result_dv, game_status_result_relative_v, game_status_result_score;
    GameObject game_status_vv_propellant_val, game_status_vv_propellant_gage, game_status_vv_propellant_gage_caution, game_status_vv_propellant_gage_warning;
    GameObject plane_movie_capture, video_capture;
<<<<<<< HEAD:project/Assets/Script/Flight/Director.cs
    GameObject guide_right, guide_left, guide_down;
=======
    GameObject guide_right, guide_left, guide_down, guide_up;
>>>>>>> flight-phase:Assets/Script/Flight/Director.cs

    public static float result_dv, result_duration, result_relative_v, result_score;

    public static float elappsedTime, timer_capture, timer_capture_clear, timer_hold_clear_500m, timer_hold_clear_250m, timer_hold_clear_100m, timer_hold_clear_30m, timer_hold_500m, timer_hold_250m, timer_hold_100m, timer_hold_30m, timer_hold_clear;
    public static int game_submode, last_game_submode;
    public static int flag_500m_HP, flag_250m_HP, flag_100m_HP, flag_30m_HP, flag_10m_HP, flag_collision, flag_KOS, flag_clear, flag_10m_HP_stay, flag_no_propellant;
    public static int flag_500m_HP_stay, flag_250m_HP_stay, flag_100m_HP_stay, flag_30m_HP_stay, flag_success;
    public static int flag_go_for_docking;
    public static float val_sim_speed;
    public static int sound_flag, flag_cam;
    public static float TimedeltaCommon;


    public static int cam_mode;      //カメラモード
    float cam_coord_pos_x;
    float cam_coord_pos_y;
    float cam_coord_pos_z;
    GameObject cam_targetObj;
    Vector3 cam_targetPos;
    int cam_initial_rotate = 0;
    float cam_initial_rotate_val = 0, cam_initial_rotate_val_temp = 0;

    public static float prox_model_scale = 100;  //カメラとモデルが接近しているときにモデル表示が不安定となるのを防ぐ
                                                 //	private float val_scale_vehicle_0 = 50f; // 地球座標系
    private float val_scale_vehicle_1 = 0.001f; // 近傍
                                                //	private float val_scale_station_0 = 1.5f; // 地球座標系
    private float val_scale_station_1 = 0.0001f; // 近傍

    private float backDist;
    private float cam_scroll;
    public float cam_speed = 1000f;
    private int flag_pressed;
    public float cam_time_offset;
<<<<<<< HEAD:project/Assets/Script/Flight/Director.cs
    float capture_duration = 5 * 60;
=======
    float capture_duration_rbar = 5 * 60;
    float capture_duration_docking = 0;
    float hold_duration_HP2 = 2.5f * 60;
>>>>>>> flight-phase:Assets/Script/Flight/Director.cs
    float hold_duration = 0 * 60;
    float vv_propellant_max = 12000;
    float vv_propellant_now, vv_propellant_deviation;
    float vv_propellant_coefficient = 300;



    private Vector3 delta_newAngle = new Vector3(0, 0, 0);
    Vector2 rotationSpeed = new Vector2(0.25f, 0.25f);       // カメラの回転速度を格納する変数
    private Vector2 lastMousePosition;  // マウス座標を格納する変数
    private Vector3 newAngle = new Vector3(0, -90, -90);   // カメラの角度を格納する変数（初期値に0,0を代入）
    private Vector3 newPosition = new Vector3(2.2e+07f, 0, 0);



    // Start is called before the first frame update
    void Start()
    {

        ////////////////////////////////
        //// scene override for debug
        ////////////////////////////////
        //GameMaster.game_scene = 1;
        //GameMaster.game_scene = 2;



        //////////////////////////////////////////////////////
        // GameMaster.game_scene共通の設定
        //////////////////////////////////////////////////////
        ///
        this.slider = GameObject.Find("Sim_Speed");
        this.sim_timer = GameObject.Find("Sim_Time");

        this.info_val_x = GameObject.Find("Val-X");
        this.info_val_y = GameObject.Find("Val-Y");
        //this.info_val_z = GameObject.Find("Val-Z");
        this.info_val_vx = GameObject.Find("Val-Vx");
        this.info_val_vy = GameObject.Find("Val-Vy");
        //this.info_val_vz = GameObject.Find("Val-Vz");
        this.info_val_attitude = GameObject.Find("Attitude");
        this.info_message = GameObject.Find("Message");
        //this.info_val_total_dv = GameObject.Find("Val-total-dV");

        this.info_val_vx_slider = GameObject.Find("Vx-slider");
        this.info_val_vy_slider = GameObject.Find("Vy-slider");

        this.game_status_result_duration = GameObject.Find("result_duration");
        this.game_status_result_dv = GameObject.Find("result_dv");
        this.game_status_result_relative_v = GameObject.Find("result_relativeV");
        this.game_status_result_score = GameObject.Find("result_score");
        this.game_status_capture_timer = GameObject.Find("Capture-Timer");
        this.game_status_hold_timer = GameObject.Find("Hold-Timer");

        this.game_status_vv_propellant_val = GameObject.Find("Val-total-dV");
        this.game_status_vv_propellant_gage = GameObject.Find("dv-gage");
        this.game_status_vv_propellant_gage_caution = GameObject.Find("dv-gage-caution");
        this.game_status_vv_propellant_gage_warning = GameObject.Find("dv-gage-warning");

        this.plane_movie_capture = GameObject.Find("CaptureVideoPlane");
        this.video_capture = GameObject.Find("VideoCapture");

        this.guide_right = GameObject.Find("GuideRight");
        this.guide_left = GameObject.Find("GuideLeft");
        this.guide_down = GameObject.Find("GuideDown");
<<<<<<< HEAD:project/Assets/Script/Flight/Director.cs

        time_medium();
        Time.timeScale = 0.5f;
=======
        this.guide_up = GameObject.Find("GuideUp");

        time_medium();
        Time.timeScale = 0.3f;
>>>>>>> flight-phase:Assets/Script/Flight/Director.cs
        timer = 0;
        TimedeltaCommon = 0;
        timer_after_scale = 0;
        elappsedTime = 0;
        Dynamics.total_delta_V = 0;

        // Camera Initialization
        cam_targetObj = GameObject.Find("Vehicle");
        cam_targetPos = cam_targetObj.transform.position;

        if (GameMaster.game_scene == 1)
        {
            cam_mode = 1;
            cam_time_offset = 15000f;
        }
        else if (GameMaster.game_scene == 2)
        {
            cam_mode = 1;
            cam_time_offset = 200000f;
        }
        else if (GameMaster.game_scene == 4)
        {
            cam_mode = 1;
            cam_time_offset = 15000f;
        }

        //if (cam_mode == 0)
        //{
        //    GameObject.Find("Main Camera").transform.position = new Vector3(2.2e+07f, 0f, 0f);
        //    GameObject.Find("Main Camera").transform.rotation = Quaternion.Euler(0f, -90f, -90f);
        //    orbital_plane.SetActive(true);
        //    //GameObject.Find("Coordinate").GetComponent<Text>().text = string.Format("EARTH");
        //}

        // モデルサイズの調整（大きさを調整して、地球とのバランスをとる）
        if (cam_mode == 1 || cam_mode == 2)
        {
            GameObject.Find("Station").transform.localScale = new Vector3(1, 1, 1) * val_scale_station_1 * prox_model_scale;
            GameObject.Find("Vehicle").transform.localScale = new Vector3(1, 1, 1) * val_scale_vehicle_1 * prox_model_scale;
            //orbital_plane.SetActive(false);
            //orbital_plane.SetActive(true);
            //GameObject.Find("Coordinate").GetComponent<Text>().text = string.Format("HILL");
        }


        //////////////////////////////////////////////////////
        // game_scene == 1:R-Barモードの場合の初期設定
        //////////////////////////////////////////////////////
        ///
        if (GameMaster.game_scene == 1)
        {
            this.game_status_result = GameObject.Find("Result");
            this.game_status_failure = GameObject.Find("Failure");

            game_submode = 0;
            flag_500m_HP = 0;
            flag_250m_HP = 0;
            flag_100m_HP = 0;
            flag_30m_HP = 0;
            flag_10m_HP = 0;
            flag_KOS = 0;
            flag_collision = 0;
            flag_clear = 0;
            flag_no_propellant = 0;
            timer_capture = 0f;
            timer_hold_250m = 0f;
            timer_hold_100m = 0f;
            timer_hold_30m = 0f;
            timer_capture_clear = 0f;
            timer_hold_clear_500m = 0f;
            timer_hold_clear_250m = 0f;
            timer_hold_clear_100m = 0f;
            timer_hold_clear_30m = 0f;
            flag_500m_HP_stay = 0;
            flag_250m_HP_stay = 0;
            flag_100m_HP_stay = 0;
            flag_30m_HP_stay = 0;
            flag_10m_HP_stay = 0;
            sound_flag = 1;
            result_dv = 0;
            result_duration = 0;
            result_score = 0;
            flag_cam = 0;
            cam_initial_rotate = 0;
            cam_initial_rotate_val = 0f;
            cam_initial_rotate_val_temp = 0f;
            vv_propellant_deviation = 0f;
<<<<<<< HEAD:project/Assets/Script/Flight/Director.cs
=======
            flag_go_for_docking = 0;
            flag_success = 0;
>>>>>>> flight-phase:Assets/Script/Flight/Director.cs
        }


        //////////////////////////////////////////////////////
        // game_scene == 2:Proxモードの場合の初期設定
        //////////////////////////////////////////////////////
        ///
        if (GameMaster.game_scene == 2)
        {
            game_submode = 0;
            flag_500m_HP = 0;
            flag_250m_HP = 0;
            flag_100m_HP = 0;
            flag_30m_HP = 0;
            flag_10m_HP = 0;
            flag_KOS = 0;
            flag_collision = 0;
            flag_clear = 0;
            flag_no_propellant = 0;
            timer_capture = 0f;
            timer_capture_clear = 0f;
            flag_10m_HP_stay = 0;
            sound_flag = 1;
            result_dv = 0;
            result_duration = 0;
            result_score = 0;
        }


        //////////////////////////////////////////////////////
        // game_scene == 4:Dockingモードの場合の初期設定
        //////////////////////////////////////////////////////
        ///
        if (GameMaster.game_scene == 4)
        {
            this.game_status_result = GameObject.Find("Result");
            this.game_status_failure = GameObject.Find("Failure");

            game_submode = 0;
            flag_500m_HP = 0;
            flag_250m_HP = 0;
            flag_100m_HP = 0;
            flag_30m_HP = 0;
            flag_10m_HP = 0;
            flag_KOS = 0;
            flag_collision = 0;
            flag_clear = 0;
            flag_no_propellant = 0;
            timer_capture = 0f;
            timer_hold_250m = 0f;
            timer_hold_100m = 0f;
            timer_hold_30m = 0f;
            timer_capture_clear = 0f;
            timer_hold_clear_500m = 0f;
            timer_hold_clear_250m = 0f;
            timer_hold_clear_100m = 0f;
            timer_hold_clear_30m = 0f;
            flag_500m_HP_stay = 0;
            flag_250m_HP_stay = 0;
            flag_100m_HP_stay = 0;
            flag_30m_HP_stay = 0;
            flag_10m_HP_stay = 0;
            sound_flag = 1;
            result_dv = 0;
            result_duration = 0;
            result_score = 0;
            flag_cam = 0;
            cam_initial_rotate = 0;
            cam_initial_rotate_val = 0f;
            cam_initial_rotate_val_temp = 0f;
            vv_propellant_deviation = 0f;
            flag_go_for_docking = 0;
            flag_success = 0;
        }

<<<<<<< HEAD:project/Assets/Script/Flight/Director.cs
=======
        //////////////////////////////////////////////////////
        // ステーションに対する機体の初期位置
        //////////////////////////////////////////////////////
        ///
        if (GameMaster.game_scene == 1)
        {
>>>>>>> flight-phase:Assets/Script/Flight/Director.cs
            Dynamics.vv_pos_hill_x0 = -905.61f + Random.Range(-0.1f, +0.1f);
            Dynamics.vv_pos_hill_y0 = -227.73f + Random.Range(-0.1f, +0.1f);
            Dynamics.vv_pos_hill_z0 = 0f;

            Dynamics.vv_vel_hill_x0 = 1.148f + Random.Range(-0.01f, +0.01f);      // [meter/sec]
            Dynamics.vv_vel_hill_y0 = 0.955f + Random.Range(-0.01f, +0.01f);
            Dynamics.vv_vel_hill_z0 = 0f;

            //Dynamics.vv_pos_hill_x0 = -743.41f + Random.Range(-0.1f, +0.1f);
            //Dynamics.vv_pos_hill_y0 = -115.74f + Random.Range(-0.1f, +0.1f);
            //Dynamics.vv_pos_hill_z0 = 0f;

            //Dynamics.vv_vel_hill_x0 = 0.913f + Random.Range(-0.01f, +0.01f);      // [meter/sec]
            //Dynamics.vv_vel_hill_y0 = 0.594f + Random.Range(-0.01f, +0.01f);
            //Dynamics.vv_vel_hill_z0 = 0f;


            Dynamics.vv_pos_hill_xd = Dynamics.vv_pos_hill_x0;
            Dynamics.vv_pos_hill_yd = Dynamics.vv_pos_hill_y0;
            Dynamics.vv_pos_hill_zd = Dynamics.vv_pos_hill_z0;
        }
        else if (GameMaster.game_scene == 2)
        {
            Dynamics.vv_pos_hill_x0 = -2.93f;
            Dynamics.vv_pos_hill_y0 = -2500.41f;
            Dynamics.vv_pos_hill_z0 = 0f;

            Dynamics.vv_vel_hill_x0 = 0.009f;      // [meter/sec]
            Dynamics.vv_vel_hill_y0 = 0.006f;
            Dynamics.vv_vel_hill_z0 = 0f;

            ////// 共軌道
            //Dynamics.vv_pos_hill_x0 = -1200f;
            //Dynamics.vv_pos_hill_y0 = -5000.00f;
            //Dynamics.vv_pos_hill_z0 = 0f;


            //float iss_velocity_scalar_co = Mathf.Sqrt((float)(Param.Co.GRAVITY_CONST / (Param.Co.EARTH_RADIOUS / 1000 + (Param.Co.STATION_ALTITUDE) / 1000))) * 1000;      // [meter/sec]
            //float vv_velocity_scalar_co = Mathf.Sqrt((float)(Param.Co.GRAVITY_CONST / (Param.Co.EARTH_RADIOUS / 1000 + (Param.Co.STATION_ALTITUDE + Dynamics.vv_pos_hill_x0) / 1000))) * 1000;      // [meter/sec]

            //Dynamics.vv_vel_hill_x0 = 0.000f;      // [meter/sec]
            //Dynamics.vv_vel_hill_y0 = (vv_velocity_scalar_co - iss_velocity_scalar_co) * 3f;
            //Dynamics.vv_vel_hill_z0 = 0f;

            Dynamics.vv_pos_hill_xd = Dynamics.vv_pos_hill_x0;
            Dynamics.vv_pos_hill_yd = Dynamics.vv_pos_hill_y0;
            Dynamics.vv_pos_hill_zd = Dynamics.vv_pos_hill_z0;
        }
        else if (GameMaster.game_scene == 4)
        {
            //Dynamics.vv_pos_hill_x0 = -2.93f;
            //Dynamics.vv_pos_hill_y0 = 550.00f;
            //Dynamics.vv_pos_hill_z0 = 0f;

            //Dynamics.vv_vel_hill_x0 = 0.009f;      // [meter/sec]
            //Dynamics.vv_vel_hill_y0 = 0.006f;
            //Dynamics.vv_vel_hill_z0 = 0f;

            //Dynamics.vv_pos_hill_x0 = -368.55f;
            //Dynamics.vv_pos_hill_y0 = 306.98f;
            //Dynamics.vv_pos_hill_z0 = 0f;

            //Dynamics.vv_vel_hill_x0 = 0.359f;      // [meter/sec]
            //Dynamics.vv_vel_hill_y0 = 0.653f;
            //Dynamics.vv_vel_hill_z0 = 0f;

            Dynamics.vv_pos_hill_x0 = -107.23f;
            Dynamics.vv_pos_hill_y0 = 557.77f;
            Dynamics.vv_pos_hill_z0 = 0f;

            Dynamics.vv_vel_hill_x0 = 0.272f;      // [meter/sec]
            Dynamics.vv_vel_hill_y0 = 0.062f;
            Dynamics.vv_vel_hill_z0 = 0f;

            Dynamics.vv_pos_hill_xd = Dynamics.vv_pos_hill_x0;
            Dynamics.vv_pos_hill_yd = Dynamics.vv_pos_hill_y0;
            Dynamics.vv_pos_hill_zd = Dynamics.vv_pos_hill_z0;
        }



    }

    // Update is called once per frame
    void Update()
    {

        //////////////////////////////////////////////////////
        // game_scene共通のゲームコントロール
        //////////////////////////////////////////////////////
        ///
        // Time Controller
        //elappsedTime += Time.deltaTime; //[sec]
        timer += Time.deltaTime;
        timer_after_scale = timer * Param.Co.TIME_SCALE_COMMON * GameMaster.c_time;
        //timer_after_scale += Time.deltaTime * Param.Co.TIME_SCALE_COMMON;
        float timer_after_scale_offset = timer_after_scale + 60 * 60 * 10;
        // Time View
        int days = Mathf.FloorToInt(timer_after_scale_offset / 60F / 60F / 24F);
        int hours = Mathf.FloorToInt(timer_after_scale_offset / 60F / 60F - days * 24);
        int minutes = Mathf.FloorToInt(timer_after_scale_offset / 60f - hours * 60 - days * 24 * 60);
        int seconds = Mathf.FloorToInt(timer_after_scale_offset - minutes * 60 - hours * 60 * 60 - days * 24 * 60 * 60);
        string niceTime = string.Format("{0:00}:{1:00}:{2:00}", hours, minutes, seconds);
        this.sim_timer.GetComponent<Text>().text = "" + niceTime;


        //////////////////////////////////////////////////////
        // game_scene == 1:R-Barモードの場合のゲームコントロール
        //////////////////////////////////////////////////////
        ///
        if (GameMaster.game_scene == 1)
        {
            Time.timeScale = 0.3f;
<<<<<<< HEAD:project/Assets/Script/Flight/Director.cs
=======
            Dynamics.attitude_cmd_pitch = 0f;
>>>>>>> flight-phase:Assets/Script/Flight/Director.cs
            // ゲーム中は結果画面を非表示設定
            this.game_status_result.SetActive(false);
            this.game_status_failure.SetActive(false);
            this.plane_movie_capture.SetActive(false);

            if (-Dynamics.vv_pos_hill_xd < 360f && Dynamics.vv_pos_hill_xd < 100f)
            {
                GameObject.Find("map-vv").GetComponent<RawImage>().enabled = true;
                Vector3 pos_ss_map = GameObject.Find("map-ss").GetComponent<RectTransform>().localPosition;
                GameObject.Find("map-vv").GetComponent<RectTransform>().localPosition = new Vector3(((-Dynamics.vv_pos_hill_yd) / 1.5f + 5f), (Dynamics.vv_pos_hill_xd / 2.5f) + 42f, 0);
                //GameObject.Find("map-vv").transform.position = new Vector3(0, 0, 0) * 1 - pos_ss_map;
            }
            else
            {
                GameObject.Find("map-vv").GetComponent<RawImage>().enabled = false;
            }


            // ゲームサブモードの遷移
            if (game_submode == 0 && Mathf.Abs(Dynamics.vv_pos_hill_yd) < 1000)
            {
                game_submode = 1;
                this.game_status_capture_timer.SetActive(false);
                this.game_status_hold_timer.SetActive(false);
            }
            if (game_submode == 1 && Mathf.Abs(Dynamics.vv_pos_hill_yd) < 800)
            {
                game_submode = 12;
                this.info_message.GetComponent<Text>().text = "宇宙ステーションに向かって、飛行を進めよう。";
            }
            if (flag_500m_HP == 1 && flag_500m_HP_stay == 1)
            {
                game_submode = 21;
            }
            if (timer_hold_clear_500m > hold_duration)
            {
                game_submode = 2;
            }
            if (flag_250m_HP == 1 && flag_250m_HP_stay == 1)
            {
                game_submode = 23;
            }
            if (flag_250m_HP == 1 && flag_250m_HP_stay == 0)
            {
                game_submode = 32;
            }
            if (timer_hold_clear_250m > hold_duration)
            {
                game_submode = 3;
            }
            if (flag_100m_HP == 1 && flag_100m_HP_stay == 1)
            {
                game_submode = 34;
            }
            if (flag_100m_HP == 1 && flag_100m_HP_stay == 0)
            {
                game_submode = 43;
            }
            if (timer_hold_clear_100m > hold_duration)
            {
                game_submode = 4;
            }
            if (flag_30m_HP == 1 && flag_30m_HP_stay == 1)
            {
                game_submode = 451;
            }
            if (flag_30m_HP == 1 && flag_30m_HP_stay == 0)
            {
                game_submode = 541;
            }
            if (timer_hold_clear_30m > hold_duration)
            {
                game_submode = 51;
            }
            if (flag_10m_HP_stay == 1)
            {
                game_submode = 5;
            }
            if (flag_10m_HP == 1 && flag_10m_HP_stay == 0)
            {
                game_submode = 54;
            }
            if (timer_capture_clear > capture_duration_rbar)
            {
                game_submode = 55;
            }
<<<<<<< HEAD:project/Assets/Script/Flight/Director.cs
            //if (flag_10m_HP == 1 && flag_30m_HP == 0)
            //{
            //    game_submode = 6;
            //}
            if (flag_collision == 1)
            {
                game_submode = 7;
            }
            if (Mathf.Abs(Dynamics.vv_pos_hill_yd) > 1000 || Mathf.Abs(Dynamics.vv_pos_hill_xd) > 1000)
            {
                game_submode = 8;
            }
            if (timer_after_scale > 60 * 60 * 12)
            {
                game_submode = 9;
            }
            if (flag_KOS == 1 && flag_250m_HP == 0)
            {
                game_submode = 10;
            }
            if (flag_no_propellant == 1)
            {
                game_submode = 11;
            }
            if (Mathf.Abs(Dynamics.vv_pos_hill_xd - 28f) * (float)(Mathf.Tan(16f / 180f * Mathf.PI)) < (Mathf.Abs(Dynamics.vv_pos_hill_yd - 6.5f)))
            {
                if (game_submode != 11)
                {
                    if (game_submode != 99)
                    {
                        last_game_submode = game_submode;
                        vv_propellant_deviation += 4f;
                        GameObject.Find("corridor").GetComponent<AudioSource>().Play();
                    }

                    game_submode = 99;
                }
            }
=======
>>>>>>> flight-phase:Assets/Script/Flight/Director.cs


            // ゲームモードごとのメッセージ生成
            if (game_submode == 0)
            {
                this.info_message.GetComponent<Text>().text = "宇宙ステーションの近傍を飛行しています。";
            }
            else if (game_submode == 1)
            {
                this.info_message.GetComponent<Text>().text = "宇宙ステーションの１ｋｍ範囲内です。";
            }
            else if (game_submode == 21)
            {
                this.info_message.GetComponent<Text>().text = "RIポイント。\nそのまま位置を保って！";
                timer_hold_clear_500m = timer_after_scale - timer_hold_500m;
                this.game_status_hold_timer.GetComponent<Text>().text = string.Format("RI Departure - {0:0.0}", hold_duration - timer_hold_clear_500m) + "秒";
            }
            else if (game_submode == 2)
            {
                this.info_message.GetComponent<Text>().text = "スラスタを使って、250ｍポイントに向かって\n目指して上昇してください。";
                this.game_status_hold_timer.GetComponent<Text>().text = "RI Depature, Go HTV!!";
            }
            else if (game_submode == 23)
            {
                this.info_message.GetComponent<Text>().text = "250mホールドポイント。\nそのまま位置を保って！";
                timer_hold_clear_250m = timer_after_scale - timer_hold_250m;
                this.game_status_hold_timer.GetComponent<Text>().text = string.Format("250m Departure - {0:0.0}", hold_duration - timer_hold_clear_250m) + "秒";
            }
            else if (game_submode == 3)
            {
                this.info_message.GetComponent<Text>().text = "250mホールドポイントを出発しよう。\nISSに向かって上昇してください。";
                this.game_status_hold_timer.GetComponent<Text>().text = "250m Depature, Go HTV!!";
            }
            else if (game_submode == 32)
            {
                this.info_message.GetComponent<Text>().text = "250mホールドポイントに\nもどって！";
                timer_hold_250m = 0;
            }
            else if (game_submode == 34)
            {
                this.info_message.GetComponent<Text>().text = "100mホールドポイント。\nそのまま位置を保って！";
                timer_hold_clear_100m = timer_after_scale - timer_hold_100m;
                this.game_status_hold_timer.GetComponent<Text>().text = string.Format("100m Departure - {0:0.0}", hold_duration - timer_hold_clear_100m) + "秒";
            }
            else if (game_submode == 4)
            {
                this.info_message.GetComponent<Text>().text = "100mポイントに到着。\nさらに上昇してください。";
                this.game_status_hold_timer.GetComponent<Text>().text = "100m Depature, Go HTV!!";
            }
            else if (game_submode == 43)
            {
                this.info_message.GetComponent<Text>().text = "100mホールドポイントに\nもどって！";
                timer_hold_100m = 0;
            }
            else if (game_submode == 451)
            {
                this.info_message.GetComponent<Text>().text = "30mホールドポイント。\nそのまま位置を保って！";
                timer_hold_clear_30m = timer_after_scale - timer_hold_30m;
                this.game_status_hold_timer.GetComponent<Text>().text = string.Format("30m Departure - {0:0.0}", hold_duration - timer_hold_clear_30m) + "秒";
            }
            else if (game_submode == 51)
            {
                this.info_message.GetComponent<Text>().text = "30ｍホールドポイント出発可能。\nキャプチャー点まで\nあと少し上昇してください。";
                this.game_status_hold_timer.GetComponent<Text>().text = "30m Depature, Go for Final Approach!!";
            }
            else if (game_submode == 541)
            {
                this.info_message.GetComponent<Text>().text = "30mホールドポイントに\nもどって！";
                timer_hold_30m = 0;
            }
            else if (game_submode == 5)
            {
                this.info_message.GetComponent<Text>().text = "キャプチャー点到着！\nそのままの位置でキャプチャーに向けて静止しよう！";
                timer_capture_clear = timer_after_scale - timer_capture;

                this.game_status_capture_timer.GetComponent<Text>().text = string.Format("Capture - {0:0.0}", capture_duration_rbar - timer_capture_clear) + "秒";
                result_duration = timer_after_scale / 60f;
                result_dv = Dynamics.total_delta_V;
                result_relative_v = Mathf.Sqrt(Mathf.Pow(Dynamics.vv_vel_hill_xd, 2) + Mathf.Pow(Dynamics.vv_vel_hill_yd, 2) + Mathf.Pow(Dynamics.vv_vel_hill_zd, 2));
                result_score = ((6.0f - result_dv) * 16f + (80f - result_duration) * 1.2f + (0.1f - result_relative_v) * 1300f + 200.0f + flag_30m_HP * 50f + flag_100m_HP * 40f + flag_250m_HP * 30f + flag_500m_HP * 20f) * 2f;
            }
            else if (game_submode == 54)
            {
                this.info_message.GetComponent<Text>().text = "キャプチャー点に\nもどって！";
                timer_capture = 0;
            }
            else if (game_submode == 55)
            {

                this.plane_movie_capture.SetActive(true);
                this.video_capture.GetComponent<UnityEngine.Video.VideoPlayer>().Play();
                this.game_status_result_dv.GetComponent<Text>().text = string.Format("{0:0.0} m/sec", result_dv);
                this.game_status_result_duration.GetComponent<Text>().text = string.Format("{0:0.0} mins", result_duration);
                this.game_status_result_relative_v.GetComponent<Text>().text = string.Format("{0:0.000} m/sec", result_relative_v);
                this.game_status_result_score.GetComponent<Text>().text = string.Format("{0:0.0} points", result_score);

                this.info_message.GetComponent<Text>().text = "[ミッション成功] \n宇宙飛行士がキャプチャー完了！";
                Time.timeScale = 0.01f;
                this.game_status_capture_timer.SetActive(false);
                this.game_status_result.SetActive(true);
                sound_success();
                sound_flag = 0;
            }
<<<<<<< HEAD:project/Assets/Script/Flight/Director.cs
            else if (game_submode == 6)
            {
                this.info_message.GetComponent<Text>().text = "[ミッション失敗] \n安全な経路で飛行しよう。";
                this.game_status_failure.SetActive(true);
                this.game_status_capture_timer.SetActive(false);
                Time.timeScale = 0.01f;
                sound_failure();
                sound_flag = 0;
            }
            else if (game_submode == 7)
            {
                this.info_message.GetComponent<Text>().text = "[ミッション失敗] \n宇宙ステーションに衝突しました。";
                this.game_status_failure.SetActive(true);
                this.game_status_capture_timer.SetActive(false);
                Time.timeScale = 0.01f;
                sound_failure();
                sound_flag = 0;
            }
            else if (game_submode == 8)
            {
                this.info_message.GetComponent<Text>().text = "[ミッション失敗]\nステーションから離れてしまった。";
                this.game_status_failure.SetActive(true);
                this.game_status_capture_timer.SetActive(false);
                Time.timeScale = 0.01f;
                sound_failure();
                sound_flag = 0;
            }
            else if (game_submode == 9)
            {
                this.info_message.GetComponent<Text>().text = "[ミッション失敗] \n時間を使いすぎました。";
                this.game_status_failure.SetActive(true);
                this.game_status_capture_timer.SetActive(false);
                Time.timeScale = 0.01f;
                sound_failure();
                sound_flag = 0;
            }
            else if (game_submode == 10)
            {
                this.info_message.GetComponent<Text>().text = "[注意] \n宇宙ステーションに接近しています。";
            }
            else if (game_submode == 11)
            {
                this.info_message.GetComponent<Text>().text = "[ミッション失敗] \n燃料切れです。";
                this.game_status_failure.SetActive(true);
                this.game_status_capture_timer.SetActive(false);
                Time.timeScale = 0.01f;
                sound_failure();
                sound_flag = 0;
            }
            else if (game_submode == 99)
            {
                this.info_message.GetComponent<Text>().text = "[安全機能作動] \nアプローチ・コリドー逸脱を検知。\n自動で軌道を修正します。";
                //this.game_status_failure.SetActive(true);
                //this.game_status_capture_timer.SetActive(false);


                Dynamics.vv_vel_hill_y0 = 0;
                Dynamics.vv_vel_hill_x0 = 0;

                Dynamics.vv_pos_hill_y0 -= Mathf.Sign(Dynamics.vv_pos_hill_yd) * 0.05f;
                Dynamics.vv_vel_hill_y0 -= Mathf.Sign(Dynamics.vv_pos_hill_yd) * 0.05f;

                game_submode = last_game_submode;
                //sound_failure();
                //sound_flag = 0;
            }


            guide_notice();

=======

            // 通知
            notification_rbar_guide();
            notification_rbar_failure();

>>>>>>> flight-phase:Assets/Script/Flight/Director.cs
        }

        if (GameMaster.game_scene == 2)
        {
            Time.timeScale = val_sim_speed;

        }

        if (GameMaster.game_scene == 4)
        {
            Time.timeScale = 0.3f;

            // ゲーム中は結果画面を非表示設定
            this.game_status_result.SetActive(false);
            this.game_status_failure.SetActive(false);
            this.plane_movie_capture.SetActive(false);

<<<<<<< HEAD:project/Assets/Script/Flight/Director.cs
            this.info_val_vx_slider.GetComponent<Slider>().value = Dynamics.vv_vel_hill_xd * 1.3f + 0.5f;
            this.info_val_vy_slider.GetComponent<Slider>().value = -Dynamics.vv_vel_hill_yd * 1.3f + 0.5f;


            // 推薬計算
            vv_propellant_now = (vv_propellant_max - vv_propellant_coefficient * Dynamics.total_delta_V - vv_propellant_coefficient * vv_propellant_deviation) / vv_propellant_max;
=======
            // LOS姿勢角制御
            Dynamics.attitude_cmd_pitch = -Mathf.Atan2(Dynamics.vv_pos_hill_xd + 2.4f, Dynamics.vv_pos_hill_yd - 12.24f) * Mathf.Rad2Deg;

>>>>>>> flight-phase:Assets/Script/Flight/Director.cs

            // ゲームサブモードの遷移
            if (game_submode == 0 && Mathf.Abs(Dynamics.vv_pos_hill_yd) < 1000)
            {
                game_submode = 1;
                this.game_status_capture_timer.SetActive(false);
                this.game_status_hold_timer.SetActive(false);
            }
<<<<<<< HEAD:project/Assets/Script/Flight/Director.cs

            this.game_status_vv_propellant_val.GetComponent<Text>().text = string.Format("{0:0.0} %", vv_propellant_now * 100);
            if (vv_propellant_now >= 0.5)
            {
                this.game_status_vv_propellant_gage.GetComponent<Image>().fillAmount = vv_propellant_now * 0.9f;
            }
            else if (vv_propellant_now < 0.5)
            {
                this.game_status_vv_propellant_gage.SetActive(false);
            }
            if (vv_propellant_now >= 0.25)
            {
                this.game_status_vv_propellant_gage_warning.GetComponent<Image>().fillAmount = vv_propellant_now * 0.9f;
            }
            else if (vv_propellant_now < 0.25)
            {
                this.game_status_vv_propellant_gage_warning.SetActive(false);
            }
            this.game_status_vv_propellant_gage_caution.GetComponent<Image>().fillAmount = vv_propellant_now * 0.9f;

        }

        //this.info_val_attitude.GetComponent<Text>().text = string.Format("{0:0.0} degree", Dynamics.attitude_cmd_yaw);


        if (cam_mode == 1 || cam_mode == 2)
        {
            GameObject.Find("Station").transform.localScale = new Vector3(1, 1, 1) * val_scale_station_1 * prox_model_scale;
            GameObject.Find("Vehicle").transform.localScale = new Vector3(1, 1, 1) * val_scale_vehicle_1 * prox_model_scale;
        }


        //////////////////////////////////////////////////////
        // game_scene == 1:R-Barモードの場合のCamera View
        //////////////////////////////////////////////////////
        ///
        if (GameMaster.game_scene == 1)
        {


            //Station
            LineRenderer renderer_orbit_station = GameObject.Find("Station").GetComponent<LineRenderer>();
            LineRenderer renderer_orbit_station_vertical = GameObject.Find("10mHP").GetComponent<LineRenderer>();
            if (Director.cam_mode == 1)
            {
                renderer_orbit_station.SetWidth(100f, 100f); // 線の幅
                renderer_orbit_station_vertical.SetWidth(100f, 100f); // 線の幅
            }
            else if (Director.cam_mode == 2)
            {
                renderer_orbit_station.SetWidth(300f, 300f); // 線の幅
                renderer_orbit_station_vertical.SetWidth(300f, 300f); // 線の幅
            }
            //Vector3 plot_point_line_orbit_station;
            //renderer_orbit_station.SetVertexCount(2); // 頂点の数
            //plot_point_line_orbit_station = Dynamics.iss_coord_pos + GameObject.Find("Station").transform.rotation * new Vector3(-2500, 0, 0) * Director.prox_model_scale;
            //renderer_orbit_station.SetPosition(0, plot_point_line_orbit_station);
            //plot_point_line_orbit_station = Dynamics.iss_coord_pos + GameObject.Find("Station").transform.rotation * new Vector3(2500, 0, 0) * Director.prox_model_scale;
            //renderer_orbit_station.SetPosition(1, plot_point_line_orbit_station);

            Vector3 plot_point_line_orbit_station_vertical;
            renderer_orbit_station_vertical.SetVertexCount(2); // 頂点の数
            plot_point_line_orbit_station_vertical = Dynamics.iss_coord_pos + GameObject.Find("Station").transform.rotation * new Vector3(-8, 0, 0) * Director.prox_model_scale;
            renderer_orbit_station_vertical.SetPosition(0, plot_point_line_orbit_station_vertical);
            plot_point_line_orbit_station_vertical = Dynamics.iss_coord_pos + GameObject.Find("Station").transform.rotation * new Vector3(-8, 0, 1000) * Director.prox_model_scale;
            renderer_orbit_station_vertical.SetPosition(1, plot_point_line_orbit_station_vertical);

            if (cam_mode == 1) // VV中心のビュー
            {

                //はじめにカメラをまわす/カメラをとおざけうる
                if (cam_initial_rotate == 0)
                {
                    cam_initial_rotate_val_temp += 1f;
                    if (cam_initial_rotate_val_temp > 75)
                    {
                        cam_initial_rotate_val += 1f;
                    }
                }
                if (cam_initial_rotate_val > 90)
                {
                    cam_initial_rotate = 1;
                }

                GameObject.Find("Main Camera").transform.rotation = GameObject.Find("Vehicle").transform.rotation * Quaternion.Euler(0, 0, -Dynamics.attitude_now) * Quaternion.Euler(180, 90, -90) * Quaternion.Euler(5 - (90 - cam_initial_rotate_val), 120 + ((Dynamics.vv_pos_hill_xd) - 150f) / 11, 0) * Quaternion.Euler(0, 0, 0) * Quaternion.Euler(delta_newAngle);
                Vector3 cam_pos_offset_2 = new Vector3(-45000 / 100, 50000 / 100, -1 * (cam_time_offset - (90 - cam_initial_rotate_val) * 150f) * prox_model_scale / 80 * ((Mathf.Abs(Dynamics.vv_pos_hill_xd) + 30) / 250));
                GameObject.Find("Main Camera").transform.position = GameObject.Find("Vehicle").transform.position + GameObject.Find("Main Camera").transform.rotation * cam_pos_offset_2;

                ////はじめにカメラをまわす
                //if (cam_initial_rotate == 0)
                //{
                //    cam_initial_rotate_val += 1f;
                //}
                //if (cam_initial_rotate_val > 90)
                //{
                //    cam_initial_rotate = 1;
                //}

                //GameObject.Find("Main Camera").transform.rotation = GameObject.Find("Vehicle").transform.rotation * Quaternion.Euler(0, 0, -Dynamics.attitude_now) * Quaternion.Euler(180, 90, -90) * Quaternion.Euler(-60 +( 90 - cam_initial_rotate_val), 45, 25) * Quaternion.Euler(0, 0, 0) * Quaternion.Euler(delta_newAngle);
                //Vector3 cam_pos_offset = new Vector3(-45000 / 100, 50000 / 100, -1 * cam_time_offset * prox_model_scale / 800);
                //GameObject.Find("Main Camera").transform.position = GameObject.Find("Vehicle").transform.position + GameObject.Find("Main Camera").transform.rotation * cam_pos_offset;

                // Sub Cameraの視点
                GameObject.Find("Sub Camera").transform.rotation = GameObject.Find("Station").transform.rotation * Quaternion.Euler(0, 0, -Dynamics.attitude_now) * Quaternion.Euler(180, 90, -90) * Quaternion.Euler(55, 155, 15);
                Vector3 cam_pos_offset_sub = new Vector3(-500 + -500, -1000 - 500, -1 * cam_time_offset * prox_model_scale / 1000 + 100f);
                GameObject.Find("Sub Camera").transform.position = GameObject.Find("Station").transform.position + GameObject.Find("Sub Camera").transform.rotation * cam_pos_offset_sub;
=======
            if (game_submode == 1 )
            {
                game_submode = 12;
                this.info_message.GetComponent<Text>().text = "宇宙ステーションに向かって、飛行を進めよう。";
>>>>>>> flight-phase:Assets/Script/Flight/Director.cs
            }
            if (flag_500m_HP == 1 && flag_500m_HP_stay == 1)
            {
                game_submode = 21;
            }
            if (timer_hold_clear_500m > hold_duration)
            {
                game_submode = 2;
            }
            if (flag_250m_HP == 1 && flag_250m_HP_stay == 1)
            {
                game_submode = 23;
            }
            if (flag_250m_HP == 1 && flag_250m_HP_stay == 0)
            {
                game_submode = 32;
            }
            if (timer_hold_clear_250m > hold_duration)
            {
                game_submode = 3;
            }
            if (flag_100m_HP == 1 && flag_100m_HP_stay == 1)
            {
                game_submode = 34;
            }
            if (flag_100m_HP == 1 && flag_100m_HP_stay == 0)
            {
                game_submode = 43;
            }
            if (timer_hold_clear_100m > hold_duration)
            {
                game_submode = 4;
            }
            if (flag_30m_HP == 1 && flag_30m_HP_stay == 1)
            {
                game_submode = 451;
            }
            if (flag_30m_HP == 1 && flag_30m_HP_stay == 0)
            {
                game_submode = 541;
            }
            if (timer_hold_clear_30m > hold_duration_HP2)
            {
                game_submode = 51;
            }
            if (flag_10m_HP_stay == 1 && flag_30m_HP == 1)
            {
                game_submode = 5;
            }
            if (flag_10m_HP == 1 && flag_10m_HP_stay == 0)
            {
                game_submode = 54;
            }
            if (flag_10m_HP == 1 && timer_capture_clear > capture_duration_docking)
            {
                game_submode = 55;
            }


            // ゲームモードごとのメッセージ生成
            if (game_submode == 0)
            {
                this.info_message.GetComponent<Text>().text = "宇宙ステーションの近傍を飛行しています。";
            }
            else if (game_submode == 1)
            {
                this.info_message.GetComponent<Text>().text = "宇宙ステーションの１ｋｍ範囲内です。";
            }
            else if (game_submode == 21)
            {
                this.info_message.GetComponent<Text>().text = "500mポイント。\nそのまま位置を保って！";
                timer_hold_clear_500m = timer_after_scale - timer_hold_500m;
                this.game_status_hold_timer.GetComponent<Text>().text = string.Format("RI Departure - {0:0.0}", hold_duration - timer_hold_clear_500m) + "秒";
            }
            else if (game_submode == 2)
            {
                this.info_message.GetComponent<Text>().text = "スラスタを使って、250ｍポイントに向かって\n目指して進んでください。";
                this.game_status_hold_timer.GetComponent<Text>().text = "500m Depature, Go HTV!!";
            }
            else if (game_submode == 23)
            {
                this.info_message.GetComponent<Text>().text = "250mホールドポイント。\nそのまま位置を保って！";
                timer_hold_clear_250m = timer_after_scale - timer_hold_250m;
                this.game_status_hold_timer.GetComponent<Text>().text = string.Format("250m Departure - {0:0.0}", hold_duration - timer_hold_clear_250m) + "秒";
            }
            else if (game_submode == 3)
            {
                this.info_message.GetComponent<Text>().text = "250mポイントを出発しよう。\nISSに向かってください。";
                this.game_status_hold_timer.GetComponent<Text>().text = "250m Depature, Go HTV!!";
            }
            else if (game_submode == 32)
            {
                this.info_message.GetComponent<Text>().text = "250mホールドポイントに\nもどって！";
                timer_hold_250m = 0;
            }
            else if (game_submode == 34)
            {
                this.info_message.GetComponent<Text>().text = "100mホールドポイント。\nそのまま位置を保って！";
                timer_hold_clear_100m = timer_after_scale - timer_hold_100m;
                this.game_status_hold_timer.GetComponent<Text>().text = string.Format("100m Departure - {0:0.0}", hold_duration - timer_hold_clear_100m) + "秒";
            }
            else if (game_submode == 4)
            {
                this.info_message.GetComponent<Text>().text = "100mポイントに到着。\nさらに進んで、次の20m地点では一度停止しよう。";
                this.game_status_hold_timer.GetComponent<Text>().text = "100m Depature, Go HTV!!";
            }
            else if (game_submode == 43)
            {
                this.info_message.GetComponent<Text>().text = "100mホールドポイントに\nもどって！";
                timer_hold_100m = 0;
            }
            else if (game_submode == 451)
            {
                this.info_message.GetComponent<Text>().text = "20mホールドポイント。\nそのまま位置を保って、出発許可を待とう！";
                timer_hold_clear_30m = timer_after_scale - timer_hold_30m;
                this.game_status_hold_timer.GetComponent<Text>().text = string.Format("20m Departure - {0:0.0}", hold_duration_HP2 - timer_hold_clear_30m) + "秒";
            }
            else if (game_submode == 51)
            {
                this.info_message.GetComponent<Text>().text = "20ｍホールドポイント出発許可。\nドッキング点まであと少し。";
                this.game_status_hold_timer.GetComponent<Text>().text = "Go for Final Approach!!";
                flag_go_for_docking = 1;
            }
            else if (game_submode == 541)
            {
                this.info_message.GetComponent<Text>().text = "20mホールドポイントに\nもどって許可を待とう！";
                timer_hold_30m = 0;
            }
            else if (game_submode == 5)
            {
                this.info_message.GetComponent<Text>().text = "キャプチャー点到着！\nそのままの位置でキャプチャーに向けて静止しよう！";
                timer_capture_clear = timer_after_scale - timer_capture;

                this.game_status_capture_timer.GetComponent<Text>().text = string.Format("Capture - {0:0.0}", capture_duration_docking - timer_capture_clear) + "秒";
                result_duration = timer_after_scale / 60f;
                result_dv = Dynamics.total_delta_V;
                result_relative_v = Mathf.Sqrt(Mathf.Pow(Dynamics.vv_vel_hill_xd, 2) + Mathf.Pow(Dynamics.vv_vel_hill_yd, 2) + Mathf.Pow(Dynamics.vv_vel_hill_zd, 2));
                result_score = ((6.0f - result_dv) * 16f + (80f - result_duration) * 1.2f + (0.1f - result_relative_v) * 1300f + 200.0f + flag_30m_HP * 50f + flag_100m_HP * 40f + flag_250m_HP * 30f + flag_500m_HP * 20f) * 2f;
            }
            else if (game_submode == 54)
            {
                this.info_message.GetComponent<Text>().text = "キャプチャー点に\nもどって！";
                timer_capture = 0;
            }
            else if (game_submode == 55)
            {

                if(flag_go_for_docking == 0)
                {

                    this.info_message.GetComponent<Text>().text = "[ミッション失敗] \nドッキング許可が出ていません。";
                    this.game_status_failure.SetActive(true);
                    this.game_status_capture_timer.SetActive(false);
                    Time.timeScale = 0.01f;
                    sound_failure();
                    sound_flag = 0;
                }

                if (Dynamics.vv_vel_hill_yd >= -0.015f)
                { 
                this.plane_movie_capture.SetActive(true);
                this.video_capture.GetComponent<UnityEngine.Video.VideoPlayer>().Play();
                this.game_status_result_dv.GetComponent<Text>().text = string.Format("{0:0.0} m/sec", result_dv);
                this.game_status_result_duration.GetComponent<Text>().text = string.Format("{0:0.0} mins", result_duration);
                this.game_status_result_relative_v.GetComponent<Text>().text = string.Format("{0:0.000} m/sec", result_relative_v);
                this.game_status_result_score.GetComponent<Text>().text = string.Format("{0:0.0} points", result_score);

                this.info_message.GetComponent<Text>().text = "[ミッション成功] \n宇宙ステーションにドッキング完了！";
                Time.timeScale = 0.00f;
                this.game_status_capture_timer.SetActive(false);
                this.game_status_result.SetActive(true);
                sound_success();
                flag_success = 1;
                sound_flag = 0;
                }
                else if(Dynamics.vv_vel_hill_yd < -0.015f)
                {
                    flag_collision = 1;
                }
            }


            // 通知
            if (flag_success ==0)
            {
                notification_docking_guide();
                notification_docking_failure();
            }

        }


        //////////////////////////////////////////////////////
        // カメラの位置の調整
        //////////////////////////////////////////////////////
        ///

        // 画面表示
        //if (Dynamics.cam_mode == 0)
        //{
        //    this.info_val_x.GetComponent<Text>().text = string.Format("{0:0.00} km", GameObject.Find("Vehicle").transform.position.x / 1000f);
        //    this.info_val_y.GetComponent<Text>().text = string.Format("{0:0.00} km", GameObject.Find("Vehicle").transform.position.y / 1000f);
        //    this.info_val_z.GetComponent<Text>().text = string.Format("{0:0.00} km", GameObject.Find("Vehicle").transform.position.z / 1000f);

        //    this.info_val_vx.GetComponent<Text>().text = string.Format("{0:0.000} m/sec", Dynamics.vv_vel_hill_xd);
        //    this.info_val_vy.GetComponent<Text>().text = string.Format("{0:0.000} m/sec", Dynamics.vv_vel_hill_yd);
        //    this.info_val_vz.GetComponent<Text>().text = string.Format("{0:0.000} m/sec", Dynamics.vv_vel_hill_zd);
        //    Debug.Log(GameObject.Find("Vehicle").GetComponent<Rigidbody>().velocity.x);
        //}
        if (cam_mode == 1 || cam_mode == 2)
        {
            this.info_val_x.GetComponent<Text>().text = string.Format("{0:0.00}", -Dynamics.vv_pos_hill_x);
            this.info_val_y.GetComponent<Text>().text = string.Format("{0:0.00}", Dynamics.vv_pos_hill_y - 8f);
            //this.info_val_z.GetComponent<Text>().text = string.Format("{0:0.00} m", Dynamics.vv_pos_hill_z);

            this.info_val_vx.GetComponent<Text>().text = string.Format("{0:0.000}", -Dynamics.vv_vel_hill_xd);
            this.info_val_vy.GetComponent<Text>().text = string.Format("{0:0.000}", Dynamics.vv_vel_hill_yd);
            //this.info_val_vz.GetComponent<Text>().text = string.Format("{0:0.000} m/sec", Dynamics.vv_vel_hill_zd);
            //this.info_val_total_dv.GetComponent<Text>().text = string.Format("{0:0.000} m/sec", Dynamics.total_delta_V);

            this.info_val_vx_slider.GetComponent<Slider>().value = Dynamics.vv_vel_hill_xd * 1.3f + 0.5f;
            this.info_val_vy_slider.GetComponent<Slider>().value = -Dynamics.vv_vel_hill_yd * 1.3f + 0.5f;


            // 推薬計算
            if(GameMaster.game_scene == 4)
            {
                vv_propellant_max = 6000;
            }

            vv_propellant_now = (vv_propellant_max - vv_propellant_coefficient * Dynamics.total_delta_V - vv_propellant_coefficient * vv_propellant_deviation) / vv_propellant_max;

            if (vv_propellant_now < 0)
            {
                vv_propellant_now = 0.0f;
                flag_no_propellant = 1;
            }

            this.game_status_vv_propellant_val.GetComponent<Text>().text = string.Format("{0:0.0} %", vv_propellant_now * 100);
            if (vv_propellant_now >= 0.5)
            {
                this.game_status_vv_propellant_gage.GetComponent<Image>().fillAmount = vv_propellant_now * 0.9f;
            }
            else if (vv_propellant_now < 0.5)
            {
                this.game_status_vv_propellant_gage.SetActive(false);
            }
            if (vv_propellant_now >= 0.25)
            {
                this.game_status_vv_propellant_gage_warning.GetComponent<Image>().fillAmount = vv_propellant_now * 0.9f;
            }
            else if (vv_propellant_now < 0.25)
            {
                this.game_status_vv_propellant_gage_warning.SetActive(false);
            }
            this.game_status_vv_propellant_gage_caution.GetComponent<Image>().fillAmount = vv_propellant_now * 0.9f;

        }

        //this.info_val_attitude.GetComponent<Text>().text = string.Format("{0:0.0} degree", Dynamics.attitude_cmd_yaw);


        if (cam_mode == 1 || cam_mode == 2)
        {
            GameObject.Find("Station").transform.localScale = new Vector3(1, 1, 1) * val_scale_station_1 * prox_model_scale;
            GameObject.Find("Vehicle").transform.localScale = new Vector3(1, 1, 1) * val_scale_vehicle_1 * prox_model_scale;
        }


        //////////////////////////////////////////////////////
        // game_scene == 1:R-Barモードの場合のCamera View
        //////////////////////////////////////////////////////
        ///
        if (GameMaster.game_scene == 1)
        {


            //Station
            LineRenderer renderer_orbit_station = GameObject.Find("Station").GetComponent<LineRenderer>();
            LineRenderer renderer_orbit_station_vertical = GameObject.Find("10mHP").GetComponent<LineRenderer>();
            if (Director.cam_mode == 1)
            {
                renderer_orbit_station.SetWidth(100f, 100f); // 線の幅
                renderer_orbit_station_vertical.SetWidth(100f, 100f); // 線の幅
            }
            else if (Director.cam_mode == 2)
            {
                renderer_orbit_station.SetWidth(300f, 300f); // 線の幅
                renderer_orbit_station_vertical.SetWidth(300f, 300f); // 線の幅
            }
            //Vector3 plot_point_line_orbit_station;
            //renderer_orbit_station.SetVertexCount(2); // 頂点の数
            //plot_point_line_orbit_station = Dynamics.iss_coord_pos + GameObject.Find("Station").transform.rotation * new Vector3(-2500, 0, 0) * Director.prox_model_scale;
            //renderer_orbit_station.SetPosition(0, plot_point_line_orbit_station);
            //plot_point_line_orbit_station = Dynamics.iss_coord_pos + GameObject.Find("Station").transform.rotation * new Vector3(2500, 0, 0) * Director.prox_model_scale;
            //renderer_orbit_station.SetPosition(1, plot_point_line_orbit_station);

            Vector3 plot_point_line_orbit_station_vertical;
            renderer_orbit_station_vertical.SetVertexCount(2); // 頂点の数
            plot_point_line_orbit_station_vertical = Dynamics.iss_coord_pos + GameObject.Find("Station").transform.rotation * new Vector3(-8, 0, 0) * Director.prox_model_scale;
            renderer_orbit_station_vertical.SetPosition(0, plot_point_line_orbit_station_vertical);
            plot_point_line_orbit_station_vertical = Dynamics.iss_coord_pos + GameObject.Find("Station").transform.rotation * new Vector3(-8, 0, 1000) * Director.prox_model_scale;
            renderer_orbit_station_vertical.SetPosition(1, plot_point_line_orbit_station_vertical);

            if (cam_mode == 1) // VV中心のビュー
            {

                //はじめにカメラをまわす/カメラをとおざけうる
                if (cam_initial_rotate == 0)
                {
                    cam_initial_rotate_val_temp += 1f;
                    if (cam_initial_rotate_val_temp > 75)
                    {
                        cam_initial_rotate_val += 1f;
                    }
                }
                if (cam_initial_rotate_val > 90)
                {
                    cam_initial_rotate = 1;
                }

                GameObject.Find("Main Camera").transform.rotation = GameObject.Find("Vehicle").transform.rotation * Quaternion.Euler(0, 0, -Dynamics.attitude_now) * Quaternion.Euler(180, 90, -90) * Quaternion.Euler(5 - (90 - cam_initial_rotate_val), 120 + ((Dynamics.vv_pos_hill_xd) - 150f) / 11, 0) * Quaternion.Euler(0, 0, 0) * Quaternion.Euler(delta_newAngle);
                Vector3 cam_pos_offset_2 = new Vector3(-45000 / 100, 50000 / 100, -1 * (cam_time_offset - (90 - cam_initial_rotate_val) * 150f) * prox_model_scale / 80 * ((Mathf.Abs(Dynamics.vv_pos_hill_xd) + 30) / 250));
                GameObject.Find("Main Camera").transform.position = GameObject.Find("Vehicle").transform.position + GameObject.Find("Main Camera").transform.rotation * cam_pos_offset_2;

                ////はじめにカメラをまわす
                //if (cam_initial_rotate == 0)
                //{
                //    cam_initial_rotate_val += 1f;
                //}
                //if (cam_initial_rotate_val > 90)
                //{
                //    cam_initial_rotate = 1;
                //}

                //GameObject.Find("Main Camera").transform.rotation = GameObject.Find("Vehicle").transform.rotation * Quaternion.Euler(0, 0, -Dynamics.attitude_now) * Quaternion.Euler(180, 90, -90) * Quaternion.Euler(-60 +( 90 - cam_initial_rotate_val), 45, 25) * Quaternion.Euler(0, 0, 0) * Quaternion.Euler(delta_newAngle);
                //Vector3 cam_pos_offset = new Vector3(-45000 / 100, 50000 / 100, -1 * cam_time_offset * prox_model_scale / 800);
                //GameObject.Find("Main Camera").transform.position = GameObject.Find("Vehicle").transform.position + GameObject.Find("Main Camera").transform.rotation * cam_pos_offset;

                // Sub Cameraの視点
                GameObject.Find("Sub Camera").transform.rotation = GameObject.Find("Station").transform.rotation * Quaternion.Euler(0, 0, -Dynamics.attitude_now) * Quaternion.Euler(180, 90, -90) * Quaternion.Euler(55, 155-90, 15);
                Vector3 cam_pos_offset_sub = new Vector3(-500 + -500, -1000 - 500, -1 * cam_time_offset * prox_model_scale / 1000 + 100f);
                GameObject.Find("Sub Camera").transform.position = GameObject.Find("Station").transform.position + GameObject.Find("Sub Camera").transform.rotation * cam_pos_offset_sub;
            }

        }


        //////////////////////////////////////////////////////
        // game_scene == 2:Proxモードの場合のCamera View
        //////////////////////////////////////////////////////
        ///
        if (GameMaster.game_scene == 2)
        {

            //Station
            LineRenderer renderer_orbit_station = GameObject.Find("Station").GetComponent<LineRenderer>();
            LineRenderer renderer_orbit_station_vertical = GameObject.Find("10mHP").GetComponent<LineRenderer>();
            if (Director.cam_mode == 1)
            {
                renderer_orbit_station.SetWidth(100f, 100f); // 線の幅
                renderer_orbit_station_vertical.SetWidth(100f, 100f); // 線の幅
            }
            else if (Director.cam_mode == 2)
            {
                renderer_orbit_station.SetWidth(300f, 300f); // 線の幅
                renderer_orbit_station_vertical.SetWidth(300f, 300f); // 線の幅
            }
            Vector3 plot_point_line_orbit_station;
            renderer_orbit_station.SetVertexCount(2); // 頂点の数
            plot_point_line_orbit_station = Dynamics.iss_coord_pos + GameObject.Find("Station").transform.rotation * new Vector3(-2500, 0, 0) * Director.prox_model_scale;
            renderer_orbit_station.SetPosition(0, plot_point_line_orbit_station);
            plot_point_line_orbit_station = Dynamics.iss_coord_pos + GameObject.Find("Station").transform.rotation * new Vector3(2500, 0, 0) * Director.prox_model_scale;
            renderer_orbit_station.SetPosition(1, plot_point_line_orbit_station);

            Vector3 plot_point_line_orbit_station_vertical;
            renderer_orbit_station_vertical.SetVertexCount(2); // 頂点の数
            plot_point_line_orbit_station_vertical = Dynamics.iss_coord_pos + GameObject.Find("Station").transform.rotation * new Vector3(-8, 0, 0) * Director.prox_model_scale;
            renderer_orbit_station_vertical.SetPosition(0, plot_point_line_orbit_station_vertical);
            plot_point_line_orbit_station_vertical = Dynamics.iss_coord_pos + GameObject.Find("Station").transform.rotation * new Vector3(-8, 0, 1000) * Director.prox_model_scale;
            renderer_orbit_station_vertical.SetPosition(1, plot_point_line_orbit_station_vertical);


            if (cam_mode == 1) // VV中心のビュー
            {
                if (Input.mousePosition.y < Screen.height - Screen.height * 0.3)
                {
                    //cam_time_offset = GameObject.Find("Cam_Offset").GetComponent<Slider>().value;
                    cam_scroll = Input.GetAxis("Mouse ScrollWheel");
                    cam_time_offset -= cam_scroll * cam_speed;
                    if (cam_time_offset >= 200000)
                    {
                        cam_time_offset = 200000;
                    }
                    else if (cam_time_offset <= 10000)
                    {
                        cam_time_offset = 10000;
                    }
                    //GameObject.Find("Cam_Offset").GetComponent<Slider>().value = cam_time_offset;

                }


                GameObject.Find("Main Camera").transform.rotation = GameObject.Find("Vehicle").transform.rotation * Quaternion.Euler(0, 0, -Dynamics.attitude_now) * Quaternion.Euler(180, 90, -90) * Quaternion.Euler(-10, 45, 25) * Quaternion.Euler(0, 0, 0) * Quaternion.Euler(delta_newAngle);
                Vector3 cam_pos_offset_2 = new Vector3(-45000 / 100, 50000 / 100, -1 * cam_time_offset * prox_model_scale / 300);
                GameObject.Find("Main Camera").transform.position = GameObject.Find("Vehicle").transform.position + GameObject.Find("Main Camera").transform.rotation * cam_pos_offset_2;

                if ((Input.mousePosition.y < Screen.height - Screen.height * 0.3) && (Input.mousePosition.x > Screen.width * 0.3))
                {
                    if (Input.mousePosition.x > Screen.height * 0.3)
                    {
                        if (Input.GetMouseButtonDown(0))
                        {
                            lastMousePosition = Input.mousePosition;
                        }
                        // 左ドラッグしている間
                        else if (Input.GetMouseButton(0))
                        {
                            delta_newAngle.y = delta_newAngle.y - (Input.mousePosition.x - lastMousePosition.x) * rotationSpeed.x;
                            delta_newAngle.x = delta_newAngle.x - (lastMousePosition.y - Input.mousePosition.y) * rotationSpeed.y;
                            lastMousePosition = Input.mousePosition;
                        }
                    }
                }

            }
            else if (cam_mode == 2) // Station中心のビュー
            {
                //cam_time_offset = GameObject.Find("Cam_Offset").GetComponent<Slider>().value;
                cam_scroll = Input.GetAxis("Mouse ScrollWheel");
                cam_time_offset -= cam_scroll * cam_speed;
                if (cam_time_offset >= 200000)
                {
                    cam_time_offset = 200000;
                }
                else if (cam_time_offset <= 10000)
                {
                    cam_time_offset = 10000;
                }
                //GameObject.Find("Cam_Offset").GetComponent<Slider>().value = cam_time_offset;



                GameObject.Find("Main Camera").transform.rotation = GameObject.Find("Station").transform.rotation * Quaternion.Euler(0, 0, -Dynamics.attitude_now) * Quaternion.Euler(180, 90, -90) * Quaternion.Euler(-10, 45, 25) * Quaternion.Euler(0, 0, 0) * Quaternion.Euler(delta_newAngle);
                Vector3 cam_pos_offset = new Vector3(-45000 / 100, 50000 / 100, -1 * cam_time_offset * prox_model_scale / 80);
                GameObject.Find("Main Camera").transform.position = GameObject.Find("Station").transform.position + GameObject.Find("Main Camera").transform.rotation * cam_pos_offset;

                if ((Input.mousePosition.y < Screen.height - Screen.height * 0.3) && (Input.mousePosition.x > Screen.width * 0.3))
                {
                    if (Input.GetMouseButtonDown(0))
                    {
                        lastMousePosition = Input.mousePosition;
                    }
                    // 左ドラッグしている間
                    else if (Input.GetMouseButton(0))
                    {
                        delta_newAngle.y = delta_newAngle.y - (Input.mousePosition.x - lastMousePosition.x) * rotationSpeed.x;
                        delta_newAngle.x = delta_newAngle.x - (lastMousePosition.y - Input.mousePosition.y) * rotationSpeed.y;
                        lastMousePosition = Input.mousePosition;

                    }
                }
            }
        }



        //////////////////////////////////////////////////////
        // game_scene == 4:Dockingモードの場合のCamera View
        //////////////////////////////////////////////////////
        ///
        if (GameMaster.game_scene == 4)
        {
            
            if (-Dynamics.vv_pos_hill_yd > -560f)
            {
                GameObject.Find("map-vv").GetComponent<RawImage>().enabled = true;
                Vector3 pos_ss_map = GameObject.Find("map-ss").GetComponent<RectTransform>().localPosition;
                GameObject.Find("map-vv").GetComponent<RectTransform>().localPosition = new Vector3(((-Dynamics.vv_pos_hill_yd) / 3.1f + 38f), (Dynamics.vv_pos_hill_xd / 1.2f) + 7f, 0);
                //GameObject.Find("map-vv").transform.position = new Vector3(0, 0, 0) * 1 - pos_ss_map;
            }
            else
            {
                GameObject.Find("map-vv").GetComponent<RawImage>().enabled = false;
            }



            //Station
            LineRenderer renderer_orbit_station = GameObject.Find("Station").GetComponent<LineRenderer>();
            LineRenderer renderer_orbit_station_vertical = GameObject.Find("10mHP").GetComponent<LineRenderer>();
            if (Director.cam_mode == 1)
            {
                renderer_orbit_station.SetWidth(100f, 100f); // 線の幅
                renderer_orbit_station_vertical.SetWidth(100f, 100f); // 線の幅
            }
            else if (Director.cam_mode == 2)
            {
                renderer_orbit_station.SetWidth(300f, 300f); // 線の幅
                renderer_orbit_station_vertical.SetWidth(300f, 300f); // 線の幅
            }
            Vector3 plot_point_line_orbit_station;
            renderer_orbit_station.SetVertexCount(2); // 頂点の数
            plot_point_line_orbit_station = Dynamics.iss_coord_pos + GameObject.Find("Station").transform.rotation * new Vector3(-2500, 0, 0) * Director.prox_model_scale;
            renderer_orbit_station.SetPosition(0, plot_point_line_orbit_station);
            plot_point_line_orbit_station = Dynamics.iss_coord_pos + GameObject.Find("Station").transform.rotation * new Vector3(2500, 0, 0) * Director.prox_model_scale;
            renderer_orbit_station.SetPosition(1, plot_point_line_orbit_station);

            Vector3 plot_point_line_orbit_station_vertical;
            renderer_orbit_station_vertical.SetVertexCount(2); // 頂点の数
            plot_point_line_orbit_station_vertical = Dynamics.iss_coord_pos + GameObject.Find("Station").transform.rotation * new Vector3(-8, 0, 0) * Director.prox_model_scale;
            renderer_orbit_station_vertical.SetPosition(0, plot_point_line_orbit_station_vertical);
            plot_point_line_orbit_station_vertical = Dynamics.iss_coord_pos + GameObject.Find("Station").transform.rotation * new Vector3(-8, 0, 1000) * Director.prox_model_scale;
            renderer_orbit_station_vertical.SetPosition(1, plot_point_line_orbit_station_vertical);

            if (cam_mode == 1) // VV中心のビュー
            {

                //はじめにカメラをまわす/カメラをとおざけうる
                cam_initial_rotate_val = 90;

                GameObject.Find("Main Camera").transform.rotation = GameObject.Find("Vehicle").transform.rotation * Quaternion.Euler(0, 0, -Dynamics.attitude_now) * Quaternion.Euler(180, 90, -90) * Quaternion.Euler(5 - (90 - cam_initial_rotate_val), -(120 - 130 -14 + -((Dynamics.vv_pos_hill_yd) + 500) / 11), 0) * Quaternion.Euler(0, 0, 0) * Quaternion.Euler(delta_newAngle);
                Vector3 cam_pos_offset_2 = new Vector3(-45000 / 100, 50000 / 100, -1 * (cam_time_offset - (90 - cam_initial_rotate_val) * 150f) * prox_model_scale / 80 * ((Mathf.Abs(Dynamics.vv_pos_hill_yd) + 30) / 280));
                GameObject.Find("Main Camera").transform.position = GameObject.Find("Vehicle").transform.position + GameObject.Find("Main Camera").transform.rotation * cam_pos_offset_2;

                ////はじめにカメラをまわす
                //if (cam_initial_rotate == 0)
                //{
                //    cam_initial_rotate_val += 1f;
                //}
                //if (cam_initial_rotate_val > 90)
                //{
                //    cam_initial_rotate = 1;
                //}

                //GameObject.Find("Main Camera").transform.rotation = GameObject.Find("Vehicle").transform.rotation * Quaternion.Euler(0, 0, -Dynamics.attitude_now) * Quaternion.Euler(180, 90, -90) * Quaternion.Euler(-60 +( 90 - cam_initial_rotate_val), 45, 25) * Quaternion.Euler(0, 0, 0) * Quaternion.Euler(delta_newAngle);
                //Vector3 cam_pos_offset = new Vector3(-45000 / 100, 50000 / 100, -1 * cam_time_offset * prox_model_scale / 800);
                //GameObject.Find("Main Camera").transform.position = GameObject.Find("Vehicle").transform.position + GameObject.Find("Main Camera").transform.rotation * cam_pos_offset;

                // Sub Cameraの視点(from ISS)
                //GameObject.Find("Sub Camera").transform.rotation = GameObject.Find("Station").transform.rotation * Quaternion.Euler(0, 0, -Dynamics.attitude_now) * Quaternion.Euler(180, 90, -90) * Quaternion.Euler(30,2,0);
                //Vector3 cam_pos_offset_sub = new Vector3(0, 1200 , -1 * cam_time_offset * prox_model_scale / 1000 + 1600f);
                //GameObject.Find("Sub Camera").transform.position = GameObject.Find("Station").transform.position + GameObject.Find("Sub Camera").transform.rotation * cam_pos_offset_sub;

				// Sub Cameraの視点(from VV)
				GameObject.Find("Sub Camera").transform.rotation = GameObject.Find("Vehicle").transform.rotation * Quaternion.Euler(0, 0, -Dynamics.attitude_now) * Quaternion.Euler(180, -90, 90) * Quaternion.Euler(0,0 , 0);
				Vector3 cam_pos_offset_sub = new Vector3(500, 0, 0);
				GameObject.Find("Sub Camera").transform.position = GameObject.Find("Vehicle").transform.position + GameObject.Find("Main Camera").transform.rotation * cam_pos_offset_sub;
			}

        }


    }






    // function

    void sound_success()
    {
        if (sound_flag == 1)
        {
            GameObject.Find("SoundSuccess").GetComponent<AudioSource>().Play();

        }
    }


    void sound_failure()
    {
        if (sound_flag == 1)
        {
            GameObject.Find("SoundFailure").GetComponent<AudioSource>().Play();

        }
    }


    void OnTriggerEnter(Collider other)
    {

        if (GameMaster.game_scene == 1)
        {

            if (other.gameObject.gameObject.tag == "500mHP")
            {
                Debug.Log("500m");
                flag_500m_HP = 1;
                flag_500m_HP_stay = 1;
                this.game_status_hold_timer.SetActive(true);
                GameObject.Find("500mHP").GetComponent<AudioSource>().Play();
                timer_hold_250m = timer_after_scale;
            }
            if (other.gameObject.gameObject.tag == "250mHP")
            {
                Debug.Log("250m");
                flag_250m_HP = 1;
                flag_250m_HP_stay = 1;
                this.game_status_hold_timer.SetActive(true);
                GameObject.Find("250mHP").GetComponent<AudioSource>().Play();
                timer_hold_250m = timer_after_scale;
            }
            else if (other.gameObject.gameObject.tag == "100mHP")
            {
                Debug.Log("100m");
                flag_100m_HP = 1;
                flag_100m_HP_stay = 1;
                this.game_status_hold_timer.SetActive(true);
                GameObject.Find("100mHP").GetComponent<AudioSource>().Play();
                timer_hold_100m = timer_after_scale;
            }
            else if (other.gameObject.gameObject.tag == "30mHP")
            {
                Debug.Log("30m");
                flag_30m_HP = 1;
                flag_30m_HP_stay = 1;
                this.game_status_hold_timer.SetActive(true);
                GameObject.Find("30mHP").GetComponent<AudioSource>().Play();
                timer_hold_30m = timer_after_scale;
            }
            else if (other.gameObject.gameObject.tag == "10mHP")
            {
                Debug.Log("10m");
                flag_10m_HP = 1;
                flag_10m_HP_stay = 1;
                this.game_status_capture_timer.SetActive(true);
                GameObject.Find("10mHP").GetComponent<AudioSource>().Play();
                timer_capture = timer_after_scale;
            }
            else if (other.gameObject.gameObject.tag == "Station" && (cam_mode == 1 || cam_mode == 2))
            {
                Debug.Log("Station Collision");
                flag_collision = 1;
            }


            if (other.gameObject.gameObject.tag == "KOS")
            {
                Debug.Log("KOS");
                flag_KOS = 1;
            }
            else
            {
                flag_KOS = 0;
            }
        }


        if (GameMaster.game_scene == 4)
        {

            if (other.gameObject.gameObject.tag == "500mHP")
            {
                Debug.Log("500m");
                flag_500m_HP = 1;
                flag_500m_HP_stay = 1;
                this.game_status_hold_timer.SetActive(true);
                GameObject.Find("500mHP").GetComponent<AudioSource>().Play();
                timer_hold_250m = timer_after_scale;
            }
            if (other.gameObject.gameObject.tag == "250mHP")
            {
                Debug.Log("250m");
                flag_250m_HP = 1;
                flag_250m_HP_stay = 1;
                this.game_status_hold_timer.SetActive(true);
                GameObject.Find("250mHP").GetComponent<AudioSource>().Play();
                timer_hold_250m = timer_after_scale;
            }
            else if (other.gameObject.gameObject.tag == "100mHP")
            {
                Debug.Log("100m");
                flag_100m_HP = 1;
                flag_100m_HP_stay = 1;
                this.game_status_hold_timer.SetActive(true);
                GameObject.Find("100mHP").GetComponent<AudioSource>().Play();
                timer_hold_100m = timer_after_scale;
            }
            else if (other.gameObject.gameObject.tag == "30mHP")
            {
                Debug.Log("30m");
                flag_30m_HP = 1;
                flag_30m_HP_stay = 1;
                this.game_status_hold_timer.SetActive(true);
                GameObject.Find("30mHP").GetComponent<AudioSource>().Play();
                timer_hold_30m = timer_after_scale;
            }
            else if (other.gameObject.gameObject.tag == "10mHP")
            {
                Debug.Log("10m");
                flag_10m_HP = 1;
                flag_10m_HP_stay = 1;
                this.game_status_capture_timer.SetActive(true);
                GameObject.Find("10mHP").GetComponent<AudioSource>().Play();
                timer_capture = timer_after_scale;
            }


            if (other.gameObject.gameObject.tag == "KOS")
            {
                Debug.Log("KOS");
                flag_KOS = 1;
            }
            else
            {
                flag_KOS = 0;
            }
        }
    }


    void OnTriggerExit(Collider other2)
    {
        if (GameMaster.game_scene == 1)
        {
            if (other2.gameObject.gameObject.tag != "10mHPC")
            {
                flag_10m_HP_stay = 0;
                timer_capture = 0;
                this.game_status_capture_timer.SetActive(false);
            }

            if (other2.gameObject.gameObject.tag != "30mHPC")
            {
                flag_30m_HP_stay = 0;
                timer_hold_30m = 0;
                this.game_status_hold_timer.SetActive(false);
            }
        
            if (other2.gameObject.gameObject.tag != "100mHPC")
            {
                flag_100m_HP_stay = 0;
                timer_hold_100m = 0;
                this.game_status_hold_timer.SetActive(false);
            }
       
            if (other2.gameObject.gameObject.tag != "250mHPC")
            {
                flag_250m_HP_stay = 0;
                timer_hold_250m = 0;
                this.game_status_hold_timer.SetActive(false);
            }
    
            if (other2.gameObject.gameObject.tag != "500mHPC")
            {
                flag_500m_HP_stay = 0;
                timer_hold_500m = 0;
                this.game_status_hold_timer.SetActive(false);
            }
        }

        if (GameMaster.game_scene == 4)
        {
            if (other2.gameObject.gameObject.tag != "10mHPC")
            {
                flag_10m_HP_stay = 0;
                timer_capture = 0;
                this.game_status_capture_timer.SetActive(false);
            }

            if (other2.gameObject.gameObject.tag != "30mHPC")
            {
                flag_30m_HP_stay = 0;
                timer_hold_30m = 0;
                this.game_status_hold_timer.SetActive(false);
            }

            if (other2.gameObject.gameObject.tag != "100mHPC")
            {
                flag_100m_HP_stay = 0;
                timer_hold_100m = 0;
                this.game_status_hold_timer.SetActive(false);
            }

            if (other2.gameObject.gameObject.tag != "250mHPC")
            {
                flag_250m_HP_stay = 0;
                timer_hold_250m = 0;
                this.game_status_hold_timer.SetActive(false);
            }

            if (other2.gameObject.gameObject.tag != "500mHPC")
            {
                flag_500m_HP_stay = 0;
                timer_hold_500m = 0;
                this.game_status_hold_timer.SetActive(false);
            }
        }
    }


    public void vehicle_view()
    {
        cam_mode = 1;
        //orbital_plane.SetActive(false);
        //GameObject.Find("Coordinate").GetComponent<Text>().text = string.Format("HILL");
        //orbital_plane.SetActive(true);
    }
    public void station_view()
    {
        cam_mode = 2;
        //orbital_plane.SetActive(false);
        //GameObject.Find("Coordinate").GetComponent<Text>().text = string.Format("HILL");
    }


    public void time_low()
    {
        val_sim_speed = 0.1f;
    }
    public void time_medium()
    {
        val_sim_speed = 1.0f;
    }
    public void time_high()
    {
        val_sim_speed = 10.0f;
    }


    public void reload_scene()
    {
        if (GameMaster.game_scene == 1)
        {
            Time.timeScale = 1.0f;
            SceneManager.LoadScene("GameRbar");
            //val_sim_speed = 1.0f;
            //Time.timeScale = val_sim_speed;
            //FadeManager.Instance.LoadScene("GameRbar", 0.3f);
        }
        else if (GameMaster.game_scene == 2)
        {
            Time.timeScale = 1.0f;
            SceneManager.LoadScene("GameProx");
            //val_sim_speed = 1.0f;
            //Time.timeScale = val_sim_speed;
            //FadeManager.Instance.LoadScene("GameProx", 0.3f);
        }
        else if (GameMaster.game_scene == 4)
        {
            Time.timeScale = 1.0f;
            SceneManager.LoadScene("GameDocking");
            //val_sim_speed = 1.0f;
            //Time.timeScale = val_sim_speed;
            //FadeManager.Instance.LoadScene("GameProx", 0.3f);
        }
    }

    public void title_scene()
    {

        Time.timeScale = 1.0f;
        SceneManager.LoadScene("TitleScene");
        //val_sim_speed = 1.0f;
        //Time.timeScale = val_sim_speed;
        //FadeManager.Instance.LoadScene("TitleScene", 0.3f);
    }

    public void ranking_scene()
    {

        Time.timeScale = 1.0f;
        SceneManager.LoadScene("ShowRank");
        //val_sim_speed = 1.0f;
        //Time.timeScale = val_sim_speed;
        //FadeManager.Instance.LoadScene("TitleScene", 0.3f);
    }



<<<<<<< HEAD:project/Assets/Script/Flight/Director.cs
    void guide_notice()
=======
    void notification_rbar_guide()
>>>>>>> flight-phase:Assets/Script/Flight/Director.cs
    {
        if (Mathf.Abs(Dynamics.vv_pos_hill_xd - 28f) * (float)(Mathf.Tan(3.8f / 180f * Mathf.PI)) < (Mathf.Abs(Dynamics.vv_pos_hill_yd - 10f)) && Dynamics.vv_pos_hill_xd >-500f)
        {
            this.info_message.GetComponent<Text>().text = "[位置警告]\nアプローチコリドーの限界です。中央に戻ってください。";
            if (Dynamics.vv_pos_hill_yd - 10f > 0)
            {
                this.guide_right.SetActive(true);
            }
            if (Dynamics.vv_pos_hill_yd - 10f < 0)
            {
                this.guide_left.SetActive(true);
            }
        }
        else
        {
            this.guide_right.SetActive(false);
            this.guide_left.SetActive(false);
        }


        if (Dynamics.vv_vel_hill_xd > 0.5f && Dynamics.vv_pos_hill_xd > -450f && Dynamics.vv_pos_hill_xd < -250f)
        {
            this.info_message.GetComponent<Text>().text = "[速度警告]\nステーションに衝突しないために減速してください。";
            this.guide_down.SetActive(true);
        }
        else if (Dynamics.vv_vel_hill_xd > 0.35f && Dynamics.vv_pos_hill_xd > -250f && Dynamics.vv_pos_hill_xd < -30f)
        {
            this.info_message.GetComponent<Text>().text = "[速度警告]\nステーションに衝突しないために減速してください。";
            this.guide_down.SetActive(true);
        }
<<<<<<< HEAD:project/Assets/Script/Flight/Director.cs
        else if (Dynamics.vv_vel_hill_xd > 0.15f && Dynamics.vv_pos_hill_xd > -30f)
=======
        else if (Dynamics.vv_vel_hill_xd > 0.015f && Dynamics.vv_pos_hill_xd > -30f)
>>>>>>> flight-phase:Assets/Script/Flight/Director.cs
        {
            this.info_message.GetComponent<Text>().text = "[速度警告]\n速度超過。ただちに減速してください。";
            this.guide_down.SetActive(true);
        }
<<<<<<< HEAD:project/Assets/Script/Flight/Director.cs
        else if (Dynamics.vv_vel_hill_xd > 0.09f && Dynamics.vv_pos_hill_xd > -15f)
=======
        else if (Dynamics.vv_vel_hill_xd > 0.01f && Dynamics.vv_pos_hill_xd > -20f)
>>>>>>> flight-phase:Assets/Script/Flight/Director.cs
        {
            this.info_message.GetComponent<Text>().text = "[速度警告]\n速度超過。キャプチャーに備えて減速してください。";
            this.guide_down.SetActive(true);
        }
        else
        {
            this.guide_down.SetActive(false);
        }
<<<<<<< HEAD:project/Assets/Script/Flight/Director.cs


=======
        
        if (Mathf.Abs(Dynamics.vv_pos_hill_xd - 28f) * (float)(Mathf.Tan(16f / 180f * Mathf.PI)) < (Mathf.Abs(Dynamics.vv_pos_hill_yd - 6.5f)))
        {

                if (game_submode != 99)
                {
                    last_game_submode = game_submode;
                    vv_propellant_deviation += 4f;
                    GameObject.Find("corridor").GetComponent<AudioSource>().Play();
                }

                game_submode = 99;


            this.info_message.GetComponent<Text>().text = "[安全機能作動] \nアプローチ・コリドー逸脱を検知。\n自動で軌道を修正します。";
            //this.game_status_failure.SetActive(true);
            //this.game_status_capture_timer.SetActive(false);


            Dynamics.vv_vel_hill_y0 = 0;
            Dynamics.vv_vel_hill_x0 = 0;

            Dynamics.vv_pos_hill_y0 -= Mathf.Sign(Dynamics.vv_pos_hill_yd) * 0.05f;
            Dynamics.vv_vel_hill_y0 -= Mathf.Sign(Dynamics.vv_pos_hill_yd) * 0.05f;

            game_submode = last_game_submode;
        }
        
>>>>>>> flight-phase:Assets/Script/Flight/Director.cs
        if (Dynamics.vv_vel_hill_xd > 0.5f && Dynamics.vv_pos_hill_xd > -450f && Dynamics.vv_pos_hill_xd < -250f)
        {
            this.info_message.GetComponent<Text>().text = "[速度警告]\nステーションに衝突しないために減速してください。";
            this.guide_down.SetActive(true);
        }
        else if (Dynamics.vv_vel_hill_xd > 0.35f && Dynamics.vv_pos_hill_xd > -250f && Dynamics.vv_pos_hill_xd < -30f)
        {
            this.info_message.GetComponent<Text>().text = "[速度警告]\nステーションに衝突しないために減速してください。";
            this.guide_down.SetActive(true);
        }
        else if (Dynamics.vv_vel_hill_xd > 0.15f && Dynamics.vv_pos_hill_xd > -30f)
        {
            this.info_message.GetComponent<Text>().text = "[速度警告]\n速度超過。ただちに減速してください。";
            this.guide_down.SetActive(true);
        }
<<<<<<< HEAD:project/Assets/Script/Flight/Director.cs
        else if (Dynamics.vv_pos_hill_xd > -6f)
=======
        else if (Dynamics.vv_pos_hill_xd > -7f)
>>>>>>> flight-phase:Assets/Script/Flight/Director.cs
        {
            this.info_message.GetComponent<Text>().text = "[位置警告]\n下降してください。衝突します！";
            this.guide_down.SetActive(true);
        }
        else
        {
            this.guide_down.SetActive(false);
        }
<<<<<<< HEAD:project/Assets/Script/Flight/Director.cs

=======
    }


    void notification_rbar_failure()
    {
               
        if (flag_collision == 1)
        {
            this.info_message.GetComponent<Text>().text = "[ミッション失敗] \n宇宙ステーションに衝突しました。";
            this.game_status_failure.SetActive(true);
            this.game_status_capture_timer.SetActive(false);
            Time.timeScale = 0.01f;
            sound_failure();
            sound_flag = 0;
        }
        if (Mathf.Abs(Dynamics.vv_pos_hill_yd) > 1000 || Mathf.Abs(Dynamics.vv_pos_hill_xd) > 1000)
        {
            this.info_message.GetComponent<Text>().text = "[ミッション失敗]\nステーションから離れてしまった。";
            this.game_status_failure.SetActive(true);
            this.game_status_capture_timer.SetActive(false);
            Time.timeScale = 0.01f;
            sound_failure();
            sound_flag = 0;
        }
        if (timer_after_scale > 60 * 60 * 2)
        {
            this.info_message.GetComponent<Text>().text = "[ミッション失敗] \n時間を使いすぎました。";
            this.game_status_failure.SetActive(true);
            this.game_status_capture_timer.SetActive(false);
            Time.timeScale = 0.01f;
            sound_failure();
            sound_flag = 0;
        }
        if (flag_no_propellant == 1)
        {
            this.info_message.GetComponent<Text>().text = "[ミッション失敗] \n燃料切れです。";
            this.game_status_failure.SetActive(true);
            this.game_status_capture_timer.SetActive(false);
            Time.timeScale = 0.01f;
            sound_failure();
            sound_flag = 0;
        }
        
    }


    void notification_docking_guide()
    {
        this.guide_right.SetActive(false);

        if ((Dynamics.vv_pos_hill_yd) * (float)(Mathf.Tan(2.3f / 180f * Mathf.PI)) < Dynamics.vv_pos_hill_xd　&& Dynamics.vv_pos_hill_yd > 50f)
        {
            this.info_message.GetComponent<Text>().text = "[位置警告]\nアプローチコリドーの上限です。中央に戻ってください。";
                this.guide_down.SetActive(true);
        }
        else if((Dynamics.vv_pos_hill_yd) * (float)(Mathf.Tan(-5.8f / 180f * Mathf.PI)) < Dynamics.vv_pos_hill_xd && Dynamics.vv_pos_hill_yd < 50f)
        {
            this.info_message.GetComponent<Text>().text = "[位置警告]\nアプローチコリドーの上限です。中央に戻ってください。";
            this.guide_down.SetActive(true);
        }
        else
        {
            this.guide_down.SetActive(false);
        }

        if (-(Dynamics.vv_pos_hill_yd) * (float)(Mathf.Tan(12.0f / 180f * Mathf.PI)) > Dynamics.vv_pos_hill_xd && Dynamics.vv_pos_hill_yd < 500f)
        {
            this.info_message.GetComponent<Text>().text = "[位置警告]\nアプローチコリドーの下限です。中央に戻ってください。";
            this.guide_up.SetActive(true);
        }
        else
        {
            this.guide_up.SetActive(false);
        }


        if (Dynamics.vv_vel_hill_yd < -0.5f && Dynamics.vv_pos_hill_yd < 450f && Dynamics.vv_pos_hill_yd > 250f)
        {
            this.info_message.GetComponent<Text>().text = "[速度警告]\nステーションに衝突しないために減速してください。";
            this.guide_left.SetActive(true);
        }
        else if (Dynamics.vv_vel_hill_yd < -0.4f && Dynamics.vv_pos_hill_yd < 250f && Dynamics.vv_pos_hill_yd > 30f)
        {
            this.info_message.GetComponent<Text>().text = "[速度警告]\nステーションに衝突しないために減速してください。";
            this.guide_left.SetActive(true);
        }
        else if (Dynamics.vv_vel_hill_yd < -0.02f && Dynamics.vv_pos_hill_yd < 30f)
        {
            this.info_message.GetComponent<Text>().text = "[速度警告]\n速度超過。ドッキングに備えて減速してください。";
            this.guide_left.SetActive(true);
        }
        else if (Dynamics.vv_vel_hill_yd < -0.013f && Dynamics.vv_pos_hill_yd < 25f)
        {
            this.info_message.GetComponent<Text>().text = "[速度警告]\n速度超過。ドッキングに備えて十分に減速してください。";
            this.guide_left.SetActive(true);
        }
        else
        {
            this.guide_left.SetActive(false);
        }

    }


        void notification_docking_failure()
    {

        if ((Dynamics.vv_pos_hill_yd) * (float)(Mathf.Tan(4.5f / 180f * Mathf.PI)) < Dynamics.vv_pos_hill_xd && Dynamics.vv_pos_hill_yd > 50f)
        {
            this.info_message.GetComponent<Text>().text = "[ミッション失敗]\nコリドー逸脱。安全化のためアボートしました。";
            this.game_status_failure.SetActive(true);
            this.game_status_capture_timer.SetActive(false);
            Time.timeScale = 0.1f;
            sound_failure();
            sound_flag = 0;

            Dynamics.vv_vel_hill_y0 = 2.0f;
        }

        if ((Dynamics.vv_pos_hill_yd) * (float)(Mathf.Tan(0.1f / 180f * Mathf.PI)) < Dynamics.vv_pos_hill_xd && Dynamics.vv_pos_hill_yd < 50f)
        {
            this.info_message.GetComponent<Text>().text = "[ミッション失敗]\nコリドー逸脱。安全化のためアボートしました。";
            this.game_status_failure.SetActive(true);
            this.game_status_capture_timer.SetActive(false);
            Time.timeScale = 0.1f;
            sound_failure();
            sound_flag = 0;

            Dynamics.vv_vel_hill_y0 = 2.0f;
        }

        if (-(Dynamics.vv_pos_hill_yd) * (float)(Mathf.Tan(20.0f / 180f * Mathf.PI)) > Dynamics.vv_pos_hill_xd && Dynamics.vv_pos_hill_yd < 500f)
        {
            this.info_message.GetComponent<Text>().text = "[ミッション失敗]\nコリドー逸脱。安全化のためアボートしました。";
            this.game_status_failure.SetActive(true);
            this.game_status_capture_timer.SetActive(false);
            Time.timeScale = 0.1f;
            sound_failure();
            sound_flag = 0;

            Dynamics.vv_vel_hill_y0 = 2.0f;
        }

        if (Mathf.Abs(Dynamics.vv_pos_hill_yd) > 1000 || Mathf.Abs(Dynamics.vv_pos_hill_xd) > 1000)
        {
            this.info_message.GetComponent<Text>().text = "[ミッション失敗]\nステーションから離れてしまった。";
            this.game_status_failure.SetActive(true);
            this.game_status_capture_timer.SetActive(false);
            Time.timeScale = 0.01f;
            sound_failure();
            sound_flag = 0;
        }
        if (timer_after_scale > 60 * 60 * 2)
        {
            this.info_message.GetComponent<Text>().text = "[ミッション失敗] \n時間を使いすぎました。";
            this.game_status_failure.SetActive(true);
            this.game_status_capture_timer.SetActive(false);
            Time.timeScale = 0.01f;
            sound_failure();
            sound_flag = 0;
        }
        if (flag_no_propellant == 1)
        {
            this.info_message.GetComponent<Text>().text = "[ミッション失敗] \n燃料切れです。";
            this.game_status_failure.SetActive(true);
            this.game_status_capture_timer.SetActive(false);
            Time.timeScale = 0.01f;
            sound_failure();
            sound_flag = 0;
        }

        if (flag_collision == 1)
        {
        this.info_message.GetComponent<Text>().text = "[ミッション失敗] \nドッキング失敗。速度が速すぎて衝突しました。";
        this.game_status_failure.SetActive(true);
        this.game_status_capture_timer.SetActive(false);
        Time.timeScale = 0.01f;
        sound_failure();
        sound_flag = 0;
        }
>>>>>>> flight-phase:Assets/Script/Flight/Director.cs

    }

}