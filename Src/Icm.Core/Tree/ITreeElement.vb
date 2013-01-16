Namespace Icm.Tree

    ''' <summary>
    ''' This is the most basic tree structure, a node with a type value and an enumeration of children.
    ''' Note that ITreeElement does not have a reference to its parent.
    ''' </summary>
    ''' <typeparam name="T"></typeparam>
    ''' <remarks></remarks>
    Public Interface ITreeElement(Of T)
        Inherits IEnumerable(Of ITreeElement(Of T))

        Property Value() As T
    End Interface

End Namespace
