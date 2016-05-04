using System.Collections.Generic;

namespace Icm.Localization
{
	public class DictionaryFixedCultureLocalizationRepository : Dictionary<string, string>, ILocalizationRepository
	{
		public DictionaryFixedCultureLocalizationRepository(Dictionary<string, string> dict)
		{
			foreach (var element in dict) {
				Add(element.Key, element.Value);
			}
		}

        string ILocalizationRepository.this[int lcid, string key] 
            => ContainsKey(key) ? this[key] : null;
	}
}