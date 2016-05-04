using System.Globalization;

namespace Icm.Localization
{

	public class ResourceLocalizationRepository : ILocalizationRepository
	{


		private readonly System.Resources.ResourceManager _resourceManager;
		public ResourceLocalizationRepository(System.Resources.ResourceManager rman)
		{
			_resourceManager = rman;
		}

	    public string this[int lcid, string key]
	    {
	        get {
                var culture = CultureInfo.GetCultureInfo(lcid);
                return _resourceManager.GetString(key, culture) ?? key;
            }
	    }
	}

}
