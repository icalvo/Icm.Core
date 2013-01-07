Imports System.Runtime.CompilerServices

Namespace Icm

    Public Module ITreeNodeExtensions

        <Extension>
        Public Function [Select](Of T1, T2)(tn As ITreeNode(Of T1), transform As Func(Of T1, T2)) As ITreeNode(Of T2)
            Return New TransformTreeNode(Of T1, T2)(tn, transform)
        End Function

        <Extension>
        Public Iterator Function DepthPreorderTraverse(Of T)(tn As ITreeNode(Of T)) As IEnumerable(Of T)
            Yield tn.Value
            For Each child In tn.GetChildren
                For Each result In DepthPreorderTraverse(child)
                    Yield result
                Next
            Next
        End Function

        ''' <summary>
        ''' Yields the ancestors of a node, not including the node
        ''' </summary>
        ''' <typeparam name="T"></typeparam>
        ''' <param name="tn"></param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        <Extension>
        Public Iterator Function ProperAncestors(Of T)(tn As ITreeNode(Of T)) As IEnumerable(Of T)
            Dim current = tn.GetParent
            Do Until current Is Nothing
                Yield current.Value
                current = current.GetParent
            Loop
        End Function

        ''' <summary>
        ''' Yields the ancestors of a node, including the node
        ''' </summary>
        ''' <typeparam name="T"></typeparam>
        ''' <param name="tn"></param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        <Extension>
        Public Iterator Function Ancestors(Of T)(tn As ITreeNode(Of T)) As IEnumerable(Of T)
            Dim current = tn
            Do
                Yield current.Value
                current = current.GetParent
            Loop Until current Is Nothing
        End Function


        <Extension>
        Public Iterator Function DepthPostorderTraverse(Of T)(tn As ITreeNode(Of T)) As IEnumerable(Of T)
            For Each child In tn.GetChildren
                For Each result In DepthPostorderTraverse(child)
                    Yield result
                Next
            Next
            Yield tn.Value
        End Function

        <Extension>
        Public Iterator Function BreadthTraverse(Of T)(tn As ITreeNode(Of T)) As IEnumerable(Of T)
            Dim queue As New Queue(Of ITreeNode(Of T))
            queue.Enqueue(tn)
            For Each result In BreadthTraverse(queue)
                Yield result
            Next
        End Function

        Private Iterator Function BreadthTraverse(Of T)(queue As Queue(Of ITreeNode(Of T))) As IEnumerable(Of T)
            If queue.Count = 0 Then
                Exit Function
            End If
            Dim tn = queue.Dequeue
            Yield tn.Value

            For Each child In tn.GetChildren
                queue.Enqueue(child)
            Next
            For Each result In BreadthTraverse(queue)
                Yield result
            Next
        End Function

        <Extension>
        Public Iterator Function DepthPreorderTraverseWithLevel(Of T)(tn As ITreeNode(Of T)) As IEnumerable(Of Tuple(Of T, Integer))
            For Each element In tn.DepthPreorderTraverseWithLevel(0)
                Yield element
            Next
        End Function

        <Extension>
        Private Iterator Function DepthPreorderTraverseWithLevel(Of T)(tn As ITreeNode(Of T), level As Integer) As IEnumerable(Of Tuple(Of T, Integer))
            Yield Tuple.Create(tn.Value, level)
            For Each child In tn.GetChildren
                For Each result In child.DepthPreorderTraverseWithLevel(level + 1)
                    Yield result
                Next
            Next
        End Function

        <Extension>
        Public Iterator Function DepthPostorderTraverseWithLevel(Of T)(tn As ITreeNode(Of T)) As IEnumerable(Of Tuple(Of T, Integer))
            For Each element In tn.DepthPostorderTraverseWithLevel(0)
                Yield element
            Next
        End Function

        <Extension>
        Private Iterator Function DepthPostorderTraverseWithLevel(Of T)(tn As ITreeNode(Of T), level As Integer) As IEnumerable(Of Tuple(Of T, Integer))
            For Each child In tn.GetChildren
                For Each result In child.DepthPostorderTraverseWithLevel(level + 1)
                    Yield result
                Next
            Next
            Yield Tuple.Create(tn.Value, level)
        End Function


        <Extension>
        Public Iterator Function BreadthTraverseWithLevel(Of T)(tn As ITreeNode(Of T)) As IEnumerable(Of Tuple(Of T, Integer))
            Dim queue As New Queue(Of Tuple(Of ITreeNode(Of T), Integer))
            queue.Enqueue(Tuple.Create(tn, 0))
            For Each result In BreadthTraverseWithLevel(queue)
                Yield result
            Next
        End Function

        Private Iterator Function BreadthTraverseWithLevel(Of T)(queue As Queue(Of Tuple(Of ITreeNode(Of T), Integer))) As IEnumerable(Of Tuple(Of T, Integer))
            If queue.Count = 0 Then
                Exit Function
            End If
            Dim tn = queue.Dequeue
            Yield Tuple.Create(tn.Item1.Value, tn.Item2)

            For Each child In tn.Item1.GetChildren
                queue.Enqueue(tuple.Create(child, tn.Item2 + 1))
            Next
            For Each result In BreadthTraverseWithLevel(queue)
                Yield result
            Next
        End Function


        Public Function Node(Of T)(v As T, ParamArray children() As TreeNode(Of T)) As TreeNode(Of T)
            Dim result As New TreeNode(Of T)(v)
            result.AddChildren(children)
            Return result
        End Function
    End Module
End Namespace
