Imports System.Text
Imports System.IO
Imports System.IO.StringWriter
Imports Icm.IO



'''<summary>
'''This is a test class for CompositeWriterTest and is intended
'''to contain all CompositeWriterTest Unit Tests
'''</summary>
<TestFixture(), Category("Icm")>
Public Class CompositeWriterTest

    '''<summary>
    '''A test for WriteLine
    '''</summary>
    <Test(), Category("Icm")>
    Public Sub CompositeWriterTest()
        Dim target As CompositeWriter = New CompositeWriter
        Dim s As String = String.Empty

        s = "hola"
        Dim sw1 As New StringWriter()

        target.Add(sw1)
        target.Write(s)

        Assert.IsTrue(sw1.ToString = "hola")

        Dim sw2 As New StringWriter()
        target.Add(sw2)
        target.WriteLine(Date.Now)
        'Debug.WriteLine(sw1)
        Dim aux2 = "hola" & sw2.ToString
        Assert.IsTrue(sw1.ToString = aux2)

        Dim sw3 As New StringWriter
        target.Add(sw3)
        target.Write("Prueba")
        Assert.IsTrue(sw1.ToString = "hola" & sw2.ToString)
        Assert.IsTrue(sw3.ToString = "Prueba")
        target.Close()


    End Sub

End Class
