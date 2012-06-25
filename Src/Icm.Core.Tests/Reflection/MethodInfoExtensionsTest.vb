Imports Icm.Reflection

<TestFixture()>
Public Class MethodInfoExtensionsTest

    <Test()>
    Public Sub GetAttributesTest()
        Dim mi = GetType(ClassB).GetMethod("Routine")

        Dim attrs() As ExampleAttribute
        attrs = mi.GetAttributes(Of Example1Attribute)(False)
        Assert.IsTrue(attrs.Count = 1)
        Assert.AreEqual("classB", attrs(0).Message)

        attrs = mi.GetAttributes(Of Example1Attribute)(True)
        Assert.IsTrue(attrs.Count = 3)
        Assert.IsTrue(attrs.Any(Function(attr) attr.Message = "classAfirst"))
        Assert.IsTrue(attrs.Any(Function(attr) attr.Message = "classAsecond"))
        Assert.IsTrue(attrs.Any(Function(attr) attr.Message = "classB"))

        attrs = mi.GetAttributes(Of Example3Attribute)(True)
        Assert.IsTrue(attrs.Count = 0)
    End Sub

    <Test()>
    Public Sub HasAttributeTest()
        Dim mi = GetType(ClassB).GetMethod("Routine")

        Assert.IsFalse(mi.HasAttribute(Of Example2Attribute)(False))
        Assert.IsTrue(mi.HasAttribute(Of Example2Attribute)(True))
    End Sub

    <Test()>
    Public Sub GetAttribute_FailsWhenNonExistentTest()
        Dim mi = GetType(ClassB).GetMethod("Routine")

        Assert.Throws(Of InvalidOperationException)(
            Sub()
                mi.GetAttribute(Of Example2Attribute)(False)
            End Sub)

        Assert.Throws(Of InvalidOperationException)(
            Sub()
                mi.GetAttribute(Of Example3Attribute)(True)
            End Sub)
    End Sub

    <Test()>
    Public Sub GetAttribute_FailsWhenMultipleTest()
        Dim mi = GetType(ClassB).GetMethod("Routine")

        Assert.Throws(Of InvalidOperationException)(
            Sub()
                mi.GetAttribute(Of Example1Attribute)(True)
            End Sub)
    End Sub

    <Test()>
    Public Sub GetAttributeTest()
        Dim mi = GetType(ClassB).GetMethod("Routine")

        Dim attr As ExampleAttribute
        attr = mi.GetAttribute(Of Example1Attribute)(False)
        Assert.AreEqual("classB", attr.Message)
    End Sub

End Class

MustInherit Class ExampleAttribute
    Inherits Attribute

    Property Message As String

    Public Sub New(Optional msg As String = "")
        Message = msg
    End Sub

End Class


<AttributeUsage(AttributeTargets.All, AllowMultiple:=True, Inherited:=True)>
Class Example1Attribute
    Inherits ExampleAttribute

    Public Sub New(Optional msg As String = "")
        MyBase.New(msg)
    End Sub
End Class

<AttributeUsage(AttributeTargets.All, AllowMultiple:=True, Inherited:=True)>
Class Example2Attribute
    Inherits ExampleAttribute

    Public Sub New(Optional msg As String = "")
        MyBase.New(msg)
    End Sub
End Class

<AttributeUsage(AttributeTargets.All, AllowMultiple:=True, Inherited:=True)>
Class Example3Attribute
    Inherits ExampleAttribute

    Public Sub New(Optional msg As String = "")
        MyBase.New(msg)
    End Sub
End Class

Class ClassA

    <Example1("classAfirst"), Example1("classAsecond"), Example2("classA")>
    Public Overridable Sub Routine()

    End Sub

End Class


Class ClassB
    Inherits ClassA

    <Example1("classB")>
    Public Overrides Sub Routine()

    End Sub

End Class
