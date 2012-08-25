

Imports Icm.IO
Imports System.IO



'''<summary>
'''This is a test class for FileToolsTest and is intended
'''to contain all FileToolsTest Unit Tests
'''</summary>
<TestFixture(), Category("Icm")>
Public Class FileToolsTest




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



    ' '''<summary>
    ' '''A test for FormatFile
    ' '''</summary>
    '<Test()>
    'Public Sub FormatFileTest()

    '    'Caso 1
    '    Dim templatefn As String = "D:\PRUEBA.txt"
    '    Dim args() As Object = {"roja", "Luna"}
    '    Dim expected As String = "la casa roja esta en la calle Luna"
    '    Dim actual As String
    '    actual = FileTools.FormatFile(templatefn, args)
    '    Assert.AreEqual(expected, actual)

    'End Sub

    '''<summary>
    '''A test for FormatFile
    '''</summary>
    <Test()>
    Public Sub FormatFileTest2()

        'Caso 1
        Dim template As New StringReader("la casa {0} esta en la calle {1}")
        Dim args() As Object = {"roja", "Luna"}
        Dim expected As String = "la casa roja esta en la calle Luna"
        Dim actual As String
        actual = FileTools.FormatFile(template, args)
        Assert.AreEqual(expected, actual)

    End Sub

End Class
