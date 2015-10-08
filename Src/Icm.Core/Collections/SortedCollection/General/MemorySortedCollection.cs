using System;
using System.Collections.Generic;

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
			get { return sl_(key); }
			set { sl_(key) = value; }
		}

		public override Nullable2<TKey> KeyOrNext(TKey key)
		{
			return sl_.KeyOrNext(key).ToNullable2;
		}

		public override Nullable2<TKey> KeyOrPrev(TKey key)
		{
			return sl_.KeyOrPrev(key).ToNullable2;
		}

		public override Nullable2<TKey> NextKey(TKey key)
		{
			return sl_.NextKey(key).ToNullable2;
		}

		public override Nullable2<TKey> PreviousKey(TKey key)
		{
			return sl_.PrevKey(key).ToNullable2;
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

//=======================================================
//Service provided by Telerik (www.telerik.com)
//Conversion powered by NRefactory.
//Twitter: @telerik
//Facebook: facebook.com/telerik
//=======================================================
