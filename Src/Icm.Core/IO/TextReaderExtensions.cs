using System.Globalization;
using System.IO;

namespace Icm.IO
{
	public static class TextReaderExtensions
	{
		public static string Format(this TextReader tr, params object[] args)
		{
			var template = tr.ReadToEnd();
			tr.Close();
			return string.Format(CultureInfo.CurrentCulture, template, args);
		}

	}

}