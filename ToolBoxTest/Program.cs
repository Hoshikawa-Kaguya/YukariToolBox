using System;
using System.Globalization;
using YukariToolBox.LightLog;

namespace ToolBoxTest;

internal static class Program
{
    private static void Main(string[] args)
    {
        Log.SetConfiguration(new LogConfiguration()
                             .SetLogLevel(LogLevel.Verbos)
                             .EnableConsoleOutput()
                             .SetLogCultureInfo(CultureInfo.InvariantCulture));
        Log.SetLogLevel(LogLevel.Verbos);
        Log.Verbos("wow", "wow");
        Log.Debug("wow", "wow");
        Log.Info("wow", "wow");
        Log.Warning("wow", "wow");
        Log.Error("wow", "wow");
        Log.Fatal(new Exception("shit"), "wow", "wow");
    }
}