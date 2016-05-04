using System;
using System.Collections.Generic;

namespace Icm.ColorConsole
{

    public class ColorConsole
    {

        public static ConsoleColor? ErrorForegroundColor = ConsoleColor.Red;
        public static ConsoleColor? ErrorBackgroundColor = null;
        public static ConsoleColor? WarningForegroundColor = ConsoleColor.Yellow;
        public static ConsoleColor? WarningBackgroundColor = null;
        public static ConsoleColor? StepForegroundColor = ConsoleColor.Green;
        public static ConsoleColor? StepBackgroundColor = null;
        public static ConsoleColor? InfoForegroundColor = ConsoleColor.White;
        public static ConsoleColor? InfoBackgroundColor = null;

        public static void WriteError(string format, params object[] args)
        {
            using (new ColorSetting(ErrorForegroundColor, ErrorBackgroundColor))
            {
                Console.Error.Write(format, args);
            }
        }

        public static void WriteLineError(string format, params object[] args)
        {
            using (new ColorSetting(ErrorForegroundColor, ErrorBackgroundColor))
            {
                Console.Error.WriteLine(format, args);
            }
        }

        public static void WriteWarning(string format, params object[] args)
        {
            using (new ColorSetting(WarningForegroundColor, WarningBackgroundColor))
            {
                Console.Error.Write(format, args);
            }
        }

        public static void WriteLineWarning(string format, params object[] args)
        {
            using (new ColorSetting(WarningForegroundColor, WarningBackgroundColor))
            {
                Console.Error.WriteLine(format, args);
            }
        }


        public static void WriteStep(string format, params object[] args)
        {
            using (new ColorSetting(StepForegroundColor, StepBackgroundColor))
            {
                Console.Write(format, args);
            }
        }

        public static void WriteLineStep(string format, params object[] args)
        {
            using (new ColorSetting(ErrorForegroundColor, ErrorBackgroundColor))
            {
                Console.WriteLine(format, args);
            }
        }

        public static void WriteInfo(string format, params object[] args)
        {
            using (new ColorSetting(InfoForegroundColor, InfoBackgroundColor))
            {
                Console.Write(format, args);
            }
        }

        public static void WriteLineInfo(string format, params object[] args)
        {
            using (new ColorSetting(InfoForegroundColor, InfoBackgroundColor))
            {
                Console.WriteLine(format, args);
            }
        }

        public static void Write(ConsoleColor fgcolor, string format, params object[] args)
        {
            Write(new ColorString(string.Format(format, args), fgcolor, null));
        }

        public static void WriteLine(ConsoleColor fgcolor, string format, params object[] args)
        {
            WriteLine(new ColorString(string.Format(format, args), fgcolor, null));
        }

        public static void Write(ConsoleColor fgcolor, string value)
        {
            Write(new ColorString(value, fgcolor, null));
        }

        public static void WriteLine(ConsoleColor fgcolor, string value)
        {
            WriteLine(new ColorString(value, fgcolor, null));
        }

        public static void WriteLine(ColorString value)
        {
            Write(value);
            Console.WriteLine();
        }

        public static void WriteLine(IEnumerable<ColorString> value)
        {
            Write(value);
            Console.WriteLine();
        }

        public static void Write(ColorString cstring)
        {
            using (new ColorSetting(cstring.ForegroundColor, cstring.BackgroundColor))
            {
                Console.Write(cstring.String);
            }
        }

        public static void Write(IEnumerable<ColorString> csb)
        {
            foreach (var cstring in csb)
            {
                Write(cstring);
            }
        }

    }

}
