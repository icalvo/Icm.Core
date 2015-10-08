
using Microsoft.VisualBasic;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.IO;

namespace Icm.IO
{
	/// <summary>
	///   Tools for managing files.
	/// </summary>
	/// <history>
	/// 	[icalvo]	05/04/2005	Created for FormatFile
	/// 	[icalvo]	05/04/2005	Documentation
	/// </history>
	public static class FileTools
	{


		/// <summary>
		///  Uses a file as a format template for String.Format.
		/// </summary>
		/// <param name="templatefn">Name of the file that will be used as template.</param>
		/// <param name="args">Arguments for the Format call.</param>
		/// <returns>String with the formatted template.</returns>
		/// <remarks>
		///  This function makes a common task: it takes a file, reads it into a String,
		///  and passes this String as the first argument of String.Format(t, args).
		///  The remaining optional arguments of String.Format are taken directly.
		/// </remarks>
		/// <history>
		///     [icalvo]    31/03/2005  Created
		/// 	[icalvo]	05/04/2005	Removed from Icm.Tools
		/// 	[icalvo]	05/04/2005	Documentation
		/// </history>
		public static string FormatFile(string templatefn, params object[] args)
		{
			dynamic sr = File.OpenText(templatefn);
			return FormatFile(sr, args);
		}

		public static string FormatFile(TextReader tr, params object[] args)
		{
			dynamic template = tr.ReadToEnd;
			tr.Close();
			return string.Format(CultureInfo.CurrentCulture, template, args);
		}

	}

}


//=======================================================
//Service provided by Telerik (www.telerik.com)
//Conversion powered by NRefactory.
//Twitter: @telerik
//Facebook: facebook.com/telerik
//=======================================================
