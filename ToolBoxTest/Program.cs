using System;
using System.Globalization;
using YukariToolBox.LightLog;

//修改log设置
Log.LogConfiguration
   .SetLogLevel(LogLevel.Verbose)
   .EnableConsoleOutput()
    // .AddLogService(CustomLogger)
   .SetLogCultureInfo(CultureInfo.InvariantCulture);
//Log
Log.Verbose("wow", "wow");
Log.Debug("wow", "wow");
Log.Info("wow", "wow");
Log.Warning("wow", "wow");
Log.Error("wow", "wow");
Log.Fatal(new Exception("shit"), "wow", "wow");