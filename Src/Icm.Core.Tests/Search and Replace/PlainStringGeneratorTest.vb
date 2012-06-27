﻿

Imports Icm.Text



'''<summary>
'''This is a test class for PlainStringGeneratorTest and is intended
'''to contain all PlainStringGeneratorTest Unit Tests
'''</summary>
<TestFixture(), Category("Icm")>
Public Class PlainStringGeneratorTest


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
    '''A test for Generate
    '''</summary>
    <Test(), Category("Icm")>
    Public Sub GenerateTest()

        Dim s As String = "HOLA"
        Dim target As PlainStringGenerator = New PlainStringGenerator(s)
        Dim expected As String = "HOLA"
        Dim actual As String
        target.MoveNext()
        actual = target.Current
        Assert.AreEqual(expected, actual)

    End Sub


End Class
