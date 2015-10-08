
using Microsoft.VisualBasic;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;

namespace Icm.Compilation
{

	public abstract class CompiledParameterInfo
	{


		private string _name;
		public string Name {
			get { return _name; }
			protected set { _name = value; }
		}

		public abstract string ArgType { get; }

	}

	public class CompiledParameterInfo<T> : CompiledParameterInfo
	{


		private static readonly string _argType;
		static CompiledParameterInfo()
		{
			_argType = typeof(T).Name;
		}

		public CompiledParameterInfo(string name)
		{
			this.Name = name;
		}

		public override string ArgType {
			get { return _argType; }
		}
	}

}

//=======================================================
//Service provided by Telerik (www.telerik.com)
//Conversion powered by NRefactory.
//Twitter: @telerik
//Facebook: facebook.com/telerik
//=======================================================
