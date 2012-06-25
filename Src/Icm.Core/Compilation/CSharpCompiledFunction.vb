Imports Microsoft.CSharp
Imports System
Imports System.Text

Namespace Icm.Compilation

    ''' <summary>
    ''' Compiles a function out of C# source code.
    ''' </summary>
    ''' <typeparam name="T"></typeparam>
    ''' <remarks></remarks>
    Public Class CSharpCompiledFunction(Of T)
        Inherits CompiledFunction(Of T)

        Private ReadOnly Namespaces As New List(Of String)()


        Public Sub New()
            Dim providerOptions As New Dictionary(Of String, String)
            providerOptions.Add("CompilerVersion", "v4.0")
            oCodeProvider = New CSharpCodeProvider(providerOptions)

            AddNamespace("System", "system.dll")
            AddNamespace("System.Xml", "system.xml.dll")
            AddNamespace("System.Data", "system.data.dll")
            AddNamespace("System.Linq", "system.core.dll")
            AddNamespace("Microsoft.CSharp", "system.dll")

            oCParams.CompilerOptions = "/t:library"
            oCParams.GenerateInMemory = True
        End Sub

        Public Sub AddNamespace(ByVal nspace As String, ByVal assm As String)
            If Not oCParams.ReferencedAssemblies.Contains(assm) Then
                oCParams.ReferencedAssemblies.Add(assm)
            End If
            Namespaces.Add(nspace)

        End Sub

        Public Overrides Function GeneratedCode() As String
            ' Setup the Compiler Parameters  
            ' Add any referenced assemblies

            ' Generate the Code Framework
            Dim sb As New StringBuilder

            For Each ns In Namespaces
                sb.AppendLine(String.Format("import {0}", ns))
            Next

            ' Build a little wrapper code, with our passed in code in the middle 

            sb.AppendLine("namespace dValuate {")
            sb.AppendLine(" class EvalRunTime {")

            sb.AppendLine(String.Format("  {0} EvaluateIt(", GetType(T).Name))

            For Each p In Parameters.Take(Parameters.Count - 1)
                sb.AppendLine(String.Format("   {1} {0},", p.Name, p.ArgType))
            Next
            With Parameters.Last
                sb.AppendLine(String.Format("   {1} {0}", .Name, .ArgType))
            End With
            sb.AppendLine("  )")
            sb.AppendLine(String.Format("    return {0};", Code))
            sb.AppendLine("  }")
            sb.AppendLine(" }")
            sb.AppendLine("}")

            Return sb.ToString
        End Function

    End Class

End Namespace