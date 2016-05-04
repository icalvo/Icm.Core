using System;
using System.Collections.Generic;
using Icm.Collections.Generic.StructKeyStructValue;

namespace Icm.Functions
{

    /// <summary>
    /// A keyed function is a math function from TX to TY which value is determined by a series
    /// of "keys", or pairs (x As TX, y As TY) that fulfills F(x) = y. The rest of values is
    /// obtained by means of an interpolation that will be based on the keys.
    /// </summary>
    /// <typeparam name="TX">Domain</typeparam>
    /// <typeparam name="TY">Image</typeparam>
    /// <remarks></remarks>
    public abstract class BaseKeyedMathFunction<TX, TY> : MathFunction<TX, TY>, ISortedCollection<TX, TY>, IKeyedMathFunction<TX, TY> where TX : struct, IComparable<TX> where TY : struct, IComparable<TY>
    {


        private ISortedCollection<TX, TY> store_;
        protected BaseKeyedMathFunction(ITotalOrder<TX> otx, ITotalOrder<TY> oty, ISortedCollection<TX, TY> coll) : base(otx, oty)
        {
            store_ = coll;
        }

        protected BaseKeyedMathFunction(IKeyedMathFunction<TX, TY> fc) : base(fc.OrdenTotalTX, fc.OrdenTotalTY)
        {
            store_ = fc.KeyStore;
        }

        public ISortedCollection<TX, TY> KeyStore => store_;

        public RangeIterator<TX, TY> Range(TX rangeStart, TX rangeEnd, bool includeExtremes)
        {
            return new RangeIterator<TX, TY>(this, rangeStart, rangeEnd, includeExtremes);
        }

        public RangeIterator<TX, TY> RangeFrom(TX rangeStart, bool includeExtremes)
        {
            return new RangeIterator<TX, TY>(this, rangeStart, GstX(), includeExtremes);
        }

        public RangeIterator<TX, TY> RangeTo(TX rangeEnd, bool includeExtremes)
        {
            return new RangeIterator<TX, TY>(this, LstX(), rangeEnd, includeExtremes);
        }

        public RangeIterator<TX, TY> TotalRange(bool includeExtremes)
        {
            return new RangeIterator<TX, TY>(this, LstX(), GstX(), includeExtremes);
        }

        public TX? PreviousOrLst(TX d)
        {
            return d.Equals(LstX()) ? d : store_.Previous(d);
        }

        public void Add(TX key, TY value)
        {
            if (AbsMaxXY().Y.CompareTo(value) < 0)
            {
                AbsMaxXY().X = key;
            }
            if (AbsMinXY().Y.CompareTo(value) > 0)
            {
                AbsMinXY().X = key;
            }
            store_.Add(key, value);
        }

        public bool ContainsKey(TX key)
        {
            return store_.ContainsKey(key);
        }

        public int Count()
        {
            return store_.Count();
        }

        public TX? GetFreeKey(TX desiredKey)
        {
            return store_.GetFreeKey(desiredKey);
        }

        public IEnumerable<Vector2<Tuple<TX, TY?>>> IntervalEnumerable(TX? intStart, TX? intEnd)
        {
            return store_.IntervalEnumerable(intStart, intEnd);
        }

        public IEnumerable<Vector2<Tuple<TX, TY?>>> IntervalEnumerable(Vector2<TX?> intf)
        {
            return store_.IntervalEnumerable(intf);
        }

        public IEnumerable<Vector2<Tuple<TX, TY?>>> IntervalEnumerable()
        {
            return store_.IntervalEnumerable(null, null);
        }

        public override TY this[TX x]
        {
            get { return store_[x]; }
            set { store_[x] = value; }
        }

        public TX? KeyOrNext(TX key)
        {
            return store_.KeyOrNext(key);
        }

        public TX? KeyOrPrev(TX key)
        {
            return store_.KeyOrPrev(key);
        }

        public TX? Next(TX key)
        {
            return store_.Next(key);
        }

        public IEnumerable<Tuple<TX, TY?>> PointEnumerable(TX? intStart, TX? intEnd)
        {
            return store_.PointEnumerable(intStart, intEnd);
        }

        public IEnumerable<Tuple<TX, TY?>> PointEnumerable(Vector2<TX?> intf)
        {
            return store_.PointEnumerable(intf);
        }

        public IEnumerable<Tuple<TX, TY?>> PointEnumerable()
        {
            return store_.PointEnumerable(null, null);
        }

        public TX? Previous(TX key)
        {
            return store_.Previous(key);
        }

        public void Remove(TX key)
        {
            store_.Remove(key);
        }

        public override string ToString()
        {
            return store_.ToString();
        }

        public string ToString(TX fromKey, TX toKey)
        {
            return store_.ToString(fromKey, toKey);
        }

        public ITotalOrder<TX> TotalOrder => store_.TotalOrder;

        public override FunctionPoint<TX, TY> AbsMaxXY()
        {
            return MaxXY(TotalOrder.Least(), TotalOrder.Greatest());
        }

        public override FunctionPoint<TX, TY> AbsMinXY()
        {
            return MinXY(TotalOrder.Least(), TotalOrder.Greatest());
        }

    }

}
