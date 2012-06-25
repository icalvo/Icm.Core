Imports Icm.Reflection

<TestFixture()>
Public Class ObjectReflectionExtensionsTest

    Class MyExample
        Public myfield As String = "mytest"

        Property MyProp As String = "mytest"

    End Class

    <Test()>
    Public Sub GetField_FailsOnNothingTest()
        Dim obj As MyExample

        obj = Nothing

        Assert.Throws(Of ArgumentNullException)(
            Sub()
                obj.GetField(Of String)("myfield")
            End Sub)
    End Sub

    <Test()>
    Public Sub GetField_FailsOnInexistentFieldTest()
        Dim obj As New MyExample

        Assert.Throws(Of ArgumentException)(
            Sub()
                obj.GetField(Of String)("myother")
            End Sub)
    End Sub

    <Test()>
    Public Sub GetField_FailsOnWrongTypeTest()
        Dim obj As New MyExample

        Assert.Throws(Of InvalidCastException)(
            Sub()
                obj.GetField(Of Integer)("myfield")
            End Sub)
    End Sub

    <Test()>
    Public Sub GetFieldTest()
        Dim obj As New MyExample

        Assert.AreEqual("mytest", obj.GetField(Of String)("myfield"))
    End Sub

    <Test()>
    Public Sub SetField_FailsOnNothingTest()
        Dim obj As MyExample

        obj = Nothing

        Assert.Throws(Of ArgumentNullException)(
            Sub()
                obj.SetField("myfield", "NEWVALUE")
            End Sub)
    End Sub

    <Test()>
    Public Sub SetField_FailsOnInexistentFieldTest()
        Dim obj As New MyExample

        Assert.Throws(Of ArgumentException)(
            Sub()
                obj.SetField("myother", "NEWVALUE")
            End Sub)
    End Sub

    <Test()>
    Public Sub SetField_FailsOnWrongTypeTest()
        Dim obj As New MyExample

        Assert.Throws(Of ArgumentException)(
            Sub()
                obj.SetField("myfield", 345)
            End Sub)
    End Sub

    <Test()>
    Public Sub SetFieldTest()
        Dim obj As New MyExample
        obj.SetField("myfield", "NEWVALUE")
        Assert.AreEqual("NEWVALUE", obj.myfield)
    End Sub

    <Test()>
    Public Sub HasField_DoesNotFailOnNothingTest()
        Dim obj As MyExample

        obj = Nothing

        Assert.IsTrue(obj.HasField("myfield"))
        Assert.IsFalse(obj.HasField("myother"))
    End Sub

    <Test()>
    Public Sub HasFieldTest()
        Dim obj As New MyExample

        Assert.IsTrue(obj.HasField("myfield"))
        Assert.IsFalse(obj.HasField("myother"))
    End Sub


    <Test()>
    Public Sub GetProp_FailsOnNothingTest()
        Dim obj As MyExample

        obj = Nothing

        Assert.Throws(Of ArgumentNullException)(
            Sub()
                obj.GetProp(Of String)("MyProp")
            End Sub)
    End Sub

    <Test()>
    Public Sub GetProp_FailsOnInexistentPropTest()
        Dim obj As New MyExample

        Assert.Throws(Of ArgumentException)(
            Sub()
                obj.GetProp(Of String)("myother")
            End Sub)
    End Sub

    <Test()>
    Public Sub GetProp_FailsOnWrongTypeTest()
        Dim obj As New MyExample

        Assert.Throws(Of InvalidCastException)(
            Sub()
                obj.GetProp(Of Integer)("MyProp")
            End Sub)
    End Sub

    <Test()>
    Public Sub GetPropTest()
        Dim obj As New MyExample

        Assert.AreEqual("mytest", obj.GetProp(Of String)("MyProp"))
    End Sub

    <Test()>
    Public Sub SetProp_FailsOnNothingTest()
        Dim obj As MyExample

        obj = Nothing

        Assert.Throws(Of ArgumentNullException)(
            Sub()
                obj.SetProp("MyProp", "NEWVALUE")
            End Sub)
    End Sub

    <Test()>
    Public Sub SetProp_FailsOnInexistentPropTest()
        Dim obj As New MyExample

        Assert.Throws(Of ArgumentException)(
            Sub()
                obj.SetProp("myother", "NEWVALUE")
            End Sub)
    End Sub

    <Test()>
    Public Sub SetProp_FailsOnWrongTypeTest()
        Dim obj As New MyExample

        Assert.Throws(Of ArgumentException)(
            Sub()
                obj.SetProp("MyProp", 345)
            End Sub)
    End Sub

    <Test()>
    Public Sub SetPropTest()
        Dim obj As New MyExample
        obj.SetProp("MyProp", "NEWVALUE")
        Assert.AreEqual("NEWVALUE", obj.MyProp)
    End Sub

    <Test()>
    Public Sub HasProp_DoesNotFailOnNothingTest()
        Dim obj As MyExample

        obj = Nothing

        Assert.IsTrue(obj.HasProp("MyProp"))
        Assert.IsFalse(obj.HasProp("myother"))
    End Sub

    <Test()>
    Public Sub HasPropTest()
        Dim obj As New MyExample

        Assert.IsTrue(obj.HasProp("MyProp"))
        Assert.IsFalse(obj.HasProp("myother"))
    End Sub

End Class
