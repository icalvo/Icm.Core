
using Microsoft.VisualBasic;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
namespace Icm.Localization
{
	public class DictionaryFixedCultureLocalizationRepository : Dictionary<string, string>, ILocalizationRepository
	{

		public DictionaryFixedCultureLocalizationRepository() : base()
		{
		}

		public DictionaryFixedCultureLocalizationRepository(Dictionary<string, string> dict) : base()
		{

			foreach (void element_loopVariable in dict) {
				element = element_loopVariable;
				Add(element.Key, element.Value);
			}
		}

		public string ItemForCulture {
			get {
				if (ContainsKey(key)) {
					return Item(key);
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
