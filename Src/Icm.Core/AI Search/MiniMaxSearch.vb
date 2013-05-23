Imports Icm.Collections.Generic


Public Module MiniMaxSearch

    ''' <summary>
    ''' Returns the most promising child for a MAX node
    ''' </summary>
    ''' <param name="root">MAX node</param>
    ''' <param name="maxDepth">Maximum depth</param>
    ''' <returns>Most promising child</returns>
    ''' <remarks>Uses alpha-beta pruning</remarks>
    Public Function AlphaBeta(root As INode, maxDepth As Integer) As INode
        Dim alpha As Integer = Integer.MinValue
        Dim beta As Integer = Integer.MaxValue

        If 0 >= maxDepth Then
            Return Nothing
        End If
        Dim result As INode = Nothing
        For Each child In root.Neighbors
            Dim alphaChild = AlphaBetaMax(child, 1, maxDepth, alpha, beta)
            If alphaChild > alpha Then
                alpha = alphaChild
                If alpha >= beta Then
                    Return child
                End If
                result = child
            End If
        Next
        Return result
    End Function


    Private Function AlphaBetaMax(root As INode, depth As Integer, maxDepth As Integer, alpha As Integer, beta As Integer) As Integer
        If depth >= maxDepth Then
            Return root.f
        End If
        If root.Neighbors.Count = 0 Then
            Return root.f
        End If
        For Each child In root.Neighbors
            alpha = Math.Max(alpha, AlphaBetaMin(child, depth + 1, maxDepth, alpha, beta))
            If alpha >= beta Then
                Return alpha
            End If
        Next
        Return alpha
    End Function

    Private Function AlphaBetaMin(root As INode, depth As Integer, maxDepth As Integer, alpha As Integer, beta As Integer) As Integer
        If depth >= maxDepth Then
            Return root.f
        End If
        If root.Neighbors.Count = 0 Then
            Return root.f
        End If
        For Each child In root.Neighbors
            beta = Math.Min(beta, AlphaBetaMax(child, depth + 1, maxDepth, alpha, beta))
            If alpha >= beta Then
                Return beta
            End If
        Next
        Return beta
    End Function

End Module
