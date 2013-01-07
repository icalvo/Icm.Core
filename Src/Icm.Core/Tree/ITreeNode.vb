Namespace Icm
    Public Interface ITreeNode(Of T)
        Property Value() As T
        Function GetParent() As ITreeNode(Of T)
        Function GetChildren() As IEnumerable(Of ITreeNode(Of T))
    End Interface
End Namespace
