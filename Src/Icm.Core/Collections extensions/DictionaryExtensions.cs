using System;
using System.Collections.Generic;

namespace Icm.Collections.Generic
{
	public static class DictionaryExtensions
	{

		/// <summary>
		/// Merges two dictionaries.
		/// </summary>
		/// <typeparam name="TK"></typeparam>
		/// <typeparam name="TV"></typeparam>
		/// <param name="dic"></param>
		/// <param name="other"></param>
		/// <remarks>The first dictionary is modified; when a key exists in both of them, the 
		/// value of the second dictionary is chosen.</remarks>
		public static void Merge<TK, TV>(this IDictionary<TK, TV> dic, IDictionary<TK, TV> other)
		{
			foreach (TK k in other.Keys) {
				dic[k] = other[k];
			}
		}

		/// <summary>
		/// If the given key exists, returns the corresponding value transformed by the given function.
		/// Otherwise, returns the given default value.
		/// </summary>
		/// <typeparam name="TK"></typeparam>
		/// <typeparam name="TV"></typeparam>
		/// <param name="o"></param>
		/// <param name="key"></param>
		/// <param name="del"></param>
		/// <param name="ifdonot"></param>
		/// <returns></returns>
		/// <remarks></remarks>
		public static TV ItemOrDefault<TK, TV>(this IDictionary<TK, TV> o, TK key, Converter<TV, TV> del, TV ifdonot)
		{
		    return o.ContainsKey(key) 
                ? del(o[key]) 
                : ifdonot;
		}

	    /// <summary>
		/// If the given key exists, returns the corresponding value. Otherwise, returns the given default value.
		/// </summary>
		/// <typeparam name="TK"></typeparam>
		/// <typeparam name="TV"></typeparam>
		/// <param name="o"></param>
		/// <param name="key"></param>
		/// <param name="ifdonot"></param>
		/// <returns></returns>
		/// <remarks></remarks>
		public static TV ItemOrDefault<TK, TV>(this IDictionary<TK, TV> o, TK key, TV ifdonot)
	    {
	        return o.ContainsKey(key) 
                ? o[key] 
                : ifdonot;
	    }
	}
}