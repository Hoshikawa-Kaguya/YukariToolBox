using System;
using System.Globalization;

namespace YukariToolBox.FormatLog
{
    public interface ILogService
    {
        public void Info(object type, object message);

        public void Warning(object type, object message);

        public void Error(object type, object message);

        public void Fatal(object type, object message);

        public void Debug(object type, object message);

        public void UnhandledExceptionLog(UnhandledExceptionEventArgs args);

        public void SetCultureInfo(CultureInfo cultureInfo);
    }
}