using System;
using System.Collections.Generic;
using YukariToolBox.Extensions;
using YukariToolBox.FormatLog;

namespace ToolBoxTest
{
    class Program
    {
        class person
        {
            public int    id;
            public string a = "";
        }

        static void Main(string[] args)
        {
            Log.SetLogLevel(LogLevel.Debug);
            Log.Debug("wow", "wow");
            Log.Info("wow", "wow");
            Log.Fatal("wow", "wow");
            Log.Warning("wow", "wow");
            Log.Error("wow", "wow");

            var list = new List<int>() {1, 2, 3, 4, 5};
            list.WhereUpdate(i => i !=2)
                .Update(1);
                 
            Console.ReadKey();
        }
    }
}