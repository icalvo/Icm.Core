
using Microsoft.VisualBasic;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.IO;
using System.Text;

namespace Icm.IO
{

	/// <summary>
	/// 
	/// </summary>
	/// <remarks>
	/// Since <see cref="Icm.Text.Replacer"></see> uses TextWriters as
	/// output, and you usually want to produce in-memory strings or
	/// files that you know the name or have a stream, these functions come handy
	/// to obtain a corresponding TextWriter.
	/// </remarks>
	public class TextWriterFactory
	{

		/// <summary>
		/// Get a TextWriter given a file name.
		/// </summary>
		/// <param name="fnout"></param>
		/// <returns></returns>
		/// <remarks></remarks>
		public static TextWriter FromFilename(string fnout)
		{
			return FromStream(new FileStream(fnout, FileMode.Create, FileAccess.Write));
		}

		/// <summary>
		/// Get a TextWriter given a stream.
		/// </summary>
		/// <param name="tw"></param>
		/// <param name="enc"></param>
		/// <returns></returns>
		/// <remarks></remarks>
		public static TextWriter FromStream(Stream tw, Encoding enc = null)
		{
			return new StreamWriter(tw, enc ?? Encoding.UTF8);
		}

		/// <summary>
		/// Get a TextWriter given a string builder.
		/// </summary>
		/// <param name="sb"></param>
		/// <returns></returns>
		/// <remarks></remarks>
		public static TextWriter FromBuilder(StringBuilder sb)
		{
			return new StringWriter(sb, CultureInfo.CurrentCulture);
		}

	}
}

//=======================================================
//Service provided by Telerik (www.telerik.com)
//Conversion powered by NRefactory.
//Twitter: @telerik
//Facebook: facebook.com/telerik
//=======================================================
