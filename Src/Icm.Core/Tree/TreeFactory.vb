Imports System.Runtime.CompilerServices

Namespace Icm.Tree

    ''' <summary>
    ''' Functions to build tree nodes and tree elements.
    ''' </summary>
    ''' <remarks>
    ''' These functions provide the easiest way to build a tree, using nested function calls:
    ''' 
    ''' <code>
    ''' Dim myTree = Node("root",
    '''                Node("child1"),
    '''                Node("child2",
    '''                  Node("grandchild1"))
    '''                Node("child3")))
    ''' </code>
    ''' </remarks>
    Public Class TreeFactory

        Public Shared Function Node(Of T)(v As T, ParamArray children() As TreeNode(Of T)) As TreeNode(Of T)
            Dim result As New TreeNode(Of T)(v)
            result.AddChildren(children)
            Return result
        End Function

        Public Shared Function Element(Of T)(v As T, ParamArray children() As TreeElement(Of T)) As TreeElement(Of T)
            Dim result As New TreeElement(Of T)(v)
            result.AddRange(children)
            Return result
        End Function

    End Class

End Namespace
