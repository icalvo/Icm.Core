
using Microsoft.VisualBasic;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Runtime.CompilerServices;

namespace Icm.Localization
{

	public static class PhraseFactory
	{

		public static PhraseFormat PhrF(string key, params object[] args)
		{
			return new PhraseFormat(key, args);
		}

		public static PhraseAnd PhrAnd(params object[] args)
		{
			return new PhraseAnd(args);
		}

		[Extension()]
		public static string TransF(ILocalizationRepository repo, string key, params object[] args)
		{
			return PhrF(key, args).Translate(repo);
		}

		[Extension()]
		public static string TransAnd(ILocalizationRepository repo, params object[] args)
		{
			return PhrAnd(args).Translate(repo);
		}


	}

}

//=======================================================
//Service provided by Telerik (www.telerik.com)
//Conversion powered by NRefactory.
//Twitter: @telerik
//Facebook: facebook.com/telerik
//=======================================================
