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

		public static string TransF(this ILocalizationRepository repo, string key, params object[] args)
		{
			return PhrF(key, args).Translate(repo);
		}

		public static string TransAnd(this ILocalizationRepository repo, params object[] args)
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
