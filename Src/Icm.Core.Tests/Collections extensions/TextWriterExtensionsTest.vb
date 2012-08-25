Imports System.IO
Imports Icm.IO

<TestFixture(), Category("Icm")>
Public Class TextWriterExtensionsTest

    <Test()>
    Public Sub WriteUnderlineTest()
        Dim tw As TextWriter = New StringWriter
        Dim s = "hola"
        tw.WriteUnderline(s)

        Dim actual = tw.ToString

        Dim expected = "hola" & vbCrLf & "----" & vbCrLf

        Assert.That(actual, [Is].EqualTo(expected))
    End Sub
End Class
