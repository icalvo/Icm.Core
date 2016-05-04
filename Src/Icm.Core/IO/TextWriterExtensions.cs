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
		public static void WriteUnderline(this TextWriter tw, string s)
		{
			tw.WriteLine(s);
			tw.WriteLine(new string('-', s.Length));
		}

	}

}