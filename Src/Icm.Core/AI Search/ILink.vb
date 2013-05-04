
Public Class ILink(Of T)

    Property Subproblems As IEnumerable(Of ISubProblem(Of T))

    Function Cost() As Integer
        Return Subproblems.Sum(Function(subp) subp.Cost + subp.Node.H)
    End Function

End Class
