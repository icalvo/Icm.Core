
using Microsoft.VisualBasic;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Globalization;

namespace Icm.Localization
{
	public struct LocalizationKey
	{
		public string Keyword { get; set; }
		public int LCID { get; set; }

		public LocalizationKey(string keyword, int lcid)
		{
			this.Keyword = keyword;
			this.LCID = lcid;
		}
	}
}

//=======================================================
//Service provided by Telerik (www.telerik.com)
//Conversion powered by NRefactory.
//Twitter: @telerik
//Facebook: facebook.com/telerik
//=======================================================
