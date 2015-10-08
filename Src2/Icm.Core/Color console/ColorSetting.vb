Namespace Icm.ColorConsole

    ''' <summary>
    ''' Console color setting.
    ''' </summary>
    ''' <remarks>
    ''' <para>ColorSetting is thought to be used with Using statements, for example:</para>
    ''' <code>
    ''' Using New ColorSetting(ConsoleColor.Yellow, ConsoleColor.DarkRed)
    '''     Console.WriteLine('Message with yellow foreground and dark red background')
    ''' End Using
    ''' </code>
    ''' <para>The advantage of this approach is that colors are restored after the Using block, so you can
    ''' implement nested configurations:</para>
    ''' <code>
    ''' Using New ColorSetting(ConsoleColor.Cyan)
    '''     Console.WriteLine('Message with cyan foreground and default background color')
    '''     Using New ColorSetting(ConsoleColor.Yellow, ConsoleColor.DarkRed)
    '''         Console.WriteLine('Message with yellow foreground and dark red background')
    '''     End Using
    '''     Console.WriteLine('Another message with cyan foreground and default background color')
    ''' End Using
    ''' </code>
    ''' </remarks>
    Public Class ColorSetting
        Implements IDisposable

        Private ReadOnly _previousFgColor As ConsoleColor
        Private ReadOnly _previousBgColor As ConsoleColor

        ''' <summary>
        ''' 
        ''' </summary>
        ''' <param name="fgColor">Foreground color. Pass Nothing to preserve existing foreground color.</param>
        ''' <param name="bgcolor">Foreground color. Pass Nothing or omit to preserve existing background color.</param>
        ''' <remarks></remarks>
        Public Sub New(Optional ByVal fgColor As ConsoleColor? = Nothing, Optional ByVal bgcolor As ConsoleColor? = Nothing)
            Dim realFgColor As ConsoleColor
            Dim realBgColor As ConsoleColor
            _previousFgColor = Console.ForegroundColor
            _previousBgColor = Console.BackgroundColor
            If fgColor.HasValue Then
                realFgColor = fgColor.V
                Console.ForegroundColor = realFgColor
            Else
                ' Don't change foreground
            End If
            If bgcolor.HasValue Then
                realBgColor = bgcolor.V
                Console.BackgroundColor = realBgColor
            Else
                ' Don't change background
            End If
        End Sub

        ''' <summary>
        ''' Restores previous color configuration
        ''' </summary>
        ''' <remarks></remarks>
        Public Sub Dispose() Implements IDisposable.Dispose
            Console.ForegroundColor = _previousFgColor
            Console.BackgroundColor = _previousBgColor
        End Sub
    End Class
End Namespace
