Imports System.Runtime.CompilerServices

Namespace Icm.Collections.Generic
    Public Module AaSearch

        <Extension()>
        Function f(ByVal n As INode) As Integer
            Return n.g + n.h
        End Function

        Public Function AaSearch(ByVal root As INode) As INode
            Dim visited As New HashSet(Of INode) From {root}
            Dim evaluated As New HashSet(Of INode)

            root.g = 0

            Do Until visited.Count = 0
                Dim current = visited.MinEntity(Function(n) n.f)

                If current.IsGoal Then
                    Return current
                End If
                visited.Remove(current)
                evaluated.Add(current)
                For Each neighbor In current.Neighbors
                    If evaluated.Contains(neighbor) Then
                        Continue For
                    End If
                    Dim tentative_g = current.g + current.Distance(neighbor)

                    If visited.Contains(neighbor) OrElse tentative_g <= neighbor.g Then
                        neighbor.CameFrom = current
                        neighbor.g = tentative_g
                        If Not visited.Contains(neighbor) Then
                            visited.Add(neighbor)
                        End If

                    End If

                Next
            Loop
            Return Nothing
        End Function

    End Module
End Namespace
