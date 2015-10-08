
using Microsoft.VisualBasic;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Runtime.CompilerServices;

namespace Icm.Collections
{

	public static class IListExtensions
	{

		/// <summary>
		/// Clears the list and adds a given amount of new items (calling the empty constructor).
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <param name="list"></param>
		/// <param name="quantity"></param>
		/// <remarks></remarks>
		[Extension()]
		public static void Initialize<T>(IList<T> list, int quantity) where T : new()
		{
			list.Clear();
			for (int i = 1; i <= quantity; i++) {
				list.Add(new T());
			}
		}

		/// <summary>
		/// Binary search over a range given by start index and length, with custom comparer.
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <param name="lst"></param>
		/// <param name="index"></param>
		/// <param name="length"></param>
		/// <param name="value"></param>
		/// <param name="comparer"></param>
		/// <returns></returns>
		/// <remarks>
		///   <see cref="List(Of T)"></see> have a BinarySearch method but IList not. This
		/// is an implementation for IList.
		/// </remarks>
		[Extension()]
		public static int Search<T>(IList<T> lst, int index, int length, T value, IComparer<T> comparer)
		{
			if ((comparer == null)) {
				comparer = System.Collections.Generic.Comparer<T>.Default;
			}
			int num = index;
			int num2 = ((index + length) - 1);
			while ((num <= num2)) {
				int num3 = (num + ((num2 - num) >> 1));
				int cmp = comparer.Compare(lst(num3), value);
				if ((cmp == 0)) {
					return num3;
				}
				if ((cmp < 0)) {
					num = (num3 + 1);
				} else {
					num2 = (num3 - 1);
				}
			}
			return !num;
		}

		/// <summary>
		/// Binary search over a range given by start index and length.
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <param name="lst"></param>
		/// <param name="index"></param>
		/// <param name="length"></param>
		/// <param name="value"></param>
		/// <returns></returns>
		/// <remarks>
		///   <see cref="List(Of T)"></see> have a BinarySearch method but IList not. This
		/// is an implementation for IList.
		/// </remarks>
		[Extension()]
		public static int Search<T>(IList<T> lst, int index, int length, T value)
		{
			return lst.Search(index, length, value, null);
		}

		/// <summary>
		/// Binary search.
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <param name="lst"></param>
		/// <param name="value"></param>
		/// <returns></returns>
		/// <remarks>
		///   <see cref="List(Of T)"></see> have a BinarySearch method but IList not. This
		/// is an implementation for IList.
		/// </remarks>
		[Extension()]
		public static int Search<T>(IList<T> lst, T value)
		{
			return lst.Search(0, lst.Count, value, null);
		}

		/// <summary>
		/// Binary search with custom comparer.
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <param name="lst"></param>
		/// <param name="value"></param>
		/// <param name="comparer"></param>
		/// <returns></returns>
		/// <remarks>
		///   <see cref="List(Of T)"></see> have a BinarySearch method but IList not. This
		/// is an implementation for IList.
		/// </remarks>
		[Extension()]
		public static int Search<T>(IList<T> lst, T value, IComparer<T> comparer)
		{
			return lst.Search(0, lst.Count, value, comparer);
		}

		/// <summary>
		/// </summary>
		/// <param name="sc"></param>
		/// <remarks>
		///   <see cref="List(Of T)"></see> have an AddRange method but IList not. This
		/// is an implementation for IList.
		/// </remarks>
		/// <history>
		/// 	[icalvo]	23/06/2005	Created
		///     [icalvo]    07/03/2006  Documented
		/// </history>
		[Extension()]
		public static void AddRange<T>(IList<T> l, IEnumerable<T> sc)
		{
			foreach (void s_loopVariable in sc) {
				s = s_loopVariable;
				l.Add(s);
			}
		}

		/// <summary>
		///   This methods builds a new List containing a tail of the current IList.
		/// </summary>
		/// <param name="start"></param>
		/// <returns></returns>
		/// <remarks>
		/// </remarks>
		/// <history>
		/// 	[icalvo]	23/06/2005	Created
		///     [icalvo]    07/03/2006  Documented
		/// </history>
		[Extension()]
		public static IList<T> Subrange<T>(IList<T> l, int start)
		{
			List<T> r = new List<T>();
			for (int i = start; i <= l.Count - 1; i++) {
				r.Add(l(i));
			}
			return r;
		}

	}
}

//=======================================================
//Service provided by Telerik (www.telerik.com)
//Conversion powered by NRefactory.
//Twitter: @telerik
//Facebook: facebook.com/telerik
//=======================================================
