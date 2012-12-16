Namespace Icm.ColorConsole

    ''' <summary>
    ''' String with foreground and background colors.
    ''' </summary>
    ''' <remarks>
    ''' </remarks>
    Public Class ColorString

        Property ForegroundColor() As ConsoleColor?
        Property BackgroundColor() As ConsoleColor?
        Property [String]() As String

        Public Sub New(ByVal str As String, ByVal foreclr As ConsoleColor?, ByVal backclr As ConsoleColor?)
            ForegroundColor = foreclr
            BackgroundColor = backclr
            [String] = str
        End Sub

    End Class

End Namespace
