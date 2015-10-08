using System.IO;
using System.Runtime.CompilerServices;

namespace Icm.IO
{

	public static class TextWriterExtensions
	{

		/// <summary>
		/// Writes underlined text.
		/// </summary>
		/// <param name="tw"></param>
		/// <param name="s"></param>
		/// <remarks></remarks>
		[Extension()]
		public static void WriteUnderline(TextWriter tw, string s)
		{
			tw.WriteLine(s);
			tw.WriteLine(new string('-', s.Length));
		}

	}

}

//=======================================================
//Service provided by Telerik (www.telerik.com)
//Conversion powered by NRefactory.
//Twitter: @telerik
//Facebook: facebook.com/telerik
//=======================================================
