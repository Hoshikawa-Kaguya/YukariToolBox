using System.Globalization;

// ReSharper disable PropertyCanBeMadeInitOnly.Global

namespace YukariToolBox.LightLog;

/// <summary>
/// log配置文件
/// </summary>
public class LogConfiguration
{
    #region 配置项

    internal bool              ConsoleOutput { get; set; }
    internal LogLevel          LogLevel      { get; set; }
    internal CultureInfo       Culture       { get; set; }
    internal List<ILogService> LogServices   { get; set; }

    #endregion

    #region 构造函数

    /// <summary>
    /// 默认的设置
    /// </summary>
    public LogConfiguration()
    {
        ConsoleOutput = false;
        LogLevel      = LogLevel.Info;
        Culture       = CultureInfo.CurrentCulture;
        LogServices   = new List<ILogService>();
    }

    #endregion

    #region 流式接口

    /// <summary>
    /// 启用控制台输出
    /// </summary>
    public LogConfiguration EnableConsoleOutput()
    {
        ConsoleOutput = true;
        return this;
    }

    /// <summary>
    /// 关闭控制台输出
    /// </summary>
    public LogConfiguration DisableConsoleOutput()
    {
        ConsoleOutput = false;
        return this;
    }

    /// <summary>
    /// 设置log等级
    /// </summary>
    public LogConfiguration SetLogLevel(LogLevel level)
    {
        LogLevel = level;
        return this;
    }

    /// <summary>
    /// 设置区域
    /// </summary>
    /// <param name="culture">区域</param>
    public LogConfiguration SetLogCultureInfo(CultureInfo culture)
    {
        Culture = culture;
        return this;
    }

    /// <summary>
    /// 添加自定义的log服务
    /// </summary>
    /// <param name="logService">log服务</param>
    public LogConfiguration AddLogService(ILogService logService)
    {
        LogServices.Add(logService);
        return this;
    }

    #endregion
}