using System;

namespace Icm
{
    public static class TotalOrderExtensions
    {
        public static T LstIfNull<T>(this ITotalOrder<T> totalOrder, T? obj) where T : struct, IComparable<T>
        {
            return obj ?? totalOrder.Least();
        }

        public static T GstIfNull<T>(this ITotalOrder<T> totalOrder, T? obj) where T : struct, IComparable<T>
        {
            return obj ?? totalOrder.Greatest();
        }

        public static T LstIfNull<T>(this ITotalOrder<T> totalOrder, Nullable2<T> obj) where T : IComparable<T>
        {
            return obj.HasValue 
                ? obj.Value
                : totalOrder.Least();
        }

        public static T GstIfNull<T>(this ITotalOrder<T> totalOrder, Nullable2<T> obj) where T : IComparable<T>
        {
            return obj.HasValue
                ? obj.Value
                : totalOrder.Greatest();
        }

    }
}