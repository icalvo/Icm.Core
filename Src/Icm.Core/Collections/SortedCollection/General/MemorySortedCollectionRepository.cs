
using Microsoft.VisualBasic;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using Icm.Collections.Generic;

namespace Icm.Collections.Generic.General
{

	public class MemorySortedCollectionRepository<TKey, TValue> : ISortedCollectionRepository<TKey, TValue> where TKey : IComparable<TKey>
	{

		public SortedList<TKey, TValue> List { get; set; }

		public virtual IEnumerable<Pair<TKey, TValue>> GetRange(TKey rangeStart, TKey rangeEnd)
		{
			return List.Where(kvp => rangeStart.CompareTo(kvp.Key) <= 0 && kvp.Key.CompareTo(rangeEnd) < 0).Select(kvp => new Pair<TKey, TValue>(kvp.Key, kvp.Value));
		}

		public virtual void Add(TKey key, TValue val)
		{
			List(key) = val;
		}

		public virtual void Update(TKey key, TValue val)
		{
			List(key) = val;
		}

		public virtual Nullable2<TKey> GetNext(TKey key)
		{
			return List.NextKey2(key);
		}

		public Nullable2<TKey> GetPrevious(TKey key)
		{
			return List.PrevKey2(key);
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


//=======================================================
//Service provided by Telerik (www.telerik.com)
//Conversion powered by NRefactory.
//Twitter: @telerik
//Facebook: facebook.com/telerik
//=======================================================
