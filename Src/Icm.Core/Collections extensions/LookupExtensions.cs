using System;
using System.Linq;

namespace Icm
{
	public static class LookupExtensions
	{

		/// <summary>
		/// ItemOrDefault implementation for ILookup.
		/// </summary>
		/// <typeparam name="K"></typeparam>
		/// <typeparam name="V"></typeparam>
		/// <param name="o"></param>
		/// <param name="key"></param>
		/// <param name="del"></param>
		/// <param name="ifdonot"></param>
		/// <returns></returns>
		/// <remarks></remarks>
		public static V ItemOrDefault<K, V>(this ILookup<K, V> o, K key, Converter<V, V> del, V ifdonot)
		{
		    return o.Contains(key) 
                ? del((V)o[key]) 
                : ifdonot;
		}

	    /// <summary>
		/// ItemOrDefault implementation for ILookup.
		/// </summary>
		/// <typeparam name="K"></typeparam>
		/// <typeparam name="V"></typeparam>
		/// <param name="o"></param>
		/// <param name="key"></param>
		/// <param name="ifdonot"></param>
		/// <returns></returns>
		/// <remarks></remarks>
		public static V ItemOrDefault<K, V>(this ILookup<K, V> o, K key, V ifdonot)
	    {
	        return o.Contains(key) 
                ? (V) o[key] 
                : ifdonot;
	    }
	}
}