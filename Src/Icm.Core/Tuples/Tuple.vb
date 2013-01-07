Namespace Icm

    Public Class Tuple

        Public Shared Function Create(Of T1, T2)(v1 As T1, v2 As T2) As Tuple(Of T1, T2)
            Return New Tuple(Of T1, T2)(v1, v2)
        End Function

    End Class


    Public Class Tuple(Of T1, T2)

        Public ReadOnly Item1 As T1
        Public ReadOnly Item2 As T2

        Public Sub New(v1 As T1, v2 As T2)
            Item1 = v1
            Item2 = v2
        End Sub
    End Class

End Namespace
