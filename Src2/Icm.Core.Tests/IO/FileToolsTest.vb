Imports Icm.IO
Imports System.IO

<TestFixture(), Category("Icm")>
Public Class FileToolsTest

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
