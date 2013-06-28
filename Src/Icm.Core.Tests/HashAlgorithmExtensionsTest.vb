Imports System.IO

Imports System.Security.Cryptography



Imports Icm.Security.Cryptography



'''<summary>
'''This is a test class for HasherTest and is intended
'''to contain all HasherTest Unit Tests
'''</summary>
<TestFixture(), Category("Icm")>
Public Class HashAlgorithmExtensionsTest


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
    '''A test for StringHash
    '''</summary>
    <Test()>
    Public Sub StringHashTest2()

        Dim target As New MD5CryptoServiceProvider
        Dim b() As Byte = {1}
        Dim expected As String = "55a54008ad1ba589aa210d2629c1df41"
        Dim actual As String
        actual = target.StringHash(b)
        Assert.AreEqual(expected, actual)

    End Sub


    '''<summary>
    '''A test for StringHash
    '''</summary>
    <Test()>
    Public Sub StringHashTest()

        'Caso1
        Dim target As New MD5CryptoServiceProvider
        Dim s As String = "hola"
        Dim expected As String = "4d186321c1a7f0f354b297e8914ab240"
        Dim actual As String
        actual = target.StringHash(s)
        Assert.AreEqual(expected, actual)

        'Caso 2
        s = ""
        expected = "d41d8cd98f00b204e9800998ecf8427e"
        actual = target.StringHash(s)
        Assert.AreEqual(expected, actual)

        'Caso3
        s = Nothing
        Try
            actual = target.StringHash(s)
            Assert.Fail("NullReferenceException: Empty string")
        Catch ex As NullReferenceException

        Catch ex As Exception
            Assert.Fail("NullReferenceException: Empty string")
        End Try
    End Sub

    '''<summary>
    '''A test for ByteToString
    '''</summary>
    <Test()>
    Public Sub ByteToStringTest()

        'Caso 1
        Dim target As New MD5CryptoServiceProvider
        Dim hash() As Byte = {}
        Dim expected As String = ""
        Dim actual As String
        actual = hash.ToHex
        Assert.AreEqual(expected, actual)

    End Sub



    '''<summary>
    '''A test for ByteHash
    '''</summary>
    <Test()>
    Public Sub ByteHashTest2()

        'Caso1
        Dim target As New MD5CryptoServiceProvider
        Dim s As String = "hola hola hola"
        Dim expected(15) As Byte
        Dim actual() As Byte
        actual = target.ComputeHash(s)
        Assert.AreEqual(expected.Length, actual.Length)

        'Caso2
        s = Nothing
        ReDim expected(14)
        Try
            actual = target.ComputeHash(s)
            Assert.Fail("NullReferenceException: Empty string")
        Catch ex As NullReferenceException

        Catch ex As Exception
            Assert.Fail("NullReferenceException: Empty string")
        End Try

    End Sub

End Class
