using YukariToolBox.FormatLog;

namespace ToolBoxTest
{
    class Program
    {
        static void Main(string[] args)
        {
            Log.SetLogLevel(LogLevel.Debug);
            Log.Debug("wow","wow");
            Log.Info("wow","wow");
            Log.Fatal("wow","wow");
            Log.Warning("wow","wow");
            Log.Error("wow","wow");
        }
    }
}
