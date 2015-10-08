
using Microsoft.VisualBasic;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using res = My.Resources.CommandLineTools;

namespace Icm.CommandLineTools
{

	/// <summary>
	///  Command line option.
	/// </summary>
	/// <remarks></remarks>
	public class NamedParameter
	{

		private string _shortName;
		private string _longName;
		private readonly List<SubArgument> _subarguments;
		private readonly bool _isRequired;

		private bool _unlimitedSubargs;

		public NamedParameter(string sShortName, string sLongName, string sDescription, bool bRequired, params SubArgument[] subargs)
		{
			Debug.Assert(sShortName.Length <= sLongName.Length);
			ShortName = sShortName;
			LongName = sLongName;
			Description = sDescription;
			_isRequired = bRequired;
			_subarguments = new List<SubArgument>();
			if (subargs != null) {
				foreach (void subarg_loopVariable in subargs) {
					subarg = subarg_loopVariable;
					AddSubArgument(subarg);
				}
			}
			MustReset = false;
		}

		/// <summary>
		/// If this property is True, it indicates that we must eliminate all the present
		/// subarguments.
		/// </summary>
		/// <value></value>
		/// <returns></returns>
		/// <remarks></remarks>
		public bool MustReset { get; set; }

		/// <summary>
		///  If true, the presence of this option in the command line makes the processor to
		/// ignore the main arguments because they are unneeded.
		/// </summary>
		/// <value></value>
		/// <returns></returns>
		/// <remarks></remarks>
		public bool MakesMainArgumentsIrrelevant { get; set; }

		/// <summary>
		/// Does this option accept an unlimited list of subarguments?
		/// </summary>
		/// <value></value>
		/// <returns></returns>
		/// <remarks></remarks>
		public bool AcceptsUnlimitedSubargs {
			get { return _unlimitedSubargs; }
		}

		public bool IsRequired {
			get { return _isRequired; }
		}

		/// <summary>
		///  Short option (for example -h)
		/// </summary>
		/// <value></value>
		/// <returns></returns>
		/// <remarks></remarks>
		public string ShortName {
			get { return _shortName; }
			set {
				Debug.Assert(value.Length >= 1);
				_shortName = value;
			}
		}

		/// <summary>
		///  Long option (for example --help)
		/// </summary>
		/// <value></value>
		/// <returns></returns>
		/// <remarks></remarks>
		public string LongName {
			get { return _longName; }
			set {
				Debug.Assert(value.Length > 1);
				_longName = value;
			}
		}

		/// <summary>
		///  Description of this option.
		/// </summary>
		/// <value></value>
		/// <returns></returns>
		/// <remarks></remarks>
		public string Description { get; set; }

		public List<SubArgument> SubArguments {
			get { return _subarguments; }
		}

		/// <summary>
		///  Adds a subargument.
		/// </summary>
		/// <param name="sa">Subargument to be added</param>
		/// <remarks></remarks>
		public void AddSubArgument(SubArgument sa)
		{
			// require
			//   p /= Void
			//   parameters.Last.isOptional implies p.isOptional
			//   not parameters.Last.isList
			Debug.Assert((sa != null), res.S_ERR_VOID_SUBARGUMENT);

			if (_subarguments.Count > 0) {
				SubArgument lastsa = (SubArgument)_subarguments(_subarguments.Count - 1);
				Debug.Assert(!lastsa.IsOptional || sa.IsOptional, string.Format(res.S_ERR_NON_OPTIONAL_AFTER_OPTIONAL, sa.Name, lastsa.Name));
				Debug.Assert(!lastsa.IsList, res.S_ERR_SUBARG_AFTER_LIST);
			}
			if (sa.IsList) {
				_unlimitedSubargs = true;
			}
			_subarguments.Add(sa);
		}

	}
}

//=======================================================
//Service provided by Telerik (www.telerik.com)
//Conversion powered by NRefactory.
//Twitter: @telerik
//Facebook: facebook.com/telerik
//=======================================================
