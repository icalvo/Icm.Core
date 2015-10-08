
using Microsoft.VisualBasic;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Runtime.CompilerServices;

namespace Icm.Localization
{
	public static class ILocalizationServiceExtensions
	{

		[Extension()]
		public static string TransAnd(ILocalizationService locService, params object[] args)
		{
			return new PhraseAnd(args).Translate(locService.Lcid, locService.Repository);
		}

		[Extension()]
		public static string TransF(ILocalizationService locService, string key, params object[] args)
		{
			return new PhraseFormat(key, args).Translate(locService.Lcid, locService.Repository);
		}

		[Extension()]
		public static string Trans(ILocalizationService locService, string key)
		{
			return locService.Repository.ItemForCulture(locService.Lcid, key);
		}
	}
}

//=======================================================
//Service provided by Telerik (www.telerik.com)
//Conversion powered by NRefactory.
//Twitter: @telerik
//Facebook: facebook.com/telerik
//=======================================================
