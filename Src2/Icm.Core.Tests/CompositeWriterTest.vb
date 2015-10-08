Imports System.Text
Imports System.IO
Imports System.IO.StringWriter
Imports Icm.IO

<TestFixture(), Category("Icm")>
Public Class CompositeWriterTest

    '''<summary>
    '''A test for WriteLine
    '''</summary>
    <Test()>
    Public Sub Write_Test()
        Dim target As New CompositeWriter
        Dim s1 = "hola"
        Dim s2 = "quetal"
        Dim s3 = "adios"
        Dim sw1 As New StringWriter
        Dim sw2 As New StringWriter
        Dim sw3 As New StringWriter

        target.Add(sw1)
        target.Write(s1)

        Assert.That(sw1.ToString = "hola")

        target.Add(sw2)
        target.Write(s2)

        Assert.That(sw1.ToString = "holaquetal")
        Assert.That(sw2.ToString = "quetal")

        target.Add(sw3)
        target.Write(s3)
        Assert.That(sw1.ToString = "holaquetaladios")
        Assert.That(sw2.ToString = "quetaladios")
        Assert.That(sw3.ToString = "adios")
        target.Close()
    End Sub

End Class
