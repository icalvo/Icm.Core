Imports System.Text
Imports System.Runtime.CompilerServices

Namespace Icm.Collections

    Public Module ICollectionExtensions

        ''' <summary>
        ''' Remove an item, not failing if it doesn't exist.
        ''' </summary>
        ''' <typeparam name="T"></typeparam>
        ''' <param name="c"></param>
        ''' <param name="item"></param>
        ''' <remarks></remarks>
        <Extension()>
        Sub ForceRemove(Of T)(ByVal c As ICollection(Of T), ByVal item As T)
            If c.Contains(item) Then
                c.Remove(item)
            End If
        End Sub

    End Module

End Namespace
