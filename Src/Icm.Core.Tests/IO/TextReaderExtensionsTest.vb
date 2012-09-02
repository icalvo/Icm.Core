Imports Icm.IO
Imports System.IO

<TestFixture(), Category("Icm")>
Public Class TextReaderExtensionsTest

    '''<summary>
    '''A test for FormatFile
    '''</summary>
    <TestCase("la casa {0} esta en la calle {1}", {"roja", "Luna"}, Result:="la casa roja esta en la calle Luna")>
    Public Function Format_Test(format As String, args As Object()) As String
        Dim template As New StringReader(format)
        Return template.Format(args)
    End Function

End Class
