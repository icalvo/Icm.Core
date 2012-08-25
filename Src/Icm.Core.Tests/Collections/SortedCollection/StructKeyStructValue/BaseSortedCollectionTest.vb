Imports Icm.Collections.Generic.StructKeyStructValue

<TestFixture>
Public Class StructKeyStructValue_BaseSortedCollectionTest

    Private Shared Function GetNewCollection() As BaseSortedCollection(Of Long, Char)
        Return New MemorySortedCollection(Of Long, Char)(New LongTotalOrder)
    End Function

    <Test>
    Public Sub GetFreeKey_WithEmptyCollection_ReturnsSameKey()
        Dim coll = GetNewCollection()
        Dim expected As Long = 13

        Dim actual = coll.GetFreeKey(13)

        Assert.That(actual.HasValue)
        Assert.AreEqual(expected, actual)
    End Sub

    <Test>
    Public Sub GetFreeKey_IfKeyNotPresent_ReturnsSameKey()
        Dim coll = GetNewCollection()
        Dim expected As Long = 13

        ' Act
        coll.Add(12, "a"c)
        coll.Add(14, "b"c)
        Dim actual = coll.GetFreeKey(13)

        Assert.That(actual.HasValue)
        Assert.AreEqual(expected, actual)
    End Sub

    <Test>
    Public Sub GetFreeKey_IfKeyPresent_ReturnsNextAvailableKey()
        Dim coll = GetNewCollection()
        Dim expected As Long = 15

        ' Act
        coll.Add(12, "a"c)
        coll.Add(13, "b"c)
        coll.Add(14, "c"c)
        Dim actual = coll.GetFreeKey(13)

        ' Assert
        Assert.That(actual.HasValue)
        Assert.AreEqual(expected, actual)
    End Sub

    <Test>
    Public Sub GetFreeKey_IfRunsOutOfValues_ReturnsNothing()
        Dim coll = GetNewCollection()
        Dim expected As Long? = Nothing

        ' Act
        coll.Add(Long.MaxValue, "a"c)
        Dim actual = coll.GetFreeKey(Long.MaxValue)

        Assert.AreEqual(expected, actual)
    End Sub

    <Test>
    Public Sub ToString_EmptyCollection()
        Dim coll = GetNewCollection()
        Dim expected As String =
            "NC 12 ---" & vbCrLf &
            "NC 14 ---" & vbCrLf

        ' Act
        Dim actual = coll.ToString(12, 14)

        ' Assert
        Assert.AreEqual(expected, actual)
    End Sub

    <Test()>
    Public Sub ToString_OneElementAtStart_AppearsAtStart()
        Dim coll = GetNewCollection()
        Dim expected As String =
            "-> 12 a" & vbCrLf &
            "NC 14 ---" & vbCrLf

        ' Act
        coll.Add(12, "a"c)
        Dim actual = coll.ToString(12, 14)

        Assert.AreEqual(expected, actual)
    End Sub

    <Test()>
    Public Sub ToString_OneElementInTheMiddle_AppearsInTheMiddle()
        Dim coll = GetNewCollection()
        Dim expected As String =
            "NC 12 ---" & vbCrLf &
            "-> 13 a" & vbCrLf &
            "NC 14 ---" & vbCrLf

        ' Act
        coll.Add(13, "a"c)
        Dim actual = coll.ToString(12, 14)

        Assert.AreEqual(expected, actual)
    End Sub


    <Test()>
    Public Sub ToString_OneElementInTheEnd_AppearsInTheEnd()
        Dim coll = GetNewCollection()
        Dim expected As String =
            "NC 12 ---" & vbCrLf &
            "-> 14 a" & vbCrLf

        ' Act
        coll.Add(14, "a"c)
        Dim actual = coll.ToString(12, 14)

        Assert.AreEqual(expected, actual)
    End Sub
End Class
