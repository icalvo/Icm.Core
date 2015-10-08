Imports System.IO

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

        Private ReadOnly textWriters_ As New List(Of TextWriter)()

        Protected ReadOnly Property TextWriters() As IList(Of TextWriter)
            Get
                Return textWriters_
            End Get
        End Property

        Public Sub New(ByVal ParamArray tws() As TextWriter)
            MyBase.New(CultureInfo.CurrentCulture)
            Add(tws)
        End Sub

        Public Sub Add(ByVal ParamArray tws() As TextWriter)
            For Each tw In tws
                If tw Is Nothing Then
                    Throw New ArgumentNullException("tws")
                End If
                TextWriters.Add(tw)
            Next
        End Sub

        Public Overloads Overrides Sub Write(ByVal value As Char)
            For Each tw As TextWriter In TextWriters
                tw.Write(value)
            Next
        End Sub

        Public Overloads Overrides Sub Write(ByVal value As String)
            For Each tw As TextWriter In TextWriters
                tw.Write(value)
            Next
        End Sub

        Public Overrides Sub Flush()
            For Each tw As TextWriter In TextWriters
                tw.Flush()
            Next
        End Sub

        Public Overrides Sub Close()
            For Each tw As TextWriter In TextWriters
                tw.Close()
            Next
        End Sub

        Public Overloads Overrides Sub WriteLine(ByVal value As String)
            For Each tw As TextWriter In TextWriters
                tw.WriteLine(value)
            Next
        End Sub

        Public Overrides ReadOnly Property Encoding() As System.Text.Encoding
            Get
                Throw New InvalidOperationException("CompositeWriter does not have a single encoding because it is composed of several child TextWriters.")
            End Get
        End Property
    End Class

End Namespace