Imports System.Collections.Generic
Imports Icm.Collections



<Category("Icm")>
<TestFixture()>
Public Class IListExtensionsTest

    Class EmptyClass

    End Class

    Shared InitializeTestCases() As Object = {
        New Object() {New List(Of EmptyClass), 5, 5}
    }

    <TestCase({1, 2, 3}, 5, 5)>
    <TestCase({1, 2, 3}, -2, 0)>
    <TestCase({1, 2, 3}, 0, 0)>
    <TestCase(New Integer() {}, 5, 5)>
    <TestCase(New Integer() {}, -2, 0)>
    <TestCase(New Integer() {}, 0, 0)>
    Public Sub Initialize_NormalTestInteger(data() As Integer, providedCount As Integer, expectedCount As Integer)

        Dim originalList As New List(Of Integer)(data)
        originalList.Initialize(providedCount)
        Assert.That(originalList.Count, [Is].EqualTo(expectedCount))
        Assert.That(originalList.All(Function(e) e = 0))
    End Sub

    <TestCaseSource("InitializeTestCases")>
    Public Sub Initialize_NormalTestEmptyClass(originalList As List(Of EmptyClass), providedCount As Integer, expectedCount As Integer)
        originalList.Initialize(providedCount)
        Assert.That(expectedCount, [Is].EqualTo(originalList.Count))
        Assert.That(originalList.All(Function(e) e IsNot Nothing))
    End Sub

    <Test>
    Public Sub InitializeNew_WithNull_ThrowsNullReferenceException()
        Dim originalList As List(Of Integer) = Nothing

        Assert.That(Sub() originalList.Initialize(5), Throws.TypeOf(Of NullReferenceException))
    End Sub

    <TestCase({1, 3, 5, 5, 8, 10}, -3, -1)>
    <TestCase({1, 3, 5, 5, 8, 10}, 0, -1)>
    <TestCase({1, 3, 5, 5, 8, 10}, 1, 0)>
    <TestCase({1, 3, 5, 5, 8, 10}, 4, -3)>
    <TestCase({1, 3, 5, 5, 8, 10}, 5, 2)>
    <TestCase({1, 3, 5, 5, 8, 10}, 10, 5)>
    <TestCase({1, 3, 5, 5, 8, 10}, 20, -7)>
    Public Sub Search1_NormalTest(data() As Integer, searchedElement As Integer, expectedIndex As Integer)
        Dim list As IList(Of Integer) = New List(Of Integer)(data)

        Dim actual = list.Search(searchedElement)
        Assert.That(actual, [Is].EqualTo(expectedIndex))
    End Sub


    <TestCase({1, 3, 5, 5, 8, 10}, 0, 6, -3, -1)>
    <TestCase({1, 3, 5, 5, 8, 10}, 0, 6, 0, -1)>
    <TestCase({1, 3, 5, 5, 8, 10}, 0, 6, 1, 0)>
    <TestCase({1, 3, 5, 5, 8, 10}, 0, 6, 4, -3)>
    <TestCase({1, 3, 5, 5, 8, 10}, 0, 6, 5, 2)>
    <TestCase({1, 3, 5, 5, 8, 10}, 0, 6, 10, 5)>
    <TestCase({1, 3, 5, 5, 8, 10}, 0, 6, 20, -7)>
    Public Sub Search3_NormalTest(data() As Integer, index As Integer, length As Integer, searchedElement As Integer, expectedIndex As Integer)
        Dim list As IList(Of Integer) = New List(Of Integer)(data)

        Dim actual = list.Search(index, length, searchedElement)
        Assert.That(actual, [Is].EqualTo(expectedIndex))
    End Sub


End Class
