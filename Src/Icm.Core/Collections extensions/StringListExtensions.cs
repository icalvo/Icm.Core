
using Microsoft.VisualBasic;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Globalization;

namespace Icm.Collections
{

	/// <summary>
	///   String collection with extra features.
	/// </summary>
	/// <remarks>
	/// </remarks>
	/// <history>
	/// 	[icalvo]	19/08/2004	Created
	/// </history>
	public static class StringListExtensions
	{

		/// <summary>
		///     Appends a formatted string to the end of the collection,
		/// which is built from a format string and its corresponding parameters.
		/// </summary>
		/// <param name="list"></param>
		/// <param name="fmt"></param>
		/// <param name="params"></param>
		/// <remarks>
		/// </remarks>
		/// <history>
		/// 	[icalvo]	23/06/2005	Created
		///     [icalvo]    07/03/2006  Documented
		/// </history>
		[Extension()]
		public static void AppendFormat(IList<string> list, string fmt, params object[] @params)
		{
			list.Add(string.Format(CultureInfo.CurrentCulture, fmt, @params));
		}

		/// <summary>
		///     Appends a formatted string to the end of the collection,
		/// which is built from a format provider, a format string and its corresponding parameters.
		/// </summary>
		/// <param name="list"></param>
		/// <param name="fp"></param>
		/// <param name="fmt"></param>
		/// <param name="params"></param>
		/// <remarks>
		/// </remarks>
		/// <history>
		/// 	[icalvo]	23/06/2005	Created
		///     [icalvo]    07/03/2006  Documented
		/// </history>
		[Extension()]
		public static void AppendFormat(IList<string> list, IFormatProvider fp, string fmt, params object[] @params)
		{
			list.Add(string.Format(fp, fmt, @params));
		}

		/// <summary>
		///     Prepends a formatted string to the start of the collection,
		/// which is built from a format string and its corresponding parameters.
		/// </summary>
		/// <param name="list"></param>
		/// <param name="fmt"></param>
		/// <param name="params"></param>
		/// <remarks>
		/// </remarks>
		/// <history>
		/// 	[icalvo]	23/06/2005	Created
		///     [icalvo]    07/03/2006  Documented
		/// </history>
		[Extension()]
		public static void PrependFormat(IList<string> list, string fmt, params object[] @params)
		{
			list.Insert(0, string.Format(CultureInfo.CurrentCulture, fmt, @params));
		}

		/// <summary>
		///     Prepends a formatted string to the start of the collection,
		/// which is built from a format provider, a format string and its corresponding parameters.
		/// </summary>
		/// <param name="list"></param>
		/// <param name="fp"></param>
		/// <param name="fmt"></param>
		/// <param name="params"></param>
		/// <remarks>
		/// </remarks>
		/// <history>
		/// 	[icalvo]	23/06/2005	Created
		///     [icalvo]    07/03/2006  Documented
		/// </history>
		[Extension()]
		public static void PrependFormat(IList<string> list, IFormatProvider fp, string fmt, params object[] @params)
		{
			list.Insert(0, string.Format(fp, fmt, @params));
		}

		/// <summary>
		///     Inserts a formatted string on a given position of the collection,
		/// which is built from a format string and its corresponding parameters.
		/// </summary>
		/// <param name="list"></param>
		/// <param name="idx"></param>
		/// <param name="fmt"></param>
		/// <param name="params"></param>
		/// <remarks>
		/// </remarks>
		/// <history>
		/// 	[icalvo]	23/06/2005	Created
		///     [icalvo]    07/03/2006  Documented
		/// </history>
		[Extension()]
		public static void InsertFormat(IList<string> list, int idx, string fmt, params object[] @params)
		{
			list.Insert(idx, string.Format(CultureInfo.CurrentCulture, fmt, @params));
		}

		/// <summary>
		///     Inserts a formatted string on a given position of the collection,
		/// which is built from a format provider, a format string and its corresponding parameters.
		/// </summary>
		/// <param name="list"></param>
		/// <param name="idx"></param>
		/// <param name="fp"></param>
		/// <param name="fmt"></param>
		/// <param name="params"></param>
		/// <remarks>
		/// </remarks>
		/// <history>
		/// 	[icalvo]	23/06/2005	Created
		///     [icalvo]    07/03/2006  Documented
		/// </history>
		[Extension()]
		public static void InsertFormat(IList<string> list, int idx, IFormatProvider fp, string fmt, params object[] @params)
		{
			list.Insert(idx, string.Format(fp, fmt, @params));
		}

	}

}

//=======================================================
//Service provided by Telerik (www.telerik.com)
//Conversion powered by NRefactory.
//Twitter: @telerik
//Facebook: facebook.com/telerik
//=======================================================
