Imports Icm

<TestFixture(), Category("Icm")>
Public Class LongExtensionsTest

    <TestCase(34L)>
    <TestCase(5L)>
    <TestCase(201L)>
    <TestCase(0L)>
    <TestCase(-1L, ExpectedException:=GetType(ArgumentOutOfRangeException))>
    Public Sub HumanFileSize1_Test(target As Long)
        Assert.That(target.HumanFileSize(), [Is].EqualTo(target.HumanFileSize(decimalUnits:=True, bigUnitNames:=False, format:="0.00")))
    End Sub

    <TestCase(34L, True, False, Nothing, Result:="34 B")>
    <TestCase(5L, False, False, Nothing, Result:="5 B")>
    <TestCase(201L, True, True, "F2", Result:="201,00 bytes")>
    <TestCase(0L, True, False, Nothing, Result:="0 B")>
    <TestCase(-1L, True, False, Nothing, ExpectedException:=GetType(ArgumentOutOfRangeException))>
    Public Function HumanFileSize_Test(target As Long, decimalUnits As Boolean, bigUnitNames As Boolean, format As String) As String
        Return target.HumanFileSize(decimalUnits, bigUnitNames, format)
    End Function

End Class
