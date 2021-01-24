using System;

namespace YukariToolBox.Time
{
    /// <summary>
    /// 时间戳相关
    /// </summary>
    public static class TimeStamp
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
    }
}
