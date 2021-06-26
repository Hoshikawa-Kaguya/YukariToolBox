using System;
using System.Globalization;
using System.Text;
using System.Threading;

namespace YukariToolBox.FormatLog
{
    public class YukariLoggerService : ILogService
    {
        #region 控制台锁

        private readonly object ConsoleWriterLock = new();

        #endregion
        
        #region 区域格式化设置

        private CultureInfo _cultureInfo = CultureInfo.CurrentCulture;

        /// <summary>
        /// 设置日志格式化区域信息
        /// </summary>
        /// <param name="cultureInfo">区域信息</param>
        public void SetCultureInfo(CultureInfo cultureInfo)
        {
            lock (ConsoleWriterLock)
            {
                _cultureInfo = cultureInfo;
            }
        }

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
                Console.ForegroundColor = ConsoleColor.White;
                Console.WriteLine($@"[{DateTime.Now.ToString(_cultureInfo)}][INFO][{type}]{message}");
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
                Console.ForegroundColor = ConsoleColor.White;
                Console.Write($@"[{DateTime.Now.ToString(_cultureInfo)}][");
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.Write(@"WARNINIG");
                Console.ForegroundColor = ConsoleColor.White;
                Console.Write($@"][{type}]");
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine($@"{message}");
                Console.ForegroundColor = ConsoleColor.White;
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
                Console.ForegroundColor = ConsoleColor.White;
                Console.Write($@"[{DateTime.Now.ToString(_cultureInfo)}][");
                Console.ForegroundColor = ConsoleColor.Red;
                Console.Write(@"ERROR");
                Console.ForegroundColor = ConsoleColor.White;
                Console.Write($@"][{type}]");
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine(message);
                Console.ForegroundColor = ConsoleColor.White;
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
                Console.ForegroundColor = ConsoleColor.White;
                Console.Write($@"[{DateTime.Now.ToString(_cultureInfo)}][");
                Console.ForegroundColor = ConsoleColor.DarkRed;
                Console.Write(@"FATAL");
                Console.ForegroundColor = ConsoleColor.White;
                Console.Write($@"][{type}]");
                Console.ForegroundColor = ConsoleColor.DarkRed;
                Console.WriteLine(message);
                Console.ForegroundColor = ConsoleColor.White;
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
                Console.ForegroundColor = ConsoleColor.White;
                Console.Write($@"[{DateTime.Now.ToString(_cultureInfo)}][");
                Console.ForegroundColor = ConsoleColor.Cyan;
                Console.Write(@"DEBUG");
                Console.ForegroundColor = ConsoleColor.White;
                Console.Write($@"][{type}]");
                Console.ForegroundColor = ConsoleColor.Cyan;
                Console.WriteLine(message);
                Console.ForegroundColor = ConsoleColor.White;
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
            StringBuilder errorLogBuilder = new();
            errorLogBuilder.Append("检测到未处理的异常");
            if (args.IsTerminating)
                errorLogBuilder.Append("进程将停止运行");
            errorLogBuilder.Append("，错误信息:");
            errorLogBuilder
                .Append(Log.ErrorLogBuilder(args.ExceptionObject as Exception));
            Fatal("System", errorLogBuilder);
            Warning("System", "将在5s后自动退出");
            Thread.Sleep(5000);
            Environment.Exit(-1);
        }

        #endregion
    }
}