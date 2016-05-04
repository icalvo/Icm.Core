using System;
using System.Collections.Generic;
using System.Linq;

namespace Icm.Collections.Generic.General
{
    /// <summary>
    /// Repository based storage sorted collection.
    /// </summary>
    /// <typeparam name="TKey"></typeparam>
    /// <typeparam name="TValue"></typeparam>
    /// <remarks>
    /// This sorted collection relies on a repository class (that implements <see cref="ISortedCollectionRepository(Of Nullable2(Of TKey), TValue)"></see>)
    /// to store most of its data. A portion of the data is stored in a cache composed of
    /// N buckets (property MaximumBuckets),
    /// each one corresponding to a portion of the TKey domain. In order to manage the partitioning of TKey
    /// an <see cref="IPeriodManager{Nullable2}"></see> implementor is employed.
    /// </remarks>
    public class RepositorySortedCollection<TKey, TValue> : BaseSortedCollection<TKey, TValue> where TKey : IComparable<TKey>
	{

		#region " Attributes "
		private readonly LinkedList<int> _bucketQueue = new LinkedList<int>();

		private readonly Dictionary<int, SortedList<Nullable2<TKey>, TValue>> _buckets = new Dictionary<int, SortedList<Nullable2<TKey>, TValue>>();
		private readonly int _maximumBuckets;
		private readonly IPeriodManager<TKey> _periodManager;
		private readonly ISortedCollectionRepository<TKey, TValue> _repository;

		#endregion

		#region " Constructor"

		public RepositorySortedCollection(ISortedCollectionRepository<TKey, TValue> repo, IPeriodManager<TKey> periodManager, int maxBuckets, ITotalOrder<TKey> totalOrder) : base(totalOrder)
		{
			_repository = repo;
			_periodManager = periodManager;
			_maximumBuckets = maxBuckets;
		}
		#endregion

		public int MaximumBuckets {
			get { return _maximumBuckets; }
		}

		#region " BaseSortedCollection "

		public override Nullable2<TKey> NextKey(TKey key)
		{
		    // 1. If the key has a corresponding bucket:
			//    1. If the next element is in that bucket we return it.
			//    2. Else, we go looking at the following consecutive buckets after the current.
			//       If someone has some date, we get the first one.
			// 2. We get the key from the DB by means of a direct query, and if the corresponding bucket
			//    is not in memory we retrieve the full bucket.

			int period = _periodManager.Period(key);
			if (_buckets.ContainsKey(period)) {
				var nextIdx = _buckets[period].IndexOfNextKey(key);
				if (nextIdx != _buckets[period].Count) {
					return _buckets[period].Keys[nextIdx];
				}

			    period += 1;
			    while (_buckets.ContainsKey(period))
			    {
			        if (_buckets[period].Count != 0)
			        {
			            return _buckets[period].Keys.First();
			        }
			    }
			}

			var functionReturnValue = _repository.GetNext(key);
			GetBucket(functionReturnValue);
			return functionReturnValue;
		}

		public override Nullable2<TKey> PreviousKey(TKey key)
		{
		    // 1. If the key has a corresponding bucket:
			//    1. If the prev element is in that bucket we return it.
			//    2. Else, we go looking at the previous consecutive buckets before the current.
			//       If someone has some date, we get the last one.
			// 2. We get the key from the DB by means of a direct query, and if the corresponding bucket
			//    is not in memory we retrieve it.

			int period = _periodManager.Period(key);
			if (_buckets.ContainsKey(period)) {
				var prevIdx = _buckets[period].IndexOfPrevKey(key);
				if (prevIdx != _buckets.Count) {
					return _buckets[period].Keys[prevIdx];
				}

			    period -= 1;
			    while (_buckets.ContainsKey(period))
			    {
			        if (_buckets[period].Count != 0)
			        {
			            return _buckets[period].Keys.Last();
			        }
			    }
			}

			var functionReturnValue = _repository.GetPrevious(key);
			GetBucket(functionReturnValue);
			return functionReturnValue;
		}

		public override bool ContainsKey(TKey key)
		{
			return GetBucket(key).ContainsKey(key);
		}

		public override TValue this[TKey key] {
			get { return GetBucket(key)[key]; }
			set {
				GetBucket(key)[key] = value;

				// Estrategia de copia directa en DB
				_repository.Update(key, value);
			}
		}

		public string BucketQueue()
		{
			return _bucketQueue.Aggregate("/", (s, x) => s + x + "/");
		}

		//Public Overrides Function HasFreeKey(ByVal desiredKey As Nullable2(Of TKey)) As Boolean
		//    Return Not desiredKey.Equals(totalOrder_.Greatest)
		//End Function

		//Public Overrides Function HasNext(ByVal key As Nullable2(Of TKey)) As Boolean
		//    Dim period As Integer = periodManager_.Period(key)
		//    If buckets_.ContainsKey(period) Then
		//        Dim nextIdx = buckets_(period).IndexOfNextKey(key)
		//        If nextIdx <> buckets_.Count Then
		//            Return True
		//        Else
		//            period += 1
		//            Do While buckets_.ContainsKey(period)
		//                If buckets_(period).Count = 0 Then
		//                    Continue Do
		//                Else
		//                    Return True
		//                End If
		//            Loop
		//        End If
		//    End If

		//    Return repository_.HasNext(key)
		//End Function

		//Public Overrides Function HasPrevious(ByVal key As Nullable2(Of TKey)) As Boolean
		//    Dim period As Integer = periodManager_.Period(key)
		//    If buckets_.ContainsKey(period) Then
		//        Dim prevIdx = buckets_(period).IndexOfPrevKey(key)
		//        If prevIdx <> buckets_.Count Then
		//            Return True
		//        Else
		//            period -= 1
		//            Do While buckets_.ContainsKey(period)
		//                If buckets_(period).Count = 0 Then
		//                    Continue Do
		//                Else
		//                    Return True
		//                End If
		//            Loop
		//        End If
		//    End If

		//    Return repository_.HasPrevious(key)
		//End Function

		public override void Add(TKey key, TValue value)
		{
			GetBucket(key).Add(key, value);
			_repository.Add(key, value);
		}

		public override Nullable2<TKey> KeyOrNext(TKey key)
		{
			if (ContainsKey(key)) {
				return key;
			} else {
				return NextKey(key);
			}
		}

		public override Nullable2<TKey> KeyOrPrev(TKey key)
		{
			if (ContainsKey(key)) {
				return key;
			} else {
				return PreviousKey(key);
			}
		}

		public override void Remove(TKey key)
		{
			int period = _periodManager.Period(key);
			if (_buckets.ContainsKey(period) && _buckets[period].ContainsKey(key)) {
				_buckets[period].Remove(key);
			}

			_repository.Remove(key);
		}

		#endregion

		/// <summary>
		/// Returns a bucket for a given key, retrieving it from the database
		/// if necessary.
		/// </summary>
		/// <param name="key"></param>
		/// <returns></returns>
		/// <remarks></remarks>
		private SortedList<Nullable2<TKey>, TValue> GetBucket(Nullable2<TKey> key)
		{
			if (!key.HasValue) {
				return null;
			}
			int period = _periodManager.Period(key.Value);
			SortedList<Nullable2<TKey>, TValue> result;
			if (_buckets.ContainsKey(period)) {
				_bucketQueue.Remove(period);
				result = _buckets[period];
				_bucketQueue.AddFirst(period);
			} else {
				// Retrieve a bucket from database
				var q = _repository.GetRange(_periodManager.PeriodStart(period), _periodManager.PeriodStart(period + 1));
				result = new SortedList<Nullable2<TKey>, TValue>();
				foreach (var element in q) {
					result.Add(element.First, element.Second);
				}
				_buckets.Add(period, result);
				if (result.Count != 0) {
					// Empty buckets do not count for the limit queue
					_bucketQueue.AddFirst(period);
				}
			}

			// Update bucket queue

			if (_buckets.Count > _maximumBuckets) {
				var lastPeriod = _bucketQueue.LastOrDefault();
				_bucketQueue.RemoveLast();
				_buckets.Remove(lastPeriod);
			}
			return result;
		}

		public override int Count()
		{
			return _repository.Count();
		}
	}
}
