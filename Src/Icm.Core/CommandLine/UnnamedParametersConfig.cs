using System;
using System.Collections.Generic;
using res = My.Resources.CommandLineTools;

namespace Icm.CommandLineTools
{
	public class UnnamedParametersConfig : ICloneable
	{

		#region "Attributes"

		private readonly Dictionary<string, UnnamedParameter> _paramDictionary = new Dictionary<string, UnnamedParameter>();
		private readonly List<UnnamedParameter> _argumentNameList = new List<UnnamedParameter>();
		private readonly UnnamedParameter _unboundArgumentsName;
		private readonly int _minimum;

		private readonly int? _maximum;
		#endregion

		/// <summary>
		/// Constructor for cloning
		/// </summary>
		/// <param name="minimum"></param>
		/// <param name="maximum"></param>
		/// <param name="unboundArgument"></param>
		/// <param name="argNameList"></param>
		/// <remarks></remarks>
		private UnnamedParametersConfig(int minimum, int? maximum, UnnamedParameter unboundArgument, IEnumerable<UnnamedParameter> argNameList = null)
		{
			_minimum = minimum;
			_maximum = maximum;
			if (unboundArgument != null) {
				_paramDictionary.Add(unboundArgument.Name, unboundArgument);
			}
			_unboundArgumentsName = unboundArgument;
			StoreArgList(argNameList);
		}

		private void StoreArgList(IEnumerable<UnnamedParameter> argNameList)
		{
			if (argNameList != null) {
				foreach (void arg_loopVariable in argNameList) {
					arg = arg_loopVariable;
					if (_paramDictionary.ContainsKey(arg.Name)) {
						if (!object.ReferenceEquals(_paramDictionary(arg.Name), arg)) {
							throw new ArgumentException(string.Format("You have passed two Unnamed Parameters with the same name ({0}). If they are of the same kind, use the same UnnamedParameter object for both. Otherwise, use different names", arg.Name));
						}
					} else {
						_paramDictionary.Add(arg.Name, arg);
					}
				}
				_argumentNameList.AddRange(argNameList);
			}

		}

		public UnnamedParametersConfig()
		{
			_minimum = 0;
			_maximum = 0;
		}

		/// <summary>
		/// Initializes a new instance of the UnnamedParametersConfig class.
		/// </summary>
		/// <param name="minimum">Minimum number of arguments</param>
		/// <param name="unboundArgumentsName">Name for the optional arguments from minimum+1 beyond</param>
		/// <param name="argNameList">Names for the required arguments between 0 and minimum</param>
		public UnnamedParametersConfig(int minimum, UnnamedParameter unboundArgumentsName, IEnumerable<UnnamedParameter> argNameList)
		{
			if (argNameList.Count != minimum) {
				throw new ArgumentException(res.S_ERR_REQUIRED_MAIN_ARGUMENTS_WITHOUT_NAME);
			}
			_minimum = minimum;
			_maximum = null;
			_unboundArgumentsName = unboundArgumentsName;
			StoreArgList(argNameList);
		}

		public UnnamedParametersConfig(int minimum, int maximum, IEnumerable<UnnamedParameter> argNameList)
		{
			if (argNameList.Count != maximum) {
				throw new ArgumentException(res.S_ERR_REQUIRED_MAIN_ARGUMENTS_WITHOUT_NAME);
			}
			_minimum = minimum;
			_maximum = maximum;
			StoreArgList(argNameList);
		}

		public int Minimum {
			get { return _minimum; }
		}

		public int? Maximum {
			get { return _maximum; }
		}

		public UnnamedParameter UnboundArgumentsName {
			get { return _unboundArgumentsName; }
		}

		public Dictionary<string, UnnamedParameter> ParamDictionary {
			get { return _paramDictionary; }
		}

		public UnnamedParameter GetArgumentName {
			get { return _argumentNameList(i - 1); }
		}

		public object Clone()
		{
			return new UnnamedParametersConfig(_minimum, _maximum, _unboundArgumentsName, _argumentNameList);
		}
	}

}

//=======================================================
//Service provided by Telerik (www.telerik.com)
//Conversion powered by NRefactory.
//Twitter: @telerik
//Facebook: facebook.com/telerik
//=======================================================
