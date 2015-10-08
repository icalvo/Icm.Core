using System.Collections.Generic;

namespace Icm.Localization
{

	public class DictionaryLocalizationRepository : Dictionary<LocalizationKey, string>, ILocalizationRepository
	{

		public DictionaryLocalizationRepository() : base()
		{
		}

		public DictionaryLocalizationRepository(Dictionary<LocalizationKey, string> dict) : base()
		{

			foreach (void element_loopVariable in dict) {
				element = element_loopVariable;
				this.Add(element.Key, element.Value);
			}
		}

		public string ItemForCulture {
			get {
				dynamic multkey = new LocalizationKey(key, lcid);

				if (ContainsKey(multkey)) {
					return Item(multkey);
				} else {
					return null;
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
