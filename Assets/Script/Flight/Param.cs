//// Param.cs
//// 変更の必要がない基本パラメータ

namespace Param
{

    public static class Co
    {
        public const int TIME_SCALE_COMMON = 50;  // time scale [-]
        public const float ROTATE_SPEED_EARTH = 0.00417807901f;  // earth rotational speed [degree/sec]
        public const float STATION_ALTITUDE = 400000f;  // ISS altitude [m]
        public const float EARTH_RADIOUS    = 6378140f;  // earth radious [m]
        public const float ISS_INC_ANG = 51.6f;  // ISS angle of inclination [deg]
        public const float GRAVITY_CONST = 398600.5f; // mu = G*M [ km^3/s^2 ]

    }

}