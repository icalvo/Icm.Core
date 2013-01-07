Imports Icm.Collections


<TestFixture>
Public Class ITreeNodeExtensionsTest

    Private Shared Function GetTree() As TreeNode(Of Char)
        Return Node("A"c,
                    Node("B"c),
                    Node("C"c,
                         Node("D"c),
                         Node("E"c)),
                    Node("F"c))
    End Function

    Private Shared Function GetRepresentation(result As IEnumerable(Of Char)) As String
        Return New String(result.ToArray)
    End Function

    Private Shared Function GetRepresentation(tupleResult As IEnumerable(Of Tuple(Of Char, Integer))) As String
        Return tupleResult.Select(Function(tup) tup.Item1 & tup.Item2).JoinStr("")
    End Function

    <Test>
    Public Sub DepthPreorderTraverseTest()
        Dim actual = GetRepresentation(GetTree().DepthPreorderTraverse)
        Assert.That(actual, [Is].EqualTo("ABCDEF"))
    End Sub

    <Test>
    Public Sub DepthPostorderTraverseTest()
        Dim actual = GetRepresentation(GetTree().DepthPostorderTraverse)
        Assert.That(actual, [Is].EqualTo("BDECFA"))
    End Sub

    <Test>
    Public Sub BreadthTraverseTest()
        Dim actual = GetRepresentation(GetTree().BreadthTraverse)
        Assert.That(actual, [Is].EqualTo("ABCFDE"))
    End Sub

    <Test>
    Public Sub DepthPreorderTraverseWithLevelTest()
        Dim actual = GetRepresentation(GetTree.DepthPreorderTraverseWithLevel)
        Assert.That(actual, [Is].EqualTo("A0B1C1D2E2F1"))
    End Sub

    <Test>
    Public Sub DepthPostorderTraverseWithLevelTest()
        Dim actual = GetRepresentation(GetTree.DepthPostorderTraverseWithLevel)
        Assert.That(actual, [Is].EqualTo("B1D2E2C1F1A0"))
    End Sub

    <Test>
    Public Sub BreadthTraverseWithLevelTest()
        Dim actual = GetRepresentation(GetTree.BreadthTraverseWithLevel)
        Assert.That(actual, [Is].EqualTo("A0B1C1F1D2E2"))
    End Sub

    <Test>
    Public Sub AncestorsTest()
        Dim leaf = Node("E"c)

        Dim tree = Node("A"c, Node("B"c, Node("C"c, Node("D"c, leaf))))

        Dim actual = GetRepresentation(leaf.Ancestors)
        Assert.That(actual, [Is].EqualTo("EDCBA"))
    End Sub

    <Test>
    Public Sub ProperAncestorsTest()
        Dim leaf = Node("E"c)

        Dim tree = Node("A"c, Node("B"c, Node("C"c, Node("D"c, leaf))))

        Dim actual = GetRepresentation(leaf.ProperAncestors)
        Assert.That(actual, [Is].EqualTo("DCBA"))
    End Sub

End Class
