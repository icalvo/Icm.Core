using System;
using System.Collections.Generic;

namespace Icm.Collections.Generic.StructKeyStructValue
{
    /// <summary>
    /// In-memory storage sorted collection. Functionally it is equivalent to a
    /// RepositorySortedCollection, using a MemorySortedCollectionRepository, and
    /// a TrivialPeriodManager.
    /// </summary>
    /// <typeparam name="TKey"></typeparam>
    /// <typeparam name="TValue"></typeparam>
    /// <remarks></remarks>
    public class MemorySortedCollection<TKey, TValue> : BaseSortedCollection<TKey, TValue> where TKey : struct, IComparable<TKey> where TValue : struct
    {


        private readonly SortedList<TKey, TValue> _sl = new SortedList<TKey, TValue>();
        public MemorySortedCollection(ITotalOrder<TKey> otkey) : base(otkey)
        {
        }

        public override bool ContainsKey(TKey key)
        {
            return _sl.ContainsKey(key);
        }

        public override void Add(TKey key, TValue value)
        {
            _sl.Add(key, value);
        }

        public override TValue this[TKey key]
        {
            get { return _sl[key]; }
            set { _sl[key] = value; }
        }

        public override TKey? KeyOrNext(TKey key)
        {
            return _sl.KeyOrNext(key);
        }

        public override TKey? KeyOrPrev(TKey key)
        {
            return _sl.KeyOrPrev(key);
        }

        public override TKey? Next(TKey key)
        {
            return _sl.NextKey(key);
        }

        public override TKey? Previous(TKey key)
        {
            return _sl.PrevKey(key);
        }

        public override void Remove(TKey key)
        {
            _sl.Remove(key);
        }

        public override int Count()
        {
            return _sl.Count;
        }
    }

}
