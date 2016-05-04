using System;

namespace Icm.ColorConsole
{

    /// <summary>
    /// String with foreground and background colors.
    /// </summary>
    /// <remarks>
    /// </remarks>
    public class ColorString
    {

        public ConsoleColor? ForegroundColor { get; }
        public ConsoleColor? BackgroundColor { get; }
        public string String { get; }

        public ColorString(string str, ConsoleColor? foreclr, ConsoleColor? backclr)
        {
            ForegroundColor = foreclr;
            BackgroundColor = backclr;
            String = str;
        }

    }

}
