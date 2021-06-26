using System;
using System.Globalization;
using System.Text;

namespace YukariToolBox.FormatLog
{
    /// <summary>
    /// 格式化的控制台日志输出
    /// </summary>
    public class Log
    {
        #region Log等级设置

        private static LogLevel _level = LogLevel.Info;

        /// <summary>
        /// <para>设置日志等级</para>
        /// <para>如需禁用log请使用<see cref="SetNoLog"/></para>
        /// </summary>
        /// <param name="newLevel">LogLevel</param>
        /// <exception cref="ArgumentOutOfRangeException">loglevel超出正常值</exception>
        public static void SetLogLevel(LogLevel newLevel)
        {
            if (newLevel is < LogLevel.Debug or > LogLevel.Fatal)
                throw new ArgumentOutOfRangeException(nameof(newLevel), "loglevel out of range");
            _level = newLevel;
        }

        /// <summary>
        /// 禁用log
        /// </summary>
        public static void SetNoLog() => _level = (LogLevel) 5;

        #endregion

        #region 区域格式化设置

        /// <summary>
        /// 设置日志格式化区域信息
        /// </summary>
        /// <param name="cultureInfo">区域信息</param>
        public static void SetCultureInfo(CultureInfo cultureInfo)
        {
            _logger.SetCultureInfo(cultureInfo);
        }

        #endregion

        #region 输出服务提供者设置

        /// <summary>
        /// 输出服务
        /// </summary>
        private static ILogService _logger = new YukariLoggerService();

        /// <summary>
        /// 设置控制台输出服务
        /// </summary>
        /// <param name="newLogger">新的控制台输出服务</param>
        public static void SetLoggerService(ILogService newLogger)
        {
            _logger = newLogger;
        }

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
            if (_level > LogLevel.Info) return;
            _logger.Info(type, message);
        }

        /// <summary>
        /// 向控制台发送Warning信息
        /// </summary>
        /// <param name="type">类型</param>
        /// <param name="message">信息内容</param>
        public static void Warning(object type, object message)
        {
            if (_level > LogLevel.Warn) return;
            _logger.Warning(type, message);
        }

        /// <summary>
        /// 向控制台发送Error信息
        /// </summary>
        /// <param name="type">类型</param>
        /// <param name="message">信息内容</param>
        public static void Error(object type, object message)
        {
            if (_level > LogLevel.Error) return;
            _logger.Error(type, message);
        }

        /// <summary>
        /// 向控制台发送Fatal信息
        /// </summary>
        /// <param name="type">类型</param>
        /// <param name="message">信息内容</param>
        public static void Fatal(object type, object message)
        {
            if (_level > LogLevel.Fatal) return;
            _logger.Fatal(type, message);
        }

        /// <summary>
        /// 向控制台发送Debug信息
        /// </summary>
        /// <param name="type">类型</param>
        /// <param name="message">信息内容</param>
        public static void Debug(object type, object message)
        {
            if (_level != LogLevel.Debug) return;
            _logger.Debug(type, message);
        }

        #endregion

        #region 全局错误Log

        /// <summary>
        /// 全局错误Log
        /// </summary>
        /// <param name="args">UnhandledExceptionEventArgs</param>
        public static void UnhandledExceptionLog(UnhandledExceptionEventArgs args)
        {
            _logger.UnhandledExceptionLog(args);
        }

        #endregion
    }
}