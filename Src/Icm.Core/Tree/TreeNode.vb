Namespace Icm

    Public Interface ITreeNode(Of T)
        Property Value() As T
        Function GetParent() As ITreeNode(Of T)
        Function GetChildren() As IEnumerable(Of ITreeNode(Of T))
    End Interface

    Public Module ITreeNodeExtensions

        Public Iterator Function DepthPreorderTraverse(Of T)(tn As ITreeNode(Of T)) As IEnumerable(Of T)
            Yield tn.Value
            For Each child In tn.GetChildren
                For Each result In DepthPreorderTraverse(child)
                    Yield result
                Next
            Next
        End Function

        Public Iterator Function DepthPostorderTraverse(Of T)(tn As ITreeNode(Of T)) As IEnumerable(Of T)
            For Each child In tn.GetChildren
                For Each result In DepthPreorderTraverse(child)
                    Yield result
                Next
            Next
            Yield tn.Value
        End Function

        Public Iterator Function BreadthTraverse(Of T)(tn As ITreeNode(Of T)) As IEnumerable(Of T)
            Dim queue As New Queue(Of ITreeNode(Of T))
            queue.Enqueue(tn)
            For Each result In BreadthTraverse(queue)
                Yield result
            Next
        End Function

        Public Iterator Function BreadthTraverse(Of T)(queue As Queue(Of ITreeNode(Of T))) As IEnumerable(Of T)
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

    End Module

    Public Class TreeNode(Of T)
        Implements ITreeNode(Of T)

        Private parent_ As TreeNode(Of T)
        Private ReadOnly children_ As New List(Of TreeNode(Of T))()
        Private level_ As Integer

        Public Sub New(ByVal v As T)
            Value = v
        End Sub

        Property Value() As T Implements ITreeNode(Of T).Value

        ReadOnly Property Children() As IEnumerable(Of TreeNode(Of T))
            Get
                Return children_
            End Get
        End Property

        Property Parent() As TreeNode(Of T)
            Get
                Return parent_
            End Get
            Set(ByVal value As TreeNode(Of T))
                If parent_ IsNot Nothing Then
                    parent_.children_.Remove(Me)
                End If
                parent_ = value
                If parent_ IsNot Nothing Then
                    parent_.children_.Add(Me)
                    level_ = parent_.level_
                Else
                    level_ = 0
                End If
            End Set
        End Property

        ReadOnly Property Level() As Integer
            Get
                Return level_
            End Get
        End Property

        Public Function AddChild(ByVal value As T) As TreeNode(Of T)
            Dim tn As New TreeNode(Of T)(value)
            AddChild(tn)
            Return tn
        End Function

        Public Sub AddChild(ByVal tn As TreeNode(Of T))
            tn.parent_ = Me
            tn.level_ = level_ + 1
            children_.Add(tn)
        End Sub

        Public Sub AddChildren(ByVal l As IEnumerable(Of T))
            Dim result As New List(Of TreeNode(Of T))
            For Each element In l
                result.Add(AddChild(element))
            Next
        End Sub

        Sub AddChildren(ByVal l As IEnumerable(Of TreeNode(Of T)))
            For Each element In l
                AddChild(element)
            Next
        End Sub

        Public Sub RemoveChild(ByVal tn As TreeNode(Of T))
            If parent_ IsNot Nothing Then
                parent_.RemoveChild(Me)
            End If
            tn.parent_ = Nothing
            tn.level_ = 0
            children_.Remove(tn)
        End Sub

        Public Function GetChildren() As IEnumerable(Of ITreeNode(Of T)) Implements ITreeNode(Of T).GetChildren
            Return Children
        End Function

        Public Function GetParent() As ITreeNode(Of T) Implements ITreeNode(Of T).GetParent
            Return Parent
        End Function
    End Class

End Namespace
