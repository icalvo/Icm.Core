using System.Globalization;

namespace Icm.Localization
{
	public static class PhraseExtensions
	{
		public static string Translate(this IPhrase phrase, ILocalizationRepository locRepo)
		{
			return phrase.Translate(CultureInfo.CurrentCulture.LCID, locRepo);
		}
	}
}
