using System;

namespace Icm
{
    public static class NullableExtensions
    {
        public static T V<T>(this T? n) where T: struct
        {
            if (n.HasValue)
            {
                return n.Value;
            }

            throw new InvalidOperationException();
        }

        public static bool HasNotValue<T>(this T? n) where T : struct
        {
            return !n.HasValue;
        }


        public static object IfNull<T>(this T? o, object subst) where T : struct
        {
            return o ?? subst;
        }
    }
}