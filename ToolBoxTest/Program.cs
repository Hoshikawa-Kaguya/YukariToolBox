using System;
using System.Threading;
using YukariToolBox.Time;

namespace ToolBoxTest
{
    static class Program
    {
        private static void Main(string[] args)
        {
            Console.WriteLine("开始啦");
            var ret = TimeMeter.Count(() =>
                                        {
                                            Thread.Sleep(1000);
                                            return 0;
                                        });
            Console.WriteLine("结束啦");
            Console.WriteLine(ret.timeSpan.TotalMilliseconds);

            Console.ReadKey();
        }
    }
}