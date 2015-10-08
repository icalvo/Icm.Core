
using Microsoft.VisualBasic;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Linq;

namespace Icm
{
	public static class ILookupExtensions
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
		[Extension()]
		public static V ItemOrDefault<K, V>(ILookup<K, V> o, K key, Converter<V, V> del, V ifdonot)
		{
			if (o.Contains(key)) {
				return del((V)o(key));
			} else {
				return ifdonot;
			}
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
		[Extension()]
		public static V ItemOrDefault<K, V>(ILookup<K, V> o, K key, V ifdonot)
		{
			if (o.Contains(key)) {
				return (V)o(key);
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
