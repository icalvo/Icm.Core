Imports System


<TestFixture(), Category("Icm")>
Public Class UndefinedValueExtensionsTest


End Class


'''<summary>
'''This is a test class for TimespanExtensionsTest and is intended
'''to contain all TimespanExtensionsTest Unit Tests
'''</summary>
<TestFixture(), Category("Icm")>
Public Class TimespanExtensionsTest

    '''<summary>
    '''A test for DividedBy
    '''</summary>
    <Test(), Category("Icm")>
    Public Sub DividedByTest()
        Dim t As TimeSpan = New TimeSpan(3, 0, 0)
        Dim divisor As Single = 3
        Dim expected As TimeSpan = New TimeSpan(CLng(t.Ticks / 3))
        Dim actual As TimeSpan

        'bassic case
        actual = TimespanExtensions.DividedBy(t, divisor)
        Assert.AreEqual(expected, actual)

        'Divisor = 0
        divisor = 0
        Try
            actual = TimespanExtensions.DividedBy(t, divisor)
            Assert.Fail("Should throw OverFlowException")
        Catch ex As OverflowException

        Catch ex As Exception
            Assert.Fail("Should throw OverFlowException")
        End Try

    End Sub

    '''<summary>
    '''A test for IsZero
    '''</summary>
    <Test(), Category("Icm")>
    Public Sub IsZeroTest()

        Dim t As TimeSpan = New TimeSpan(3, 0, 0)
        Dim expected As Boolean = False
        Dim actual As Boolean
        actual = TimespanExtensions.IsZero(t)
        Assert.AreEqual(expected, actual)

        'Caso 2 (la fecha es igual a 0)
        Dim d As TimeSpan = New TimeSpan(0, 0, 0)
        expected = True
        actual = TimespanExtensions.IsZero(d)
        Assert.AreEqual(expected, actual)
    End Sub

    '''<summary>
    '''A test for IsZero
    '''</summary>
    <Test(), Category("Icm")>
    Public Sub IsNotZerotest()

        Dim t As TimeSpan = New TimeSpan(3, 0, 0)
        Dim expected As Boolean = False
        Dim actual As Boolean
        actual = TimespanExtensions.IsZero(t)
        Assert.AreEqual(expected, actual)

    End Sub


    '''<summary>
    '''A test for ToAbrev
    '''</summary>
    <Test(), Category("Icm")>
    Public Sub ToAbrevTest()
        Dim ts As TimeSpan = New TimeSpan()
        Dim expected As String = String.Empty
        Dim actual As String

        'Caso 1
        ts = New TimeSpan(2, 14, 2)
        expected = "2h14'2''"
        actual = TimespanExtensions.ToAbbrev(ts)
        Assert.AreEqual(expected, actual)

        'Caso 1
        ts = New TimeSpan(0, 0, 2)
        expected = "2''"
        actual = TimespanExtensions.ToAbbrev(ts)
        Assert.AreEqual(expected, actual)

        'Caso 3
        ts = New TimeSpan(-5, 14, 18)
        expected = "-4h-45'-42''"
        actual = TimespanExtensions.ToAbbrev(ts)
        Assert.AreEqual(expected, actual)

    End Sub

    '''<summary>
    '''A test for ToHHmm
    '''</summary>
    <Test(), Category("Icm")>
    Public Sub ToHHmmTest()
        Dim ts As TimeSpan = New TimeSpan()
        Dim expected As String = String.Empty
        Dim actual As String

        'Caso 1
        ts = New TimeSpan(2, 14, 2)
        expected = "02:14"
        actual = TimespanExtensions.ToHHmm(ts)
        Assert.AreEqual(expected, actual)

        'Caso 2
        ts = New TimeSpan(-5, 14, 18)
        expected = "-04:45"
        actual = TimespanExtensions.ToHHmm(ts)
        Assert.AreEqual(expected, actual)

    End Sub

    '''<summary>
    '''A test for ToHHmmss
    '''</summary>
    <Test(), Category("Icm")>
    Public Sub ToHHmmssTest()
        Dim ts As TimeSpan
        Dim expected As String = String.Empty
        Dim actual As String

        'Caso 1
        ts = New TimeSpan(2, 14, 18)
        expected = "02:14:18"
        actual = TimespanExtensions.ToHHmmss(ts)
        Assert.AreEqual(expected, actual)

        'Caso 2
        ts = New TimeSpan(-5, 14, 18)
        expected = "-04:45:42"
        actual = TimespanExtensions.ToHHmmss(ts)
        Assert.AreEqual(expected, actual)

        'Caso 3
        ts = New TimeSpan(0, 0, 0)
        expected = "00:00:00"
        actual = TimespanExtensions.ToHHmmss(ts)
        Assert.AreEqual(expected, actual)

    End Sub

    '''<summary>
    '''A test for ToMicroseconds
    '''</summary>
    <Test(), Category("Icm")>
    Public Sub ToMicrosecondsTest()
        Dim ts As TimeSpan = New TimeSpan()
        Dim expected As Long = 0
        Dim actual As Long

        'Caso 1
        ts = New TimeSpan(2, 14, 18, 25)
        expected = 224305000000
        actual = TimespanExtensions.ToMicroseconds(ts)
        Assert.AreEqual(expected, actual)

        'Caso 2
        ts = New TimeSpan(0, 0, 0, 2)
        expected = 2000000
        actual = TimespanExtensions.ToMicroseconds(ts)
        Assert.AreEqual(expected, actual)

        'Caso 2
        ts = New TimeSpan(-5, 14, 18)
        expected = -17142000000
        actual = TimespanExtensions.ToMicroseconds(ts)
        Assert.AreEqual(expected, actual)

    End Sub

    '''<summary>
    '''A test for ToMillisecondsAndOne
    '''</summary>
    <Test(), Category("Icm")>
    Public Sub ToMillisecondsAndOneTest()
        Dim ts As TimeSpan = New TimeSpan()
        Dim expected As String = String.Empty
        Dim actual As String

        'Caso 1
        ts = New TimeSpan(2, 14, 18, 25)
        expected = "224305000,0"
        actual = TimespanExtensions.ToMillisecondsAndOne(ts)
        Assert.AreEqual(expected, actual)

        'Caso 2
        ts = New TimeSpan(0, 0, 0, 2)
        expected = "2000,0"
        actual = TimespanExtensions.ToMillisecondsAndOne(ts)
        Assert.AreEqual(expected, actual)

        'Caso 3
        ts = New TimeSpan(-5, 14, 18)
        expected = "-17142000,0"
        actual = TimespanExtensions.ToMillisecondsAndOne(ts)
        Assert.AreEqual(expected, actual)
    End Sub

    '''<summary>
    '''A test for Tommssttt
    '''</summary>
    <Test(), Category("Icm")>
    Public Sub TommsstttTest()
        Dim ts As TimeSpan = New TimeSpan()
        Dim expected As String = String.Empty
        Dim actual As String


        'Caso 1
        ts = New TimeSpan(2, 14, 18, 25)
        expected = "3738:25.000"
        actual = TimespanExtensions.Tommssttt(ts)
        Assert.AreEqual(expected, actual)

        'Caso 2
        ts = New TimeSpan(0, 0, 0, 2)
        expected = "00:02.000"
        actual = TimespanExtensions.Tommssttt(ts)
        Assert.AreEqual(expected, actual)

        'Caso 3
        ts = New TimeSpan(-5, 14, 18)
        expected = "-285:42.000"
        actual = TimespanExtensions.Tommssttt(ts)
        Assert.AreEqual(expected, actual)
    End Sub
End Class
