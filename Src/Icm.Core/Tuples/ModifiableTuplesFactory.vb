Namespace Icm
    Public Module ModifiableTuplesFactory

        Public Function NewPair(Of T1, T2)(ByVal o1 As T1, ByVal o2 As T2) As Pair(Of T1, T2)
            Return New Pair(Of T1, T2)(o1, o2)
        End Function

        Public Function NewTuple3(Of T1, T2, T3)(ByVal o1 As T1, ByVal o2 As T2, ByVal o3 As T3) As Tuple3(Of T1, T2, T3)
            Return New Tuple3(Of T1, T2, T3)(o1, o2, o3)
        End Function

        Public Function NewTuple4(Of T1, T2, T3, T4)(ByVal o1 As T1, ByVal o2 As T2, ByVal o3 As T3, ByVal o4 As T4) As Tuple4(Of T1, T2, T3, T4)
            Return New Tuple4(Of T1, T2, T3, T4)(o1, o2, o3, o4)
        End Function


    End Module

End Namespace
