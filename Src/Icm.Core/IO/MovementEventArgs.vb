
Namespace Icm.IO
    Public Class MovementEventArgs
        Inherits EventArgs
        Protected origen_ As String
        Protected destino_ As String
        Public Sub New(ByVal o As String, ByVal d As String)
            origen_ = o
            destino_ = d
        End Sub
        ReadOnly Property Origen() As String
            Get
                Return origen_
            End Get
        End Property
        ReadOnly Property Destino() As String
            Get
                Return destino_
            End Get
        End Property
    End Class
End Namespace
