using System.Collections.Generic;

namespace Icm.Localization
{

	public class DictionaryLocalizationRepository : Dictionary<LocalizationKey, string>, ILocalizationRepository
	{
		public DictionaryLocalizationRepository(Dictionary<LocalizationKey, string> dict) 
		{
			foreach (var element in dict) {
				Add(element.Key, element.Value);
			}
		}

	    public string this[int lcid, string key]
	    {
	        get
	        {
                var multkey = new LocalizationKey(key, lcid);

                return ContainsKey(multkey) ? this[multkey] : null;
	        }
	    }
	}
}
