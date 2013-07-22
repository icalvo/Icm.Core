Imports System.Runtime.CompilerServices

Namespace Icm.Tree

    ''' <summary>
    ''' Tree traversals.
    ''' </summary>
    ''' <remarks>
    ''' 
    ''' </remarks>
    Public Module ITreeElementExtensions


        <Extension>
        Public Iterator Function DepthPreorderTraverse(Of T)(tn As ITreeElement(Of T)) As IEnumerable(Of T)
            Dim childStack As New Stack(Of IEnumerator(Of ITreeElement(Of T)))

            childStack.Push(tn.GetChildElements.GetEnumerator)
            Yield tn.Value
            Do Until childStack.Count = 0
                Dim childEnum = childStack.Peek

                If childEnum.MoveNext() Then
                    Dim child = childEnum.Current
                    childStack.Push(child.GetChildElements.GetEnumerator)
                    Yield child.Value
                Else
                    childStack.Pop()
                End If
            Loop
        End Function

        <Extension>
        Public Iterator Function DepthPostorderTraverse(Of T)(tn As ITreeElement(Of T)) As IEnumerable(Of T)
            Dim childStack As New Stack(Of IEnumerator(Of ITreeElement(Of T)))
            Dim rootEnum = {tn}.ToList.GetEnumerator
            childStack.Push(rootEnum)
            Do
                Dim childEnum = childStack.Peek

                If childEnum.MoveNext() Then
                    Dim child = childEnum.Current
                    childStack.Push(child.GetChildElements.GetEnumerator)
                Else
                    childStack.Pop()
                    Yield childStack.Peek.Current.Value
                End If
            Loop Until childStack.Count = 1
        End Function

        <Extension>
        Public Iterator Function BreadthTraverse(Of T)(tn As ITreeElement(Of T)) As IEnumerable(Of T)
            Dim queue As New Queue(Of ITreeElement(Of T))

            queue.Enqueue(tn)
            While Not queue.Count = 0
                tn = queue.Dequeue()
                Yield tn.Value
                For Each child In tn.GetChildElements
                    queue.Enqueue(child)
                Next
            End While
        End Function


        <Extension>
        Public Iterator Function DepthPreorderTraverseWithLevel(Of T)(tn As ITreeElement(Of T)) As IEnumerable(Of Tuple(Of T, Integer))
            Dim childStack As New Stack(Of IEnumerator(Of ITreeElement(Of T)))

            childStack.Push(tn.GetChildElements.GetEnumerator)
            Yield Tuple.Create(tn.Value, childStack.Count - 1)
            Do Until childStack.Count = 0
                Dim childEnum = childStack.Peek

                If childEnum.MoveNext() Then
                    Dim child = childEnum.Current
                    childStack.Push(child.GetChildElements.GetEnumerator)
                    Yield Tuple.Create(child.Value, childStack.Count - 1)
                Else
                    childStack.Pop()
                End If
            Loop

        End Function

        <Extension>
        Public Iterator Function DepthPostorderTraverseWithLevel(Of T)(tn As ITreeElement(Of T)) As IEnumerable(Of Tuple(Of T, Integer))
            Dim childStack As New Stack(Of IEnumerator(Of ITreeElement(Of T)))
            Dim rootEnum = {tn}.ToList.GetEnumerator
            childStack.Push(rootEnum)
            Do
                Dim childEnum = childStack.Peek

                If childEnum.MoveNext() Then
                    Dim child = childEnum.Current
                    childStack.Push(child.GetChildElements.GetEnumerator)
                Else
                    childStack.Pop()
                    Yield Tuple.Create(childStack.Peek.Current.Value, childStack.Count - 1)
                End If
            Loop Until childStack.Count = 1
        End Function

        <Extension>
        Private Iterator Function DepthPostorderTraverseWithLevel(Of T)(tn As ITreeElement(Of T), level As Integer) As IEnumerable(Of Tuple(Of T, Integer))
            For Each child In tn.GetChildElements
                For Each result In child.DepthPostorderTraverseWithLevel(level + 1)
                    Yield result
                Next
            Next
            Yield Tuple.Create(tn.Value, level)
        End Function


        <Extension>
        Public Iterator Function BreadthTraverseWithLevel(Of T)(tn As ITreeElement(Of T)) As IEnumerable(Of Tuple(Of T, Integer))
            Dim queue As New Queue(Of ITreeElement(Of T))
            Dim levelQueue As New Queue(Of Integer)

            queue.Enqueue(tn)
            levelQueue.Enqueue(0)
            While Not queue.Count = 0
                tn = queue.Dequeue()
                Dim level = levelQueue.Dequeue
                Yield Tuple.Create(tn.Value, level)
                For Each child In tn.GetChildElements
                    queue.Enqueue(child)
                    levelQueue.Enqueue(level + 1)
                Next
            End While
        End Function

    End Module
End Namespace
