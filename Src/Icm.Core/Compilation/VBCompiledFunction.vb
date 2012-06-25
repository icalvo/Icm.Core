Imports Microsoft.VisualBasic
Imports System
Imports System.Text

Namespace Icm.Compilation

    ''' <summary>
    ''' Compiles a function out of Visual Basic source code.
    ''' </summary>
    ''' <typeparam name="T"></typeparam>
    ''' <remarks></remarks>
    Public Class VBCompiledFunction(Of T)
        Inherits CompiledFunction(Of T)

        Private ReadOnly Namespaces As New List(Of String)()


        Public Sub New()
            Dim providerOptions As New Dictionary(Of String, String)
            providerOptions.Add("CompilerVersion", "v4.0")
            oCodeProvider = New VBCodeProvider(providerOptions)

            AddNamespace("System", "system.dll")
            AddNamespace("System.Xml", "system.xml.dll")
            AddNamespace("System.Data", "system.data.dll")
            AddNamespace("System.Linq", "system.core.dll")
            AddNamespace("Microsoft.VisualBasic", "system.dll")

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
                sb.AppendLine(String.Format("Imports {0}", ns))
            Next

            ' Build a little wrapper code, with our passed in code in the middle 

            sb.AppendLine("Namespace dValuate")
            sb.AppendLine(" Class EvalRunTime ")

            sb.AppendLine("  Public Function EvaluateIt( _")

            For Each p In Parameters.Take(Parameters.Count - 1)
                sb.AppendLine(String.Format("   ByVal {0} As {1}, _", p.Name, p.ArgType))
            Next
            With Parameters.Last
                sb.AppendLine(String.Format("   ByVal {0} As {1} _", .Name, .ArgType))
            End With
            sb.AppendLine(String.Format("  ) As {0}", GetType(T).Name))
            sb.AppendLine(String.Format("    Return {0}", Code))
            sb.AppendLine("  End Function")
            sb.AppendLine(" End Class ")
            sb.AppendLine("End Namespace")

            Return sb.ToString
        End Function

    End Class

End Namespace