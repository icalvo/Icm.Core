
using Microsoft.VisualBasic;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
namespace Icm.Localization
{

	public class ResourceLocalizationRepository : ILocalizationRepository
	{


		private readonly System.Resources.ResourceManager _resourceManager;
		public ResourceLocalizationRepository(System.Resources.ResourceManager rman)
		{
			_resourceManager = rman;
		}

		public string ItemForCulture {
			get {
				dynamic culture = CultureInfo.GetCultureInfo(lcid);
				return _resourceManager.GetString(key, culture) ?? key;
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
