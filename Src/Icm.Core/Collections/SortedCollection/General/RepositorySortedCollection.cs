using System;
using System.Collections.Generic;

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
	/// an <see cref="IPeriodManager(Of Nullable2(Of TKey))"></see> implementor is employed.
	/// </remarks>
	public class RepositorySortedCollection<TKey, TValue> : BaseSortedCollection<TKey, TValue> where TKey : IComparable<TKey>
	{

		#region " Attributes "
		private LinkedList<int> _bucketQueue = new LinkedList<int>();

		private Dictionary<int, SortedList<Nullable2<TKey>, TValue>> buckets_ = new Dictionary<int, SortedList<Nullable2<TKey>, TValue>>();
		private int maximumBuckets_;
		private IPeriodManager<TKey> periodManager_;
		private ISortedCollectionRepository<TKey, TValue> repository_;

		private ITotalOrder<TKey> totalOrder_;
		#endregion

		#region " Constructor"

		public RepositorySortedCollection(ISortedCollectionRepository<TKey, TValue> repo, IPeriodManager<TKey> periodManager, int maxBuckets, ITotalOrder<TKey> totalOrder) : base(totalOrder)
		{
			repository_ = repo;
			periodManager_ = periodManager;
			maximumBuckets_ = maxBuckets;
			totalOrder_ = totalOrder;
		}
		#endregion

		public int MaximumBuckets {
			get { return maximumBuckets_; }
		}

		#region " BaseSortedCollection "

		public override Nullable2<TKey> NextKey(TKey key)
		{
			Nullable2<TKey> functionReturnValue = default(Nullable2<TKey>);
			// 1. If the key has a corresponding bucket:
			//    1. If the next element is in that bucket we return it.
			//    2. Else, we go looking at the following consecutive buckets after the current.
			//       If someone has some date, we get the first one.
			// 2. We get the key from the DB by means of a direct query, and if the corresponding bucket
			//    is not in memory we retrieve the full bucket.

			int period = periodManager_.Period(key);
			if (buckets_.ContainsKey(period)) {
				dynamic nextIdx = buckets_(period).IndexOfNextKey(key);
				if (nextIdx != buckets_(period).Count) {
					return buckets_(period).Keys(nextIdx);
				} else {
					period += 1;
					while (buckets_.ContainsKey(period)) {
						if (buckets_(period).Count == 0) {
							continue;
						} else {
							return buckets_(period).Keys.First;
						}
					}
				}
			}

			functionReturnValue = repository_.GetNext(key);
			GetBucket(NextKey());
			return functionReturnValue;
		}

		public override Nullable2<TKey> PreviousKey(TKey key)
		{
			Nullable2<TKey> functionReturnValue = default(Nullable2<TKey>);
			// 1. If the key has a corresponding bucket:
			//    1. If the prev element is in that bucket we return it.
			//    2. Else, we go looking at the previous consecutive buckets before the current.
			//       If someone has some date, we get the last one.
			// 2. We get the key from the DB by means of a direct query, and if the corresponding bucket
			//    is not in memory we retrieve it.

			int period = periodManager_.Period(key);
			if (buckets_.ContainsKey(period)) {
				dynamic prevIdx = buckets_(period).IndexOfPrevKey(key);
				if (prevIdx != buckets_.Count) {
					return buckets_(period).Keys(prevIdx);
				} else {
					period -= 1;
					while (buckets_.ContainsKey(period)) {
						if (buckets_(period).Count == 0) {
							continue;
						} else {
							return buckets_(period).Keys.Last;
						}
					}
				}
			}

			functionReturnValue = repository_.GetPrevious(key);
			GetBucket(PreviousKey());
			return functionReturnValue;
		}

		public override bool ContainsKey(TKey key)
		{
			return GetBucket(key).ContainsKey(key);
		}

		public override TValue this[TKey key] {
			get { return GetBucket(key)(key); }
			set {
				GetBucket(key)(key) = value;

				// Estrategia de copia directa en DB
				repository_.Update(key, value);
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
			repository_.Add(key, value);
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
			int period = periodManager_.Period(key);
			if (buckets_.ContainsKey(period) && buckets_(period).ContainsKey(key)) {
				buckets_(period).Remove(key);
			}

			repository_.Remove(key);
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
			int period = periodManager_.Period(key.Value);
			SortedList<Nullable2<TKey>, TValue> result = default(SortedList<Nullable2<TKey>, TValue>);
			if (buckets_.ContainsKey(period)) {
				_bucketQueue.Remove(period);
				result = buckets_(period);
				_bucketQueue.AddFirst(period);
			} else {
				// Retrieve a bucket from database
				dynamic q = repository_.GetRange(periodManager_.PeriodStart(period), periodManager_.PeriodStart(period + 1));
				result = new SortedList<Nullable2<TKey>, TValue>();
				foreach (void element_loopVariable in q) {
					element = element_loopVariable;
					result.Add(element.First, element.Second);
				}
				buckets_.Add(period, result);
				if (result.Count != 0) {
					// Empty buckets do not count for the limit queue
					_bucketQueue.AddFirst(period);
				}
			}

			// Update bucket queue

			if (buckets_.Count > maximumBuckets_) {
				dynamic lastPeriod = _bucketQueue.LastOrDefault;
				_bucketQueue.RemoveLast();
				buckets_.Remove(lastPeriod);
			}
			return result;
		}

		public override int Count()
		{
			return repository_.Count;
		}
	}
}

//=======================================================
//Service provided by Telerik (www.telerik.com)
//Conversion powered by NRefactory.
//Twitter: @telerik
//Facebook: facebook.com/telerik
//=======================================================
