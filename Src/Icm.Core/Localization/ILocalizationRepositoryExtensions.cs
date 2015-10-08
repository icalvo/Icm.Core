
using Microsoft.VisualBasic;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Runtime.CompilerServices;

namespace Icm.Localization
{

	public static class ILocalizationRepositoryExtensions
	{

		[Extension()]
		public static string Trans(ILocalizationRepository locRepo, string key)
		{
			return locRepo(CultureInfo.CurrentCulture.LCID, key);
		}

	}

}

//=======================================================
//Service provided by Telerik (www.telerik.com)
//Conversion powered by NRefactory.
//Twitter: @telerik
//Facebook: facebook.com/telerik
//=======================================================
