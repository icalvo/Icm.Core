namespace Icm.Localization
{
	public static class LocalizationServiceExtensions
	{
		public static string TransAnd(this ILocalizationService locService, params object[] args)
		{
			return new PhraseAnd(args).Translate(locService.Lcid, locService.Repository);
		}
        
		public static string TransF(this ILocalizationService locService, string key, params object[] args)
		{
			return new PhraseFormat(key, args).Translate(locService.Lcid, locService.Repository);
		}
        
		public static string Trans(this ILocalizationService locService, string key)
		{
			return locService.Repository[locService.Lcid, key];
		}
	}
}