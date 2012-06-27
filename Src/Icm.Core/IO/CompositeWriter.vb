﻿Imports System.IO

Namespace Icm.IO

    ''' <summary>
    ''' TextWriter composed by other TextWriters. Every write
    ''' is transferred to all the TextWriters.
    ''' </summary>
    ''' <remarks>
    ''' </remarks>
    ''' <history>
    ''' 	[icalvo]	03/12/2005	Created
    ''' </history>
    Public Class CompositeWriter
        Inherits TextWriter

        Protected textWriters As New Generic.List(Of TextWriter)

        Public Sub New(ByVal ParamArray tws() As TextWriter)
            Add(tws)
        End Sub

        Public Shadows Sub Add(ByVal ParamArray tws() As TextWriter)
            For Each tw In tws
                If tw Is Nothing Then
                    Throw New ArgumentNullException("No se pueden pasar textwriter nulos")
                End If
                textWriters.Add(tw)
            Next
        End Sub

        Public Overloads Overrides Sub Write(ByVal c As Char)
            For Each tw As TextWriter In textWriters
                tw.Write(c)
            Next
        End Sub

        Public Overloads Overrides Sub Write(ByVal s As String)
            For Each tw As TextWriter In textWriters
                tw.Write(s)
            Next
        End Sub

        Public Overrides Sub Flush()
            For Each tw As TextWriter In textWriters
                tw.Flush()
            Next
        End Sub

        Public Overrides Sub Close()
            For Each tw As TextWriter In textWriters
                tw.Close()
            Next
        End Sub

        Public Overloads Overrides Sub WriteLine(ByVal s As String)
            For Each tw As TextWriter In textWriters
                tw.WriteLine(s)
            Next
        End Sub

        Public Overrides ReadOnly Property Encoding() As System.Text.Encoding
            Get
                Return System.Text.Encoding.Default
            End Get
        End Property
    End Class

End Namespace