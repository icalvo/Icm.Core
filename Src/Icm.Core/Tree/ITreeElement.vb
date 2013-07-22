Namespace Icm.Tree

    ''' <summary>
    ''' This is the most basic tree structure, a node with a type value and an enumeration of children.
    ''' </summary>
    ''' <typeparam name="T"></typeparam>
    ''' <remarks>Note that ITreeElement does not have a reference to its parent.</remarks>
    Public Interface ITreeElement(Of T)

        Property Value() As T
        Function GetChildElements() As IEnumerable(Of ITreeElement(Of T))
    End Interface

End Namespace
