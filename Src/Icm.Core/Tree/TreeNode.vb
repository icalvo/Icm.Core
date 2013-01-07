Namespace Icm

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

        Public Function AddChild(ByVal tn As TreeNode(Of T)) As TreeNode(Of T)
            tn.parent_ = Me
            tn.level_ = level_ + 1
            children_.Add(tn)

            Return tn
        End Function

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
            Return Children.Cast(Of ITreeNode(Of T))()
        End Function

        Public Function GetParent() As ITreeNode(Of T) Implements ITreeNode(Of T).GetParent
            Return Parent
        End Function
    End Class

End Namespace
