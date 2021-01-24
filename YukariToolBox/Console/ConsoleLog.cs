using System;
using System.Text;
using System.Threading;

namespace YukariToolBox.Console
{
    /// <summary>
    /// 格式化的控制台日志输出
    /// </summary>
    public class ConsoleLog
    {
        #region Log等级设置
        private static LogLevel Level = LogLevel.Info;

        /// <summary>
        /// <para>设置日志等级</para>
        /// <para>如需禁用log请使用<see cref="SetNoLog"/></para>
        /// </summary>
        /// <param name="level">LogLevel</param>
        /// <exception cref="ArgumentOutOfRangeException">loglevel超出正常值</exception>
        public static void SetLogLevel(LogLevel level)
        {
            if (level is < LogLevel.Debug or > LogLevel.Fatal)
                throw new ArgumentOutOfRangeException(nameof(level), "loglevel out of range");
            Level = level;
        }

        /// <summary>
        /// 禁用log
        /// </summary>
        public static void SetNoLog() => Level = (LogLevel) 5;
        #endregion

        #region 控制台锁
        private static readonly object ConsoleWriterLock = new();
        #endregion

        #region 格式化错误Log
        /// <summary>
        /// 生成格式化的错误Log文本
        /// </summary>
        /// <param name="e">错误</param>
        /// <returns>格式化Log</returns>
        public static string ErrorLogBuilder(Exception e)
        {
            StringBuilder errorMessageBuilder = new StringBuilder();
            errorMessageBuilder.Append("\r\n");
            errorMessageBuilder.Append("==============ERROR==============\r\n");
            errorMessageBuilder.Append("Error:");
            errorMessageBuilder.Append(e.GetType().FullName);
            errorMessageBuilder.Append("\r\n\r\n");
            errorMessageBuilder.Append("Message:");
            errorMessageBuilder.Append(e.Message);
            errorMessageBuilder.Append("\r\n\r\n");
            errorMessageBuilder.Append("Stack Trace:\r\n");
            errorMessageBuilder.Append(e.StackTrace);
            errorMessageBuilder.Append("\r\n");
            errorMessageBuilder.Append("=================================\r\n");
            return errorMessageBuilder.ToString();
        }
        #endregion

        #region 格式化控制台Log函数
        /// <summary>
        /// 向控制台发送Info信息
        /// </summary>
        /// <param name="type">类型</param>
        /// <param name="message">信息内容</param>
        public static void Info(object type, object message)
        {
            if (Level > LogLevel.Info) return;
            lock (ConsoleWriterLock)
            {
                System.Console.ForegroundColor = ConsoleColor.White;
                System.Console.WriteLine($@"[{DateTime.Now}][INFO][{type}]{message}");
            }
        }

        /// <summary>
        /// 向控制台发送Warning信息
        /// </summary>
        /// <param name="type">类型</param>
        /// <param name="message">信息内容</param>
        public static void Warning(object type, object message)
        {
            if (Level > LogLevel.Warn) return;
            lock (ConsoleWriterLock)
            {
                System.Console.ForegroundColor = ConsoleColor.White;
                System.Console.Write($@"[{DateTime.Now}][");
                System.Console.ForegroundColor = ConsoleColor.Yellow;
                System.Console.Write(@"WARNINIG");
                System.Console.ForegroundColor = ConsoleColor.White;
                System.Console.Write($@"][{type}]");
                System.Console.ForegroundColor = ConsoleColor.Yellow;
                System.Console.WriteLine($@"{message}");
                System.Console.ForegroundColor = ConsoleColor.White;
            }
        }

        /// <summary>
        /// 向控制台发送Error信息
        /// </summary>
        /// <param name="type">类型</param>
        /// <param name="message">信息内容</param>
        public static void Error(object type, object message)
        {
            if (Level > LogLevel.Error) return;
            lock (ConsoleWriterLock)
            {
                System.Console.ForegroundColor = ConsoleColor.White;
                System.Console.Write($@"[{DateTime.Now}][");
                System.Console.ForegroundColor = ConsoleColor.Red;
                System.Console.Write(@"ERROR");
                System.Console.ForegroundColor = ConsoleColor.White;
                System.Console.Write($@"][{type}]");
                System.Console.ForegroundColor = ConsoleColor.Red;
                System.Console.WriteLine(message);
                System.Console.ForegroundColor = ConsoleColor.White;
            }
        }

        /// <summary>
        /// 向控制台发送Fatal信息
        /// </summary>
        /// <param name="type">类型</param>
        /// <param name="message">信息内容</param>
        public static void Fatal(object type, object message)
        {
            if (Level > LogLevel.Fatal) return;
            lock (ConsoleWriterLock)
            {
                System.Console.ForegroundColor = ConsoleColor.White;
                System.Console.Write($@"[{DateTime.Now}][");
                System.Console.ForegroundColor = ConsoleColor.DarkRed;
                System.Console.Write(@"FATAL");
                System.Console.ForegroundColor = ConsoleColor.White;
                System.Console.Write($@"][{type}]");
                System.Console.ForegroundColor = ConsoleColor.DarkRed;
                System.Console.WriteLine(message);
                System.Console.ForegroundColor = ConsoleColor.White;
            }
        }

        /// <summary>
        /// 向控制台发送Debug信息
        /// </summary>
        /// <param name="type">类型</param>
        /// <param name="message">信息内容</param>
        public static void Debug(object type, object message)
        {
            if (Level != LogLevel.Debug) return;
            lock (ConsoleWriterLock)
            {
                System.Console.ForegroundColor = ConsoleColor.White;
                System.Console.Write($@"[{DateTime.Now}][");
                System.Console.ForegroundColor = ConsoleColor.Cyan;
                System.Console.Write(@"DEBUG");
                System.Console.ForegroundColor = ConsoleColor.White;
                System.Console.Write($@"][{type}]");
                System.Console.ForegroundColor = ConsoleColor.Cyan;
                System.Console.WriteLine(message);
                System.Console.ForegroundColor = ConsoleColor.White;
            }
        }
        #endregion

        #region 全局错误Log
        /// <summary>
        /// 全局错误Log
        /// </summary>
        /// <param name="args">UnhandledExceptionEventArgs</param>
        public static void UnhandledExceptionLog(UnhandledExceptionEventArgs args)
        {
            StringBuilder errorLogBuilder = new StringBuilder();
            errorLogBuilder.Append("检测到未处理的异常");
            if (args.IsTerminating)
                errorLogBuilder.Append("，服务器将停止运行");
            errorLogBuilder.Append("，错误信息:");
            errorLogBuilder
                .Append(ErrorLogBuilder(args.ExceptionObject as Exception));
            Fatal("Sora",errorLogBuilder);
            Warning("Sora","将在5s后自动退出");
            Thread.Sleep(5000);
            Environment.Exit(-1);
        }
        #endregion
    }
}
