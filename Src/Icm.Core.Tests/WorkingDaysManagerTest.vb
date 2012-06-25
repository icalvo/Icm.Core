Imports Icm

'''<summary>
'''This is a test class for WorkingDaysManagerTest and is intended
'''to contain all WorkingDaysManagerTest Unit Tests
'''</summary>
<TestFixture(), Category("Icm")>
Public Class WorkingDaysManagerTest

    '''<summary>
    '''A test for WeeklyHolidays
    '''</summary>
    <Test(), Category("Icm")>
    Public Sub WeeklyHolidaysTest()
        Dim target As New WorkingDaysManager({#1/1/2011#}, {DayOfWeek.Saturday, DayOfWeek.Sunday})
        Dim actual As IEnumerable(Of DayOfWeek)
        actual = target.WeeklyHolidays
        Assert.IsTrue(actual.Count = 2)
        Assert.IsTrue(actual.Contains(DayOfWeek.Saturday))
        Assert.IsTrue(actual.Contains(DayOfWeek.Sunday))
    End Sub

    '''<summary>
    '''A test for DayHolidays
    '''</summary>
    <Test(), Category("Icm")>
    <Ignore()>
    Public Sub DayHolidaysTest()
        Dim target As New WorkingDaysManager({#1/1/2011#}, {DayOfWeek.Saturday, DayOfWeek.Sunday})
        Dim actual As IEnumerable(Of Date)
        actual = target.DayHolidays
        Assert.Inconclusive()
    End Sub

    '''<summary>
    '''A test for PrevWorkingDay
    '''</summary>
    <Test(), Category("Icm")>
    Public Sub PrevWorkingDayTest()
        Dim target As New WorkingDaysManager
        Dim d As Date = New Date
        Dim expected As Date = New Date
        Dim actual As Date

        'Caso 1
        d = New Date(2010, 4, 21)
        expected = New Date(2010, 4, 21)
        actual = target.PrevWorkingDay(d)
        Assert.AreEqual(expected, actual)

        'Caso 1
        d = New Date(2010, 4, 25)
        expected = New Date(2010, 4, 23)
        actual = target.PrevWorkingDay(d)
        Assert.AreEqual(expected, actual)

    End Sub

    '''<summary>
    '''A test for NextWorkingDay
    '''</summary>
    <Test(), Category("Icm")>
    Public Sub NextWorkingDayTest()
        Dim target As New WorkingDaysManager
        Dim d As Date
        Dim expected As Date
        Dim actual As Date

        'Caso 1
        d = New Date(2010, 4, 25)
        expected = New Date(2010, 4, 26)
        actual = target.NextWorkingDay(d)
        Assert.AreEqual(expected, actual)

        'Caso 2
        d = New Date(2010, 4, 20)
        expected = New Date(2010, 4, 20)
        actual = target.NextWorkingDay(d)
        Assert.AreEqual(expected, actual)

    End Sub

    '''<summary>
    '''A test for IsWorking
    '''</summary>
    <Test(), Category("Icm")>
    Public Sub IsWorkingTest()
        Dim target As New WorkingDaysManager
        Dim d As Date
        Dim expected As Boolean = False
        Dim actual As Boolean

        'Caso 1
        d = New Date(2010, 4, 20)
        actual = target.IsWorking(d)
        expected = True
        Assert.AreEqual(expected, actual)

        'Caso 2
        d = New Date(2010, 4, 24)
        actual = target.IsWorking(d)
        expected = False
        Assert.AreEqual(expected, actual)

    End Sub

    '''<summary>
    '''A test for AddDays
    '''</summary>
    <Test(), Category("Icm")>
    Public Sub AddDaysTest()
        Dim target As New WorkingDaysManager
        Dim d As Date = New Date
        Dim n As Integer = 0
        Dim expected As Date = New Date
        Dim actual As Date

        'Caso 1
        n = 3
        expected = New Date(2010, 4, 23)
        d = New Date(2010, 4, 20)
        actual = target.AddDays(d, n)
        Assert.AreEqual(expected, actual)

        'Caso 2
        n = -5
        expected = New Date(2010, 4, 13)
        d = New Date(2010, 4, 20)
        actual = target.AddDays(d, n)
        Assert.AreEqual(expected, actual)

        'Caso 3
        n = 0
        expected = New Date(2010, 4, 20)
        d = New Date(2010, 4, 20)
        actual = target.AddDays(d, n)
        Assert.AreEqual(expected, actual)

    End Sub

End Class
