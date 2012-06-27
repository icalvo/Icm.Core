Imports Icm

'''<summary>
'''This is a test class for StringExtensionsTest and is intended
'''to contain all StringExtensionsTest Unit Tests
'''</summary>
<TestFixture(), Category("Icm")>
Public Class StringExtensionsTest

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
    '''A test for ToUpperFirst
    '''</summary>
    <Test(), Category("Icm")>
    Public Sub ToUpperFirstTest_BasicCase()

        'Case 1
        Dim s As String = "bLioNNpPA"
        Dim expected As String = "BLioNNpPA"
        Dim actual As String
        actual = s.ToUpperFirst()
        Assert.AreEqual(expected, actual)

        'Case 2
        s = "BaMBu"
        expected = "BaMBu"
        actual = s.ToUpperFirst()
        Assert.AreEqual(expected, actual)
    End Sub

    <Test(), Category("Icm")>
    Public Sub ToUpperFirstTest_ExtremeCases()

        'Case 1
        Dim s As String = "áNGeL"
        Dim expected As String = "ÁNGeL"
        Dim actual As String
        actual = s.ToUpperFirst()
        Assert.AreEqual(expected, actual)

        'Case 2
        s = "78a"
        expected = "78a"
        actual = s.ToUpperFirst()
        Assert.AreEqual(expected, actual)

        'Case 3
        s = "a1"
        expected = "A1"
        actual = s.ToUpperFirst()
        Assert.AreEqual(expected, actual)

    End Sub

    '''<summary>
    '''A test for Repeat
    '''</summary>
    <Test(), Category("Icm")>
    Public Sub RepeatTest()
        Dim s As String = String.Empty
        Dim count As Integer = 0
        Dim expected As String = String.Empty
        Dim actual As String

        'Case 1
        s = "aa"
        count = 3
        expected = "aaaaaa"
        actual = StringExtensions.Repeat(s, count)
        Assert.AreEqual(expected, actual)


        'Case 2
        s = ""
        count = 3
        expected = ""
        actual = StringExtensions.Repeat(s, count)
        Assert.AreEqual(expected, actual)

        ''Case 3
        s = "aaa"
        count = -3
        expected = ""
        Try
            actual = StringExtensions.Repeat(s, count)
            Assert.Fail("Should throw ArgumentOutOfRangeException")
        Catch ex As ArgumentOutOfRangeException

        Catch ex As Exception
            Assert.Fail("Should throw ArgumentOutOfRangeException")
        End Try

        'Case 4
        s = "aaaa"
        count = 0
        expected = ""
        actual = StringExtensions.Repeat(s, count)
        Assert.AreEqual(expected, actual)

    End Sub


    '''<summary>
    '''A test for Left
    '''</summary>
    <Test(), Category("Icm")>
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
    <Test(), Category("Icm")>
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
    <Test(), Category("Icm")>
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
    <Test(), Category("Icm")>
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
    <Test(), Category("Icm")>
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
