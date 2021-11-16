using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YukariToolBox.LightLog
{

    public static class ConsoleUtils
    {
        #region 通用

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
}