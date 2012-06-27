Namespace Icm.Text

    ''' <summary>
    ''' Returns the natural numbers, starting with 1
    ''' </summary>
    ''' <remarks></remarks>
    Public Class AutoNumberGenerator
        Implements IEnumerator(Of String)


        Private counter_ As Integer
        Public Sub New()
            counter_ = 0
        End Sub


        Public ReadOnly Property Current As String Implements IEnumerator(Of String).Current
            Get
                Return CStr(counter_)
            End Get
        End Property

        Public ReadOnly Property Current1 As Object Implements IEnumerator.Current
            Get
                Return Current
            End Get
        End Property

        Public Function MoveNext() As Boolean Implements IEnumerator.MoveNext
            counter_ = counter_ + 1
        End Function

        Public Sub Reset() Implements IEnumerator.Reset
            Throw New NotImplementedException
        End Sub


        Public Sub Dispose() Implements IDisposable.Dispose
        End Sub

    End Class

End Namespace
