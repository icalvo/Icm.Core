
using Microsoft.VisualBasic;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
namespace Icm
{

	/// <summary>
	/// Objects with a name or, if the name is null, a prefix with which a new name
	/// can be generated (adding a unique number to the prefix)
	/// </summary>
	/// <remarks></remarks>
	public interface INamed
	{
		string Name { get; set; }
	}

}

//=======================================================
//Service provided by Telerik (www.telerik.com)
//Conversion powered by NRefactory.
//Twitter: @telerik
//Facebook: facebook.com/telerik
//=======================================================
