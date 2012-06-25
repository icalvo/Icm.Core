Imports System


Imports Icm.Collections



'''<summary>
'''This is a test class for ArrayExtensionsTest and is intended
'''to contain all ArrayExtensionsTest Unit Tests
'''</summary>
<TestFixture(), Category("Icm")>
Public Class ArrayExtensionsTest

    '''<summary>
    '''A test for MultiGetRow
    '''</summary>
    Public Sub MultiGetRowTestString()

        Dim a(,) As String = {{"maria", "rafa"}, {"juan", "pepe"}}
        Dim iteratingDimension As Integer = 0
        Dim fixedDimensionValues() As Integer = Nothing
        Dim expected() As String
        Dim actual() As String

        'Caso 1
        fixedDimensionValues = {1}
        expected = {"rafa", "pepe"}
        actual = ArrayExtensions.MultiGetRow(Of String)(a, iteratingDimension, fixedDimensionValues)
        Assert.IsTrue(expected.SequenceEqual(actual))


    End Sub

    <Test(), Category("Icm")>
    Public Sub MultiGetRow_ExtremeCases()

        Dim a(,) As String
        Dim iteratingDimension As Integer = 0
        Dim fixedDimensionValues() As Integer = Nothing
        Dim expected() As String
        Dim actual() As String

        'Caso 1
        'fixedDimensionValues out of range case
        Try
            a = {{"", ""}, {"", ""}}
            iteratingDimension = 0
            fixedDimensionValues = {2}
            expected = {"", ""}
            actual = ArrayExtensions.MultiGetRow(Of String)(a, iteratingDimension, fixedDimensionValues)
            Assert.Fail("Should not launch when you enter exception index out of range")
        Catch ex As IndexOutOfRangeException
        Catch ex As Exception
            Assert.Fail("Should throw IndexOutOfRangeException")

        End Try

        'Caso 2
        'iteratingDimension es numero negativo
        Try
            a = {{"", ""}, {"", ""}}
            iteratingDimension = -1
            fixedDimensionValues = {1}
            expected = {"", ""}
            actual = ArrayExtensions.MultiGetRow(Of String)(a, iteratingDimension, fixedDimensionValues)
            Assert.Fail("Should not accept negative number")
        Catch ex As IndexOutOfRangeException
        Catch ex As Exception
            Assert.Fail("Should throw IndexOutOfRangeException")
        End Try

        'Caso 3
        'FixedDimensionValues es numero negativo
        Try
            a = {{"", ""}, {"", ""}}
            iteratingDimension = 0
            fixedDimensionValues = {-1}
            expected = {"", ""}
            actual = ArrayExtensions.MultiGetRow(Of String)(a, iteratingDimension, fixedDimensionValues)
            Assert.Fail("Should not accept negative number")
        Catch ex As IndexOutOfRangeException
        Catch ex As Exception
            Assert.Fail("Should throw IndexOutOfRangeException")
        End Try

        'Caso 4
        'FixedDimensionValues out of range case
        Try
            a = {{"", ""}, {"", ""}}
            iteratingDimension = 5
            fixedDimensionValues = {1}
            expected = {"", ""}
            actual = ArrayExtensions.MultiGetRow(Of String)(a, iteratingDimension, fixedDimensionValues)
            Assert.Fail("Should not launch when you enter exception index out of range")
        Catch ex As IndexOutOfRangeException
        Catch ex As Exception
            Assert.Fail("Should throw IndexOutOfRangeException")
        End Try

    End Sub

    '''<summary>
    '''A test for MultiGetRow
    '''</summary>
    Public Sub MultiGetRowTestInteger()

        Dim a(,) As Integer = {{1, 5}, {2, 7}, {3, 9}}
        Dim iteratingDimension As Integer = 1
        Dim fixedDimensionValues() As Integer = Nothing
        Dim expected() As Integer
        Dim actual() As Integer

        'Caso 1
        fixedDimensionValues = {1}
        expected = {2, 7}
        actual = ArrayExtensions.MultiGetRow(Of Integer)(a, iteratingDimension, fixedDimensionValues)
        Assert.IsTrue(expected.SequenceEqual(actual))

    End Sub

    <Test(), Category("Icm")>
    Public Sub MultiGetRowTest_BasicCases()
        MultiGetRowTestString()
        MultiGetRowTestInteger()
    End Sub
End Class
