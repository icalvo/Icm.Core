

Imports Icm



'''<summary>
'''This is a test class for LongExtensionsTest and is intended
'''to contain all LongExtensionsTest Unit Tests
'''</summary>
<TestFixture(), Category("Icm")>
Public Class LongExtensionsTest



#Region "Additional test attributes"
    '
    'You can use the following additional attributes as you write your tests:
    '
    'Use ClassInitialize to run code before running the first test in the class
    '<ClassInitialize()>  _
    'Public Shared Sub MyClassInitialize(ByVal testContext As TestContext)
    'End Sub
    '
    'Use ClassCleanup to run code after all tests in a class have run
    '<ClassCleanup()>  _
    'Public Shared Sub MyClassCleanup()
    'End Sub
    '
    'Use TestInitialize to run code before running each test
    '<TestInitialize()>  _
    'Public Sub MyTestInitialize()
    'End Sub
    '
    'Use TestCleanup to run code after each test has run
    '<TestCleanup()>  _
    'Public Sub MyTestCleanup()
    'End Sub
    '
#End Region


    '''<summary>
    '''A test for HumanFileSize
    '''</summary>
    <Test(), Category("Icm")>
    Public Sub HumanFileSizeTest_BasicCases()
        Dim expected As String
        Dim actual As String

        'Caso 1
        expected = "34 B"
        actual = LongExtensions.HumanFileSize(34, decimalUnits:=True, bigUnitNames:=False, format:=Nothing)
        Assert.AreEqual(expected, actual)

        'Caso 2
        expected = "5 B"
        actual = LongExtensions.HumanFileSize(5, decimalUnits:=False, bigUnitNames:=False, format:=Nothing)
        Assert.AreEqual(expected, actual)

        'Caso 3
        expected = "201,00 bytes"
        actual = LongExtensions.HumanFileSize(CLng(200.80000000000001), decimalUnits:=True, bigUnitNames:=True, format:=("F2"))
        Assert.AreEqual(expected, actual)

    End Sub

    <Test(), Category("Icm")>
    Public Sub HumanFileSizeTest_ExtremeCases()
        Dim expected As String
        Dim actual As String

        'Caso 1
        Try
            actual = LongExtensions.HumanFileSize(-34, decimalUnits:=True, bigUnitNames:=False, format:=Nothing)
            Assert.Fail("Should not accept negative numbers")
        Catch ex As ArgumentOutOfRangeException
        Catch ex As Exception
            Assert.Fail("Should throw ArgumentOutOfRangeException")
        End Try

        'Caso 2
        expected = "0 bytes"
        actual = LongExtensions.HumanFileSize(0, decimalUnits:=True, bigUnitNames:=True, format:=Nothing)
        Assert.AreEqual(expected, actual)

    End Sub

End Class
