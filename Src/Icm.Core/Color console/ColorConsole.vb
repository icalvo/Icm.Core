Imports Icm.Collections
Imports res = My.Resources.CommandLine

Namespace Icm.CommandLine

    Public Class ColorConsole

        Public Shared ErrorForegroundColor As ConsoleColor? = ConsoleColor.Red
        Public Shared ErrorBackgroundColor As ConsoleColor? = Nothing
        Public Shared WarningForegroundColor As ConsoleColor? = ConsoleColor.Yellow
        Public Shared WarningBackgroundColor As ConsoleColor? = Nothing
        Public Shared StepForegroundColor As ConsoleColor? = ConsoleColor.Green
        Public Shared StepBackgroundColor As ConsoleColor? = Nothing
        Public Shared InfoForegroundColor As ConsoleColor? = ConsoleColor.White
        Public Shared InfoBackgroundColor As ConsoleColor? = Nothing


        Public Shared Sub WriteError(format As String, ParamArray args() As Object)
            Using New ColorSetting(ErrorForegroundColor, ErrorBackgroundColor)
                Console.Error.Write(format, args)
            End Using
        End Sub

        Public Shared Sub WriteLineError(format As String, ParamArray args() As Object)
            Using New ColorSetting(ErrorForegroundColor, ErrorBackgroundColor)
                Console.Error.WriteLine(format, args)
            End Using
        End Sub

        Public Shared Sub WriteWarning(format As String, ParamArray args() As Object)
            Using New ColorSetting(WarningForegroundColor, WarningBackgroundColor)
                Console.Error.Write(format, args)
            End Using
        End Sub

        Public Shared Sub WriteLineWarning(format As String, ParamArray args() As Object)
            Using New ColorSetting(WarningForegroundColor, WarningBackgroundColor)
                Console.Error.WriteLine(format, args)
            End Using
        End Sub


        Public Shared Sub WriteStep(format As String, ParamArray args() As Object)
            Using New ColorSetting(StepForegroundColor, StepBackgroundColor)
                Console.Write(format, args)
            End Using
        End Sub

        Public Shared Sub WriteLineStep(format As String, ParamArray args() As Object)
            Using New ColorSetting(ErrorForegroundColor, ErrorBackgroundColor)
                Console.WriteLine(format, args)
            End Using
        End Sub

        Public Shared Sub WriteInfo(format As String, ParamArray args() As Object)
            Using New ColorSetting(InfoForegroundColor, InfoBackgroundColor)
                Console.Write(format, args)
            End Using
        End Sub

        Public Shared Sub WriteLineInfo(format As String, ParamArray args() As Object)
            Using New ColorSetting(InfoForegroundColor, InfoBackgroundColor)
                Console.WriteLine(format, args)
            End Using
        End Sub

        Public Shared Sub Write(fgcolor As ConsoleColor, format As String, ParamArray args() As Object)
            Write(New ColorString(String.Format(format, args), fgcolor, Nothing))
        End Sub

        Public Shared Sub WriteLine(fgcolor As ConsoleColor, format As String, ParamArray args() As Object)
            WriteLine(New ColorString(String.Format(format, args), fgcolor, Nothing))
        End Sub

        Public Shared Sub Write(fgcolor As ConsoleColor, value As String)
            Write(New ColorString(value, fgcolor, Nothing))
        End Sub

        Public Shared Sub WriteLine(fgcolor As ConsoleColor, value As String)
            WriteLine(New ColorString(value, fgcolor, Nothing))
        End Sub

        Public Shared Sub WriteLine(value As ColorString)
            Write(value)
            Console.WriteLine()
        End Sub

        Public Shared Sub WriteLine(value As IEnumerable(Of ColorString))
            Write(value)
            Console.WriteLine()
        End Sub

        Public Shared Sub Write(ByVal cstring As ColorString)
            Using New ColorSetting(cstring.ForegroundColor, cstring.BackgroundColor)
                Console.Write(cstring.String)
            End Using
        End Sub

        Public Shared Sub Write(ByVal csb As IEnumerable(Of ColorString))
            For Each cstring In csb
                Write(cstring)
            Next
        End Sub

    End Class

End Namespace
