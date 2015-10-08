
using Microsoft.VisualBasic;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Text;

namespace Icm
{

	public static class Tools
	{


		/// <summary>
		/// String join function that accepts a ParamArray of strings.
		/// </summary>
		/// <param name="sep"></param>
		/// <param name="strs"></param>
		/// <returns></returns>
		/// <remarks>Ignores null and empty strings.</remarks>
		public static string JoinStr(string sep, params string[] strs)
		{
			StringBuilder sb = new StringBuilder();
			int i = Information.LBound(strs);
			bool firstOne = true;
			do {
				if (i > Information.UBound(strs)) {
					return sb.ToString;
				} else if (string.IsNullOrEmpty(strs(i))) {
					i += 1;
				} else {
					if (firstOne) {
						sb.Append(strs(i));
						firstOne = false;
					} else {
						sb.Append(sep + strs(i));
					}
					i += 1;
				}
			} while (true);
		}

	}

}

//=======================================================
//Service provided by Telerik (www.telerik.com)
//Conversion powered by NRefactory.
//Twitter: @telerik
//Facebook: facebook.com/telerik
//=======================================================
