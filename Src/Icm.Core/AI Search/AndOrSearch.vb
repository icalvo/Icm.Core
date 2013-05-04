Public Class AndOrSearch


    Public Sub Search(Of T)(root As AndOrNode(Of T))
        Do Until root.Resolved OrElse root.H = Integer.MaxValue
            Dim n As AndOrNode(Of T) = root.ChooseMarkedNonSolvedLeaf

            n.Expand()

            Dim SetS As New List(Of AndOrNode(Of T)) From {n}

            Do Until SetS.Count = 0
                Dim s = SetS.First(Function(node) node.Descendants.All(Function(desc) Not SetS.Contains(desc)))
                If s.Links.Count > 0 Then
                    Dim amin = s.Links.First(Function(lnk) lnk.Cost = s.Links.Min(Function(lnk2) lnk2.Cost))
                    If s.H <> amin.Cost Then
                        SetS.AddRange(s.Predecessors)
                    End If
                    s.MarkedLink = amin
                    's.H = amin.Cost
                Else
                End If
            Loop
        Loop
    End Sub
End Class

Public Interface ISubProblem(Of T)

    Property Node As AndOrNode(Of T)
    Property Cost As Integer

End Interface
