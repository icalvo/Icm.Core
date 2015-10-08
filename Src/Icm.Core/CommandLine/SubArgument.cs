
using Microsoft.VisualBasic;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;

namespace Icm.CommandLineTools
{
	/// <summary>
	///  Subargument of a command line option.
	/// </summary>
	/// <remarks></remarks>

	public class SubArgument
	{

		private readonly SubArgumentType _type;
		private readonly string _description;

		private readonly string _name;
		public string Name {
			get { return _name; }
		}

		public string Description {
			get { return _description; }
		}

		public SubArgumentType Type {
			get { return _type; }
		}

		private SubArgument(string sName, SubArgumentType eType) : this(sName, "", eType)
		{
		}

		private SubArgument(string sName, string sDescription, SubArgumentType eType)
		{
			_name = sName;
			_description = sDescription;
			_type = eType;
		}

		public static SubArgument List(string sName)
		{
			return new SubArgument(sName, SubArgumentType.List);
		}

		public static SubArgument Optional(string sName)
		{
			return new SubArgument(sName, SubArgumentType.Optional);
		}

		public static SubArgument Required(string sName)
		{
			return new SubArgument(sName, SubArgumentType.Required);
		}

		public static SubArgument List(string sName, string sDescription)
		{
			return new SubArgument(sName, sDescription, SubArgumentType.List);
		}

		public static SubArgument Optional(string sName, string sDescription)
		{
			return new SubArgument(sName, sDescription, SubArgumentType.Optional);
		}

		public static SubArgument Required(string sName, string sDescription)
		{
			return new SubArgument(sName, sDescription, SubArgumentType.Required);
		}

		public bool IsList()
		{
			return Type == SubArgumentType.List;
		}

		public bool IsOptional()
		{
			return Type == SubArgumentType.Optional;
		}

		public bool IsRequired()
		{
			return Type == SubArgumentType.Required;
		}
	}

}

//=======================================================
//Service provided by Telerik (www.telerik.com)
//Conversion powered by NRefactory.
//Twitter: @telerik
//Facebook: facebook.com/telerik
//=======================================================
