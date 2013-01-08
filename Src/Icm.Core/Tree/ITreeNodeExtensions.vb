Imports System.Runtime.CompilerServices

Namespace Icm

    Public Module ITreeNodeExtensions

        <Extension>
        Public Function [Select](Of T1, T2)(tn As ITreeNode(Of T1), transform As Func(Of T1, T2)) As ITreeNode(Of T2)
            Return New TransformTreeNode(Of T1, T2)(tn, transform)
        End Function

        <Extension>
        Public Iterator Function DepthPreorderTraverse(Of T)(tn As ITreeNode(Of T)) As IEnumerable(Of T)
            Dim childStack As New Stack(Of IEnumerator(Of ITreeNode(Of T)))

            childStack.Push(tn.GetChildren.GetEnumerator)
            Yield tn.Value
            Do Until childStack.Count = 0
                Dim childEnum = childStack.Peek

                If childEnum.MoveNext() Then
                    Dim child = childEnum.Current
                    childStack.Push(child.GetChildren.GetEnumerator)
                    Yield child.Value
                Else
                    childStack.Pop()
                End If
            Loop
        End Function

        <Extension>
        Public Iterator Function DepthPostorderTraverse(Of T)(tn As ITreeNode(Of T)) As IEnumerable(Of T)
            Dim childStack As New Stack(Of IEnumerator(Of ITreeNode(Of T)))
            Dim rootEnum = {tn}.ToList.GetEnumerator
            childStack.Push(rootEnum)
            Do
                Dim childEnum = childStack.Peek

                If childEnum.MoveNext() Then
                    Dim child = childEnum.Current
                    childStack.Push(child.GetChildren.GetEnumerator)
                Else
                    childStack.Pop()
                    Yield childStack.Peek.Current.Value
                End If
            Loop Until childStack.Count = 1
        End Function

        <Extension>
        Public Iterator Function BreadthTraverse(Of T)(tn As ITreeNode(Of T)) As IEnumerable(Of T)
            Dim queue As New Queue(Of ITreeNode(Of T))

            queue.Enqueue(tn)
            While Not queue.Count = 0
                tn = queue.Dequeue()
                Yield tn.Value
                For Each child In tn.GetChildren
                    queue.Enqueue(child)
                Next
            End While
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
        Public Iterator Function DepthPreorderTraverseWithLevel(Of T)(tn As ITreeNode(Of T)) As IEnumerable(Of Tuple(Of T, Integer))
            Dim childStack As New Stack(Of IEnumerator(Of ITreeNode(Of T)))

            childStack.Push(tn.GetChildren.GetEnumerator)
            Yield Tuple.Create(tn.Value, childStack.Count - 1)
            Do Until childStack.Count = 0
                Dim childEnum = childStack.Peek

                If childEnum.MoveNext() Then
                    Dim child = childEnum.Current
                    childStack.Push(child.GetChildren.GetEnumerator)
                    Yield Tuple.Create(child.Value, childStack.Count - 1)
                Else
                    childStack.Pop()
                End If
            Loop

        End Function

        <Extension>
        Public Iterator Function DepthPostorderTraverseWithLevel(Of T)(tn As ITreeNode(Of T)) As IEnumerable(Of Tuple(Of T, Integer))
            Dim childStack As New Stack(Of IEnumerator(Of ITreeNode(Of T)))
            Dim rootEnum = {tn}.ToList.GetEnumerator
            childStack.Push(rootEnum)
            Do
                Dim childEnum = childStack.Peek

                If childEnum.MoveNext() Then
                    Dim child = childEnum.Current
                    childStack.Push(child.GetChildren.GetEnumerator)
                Else
                    childStack.Pop()
                    Yield Tuple.Create(childStack.Peek.Current.Value, childStack.Count - 1)
                End If
            Loop Until childStack.Count = 1
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
            Dim queue As New Queue(Of ITreeNode(Of T))
            Dim levelQueue As New Queue(Of Integer)

            queue.Enqueue(tn)
            levelQueue.Enqueue(0)
            While Not queue.Count = 0
                tn = queue.Dequeue()
                Dim level = levelQueue.Dequeue
                Yield Tuple.Create(tn.Value, level)
                For Each child In tn.GetChildren
                    queue.Enqueue(child)
                    levelQueue.Enqueue(level + 1)
                Next
            End While
        End Function

        Public Function Node(Of T)(v As T, ParamArray children() As TreeNode(Of T)) As TreeNode(Of T)
            Dim result As New TreeNode(Of T)(v)
            result.AddChildren(children)
            Return result
        End Function
    End Module
End Namespace
