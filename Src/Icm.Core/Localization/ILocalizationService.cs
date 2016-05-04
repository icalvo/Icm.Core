namespace Icm.Localization
{

	/// <summary>
	/// Represents a translation service for a given language.
	/// </summary>
	/// <remarks>The fact that the language is specified allows for very short translation functions, that are provided in the <see cref="LocalizationServiceExtensions"></see> module.</remarks>
	public interface ILocalizationService
	{

		ILocalizationRepository Repository { get; }

		int Lcid { get; }
	}
}
