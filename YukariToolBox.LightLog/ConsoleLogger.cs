using System.Globalization;
using System.Text;
using static YukariToolBox.LightLog.ConsoleUtils;

namespace YukariToolBox.LightLog;

internal class ConsoleLogger
{
    #region 控制台缓冲区

    private readonly StringBuilder _buffer = new();

    #endregion

    #region 控制台初始颜色

    private readonly ConsoleColor _consoleColor = Console.BackgroundColor;

    #endregion

    #region 控制台锁

    private readonly object _consoleWriterLock = new();

    #endregion

    #region 区域格式化设置

    private CultureInfo _cultureInfo = CultureInfo.CurrentCulture;

    /// <summary>
    /// 设置日志格式化区域信息
    /// </summary>
    /// <param name="cultureInfo">区域信息</param>
    public void SetCultureInfo(CultureInfo cultureInfo)
    {
        lock (_consoleWriterLock)
        {
            _cultureInfo = cultureInfo;
        }
    }

    #endregion

    #region 标准输出函数

    public ConsoleLogger ChangeColor(ConsoleColor fColor, ConsoleColor bColor)
    {
        ChangeConsoleColor(fColor, bColor);
        return this;
    }

    public ConsoleLogger WriteLine(string msg)
    {
        lock (_consoleWriterLock)
        {
            Console.WriteLine(msg);
        }

        return this;
    }

    public ConsoleLogger Write(string msg)
    {
        lock (_consoleWriterLock)
        {
            Console.Write(msg);
        }

        return this;
    }

    public ConsoleLogger WriteLine(params string[] msg)
    {
        lock (_consoleWriterLock)
        {
            foreach (var s in msg) Console.WriteLine(s);
        }

        return this;
    }

    public ConsoleLogger Write(params string[] msg)
    {
        lock (_consoleWriterLock)
        {
            foreach (var s in msg) Console.Write(s);
        }

        return this;
    }

    public ConsoleLogger BufferAppend(string msg)
    {
        _buffer.Append(msg);
        return this;
    }

    public ConsoleLogger BufferAppendLine(string msg)
    {
        _buffer.AppendLine(msg);
        return this;
    }

    public ConsoleLogger BufferWrite(string msg)
    {
        lock (_consoleWriterLock)
        {
            Console.Write(_buffer.ToString());
            _buffer.Clear();
        }

        return this;
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
        lock (_consoleWriterLock)
        {
            ChangeConsoleColor(ConsoleColor.White, _consoleColor);
            Console.Write($@"{DateTime.Now.ToString(_cultureInfo)}| ");
            ChangeConsoleColor(ConsoleColor.Black, ConsoleColor.White);
            Console.Write(@"Info   ");
            ChangeConsoleColor(ConsoleColor.White, _consoleColor);
            Console.WriteLine($@" | {GetTypeName()}");
            Console.WriteLine($@"[{type}]{message}");
        }
    }

    /// <summary>
    /// 向控制台发送Warning信息
    /// </summary>
    /// <param name="type">类型</param>
    /// <param name="message">信息内容</param>
    public void Warning(string type, string message)
    {
        lock (_consoleWriterLock)
        {
            ChangeConsoleColor(ConsoleColor.DarkYellow, _consoleColor);
            Console.Write($@"{DateTime.Now.ToString(_cultureInfo)}| ");
            ChangeConsoleColor(ConsoleColor.Black, ConsoleColor.DarkYellow);
            Console.Write(@"Warning");
            ChangeConsoleColor(ConsoleColor.DarkYellow, _consoleColor);
            Console.WriteLine($@" | {GetTypeName()}");
            Console.WriteLine($@"[{type}]{message}");
            ChangeConsoleColor(ConsoleColor.White, _consoleColor);
        }
    }

    /// <summary>
    /// 向控制台发送Error信息
    /// </summary>
    /// <param name="type">类型</param>
    /// <param name="message">信息内容</param>
    public void Error(string type, string message)
    {
        lock (_consoleWriterLock)
        {
            ChangeConsoleColor(ConsoleColor.DarkRed, _consoleColor);
            Console.Write($@"{DateTime.Now.ToString(_cultureInfo)}| ");
            ChangeConsoleColor(ConsoleColor.Black, ConsoleColor.DarkRed);
            Console.Write(@"Error  ");
            ChangeConsoleColor(ConsoleColor.DarkRed, _consoleColor);
            Console.WriteLine($@" | {GetTypeName()}");
            Console.WriteLine($@"[{type}]{message}");
            ChangeConsoleColor(ConsoleColor.White, _consoleColor);
        }
    }

    /// <summary>
    /// 向控制台发送Fatal信息
    /// </summary>
    /// <param name="type">类型</param>
    /// <param name="message">信息内容</param>
    public void Fatal(string type, string message)
    {
        lock (_consoleWriterLock)
        {
            ChangeConsoleColor(ConsoleColor.DarkRed, _consoleColor);
            Console.Write($@"{DateTime.Now.ToString(_cultureInfo)}| ");
            ChangeConsoleColor(ConsoleColor.Black, ConsoleColor.DarkRed);
            Console.Write(@"Fatal  ");
            ChangeConsoleColor(ConsoleColor.DarkRed, _consoleColor);
            Console.WriteLine($@" | {GetTypeName()}");
            Console.WriteLine($@"[{type}]{message}");
            ChangeConsoleColor(ConsoleColor.White, _consoleColor);
        }
    }

    /// <summary>
    /// 向控制台发送Debug信息
    /// </summary>
    /// <param name="type">类型</param>
    /// <param name="message">信息内容</param>
    public void Debug(string type, string message)
    {
        lock (_consoleWriterLock)
        {
            ChangeConsoleColor(ConsoleColor.Cyan, _consoleColor);
            Console.Write($@"{DateTime.Now.ToString(_cultureInfo)}| ");
            ChangeConsoleColor(ConsoleColor.Black, ConsoleColor.Cyan);
            Console.Write(@"Debug  ");
            ChangeConsoleColor(ConsoleColor.Cyan, _consoleColor);
            Console.WriteLine($@" | {GetTypeName()}");
            Console.WriteLine($@"[{type}]{message}");
            ChangeConsoleColor(ConsoleColor.White, _consoleColor);
        }
    }

    /// <summary>
    /// 向控制台发送Verbose信息
    /// </summary>
    /// <param name="type">类型</param>
    /// <param name="message">信息内容</param>
    public void Verbose(string type, string message)
    {
        lock (_consoleWriterLock)
        {
            ChangeConsoleColor(ConsoleColor.Green, _consoleColor);
            Console.Write($@"{DateTime.Now.ToString(_cultureInfo)}| ");
            ChangeConsoleColor(ConsoleColor.Black, ConsoleColor.Green);
            Console.Write(@"Verbose");
            ChangeConsoleColor(ConsoleColor.Green, _consoleColor);
            Console.WriteLine($@" | {GetTypeName()}");
            Console.WriteLine($@"[{type}]{message}");
            ChangeConsoleColor(ConsoleColor.White, _consoleColor);
        }
    }

    #endregion
}