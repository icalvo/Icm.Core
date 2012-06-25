Namespace Icm

    Public Class EventArgs(Of T)
        Inherits EventArgs

        Private ReadOnly data_ As T
        ReadOnly Property Data() As T
            Get
                Return data_
            End Get
        End Property

        Public Sub New(ByVal _data As T)
            data_ = _data
        End Sub

    End Class

End Namespace
