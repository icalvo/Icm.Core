Imports System.Runtime.CompilerServices

Namespace Icm.Collections
    Public Module LinkedListExtensions

        ''' <summary>
        ''' Copy operation for linked lists.
        ''' </summary>
        ''' <typeparam name="T"></typeparam>
        ''' <param name="l1"></param>
        ''' <param name="l2"></param>
        ''' <remarks></remarks>
        <Extension()>
        Public Sub CopyInto(Of T As ICloneable)(ByVal l1 As LinkedList(Of T), ByVal l2 As LinkedList(Of T))
            Dim itNode As LinkedListNode(Of T)

            itNode = l1.First
            Do Until itNode Is Nothing
                Dim copia As T = DirectCast(itNode.Value.Clone, T)
                l2.AddLast(copia)
                itNode = itNode.Next
            Loop
        End Sub

    End Module
End Namespace
