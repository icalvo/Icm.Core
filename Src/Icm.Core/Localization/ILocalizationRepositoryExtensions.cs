using System.Globalization;
using System.Runtime.CompilerServices;

namespace Icm.Localization
{

	public static class LocalizationRepositoryExtensions
	{
		public static string Trans(this ILocalizationRepository locRepo, string key)
		{
			return locRepo[CultureInfo.CurrentCulture.LCID, key];
		}

	}

}
