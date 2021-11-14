using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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

    public LogConfiguration()
    {
        ConsoleOutput = false;
        LogLevel      = LogLevel.Info;
        Culture       = CultureInfo.CurrentCulture;
        LogServices   = new List<ILogService>();
    }

    #endregion

    #region 流式接口

    public LogConfiguration EnableConsoleOutput()
    {
        ConsoleOutput = true;
        return this;
    }

    public LogConfiguration DisableConsoleOutput()
    {
        ConsoleOutput = false;
        return this;
    }

    public LogConfiguration SetLogLevel(LogLevel level)
    {
        LogLevel = level;
        return this;
    }

    public LogConfiguration SetLogCultureInfo(CultureInfo culture)
    {
        Culture = culture;
        return this;
    }

    public LogConfiguration AddLogService(ILogService logService)
    {
        LogServices.Add(logService);
        return this;
    }

    #endregion
}