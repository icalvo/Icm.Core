Imports Icm.Reflection


<TestFixture>
Public Class ObjectCopyExtensionsTest

    <Test>
    Public Sub CloneTest()
        Dim source As New MyExample

        source.mynumber = 3849
        source.MyProp = "clone value 1"
        source.MyPropNumber = 457

        Dim clone = source.Clone

        Assert.That(source.MyProp, [Is].EqualTo(clone.MyProp))
        Assert.That(source.MyPropNumber, [Is].EqualTo(clone.MyPropNumber))
        Assert.That(source.mynumber, [Is].EqualTo(clone.mynumber))
    End Sub

    <Test>
    Public Sub ClonePropertyExcludeTest()
        Dim source As New MyExample

        source.mynumber = 3849
        source.MyProp = "clone value 1"
        source.MyPropNumber = 457

        Dim clone = source.Clone(excludedMembers:={"MyProp"})

        Assert.That(source.MyProp, [Is].Not.EqualTo(clone.MyProp))
        Assert.That(source.MyPropNumber, [Is].EqualTo(clone.MyPropNumber))
        Assert.That(source.mynumber, [Is].EqualTo(clone.mynumber))
    End Sub

    <Test>
    Public Sub CloneTypeExcludeTest()
        Dim source As New MyExample

        source.mynumber = 3849
        source.MyProp = "clone value 1"
        source.MyPropNumber = 457

        Dim clone = source.Clone(excludedTypes:={"String"})

        Assert.That(source.MyProp, [Is].Not.EqualTo(clone.MyProp))
        Assert.That(source.MyPropNumber, [Is].EqualTo(clone.MyPropNumber))
        Assert.That(source.mynumber, [Is].EqualTo(clone.mynumber))
    End Sub

End Class
