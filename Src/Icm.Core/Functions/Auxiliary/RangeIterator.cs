using Microsoft.VisualBasic;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
namespace Icm.Functions
{
    /// <summary>
    /// Iterator over a range of TX of function keys.
    /// </summary>
    /// <remarks>The iteration value is a segment between two points of the function. At the first and last points,
    /// either extreme of the segment could be not a key (they would be the start and end of the iteration).
    /// </remarks>
    public class RangeIterator<TX, TY> : IEnumerator<FunctionPointPair<TX, TY>>, IEnumerable<FunctionPointPair<TX, TY>> where TX : struct, IComparable<TX> where TY : struct, IComparable<TY>
    {

        private TX _firstKey;
        private TX _lastKey;
        private IKeyedMathFunction<TX, TY> f_;
        private TX rangeStart_;
        private TX rangeEnd_;
        private FunctionPointPair<TX, TY> current_;
        private IEnumerator<Vector2<Tuple<TX, TY?>>> interiorKeysEnumerator_;

        private int idx_;
        public RangeIterator(IKeyedMathFunction<TX, TY> f, TX rangeStart, TX rangeEnd, bool includeExtremes)
        {
            _firstKey = f.KeyStore.KeyOrNext(rangeStart).Value;
            _lastKey = f.KeyStore.KeyOrPrev(rangeEnd).Value;
            if (includeExtremes)
            {
                rangeStart_ = rangeStart;
                rangeEnd_ = rangeEnd;
            }
            else
            {
                rangeStart_ = _firstKey;
                rangeEnd_ = _lastKey;
            }
            f_ = f;
            interiorKeysEnumerator_ = f_.KeyStore.IntervalEnumerable(_firstKey, _lastKey).GetEnumerator();
        }

        public FunctionPointPair<TX, TY> Current
        {
            get
            {
                if (current_ == null)
                {
                    throw new InvalidOperationException("Enumerator has not been reset");
                }
                return current_;
            }
        }

        private object Current1
        {
            get
            {
                if (current_ == null)
                {
                    throw new InvalidOperationException("Enumerator has not been reset");
                }
                return current_;
            }
        }
        object System.Collections.IEnumerator.Current
        {
            get { return Current1; }
        }

        public bool MoveNext()
        {
            if (current_ == null)
            {
                current_ = new FunctionPointPair<TX, TY>(f_);
                current_.Item1.X = rangeStart_;

            }
            else if (current_.Item2.X.Equals(rangeEnd_))
            {
                current_ = null;
                return false;
            }
            else
            {
                current_.Item1.X = Current.Item2.X;
                idx_ += 1;
            }
            if (rangeStart_.Equals(_firstKey))
            {
                interiorKeysEnumerator_.MoveNext();
            }

            current_.Item2.X = interiorKeysEnumerator_.Current.Item2.Item1;

            return true;
        }

        public void Reset()
        {
            current_ = null;
            interiorKeysEnumerator_.Reset();
        }

        // To detect redundant calls
        private bool disposedValue = false;

        // IDisposable
        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposedValue)
            {
                if (disposing)
                {
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

        public System.Collections.Generic.IEnumerator<FunctionPointPair<TX, TY>> GetEnumerator()
        {
            return this;
        }

        public System.Collections.IEnumerator GetEnumerator1()
        {
            return this;
        }
        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
        {
            return GetEnumerator1();
        }
    }
}
