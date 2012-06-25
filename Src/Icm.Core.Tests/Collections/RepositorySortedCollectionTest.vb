Imports Icm.Collections.Generic
Imports Icm.Collections.Generic.StructKeyClassValue

<TestFixture()>
Public Class RepositorySortedCollectionTest

    Public InitialList As New SortedList(Of Date, String) From {
                {#1/1/2007#, "Item 1a"},
                {#2/1/2007#, "Item 1b"},
                {#1/1/2008#, "Item 2"},
                {#1/1/2009#, "Item 3"},
                {#1/1/2010#, "Item 4"},
                {#1/1/2012#, "Item 5"}
            }

    Public Class FakeRepo
        Inherits MemorySortedCollectionRepository(Of Date, String)

        Public Sub New(ByVal InitialList As SortedList(Of Date, String))
            For Each kvp In InitialList
                List.Add(kvp.Key, kvp.Value)
            Next
        End Sub


    End Class

    <Test(), Category("Icm")>
    Public Sub Constructor()
        ' ARRANGE
        Dim repo = New FakeRepo(InitialList)
        Dim sc As New RepositorySortedCollection(Of Date, String)(repo, New YearManager, 2, New DateTotalOrder)

        ' ACT

        ' ASSERT
        Assert.AreEqual("/", sc.BucketQueue)
        Assert.IsTrue(repo.List.SequenceEqual(InitialList))
    End Sub

    <Test(), Category("Icm")>
    Public Sub DoesNotGetBucketIfTheresNotDatesInBucket()

        ' ARRANGE
        Dim repo = New FakeRepo(InitialList)
        Dim sc As New RepositorySortedCollection(Of Date, String)(repo, New YearManager, 2, New DateTotalOrder)
        Dim result As Boolean

        ' ACT
        result = sc.ContainsKey(#1/5/2011#)
        ' ASSERT
        Assert.AreEqual("/", sc.BucketQueue)
        Assert.IsFalse(result)
        Assert.IsTrue(repo.List.SequenceEqual(InitialList))
    End Sub

    <Test(), Category("Icm")>
    Public Sub DoesGetBucketIfThereAreDatesInBucket()
        ' ARRANGE
        Dim repo = New FakeRepo(InitialList)
        Dim sc As New RepositorySortedCollection(Of Date, String)(repo, New YearManager, 2, New DateTotalOrder)
        Dim result As Boolean

        ' ACT
        result = sc.ContainsKey(#1/1/2007#)
        ' ASSERT
        Assert.AreEqual("/2007/", sc.BucketQueue)
        Assert.IsTrue(result)

    End Sub

    <Test(), Category("Icm")>
    Public Sub LastBucketAccessedGetsFirst()
        ' ARRANGE
        Dim repo = New FakeRepo(InitialList)
        Dim sc As New RepositorySortedCollection(Of Date, String)(repo, New YearManager, 3, New DateTotalOrder)
        Dim result As Boolean

        ' ACT
        result = sc.ContainsKey(#1/1/2009#) ' -> /2009/
        result = sc.ContainsKey(#1/1/2007#) ' -> /2007/2009/
        result = sc.ContainsKey(#1/5/2011#) ' -> /2007/2009/
        result = sc.ContainsKey(#1/1/2008#) ' -> /2008/2007/2009/
        result = sc.ContainsKey(#1/1/2007#) ' -> /2007/2008/2009/

        ' ASSERT
        Assert.AreEqual("/2007/2008/2009/", sc.BucketQueue)

    End Sub

    <Test(), Category("Icm")>
    Public Sub OldestBucketIsThrownAway()
        ' ARRANGE
        Dim repo = New FakeRepo(InitialList)
        Dim sc As New RepositorySortedCollection(Of Date, String)(repo, New YearManager, 2, New DateTotalOrder)
        Dim result As Boolean

        ' ACT
        result = sc.ContainsKey(#1/1/2009#)
        result = sc.ContainsKey(#1/1/2007#)

        ' Third year examined causes the oldest (2009) to be expelled.
        result = sc.ContainsKey(#1/1/2008#)
        ' ASSERT
        Assert.AreEqual("/2008/2007/", sc.BucketQueue)
    End Sub

    <Test(), Category("Icm")>
    Public Sub NextInSameBucket()
        ' ARRANGE
        Dim repo = New FakeRepo(InitialList)
        Dim sc As New RepositorySortedCollection(Of Date, String)(repo, New YearManager, 2, New DateTotalOrder)
        Dim result As Date?
        ' ACT
        result = sc.Next(#1/1/2007#)

        ' ASSERT
        Assert.AreEqual(#2/1/2007#, result)
    End Sub


    <Test(), Category("Icm")>
    Public Sub NextInOtherNotLoadedBucket()
        ' ARRANGE
        Dim repo = New FakeRepo(InitialList)
        Dim sc As New RepositorySortedCollection(Of Date, String)(repo, New YearManager, 2, New DateTotalOrder)
        Dim result As Date?
        ' ACT
        Assert.AreEqual("/", sc.BucketQueue)
        result = sc.Next(#2/1/2007#)

        ' ASSERT
        Assert.AreEqual(#1/1/2008#, result)
        Assert.AreEqual("/2008/", sc.BucketQueue)
    End Sub


    <Test(), Category("Icm")>
    Public Sub NextInOtherLoadedBucket()
        ' ARRANGE
        Dim repo = New FakeRepo(InitialList)
        Dim sc As New RepositorySortedCollection(Of Date, String)(repo, New YearManager, 2, New DateTotalOrder)
        Dim result As Date?
        ' ACT
        sc.ContainsKey(#1/1/2008#)

        Assert.AreEqual("/2008/", sc.BucketQueue)
        result = sc.Next(#2/1/2007#)

        ' ASSERT
        Assert.AreEqual(#1/1/2008#, result)
        Assert.AreEqual("/2008/", sc.BucketQueue)
    End Sub

    <Test(), Category("Icm")>
    Public Sub NextInOtherNotConsecutiveBucket()
        ' ARRANGE
        Dim repo = New FakeRepo(InitialList)
        Dim sc As New RepositorySortedCollection(Of Date, String)(repo, New YearManager, 2, New DateTotalOrder)
        Dim result As Date?
        ' ACT
        result = sc.Next(#1/1/2010#)

        ' ASSERT
        Assert.AreEqual(#1/1/2012#, result)
        Assert.AreEqual("/2012/", sc.BucketQueue)
    End Sub

    <Test(), Category("Icm")>
    Public Sub NextDoesNotExist()
        ' ARRANGE
        Dim repo = New FakeRepo(InitialList)
        Dim sc As New RepositorySortedCollection(Of Date, String)(repo, New YearManager, 2, New DateTotalOrder)
        Dim result As Date?
        ' ACT
        result = sc.Next(#1/1/2013#)

        ' ASSERT
        Assert.IsNull(result)

    End Sub
End Class
