namespace Icm.Localization
{

	public abstract class Phrase : IPhrase
	{

		public abstract string Translate(int lcid, ILocalizationRepository locRepo);

		protected static object TranslateObject(ILocalizationRepository locRepo, object trans)
		{
		    var translit = trans as IPhrase;
		    return translit == null ? trans : translit.Translate(locRepo);
		}
	}

}