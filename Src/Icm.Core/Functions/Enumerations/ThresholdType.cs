
using Microsoft.VisualBasic;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
namespace Icm.Functions
{
	public enum ThresholdType
	{
		/// <summary>
		/// X &lt; threshold
		/// </summary>
		/// <remarks></remarks>
		RightOpen,

		/// <summary>
		/// X &lt;= threshold
		/// </summary>
		/// <remarks></remarks>
		RightClosed,

		/// <summary>
		/// X &gt; threshold
		/// </summary>
		/// <remarks></remarks>
		LeftOpen,

		/// <summary>
		/// X &gt;= threshold
		/// </summary>
		/// <remarks></remarks>
		LeftClosed
	}
}

//=======================================================
//Service provided by Telerik (www.telerik.com)
//Conversion powered by NRefactory.
//Twitter: @telerik
//Facebook: facebook.com/telerik
//=======================================================
