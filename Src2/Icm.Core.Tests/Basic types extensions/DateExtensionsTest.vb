Imports System

<TestFixture(), Category("Icm")>
Public Class DateExtensionsTest

    Shared ReadOnly SeasonTestCases() As Object = {
        New TestCaseData(New Date(2010, 4, 2)).Returns(Seasons.Spring),
        New TestCaseData(New Date(2010, 12, 25)).Returns(Seasons.Winter),
        New TestCaseData(New Date(2010, 10, 27)).Returns(Seasons.Fall),
        New TestCaseData(New Date(2010, 7, 23)).Returns(Seasons.Summer)
    }

    <TestCaseSource("SeasonTestCases")>
    Public Function Season_Test(d As Date) As Seasons
        Return d.Season
    End Function

    Shared ReadOnly AddSTestCases() As Object = {
        New TestCaseData(New Date(2010, 4, 2), TimeSpan.FromDays(3)).Returns(New Date(2010, 4, 5)),
        New TestCaseData(Date.MaxValue, TimeSpan.FromDays(3)).Returns(Date.MaxValue),
        New TestCaseData(New Date(2010, 4, 2), TimeSpan.MaxValue).Returns(Date.MaxValue),
        New TestCaseData(New Date(2010, 4, 2), TimeSpan.FromDays(-3)).Throws(GetType(ArgumentException)),
        New TestCaseData(New Date(2010, 4, 2), TimeSpan.Zero).Returns(New Date(2010, 4, 2))
    }

    <TestCaseSource("AddSTestCases")>
    Public Function AddS_Test(d As Date, dur As TimeSpan) As Date
        Return d.AddS(dur)
    End Function

End Class

