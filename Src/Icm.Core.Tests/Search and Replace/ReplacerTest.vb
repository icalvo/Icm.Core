Imports System.IO
Imports Icm.Text

Imports System.IO.StringWriter

'''<summary>
'''This is a test class for ReplacerTest and is intended
'''to contain all ReplacerTest Unit Tests
'''</summary>
<TestFixture(), Category("Icm")>
Public Class ReplacerTest



    '''<summary>
    '''A test for TagStart
    '''</summary>
    <Test(), Category("Icm")>
    Public Sub EmptyTagStartTest()

        Dim s1 As String = ""
        Dim tgstart As String = ""
        Dim tgend As String = "asdf"

        Using sw As New StringWriter, sr As New StringReader(s1)
            Assert.Throws(Of ArgumentException)(
                Sub()
                    Dim target As Replacer = New Replacer(sr, sw, tgstart, tgend)
                End Sub
            )
        End Using

    End Sub


    <Test(), Category("Icm")>
    Public Sub TagEndTest()
        Dim s1 As String = ""
        Dim tgstart As String = "asdf"
        Dim tgend As String = ""

        Using sw As New StringWriter, sr As New StringReader(s1)
            Assert.Throws(Of ArgumentException)(
                Sub()
                    Dim target As Replacer = New Replacer(sr, sw, tgstart, tgend)
                End Sub
            )
        End Using

    End Sub


    '''<summary>
    '''A test for Replace
    '''</summary>
    <Test(), Category("Icm")>
    Public Sub ReplaceTest()

        Dim s1 As String = "HOLA SOY -NOMBRE- HOY ES -FECHA-"
        Dim sw As New StringWriter
        Dim sr As New StringReader(s1)
        Dim tgstart As String = "-"
        Dim tgend As String = "-"

        Dim target As New Replacer(sr, sw, tgstart, tgend)
        target.AddReplacement("NOMBRE", "MARIA")
        target.AddReplacement("FECHA", "25/04/2010")
        target.ReplaceAndClose()

        Debug.WriteLine(sw)
        Dim resultado As String = sw.ToString
        Dim expected1 As String = "HOLA SOY MARIA HOY ES 25/04/2010"
        Assert.AreEqual(expected1, resultado)

    End Sub


    '''<summary>
    '''A test for ModifyReplacement
    '''</summary>
    <Test(), Category("Icm")>
    Public Sub ModifyReplacementTest1()

        Dim s1 As String = "HOLA SOY -NOMBRE- HOY ES -FECHA-"
        Dim sw As New StringWriter
        Dim sr As New StringReader(s1)
        Dim tgstart As String = "-"
        Dim tgend As String = "-"

        Dim target As New Replacer(sr, sw, tgstart, tgend)
        target.AddReplacement("NOMBRE", "MARIA")
        target.AddReplacement("FECHA", "25/04/2010")
        target.ModifyReplacement("FECHA", "26/04/2010")
        target.ReplaceAndClose()

        Debug.WriteLine(sw)
        Dim resultado As String = sw.ToString
        Dim expected1 As String = "HOLA SOY MARIA HOY ES 26/04/2010"
        Assert.AreEqual(expected1, resultado)
    End Sub


    '''<summary>
    '''A test for AddReplacement
    '''</summary>
    <Test(), Category("Icm")>
    Public Sub AddReplacementTest()

        Dim s1 As String = "HOLA SOY -NOMBRE- HOY ES -NOMBRE-"
        Dim sw As New StringWriter
        Dim sr As New StringReader(s1)
        Dim tgstart As String = "-"
        Dim tgend As String = "-"

        Dim target As New Replacer(sr, sw, tgstart, tgend)
        Dim search As String = "NOMBRE"
        Dim replacement As New AutoNumberGenerator()
        target.AddReplacement(search, replacement)
        target.ReplaceAndClose()
        Debug.WriteLine(sw)

        Dim resultado As String = sw.ToString
        Dim expected1 As String = "HOLA SOY 2 HOY ES 3"
        Assert.AreEqual(expected1, resultado)

    End Sub



End Class
