using System;
using System.Collections.Generic;
using System.Linq;

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


		private readonly List<TValue> _list = new List<TValue>();
		private readonly Dictionary<TKey, int> _dictIndices = new Dictionary<TKey, int>();
		private readonly Dictionary<TKey, TValue> _dict = new Dictionary<TKey, TValue>();

		private bool _isReadOnly = false;

		private ICollection<KeyValuePair<TKey, TValue>> Coll => _dict;

	    public void SetReadOnly()
		{
			_isReadOnly = true;
		}

	    void ICollection<KeyValuePair<TKey, TValue>>.Add(KeyValuePair<TKey, TValue> item)
		{
			if (_isReadOnly) {
				throw new NotSupportedException("Add is not supported on read-only dictionaries");
			}
			Coll.Add(item);
			_list.Add(item.Value);
			_dictIndices.Add(item.Key, _list.Count - 1);
		}

		public void Clear()
		{
			if (_isReadOnly) {
				throw new NotSupportedException("Clear is not supported on read-only dictionaries");
			}
			_dict.Clear();
			_list.Clear();
			_dictIndices.Clear();
		}

		public void Append(OrderedDictionary<TKey, TValue> other)
		{
			if (_isReadOnly) {
				throw new NotSupportedException("Append is not supported on read-only dictionaries");
			}
			for (var i = 0; i <= other.Count - 1; i++) {
				Add(other.Keys.ElementAt(i), other.Values.ElementAt(i));
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

        public void CopyTo(KeyValuePair<TKey, TValue>[] array, int arrayIndex)
		{
			Coll.CopyTo(array, arrayIndex);
		}

		public int Count => _dict.Count;

	    bool ICollection<KeyValuePair<TKey, TValue>>.IsReadOnly => _isReadOnly;

	    private bool Remove1(KeyValuePair<TKey, TValue> item)
		{
			if (_isReadOnly) {
				throw new NotSupportedException("Remove is not supported on read-only dictionaries");
			}
			if (Coll.Remove(item)) {
				_list.RemoveAt(_dictIndices[item.Key]);
				_dictIndices.Remove(item.Key);
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
			return _dict.ContainsKey(key);
		}

		public TValue this[TKey key] {
			get { return _dict[key]; }
			set {
				if (_isReadOnly) {
					throw new NotSupportedException("Item set is not supported on read-only dictionaries");
				}
				_dict[key] = value;
				_list[_dictIndices[key]] = value;
			}
		}

		public ICollection<TKey> Keys => _dict.Keys;

	    public bool RemoveKey(TKey key)
		{
			if (_isReadOnly) {
				throw new NotSupportedException("RemoveKey is not supported on read-only dictionaries");
			}
			if (_dict.Remove(key)) {
				_list.RemoveAt(_dictIndices[key]);
				_dictIndices.Remove(key);
				return true;
			} else {
				return false;
			}
		}

		bool IDictionary<TKey, TValue>.Remove(TKey key)
		{
			return RemoveKey(key);
		}

		public bool TryGetValue(TKey key, out TValue value)
		{
			return _dict.TryGetValue(key, out value);
		}

		public ICollection<TValue> Values => _list;

	    IEnumerator<KeyValuePair<TKey, TValue>> IEnumerable<KeyValuePair<TKey, TValue>>.GetEnumerator()
		{
			return _dict.GetEnumerator();
		}

		private System.Collections.IEnumerator GetEnumerator2()
		{
			return _dict.GetEnumerator();
		}

		System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
		{
			return GetEnumerator2();
		}

		public void Add(TKey key, TValue value)
		{
			if (_isReadOnly) {
				throw new NotSupportedException("Add is not supported on read-only dictionaries");
			}
			_dict.Add(key, value);
			_list.Add(value);
			_dictIndices.Add(key, _list.Count - 1);
		}
	}

}
