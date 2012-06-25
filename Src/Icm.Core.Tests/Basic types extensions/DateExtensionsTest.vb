Imports System





'''<summary>
'''This is a test class for DateExtensionsTest and is intended
'''to contain all DateExtensionsTest Unit Tests
'''</summary>
<TestFixture(), Category("Icm")>
Public Class DateExtensionsTest


#Region "Additional test attributes"
    '
    'You can use the following additional attributes as you write your tests:
    '
    'Use ClassInitialize to run code before running the first test in the class
    '<ClassInitialize()>  _
    'Public Shared Sub MyClassInitialize(ByVal testContext As TestContext)
    'End Sub
    '
    'Use ClassCleanup to run code after all tests in a class have run
    '<ClassCleanup()>  _
    'Public Shared Sub MyClassCleanup()
    'End Sub
    '
    'Use TestInitialize to run code before running each test
    '<TestInitialize()>  _
    'Public Sub MyTestInitialize()
    'End Sub
    '
    'Use TestCleanup to run code after each test has run
    '<TestCleanup()>  _
    'Public Sub MyTestCleanup()
    'End Sub
    '
#End Region


    '''<summary>
    '''A test for Season
    '''</summary>
    <Test(), Category("Icm")>
    Public Sub SeasonTest()
        Dim d As Date
        Dim expected As DateExtensions.Seasons = DateExtensions.Seasons.Spring
        Dim actual As DateExtensions.Seasons

        'When date is spring
        d = New Date(2010, 4, 2)
        actual = DateExtensions.Season(d)
        Assert.AreEqual(expected, actual)

        'When date is winter
        d = CDate("2010-12-28")
        expected = DateExtensions.Seasons.Winter
        actual = DateExtensions.Season(d)
        Assert.AreEqual(expected, actual)

        'when date is fall
        d = New Date(2010, 10, 27)
        expected = DateExtensions.Seasons.Fall
        actual = DateExtensions.Season(d)
        Assert.AreEqual(expected, actual)

        'when date is summer
        d = New Date(2010, 7, 27)
        expected = DateExtensions.Seasons.Summer
        actual = DateExtensions.Season(d)
        Assert.AreEqual(expected, actual)

    End Sub
End Class

