using System.Collections.Generic;

namespace Icm.Localization
{

	/// <summary>
	/// A Phrase encapsulates a hierarchical phrase structure without having to
	/// translate anything beforehand, using a key and arrays of child values (which can be literals
	/// or other phrases). This allows to build complex strings without depending on a
	/// concrete repository of strings, and translating them only where the repository is most 
	/// appropriately available.
	/// </summary>
	/// <remarks>
	/// Use the PhraseFactory.Phr functions to abbreviate construction of phrases.
	/// </remarks>
	public class PhraseFormat : Phrase
	{

		public string PhraseKey { get; set; }


		private List<object> _arguments;
		public ICollection<object> Arguments {
			get { return _arguments; }
		}

		public PhraseFormat(string key, params object[] args)
		{
			this.PhraseKey = key;
			if (args == null) {
				_arguments = new List<object>();
			} else {
				_arguments = new List<object>(args);
			}
		}

		public PhraseFormat(string key, IEnumerable<object> args)
		{
			this.PhraseKey = key;
			if (args == null) {
				_arguments = new List<object>();
			} else {
				_arguments = new List<object>(args);
			}
		}

		public override string Translate(int lcid, ILocalizationRepository locRepo)
		{
			string queryKey = null;
			dynamic args = Arguments.Select(trans => TranslateObject(locRepo, trans)).ToArray;
			// La clave de la BD lleva un sufijo con el n�mero de par�metros
			if (args != null && args.Count > 0) {
				queryKey = string.Format("{0}_{1}", PhraseKey, args.Count);
			} else {
				queryKey = PhraseKey;
			}

			dynamic translatedString = locRepo(lcid, queryKey);


			if (translatedString == null) {
				translatedString = string.Format("{0}-{1}", PhraseKey, lcid);
				if (args != null && args.Count > 0) {
					args.Select(obj => obj.ToString).JoinStr(",");
					translatedString += string.Format("({0})", args.Select(obj => string.Format("'{0}'", obj)).JoinStr(","));
				}
				return translatedString;
			} else {
				if (args != null && args.Length > 0) {
					return string.Format(translatedString, args);
				} else {
					return translatedString;
				}
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
