Namespace Icm.Tree


    ''' <summary>
    ''' Proxy for a ITreeNode(Of T1) that implements an ITreeNode(Of T2).
    ''' </summary>
    ''' <typeparam name="T1"></typeparam>
    ''' <typeparam name="T2"></typeparam>
    ''' <remarks>A TransformTreeNode(Of T1,T2) gives the full funcionality of
    ''' an ITreeNode(Of T2) by using a transformation function from T1 to T2. It builds its members on the fly,
    ''' whenever GetChildren is called, by building new TransformTreeNodes wrapping the original children.</remarks>
    Public Class TransformTreeNode(Of T1, T2)
        Inherits TransformTreeElement(Of T1, T2)
        Implements ITreeNode(Of T2)

        Private ReadOnly _baseNode As ITreeNode(Of T1)

        Public Sub New(node As ITreeNode(Of T1), transform As Func(Of T1, T2))
            MyBase.New(node, transform)
            _baseNode = node
        End Sub

        Public Function GetParent() As ITreeNode(Of T2) Implements ITreeNode(Of T2).GetParent
            Dim sourceParent = _basenode.GetParent
            If sourceParent Is Nothing Then
                Return Nothing
            Else
                Return New TransformTreeNode(Of T1, T2)(sourceParent, Transform)
            End If
        End Function

        Public Function GetChildNodes() As IEnumerable(Of ITreeNode(Of T2)) Implements ITreeNode(Of T2).GetChildNodes
#If FrameworkNet35 Then
            Return _basenode.GetChildNodes.Select(Function(child) DirectCast(New TransformTreeNode(Of T1, T2)(child, Transform), ITreeNode(Of T2)))
#Else
            Return _baseNode.GetChildNodes.Select(Function(child) New TransformTreeNode(Of T1, T2)(child, Transform))
#End If
        End Function
    End Class
End Namespace
