<TestFixture()>
Public Class BooleanExtensionsTest

    <Test()>
    Public Sub ToIntegerTest()
        Assert.AreEqual(1, True.ToInteger)
        Assert.AreEqual(0, False.ToInteger)
    End Sub

    <Test()>
    Public Sub IfNTest()
        Dim bool As Boolean?
        bool = True
        Assert.AreEqual("cadena verdad", bool.IfN("cadena verdad", "cadena falso", "cadena nulo"))
        bool = False
        Assert.AreEqual("cadena falso", bool.IfN("cadena verdad", "cadena falso", "cadena nulo"))
        bool = Nothing
        Assert.AreEqual("cadena nulo", bool.IfN("cadena verdad", "cadena falso", "cadena nulo"))
    End Sub

End Class
