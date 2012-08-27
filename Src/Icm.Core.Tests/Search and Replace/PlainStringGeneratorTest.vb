Imports Icm.Text

<TestFixture(), Category("Icm")>
Public Class PlainStringGeneratorTest

    <Test()>
    Public Sub PlainStringGenerator_Test()
        Dim s As String = "HOLA"
        Dim target As New PlainStringGenerator(s)

        Dim i = 0

        For Each element In target
            Assert.That(element, [Is].EqualTo(s))
            i += 1
            If i = 20 Then Exit For
        Next
    End Sub


End Class
