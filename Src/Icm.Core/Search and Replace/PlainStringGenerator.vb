
Namespace Icm.Text

    ''' <summary>
    ''' Returns always the string passed to the constructor.
    ''' </summary>
    ''' <remarks></remarks>
    Public Class PlainStringGenerator
        Implements IEnumerator(Of String)
        Implements IEnumerable(Of String)

        Private ReadOnly s_ As String

        Public Sub New(ByVal s As String)
            s_ = s
        End Sub

        Protected Sub New()
        End Sub

        Public ReadOnly Property Current As String Implements IEnumerator(Of String).Current
            Get
                Return s_
            End Get
        End Property

        Public ReadOnly Property Current1 As Object Implements IEnumerator.Current
            Get
                Return Current
            End Get
        End Property

        Public Function MoveNext() As Boolean Implements IEnumerator.MoveNext
            Return False
        End Function

        Public Sub Reset() Implements IEnumerator.Reset
        End Sub


        Public Sub Dispose() Implements IDisposable.Dispose
        End Sub

        Public Function GetEnumerator() As IEnumerator(Of String) Implements IEnumerable(Of String).GetEnumerator
            Return Me
        End Function

        Public Function GetEnumerator1() As IEnumerator Implements IEnumerable.GetEnumerator
            Return Me
        End Function
    End Class
End Namespace
