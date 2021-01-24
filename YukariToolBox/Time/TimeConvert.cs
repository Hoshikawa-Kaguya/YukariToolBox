using System;

namespace YukariToolBox.Time
{
    /// <summary>
    /// <para>DateTime和时间戳的转换</para>
    /// </summary>
    public static class TimeConvert
    {
        /// <summary>
        /// DateTime转时间戳
        /// </summary>
        public static long ToTimeStamp(this DateTime date) =>(long) (date - new DateTime(1970, 1, 1, 8, 0, 0, 0)).TotalSeconds;

        /// <summary>
        /// 时间戳转DateTime
        /// </summary>
        public static DateTime ToDateTime(this long stamp) =>new DateTime(1970, 1, 1, 8, 0, 0, 0).AddSeconds(stamp);
    }
}
