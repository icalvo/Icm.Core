<TestFixture()> _
Public Class DoubleExtensionsTest

    <Test()> _
    Public Sub ChangePrecisionTest()
        Dim actual = (5.2347999999999999R).ChangePrecision(1)
        Const expected As Double = 5.2000000000000002R
        Assert.AreEqual(expected, actual, 0.0001R)
    End Sub

    <Test()> _
    Public Sub Deg2RadTest()
        Dim actual = (90.0R).Deg2Rad
        Const expected As Double = Math.PI / 2
        Assert.AreEqual(expected, actual, 0.0001R)
    End Sub

    <Test()> _
    Public Sub Rad2DegTest()
        Dim actual As Double = (Math.PI / 2).Rad2Deg
        Const expected = 90.0R
        Assert.AreEqual(expected, actual, 0.0001R)
    End Sub

End Class
