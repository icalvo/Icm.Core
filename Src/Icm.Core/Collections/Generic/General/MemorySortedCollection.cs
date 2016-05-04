using System;
using System.Collections.Generic;

using Icm;

namespace Icm.Collections.Generic.General
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


		private SortedList<TKey, TValue> sl_ = new SortedList<TKey, TValue>();
		public MemorySortedCollection(ITotalOrder<TKey> otkey) : base(otkey)
		{
		}

		public override bool ContainsKey(TKey key)
		{
			return sl_.ContainsKey(key);
		}

		public override void Add(TKey key, TValue value)
		{
			sl_.Add(key, value);
		}

		public override TValue this[TKey key] {
			get { return sl_[key]; }
			set { sl_[key] = value; }
		}

		public override Nullable2<TKey> KeyOrNext(TKey key)
		{
			return Nullable2Conversion.ToNullable2(sl_.KeyOrNext(key));
		}

		public override Nullable2<TKey> KeyOrPrev(TKey key)
		{
			return Nullable2Conversion.ToNullable2(sl_.KeyOrPrev(key));
		}

		public override Nullable2<TKey> NextKey(TKey key)
		{
			return Nullable2Conversion.ToNullable2(sl_.NextKey(key));
		}

		public override Nullable2<TKey> PreviousKey(TKey key)
		{
			return Nullable2Conversion.ToNullable2(sl_.PrevKey(key));
		}

		public override void Remove(TKey key)
		{
			sl_.Remove(key);
		}


		public override int Count()
		{
			return sl_.Count;
		}
	}

}
