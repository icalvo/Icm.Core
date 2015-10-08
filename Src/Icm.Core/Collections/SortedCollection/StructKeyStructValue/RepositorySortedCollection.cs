using System;
using System.Collections.Generic;
using Icm.Collections.Generic;
using Icm.Collections.Generic.General;
using Icm.Functions;

namespace Icm.Collections.Generic.StructKeyStructValue {
    
    // '' <summary>
    // '' Repository based storage sorted collection.
    // '' </summary>
    // '' <typeparam name="TKey"></typeparam>
    // '' <typeparam name="TValue"></typeparam>
    // '' <remarks>
    // '' This sorted collection relies on a repository class (that implements <see cref="ISortedCollectionRepository(Of TKey?, TValue)"></see>)
    // '' to store most of its data. A portion of the data is stored in a cache composed of
    // '' N buckets (property MaximumBuckets),
    // '' each one corresponding to a portion of the TKey? space. In order to manage the partition of TKey?
    // '' space, an <see cref="IPeriodManager(Of TKey?)"></see> implementor is employed.
    // '' </remarks>
    public class RepositorySortedCollection<TKey, TValue> where TKey : struct, IComparable<TKey> where TValue: struct
    {
        
        struct Unknown : BaseSortedCollection<TKey, TValue> {
            
            private LinkedList<int> bucketQueue_ = new LinkedList<int>();
            
            private Dictionary<int, SortedList<TKey, TValue>> buckets_ = new Dictionary<int, SortedList<TKey, TValue>>();
            
            private int maximumBuckets_;
            
            private IPeriodManager<TKey> periodManager_;
            
            private ISortedCollectionRepository<TKey, TValue> repository_;
            
            private ITotalOrder<TKey> totalOrder_;
            
            public Unknown(ISortedCollectionRepository<TKey, TValue> repo, IPeriodManager<TKey> periodManager, int maxBuckets, ITotalOrder<TKey> totalOrder) : 
                    base(totalOrder) {
                repository_ = repo;
                periodManager_ = periodManager;
                maximumBuckets_ = maxBuckets;
                totalOrder_ = totalOrder;
            }
            
            int MaximumBuckets {
                get {
                    return maximumBuckets_;
                }
            }
            
            public override TKey Next(TKey key) {
                //  1. If the key has a corresponding bucket:
                //     1. If the next element is in that bucket we return it.
                //     2. Else, we go looking at the following consecutive buckets after the current.
                //        If someone has some date, we get the first one.
                //  2. We get the key from the DB by means of a direct query, and if the corresponding bucket
                //     is not in memory we retrieve it.
                int period = periodManager_.Period(key);
                if (buckets_.ContainsKey(period)) {
                    object nextIdx = buckets_[period].IndexOfNextKey(key);
                    if ((nextIdx != buckets_[period].Count)) {
                        return buckets_[period].Keys[nextIdx];
                    }
                    else {
                        period++;
                        while (buckets_.ContainsKey(period)) {
                            if ((buckets_[period].Count == 0)) {
                                // TODO: Continue Do... Warning!!! not translated
                            }
                            else {
                                return buckets_[period].Keys.First;
                            }
                            
                        }
                        
                    }
                    
                }
                
                Next = repository_.GetNext(key);
                this.GetBucket(Next);
            }
            
            public override TKey Previous(TKey key) {
                //  1. If the key has a corresponding bucket:
                //     1. If the prev element is in that bucket we return it.
                //     2. Else, we go looking at the previous consecutive buckets before the current.
                //        If someone has some date, we get the last one.
                //  2. We get the key from the DB by means of a direct query, and if the corresponding bucket
                //     is not in memory we retrieve it.
                int period = periodManager_.Period(key);
                if (buckets_.ContainsKey(period)) {
                    object prevIdx = buckets_[period].IndexOfPrevKey(key);
                    if ((prevIdx != buckets_.Count)) {
                        return buckets_[period].Keys[prevIdx];
                    }
                    else {
                        period--;
                        while (buckets_.ContainsKey(period)) {
                            if ((buckets_[period].Count == 0)) {
                                // TODO: Continue Do... Warning!!! not translated
                            }
                            else {
                                return buckets_[period].Keys.Last;
                            }
                            
                        }
                        
                    }
                    
                }
                
                Previous = repository_.GetPrevious(key);
                this.GetBucket(Previous);
            }
            
            public override bool ContainsKey(TKey key) {
                return this.GetBucket(key).ContainsKey(key);
            }
            
            override TValue this[TKey key] {
                get {
                    return this.GetBucket(key)[key];
                }
                set {
                    this.GetBucket(key)[key] = value;
                    //  Estrategia de copia directa en DB
                    repository_.Update(key, value);
                }
            }
            
            public string BucketQueue() {
                return bucketQueue_.Aggregate("/", Function, s, x);
                (s 
                            + (x + "/"));
            }
            
            // Public Overrides Function HasFreeKey(ByVal desiredKey As TKey?) As Boolean
            //     Return Not desiredKey.Equals(totalOrder_.Greatest)
            // End Function
            // Public Overrides Function HasNext(ByVal key As TKey?) As Boolean
            //     Dim period As Integer = periodManager_.Period(key)
            //     If buckets_.ContainsKey(period) Then
            //         Dim nextIdx = buckets_(period).IndexOfNextKey(key)
            //         If nextIdx <> buckets_.Count Then
            //             Return True
            //         Else
            //             period += 1
            //             Do While buckets_.ContainsKey(period)
            //                 If buckets_(period).Count = 0 Then
            //                     Continue Do
            //                 Else
            //                     Return True
            //                 End If
            //             Loop
            //         End If
            //     End If
            //     Return repository_.HasNext(key)
            // End Function
            // Public Overrides Function HasPrevious(ByVal key As TKey?) As Boolean
            //     Dim period As Integer = periodManager_.Period(key)
            //     If buckets_.ContainsKey(period) Then
            //         Dim prevIdx = buckets_(period).IndexOfPrevKey(key)
            //         If prevIdx <> buckets_.Count Then
            //             Return True
            //         Else
            //             period -= 1
            //             Do While buckets_.ContainsKey(period)
            //                 If buckets_(period).Count = 0 Then
            //                     Continue Do
            //                 Else
            //                     Return True
            //                 End If
            //             Loop
            //         End If
            //     End If
            //     Return repository_.HasPrevious(key)
            // End Function
            public override void Add(TKey key, TValue value) {
                this.GetBucket(key).Add(key, value);
                repository_.Update(key, value);
            }
            
            public override TKey KeyOrNext(TKey key) {
                if (this.ContainsKey(key)) {
                    return key;
                }
                else {
                    return this.Next(key);
                }
                
            }
            
            public override TKey KeyOrPrev(TKey key) {
                if (this.ContainsKey(key)) {
                    return key;
                }
                else {
                    return this.Previous(key);
                }
                
            }
            
            public override void Remove(TKey key) {
                int period = periodManager_.Period(key);
                if ((buckets_.ContainsKey(period) && buckets_[period].ContainsKey(key))) {
                    buckets_[period].Remove(key);
                }
                
                repository_.Remove(key);
            }
            
            // '' <summary>
            // '' Returns a bucket for a given key, retrieving it from the database
            // '' if necessary.
            // '' </summary>
            // '' <param name="key"></param>
            // '' <returns></returns>
            // '' <remarks></remarks>
            private SortedList<TKey, TValue> GetBucket(TKey key, void Question) {
                if (!key.HasValue) {
                    return null;
                }
                
                int period = periodManager_.Period(key.Value);
                SortedList<TKey, TValue> result;
                if (buckets_.ContainsKey(period)) {
                    bucketQueue_.Remove(period);
                    result = buckets_[period];
                    bucketQueue_.AddFirst(period);
                }
                else {
                    //  Retrieve a bucket from database
                    object q = repository_.GetRange(periodManager_.PeriodStart(period), periodManager_.PeriodStart((period + 1)));
                    result = new SortedList<TKey, TValue>();
                    foreach (element in q) {
                        result.Add(element.First, element.Second);
                    }
                    
                    buckets_.Add(period, result);
                    if ((result.Count != 0)) {
                        //  Empty buckets do not count for the limit queue
                        bucketQueue_.AddFirst(period);
                    }
                    
                }
                
                //  Update bucket queue
                if ((buckets_.Count > maximumBuckets_)) {
                    object lastPeriod = bucketQueue_.LastOrDefault;
                    bucketQueue_.RemoveLast();
                    buckets_.Remove(lastPeriod);
                }
                
                return result;
            }
            
            public override int Count() {
                return repository_.Count;
            }
        }
    }
}