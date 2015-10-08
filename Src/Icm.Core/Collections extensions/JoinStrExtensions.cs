
using Microsoft.VisualBasic;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Text;
using System.Runtime.CompilerServices;

namespace Icm.Collections
{
	public static class JoinStrExtensions
	{

		/// <summary>
		/// String join function that accepts a ParamArray of strings.
		/// </summary>
		/// <param name="list"></param>
		/// <param name="separator">Regular separator, separates words except the two last ones.</param>
		/// <param name="finalSeparator">Final separator, separates the two last ones.</param>
		/// <returns></returns>
		/// <remarks>Ignores null and empty strings.</remarks>
		[Extension()]
		public static string JoinStr(IEnumerable<string> list, string separator, string finalSeparator)
		{
			StringBuilder sb = new StringBuilder();
			int i = list.Count;

			dynamic nonEmpty = list.Where(s => !string.IsNullOrEmpty(s));

			if (nonEmpty.Count >= 1) {
				sb.Append(nonEmpty(0));
			}

			for (i = 1; i <= nonEmpty.Count - 2; i++) {
				sb.Append(separator + nonEmpty(i));
			}

			if (nonEmpty.Count >= 2) {
				sb.Append(finalSeparator + nonEmpty(i));
			}

			return sb.ToString;
		}


		/// <summary>
		///   Usual string join, with separator.
		/// </summary>
		/// <param name="list"></param>
		/// <param name="separator">Separator string</param>
		/// <returns>The joined string.</returns>
		/// <remarks>
		/// </remarks>
		/// <history>
		/// 	[icalvo]	19/08/2004	Created
		/// 	[icalvo]	07/03/2006	Documented
		/// </history>
		[Extension()]
		public static string JoinStr(IEnumerable<string> list, string separator)
		{
			return string.Join(separator, list.Where(s => !string.IsNullOrEmpty(s)).ToArray);
		}

		/// <summary>
		///   Extended join, with separator and global prefix/suffix.
		/// </summary>
		/// <param name="list"></param>
		/// <param name="globalprefix">Global prefix</param>
		/// <param name="separator">Separator string</param>
		/// <param name="globalsuffix">Global suffix</param>
		/// <returns>The joined string.</returns>
		/// <remarks>
		///  <para>This method joins the collection with the separator and
		/// (if the original array is not empty), the global prefix and suffix
		/// are added.</para>
		/// </remarks>
		/// <example>
		///   The following call will render a string in vector form, with
		/// parentheses and comma-separated, BUT will return the empty string
		/// if the collection has zero elements:
		/// <code>
		///     Dim myCollection As List(Of String)
		///     Dim htmlList As String
		///     ...
		///     htmlList = myCollection.Join("(", ", ", ")")
		/// </code>
		/// </example>
		/// <history>
		/// 	[icalvo]	07/03/2006	Created
		/// 	[icalvo]	07/03/2006	Documented
		/// </history>
		[Extension()]
		public static string JoinStr(IEnumerable<string> list, string globalprefix, string separator, string globalsuffix)
		{
			if (list.Count == 0) {
				return "";
			} else {
				return globalprefix + list.JoinStr(separator) + globalsuffix;
			}
		}

		/// <summary>
		///   Extended join, with item prefixes/suffixes, and global prefix/suffix.
		/// </summary>
		/// <param name="list"></param>
		/// <param name="globalprefix">Global prefix</param>
		/// <param name="itemprefix">Item prefix</param>
		/// <param name="itemsuffix">Item suffix</param>
		/// <param name="globalsuffix">Global suffix</param>
		/// <returns>The joined string.</returns>
		/// <remarks>
		///  <para>This method is really a combination of a Map operation and a Join.</para>
		///  <para>The Map operation substitutes each element <c>x</c> with:
		///  <code>
		///     itemprefix &amp; x &amp; itemsuffix
		///  </code>
		/// </para>
		/// <para>Then, the resulting array is joined with the empty separator and
		/// finally (if the original array is not empty), the global prefix and suffix
		/// are added.</para>
		/// </remarks>
		/// <example>
		///   The following call will convert a string collection into an HTML list,
		/// but will return the empty string if the collection has zero elements:
		/// <code>
		///     Dim myCollection As List(Of String)
		///     Dim htmlList As String
		///     ...
		///     htmlList = myCollection.Join("&lt;ul>", "&lt;li>", "&lt;/li>", "&lt;/ul>")
		/// </code>
		///   The following example generates a pretty printed text list:
		/// <code>
		///     Dim myCollection As List(Of String)
		///     Dim textList As String
		///     ...
		///     textList = myCollection.Join("My list:" &amp; vbCrLf, " - ", vbCrLf, "")
		/// </code>
		/// </example>
		/// <history>
		/// 	[icalvo]	07/03/2006	Created
		/// 	[icalvo]	07/03/2006	Documented
		///     [icalvo]    17/03/2006  BUG: first itemprefix and last itemsuffix added
		/// </history>
		[Extension()]
		public static string JoinStr(IEnumerable<string> list, string globalprefix, string itemprefix, string itemsuffix, string globalsuffix)
		{
			if (list.Count == 0) {
				return "";
			} else {
				return globalprefix + itemprefix + list.JoinStr(itemsuffix + itemprefix) + itemsuffix + globalsuffix;
			}
		}

		/// <summary>
		///   Extended join, with item prefixes/suffixes, separators and global prefix/suffix.
		/// </summary>
		/// <param name="list"></param>
		/// <param name="globalprefix">Global prefix</param>
		/// <param name="itemprefix">Item prefix</param>
		/// <param name="separator">Separator</param>
		/// <param name="itemsuffix">Item suffix</param>
		/// <param name="globalsuffix">Global suffix</param>
		/// <returns>The joined string.</returns>
		/// <remarks>
		///  <para>This method is really a combination of a Map operation and a Join.</para>
		///  <para>The map operation substitutes each element <c>x</c> with:
		///  <code>
		///     itemprefix &amp; x &amp; itemsuffix
		///  </code>
		/// </para>
		/// <para>Then, the resulting array is joined with the given separator and
		/// finally (if the original array is not empty), the global prefix and suffix
		/// are added.</para>
		/// </remarks>
		/// <example>
		///   The following call will convert a string collection into an HTML list,
		/// pretty printed,
		/// but will return the empty string if the collection has zero elements:
		/// <code>
		///     Dim myCollection As List(Of String)
		///     Dim htmlList As String
		///     ...
		///     htmlList = myCollection.Join("&lt;ul>", "&lt;li>", vbCrLf, "&lt;/li>", "&lt;/ul>")
		/// </code>
		///   The following example generates a parenthesized, comma-sepparated, quoted string list:
		/// <code>
		///     Dim myCollection As List(Of String)
		///     Dim textList As String
		///     ...
		///     textList = myCollection.Join("(", "'", ", ", "'", ")")
		/// </code>
		///    It would return, for example, <c>('str1', 'str2', 'str3')</c>.
		/// </example>
		/// <history>
		/// 	[icalvo]	17/03/2006	Created
		/// 	[icalvo]	17/03/2006	Documented
		/// </history>
		[Extension()]
		public static string JoinStr(IEnumerable<string> list, string globalprefix, string itemprefix, string separator, string itemsuffix, string globalsuffix)
		{
			if (list.Count == 0) {
				return "";
			} else {
				return globalprefix + itemprefix + list.JoinStr(itemsuffix + separator + itemprefix) + itemsuffix + globalsuffix;
			}
		}


		/// <summary>
		/// String join extension function for lists of strings. It also
		/// accepts a mapping function for the strings.
		/// </summary>
		/// <param name="list"></param>
		/// <param name="sep"></param>
		/// <param name="conv"></param>
		/// <returns></returns>
		/// <remarks></remarks>
		[Extension()]
		public static string JoinStr<T>(IEnumerable<T> list, string sep, Func<T, string> conv = null)
		{
			StringBuilder sb = new StringBuilder();
			int i = 0;
			bool firstOne = true;
			if (conv == null) {
				conv = obj => obj.ToString;
			}
			do {
				if (i > list.Count - 1) {
					return sb.ToString;
				} else if (string.IsNullOrEmpty(conv(list(i)))) {
					i += 1;
				} else {
					if (firstOne) {
						sb.Append(conv(list(i)));
						firstOne = false;
					} else {
						sb.Append(sep + conv(list(i)));
					}
					i += 1;
				}
			} while (true);
		}




		/// <summary>
		/// String join extension function for collections of strings. It also
		/// accepts a mapping function for the strings.
		/// </summary>
		/// <param name="list"></param>
		/// <param name="sep"></param>
		/// <param name="conv"></param>
		/// <returns></returns>
		/// <remarks></remarks>
		[Extension()]
		public static string JoinStr<T>(ICollection list, string sep, Func<T, string> conv)
		{
			StringBuilder sb = new StringBuilder();
			int i = 0;

			bool firstOne = true;
			do {
				dynamic item = (T)list(i);
				if (i > list.Count - 1) {
					return sb.ToString;
				} else if (string.IsNullOrEmpty(conv(item))) {
					i += 1;
				} else {
					if (firstOne) {
						sb.Append(conv(item));
						firstOne = false;
					} else {
						sb.Append(sep + conv(item));
					}
					i += 1;
				}
			} while (true);
		}


		/// <summary>
		///  Joins a collection of String-convertible objects with a given separator.
		/// </summary>
		/// <param name="col">Collection of String-convertible objects.</param>
		/// <param name="separator">Separator</param>
		/// <returns>New string created by joining col with the separator.</returns>
		/// <remarks>
		/// </remarks>
		/// <history>
		/// 	[icalvo]	19/08/2004	Created
		///     [icalvo]    31/03/2005  Documented
		///     [icalvo]    31/03/2005  o declaration integrated in For
		/// </history>
		[Extension()]
		public static string JoinStrCol(ICollection col, string separator)
		{
			if (col == null) {
				return "";
			}

			string[] a = new string[col.Count];
			int i = 0;

			foreach (object o in col) {
				a(i) = o.ToString();
				i += 1;
			}
			return string.Join(separator, a);
		}
	}

}

//=======================================================
//Service provided by Telerik (www.telerik.com)
//Conversion powered by NRefactory.
//Twitter: @telerik
//Facebook: facebook.com/telerik
//=======================================================
