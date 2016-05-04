using System;
using System.Collections.Generic;
using System.Linq;

namespace Icm.Collections.Generic.StructKeyStructValue
{
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
    public class RepositorySortedCollection<TKey, TValue> : BaseSortedCollection<TKey, TValue>
        where TKey : struct, IComparable<TKey>
        where TValue : struct
    {
        private readonly LinkedList<int> _bucketQueue = new LinkedList<int>();

        private readonly Dictionary<int, SortedList<TKey?, TValue>> _buckets =
            new Dictionary<int, SortedList<TKey?, TValue>>();

        private readonly int _maximumBuckets;
        private readonly IPeriodManager<TKey> _periodManager;
        private readonly ISortedCollectionRepository<TKey, TValue> _repository;

        public RepositorySortedCollection(ISortedCollectionRepository<TKey, TValue> repo,
            IPeriodManager<TKey> periodManager, int maxBuckets, ITotalOrder<TKey> totalOrder) :
                base(totalOrder)
        {
            _repository = repo;
            _periodManager = periodManager;
            _maximumBuckets = maxBuckets;
        }

        private int MaximumBuckets => _maximumBuckets;

        public override TKey? Next(TKey key)
        {
            //  1. If the key has a corresponding bucket:
            //     1. If the next element is in that bucket we return it.
            //     2. Else, we go looking at the following consecutive buckets after the current.
            //        If someone has some date, we get the first one.
            //  2. We get the key from the DB by means of a direct query, and if the corresponding bucket
            //     is not in memory we retrieve it.
            int period = _periodManager.Period(key);
            if (_buckets.ContainsKey(period))
            {
                var nextIdx = _buckets[period].IndexOfNextKey(key);
                if ((nextIdx != _buckets[period].Count))
                {
                    return _buckets[period].Keys[nextIdx];
                }
                else
                {
                    period++;
                    while (_buckets.ContainsKey(period))
                    {
                        if ((_buckets[period].Count == 0))
                        {
                            // TODO: Continue Do... Warning!!! not translated
                        }
                        else
                        {
                            return _buckets[period].Keys.First();
                        }
                    }
                }
            }

            var result = _repository.GetNext(key);
            this.GetBucket(result);

            return result;
        }

        public override TKey? Previous(TKey key)
        {
            //  1. If the key has a corresponding bucket:
            //     1. If the prev element is in that bucket we return it.
            //     2. Else, we go looking at the previous consecutive buckets before the current.
            //        If someone has some date, we get the last one.
            //  2. We get the key from the DB by means of a direct query, and if the corresponding bucket
            //     is not in memory we retrieve it.
            int period = _periodManager.Period(key);
            if (_buckets.ContainsKey(period))
            {
                var prevIdx = _buckets[period].IndexOfPrevKey(key);
                if (prevIdx != _buckets.Count)
                {
                    return _buckets[period].Keys[prevIdx];
                }

                period--;
                while (_buckets.ContainsKey(period))
                {
                    if ((_buckets[period].Count == 0))
                    {
                        // TODO: Continue Do... Warning!!! not translated
                    }
                    else
                    {
                        return _buckets[period].Keys.Last();
                    }
                }
            }

            var result = _repository.GetPrevious(key);
            GetBucket(result);

            return result;
        }

        public override bool ContainsKey(TKey key)
        {
            return this.GetBucket(key).ContainsKey(key);
        }

        public override TValue this[TKey key]
        {
            get { return this.GetBucket(key)[key]; }
            set
            {
                this.GetBucket(key)[key] = value;
                //  Estrategia de copia directa en DB
                _repository.Update(key, value);
            }
        }

        public string BucketQueue()
        {
            return _bucketQueue.Aggregate(
                "/",
                (s, x) => s + x + "/");
        }

        public override void Add(TKey key, TValue value)
        {
            this.GetBucket(key).Add(key, value);
            _repository.Update(key, value);
        }

        public override TKey? KeyOrNext(TKey key)
        {
            return ContainsKey(key)
                ? key
                : Next(key);
        }

        public override TKey? KeyOrPrev(TKey key)
        {
            return this.ContainsKey(key)
                ? key
                : this.Previous(key);
        }

        public override void Remove(TKey key)
        {
            int period = _periodManager.Period(key);
            if ((_buckets.ContainsKey(period) && _buckets[period].ContainsKey(key)))
            {
                _buckets[period].Remove(key);
            }

            _repository.Remove(key);
        }

        // '' <summary>
        // '' Returns a bucket for a given key, retrieving it from the database
        // '' if necessary.
        // '' </summary>
        // '' <param name="key"></param>
        // '' <returns></returns>
        // '' <remarks></remarks>
        private SortedList<TKey?, TValue> GetBucket(TKey? key)
        {
            if (!key.HasValue)
            {
                return null;
            }

            int period = _periodManager.Period(key.Value);
            SortedList<TKey?, TValue> result;
            if (_buckets.ContainsKey(period))
            {
                _bucketQueue.Remove(period);
                result = _buckets[period];
                _bucketQueue.AddFirst(period);
            }
            else
            {
                //  Retrieve a bucket from database
                var q = _repository.GetRange(_periodManager.PeriodStart(period),
                    _periodManager.PeriodStart((period + 1)));
                result = new SortedList<TKey?, TValue>();
                foreach (var element in q)
                {
                    result.Add(element.First, element.Second);
                }

                _buckets.Add(period, result);
                if ((result.Count != 0))
                {
                    //  Empty buckets do not count for the limit queue
                    _bucketQueue.AddFirst(period);
                }
            }

            //  Update bucket queue
            if ((_buckets.Count > _maximumBuckets))
            {
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