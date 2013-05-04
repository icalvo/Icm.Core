
Public Class AndOrNode(Of T)
    Private _h As Func(Of AndOrNode(Of T), Integer)

    Private Property IsExpanded As Boolean

    Sub Expand()

    End Sub

    Function H() As Integer
        If IsTerminal Then
            Return 0
        ElseIf IsExpanded Then
            If Links.Count = 0 Then
                Return Integer.MaxValue
            Else
                '' TODO
                Throw New NotImplementedException
            End If
        Else
            Return _h(Me)
        End If
    End Function

    Function Resolved() As Boolean
        Return IsTerminal OrElse MarkedLink.Subproblems.All(Function(subp) subp.Node.Resolved)
    End Function

    Property MarkedLink As ILink(Of T)

    Function Descendants() As IEnumerable(Of AndOrNode(Of T))

    End Function

    Function ChooseMarkedNonSolvedLeaf() As AndOrNode(Of T)

    End Function

    Property Links() As IEnumerable(Of ILink(Of T))

    Property IsTerminal() As Boolean

    Function Predecessors() As IEnumerable(Of AndOrNode(Of T))
        Throw New NotImplementedException
    End Function

End Class
