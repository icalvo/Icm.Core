Imports System.Collections.Generic
Imports Icm.Collections

<Category("Icm")>
<TestFixture()>
Public Class IListExtensionsTest

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
        Assert.IsTrue(originalList.All(Function(e) e = 0))
    End Sub

    '<TestCase({EmptyClass.GetNew}, 5, 5)>
    '<TestCase({1, 2, 3}, -2, 0)>
    '<TestCase({1, 2, 3}, 0, 0)>
    '<TestCase(New Integer() {}, 5, 5)>
    '<TestCase(New Integer() {}, -2, 0)>
    '<TestCase(New Integer() {}, 0, 0)>
    'Public Sub Initialize_NormalTestEmptyClass(data() As EmptyClass, providedCount As Integer, expectedCount As Integer)
    '    Dim originalList As New List(Of Integer)(data)
    '    originalList.Initialize(providedCount)
    '    Assert.That(expectedCount, [Is].EqualTo(originalList.Count))
    '    Assert.IsTrue(originalList.All(Function(e) e = 0))
    'End Sub

    <Test>
    Public Sub InitializeNew_WithNull_ThrowsNullReferenceException()
        Dim originalList As List(Of Integer) = Nothing
        Assert.That(Sub() originalList.Initialize(5), Throws.TypeOf(Of NullReferenceException))
    End Sub

    <TestCase({4, 7, 2, 2, 6, 10}, 6, 4)>
    <TestCase({4, 7, 2, 2, 6, 10}, 2, 3)>
    <TestCase({4, 7, 2, 2, 6, 10}, 1, -1)>
    <TestCase({4, 7, 2, 2, 6, 10}, 3, -1)>
    <TestCase({4, 7, 2, 2, 6, 10}, 20, -1)>
    Public Sub Search_NormalTest(data() As Integer, searchedElement As Integer, expectedIndex As Integer)
        Dim list As IList(Of Integer) = New List(Of Integer)(data)

        Dim actual = list.Search(searchedElement)
        Assert.That(actual, [Is].EqualTo(expectedIndex))
    End Sub

End Class
