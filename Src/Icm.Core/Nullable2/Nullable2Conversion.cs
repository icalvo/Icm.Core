namespace Icm
{
    /// <summary>
    /// Conversions of Nullable2 from/to Nullable
    /// </summary>
    /// <remarks>
    /// Unfortunately, the Nullable restriction to struct types forbids a direct solution via
    /// conversion operators (conversion operators don't accept generic parameters). 
    /// </remarks>
    public static class Nullable2Conversion
    {
        public static Nullable2<T> ToNullable2<T>(T? other) where T : struct
        {
            // Optimizator: don't do the following because it returns 0 for a null 'other':
            // If(other.HasValue, other.Value, Nothing)
            if (other.HasValue)
            {
                return other.Value;
            }

            return default(Nullable2<T>);
        }

        public static T? ToNullable<T>(this Nullable2<T> other) where T : struct
        {
            // Optimizator: don't do the following because it returns 0 for a null 'other':
            // If(other.HasValue, other.Value, Nothing)
            if (other.HasValue)
            {
                return other.Value;
            }

            return null;
        }
    }

}
