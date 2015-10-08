Imports System.IO
Imports Icm.Text

<TestFixture(), Category("Icm")>
Public Class ReplacerTest

    Shared ReadOnly ConstructorTestCases As Object() = {
        New TestCaseData(New StringReader(""), New StringWriter(), "", "}").Throws(GetType(ArgumentException)),
        New TestCaseData(New StringReader(""), New StringWriter(), "{", "").Throws(GetType(ArgumentException)),
        New TestCaseData(New StringReader(""), New StringWriter(), Nothing, "}").Throws(GetType(ArgumentException)),
        New TestCaseData(New StringReader(""), New StringWriter(), "{", Nothing).Throws(GetType(ArgumentException)),
        New TestCaseData(New StringReader(""), Nothing, "{", "}").Throws(GetType(ArgumentNullException)),
        New TestCaseData(Nothing, New StringWriter(), "{", "}").Throws(GetType(ArgumentNullException))
    }

    <TestCaseSource("ConstructorTestCases")>
    Public Sub Constructor_Test(sr As StringReader, sw As StringWriter, tgstart As String, tgend As String)
        Dim target As New Replacer(sr, sw, tgstart, tgend)
    End Sub

    Shared ReadOnly ReplaceTestCases As Object() = {
        New TestCaseData(
            "HOLA SOY {<NOMBRE>} HOY ES {<FECHA>}", "{<", ">}",
            {{"NOMBRE", "MARIA"}, {"FECHA", "25/04/2010"}}).Returns("HOLA SOY MARIA HOY ES 25/04/2010"),
        New TestCaseData(
            "HOLA SOY {<NOMBRE>} HOY ES {<FECHA>}", "{<", ">}",
            {{"a", "n"}}).Returns("HOLA SOY {<NOMBRE>} HOY ES {<FECHA>}"),
        New TestCaseData(
            "HOLA SOY {<NOMBRE>OTRO>} HOY ES {<FECHA>}", "{<", ">}",
            {{"NOMBRE>OTRO", "MARIA"}, {"FECHA", "25/04/2010"}}).Returns("HOLA SOY MARIA HOY ES 25/04/2010"),
        New TestCaseData(
            "HOLA SOY {<NOMBRE>} HOY ES {<FECHA", "{<", ">}",
            {{"NOMBRE", "MARIA"}, {"FECHA", "25/04/2010"}}).Returns("HOLA SOY MARIA HOY ES 25/04/2010"),
        New TestCaseData(
            "Noreps", "{<", ">}",
            {{"NOMBRE", "MARIA"}, {"FECHA", "25/04/2010"}}).Returns("Noreps"),
        New TestCaseData(
            "", "{<", ">}",
            {{"NOMBRE", "MARIA"}, {"FECHA", "25/04/2010"}}).Returns(""),
        New TestCaseData(
            "HOLA SOY {<{<NOMBRE>}>} HOY ES {<FECHA>}", "{<", ">}",
            {{"NOMBRE", "MARIA"}, {"FECHA", "25/04/2010"}}).Returns("HOLA SOY {<{<NOMBRE>}>} HOY ES 25/04/2010")
    }

    <TestCaseSource("ReplaceTestCases")>
    Public Function ReplaceAndClose_Test(source As String, tgstart As String, tgend As String, replacements As String(,)) As String
        Dim sw As New StringWriter
        Dim sr As New StringReader(source)
        Dim target As New Replacer(sr, sw, tgstart, tgend)
        Dim repDict As New Dictionary(Of String, String)

        For i = 0 To replacements.GetUpperBound(0)
            target.AddReplacement(replacements(i, 0), replacements(i, 1))
        Next
        target.ReplaceAndClose()

        Return sw.ToString
    End Function


    '''<summary>
    '''A test for ModifyReplacement
    '''</summary>
    <Test()>
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
    <Test()>
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
