Namespace Icm.Tree

    ''' <summary>
    ''' Physical, in-memory implementation of a ITreeElement.
    ''' </summary>
    ''' <typeparam name="T"></typeparam>
    ''' <remarks>
    ''' <para>Unlike TreeNode, TreeElement doesn't forces its relatives to be TreeElements. The absence of parent
    ''' concept in ITreeElements makes it impossible to maintain the parent-child relationship.</para>
    ''' <para>A TreeElement is just implementing the children enumeration with a list and providing methods to
    ''' manage that list.</para>
    ''' </remarks>
    Public Class TreeElement(Of T)
        Inherits List(Of ITreeElement(Of T))
        Implements ITreeElement(Of T)

        Public Sub New(ByVal v As T)
            Value = v
        End Sub

        Property Value() As T Implements ITreeElement(Of T).Value

        Public Function AddChild(ByVal value As T) As TreeElement(Of T)
            Dim tn As New TreeElement(Of T)(value)
            Add(tn)
            Return tn
        End Function

        Public Overridable Sub AddChildren(ByVal l As IEnumerable(Of T))
            For Each element In l
                Add(AddChild(element))
            Next
        End Sub

    End Class
End Namespace
