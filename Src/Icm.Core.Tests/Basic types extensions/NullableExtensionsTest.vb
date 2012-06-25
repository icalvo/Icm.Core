<TestFixture()>
Public Class NullableExtensionsTest

    <Test()>
    Public Sub VTest()
        Dim obj As Integer?

        obj = 3
        Assert.AreEqual(obj.V, obj.Value)

        obj = Nothing
        Assert.Throws(Of InvalidOperationException)(
            Sub()
                Dim a = obj.V
            End Sub)
    End Sub


    <Test()>
    Public Sub HasNotValueTest()
        Dim obj As Integer?

        obj = 3
        Assert.AreEqual(obj.HasNotValue, Not obj.HasValue)

        obj = Nothing
        Assert.AreEqual(obj.HasNotValue, Not obj.HasValue)
    End Sub

End Class
