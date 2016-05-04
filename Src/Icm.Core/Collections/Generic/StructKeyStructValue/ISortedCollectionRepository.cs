using System.Collections.Generic;
namespace Icm.Collections.Generic.StructKeyStructValue
{

    public interface ISortedCollectionRepository<TKey, TValue> where TKey : struct
    {
        IEnumerable<Pair<TKey, TValue>> GetRange(TKey rangeStart, TKey rangeEnd);
        void Update(TKey key, TValue val);
        TKey? GetNext(TKey key);
        TKey? GetPrevious(TKey key);
        void Remove(TKey key);
        int Count();
    }

}
