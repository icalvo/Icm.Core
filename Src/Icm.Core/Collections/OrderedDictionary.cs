
using Microsoft.VisualBasic;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
namespace Icm
{

	/// <summary>
	/// Dictionary whose lists of Keys and Values maintain the order of insertion.
	/// </summary>
	/// <typeparam name="TKey"></typeparam>
	/// <typeparam name="TValue"></typeparam>
	/// <remarks></remarks>
	public class OrderedDictionary<TKey, TValue> : IDictionary<TKey, TValue>
	{


		private readonly List<TValue> list_ = new List<TValue>();
		private readonly Dictionary<TKey, int> dictIndices_ = new Dictionary<TKey, int>();
		private readonly Dictionary<TKey, TValue> dict_ = new Dictionary<TKey, TValue>();

		private bool isReadOnly_ = false;
		private ICollection<KeyValuePair<TKey, TValue>> Coll {
			get { return dict_; }
		}

		public void SetReadOnly()
		{
			isReadOnly_ = true;
		}

		private void Add(KeyValuePair<TKey, TValue> item)
		{
			if (isReadOnly_) {
				throw new NotSupportedException("Add is not supported on read-only dictionaries");
			}
			Coll.Add(item);
			list_.Add(item.Value);
			dictIndices_.Add(item.Key, list_.Count - 1);
		}

		public void Clear()
		{
			if (isReadOnly_) {
				throw new NotSupportedException("Clear is not supported on read-only dictionaries");
			}
			dict_.Clear();
			list_.Clear();
			dictIndices_.Clear();
		}

		public void Append(OrderedDictionary<TKey, TValue> other)
		{
			if (isReadOnly_) {
				throw new NotSupportedException("Append is not supported on read-only dictionaries");
			}
			for (i = 0; i <= other.Count - 1; i++) {
				Add(other.Keys(i), other.Values(i));
			}
		}

		private bool Contains1(KeyValuePair<TKey, TValue> item)
		{
			return Coll.Contains(item);
		}
		bool ICollection<KeyValuePair<TKey, TValue>>.Contains(KeyValuePair<TKey, TValue> item)
		{
			return Contains1(item);
		}

		private void CopyTo(KeyValuePair<TKey, TValue>[] array, int arrayIndex)
		{
			Coll.CopyTo(array, arrayIndex);
		}

		public int Count {
			get { return dict_.Count; }
		}

		private bool IsReadOnly {
			get { return isReadOnly_; }
		}

		private bool Remove1(KeyValuePair<TKey, TValue> item)
		{
			if (isReadOnly_) {
				throw new NotSupportedException("Remove is not supported on read-only dictionaries");
			}
			if (Coll.Remove(item)) {
				list_.RemoveAt(dictIndices_(item.Key));
				dictIndices_.Remove(item.Key);
				return true;
			} else {
				return false;
			}
		}
		bool ICollection<KeyValuePair<TKey, TValue>>.Remove(KeyValuePair<TKey, TValue> item)
		{
			return Remove1(item);
		}

		public bool ContainsKey(TKey key)
		{
			return dict_.ContainsKey(key);
		}

		public TValue this[TKey key] {
			get { return dict_(key); }
			set {
				if (isReadOnly_) {
					throw new NotSupportedException("Item set is not supported on read-only dictionaries");
				}
				dict_(key) = value;
				list_(dictIndices_(key)) = value;
			}
		}

		public ICollection<TKey> Keys {
			get { return dict_.Keys; }
		}

		public bool RemoveKey(TKey key)
		{
			if (isReadOnly_) {
				throw new NotSupportedException("RemoveKey is not supported on read-only dictionaries");
			}
			if (dict_.Remove(key)) {
				list_.RemoveAt(dictIndices_(key));
				dictIndices_.Remove(key);
				return true;
			} else {
				return false;
			}
		}
		bool IDictionary<TKey, TValue>.Remove(TKey key)
		{
			return RemoveKey(key);
		}

		public bool TryGetValue(TKey key, ref TValue value)
		{
			return dict_.TryGetValue(key, value);
		}

		public ICollection<TValue> Values {
			get { return list_; }
		}

		private IEnumerator<KeyValuePair<TKey, TValue>> GetEnumerator()
		{
			return dict_.GetEnumerator;
		}

		private System.Collections.IEnumerator GetEnumerator2()
		{
			return dict_.GetEnumerator;
		}
		System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
		{
			return GetEnumerator2();
		}

		public void Add(TKey key, TValue value)
		{
			if (isReadOnly_) {
				throw new NotSupportedException("Add is not supported on read-only dictionaries");
			}
			dict_.Add(key, value);
			list_.Add(value);
			dictIndices_.Add(key, list_.Count - 1);
		}
	}

}

//=======================================================
//Service provided by Telerik (www.telerik.com)
//Conversion powered by NRefactory.
//Twitter: @telerik
//Facebook: facebook.com/telerik
//=======================================================
