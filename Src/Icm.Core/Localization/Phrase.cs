namespace Icm.Localization
{

	public abstract class Phrase : IPhrase
	{

		public abstract string Translate(int lcid, ILocalizationRepository locRepo);

		protected static object TranslateObject(ILocalizationRepository locRepo, object trans)
		{
			dynamic translit = trans as IPhrase;
			if (translit == null) {
				return trans;
			} else {
				return translit.Translate(locRepo);
			}
		}

	}

}

//=======================================================
//Service provided by Telerik (www.telerik.com)
//Conversion powered by NRefactory.
//Twitter: @telerik
//Facebook: facebook.com/telerik
//=======================================================
