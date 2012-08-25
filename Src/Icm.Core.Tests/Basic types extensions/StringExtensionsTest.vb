Imports Icm

'''<summary>
'''This is a test class for StringExtensions and is intended
'''to contain all StringExtensions Unit Tests
'''</summary>
<TestFixture(), Category("Icm")>
Public Class StringExtensionsTest

    <TestCase("bLioNNpPA", "BLioNNpPA")>
    <TestCase("BaMBu", "BaMBu")>
    <TestCase("áNGeL", "ÁNGeL")>
    <TestCase("78a", "78a")>
    <TestCase("a1", "A1")>
    <TestCase("", "")>
    <TestCase(Nothing, "")>
    Public Sub ToUpperFirstTest_BasicCase(s As String, expected As String)
        Dim actual As String
        actual = s.ToUpperFirst()
        Assert.AreEqual(expected, actual)
    End Sub

    <TestCase("aa", 3, "aaaaaa", Nothing)>
    <TestCase("", 3, "", Nothing)>
    <TestCase("aa", 0, "", Nothing)>
    <TestCase("aa", -5, Nothing, GetType(ArgumentOutOfRangeException))>
    <TestCase(Nothing, 3, Nothing, GetType(NullReferenceException))>
    Public Sub RepeatTest(s As String, count As Integer, expected As String, expectedException As Type)
        Dim actual As String

        If expectedException Is Nothing Then
            actual = s.Repeat(count)
            Assert.AreEqual(expected, actual)
        Else
            Assert.That(Sub() s.Repeat(count), Throws.TypeOf(expectedException))
        End If
    End Sub


    '''<summary>
    '''A test for Left
    '''</summary>
    <Test()>
    Public Sub LeftTest()
        Dim s As String = String.Empty
        Dim length As Integer = 0
        Dim expected As String = String.Empty
        Dim actual As String

        'Caso 1
        s = "Maria"
        length = 3
        expected = "Mar"
        actual = StringExtensions.Left(s, length)
        Assert.AreEqual(expected, actual)

        'Caso 2
        s = "M12a"
        length = 3
        expected = "M12"
        actual = StringExtensions.Left(s, length)
        Assert.AreEqual(expected, actual)

        'Caso 3
        s = "Viernes"
        length = 12
        expected = ""
        Try
            actual = StringExtensions.Left(s, length)
            Assert.Fail("Should throw ArgumentOutOfRangeException")
        Catch ex As ArgumentOutOfRangeException

        Catch ex As Exception
            Assert.Fail("Should throw ArgumentOutOfRangeException")
        End Try

        'Caso 4
        s = "Maria"
        length = -2
        expected = "Mar"
        Try
            actual = StringExtensions.Left(s, length)
            Assert.Fail("Should throw ArgumentOutOfRangeException")
        Catch ex As ArgumentOutOfRangeException

        Catch ex As Exception
            Assert.Fail("Should throw ArgumentOutOfRangeException")
        End Try

    End Sub

    '''<summary>
    '''A test for Med
    '''</summary>
    <Test()>
    Public Sub MedTest()
        Dim s As String = String.Empty
        Dim startIdx As Integer = 0
        Dim endIdx As Integer = 0
        Dim expected As String = String.Empty
        Dim actual As String

        'Caso 1
        s = "ViernesLunes"
        startIdx = 2
        endIdx = 8
        expected = "ernesLu"
        actual = StringExtensions.Med(s, startIdx, endIdx)
        Assert.AreEqual(expected, actual)

        'Caso 2
        s = "ViernesLunes"
        startIdx = 0
        endIdx = 0
        expected = "V"
        actual = StringExtensions.Med(s, startIdx, endIdx)
        Assert.AreEqual(expected, actual)

        'Caso 3
        s = "ViernesLunes"
        startIdx = -4
        endIdx = -6

        Try
            actual = StringExtensions.Med(s, startIdx, endIdx)
            Assert.Fail("Should throw ArgumentOutOfRangeException")
        Catch ex As ArgumentOutOfRangeException

        Catch ex As Exception
            Assert.Fail("Should throw ArgumentOutOfRangeException")
        End Try



    End Sub

    '''<summary>
    '''A test for SkipBoth
    '''</summary>
    <Test()>
    Public Sub SkipBothTest()
        Dim s As String = String.Empty '
        Dim startLength As Integer = 0 ' 
        Dim endLength As Integer = 0 '
        Dim expected As String = String.Empty
        Dim actual As String

        'Caso 1
        s = "LunesMartes"
        startLength = 1
        endLength = 3
        expected = "unesMar"
        actual = StringExtensions.SkipBoth(s, startLength, endLength)
        Assert.AreEqual(expected, actual)

        'Caso 2
        s = "LunesMartes"
        startLength = -1
        endLength = -5
        expected = ""

        Try
            actual = StringExtensions.SkipBoth(s, startLength, endLength)
            Assert.Fail("Should throw ArgumentOutOfRangeException")
        Catch ex As ArgumentOutOfRangeException

        Catch ex As Exception
            Assert.Fail("Should throw ArgumentOutOfRangeException")
        End Try

        'Caso 3
        s = "LunesMartes"
        startLength = 0
        endLength = 0
        expected = "LunesMartes"
        actual = StringExtensions.SkipBoth(s, startLength, endLength)
        Assert.AreEqual(expected, actual)

    End Sub

    '''<summary>
    '''A test for Right
    '''</summary>
    <Test()>
    Public Sub RightTest()
        Dim s As String = String.Empty
        Dim length As Integer = 0
        Dim expected As String = String.Empty
        Dim actual As String

        'Caso 1
        s = "Maria"
        length = 3
        expected = "ria"
        actual = StringExtensions.Right(s, length)
        Assert.AreEqual(expected, actual)

        'Caso 2
        s = "M12a"
        length = 3
        expected = "12a"
        actual = StringExtensions.Right(s, length)
        Assert.AreEqual(expected, actual)

        'Caso 3
        s = "Viernes"
        length = 12
        expected = ""
        Try
            actual = StringExtensions.Right(s, length)
            Assert.Fail("Should throw ArgumentOutOfRangeException")
        Catch ex As ArgumentOutOfRangeException

        Catch ex As Exception
            Assert.Fail("Should throw ArgumentOutOfRangeException")
        End Try

        'Caso 4
        s = "Maria"
        length = -2
        expected = "Mar"
        Try
            actual = StringExtensions.Right(s, length)
            Assert.Fail("Should throw ArgumentOutOfRangeException")
        Catch ex As ArgumentOutOfRangeException

        Catch ex As Exception
            Assert.Fail("Should throw ArgumentOutOfRangeException")
        End Try
    End Sub

    '''<summary>
    '''A test for SurroundedBy
    '''</summary>
    <Test()>
    Public Sub SurroundedByTest()
        Dim s As String = String.Empty '
        Dim startS As String = String.Empty
        Dim endS As String = String.Empty
        Dim expected As Boolean = False ' 
        Dim actual As Boolean

        'Caso1
        s = "LaCasaBonita"
        startS = "La"
        endS = "ta"
        expected = True

        actual = StringExtensions.SurroundedBy(s, startS, endS)
        Assert.AreEqual(expected, actual)


        'Caso 2
        s = "ángela"
        startS = "án"
        endS = "la"
        expected = True

        actual = StringExtensions.SurroundedBy(s, startS, endS)
        Assert.AreEqual(expected, actual)

        'Caso3
        s = "Concierto"
        startS = "conc"
        endS = "a"
        expected = False

        actual = StringExtensions.SurroundedBy(s, startS, endS)
        Assert.AreEqual(expected, actual)



    End Sub
End Class
