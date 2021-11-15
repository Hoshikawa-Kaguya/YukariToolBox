using System.Globalization;

namespace YukariToolBox.LightLog;

internal class ConsoleLogger
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
    public void Info(string type, string message)
    {
        lock (ConsoleWriterLock)
        {
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine($@"[{DateTime.Now.ToString(_cultureInfo)}][Info][{type}]{message}");
        }
    }

    /// <summary>
    /// 向控制台发送Warning信息
    /// </summary>
    /// <param name="type">类型</param>
    /// <param name="message">信息内容</param>
    public void Warning(string type, string message)
    {
        lock (ConsoleWriterLock)
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine($@"[{DateTime.Now.ToString(_cultureInfo)}][Warning][{type}]{message}");
        }
    }

    /// <summary>
    /// 向控制台发送Error信息
    /// </summary>
    /// <param name="type">类型</param>
    /// <param name="message">信息内容</param>
    public void Error(string type, string message)
    {
        lock (ConsoleWriterLock)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine($@"[{DateTime.Now.ToString(_cultureInfo)}][Error][{type}]{message}");
        }
    }

    /// <summary>
    /// 向控制台发送Fatal信息
    /// </summary>
    /// <param name="type">类型</param>
    /// <param name="message">信息内容</param>
    public void Fatal(string type, string message)
    {
        lock (ConsoleWriterLock)
        {
            Console.ForegroundColor = ConsoleColor.DarkRed;
            Console.WriteLine($@"[{DateTime.Now.ToString(_cultureInfo)}][Fatal][{type}]{message}");
        }
    }

    /// <summary>
    /// 向控制台发送Debug信息
    /// </summary>
    /// <param name="type">类型</param>
    /// <param name="message">信息内容</param>
    public void Debug(string type, string message)
    {
        lock (ConsoleWriterLock)
        {
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine($@"[{DateTime.Now.ToString(_cultureInfo)}][Debug][{type}]{message}");
        }
    }

    /// <summary>
    /// 向控制台发送Verbos信息
    /// </summary>
    /// <param name="type">类型</param>
    /// <param name="message">信息内容</param>
    public void Verbos(string type, string message)
    {
        lock (ConsoleWriterLock)
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine($@"[{DateTime.Now.ToString(_cultureInfo)}][Verbos][{type}]{message}");
        }
    }

    #endregion
}