Imports Icm.Functions
Imports Icm.Collections.Generic

Namespace Icm.Collections.Generic.General
    ''' <summary>
    ''' Repository based storage sorted collection.
    ''' </summary>
    ''' <typeparam name="TKey"></typeparam>
    ''' <typeparam name="TValue"></typeparam>
    ''' <remarks>
    ''' This sorted collection relies on a repository class (that implements <see cref="ISortedCollectionRepository(Of Nullable2(Of TKey), TValue)"></see>)
    ''' to store most of its data. A portion of the data is stored in a cache composed of
    ''' N buckets (property MaximumBuckets),
    ''' each one corresponding to a portion of the TKey domain. In order to manage the partitioning of TKey
    ''' an <see cref="IPeriodManager(Of Nullable2(Of TKey))"></see> implementor is employed.
    ''' </remarks>
    Public Class RepositorySortedCollection(Of TKey As {IComparable(Of TKey)}, TValue)
        Inherits BaseSortedCollection(Of TKey, TValue)

#Region " Attributes "
        Private _bucketQueue As New LinkedList(Of Integer)
        Private buckets_ As New Dictionary(Of Integer, SortedList(Of Nullable2(Of TKey), TValue))

        Private maximumBuckets_ As Integer
        Private periodManager_ As IPeriodManager(Of TKey)
        Private repository_ As ISortedCollectionRepository(Of TKey, TValue)
        Private totalOrder_ As ITotalOrder(Of TKey)

#End Region

#Region " Constructor"

        Public Sub New(
                      ByVal repo As ISortedCollectionRepository(Of TKey, TValue),
                      ByVal periodManager As IPeriodManager(Of TKey),
                      ByVal maxBuckets As Integer,
                      ByVal totalOrder As ITotalOrder(Of TKey)
                      )
            MyBase.new(totalOrder)
            repository_ = repo
            periodManager_ = periodManager
            maximumBuckets_ = maxBuckets
            totalOrder_ = totalOrder
        End Sub
#End Region

        ReadOnly Property MaximumBuckets As Integer
            Get
                Return maximumBuckets_
            End Get
        End Property

#Region " BaseSortedCollection "

        Public Overrides Function NextKey(ByVal key As TKey) As Nullable2(Of TKey)
            ' 1. If the key has a corresponding bucket:
            '    1. If the next element is in that bucket we return it.
            '    2. Else, we go looking at the following consecutive buckets after the current.
            '       If someone has some date, we get the first one.
            ' 2. We get the key from the DB by means of a direct query, and if the corresponding bucket
            '    is not in memory we retrieve the full bucket.

            Dim period As Integer = periodManager_.Period(key)
            If buckets_.ContainsKey(period) Then
                Dim nextIdx = buckets_(period).IndexOfNextKey(key)
                If nextIdx <> buckets_(period).Count Then
                    Return buckets_(period).Keys(nextIdx)
                Else
                    period += 1
                    Do While buckets_.ContainsKey(period)
                        If buckets_(period).Count = 0 Then
                            Continue Do
                        Else
                            Return buckets_(period).Keys.First
                        End If
                    Loop
                End If
            End If

            NextKey = repository_.GetNext(key)
            GetBucket(NextKey)
        End Function

        Public Overrides Function PreviousKey(ByVal key As TKey) As Nullable2(Of TKey)
            ' 1. If the key has a corresponding bucket:
            '    1. If the prev element is in that bucket we return it.
            '    2. Else, we go looking at the previous consecutive buckets before the current.
            '       If someone has some date, we get the last one.
            ' 2. We get the key from the DB by means of a direct query, and if the corresponding bucket
            '    is not in memory we retrieve it.

            Dim period As Integer = periodManager_.Period(key)
            If buckets_.ContainsKey(period) Then
                Dim prevIdx = buckets_(period).IndexOfPrevKey(key)
                If prevIdx <> buckets_.Count Then
                    Return buckets_(period).Keys(prevIdx)
                Else
                    period -= 1
                    Do While buckets_.ContainsKey(period)
                        If buckets_(period).Count = 0 Then
                            Continue Do
                        Else
                            Return buckets_(period).Keys.Last
                        End If
                    Loop
                End If
            End If

            PreviousKey = repository_.GetPrevious(key)
            GetBucket(PreviousKey)
        End Function

        Public Overrides Function ContainsKey(ByVal key As TKey) As Boolean
            Return GetBucket(key).ContainsKey(key)
        End Function

        Default Overrides Property Item(ByVal key As TKey) As TValue
            Get
                Return GetBucket(key)(key)
            End Get
            Set(ByVal value As TValue)
                GetBucket(key)(key) = value

                ' Estrategia de copia directa en DB
                repository_.Update(key, value)
            End Set
        End Property

        Public Function BucketQueue() As String
            Return _bucketQueue.Aggregate("/", Function(s, x) s & x & "/")
        End Function

        'Public Overrides Function HasFreeKey(ByVal desiredKey As Nullable2(Of TKey)) As Boolean
        '    Return Not desiredKey.Equals(totalOrder_.Greatest)
        'End Function

        'Public Overrides Function HasNext(ByVal key As Nullable2(Of TKey)) As Boolean
        '    Dim period As Integer = periodManager_.Period(key)
        '    If buckets_.ContainsKey(period) Then
        '        Dim nextIdx = buckets_(period).IndexOfNextKey(key)
        '        If nextIdx <> buckets_.Count Then
        '            Return True
        '        Else
        '            period += 1
        '            Do While buckets_.ContainsKey(period)
        '                If buckets_(period).Count = 0 Then
        '                    Continue Do
        '                Else
        '                    Return True
        '                End If
        '            Loop
        '        End If
        '    End If

        '    Return repository_.HasNext(key)
        'End Function

        'Public Overrides Function HasPrevious(ByVal key As Nullable2(Of TKey)) As Boolean
        '    Dim period As Integer = periodManager_.Period(key)
        '    If buckets_.ContainsKey(period) Then
        '        Dim prevIdx = buckets_(period).IndexOfPrevKey(key)
        '        If prevIdx <> buckets_.Count Then
        '            Return True
        '        Else
        '            period -= 1
        '            Do While buckets_.ContainsKey(period)
        '                If buckets_(period).Count = 0 Then
        '                    Continue Do
        '                Else
        '                    Return True
        '                End If
        '            Loop
        '        End If
        '    End If

        '    Return repository_.HasPrevious(key)
        'End Function

        Public Overrides Sub Add(ByVal key As TKey, ByVal value As TValue)
            GetBucket(key).Add(key, value)
            repository_.Add(key, value)
        End Sub

        Public Overrides Function KeyOrNext(ByVal key As TKey) As Nullable2(Of TKey)
            If ContainsKey(key) Then
                Return key
            Else
                Return NextKey(key)
            End If
        End Function

        Public Overrides Function KeyOrPrev(ByVal key As TKey) As Nullable2(Of TKey)
            If ContainsKey(key) Then
                Return key
            Else
                Return PreviousKey(key)
            End If
        End Function

        Public Overrides Sub Remove(ByVal key As TKey)
            Dim period As Integer = periodManager_.Period(key)
            If buckets_.ContainsKey(period) AndAlso buckets_(period).ContainsKey(key) Then
                buckets_(period).Remove(key)
            End If

            repository_.Remove(key)
        End Sub

#End Region

        ''' <summary>
        ''' Returns a bucket for a given key, retrieving it from the database
        ''' if necessary.
        ''' </summary>
        ''' <param name="key"></param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Private Function GetBucket(ByVal key As Nullable2(Of TKey)) As SortedList(Of Nullable2(Of TKey), TValue)
            If Not key.HasValue Then
                Return Nothing
            End If
            Dim period As Integer = periodManager_.Period(key.Value)
            Dim result As SortedList(Of Nullable2(Of TKey), TValue)
            If buckets_.ContainsKey(period) Then
                _bucketQueue.Remove(period)
                result = buckets_(period)
                _bucketQueue.AddFirst(period)
            Else
                ' Retrieve a bucket from database
                Dim q = repository_.GetRange(periodManager_.PeriodStart(period), periodManager_.PeriodStart(period + 1))
                result = New SortedList(Of Nullable2(Of TKey), TValue)
                For Each element In q
                    result.Add(element.First, element.Second)
                Next
                buckets_.Add(period, result)
                If result.Count <> 0 Then
                    ' Empty buckets do not count for the limit queue
                    _bucketQueue.AddFirst(period)
                End If
            End If

            ' Update bucket queue

            If buckets_.Count > maximumBuckets_ Then
                Dim lastPeriod = _bucketQueue.LastOrDefault
                _bucketQueue.RemoveLast()
                buckets_.Remove(lastPeriod)
            End If
            Return result
        End Function

        Public Overrides Function Count() As Integer
            Return repository_.Count
        End Function
    End Class
End Namespace
