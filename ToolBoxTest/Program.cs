using System;
using System.Threading.Tasks;
using YukariToolBox.Time;

namespace ToolBoxTest
{
    static class Program
    {
        private static void Main(string[] args)
        {
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