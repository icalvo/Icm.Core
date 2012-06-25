<TestFixture()>
Public Class ObjectExtensionsTest

    <Test()> _
    Public Sub IfNothingTest()
        Dim str As String

        str = "value"
        Assert.AreEqual("value", str.IfNothing("subst"))

        str = Nothing
        Assert.AreEqual("subst", str.IfNothing("subst"))

        Assert.AreEqual("subst", DBNull.Value.IfNothing("subst"))

    End Sub

End Class
