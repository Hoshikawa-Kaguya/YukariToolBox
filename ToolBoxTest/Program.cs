using System;
using System.Globalization;
using YukariToolBox.LightLog;

Log.SetConfiguration(new LogConfiguration()
                     .SetLogLevel(LogLevel.Verbos)
                     .EnableConsoleOutput()
                     // .AddLogService(CustomLogger)
                     .SetLogCultureInfo(CultureInfo.InvariantCulture));
Log.Verbos("wow", "wow");
Log.Debug("wow", "wow");
Log.Info("wow", "wow");
Log.Warning("wow", "wow");
Log.Error("wow", "wow");
Log.Fatal(new Exception("shit"), "wow", "wow");