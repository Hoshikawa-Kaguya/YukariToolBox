using System.Globalization;

namespace YukariToolBox.LightLog
{
    public interface ILogService
    {
        public void Info(string source, string message);
        public void Info<T>(string source, string message, T context);
        public void Warning(string source, string message);
        public void Warning<T>(string source, string message, T context);
        public void Error(string source, string message);
        public void Error(Exception exception, string source, string message);
        public void Error<T>(string source, string message, T context);
        public void Error<T>(Exception exception, string source, string message, T context);
        public void Fatal(Exception exception, string source, string message);
        public void Fatal<T>(Exception exception, string source, string message, T context);
        public void Debug(string source, string message);
        public void Debug<T>(string source, string message, T context);
        public void Verbos(string source, string message);
        public void Verbos<T>(string source, string message, T context);
        public void UnhandledExceptionLog(UnhandledExceptionEventArgs args);
        public void SetCultureInfo(CultureInfo cultureInfo);
    }
}