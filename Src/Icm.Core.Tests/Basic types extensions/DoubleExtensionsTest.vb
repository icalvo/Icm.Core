<Category("Icm")>
<TestFixture()>
Public Class DoubleExtensionsTest

    <TestCase(5.2348R, 1, Result:=5.2R)>
    Public Function ChangePrecision_Test(target As Double, precision As Integer) As Double
        Return target.ChangePrecision(precision)
    End Function

    <TestCase(90.0R, Result:=Math.PI / 2)>
    <TestCase(0.0R, Result:=0)>
    Public Function Deg2Rad_Test(target As Double) As Double
        Return target.Deg2Rad
    End Function

    <TestCase(Math.PI / 2, Result:=90.0R)>
    <TestCase(0.0R, Result:=0.0R)>
    Public Function Rad2Deg_Test(target As Double) As Double
        Return target.Rad2Deg
    End Function

End Class
