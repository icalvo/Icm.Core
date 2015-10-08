
using Microsoft.VisualBasic;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using Icm.Collections;
namespace Icm.Collections.Generic
{
	public static class IDictionaryExtensions
	{

		/// <summary>
		/// Merges two dictionaries.
		/// </summary>
		/// <typeparam name="K"></typeparam>
		/// <typeparam name="V"></typeparam>
		/// <param name="dic"></param>
		/// <param name="other"></param>
		/// <remarks>The first dictionary is modified; when a key exists in both of them, the 
		/// value of the second dictionary is chosen.</remarks>
		[Extension()]
		public static void Merge<K, V>(IDictionary<K, V> dic, IDictionary<K, V> other)
		{
			foreach (K k_loopVariable in other.Keys) {
				k = k_loopVariable;
				dic(k) = other(k);
			}
		}

		/// <summary>
		/// If the given key exists, returns the corresponding value transformed by the given function.
		/// Otherwise, returns the given default value.
		/// </summary>
		/// <typeparam name="K"></typeparam>
		/// <typeparam name="V"></typeparam>
		/// <param name="o"></param>
		/// <param name="key"></param>
		/// <param name="del"></param>
		/// <param name="ifdonot"></param>
		/// <returns></returns>
		/// <remarks></remarks>
		[Extension()]
		public static V ItemOrDefault<K, V>(IDictionary<K, V> o, K key, Converter<V, V> del, V ifdonot)
		{
			if (o.ContainsKey(key)) {
				return del(o(key));
			} else {
				return ifdonot;
			}
		}

		/// <summary>
		/// If the given key exists, returns the corresponding value. Otherwise, returns the given default value.
		/// </summary>
		/// <typeparam name="K"></typeparam>
		/// <typeparam name="V"></typeparam>
		/// <param name="o"></param>
		/// <param name="key"></param>
		/// <param name="ifdonot"></param>
		/// <returns></returns>
		/// <remarks></remarks>
		[Extension()]
		public static V ItemOrDefault<K, V>(IDictionary<K, V> o, K key, V ifdonot)
		{
			if (o.ContainsKey(key)) {
				return o(key);
			} else {
				return ifdonot;
			}
		}

	}
}

//=======================================================
//Service provided by Telerik (www.telerik.com)
//Conversion powered by NRefactory.
//Twitter: @telerik
//Facebook: facebook.com/telerik
//=======================================================
