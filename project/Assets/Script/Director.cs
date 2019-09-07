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
    float timer, timer_after_scale;
    GameObject slider;
    GameObject sim_timer;
    GameObject info_val_x, info_val_y, info_val_z, info_val_vx, info_val_vy, info_val_vz, info_val_attitude, info_message, info_val_total_dv, game_status_capture_timer, game_status_hold_timer;
    GameObject info_val_vx_slider, info_val_vy_slider;
    GameObject game_status_result, game_status_failure;
    GameObject game_status_result_duration, game_status_result_dv, game_status_result_relative_v,game_status_result_score;
    GameObject game_status_vv_propellant_val, game_status_vv_propellant_gage;
    GameObject plane_movie_capture, video_capture;

    float result_dv, result_duration, result_relative_v, result_score;

    public static float elappsedTime, timer_capture, timer_capture_clear, timer_hold_clear_250m, timer_hold_clear_100m, timer_hold_clear_30m, timer_hold_250m, timer_hold_100m, timer_hold_30m, timer_hold_clear;
    public static int game_submode;
    public static int flag_250m_HP, flag_100m_HP, flag_30m_HP, flag_10m_HP, flag_collision, flag_KOS, flag_clear, flag_10m_HP_stay, flag_no_propellant;
    public static int flag_250m_HP_stay, flag_100m_HP_stay, flag_30m_HP_stay;
    public static float val_sim_speed;
    public static int sound_flag, flag_cam;

	public static int cam_mode;      //カメラモード
	float cam_coord_pos_x;
	float cam_coord_pos_y;
	float cam_coord_pos_z;
	GameObject cam_targetObj;
	Vector3 cam_targetPos;
    int cam_initial_rotate = 0;
    float cam_initial_rotate_val = 0;

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
    float capture_duration = 10 * 60;
    float hold_duration = 5 * 60;
    float vv_propellant_max = 1000;
    float vv_propellant_now;
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

        this.plane_movie_capture = GameObject.Find("CaptureVideoPlane");
        this.video_capture = GameObject.Find("VideoCapture");


        time_medium();
        Time.timeScale = val_sim_speed;
        timer = 0;
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
            cam_time_offset = 100000f;
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
            timer_hold_clear_250m = 0f;
            timer_hold_clear_100m = 0f;
            timer_hold_clear_30m = 0f;
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
        }


        //////////////////////////////////////////////////////
        // game_scene == 2:Proxモードの場合の初期設定
        //////////////////////////////////////////////////////
        ///
        if (GameMaster.game_scene == 2)
        {
            game_submode = 0;
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
        // ステーションに対する機体の初期位置
        //////////////////////////////////////////////////////
        ///
        if (GameMaster.game_scene == 1)
        {
            Dynamics.vv_pos_hill_x0 = -489.58f;
            Dynamics.vv_pos_hill_y0 = -245.44f;
            Dynamics.vv_pos_hill_z0 = 0f;

            Dynamics.vv_vel_hill_x0 = 0.516f;      // [meter/sec]
            Dynamics.vv_vel_hill_y0 = 0.653f;
            Dynamics.vv_vel_hill_z0 = 0f;

            Dynamics.vv_pos_hill_x0 = -812.9f;
            Dynamics.vv_pos_hill_y0 = -441.0f;
            Dynamics.vv_pos_hill_z0 = 0f;

            Dynamics.vv_vel_hill_x0 = 0.882f;      // [meter/sec]
            Dynamics.vv_vel_hill_y0 = 1.236f;
            Dynamics.vv_vel_hill_z0 = 0f;

            Dynamics.vv_pos_hill_xd = Dynamics.vv_pos_hill_x0;
            Dynamics.vv_pos_hill_yd = Dynamics.vv_pos_hill_y0;
            Dynamics.vv_pos_hill_zd = Dynamics.vv_pos_hill_z0;
        }
        else if (GameMaster.game_scene == 2)
        {
            Dynamics.vv_pos_hill_x0 = -150f;
            Dynamics.vv_pos_hill_y0 = -2000f;
            Dynamics.vv_pos_hill_z0 = 0f;

            Dynamics.vv_vel_hill_x0 = 0.28f;      // [meter/sec]
            Dynamics.vv_vel_hill_y0 = 0.2f;
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
        elappsedTime += Time.deltaTime; //[sec]
        Time.timeScale = val_sim_speed;
        timer += Time.deltaTime;
        timer_after_scale = timer * Param.Co.TIME_SCALE_COMMON;

        // Time View
        int days = Mathf.FloorToInt(timer_after_scale / 60F / 60F/ 24F);
        int hours = Mathf.FloorToInt(timer_after_scale / 60F / 60F - days *24);
        int minutes = Mathf.FloorToInt(timer_after_scale / 60f - hours * 60 - days * 24 * 60);
        int seconds = Mathf.FloorToInt(timer_after_scale - minutes * 60 - hours * 60 * 60 - days * 24 * 60 * 60);
        string niceTime = string.Format("{0:00}:{1:00}:{2:00}", hours, minutes, seconds);
        this.sim_timer.GetComponent<Text>().text = "" + niceTime;


        //////////////////////////////////////////////////////
        // game_scene == 1:R-Barモードの場合のゲームコントロール
        //////////////////////////////////////////////////////
        ///
        if (GameMaster.game_scene == 1)
        {
            // ゲーム中は結果画面を非表示設定
            this.game_status_result.SetActive(false);
            this.game_status_failure.SetActive(false);
            this.plane_movie_capture.SetActive(false);

            if (-Dynamics.vv_pos_hill_xd < 360f && Dynamics.vv_pos_hill_xd <100f)
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
            if (game_submode == 0 && Mathf.Abs(Dynamics.vv_pos_hill_yd) < 800)
            {
                game_submode = 1;
                this.game_status_capture_timer.SetActive(false);
                this.game_status_hold_timer.SetActive(false);
            }
            if (game_submode == 1 && Mathf.Abs(Dynamics.vv_pos_hill_yd) < 500)
            {
                game_submode = 2;
                this.info_message.GetComponent<Text>().text = "軌道に沿って２５０ｍ下方を目指してください。";
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
            if (game_submode == 3 && flag_100m_HP == 1 && flag_100m_HP_stay == 1)
            {
                game_submode =34;
            }
            if (flag_100m_HP == 1 && flag_100m_HP_stay == 0)
            {
                game_submode = 43;
            }
            if (timer_hold_clear_100m > hold_duration)
            {
                game_submode = 4;
            }
            if (game_submode == 4 && flag_30m_HP == 1 && flag_30m_HP_stay == 1)
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
            if (game_submode == 51 && flag_10m_HP == 1 && flag_30m_HP == 1 && flag_100m_HP == 1 && flag_250m_HP == 1 && flag_10m_HP_stay == 1)
            {
                game_submode = 5;
            }
            if (flag_10m_HP == 1 && flag_30m_HP == 1 && flag_100m_HP == 1 && flag_250m_HP == 1 && flag_10m_HP_stay == 0)
            {
                game_submode = 54;
            }
            if (timer_capture_clear > capture_duration)
            {
                game_submode = 55;
            }
            if (flag_10m_HP == 1 && flag_30m_HP == 0)
            {
                game_submode = 6;
            }
            if (flag_collision == 1)
            {
                game_submode = 7;
            }
            if (Mathf.Abs(Dynamics.vv_pos_hill_yd) > 500)
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
            if (Mathf.Abs(Dynamics.vv_pos_hill_xd -28f) * (float)(Mathf.Tan(16f / 180f * Mathf.PI))　< (Mathf.Abs(Dynamics.vv_pos_hill_yd -6.5f)))
            {
                game_submode = 99;
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
            else if (game_submode == 2)
            {
                this.info_message.GetComponent<Text>().text = "軌道に沿って２５０ｍ下方を目指してください。";
            }
            else if (game_submode == 23)
            {
                this.info_message.GetComponent<Text>().text = "250mホールドポイント。そのままの位置を保って！";
                timer_hold_clear_250m = timer_after_scale - timer_hold_250m;
                this.game_status_hold_timer.GetComponent<Text>().text = string.Format("250m Departure - {0:0.0}", hold_duration - timer_hold_clear_250m) + "秒";
            }
            else if (game_submode == 3)
            {
                this.info_message.GetComponent<Text>().text = "２５０ｍ出発可能。上昇してください。";
                this.game_status_hold_timer.GetComponent<Text>().text = "250m Depature, Go HTV!!";
            }
            else if (game_submode == 32)
            {
                this.info_message.GetComponent<Text>().text = "250mホールドポイントにもどって！";
                timer_hold_250m = 0;
            }
            else if (game_submode == 34)
            {
                this.info_message.GetComponent<Text>().text = "100mホールドポイント。そのままの位置を保って！";
                timer_hold_clear_100m = timer_after_scale - timer_hold_100m;
                this.game_status_hold_timer.GetComponent<Text>().text = string.Format("100m Departure - {0:0.0}", hold_duration - timer_hold_clear_100m) + "秒";
            }
            else if (game_submode == 4)
            {
                this.info_message.GetComponent<Text>().text = "１００ｍ出発可能。さらに上昇してください。";
                this.game_status_hold_timer.GetComponent<Text>().text = "100m Depature, Go HTV!!";
            }
            else if (game_submode == 43)
            {
                this.info_message.GetComponent<Text>().text = "100mホールドポイントにもどって！";
                timer_hold_100m = 0;
            }
            else if (game_submode == 451)
            {
                this.info_message.GetComponent<Text>().text = "30mホールドポイント。そのままの位置を保って！";
                timer_hold_clear_30m = timer_after_scale - timer_hold_30m;
                this.game_status_hold_timer.GetComponent<Text>().text = string.Format("30m Departure - {0:0.0}", hold_duration - timer_hold_clear_30m) + "秒";
            }
            else if (game_submode == 51)
            {
                this.info_message.GetComponent<Text>().text = "3０ｍ出発可能。キャプチャー点まで上昇してください。";
                this.game_status_hold_timer.GetComponent<Text>().text = "30m Depature, Go for Final Approach!!";
            }
            else if (game_submode == 541)
            {
                this.info_message.GetComponent<Text>().text = "30mホールドポイントにもどって！";
                timer_hold_30m = 0;
            }
            else if (game_submode == 5)
            {
                this.info_message.GetComponent<Text>().text = "キャプチャー点到着、そのままの位置を保って！";
                timer_capture_clear = timer_after_scale - timer_capture;

                this.game_status_capture_timer.GetComponent<Text>().text = string.Format("Capture - {0:0.0}", capture_duration-timer_capture_clear) + "秒";
                result_duration = timer_after_scale / 60f;
                result_dv = Dynamics.total_delta_V;
                result_relative_v = Mathf.Sqrt(Mathf.Pow(Dynamics.vv_vel_hill_xd, 2) + Mathf.Pow(Dynamics.vv_vel_hill_yd, 2) + Mathf.Pow(Dynamics.vv_vel_hill_zd, 2));
                result_score = (1 / result_duration /60f　* 4000f + 1　/ (result_dv) * 100f + 1 / (result_relative_v)) * 2.8f + 50f;
            }
            else if (game_submode == 54)
            {
                this.info_message.GetComponent<Text>().text = "キャプチャー点にもどって！";
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

                this.info_message.GetComponent<Text>().text = "[ミッション成功] 宇宙飛行士がキャプチャー完了！";
                Time.timeScale = 0.01f;
                this.game_status_capture_timer.SetActive(false);
                this.game_status_result.SetActive(true);
                sound_success();
                sound_flag = 0;
            }
            else if (game_submode == 6)
            {
                this.info_message.GetComponent<Text>().text = "[ミッション失敗] 安全な経路で飛行しよう。";
                this.game_status_failure.SetActive(true);
                this.game_status_capture_timer.SetActive(false);
                Time.timeScale = 0.01f;
                sound_failure();
                sound_flag = 0;
            }
            else if (game_submode == 7)
            {
                this.info_message.GetComponent<Text>().text = "[ミッション失敗] 宇宙ステーションに衝突しました。";
                this.game_status_failure.SetActive(true);
                this.game_status_capture_timer.SetActive(false);
                Time.timeScale = 0.01f;
                sound_failure();
                sound_flag = 0;
            }
            else if (game_submode == 8)
            {
                this.info_message.GetComponent<Text>().text = "[ミッション失敗]ステーションから500ｍ離れてしまった。";
                this.game_status_failure.SetActive(true);
                this.game_status_capture_timer.SetActive(false);
                Time.timeScale = 0.01f;
                sound_failure();
                sound_flag = 0;
            }
            else if (game_submode == 9)
            {
                this.info_message.GetComponent<Text>().text = "[ミッション失敗] 時間を使いすぎました。";
                this.game_status_failure.SetActive(true);
                this.game_status_capture_timer.SetActive(false);
                Time.timeScale = 0.01f;
                sound_failure();
                sound_flag = 0;
            }
            else if (game_submode == 10)
            {
                this.info_message.GetComponent<Text>().text = "[注意] 宇宙ステーションに接近しています。";
            }
            else if (game_submode == 11)
            {
                this.info_message.GetComponent<Text>().text = "[ミッション失敗] 燃料切れです。";
                this.game_status_failure.SetActive(true);
                this.game_status_capture_timer.SetActive(false);
                Time.timeScale = 0.01f;
                sound_failure();
                sound_flag = 0;
            }
            else if (game_submode == 99 && flag_250m_HP == 1)
            {
                this.info_message.GetComponent<Text>().text = "[ミッション失敗] アプローチ・コリドー逸脱を検知。\nアボートしました。";
                this.game_status_failure.SetActive(true);
                this.game_status_capture_timer.SetActive(false);
                Time.timeScale = 1.5f;

                if (flag_cam == 0)
                {
                    Dynamics.vv_vel_hill_y0 += 1.1f;
                }
                flag_cam = 1;
                
                sound_failure();
                sound_flag = 0;
            }
        }

        if (GameMaster.game_scene == 2)
        {

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
        if (cam_mode ==1 || cam_mode == 2)
        {
            this.info_val_x.GetComponent<Text>().text = string.Format("{0:0.00}", -Dynamics.vv_pos_hill_x);
            this.info_val_y.GetComponent<Text>().text = string.Format("{0:0.00}", Dynamics.vv_pos_hill_y-8f);
            //this.info_val_z.GetComponent<Text>().text = string.Format("{0:0.00} m", Dynamics.vv_pos_hill_z);

            this.info_val_vx.GetComponent<Text>().text = string.Format("{0:0.000}", -Dynamics.vv_vel_hill_xd);
            this.info_val_vy.GetComponent<Text>().text = string.Format("{0:0.000}", Dynamics.vv_vel_hill_yd);
            //this.info_val_vz.GetComponent<Text>().text = string.Format("{0:0.000} m/sec", Dynamics.vv_vel_hill_zd);
            //this.info_val_total_dv.GetComponent<Text>().text = string.Format("{0:0.000} m/sec", Dynamics.total_delta_V);

            this.info_val_vx_slider.GetComponent<Slider>().value = -Dynamics.vv_vel_hill_xd * 1.3f + 0.5f;
            this.info_val_vy_slider.GetComponent<Slider>().value = Dynamics.vv_vel_hill_yd * 1.3f + 0.5f;

            vv_propellant_now = (vv_propellant_max - vv_propellant_coefficient * Dynamics.total_delta_V) / vv_propellant_max;

            if (vv_propellant_now < 0)
            {
                vv_propellant_now = 0.0f;
                flag_no_propellant = 1;
            }

            this.game_status_vv_propellant_val.GetComponent<Text>().text = string.Format("{0:0.0} %", vv_propellant_now * 100);
            this.game_status_vv_propellant_gage.GetComponent<Image>().fillAmount = vv_propellant_now * 0.9f;

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
            if (cam_mode == 1) // VV中心のビュー
            {

                ////はじめにカメラをまわす
                //if (cam_initial_rotate == 0)
                //{
                //    cam_initial_rotate_val += 1f;
                //}
                //if (cam_initial_rotate_val > 90)
                //{
                //    cam_initial_rotate = 1;
                //}

                //はじめにカメラをまわす
                if (cam_initial_rotate == 0)
                {
                    cam_initial_rotate_val += 1f;
                }
                if (cam_initial_rotate_val > 90)
                {
                    cam_initial_rotate = 1;
                }

                GameObject.Find("Main Camera").transform.rotation = GameObject.Find("Vehicle").transform.rotation * Quaternion.Euler(0, 0, -Dynamics.attitude_now) * Quaternion.Euler(180, 90, -90) * Quaternion.Euler(5 - (90 - cam_initial_rotate_val), 70, 5) * Quaternion.Euler(0, 0, 0) * Quaternion.Euler(delta_newAngle);
                Vector3 cam_pos_offset_2 = new Vector3(-45000 / 100, 50000 / 100, -1 * cam_time_offset * prox_model_scale / 70 * ((Mathf.Abs(Dynamics.vv_pos_hill_xd) + 30 )/ 250));
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
                GameObject.Find("Sub Camera").transform.rotation = GameObject.Find("Station").transform.rotation * Quaternion.Euler(0, 0, -Dynamics.attitude_now) * Quaternion.Euler(180, 90, -90) * Quaternion.Euler(65, 155, 15);
                Vector3 cam_pos_offset_sub = new Vector3(-500, -1000, -1 * cam_time_offset * prox_model_scale /1000);
                GameObject.Find("Sub Camera").transform.position = GameObject.Find("Station").transform.position + GameObject.Find("Sub Camera").transform.rotation * cam_pos_offset_sub;
            }

        }


        //////////////////////////////////////////////////////
        // game_scene == 2:Proxモードの場合のCamera View
        //////////////////////////////////////////////////////
        ///
        if (GameMaster.game_scene == 2)
        {
            if (cam_mode == 1) // VV中心のビュー
            {
                if (Input.mousePosition.y < Screen.height - Screen.height * 0.3)
                {
                    //cam_time_offset = GameObject.Find("Cam_Offset").GetComponent<Slider>().value;
                    cam_scroll = Input.GetAxis("Mouse ScrollWheel");
                    cam_time_offset -= cam_scroll * cam_speed;
                    if (cam_time_offset >= 100000)
                    {
                        cam_time_offset = 100000;
                    }
                    else if (cam_time_offset <= 10000)
                    {
                        cam_time_offset = 10000;
                    }
                    //GameObject.Find("Cam_Offset").GetComponent<Slider>().value = cam_time_offset;

                }


                GameObject.Find("Main Camera").transform.rotation = GameObject.Find("Vehicle").transform.rotation * Quaternion.Euler(0, 0, -Dynamics.attitude_now) * Quaternion.Euler(180, 90, -90) * Quaternion.Euler(-10, 45, 25) * Quaternion.Euler(0, 0, 0) * Quaternion.Euler(delta_newAngle);
                Vector3 cam_pos_offset_2 = new Vector3(-45000 / 100, 50000 / 100, -1 * cam_time_offset * prox_model_scale /300);
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
                if (cam_time_offset >= 100000)
                {
                    cam_time_offset = 100000;
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

    }




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
        }

        if (GameMaster.game_scene == 1)
        {
            if (other2.gameObject.gameObject.tag != "30mHPC")
            {
                flag_30m_HP_stay = 0;
                timer_hold_30m = 0;
                this.game_status_hold_timer.SetActive(false);
            }
        }
        if (GameMaster.game_scene == 1)
        {
            if (other2.gameObject.gameObject.tag != "100mHPC")
            {
                flag_100m_HP_stay = 0;
                timer_hold_100m = 0;
                this.game_status_hold_timer.SetActive(false);
            }
        }
        if (GameMaster.game_scene == 1)
        {
            if (other2.gameObject.gameObject.tag != "250mHPC")
            {
                flag_250m_HP_stay = 0;
                timer_hold_250m = 0;
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
    }

    public void title_scene()
    {

        Time.timeScale = 1.0f;
        SceneManager.LoadScene("TitleScene");
        //val_sim_speed = 1.0f;
        //Time.timeScale = val_sim_speed;
        //FadeManager.Instance.LoadScene("TitleScene", 0.3f);
    }
    


}
