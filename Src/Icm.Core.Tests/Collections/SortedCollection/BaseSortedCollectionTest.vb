Imports Icm.Collections.Generic.StructKeyClassValue

<TestFixture>
Public Class BaseSortedCollectionTest

    Private Shared Function GetNewCollection() As BaseSortedCollection(Of Long, String)
        Return New MemorySortedCollection(Of Long, String)(New LongTotalOrder)
    End Function

    <Test>
    Public Sub GetFreeKey_ReturnsSameKeyWithEmptyCollection()
        Dim coll = GetNewCollection()
        Dim expected As Long = 13

        Dim actual = coll.GetFreeKey(13)

        Assert.IsTrue(actual.HasValue)
        Assert.AreEqual(expected, actual)
    End Sub

    <Test>
    Public Sub GetFreeKey_ReturnsSameKeyIfNotPresent()
        Dim coll = GetNewCollection()
        Dim expected As Long = 13

        ' Act
        coll.Add(12, "value1")
        coll.Add(14, "value2")
        Dim actual = coll.GetFreeKey(13)

        Assert.IsTrue(actual.HasValue)
        Assert.AreEqual(expected, actual)
    End Sub

    <Test>
    Public Sub GetFreeKey_ReturnsNextAvailableKeyIfPresent()
        Dim coll = GetNewCollection()
        Dim expected As Long = 15

        ' Act
        coll.Add(12, "value1")
        coll.Add(13, "value1")
        coll.Add(14, "value2")
        Dim actual = coll.GetFreeKey(13)

        Assert.IsTrue(actual.HasValue)
        Assert.AreEqual(expected, actual)
    End Sub

    <Test>
    Public Sub GetFreeKey_ReturnsNothingIfRunsOutOfValues()
        Dim coll = GetNewCollection()
        Dim expected As Long? = Nothing

        ' Act
        coll.Add(Long.MaxValue, "value1")
        Dim actual = coll.GetFreeKey(Long.MaxValue)

        Assert.AreEqual(expected, actual)
    End Sub

    <Test>
    Public Sub ToString_EmptyCollection()
        Dim coll = GetNewCollection()
        Dim expected As String = ""

        ' Act
        'coll.Add(Long.MaxValue, "value1")
        Dim actual = coll.ToString(12, 14)

        Assert.AreEqual(expected, actual)
    End Sub

    <Test>
    Public Sub ToString_OneElement()
        Dim coll = GetNewCollection()
        Dim expected As String = Nothing

        ' Act
        'coll.Add(Long.MaxValue, "value1")
        Dim actual = coll.ToString

        Assert.AreEqual(expected, actual)
    End Sub

End Class
