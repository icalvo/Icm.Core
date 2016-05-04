using System;
using System.Collections;
using System.Collections.Generic;

namespace Icm.Collections.Generic.StructKeyStructValue
{
    public abstract class BaseSortedCollection<TKey, TValue> : ISortedCollection<TKey, TValue> where TKey : struct, IComparable<TKey> where TValue : struct
    {
        protected BaseSortedCollection(ITotalOrder<TKey> otkey)
        {
            TotalOrder = otkey;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <remarks></remarks>
        private sealed class RangePointIterator : IEnumerator<Tuple<TKey, TValue?>>, IEnumerable<Tuple<TKey, TValue?>>
        {

            private readonly ISortedCollection<TKey, TValue> _f;
            private readonly TKey _rangeStart;
            private readonly TKey _rangeEnd;

            private Tuple<TKey, TValue?> _current;
            
            public RangePointIterator(ISortedCollection<TKey, TValue> f, TKey? rangeStart, TKey? rangeEnd)
            {
                _rangeStart = f.TotalOrder.LstIfNull(rangeStart);
                _rangeEnd = f.TotalOrder.GstIfNull(rangeEnd);
                _f = f;
            }
            
            public Tuple<TKey, TValue?> Current
            {
                get
                {
                    if (_current == null)
                    {
                        throw new InvalidOperationException("Enumerator has not been reset");
                    }
                    return _current;
                }
            }

            object IEnumerator.Current
            {
                get
                {
                    if (_current == null)
                    {
                        throw new InvalidOperationException("Enumerator has not been reset");
                    }

                    return _current;
                }
            }

            public bool MoveNext()
            {
                if (_current == null)
                {
                    _current = Pair(_rangeStart);
                }
                else if (_current.Item1.Equals(_rangeEnd))
                {
                    _current = null;
                    return false;
                }
                else
                {
                    var sig = _f.Next(_current.Item1);


                    if (sig.HasValue)
                    {
                        if (sig.Value.CompareTo(_rangeEnd) > 0)
                        {
                            _current = Pair(_rangeEnd);
                            return false;
                        }

                        _current = Pair(sig.Value);
                    }
                    else
                    {
                        if (_current.Item1.CompareTo(_rangeEnd) < 0)
                        {
                            _current = Pair(_rangeEnd);
                        }
                        else
                        {
                            // NO DEBERÍA SUCEDER
                            throw new Exception();
                        }
                    }
                }

                return true;
            }

            private Tuple<TKey, TValue?> Pair(TKey key)
            {
                return _f.ContainsKey(key) 
                    ? new Tuple<TKey, TValue?>(key, _f[key]) 
                    : new Tuple<TKey, TValue?>(key, null);
            }

            public void Reset()
            {
                _current = null;
            }
            
            public void Dispose()
            {

            }

            public IEnumerator<Tuple<TKey, TValue?>> GetEnumerator()
            {
                return this;
            }
            
            IEnumerator IEnumerable.GetEnumerator()
            {
                return this;
            }
        }


        public ITotalOrder<TKey> TotalOrder { get; }

        public abstract void Add(TKey key, TValue value);

        public abstract bool ContainsKey(TKey key);

        public TKey? GetFreeKey(TKey desiredKey)
        {
            TKey? fFinal = desiredKey;
            while (ContainsKey(fFinal.Value))
            {
                if (TotalOrder.Compare(fFinal.Value, TotalOrder.Greatest()) == 0)
                {
                    return null;
                }

                fFinal = TotalOrder.Next(fFinal.Value);
            }

            return fFinal;
        }

        public abstract TValue this[TKey key] { get; set; }

        public abstract TKey? Next(TKey key);

        public abstract TKey? Previous(TKey key);

        public override string ToString()
        {
            return ToString(TotalOrder.Least(), TotalOrder.Greatest());
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

            foreach (var element in PointEnumerable(f1, f2))
            {
                if (element.Item2 == null)
                {
                    result.AppendFormat("NC {0} ---\r\n", element.Item1);
                }
                else
                {
                    result.AppendFormat("-> {0} {1}\r\n", element.Item1, element.Item2);
                }
            }

            return result.ToString();
        }

        public abstract TKey? KeyOrNext(TKey key);

        public abstract TKey? KeyOrPrev(TKey key);

        public abstract void Remove(TKey key);

        public IEnumerable<Vector2<Tuple<TKey, TValue?>>> IntervalEnumerable(Vector2<TKey?> intf)
        {
            return IntervalEnumerable(intf.Item1, intf.Item2);
        }

        public IEnumerable<Vector2<Tuple<TKey, TValue?>>> IntervalEnumerable(TKey? intStart, TKey? intEnd)
        {
            return new PairEnumerator<Tuple<TKey, TValue?>>(new RangePointIterator(this, intStart, intEnd));
        }

        public abstract int Count();

        public IEnumerable<Tuple<TKey, TValue?>> PointEnumerable(TKey? intStart, TKey? intEnd)
        {
            return new RangePointIterator(this, intStart, intEnd);
        }

        public IEnumerable<Tuple<TKey, TValue?>> PointEnumerable(Vector2<TKey?> intf)
        {
            return PointEnumerable(intf.Item1, intf.Item2);
        }

        public IEnumerable<Vector2<Tuple<TKey, TValue?>>> IntervalEnumerable()
        {
            return IntervalEnumerable(null, null);
        }

        public IEnumerable<Tuple<TKey, TValue?>> PointEnumerable()
        {
            return PointEnumerable(null, null);
        }
    }
}
