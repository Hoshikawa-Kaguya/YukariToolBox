using System;

namespace YukariToolBox.TimeStamp
{
    /// <summary>
    /// <para>用于DateTime和时间戳的转换</para>
    /// <para>时间戳的获取</para>
    /// </summary>
    public static class TimeConvert
    {
        /// <summary>
        /// 获取今天零点的时间戳
        /// 时间戳单位(秒)
        /// </summary>
        public static long GetTodayStampLong() =>(long) (DateTime.Today - new DateTime(1970, 1, 1, 8, 0, 0, 0)).TotalSeconds;

        /// <summary>
        /// 获取现在的时间戳
        /// 时间戳单位(秒)
        /// </summary>
        public static long GetNowTimeStamp() =>(long) (DateTime.Now - new DateTime(1970, 1, 1, 8, 0, 0, 0)).TotalSeconds;

        /// <summary>
        /// DateTime转时间戳
        /// </summary>
        public static long ToTimeStamp(this DateTime date) =>(long) (date - new DateTime(1970, 1, 1, 8, 0, 0, 0)).TotalSeconds;

        /// <summary>
        /// 时间戳转DateTime
        /// </summary>
        public static DateTime ToDateTimw(this long stamp) =>new DateTime(1970, 1, 1, 8, 0, 0, 0).AddSeconds(stamp);
    }
}
