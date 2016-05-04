using System;

namespace Icm
{

    public abstract class BaseTotalOrder<T> : ITotalOrder<T> where T : IComparable<T>
    {

        public abstract T Least();

        public abstract T Long2T(long d);

        public abstract T Greatest();

        public abstract long T2Long(T t);

        public virtual int Compare(T x, T y)
        {
            return T2Long(x).CompareTo(T2Long(y));
        }

        public virtual T Next(T t)
        {
            long longVal = T2Long(t);
            long longGst = T2Long(Greatest());
            T result;

            do
            {
                if (longVal >= longGst)
                {
                    throw new ArgumentOutOfRangeException(nameof(t));
                }
                longVal += 1;
                result = Long2T(longVal);
            } while (!(t.CompareTo(result) < 0));

            return result;
        }

        public virtual T Previous(T t)
        {
            long longVal = T2Long(t);
            long longLst = T2Long(Least());
            T result;

            do
            {
                if (longVal <= longLst)
                {
                    throw new ArgumentOutOfRangeException(nameof(t));
                }
                
                longVal -= 1;
                result = Long2T(longVal);
            } while (!(t.CompareTo(result) > 0));

            return result;
        }

    }
}
