namespace Icm.Localization
{

	/// <summary>
	///  Methods to manage localized strings.
	/// </summary>
	public interface ILocalizationRepository
	{


		string this[int lcid, string key] { get; }
	}
}

//=======================================================
//Service provided by Telerik (www.telerik.com)
//Conversion powered by NRefactory.
//Twitter: @telerik
//Facebook: facebook.com/telerik
//=======================================================
