Namespace Icm

    Public Class ErrorEventArgs(Of T)
        Inherits EventArgs(Of T)

        Private exception_ As Exception
        Public Property Exception() As Exception
            Get
                Return exception_
            End Get
            Set(ByVal value As Exception)
                exception_ = value
            End Set
        End Property

        Public Sub New(ByVal _data As T, ByVal _exception As Exception)
            MyBase.New(_data)
            exception_ = _exception
        End Sub

    End Class

End Namespace
