using System;
using System.Collections;
using System.Collections.Generic;

namespace Icm.Collections.Generic.General
{
	public abstract class BaseSortedCollection<TKey, TValue> : ISortedCollection<TKey, TValue> where TKey : IComparable<TKey>
	{

		#region " Attributes "


		private readonly ITotalOrder<TKey> _totalOrder;
		#endregion

		protected BaseSortedCollection(ITotalOrder<TKey> otkey)
		{
			_totalOrder = otkey;
		}

		/// <summary>
		/// 
		/// </summary>
		/// <remarks></remarks>
		private class RangePointIterator : IEnumerator<Tuple<TKey, Nullable2<TValue>>>, IEnumerable<Tuple<TKey, Nullable2<TValue>>>
		{

			private readonly ISortedCollection<TKey, TValue> _f;
			private readonly TKey _rangeStart;
			private readonly TKey _rangeEnd;

			private Tuple<TKey, Nullable2<TValue>> _current;
			public RangePointIterator(ISortedCollection<TKey, TValue> f, Nullable2<TKey> rangeStart, Nullable2<TKey> rangeEnd)
			{
				_rangeStart = f.TotalOrder.LstIfNull(rangeStart);
				_rangeEnd = f.TotalOrder.GstIfNull(rangeEnd);
				_f = f;
			}

			public RangePointIterator(ISortedCollection<TKey, TValue> f, Vector2<Nullable2<TKey>> intf) : this(f, intf.Item1, intf.Item2)
			{
			}

			public Tuple<TKey, Nullable2<TValue>> Current {
				get {
					if (_current == null) {
						throw new InvalidOperationException("Enumerator has not been reset");
					}
					return _current;
				}
			}

			public object Current1 {
				get {
					if (_current == null) {
						throw new InvalidOperationException("Enumerator has not been reset");
					}
					return _current;
				}
			}
			object System.Collections.IEnumerator.Current {
				get { return Current1; }
			}

			public bool MoveNext()
			{
				if (_current == null) {
					_current = Pair[_rangeStart];
				} else if (_current.Item1.Equals(_rangeEnd)) {
					_current = null;
					return false;
				} else {
					dynamic sig = _f.NextKey(_current.Item1);


					if (sig.HasValue) {
						if (sig.Value.CompareTo(_rangeEnd) > 0) {
							_current = Pair[_rangeEnd];
							return false;
						} else {
							_current = Pair[sig.Value];
						}
					} else {
						if (_current.Item1.CompareTo(_rangeEnd) < 0) {
							_current = Pair[_rangeEnd];
						} else {
							// NO DEBERÍA SUCEDER
							throw new InvalidOperationException();
						}
					}
				}

				return true;
			}

			public Tuple<TKey, Nullable2<TValue>> Pair {
				get {
					if (_f.ContainsKey(key)) {
						return new Tuple<TKey, Nullable2<TValue>>(key, _f(key));
					} else {
						return new Tuple<TKey, Nullable2<TValue>>(key, null);
					}
				}
			}

			public void Reset()
			{
				_current = null;
			}

				// To detect redundant calls
			private bool disposedValue = false;

			// IDisposable
			protected virtual void Dispose(bool disposing)
			{
				if (!this.disposedValue) {
					if (disposing) {
						// free other state (managed objects).
					}

					// free your own state (unmanaged objects).
					// set large fields to null.
				}
				this.disposedValue = true;
			}

			#region " IDisposable Support "
			// This code added by Visual Basic to correctly implement the disposable pattern.
			public void Dispose()
			{
				// Do not change this code.  Put cleanup code in Dispose(ByVal disposing As Boolean) above.
				Dispose(true);
				GC.SuppressFinalize(this);
			}
			#endregion

			public IEnumerator<Tuple<TKey, Nullable2<TValue>>> GetEnumerator()
			{
				return this;
			}

			public IEnumerator GetEnumerator1()
			{
				return this;
			}
			IEnumerator IEnumerable.GetEnumerator()
			{
				return GetEnumerator1();
			}
		}

		public ITotalOrder<TKey> TotalOrder {
			get { return _totalOrder; }
		}

		public abstract void Add(TKey key, TValue value);

		public abstract bool ContainsKey(TKey key);

		public Nullable2<TKey> GetFreeKey(TKey desiredKey)
		{
			Nullable2<TKey> fFinal = default(Nullable2<TKey>);
			fFinal = desiredKey;
			while (fFinal.HasValue && ContainsKey(fFinal.Value)) {
				fFinal = TotalOrder.Next(fFinal.Value);
			}
			return fFinal;
		}

		public abstract TValue this[TKey key] { get; set; }

		public abstract Nullable2<TKey> NextKey(TKey key);

		public abstract Nullable2<TKey> PreviousKey(TKey key);

		public override string ToString()
		{
			return ToString(TotalOrder.Least, TotalOrder.Greatest);
		}

		/// <summary>
		/// String representation of an interval of the sorted collection.
		/// For printing values, ToString will be used.
		/// </summary>
		/// <param name="f1">Initial key.</param>
		/// <param name="f2">Final key.</param>
		/// <returns></returns>
		/// <remarks></remarks>
		public virtual string ToString(TKey f1, TKey f2)
		{
			System.Text.StringBuilder result = new System.Text.StringBuilder();

			foreach (void element_loopVariable in PointEnumerable(f1, f2)) {
				element = element_loopVariable;
				if (element.Item2.HasValue) {
					result.AppendFormat("-> {0} {1}" + Constants.vbCrLf, element.Item1, element.Item2.ToString);
				} else {
					result.AppendFormat("NC {0} ---" + Constants.vbCrLf, element.Item1);
				}
			}

			return result.ToString;
		}


		public abstract Nullable2<TKey> KeyOrNext(TKey key);

		public abstract Nullable2<TKey> KeyOrPrev(TKey key);

		public abstract void Remove(TKey key);

		public IEnumerable<Vector2<Tuple<TKey, Nullable2<TValue>>>> IntervalEnumerable(Vector2<Nullable2<TKey>> intf)
		{
			return IntervalEnumerable(intf.Item1, intf.Item2);
		}

		public IEnumerable<Vector2<Tuple<TKey, Nullable2<TValue>>>> IntervalEnumerable(Nullable2<TKey> intStart, Nullable2<TKey> intEnd)
		{
			dynamic it = new PairEnumerator<Tuple<TKey, Nullable2<TValue>>>(new RangePointIterator(this, intStart, intEnd));
			return it;
		}

		public abstract int Count();

		public IEnumerable<System.Tuple<TKey, Nullable2<TValue>>> PointEnumerable(Nullable2<TKey> intStart, Nullable2<TKey> intEnd)
		{
			return new RangePointIterator(this, intStart, intEnd);
		}

		public IEnumerable<System.Tuple<TKey, Nullable2<TValue>>> PointEnumerable(Vector2<Nullable2<TKey>> intf)
		{
			return PointEnumerable(intf.Item1, intf.Item2);
		}

		public IEnumerable<Vector2<System.Tuple<TKey, Nullable2<TValue>>>> IntervalEnumerable()
		{
			return IntervalEnumerable(null, null);
		}

		public IEnumerable<System.Tuple<TKey, Nullable2<TValue>>> PointEnumerable()
		{
			return PointEnumerable(null, null);
		}
	}
}


//=======================================================
//Service provided by Telerik (www.telerik.com)
//Conversion powered by NRefactory.
//Twitter: @telerik
//Facebook: facebook.com/telerik
//=======================================================
