Imports Icm.Collections.Generic.StructKeyClassValue

<TestFixture>
Public Class BaseSortedCollectionTest

    Private Shared Function GetNewCollection() As BaseSortedCollection(Of Long, String)
        Return New MemorySortedCollection(Of Long, String)(New LongTotalOrder)
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
        coll.Add(12, "value1")
        coll.Add(14, "value2")
        Dim actual = coll.GetFreeKey(13)

        Assert.That(actual.HasValue)
        Assert.AreEqual(expected, actual)
    End Sub

    <Test>
    Public Sub GetFreeKey_IfKeyPresent_ReturnsNextAvailableKey()
        Dim coll = GetNewCollection()
        Dim expected As Long = 15

        ' Act
        coll.Add(12, "value1")
        coll.Add(13, "value1")
        coll.Add(14, "value2")
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
        coll.Add(Long.MaxValue, "value1")
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
            "-> 12 value1" & vbCrLf &
            "NC 14 ---" & vbCrLf

        ' Act
        coll.Add(12, "value1")
        Dim actual = coll.ToString(12, 14)

        Assert.AreEqual(expected, actual)
    End Sub

    <Test()>
    Public Sub ToString_OneElementInTheMiddle_AppearsInTheMiddle()
        Dim coll = GetNewCollection()
        Dim expected As String =
            "NC 12 ---" & vbCrLf &
            "-> 13 value1" & vbCrLf &
            "NC 14 ---" & vbCrLf

        ' Act
        coll.Add(13, "value1")
        Dim actual = coll.ToString(12, 14)

        Assert.AreEqual(expected, actual)
    End Sub


    <Test()>
    Public Sub ToString_OneElementInTheEnd_AppearsInTheEnd()
        Dim coll = GetNewCollection()
        Dim expected As String =
            "NC 12 ---" & vbCrLf &
            "-> 14 value1" & vbCrLf

        ' Act
        coll.Add(14, "value1")
        Dim actual = coll.ToString(12, 14)

        Assert.AreEqual(expected, actual)
    End Sub
End Class
