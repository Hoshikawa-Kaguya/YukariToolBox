using System;
using System.Text;
using System.Threading;

namespace YukariToolBox.Console
{
    public class YukariConsoleLoggerService : IConsoleLogService
    {
        #region 控制台锁
        private static readonly object ConsoleWriterLock = new();
        #endregion
        
        #region 格式化控制台Log函数
        /// <summary>
        /// 向控制台发送Info信息
        /// </summary>
        /// <param name="type">类型</param>
        /// <param name="message">信息内容</param>
        public void Info(object type, object message)
        {
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
        public void Warning(object type, object message)
        {
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
        public void Error(object type, object message)
        {
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
        public void Fatal(object type, object message)
        {
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
        public void Debug(object type, object message)
        {
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
        public void UnhandledExceptionLog(UnhandledExceptionEventArgs args)
        {
            StringBuilder errorLogBuilder = new StringBuilder();
            errorLogBuilder.Append("检测到未处理的异常");
            if (args.IsTerminating)
                errorLogBuilder.Append("，服务器将停止运行");
            errorLogBuilder.Append("，错误信息:");
            errorLogBuilder
                .Append(ConsoleLog.ErrorLogBuilder(args.ExceptionObject as Exception));
            Fatal("Sora", errorLogBuilder);
            Warning("Sora", "将在5s后自动退出");
            Thread.Sleep(5000);
            Environment.Exit(-1);
        }
        #endregion
    }
}