Imports System.Runtime.CompilerServices

Namespace Icm.Tree

    Public Module ITreeNodeExtensions

        ''' <summary>
        ''' Transforms a tree of T1 into a tree of T2 by means of a TransformTreeNode and a transform function that
        ''' converts T1 into T2.
        ''' </summary>
        ''' <typeparam name="T1"></typeparam>
        ''' <typeparam name="T2"></typeparam>
        ''' <param name="tn"></param>
        ''' <param name="transform"></param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        <Extension>
        Public Function [Select](Of T1, T2)(tn As ITreeNode(Of T1), transform As Func(Of T1, T2)) As ITreeNode(Of T2)
            Return New TransformTreeNode(Of T1, T2)(tn, transform)
        End Function

        ''' <summary>
        ''' Yields the ancestors of a node, not including the node
        ''' </summary>
        ''' <typeparam name="T"></typeparam>
        ''' <param name="tn"></param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        <Extension>
        Public Iterator Function ProperAncestors(Of T)(tn As ITreeNode(Of T)) As IEnumerable(Of T)
            Dim current = tn.GetParent
            Do Until current Is Nothing
                Yield current.Value
                current = current.GetParent
            Loop
        End Function

        ''' <summary>
        ''' Yields the ancestors of a node, including the node
        ''' </summary>
        ''' <typeparam name="T"></typeparam>
        ''' <param name="tn"></param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        <Extension>
        Public Iterator Function Ancestors(Of T)(tn As ITreeNode(Of T)) As IEnumerable(Of T)
            Dim current = tn
            Do
                Yield current.Value
                current = current.GetParent
            Loop Until current Is Nothing
        End Function

    End Module
End Namespace
