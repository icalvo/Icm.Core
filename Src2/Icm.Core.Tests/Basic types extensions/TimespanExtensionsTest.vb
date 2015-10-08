Imports System

<TestFixture(), Category("Icm")>
Public Class TimespanExtensionsTest

    Shared ReadOnly DividedByTestCases As Object() = {
        New TestCaseData(TimeSpan.FromHours(3), 3).Returns(TimeSpan.FromHours(1)),
        New TestCaseData(TimeSpan.FromHours(-3), 3).Returns(TimeSpan.FromHours(-1)),
        New TestCaseData(TimeSpan.FromHours(3), -3).Returns(TimeSpan.FromHours(-1)),
        New TestCaseData(TimeSpan.Zero, 3).Returns(TimeSpan.Zero),
        New TestCaseData(TimeSpan.FromHours(3), 0).Throws(GetType(OverflowException))
    }

    <TestCaseSource("DividedByTestCases")>
    Public Function DividedBy_Test(span As TimeSpan, divisor As Single) As TimeSpan
        Return span.DividedBy(divisor)
    End Function

    Shared ReadOnly IsZeroTestCases As Object() = {
        New TestCaseData(TimeSpan.FromHours(3)).Returns(False),
        New TestCaseData(TimeSpan.FromHours(-3)).Returns(False),
        New TestCaseData(TimeSpan.Zero).Returns(True)
    }

    <TestCaseSource("IsZeroTestCases")>
    Public Function IsZero_Test(target As TimeSpan) As Boolean
        Return target.IsZero()
    End Function
    
    Shared ReadOnly IsNotZeroTestCases As Object() = {
        New TestCaseData(TimeSpan.FromHours(3)).Returns(True),
        New TestCaseData(TimeSpan.FromHours(-3)).Returns(True),
        New TestCaseData(TimeSpan.Zero).Returns(False)
    }

    <TestCaseSource("IsNotZeroTestCases")>
    Public Function IsNotZero_Test(target As TimeSpan) As Boolean
        Return target.IsNotZero()
    End Function

    Shared ReadOnly ToAbbrevTestCases As Object() = {
        New TestCaseData(New TimeSpan(2, 14, 2)).Returns("2h14'2''"),
        New TestCaseData(New TimeSpan(0, 0, 2)).Returns("2''"),
        New TestCaseData(TimeSpan.Zero).Returns("0"),
        New TestCaseData(New TimeSpan(-5, 14, 18)).Returns("-4h45'42''")
    }

    <TestCaseSource("ToAbbrevTestCases")>
    Public Function ToAbbrev_Test(target As TimeSpan) As String
        Return target.ToAbbrev
    End Function

    Shared ReadOnly ToHHmmTestCases As Object() = {
        New TestCaseData(New TimeSpan(2, 14, 18)).Returns("02:14"),
        New TestCaseData(New TimeSpan(0, 0, 2)).Returns("00:00"),
        New TestCaseData(TimeSpan.Zero).Returns("00:00"),
        New TestCaseData(New TimeSpan(-5, 14, 18)).Returns("-04:45")
    }

    <TestCaseSource("ToHHmmTestCases")>
    Public Function ToHHmm_Test(target As TimeSpan) As String
        Return target.ToHHmm
    End Function

    Shared ReadOnly ToHHmmssTestCases As Object() = {
        New TestCaseData(New TimeSpan(2, 14, 18)).Returns("02:14:18"),
        New TestCaseData(New TimeSpan(0, 0, 2)).Returns("00:00:02"),
        New TestCaseData(TimeSpan.Zero).Returns("00:00:00"),
        New TestCaseData(New TimeSpan(-5, 14, 18)).Returns("-04:45:42")
    }

    <TestCaseSource("ToHHmmssTestCases")>
    Public Function ToHHmmss_Test(target As TimeSpan) As String
        Return target.ToHHmmss
    End Function

    Shared ReadOnly TommsstttTestCases As Object() = {
        New TestCaseData(New TimeSpan(2, 14, 18, 25)).Returns("3738:25.000"),
        New TestCaseData(New TimeSpan(0, 0, 2)).Returns("00:02.000"),
        New TestCaseData(TimeSpan.Zero).Returns("00:00.000"),
        New TestCaseData(New TimeSpan(-5, 14, 18)).Returns("-285:42.000")
    }

    <TestCaseSource("TommsstttTestCases")>
    Public Function Tommssttt_Test(target As TimeSpan) As String
        Return target.Tommssttt
    End Function

    Shared ReadOnly ToHHmmsstttTestCases As Object() = {
        New TestCaseData(New TimeSpan(2, 14, 18, 25)).Returns("62:18:25.000"),
        New TestCaseData(New TimeSpan(0, 0, 2)).Returns("00:00:02.000"),
        New TestCaseData(TimeSpan.Zero).Returns("00:00:00.000"),
        New TestCaseData(New TimeSpan(-5, 14, 18)).Returns("-04:45:42.000")
    }

    <TestCaseSource("ToHHmmsstttTestCases")>
    Public Function ToHHmmssttt_Test(target As TimeSpan) As String
        Return target.ToHHmmssttt
    End Function

    Shared ReadOnly TotalMicrosecondsTestCases As Object() = {
        New TestCaseData(New TimeSpan(2, 14, 18, 25)).Returns(224305000000),
        New TestCaseData(New TimeSpan(0, 0, 0, 2)).Returns(2000000),
        New TestCaseData(TimeSpan.Zero).Returns(0),
        New TestCaseData(New TimeSpan(-5, 14, 18)).Returns(-17142000000)
    }

    <TestCaseSource("TotalMicrosecondsTestCases")>
    Public Function TotalMicroseconds_Test(target As TimeSpan) As Long
        Return target.TotalMicroseconds
    End Function

End Class
