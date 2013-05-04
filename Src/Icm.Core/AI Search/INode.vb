
Namespace Icm.Collections.Generic

    Public Interface INode
        Property Value As String
        Property CameFrom As INode
        Property g As Integer
        Function h() As Integer

        Property Neighbors As IEnumerable(Of INode)
        Function IsGoal() As Boolean
        Function Distance(ByVal other As INode) As Integer
    End Interface
End Namespace