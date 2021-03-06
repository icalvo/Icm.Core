Imports Microsoft.VisualBasic
Imports System
Imports System.Text
Imports System.Globalization

Namespace Icm.Compilation

    ''' <summary>
    ''' Compiles a function out of Visual Basic source code.
    ''' </summary>
    ''' <typeparam name="T"></typeparam>
    ''' <remarks></remarks>
    Public Class VBCompiledFunction(Of T)
        Inherits CompiledFunction(Of T)

        Public Sub New()
            Dim providerOptions As New Dictionary(Of String, String)
#If FrameworkNet35 Then
            providerOptions.Add("CompilerVersion", "v3.5")
#End If
#If FrameworkNet40 Then
            providerOptions.Add("CompilerVersion", "v4.0")
#End If
#If FrameworkNet45 Then
            providerOptions.Add("CompilerVersion", "v4.0")
#End If
            CodeProvider = New VBCodeProvider(providerOptions)

            AddNamespace("System", "system.dll")
            AddNamespace("System.Xml", "system.xml.dll")
            AddNamespace("System.Data", "system.data.dll")
            AddNamespace("System.Linq", "system.core.dll")
            AddNamespace("Microsoft.VisualBasic", "system.dll")

            CompilerParameters.CompilerOptions = "/t:library"
            CompilerParameters.GenerateInMemory = True
        End Sub

        Public Overrides Function GeneratedCode() As String
            Dim sb As New StringBuilder

            ' Add Imports
            For Each ns In Namespaces
                sb.AppendLine(String.Format(CultureInfo.InvariantCulture, "Imports {0}", ns))
            Next

            ' Build a little wrapper code, with our passed in code in the middle 
            sb.AppendLine("Namespace dValuate")
            sb.AppendLine(" Class EvalRunTime ")

            sb.AppendLine("  Public Function EvaluateIt( _")

            For Each p In Parameters.Take(Parameters.Count - 1)
                sb.AppendLine(String.Format(CultureInfo.InvariantCulture, "   ByVal {0} As {1}, _", p.Name, p.ArgType))
            Next
            With Parameters.Last
                sb.AppendLine(String.Format(CultureInfo.InvariantCulture, "   ByVal {0} As {1} _", .Name, .ArgType))
            End With
            sb.AppendLine(String.Format(CultureInfo.InvariantCulture, "  ) As {0}", GetType(T).Name))
            sb.AppendLine(String.Format(CultureInfo.InvariantCulture, "    Return {0}", Code))
            sb.AppendLine("  End Function")
            sb.AppendLine(" End Class ")
            sb.AppendLine("End Namespace")

            Return sb.ToString
        End Function

    End Class

End Namespace