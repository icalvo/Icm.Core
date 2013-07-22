Namespace Icm.Tree

    ''' <summary>
    ''' A TreeNode is a physical, in-memory implementation of ITreeNode.
    ''' </summary>
    ''' <typeparam name="T"></typeparam>
    ''' <remarks>
    ''' <para>A TreeNode only relates with other TreeNodes: its parent can only be a TreeNode and
    ''' its children are also bound to be TreeNodes. This allows to implement automatic Parent-Children
    ''' relationship management, so that if you add a child to a TreeNode, this child will automatically have
    ''' the current node as its parent; and if you set a TreeNode to be the parent of another TreeNode, that
    ''' one will be automatically added to the children collection of the current one.</para>
    ''' <para>In addition, a TreeNode maintains its level, which is zero for a parentless node and
    ''' equal to the parent's level plus one if the node has a parent.</para>
    ''' </remarks>
    Public Class TreeNode(Of T)
        Implements ITreeNode(Of T)

        Private _parent As TreeNode(Of T)
        Private ReadOnly _children As New List(Of TreeNode(Of T))()
        Private _level As Integer

        Public Sub New(ByVal v As T)
            Value = v
        End Sub

        Property Value() As T Implements ITreeNode(Of T).Value

        ReadOnly Property Children() As IEnumerable(Of TreeNode(Of T))
            Get
                Return _children
            End Get
        End Property

        Property Parent() As TreeNode(Of T)
            Get
                Return _parent
            End Get
            Set(ByVal value As TreeNode(Of T))
                If _parent IsNot Nothing Then
                    _parent._children.Remove(Me)
                End If
                _parent = value
                If _parent IsNot Nothing Then
                    _parent._children.Add(Me)
                    _level = _parent._level
                Else
                    _level = 0
                End If
            End Set
        End Property

        ReadOnly Property Level() As Integer
            Get
                Return _level
            End Get
        End Property

        Public Function AddChild(ByVal value As T) As TreeNode(Of T)
            Dim tn As New TreeNode(Of T)(value)
            AddChild(tn)
            Return tn
        End Function

        Public Function AddChild(ByVal tn As TreeNode(Of T)) As TreeNode(Of T)
            tn._parent = Me
            tn._level = _level + 1
            _children.Add(tn)

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
            If _parent IsNot Nothing Then
                _parent.RemoveChild(Me)
            End If
            tn._parent = Nothing
            tn._level = 0
            _children.Remove(tn)
        End Sub

        Public Function GetParent() As ITreeNode(Of T) Implements ITreeNode(Of T).GetParent
            Return Parent
        End Function

        Private Function GetChildElements() As IEnumerable(Of ITreeElement(Of T)) Implements ITreeElement(Of T).GetChildElements
#If FrameworkNet35 Then
            Return _children.ConvertAll(Of ITreeElement(Of T))(Function(elem) elem)
#Else
            Return _children
#End If
        End Function

        Private Function GetChildNodes() As IEnumerable(Of ITreeNode(Of T)) Implements ITreeNode(Of T).GetChildNodes
#If FrameworkNet35 Then
            Return _children.ConvertAll(Of ITreeNode(Of T))(Function(elem) elem)
#Else
            Return _children
#End If
        End Function
    End Class

End Namespace
