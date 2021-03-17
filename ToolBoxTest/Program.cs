using System;
using System.Threading;
using System.Threading.Tasks;
using YukariToolBox.Extensions;
using YukariToolBox.FormatLog;
using YukariToolBox.Time;

namespace ToolBoxTest
{
    class Program
    {
        struct person
        {
            public int    id;
            public string a;
        }

        static void Main(string[] args)
        {
            Log.SetLogLevel(LogLevel.Debug);
            Log.Debug("wow", "wow");
            Log.Info("wow", "wow");
            Log.Fatal("wow", "wow");
            Log.Warning("wow", "wow");
            Log.Error("wow", "wow");
            Console.WriteLine("开始啦");
            var ret = TimeMeter.Count(async () =>
                                      {
                                          await Task.Delay(1234);
                                          return 1;
                                      });
            Console.WriteLine("结束啦");
            Console.WriteLine(ret.Result.timeSpan.TotalMilliseconds);

            Console.ReadKey();
        }
    }
}