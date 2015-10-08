using System.Runtime.CompilerServices;

namespace Icm.Localization
{

	public static class IPhraseExtensions
	{

		[Extension()]
		public static string Translate(IPhrase phrase, ILocalizationRepository locRepo)
		{
			return phrase.Translate(CultureInfo.CurrentCulture.LCID, locRepo);
		}

	}

}

//=======================================================
//Service provided by Telerik (www.telerik.com)
//Conversion powered by NRefactory.
//Twitter: @telerik
//Facebook: facebook.com/telerik
//=======================================================
