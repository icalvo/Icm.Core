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

    '''<summary>
    '''A test for IsOneOf
    '''</summary>
    <Test()>
    Public Sub IsOneOfTest()
        Dim s As String = String.Empty

        Dim expected As Boolean = False
        Dim actual As Boolean

        'Caso 1
        Dim sa As String() = {"hola", "maria", "pato", "perro"}
        s = "hola"
        expected = True
        actual = s.IsOneOf(sa)
        Assert.AreEqual(expected, actual)
        sa = Nothing

        'Caso 2
        s = "adios"
        sa = {"hola", "maria", "pato", "perro"}
        expected = False
        actual = s.IsOneOf(sa)
        Assert.AreEqual(expected, actual)
        sa = Nothing

        'Caso3
        s = Nothing
        sa = {"hola", "maria", "pato", "perro"}
        expected = False
        actual = s.IsOneOf(sa)
        Assert.AreEqual(expected, actual)

    End Sub
End Class
