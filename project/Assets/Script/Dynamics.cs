//// Dynamics.cs
//// DynamicsAllにアタッチされて、地球の回転・機体の位置の計算、JoystickからのΔV入力を受ける。


using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Dynamics : MonoBehaviour
{

    public FixedJoystick joystick;

    float iss_velocity_scalar;
    float iss_orbital_period;
    float iss_rotation_speed;
    float vv_rotation_speed;

    float TimeNow, TimeLast, TimeDelta;

    public static float iss_coord_pos_x;
    public static float iss_coord_pos_y;
    public static float iss_coord_pos_z;
    public static Vector3 iss_coord_pos;
    public static float vv_coord_pos_x;
    public static float vv_coord_pos_y;
    public static float vv_coord_pos_z;
    public static Vector3 vv_coord_pos;
    public static Vector3 cam_coord_pos;
    public float vv_time_offset = 2;
    Quaternion ISS_attitude_q;
    Quaternion vv_attitude_q;
    public static int debug_mode;

    Vector3 vv_coord_force;
    public static float total_delta_V;

    //float val_dv_const = 0.005f;  // iPhone
    float val_dv_const = 0.02f;  // WebGL
    //float val_dv_const = 0.03f;  // debug




    public static float attitude_cmd_yaw;
    public static float attitude_now;

    public static float deltav_cmd_xp, deltav_cmd_xm, deltav_cmd_yp, deltav_cmd_ym;

    public static float vv_pos_hill_x, vv_pos_hill_y, vv_pos_hill_z;
    public static float vv_vel_hill_x, vv_vel_hill_y, vv_vel_hill_z;
    public static float vv_pos_hill_x0, vv_pos_hill_y0, vv_pos_hill_z0;
    public static float vv_vel_hill_x0, vv_vel_hill_y0, vv_vel_hill_z0;
    public static float vv_pos_hill_xd, vv_pos_hill_yd, vv_pos_hill_zd;
    public static float vv_vel_hill_xd, vv_vel_hill_yd, vv_vel_hill_zd;


    public GameObject orbital_plane;
    GameObject slider2;




    // Start is called before the first frame update
    void Start()
    {
        // ISS Position Calc [meter](なぜか回転スピードが３倍くらい早いので割る３＊＊要調査)
        iss_velocity_scalar = Mathf.Sqrt((float)(Param.Co.GRAVITY_CONST / (Param.Co.EARTH_RADIOUS / 1000 + Param.Co.STATION_ALTITUDE / 1000))) * 1000;      // [meter/sec]
        Debug.Log(iss_velocity_scalar);
        iss_orbital_period = (float)(2 * Mathf.PI * (Param.Co.EARTH_RADIOUS + Param.Co.STATION_ALTITUDE) / iss_velocity_scalar);            // [sec/orbit]
        iss_rotation_speed = 360f / iss_orbital_period;                                                     // [deg/sec]
        iss_coord_pos = new Vector3(Param.Co.STATION_ALTITUDE + Param.Co.EARTH_RADIOUS, 0, 0);

        TimeNow = 0;
        TimeLast = 0;
        TimeDelta = 0;

        debug_mode = 2;


    }





    // Update is called once per frame
    void Update()
    {
        TimeNow = Director.timer_after_scale;
        TimeDelta = TimeNow - TimeLast;


        //********************************************
        //************** Earth Dynamics **************
        //********************************************
        GameObject.Find("Earth").transform.Rotate(new Vector3(0, 0, -Param.Co.ROTATE_SPEED_EARTH * Param.Co.TIME_SCALE_COMMON) * Director.TimedeltaCommon, Space.World);


        //********************************************
        //************** ISS Dynamics ****************
        //********************************************
        iss_coord_pos_x = (Param.Co.STATION_ALTITUDE + Param.Co.EARTH_RADIOUS) * (float)(Mathf.Cos(-iss_rotation_speed * Director.timer_after_scale * Mathf.Deg2Rad));
        iss_coord_pos_y = (Param.Co.STATION_ALTITUDE + Param.Co.EARTH_RADIOUS) * (float)(Mathf.Sin(-iss_rotation_speed * Director.timer_after_scale * Mathf.Deg2Rad)) * (float)(Mathf.Cos(Param.Co.ISS_INC_ANG * Mathf.Deg2Rad));
        iss_coord_pos_z = (Param.Co.STATION_ALTITUDE + Param.Co.EARTH_RADIOUS) * (float)(Mathf.Sin(-iss_rotation_speed * Director.timer_after_scale * Mathf.Deg2Rad)) * (float)(Mathf.Sin(Param.Co.ISS_INC_ANG * Mathf.Deg2Rad));
        iss_coord_pos = new Vector3(iss_coord_pos_x, iss_coord_pos_y, iss_coord_pos_z);
        //iss_coord_pos = Quaternion.Euler(0f, 90f, 0f) * iss_coord_pos;
        GameObject.Find("Station").transform.position = iss_coord_pos;

        // ISS Attitude Calc [degree at ISS]
        ISS_attitude_q = Quaternion.Euler(Param.Co.ISS_INC_ANG, 0, 0) * Quaternion.Euler(0, 0, -iss_rotation_speed * Director.timer_after_scale);
        GameObject.Find("Station").transform.rotation = ISS_attitude_q * Quaternion.Euler(0, -90, 90);


        //********************************************
        //************** Vehicle Dynamics ************
        //********************************************

        if (debug_mode == 1)  // Vehicle motion with an orbit kinematic equation
        {
            // VV Position Calc [meter]
            vv_coord_pos_x = (Param.Co.STATION_ALTITUDE + Param.Co.EARTH_RADIOUS) * (float)(Mathf.Cos(-iss_rotation_speed * (Director.timer_after_scale - vv_time_offset) * Mathf.Deg2Rad));
            vv_coord_pos_y = (Param.Co.STATION_ALTITUDE + Param.Co.EARTH_RADIOUS) * (float)(Mathf.Sin(-iss_rotation_speed * (Director.timer_after_scale - vv_time_offset) * Mathf.Deg2Rad)) * (float)(Mathf.Cos(Param.Co.ISS_INC_ANG * Mathf.Deg2Rad));
            vv_coord_pos_z = (Param.Co.STATION_ALTITUDE + Param.Co.EARTH_RADIOUS) * (float)(Mathf.Sin(-iss_rotation_speed * (Director.timer_after_scale - vv_time_offset) * Mathf.Deg2Rad)) * (float)(Mathf.Sin(Param.Co.ISS_INC_ANG * Mathf.Deg2Rad));
            vv_coord_pos = new Vector3(vv_coord_pos_x, vv_coord_pos_y, vv_coord_pos_z);
            //iss_coord_pos = Quaternion.Euler(0f, 90f, 0f) * iss_coord_pos;
            GameObject.Find("Vehicle").transform.position = vv_coord_pos;

        }
        else if (debug_mode == 2)  // Vehicle motion with CW equation
        {

            // Keyboard input
            if (Input.GetKeyDown("w"))
            {
                deltav_cmd_xp = 1;
            }
            else if (Input.GetKeyUp("w"))
            {
                deltav_cmd_xp = 0;
            }
            if (Input.GetKeyDown("z"))
            {
                deltav_cmd_xm = 1;
            }
            else if (Input.GetKeyUp("z"))
            {
                deltav_cmd_xm = 0;
            }
            if (Input.GetKeyDown("a"))
            {
                deltav_cmd_yp = 1;
            }
            else if (Input.GetKeyUp("a"))
            {
                deltav_cmd_yp = 0;
            }
            if (Input.GetKeyDown("d"))
            {
                deltav_cmd_ym = 1;
            }
            else if (Input.GetKeyUp("d"))
            {
                deltav_cmd_ym = 0;
            }
            // Delta V induce
            vv_vel_hill_x0 += (deltav_cmd_xp * val_dv_const + (-1) * deltav_cmd_xm * val_dv_const) * Time.timeScale;
            vv_vel_hill_y0 += (deltav_cmd_yp * val_dv_const + (-1) * deltav_cmd_ym * val_dv_const) * Time.timeScale;
            vv_vel_hill_z0 += 0;
            total_delta_V += (deltav_cmd_xp * val_dv_const + (1) * deltav_cmd_xm * val_dv_const) * Director.val_sim_speed + (deltav_cmd_yp * val_dv_const + (1) * deltav_cmd_ym * val_dv_const) * Director.val_sim_speed;



            // Physical joystick input
            float input_horizontal = Input.GetAxis("Horizontal");
            float input_vertical = Input.GetAxis("Vertical");
            // Delta V induce
            vv_vel_hill_x0 += (input_vertical * val_dv_const) * Time.timeScale;
            vv_vel_hill_y0 += (-input_horizontal * val_dv_const) * Time.timeScale;
            vv_vel_hill_z0 += 0;
            total_delta_V += (Mathf.Abs(input_vertical) * val_dv_const) * Director.val_sim_speed + (Mathf.Abs(input_horizontal) * val_dv_const) * Director.val_sim_speed;

            GameObject.Find("Joy-Handle2").GetComponent<RectTransform>().localPosition = new Vector3(80f * input_horizontal, 80f * input_vertical, 0);


            // Delta V induce (virtual joystick input)
            vv_vel_hill_x0 += (joystick.Direction.y * val_dv_const) * Time.timeScale;
            vv_vel_hill_y0 += (-joystick.Direction.x * val_dv_const) * Time.timeScale;
            vv_vel_hill_z0 += 0;
            total_delta_V += (Mathf.Abs(joystick.Direction.y) * val_dv_const) * Director.val_sim_speed + (Mathf.Abs(joystick.Direction.x) * val_dv_const) * Director.val_sim_speed;



            // CW equation
            float mu = Param.Co.GRAVITY_CONST * 1000000000f;
            float n_cw = (float)(Mathf.Sqrt(mu / (float)(Mathf.Pow(Param.Co.STATION_ALTITUDE + Param.Co.EARTH_RADIOUS, 3))));
            float s_cw0 = Mathf.Sin(n_cw * TimeDelta);
            float c_cw0 = Mathf.Cos(n_cw * TimeDelta);

            vv_pos_hill_xd = -(3 * vv_pos_hill_x0 + 2 * vv_vel_hill_y0 / n_cw) * c_cw0 + (vv_vel_hill_x0 / n_cw) * s_cw0 + 2 * (2 * vv_pos_hill_x0 + vv_vel_hill_y0 / n_cw);
            vv_pos_hill_yd = (2 * vv_vel_hill_x0 / n_cw) * c_cw0 + 2 * (3 * vv_pos_hill_x0 + 2 * vv_vel_hill_y0 / n_cw) * s_cw0 - 3 * n_cw * (2 * vv_pos_hill_x0 + vv_vel_hill_y0 / n_cw) * TimeDelta + (vv_pos_hill_y0 - 2 * vv_vel_hill_x0 / n_cw);
            vv_pos_hill_zd = 0;

            vv_vel_hill_xd = vv_vel_hill_x0 * c_cw0 + n_cw * (3 * vv_pos_hill_x0 + 2 * vv_vel_hill_y0 / n_cw) * s_cw0;
            vv_vel_hill_yd = 2 * n_cw * (3 * vv_pos_hill_x0 + 2 * vv_vel_hill_y0 / n_cw) * c_cw0 - 2 * vv_vel_hill_x0 * s_cw0 - 3 * n_cw * (2 * vv_pos_hill_x0 + vv_vel_hill_y0 / n_cw);
            vv_vel_hill_zd = 0;


            //　プロット用格納
            vv_pos_hill_x = vv_pos_hill_xd;
            vv_pos_hill_y = vv_pos_hill_yd;
            vv_pos_hill_z = vv_pos_hill_zd;


            // VV Position Calc and plot [meter]
            vv_coord_pos = iss_coord_pos + GameObject.Find("Station").transform.rotation * new Vector3(-vv_pos_hill_y, -vv_pos_hill_z, -vv_pos_hill_x) * Director.prox_model_scale;
            //iss_coord_pos = Quaternion.Euler(0f, 90f, 0f) * iss_coord_pos;
            GameObject.Find("Vehicle").transform.position = vv_coord_pos;


            //// 軌道軌跡の描画
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
            plot_point_line_orbit_station = iss_coord_pos + GameObject.Find("Station").transform.rotation * new Vector3(-2500, 0, 0) * Director.prox_model_scale;
            renderer_orbit_station.SetPosition(0, plot_point_line_orbit_station);
            plot_point_line_orbit_station = iss_coord_pos + GameObject.Find("Station").transform.rotation * new Vector3(2500, 0, 0) * Director.prox_model_scale;
            renderer_orbit_station.SetPosition(1, plot_point_line_orbit_station);

            Vector3 plot_point_line_orbit_station_vertical;
            renderer_orbit_station_vertical.SetVertexCount(2); // 頂点の数
            plot_point_line_orbit_station_vertical = iss_coord_pos + GameObject.Find("Station").transform.rotation * new Vector3(-8, 0, 0) * Director.prox_model_scale;
            renderer_orbit_station_vertical.SetPosition(0, plot_point_line_orbit_station_vertical);
            plot_point_line_orbit_station_vertical = iss_coord_pos + GameObject.Find("Station").transform.rotation * new Vector3(-8, 0, 1000) * Director.prox_model_scale;
            renderer_orbit_station_vertical.SetPosition(1, plot_point_line_orbit_station_vertical);


            // Vehicle
            LineRenderer renderer_orbit = GameObject.Find("OrbitalLine").GetComponent<LineRenderer>();
            if (Director.cam_mode == 1)
            {
                renderer_orbit.SetWidth(100f, 60f); // 線の幅
            }
            else if (Director.cam_mode == 2)
            {
                renderer_orbit.SetWidth(300f, 300f); // 線の幅
            }
            float max_point_line_orbit = 500;
            renderer_orbit.SetVertexCount((int)(max_point_line_orbit + 1)); // 頂点の数

            float time_step_line_orbit;
            float s_cw0_render;
            float c_cw0_render;
            float vv_pos_hill_x_render;
            float vv_pos_hill_y_render;
            float vv_pos_hill_z_render;
            Vector3 plot_point_line_orbit;

            for (int i_temp = 0; i_temp < max_point_line_orbit + 1; ++i_temp)
            {
                time_step_line_orbit = iss_orbital_period * 3 * (float)(i_temp) / (float)(max_point_line_orbit);
                s_cw0_render = Mathf.Sin(n_cw * (0 + time_step_line_orbit));
                c_cw0_render = Mathf.Cos(n_cw * (0 + time_step_line_orbit));
                vv_pos_hill_x_render = -(3 * vv_pos_hill_x0 + 2 * vv_vel_hill_y0 / n_cw) * c_cw0_render + (vv_vel_hill_x0 / n_cw) * s_cw0_render + 2 * (2 * vv_pos_hill_x0 + vv_vel_hill_y0 / n_cw);
                vv_pos_hill_y_render = (2 * vv_vel_hill_x0 / n_cw) * c_cw0_render + 2 * (3 * vv_pos_hill_x0 + 2 * vv_vel_hill_y0 / n_cw) * s_cw0_render - 3 * n_cw * (2 * vv_pos_hill_x0 + vv_vel_hill_y0 / n_cw) * time_step_line_orbit + (vv_pos_hill_y0 - 2 * vv_vel_hill_x0 / n_cw);
                vv_pos_hill_z_render = 0;
                plot_point_line_orbit = iss_coord_pos + GameObject.Find("Station").transform.rotation * new Vector3(-vv_pos_hill_y_render, -vv_pos_hill_z_render, -vv_pos_hill_x_render) * Director.prox_model_scale;
                renderer_orbit.SetPosition(i_temp, plot_point_line_orbit);
            }


            // 更新
            vv_pos_hill_x0 = vv_pos_hill_xd;
            vv_pos_hill_y0 = vv_pos_hill_yd;
            vv_pos_hill_z0 = vv_pos_hill_zd;
            vv_vel_hill_x0 = vv_vel_hill_xd;
            vv_vel_hill_y0 = vv_vel_hill_yd;
            vv_vel_hill_z0 = vv_vel_hill_zd;
        }


        // VV Attitude Calc [degree at VV]
        vv_rotation_speed = iss_rotation_speed;   // [deg/sec]　軌道レートピッチ補正
        float attitude_delta = attitude_now - attitude_cmd_yaw;
        if (attitude_delta > 1) //差分が正(+)の場合
        {
            attitude_now -= 1.0f * Time.timeScale;
        }
        else if (attitude_delta < -1) //差分が負(-)の場合
        {
            attitude_now += 1.0f * Time.timeScale;
        }

        vv_attitude_q = Quaternion.Euler(Param.Co.ISS_INC_ANG, 0, 0) * Quaternion.Euler(0, 0, -vv_rotation_speed * Director.timer_after_scale);
        GameObject.Find("Vehicle").transform.rotation = vv_attitude_q * Quaternion.Euler(0, -90, 90) * Quaternion.Euler(0, 0, attitude_now);



        //********************************************
        //************** Camera Position *************
        //********************************************
        //if (cam_mode == 0)  // 地球中心のビュー
        //{
        //    if (Input.mousePosition.y < Screen.height - 150)
        //    {
        //        // 左クリックした時
        //        if (Input.GetMouseButtonDown(0))
        //        {
        //            // カメラの角度を変数"newAngle"に格納
        //            newAngle = GameObject.Find("Main Camera").transform.localEulerAngles;
        //            // マウス座標を変数"lastMousePosition"に格納
        //            lastMousePosition = Input.mousePosition;
        //        }
        //        // 左ドラッグしている間
        //        else if (Input.GetMouseButton(0))
        //        {
        //            //newAngle.x = newAngle.x - (Input.mousePosition.x - lastMousePosition.x) * rotationSpeed.x;
        //            //newAngle.y = newAngle.y + (lastMousePosition.y - Input.mousePosition.y) * rotationSpeed.y;
        //            //GameObject.Find("Main Camera").transform.rotation = Quaternion.Euler(newAngle);

        //            GameObject.Find("Main Camera").transform.RotateAround(new Vector3(0, 0, 0), new Vector3(0, 0, 1), (Input.mousePosition.x - lastMousePosition.x) * rotationSpeed.x);
        //            //GameObject.Find("Main Camera").transform.RotateAround(new Vector3(0,0,0), new Vector3(0, 1, 0), (Input.mousePosition.y - lastMousePosition.y) * rotationSpeed.y);
        //            // マウス座標を変数"lastMousePosition"に格納
        //            lastMousePosition = Input.mousePosition;
        //        }
        //    }
        //}

        TimeLast = TimeNow;

    }



    // Other functions

    //public void earth_view()
    //{
    //    cam_mode = 0;
    //    GameObject.Find("Main Camera").transform.position = new Vector3(2.2e+07f, 0f, 0f);
    //    GameObject.Find("Main Camera").transform.rotation = Quaternion.Euler(0f, -90f, -90f);
    //    GameObject.Find("Station").transform.localScale = new Vector3(1, 1, 1) * val_scale_station_0;
    //    GameObject.Find("Vehicle").transform.localScale = new Vector3(1, 1, 1) * val_scale_vehicle_0;
    //    GameObject.Find("Main Camera").transform.RotateAround(new Vector3(0, 0, 0), new Vector3(0, 0, 1), -30f);
    //    orbital_plane.SetActive(true);
    //    //GameObject.Find("Coordinate").GetComponent<Text>().text = string.Format("EARTH");
    //}


    public void deltaV_xp_on()
    {
        deltav_cmd_xp = 1;
    }
    public void deltaV_xp_off()
    {
        deltav_cmd_xp = 0;
    }

    public void deltaV_xm_on()
    {
        deltav_cmd_xm = 1;
    }
    public void deltaV_xm_off()
    {
        deltav_cmd_xm = 0;
    }

    public void deltaV_yp_on()
    {
        deltav_cmd_yp = 1;
    }
    public void deltaV_yp_off()
    {
        deltav_cmd_yp = 0;
    }

    public void deltaV_ym_on()
    {
        deltav_cmd_ym = 1;
    }
    public void deltaV_ym_off()
    {
        deltav_cmd_ym = 0;
    }


}

