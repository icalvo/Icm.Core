Imports Microsoft.CSharp
Imports System.Text
Imports System.Globalization

Namespace Icm.Compilation

    ''' <summary>
    ''' Compiles a function out of C# source code.
    ''' </summary>
    ''' <typeparam name="T"></typeparam>
    ''' <remarks></remarks>
    Public Class CSharpCompiledFunction(Of T)
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
            CodeProvider = New CSharpCodeProvider(providerOptions)

            AddNamespace("System", "system.dll")
            AddNamespace("System.Xml", "system.xml.dll")
            AddNamespace("System.Data", "system.data.dll")
            AddNamespace("System.Linq", "system.core.dll")
            AddNamespace("Microsoft.CSharp", "system.dll")

            CompilerParameters.CompilerOptions = "/t:library"
            CompilerParameters.GenerateInMemory = True
        End Sub

        Public Overrides Function GeneratedCode() As String
            Dim sb As New StringBuilder

            ' Imports
            For Each ns In Namespaces
                sb.AppendLine(String.Format(CultureInfo.InvariantCulture, "import {0}", ns))
            Next

            ' Build a little wrapper code, with our passed in code in the middle 
            sb.AppendLine("namespace dValuate {")
            sb.AppendLine(" class EvalRunTime {")

            sb.AppendLine(String.Format(CultureInfo.InvariantCulture, "  {0} EvaluateIt(", GetType(T).Name))

            For Each p In Parameters.Take(Parameters.Count - 1)
                sb.AppendLine(String.Format(CultureInfo.InvariantCulture, "   {1} {0},", p.Name, p.ArgType))
            Next
            With Parameters.Last
                sb.AppendLine(String.Format(CultureInfo.InvariantCulture, "   {1} {0}", .Name, .ArgType))
            End With
            sb.AppendLine("  )")
            sb.AppendLine(String.Format(CultureInfo.InvariantCulture, "    return {0};", Code))
            sb.AppendLine("  }")
            sb.AppendLine(" }")
            sb.AppendLine("}")

            Return sb.ToString
        End Function

    End Class

End Namespace