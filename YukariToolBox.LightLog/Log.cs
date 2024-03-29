using System.Globalization;
using System.Text;

namespace YukariToolBox.LightLog;

/// <summary>
/// 格式化的控制台日志输出
/// </summary>
public static class Log
{
    #region Log设置

    /// <summary>
    /// Log配置
    /// </summary>
    public static LogConfiguration LogConfiguration { get; } = new();

    #endregion

    #region Log等级设置

    /// <summary>
    /// <para>设置日志等级</para>
    /// <para>如需禁用log请使用<see cref="SetNoLog"/></para>
    /// </summary>
    /// <param name="newLevel">LogLevel</param>
    /// <exception cref="ArgumentOutOfRangeException">loglevel超出正常值</exception>
    public static void SetLogLevel(LogLevel newLevel)
    {
        if (newLevel is < LogLevel.Verbose or > LogLevel.Fatal)
            throw new ArgumentOutOfRangeException(nameof(newLevel), "loglevel out of range");
        LogConfiguration.LogLevel = newLevel;
    }

    /// <summary>
    /// 获取当前的日志等级
    /// </summary>
    public static LogLevel GetLogLevel()
    {
        return LogConfiguration.LogLevel;
    }

    /// <summary>
    /// 禁用log
    /// </summary>
    public static void SetNoLog()
    {
        LogConfiguration.LogLevel = (LogLevel) 5;
    }

    #endregion

    #region 区域格式化设置

    /// <summary>
    /// 设置日志格式化区域信息
    /// </summary>
    /// <param name="cultureInfo">区域信息</param>
    public static void SetCultureInfo(CultureInfo cultureInfo)
    {
        LogConfiguration.Culture = cultureInfo;
        _consoleLogger.SetCultureInfo(cultureInfo);
        foreach (var service in LogConfiguration.LogServices) service.SetCultureInfo(cultureInfo);
    }

    #endregion

    #region 输出服务提供者设置

    private static readonly ConsoleLogger _consoleLogger = new();

    #endregion

    #region Log

    /// <summary>
    /// Info
    /// </summary>
    /// <param name="source">源</param>
    /// <param name="message">信息内容</param>
    public static void Info(string source, string message)
    {
        if (LogConfiguration.LogLevel > LogLevel.Info) return;
        if (LogConfiguration.ConsoleOutput) _consoleLogger.Info(source, message);
        foreach (var service in LogConfiguration.LogServices) service.Info(source, message);
    }

    /// <summary>
    /// Info
    /// </summary>
    /// <param name="source">源</param>
    /// <param name="message">信息内容</param>
    /// <param name="context">自定义数据</param>
    public static void Info<T>(string source, string message, T context)
    {
        if (LogConfiguration.LogLevel > LogLevel.Info) return;
        if (LogConfiguration.ConsoleOutput) _consoleLogger.Info(source, $"{message}|{context}");
        foreach (var service in LogConfiguration.LogServices) service.Info(source, message, context);
    }

    /// <summary>
    /// Warning
    /// </summary>
    /// <param name="source">源</param>
    /// <param name="message">信息内容</param>
    public static void Warning(string source, string message)
    {
        if (LogConfiguration.LogLevel > LogLevel.Warn) return;
        if (LogConfiguration.ConsoleOutput) _consoleLogger.Warning(source, message);
        foreach (var service in LogConfiguration.LogServices) service.Warning(source, message);
    }

    /// <summary>
    /// Warning
    /// </summary>
    /// <param name="source">源</param>
    /// <param name="message">信息内容</param>
    /// <param name="context">自定义数据</param>
    public static void Warning<T>(string source, string message, T context)
    {
        if (LogConfiguration.LogLevel > LogLevel.Warn) return;
        if (LogConfiguration.ConsoleOutput) _consoleLogger.Warning(source, $"{message}|{context}");
        foreach (var service in LogConfiguration.LogServices) service.Warning(source, message, context);
    }

    /// <summary>
    /// Error
    /// </summary>
    /// <param name="source">源</param>
    /// <param name="message">信息内容</param>
    public static void Error(string source, string message)
    {
        if (LogConfiguration.LogLevel > LogLevel.Error) return;
        if (LogConfiguration.ConsoleOutput) _consoleLogger.Error(source, message);
        foreach (var service in LogConfiguration.LogServices) service.Error(source, message);
    }

    /// <summary>
    /// Error
    /// </summary>
    /// <param name="exception">异常</param>
    /// <param name="source">源</param>
    /// <param name="message">信息内容</param>
    public static void Error(Exception exception, string source, string message)
    {
        if (LogConfiguration.LogLevel > LogLevel.Error) return;
        if (LogConfiguration.ConsoleOutput) _consoleLogger.Error(source, $"{message}\r\n{ErrorLogBuilder(exception)}");
        foreach (var service in LogConfiguration.LogServices) service.Error(exception, source, message);
    }

    /// <summary>
    /// Error
    /// </summary>
    /// <param name="context">自定义数据</param>
    /// <param name="source">源</param>
    /// <param name="message">信息内容</param>
    public static void Error<T>(string source, string message, T context)
    {
        if (LogConfiguration.LogLevel > LogLevel.Error) return;
        if (LogConfiguration.ConsoleOutput) _consoleLogger.Error(source, $"{message}|{context}");
        foreach (var service in LogConfiguration.LogServices) service.Error(source, message, context);
    }

    /// <summary>
    /// Error
    /// </summary>
    /// <param name="context">自定义数据</param>
    /// <param name="exception">异常</param>
    /// <param name="source">源</param>
    /// <param name="message">信息内容</param>
    public static void Error<T>(Exception exception, string source, string message, T context)
    {
        if (LogConfiguration.LogLevel > LogLevel.Error) return;
        if (LogConfiguration.ConsoleOutput)
            _consoleLogger.Error(source, $"{message}|{context}\r\n{ErrorLogBuilder(exception)}");
        foreach (var service in LogConfiguration.LogServices) service.Error(exception, source, message, context);
    }

    /// <summary>
    /// Fatal
    /// </summary>
    /// <param name="exception">异常</param>
    /// <param name="source">源</param>
    /// <param name="message">信息内容</param>
    public static void Fatal(Exception exception, string source, string message)
    {
        if (LogConfiguration.LogLevel > LogLevel.Fatal) return;
        if (LogConfiguration.ConsoleOutput)
        {
            _consoleLogger.Fatal(source, message);
            _consoleLogger.Fatal(source, $"\r\n{ErrorLogBuilder(exception)}");
        }

        foreach (var service in LogConfiguration.LogServices) service.Fatal(exception, source, message);
    }

    /// <summary>
    /// Fatal
    /// </summary>
    /// <param name="exception">异常</param>
    /// <param name="source">源</param>
    /// <param name="message">信息内容</param>
    /// <param name="context">自定义数据</param>
    public static void Fatal<T>(Exception exception, string source, string message, T context)
    {
        if (LogConfiguration.LogLevel > LogLevel.Fatal) return;
        if (LogConfiguration.ConsoleOutput)
        {
            _consoleLogger.Fatal(source, $"{message}|with context:{context}");
            _consoleLogger.Fatal(source, $"\r\n{ErrorLogBuilder(exception)}");
        }

        foreach (var service in LogConfiguration.LogServices) service.Fatal(exception, source, message, context);
    }

    /// <summary>
    /// Debug
    /// </summary>
    /// <param name="source">源</param>
    /// <param name="message">信息内容</param>
    public static void Debug(string source, string message)
    {
        if (LogConfiguration.LogLevel > LogLevel.Debug) return;
        if (LogConfiguration.ConsoleOutput) _consoleLogger.Debug(source, message);
        foreach (var service in LogConfiguration.LogServices) service.Debug(source, message);
    }

    /// <summary>
    /// Debug
    /// </summary>
    /// <param name="source">源</param>
    /// <param name="message">信息内容</param>
    /// <param name="context">自定义数据</param>
    public static void Debug<T>(string source, string message, T context)
    {
        if (LogConfiguration.LogLevel > LogLevel.Debug) return;
        if (LogConfiguration.ConsoleOutput) _consoleLogger.Debug(source, $"{message}|{context}");
        foreach (var service in LogConfiguration.LogServices) service.Debug(source, message, context);
    }

    /// <summary>
    /// Verbos
    /// </summary>
    /// <param name="source">源</param>
    /// <param name="message">信息内容</param>
    public static void Verbose(string source, string message)
    {
        if (LogConfiguration.LogLevel != LogLevel.Verbose) return;
        if (LogConfiguration.ConsoleOutput) _consoleLogger.Verbose(source, message);
        foreach (var service in LogConfiguration.LogServices) service.Verbose(source, message);
    }

    /// <summary>
    /// Verbos
    /// </summary>
    /// <param name="source">源</param>
    /// <param name="message">信息内容</param>
    /// <param name="context">自定义数据</param>
    public static void Verbose<T>(string source, string message, T context)
    {
        if (LogConfiguration.LogLevel != LogLevel.Verbose) return;
        if (LogConfiguration.ConsoleOutput) _consoleLogger.Verbose(source, $"{message}|{context}");
        foreach (var service in LogConfiguration.LogServices) service.Verbose(source, message, context);
    }

    #endregion

    #region 全局错误Log

    /// <summary>
    /// 全局错误Log
    /// </summary>
    /// <param name="args">UnhandledExceptionEventArgs</param>
    public static void UnhandledExceptionLog(UnhandledExceptionEventArgs args)
    {
        if (LogConfiguration.ConsoleOutput && args.ExceptionObject is Exception ex)
            _consoleLogger.Fatal("UnhandledException", ErrorLogBuilder(ex));
        foreach (var service in LogConfiguration.LogServices) service.UnhandledExceptionLog(args);
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
        var errorMessageBuilder = new StringBuilder();
        errorMessageBuilder.AppendLine("==============ERROR INFO==============\r\n");
        errorMessageBuilder.Append("Error:");
        errorMessageBuilder.AppendLine(e.GetType().FullName);
        errorMessageBuilder.AppendLine();
        errorMessageBuilder.Append("Message:");
        errorMessageBuilder.AppendLine(e.Message);
        errorMessageBuilder.AppendLine();
        errorMessageBuilder.Append("Stack Trace:\r\n");
        errorMessageBuilder.AppendLine(e.StackTrace);
        errorMessageBuilder.Append("======================================");
        return errorMessageBuilder.ToString();
    }

    #endregion
}