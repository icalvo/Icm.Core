Imports System.IO
Imports Icm.IO




'''<summary>
'''This is a test class for TextWriterExtensionsTest and is intended
'''to contain all TextWriterExtensionsTest Unit Tests
'''</summary>
<TestFixture(), Category("Icm")>
Public Class TextWriterExtensionsTest


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
    '''A test for WriteUnderline
    '''</summary>
    <Test(), Category("Icm")>
    Public Sub WriteUnderlineTest()

        'Create file and underline text with dashes
        Dim tw = File.CreateText("D:\prueba22.txt")
        Dim s As String = String.Empty
        s = "hola"
        tw.WriteUnderline(s)
        tw.Close()
        Dim rt = File.OpenText("D:\prueba22.txt")
        Dim resultado As String
        resultado = rt.ReadLine
        Assert.AreEqual(s, resultado)
        resultado = rt.ReadLine
        Assert.AreEqual(resultado, "----")

    End Sub
End Class
