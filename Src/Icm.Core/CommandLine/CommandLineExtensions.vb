Imports System.Runtime.CompilerServices
Imports res = My.Resources.CommandLineTools
Imports Icm.Collections
Imports Icm.ColorConsole

Namespace Icm.CommandLineTools

    Public Module CommandLineExtensions

        ''' <summary>
        '''  Prints the set of instructions for a CommandLine.
        ''' </summary>
        ''' <remarks></remarks>
        <Extension>
        Public Sub Instructions(cmdline As CommandLine)
            Dim o As NamedParameter

            Console.Write("{0}: ", res.S_HELP_USAGE, System.Diagnostics.Process.GetCurrentProcess.ProcessName)

            Using New ColorSetting(ConsoleColor.Cyan)
                Console.Write(System.Diagnostics.Process.GetCurrentProcess.ProcessName)

                For Each param In cmdline.NamedParameters
                    If param.IsRequired Then
                        Console.Write(" --{0}", param.LongName)
                    Else
                        Console.Write(" [--{0}", param.LongName)
                    End If
                    For Each subarg In param.SubArguments
                        If subarg.IsRequired Then
                            Console.Write(" " & subarg.Name)
                        Else
                            Console.Write(" [{0}]", subarg.Name)
                        End If
                    Next
                    If Not param.IsRequired Then
                        Console.Write("]")
                    End If

                Next
            End Using
            Using New ColorSetting(ConsoleColor.Green)

                With cmdline.MainParameters
                    For i = 1 To .Minimum
                        Console.Write(" {0}", .GetArgumentName(i).Name)
                    Next
                    If .Maximum.HasValue Then
                        For i = .Minimum + 1 To .Maximum.V
                            Console.Write(" [{0}]", .GetArgumentName(i).Name)
                        Next
                    Else
                        Console.Write(" {{{0}}}", .UnboundArgumentsName.Name)
                    End If
                End With
            End Using
            Using New ColorSetting(ConsoleColor.White)
                Console.WriteLine()
                Console.WriteLine()
                Console.Write("{0}:", res.S_OPTIONS)
                Console.WriteLine()

                For Each o In cmdline.NamedParameters
                    Dim subargs As New List(Of String)

                    For Each s As SubArgument In o.SubArguments
                        If s.IsRequired Then
                            subargs.Add(s.Name)
                        ElseIf s.IsOptional Then
                            subargs.Add(String.Format("[{0}]", s.Name))
                        ElseIf s.IsList Then
                            subargs.Add(String.Format("{{{0}}}", s.Name))
                        End If
                    Next
                    Using New ColorSetting(ConsoleColor.Cyan)
                        Console.WriteLine("  -{0} {1}", o.ShortName, subargs.JoinStr(" "))
                        Console.WriteLine("  --{0} {1}", o.LongName, subargs.JoinStr(" "))
                    End Using
                    Console.WriteLine("      {0}{1}", If(o.IsRequired, res.S_HELP_REQUIRED, ""), o.Description)
                Next

                Console.WriteLine()
                With cmdline.MainParameters
                    For Each key In .ParamDictionary.Keys
                        Using New ColorSetting(ConsoleColor.Green)
                            Console.WriteLine("  {0}", key)
                        End Using
                        Console.WriteLine("      {0}", .ParamDictionary(key).Description)
                    Next
                End With
                Console.WriteLine()
                Console.WriteLine(res.S_SUBARGUMENTS_SEPARATED)
            End Using

        End Sub

        ''' <summary>
        '''  Prints the set of values for a CommandLine.
        ''' </summary>
        ''' <remarks></remarks>
        <Extension>
        Public Sub Print(cmdline As CommandLine)
            Using New ColorSetting(ConsoleColor.Cyan)
                For Each param In cmdline.NamedParameters
                    If cmdline.IsPresent(param.ShortName) Then
                        ColorConsole.ColorConsole.Write(ConsoleColor.Cyan, "{0}=", param.LongName)
                        ColorConsole.ColorConsole.WriteLine(ConsoleColor.White, "({0})", cmdline.GetValues(param.ShortName).JoinStr(
                                "", """", ",", """", ""))
                    Else
                        ColorConsole.ColorConsole.Write(ConsoleColor.Cyan, "{0}=", param.LongName)
                        ColorConsole.ColorConsole.WriteLine(ConsoleColor.White, "UNDEFINED")
                    End If
                Next
            End Using
            Using New ColorSetting(ConsoleColor.Green)

                Console.WriteLine(cmdline.MainValues.JoinStr(
                            "(", """", ",", """", ")"))
            End Using

            Console.WriteLine()

        End Sub

        <Extension>
        Public Function GetInteger(cmdline As CommandLine, key As String) As Integer?
            If cmdline.IsPresent(key) Then
                Return CInt(cmdline.GetValue(key))
            Else
                Return Nothing
            End If
        End Function
    End Module

End Namespace
