using System;

namespace YukariToolBox.Console
{
    public interface IConsoleLogService
    {
        public void Info(object type, object message);

        public void Warning(object type, object message);

        public void Error(object type, object message);

        public void Fatal(object type, object message);

        public void Debug(object type, object message);

        public void UnhandledExceptionLog(UnhandledExceptionEventArgs args);
    }
}