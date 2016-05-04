using System;
using System.Collections.Generic;
using System.Linq;

namespace Icm.Collections.Generic.StructKeyStructValue
{

    public class MemorySortedCollectionRepository<TKey, TValue> : ISortedCollectionRepository<TKey, TValue> where TKey : struct, IComparable<TKey>
    {
        public SortedList<TKey, TValue> List { get; set; }

        public virtual IEnumerable<Pair<TKey, TValue>> GetRange(TKey rangeStart, TKey rangeEnd)
        {
            return List.Where(kvp => rangeStart.CompareTo(kvp.Key) <= 0 && kvp.Key.CompareTo(rangeEnd) < 0).Select(kvp => new Pair<TKey, TValue>(kvp.Key, kvp.Value));
        }
        public virtual void Update(TKey key, TValue val)
        {
            List[key] = val;
        }

        public virtual TKey? GetNext(TKey key)
        {
            return List.NextKey(key);
        }

        public TKey? GetPrevious(TKey key)
        {
            return List.PrevKey(key);
        }


        public void Remove(TKey key)
        {
            List.Remove(key);
        }


        public int Count()
        {
            return List.Count;
        }
    }
}
