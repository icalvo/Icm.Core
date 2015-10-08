Namespace Icm

    Public Class Vector2(Of T)
        Inherits Tuple(Of T, T)

        Public Sub New(ByVal item1 As T, ByVal item2 As T)
            MyBase.New(item1, item2)

        End Sub

    End Class

End Namespace
