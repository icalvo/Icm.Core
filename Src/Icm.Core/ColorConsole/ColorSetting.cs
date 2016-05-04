using Microsoft.VisualBasic;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
namespace Icm.ColorConsole
{

    /// <summary>
    /// Console color setting.
    /// </summary>
    /// <remarks>
    /// <para>ColorSetting is thought to be used with Using statements, for example:</para>
    /// <code>
    /// Using New ColorSetting(ConsoleColor.Yellow, ConsoleColor.DarkRed)
    ///     Console.WriteLine('Message with yellow foreground and dark red background')
    /// End Using
    /// </code>
    /// <para>The advantage of this approach is that colors are restored after the Using block, so you can
    /// implement nested configurations:</para>
    /// <code>
    /// Using New ColorSetting(ConsoleColor.Cyan)
    ///     Console.WriteLine('Message with cyan foreground and default background color')
    ///     Using New ColorSetting(ConsoleColor.Yellow, ConsoleColor.DarkRed)
    ///         Console.WriteLine('Message with yellow foreground and dark red background')
    ///     End Using
    ///     Console.WriteLine('Another message with cyan foreground and default background color')
    /// End Using
    /// </code>
    /// </remarks>
    public class ColorSetting : IDisposable
    {

        private readonly ConsoleColor _previousFgColor;

        private readonly ConsoleColor _previousBgColor;
        /// <summary>
        /// 
        /// </summary>
        /// <param name="fgColor">Foreground color. Pass Nothing to preserve existing foreground color.</param>
        /// <param name="bgcolor">Foreground color. Pass Nothing or omit to preserve existing background color.</param>
        /// <remarks></remarks>
        public ColorSetting(ConsoleColor? fgColor = null, ConsoleColor? bgcolor = null)
        {
            _previousFgColor = Console.ForegroundColor;
            _previousBgColor = Console.BackgroundColor;
            if (fgColor.HasValue)
            {
                Console.ForegroundColor = fgColor.Value;
            }

            if (bgcolor.HasValue)
            {
                Console.BackgroundColor = bgcolor.Value;
            }
        }

        /// <summary>
        /// Restores previous color configuration
        /// </summary>
        /// <remarks></remarks>
        public void Dispose()
        {
            Console.ForegroundColor = _previousFgColor;
            Console.BackgroundColor = _previousBgColor;
        }
    }
}
