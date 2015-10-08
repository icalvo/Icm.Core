
using Microsoft.VisualBasic;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
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
