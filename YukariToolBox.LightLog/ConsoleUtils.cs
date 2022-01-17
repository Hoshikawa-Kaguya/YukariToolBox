using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace YukariToolBox.LightLog;

internal static class ConsoleUtils
{
    #region 通用

    public static string GetTypeName()
    {
        StackTrace  trace = new StackTrace();
        StackFrame? frame = trace.GetFrame(3);
        if (frame is null) return string.Empty;
        MethodBase? method = frame.GetMethod();
        if (method is null) return string.Empty;
        Type? declaringType = method.DeclaringType;
        if (declaringType is null) return string.Empty;
        string? name = declaringType.ReflectedType?.FullName;
        if (string.IsNullOrEmpty(name)) return declaringType.FullName ?? string.Empty;
        return name;
    }

    #endregion

    #region 着色设置

    internal static void WithConsoleColor(this Action action, ConsoleColor fColor, ConsoleColor bColor)
    {
        var (beforeFg, beforeBg) = (Console.ForegroundColor, Console.BackgroundColor);
        action?.Invoke();
        (Console.ForegroundColor, Console.BackgroundColor) = (beforeFg, beforeBg);
    }

    internal static void ChangeConsoleColor(ConsoleColor fColor, ConsoleColor bColor)
    {
        Console.ForegroundColor = fColor;
        Console.BackgroundColor = bColor;
    }

    #endregion
}