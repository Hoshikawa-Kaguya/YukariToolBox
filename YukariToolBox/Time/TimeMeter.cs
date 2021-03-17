using System;
using System.Runtime.InteropServices;
using System.Threading;
using System.Threading.Tasks;

namespace YukariToolBox.Time
{
    public static class TimeMeter
    {
        [DllImport("Kernel32.dll")]
        private static extern bool QueryPerformanceCounter(out long lpPerformanceCount); //查询高精度计数器该时刻的实际值

        [DllImport("Kernel32.dll")]
        private static extern bool QueryPerformanceFrequency(out long lpFrequency); //查询高精度计数器每秒的计数次数

        public static (T data, TimeSpan timeSpan) Count<T>(Func<T> action)
        {
            long _startTime;
            long _stopTime;
            long _frequency = 0;
            var useHighPerformance = Environment.OSVersion.Platform == PlatformID.Win32NT &&
                                     QueryPerformanceFrequency(out _frequency);

            Thread.Sleep(0);
            if (useHighPerformance)
                QueryPerformanceCounter(out _startTime);
            else
                _startTime = DateTime.Now.ToTimeStamp(true);

            var ret = action();

            if (useHighPerformance)
                QueryPerformanceCounter(out _stopTime);
            else
                _stopTime = DateTime.Now.ToTimeStamp(true);

            var eliminateMilliSeconds = useHighPerformance
                ? Convert.ToInt32((double) (_stopTime - _startTime) * 1000 / _frequency)
                : Convert.ToInt32(_stopTime - _startTime);

            var ts = new TimeSpan(0, 0, 0, 0, eliminateMilliSeconds);
            return (ret, ts);
        }

        public static async Task<(T data, TimeSpan timeSpan)> Count<T>(Func<Task<T>> action)
        {
            long _startTime;
            long _stopTime;
            long _frequency = 0;
            var useHighPerformance = Environment.OSVersion.Platform == PlatformID.Win32NT &&
                                     QueryPerformanceFrequency(out _frequency);


            Thread.Sleep(0);
            if (useHighPerformance)
                QueryPerformanceCounter(out _startTime);
            else
                _startTime = DateTime.Now.ToTimeStamp(true);

            var ret = await action();

            if (useHighPerformance)
                QueryPerformanceCounter(out _stopTime);
            else
                _stopTime = DateTime.Now.ToTimeStamp(true);

            var eliminateMilliSeconds = useHighPerformance
                ? Convert.ToInt32((double) (_stopTime - _startTime) * 1000 / _frequency)
                : Convert.ToInt32(_stopTime - _startTime);

            var ts = new TimeSpan(0, 0, 0, 0, eliminateMilliSeconds);
            return (ret, ts);
        }
    }
}