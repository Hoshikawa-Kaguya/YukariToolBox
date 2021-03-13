using System;
using System.Collections.Generic;
using YukariToolBox.Extensions;
using YukariToolBox.FormatLog;

namespace ToolBoxTest
{
    class Program
    {
        struct person
        {
            public int    id;
            public string a ;
        }

        static void Main(string[] args)
        {
            Log.SetLogLevel(LogLevel.Debug);
            Log.Debug("wow", "wow");
            Log.Info("wow", "wow");
            Log.Fatal("wow", "wow");
            Log.Warning("wow", "wow");
            Log.Error("wow", "wow");

            var list = new List <person>()
            {
                new person()
                {
                    id=1,
                    a="123"
                },
                new person()
                {
                    id =2,
                    a  ="1331223"
                },
                new person()
                {
                    id =3,
                    a  ="32178678123123"
                }
            };
            list.UpdateWhen(i => i.id !=2)
                .ExecuteUpdate(new person()
                {
                    id=2,
                    a="12312211221121212"
                });
                 
            Console.ReadKey();
        }
    }
}