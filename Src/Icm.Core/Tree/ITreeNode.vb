Namespace Icm.Tree

    ''' <summary>
    ''' Adds notion of parent. The parent is an ITreeNode itself, so that we can follow the lineage
    ''' up to the root (see <see cref="ITreeNodeExtensions"></see>)
    ''' </summary>
    ''' <typeparam name="T"></typeparam>
    ''' <remarks></remarks>
    Public Interface ITreeNode(Of T)
        Inherits ITreeElement(Of T)

        Function GetParent() As ITreeNode(Of T)

    End Interface
End Namespace
