
using Microsoft.VisualBasic;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
namespace Icm.Localization
{

	/// <summary>
	/// Represents a translation service for a given language.
	/// </summary>
	/// <remarks>The fact that the language is specified allows for very short translation functions, that are provided in the <see cref="ILocalizationServiceExtensions"></see> module.</remarks>
	public interface ILocalizationService
	{

		ILocalizationRepository Repository { get; }

		int Lcid { get; }
	}
}

//=======================================================
//Service provided by Telerik (www.telerik.com)
//Conversion powered by NRefactory.
//Twitter: @telerik
//Facebook: facebook.com/telerik
//=======================================================
