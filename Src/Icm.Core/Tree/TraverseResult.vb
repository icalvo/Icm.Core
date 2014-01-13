Namespace Icm.Tree

    Public Class TraverseResult(Of T)

        Public Sub New(result As T, level As Integer)
            Me.Result = result
            Me.Level = level
        End Sub

        Property Result As T

        Property Level As Integer

    End Class

    Public Class TraverseResult

        Public Shared Function Create(Of T)(result As T, level As Integer) As TraverseResult(Of T)
            Return New TraverseResult(Of T)(result, level)
        End Function

    End Class

End Namespace
