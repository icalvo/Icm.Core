
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
	/// </summary>
	/// <remarks>
	/// Since <see cref="Icm.Text.Replacer"></see> uses TextReaders as
	/// input, and you usually want to perform replacements on in-memory strings or
	/// files that you know the name or have a stream, these functions come handy
	/// to obtain a corresponding TextReader.
	/// </remarks>
	public class TextReaderFactory
	{

		/// <summary>
		/// Get a TextReader given a string.
		/// </summary>
		/// <param name="str"></param>
		/// <returns></returns>
		/// <remarks></remarks>
		public static StringReader FromString(string str)
		{
			return new StringReader(str);
		}

		/// <summary>
		/// Get a TextReader given a file name.
		/// </summary>
		/// <param name="fnin"></param>
		/// <returns></returns>
		/// <remarks></remarks>
		public static StreamReader FromFilename(string fnin)
		{
			return FromStream(new FileStream(fnin, FileMode.Open, FileAccess.Read));
		}

		/// <summary>
		/// Get a TextReader given a stream.
		/// </summary>
		/// <param name="tr"></param>
		/// <param name="enc"></param>
		/// <returns></returns>
		/// <remarks></remarks>
		public static StreamReader FromStream(Stream tr, Encoding enc = null)
		{
			return new StreamReader(tr, enc ?? Encoding.UTF8);
		}

	}
}

//=======================================================
//Service provided by Telerik (www.telerik.com)
//Conversion powered by NRefactory.
//Twitter: @telerik
//Facebook: facebook.com/telerik
//=======================================================
