
using Microsoft.VisualBasic;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
namespace Icm
{
	/// <summary>
	/// Generic parser interface.
	/// </summary>
	/// <remarks>
	/// Function Parse() parses the string and appends the obtained tokens to the Tokens list. It also appends
	/// any error to the Errors list. If you need to start from scratch with an already used ITokenParser,
	/// you should use Initialize before (this will clear the Tokens and Errors lists).
	/// </remarks>
	public interface ITokenParser
	{

		// Tokens recognized so far

		IEnumerable<string> Tokens { get; }

		IEnumerable<ParseError> Errors { get; }

		void Initialize();

		void Parse(string line);
	}
}

//=======================================================
//Service provided by Telerik (www.telerik.com)
//Conversion powered by NRefactory.
//Twitter: @telerik
//Facebook: facebook.com/telerik
//=======================================================
