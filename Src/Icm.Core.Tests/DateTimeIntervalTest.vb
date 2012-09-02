<TestFixture>
Public Class DateTimeIntervalTest

    Dim Constructor1TestCases As Object() = {
        New TestCaseData(New Date(2010, 4, 5)),
        New TestCaseData(Date.MaxValue).Throws(GetType(ArgumentOutOfRangeException)),
        New TestCaseData(Date.MinValue)
    }
    <TestCaseSource("Constructor1TestCases")>
    Public Sub Constructor1_Test(point As Date)
        Dim dti As New DateTimeInterval(point)

        Assert.That(dti.Start, [Is].EqualTo(point))
        Assert.That(dti.End, [Is].EqualTo(point))
    End Sub

    Dim Constructor2TestCases As Object() = {
        New TestCaseData(New Date(2010, 4, 5), New Date(2010, 4, 7)),
        New TestCaseData(New Date(2010, 4, 5), New Date(2010, 4, 5)),
        New TestCaseData(New Date(2010, 4, 5), New Date(2010, 4, 3)).Throws(GetType(ArgumentException)),
        New TestCaseData(Date.MaxValue, Date.MaxValue).Throws(GetType(ArgumentOutOfRangeException)),
        New TestCaseData(Date.MinValue, New Date(2010, 4, 7))
    }
    <TestCaseSource("Constructor2TestCases")>
    Public Sub Constructor2_Test(startInt As Date, endInt As Date)
        Dim dti As New DateTimeInterval(startInt, endInt)

        Assert.That(dti.Start, [Is].EqualTo(startInt))
        Assert.That(dti.End, [Is].EqualTo(endInt))
    End Sub

    Dim Constructor3TestCases As Object() = {
        New TestCaseData(New Date(2010, 4, 5), TimeSpan.FromDays(2), New Date(2010, 4, 7)),
        New TestCaseData(New Date(2010, 4, 5), TimeSpan.Zero, New Date(2010, 4, 5)),
        New TestCaseData(New Date(2010, 4, 5), TimeSpan.FromDays(-2), New Date(2010, 4, 3)).Throws(GetType(ArgumentOutOfRangeException)),
        New TestCaseData(Date.MaxValue, TimeSpan.Zero, Date.MaxValue).Throws(GetType(ArgumentOutOfRangeException)),
        New TestCaseData(Date.MinValue, TimeSpan.FromDays(2), New Date(1, 1, 3))
    }
    <TestCaseSource("Constructor3TestCases")>
    Public Sub Constructor3_Test(startInt As Date, ts As TimeSpan, expectedEnd As Date)
        Dim dti As New DateTimeInterval(startInt, ts)

        Assert.That(dti.Start, [Is].EqualTo(startInt))
        Assert.That(dti.End, [Is].EqualTo(expectedEnd))
    End Sub

End Class
