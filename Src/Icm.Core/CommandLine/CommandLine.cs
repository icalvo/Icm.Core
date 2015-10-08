using System;
using System.Collections.Generic;
using res = My.Resources.CommandLineTools;

namespace Icm.CommandLineTools
{

	/// <summary>
	///  This class abstracts a command line following a GNU-like syntax.
	/// </summary>
	/// <remarks>
	/// <para>This class performs a full processing
	/// of command line arguments. It assumes the following general syntax:</para>
	/// <para>program -namedparam1 subarg1a subarg1b -namedparam2 subarg2a subarg2b [-] unnamedparam1 unnamedparam2</para>
	/// <para>
	/// To use it, create an instance, configure named and unnamed parameters, and process a command line
	/// with <see cref="CommandLine.ProcessArguments"></see>, which must be passed a String array like
	/// the one provided by <see cref="Environment.GetCommandLineArgs"></see> (In fact, calling <see cref="CommandLine.ProcessArguments"></see>
	/// without arguments will process the result of <see cref="Environment.GetCommandLineArgs"></see>). After the processing,
	/// <see cref="CommandLine.ErrorList"></see> will provide the errors found. If no error is found,
	/// <see cref="CommandLine.IsPresent"></see> will say if a named parameter is present,
	/// <see cref="CommandLine.GetValue"></see> and <see cref="CommandLine.GetValues"></see> will
	/// return subarguments of some named parameter, and
	/// <see cref="CommandLine.MainValue"></see> and <see cref="CommandLine.MainValues"></see> will
	/// return the unnamed parameters.
	/// </para>
	/// <para>NOTE: By default, a <see cref="CommandLine"></see> will feature a --help option.</para>
	/// <para><see cref="CommandLineExtensions.Instructions"></see> paints a colorized help text, based on the configured parameters.</para>
	/// <para><see cref="CommandLine"></see> only includes validation of optional/required arguments
	/// or subarguments, existence of parameters, and proper syntax (including double hyphen for
	/// long parameter name, single hyphen for single parameter name, and optional single hyphen alone
	/// for separating the named parameter part from the unnamed arguments.</para>
	/// <para>For any more advanced or specific validations, and of course for actually doing something with
	/// the arguments, the client will have to do it herself.</para>
	/// <para>Also, <see cref="CommandLine.ProcessArguments"></see> do NOT fail, so the client will have to use 
	/// <see cref="CommandLine.HasErrors"></see> and <see cref="CommandLine.ErrorList"></see> properties and react accordingly.</para>
	/// <para>For an example, see Icm.CommandLine.Sample package.</para>
	/// </remarks>
	/// <history>
	/// 	[icalvo]	31/03/2005	Created
	/// </history>
	public class CommandLine
	{

		#region " Attributes "

		// Space-splitted command line arguments

		private string[] arguments_;
		// List of generated errors

		private readonly List<string> errorList_ = new List<string>();
		// Defined named parameters

		private readonly List<NamedParameter> namedParameters_ = new List<NamedParameter>();
		// Named parameters hash table, for finding a parameter given a name. For example, "h" and
		// "help" are keys and they point to the same named parameter.

		private readonly Dictionary<string, NamedParameter> namedParameterDictionary_ = new Dictionary<string, NamedParameter>();
		// Values for each of the named and unnamed parameters

		private readonly ValuesStore valuesStore_ = new ValuesStore();

		private int passedParametersCount_;
		// Length of the greatest long name. Used to format instructions.

		private int parameterLongNamesMaxLength_;
		// Length of the greatest short name. Used to format instructions.

		private int parameterShortNamesMaxLength_;
		// Configuration for unnamed (main) parameters
		private UnnamedParametersConfig unnamedParametersConfig_ = new UnnamedParametersConfig(0, new UnnamedParameter("args", ""), {
			

		});
		// Some named parameters can change the configuration for unnamed parameters, 
		// rendering them optional. This variable will hold either the original, user provided configuration
		// if none of those named parameters is found, or MainArgumentsConfig.None.
			#endregion
		private UnnamedParametersConfig realMainArgumentConfig_;

		public CommandLine()
		{
			AddHelpOption();
		}

		#region " Query "

		#region " Before process "


		public NamedParameter this[string s] {
			get {
				if (namedParameterDictionary_.ContainsKey(s)) {
					return namedParameterDictionary_(s);
				} else {
					return null;
				}
			}
		}

		public bool IsDeclaredHelp {
			get { return IsPresent[res.S_DEFAULT_HELP_LONG]; }
		}

		public int DeclaredOptions {
			get { return passedParametersCount_; }
		}

		#endregion

		#region " After process "

		public bool IsPresent {
			get {
				if (namedParameterDictionary_.ContainsKey(s)) {
					return valuesStore_.ContainsKey(s);
				} else {
					throw new UndefinedOptionException(s);
				}
			}
		}

		public List<string> ErrorList {
			get { return errorList_; }
		}

		public ICollection<string> MainValues {
			get { return valuesStore_.MainValues; }
		}

		public string MainValue {
			get {
				if (valuesStore_.MainValues.Count == 0) {
					return null;
				} else {
					return valuesStore_.MainValues(0);
				}
			}
		}

		public ICollection<string> GetValues {
			get {
				if (namedParameterDictionary_.ContainsKey(s)) {
					return valuesStore_.Values(s);
				} else {
					throw new UndefinedOptionException(s);
				}
			}
		}

		public string GetValue {
			get {
				// Suppose we declare (only) named parameter -e [subarg1] and we find these examples:
				// 1. example.exe
				// 2. example.exe -e
				// 3. example.exe -e 123
				// 3. example.exe -e 123 456
				//
				// Suppose we DON'T declare named parameter -e and we find these examples:
				// 4. example.exe -e
				// 5. example.exe -e 123
				if (namedParameterDictionary_.ContainsKey(s)) {
					if (valuesStore_.ContainsKey(s)) {
						if (valuesStore_.Values(s).Count == 0) {
							// Case 2: There is -e but without subarguments
							return null;
						} else {
							// Case 3: There is -e with subarguments, we return the first one (123)
							return valuesStore_.Values(s)(0);
						}
					} else {
						// Case 1: No -e
						return null;
					}
				} else {
					//Cases 4 and 5: -e has not been declared
					throw new UndefinedOptionException(s);
				}
			}
		}

		public bool HasErrors()
		{
			return errorList_.Count > 0;
		}

		#endregion

		#endregion

		#region " Fluid configuration "

		#region " Main parameters "

		public UnnamedParametersConfig MainParameters {
			get { return unnamedParametersConfig_; }
		}

		/// <summary>
		/// Establishes that unnamed parameters are optional. This is the default value.
		/// </summary>
		/// <returns></returns>
		/// <remarks></remarks>
		public CommandLine MainParametersOptional(UnnamedParameter unboundArgumentsName)
		{
			unnamedParametersConfig_ = new UnnamedParametersConfig(0, unboundArgumentsName, {
				
			});
			return this;
		}

		/// <summary>
		/// Establishes that the number of unnamed parameters must be between min and max.
		/// </summary>
		/// <param name="min"></param>
		/// <param name="argList">Names and descriptions of all arguments be</param>
		/// <returns></returns>
		/// <remarks></remarks>
		public CommandLine MainParametersBetween(int min, IEnumerable<UnnamedParameter> argList)
		{
			unnamedParametersConfig_ = new UnnamedParametersConfig(min, argList.Count, argList);
			return this;
		}

		/// <summary>
		/// Establishes that at least min unnamed parameters will be required.
		/// </summary>
		/// <param name="unboundArgument"></param>
		/// <returns></returns>
		/// <remarks></remarks>
		public CommandLine MainParametersAtLeast(UnnamedParameter unboundArgument, IEnumerable<UnnamedParameter> argList)
		{
			unnamedParametersConfig_ = new UnnamedParametersConfig(argList.Count, unboundArgument, argList);
			return this;
		}

		/// <summary>
		/// Establishes that at most max unnamed parameters will be required.
		/// </summary>
		/// <param name="argList"></param>
		/// <returns></returns>
		/// <remarks></remarks>
		public CommandLine MainParametersAtMost(IEnumerable<UnnamedParameter> argList)
		{
			unnamedParametersConfig_ = new UnnamedParametersConfig(0, argList.Count, argList);
			return this;
		}

		/// <summary>
		/// Establishes that exactly num unnamed parameters will be required.
		/// </summary>
		/// <param name="argList"></param>
		/// <returns></returns>
		/// <remarks></remarks>
		public CommandLine MainParametersExactly(IEnumerable<UnnamedParameter> argList)
		{
			unnamedParametersConfig_ = new UnnamedParametersConfig(argList.Count, argList.Count, argList);
			return this;
		}

		/// <summary>
		/// Establishes that at least one main parameter is required.
		/// </summary>
		/// <returns></returns>
		/// <remarks></remarks>
		public CommandLine MainParametersRequired(UnnamedParameter requiredArg, UnnamedParameter unboundArgument)
		{
			unnamedParametersConfig_ = new UnnamedParametersConfig(1, unboundArgument, { requiredArg });
			return this;
		}

		/// <summary>
		/// Establishes that at least one main parameter is required.
		/// </summary>
		/// <returns></returns>
		/// <remarks></remarks>
		public CommandLine MainParametersRequired(UnnamedParameter arg)
		{
			unnamedParametersConfig_ = new UnnamedParametersConfig(1, arg, { arg });
			return this;
		}

		/// <summary>
		/// Establishes that no unnamed parameters will be admitted.
		/// </summary>
		/// <returns></returns>
		/// <remarks></remarks>
		public CommandLine MainParametersNone()
		{
			unnamedParametersConfig_ = new UnnamedParametersConfig();
			return this;
		}

		#endregion

		#region " Named parameters "

		public List<NamedParameter> NamedParameters {
			get { return namedParameters_; }
		}

		/// <summary>
		/// Adds an optional named parameter.
		/// </summary>
		/// <param name="sShortName"></param>
		/// <param name="sLongName"></param>
		/// <param name="sDescription"></param>
		/// <param name="subargs"></param>
		/// <returns></returns>
		/// <remarks></remarks>
		public CommandLine Optional(string sShortName, string sLongName, string sDescription, params SubArgument[] subargs)
		{

			NamedParameter o = new NamedParameter(sShortName, sLongName, sDescription, false, subargs);

			NamedParameter(o);

			return this;
		}

		/// <summary>
		/// Adds a required named parameter.
		/// </summary>
		/// <param name="sShortName"></param>
		/// <param name="sLongName"></param>
		/// <param name="sDescription"></param>
		/// <param name="subargs"></param>
		/// <returns></returns>
		/// <remarks></remarks>
		public CommandLine Required(string sShortName, string sLongName, string sDescription, params SubArgument[] subargs)
		{

			NamedParameter o = new NamedParameter(sShortName, sLongName, sDescription, true, subargs);

			NamedParameter(o);

			return this;
		}

		/// <summary>
		/// Adds a named parameter.
		/// </summary>
		/// <param name="o"></param>
		/// <returns></returns>
		/// <remarks></remarks>
		public CommandLine NamedParameter(NamedParameter o)
		{
			namedParameterDictionary_.Add(o.ShortName.ToLower, o);
			namedParameterDictionary_.Add(o.LongName.ToLower, o);
			if (o.ShortName.Length > parameterShortNamesMaxLength_) {
				parameterShortNamesMaxLength_ = o.ShortName.Length;
			}
			if (o.LongName.Length > parameterLongNamesMaxLength_) {
				parameterLongNamesMaxLength_ = o.LongName.Length;
			}
			namedParameters_.Add(o);

			return this;
		}

		#endregion

		#endregion

		#region " Processing "

		/// <summary>
		/// Process arguments from the application settings
		/// </summary>
		/// <param name="settings"></param>
		/// <remarks></remarks>
		public void ProcessArguments(System.Configuration.ApplicationSettingsBase settings)
		{
			ProcessArguments(settings, null);
		}

		/// <summary>
		/// Process arguments from an array of strings
		/// </summary>
		/// <param name="args"></param>
		/// <remarks></remarks>
		public void ProcessArguments(params string[] args)
		{
			ProcessArguments(null, args);
		}

		/// <summary>
		/// Process arguments from the application settings and an array of strings
		/// </summary>
		/// <param name="args"></param>
		/// <remarks>
		/// <para>If args is nothing, <see cref="Environment.GetCommandLineArgs"></see> result will used.</para>
		/// <para>Application settings can be also null and in that case they will be ignored.</para>
		/// <para>The args array takes precedence over the application settings.</para>
		/// </remarks>
		public void ProcessArguments(System.Configuration.ApplicationSettingsBase settings, string[] args)
		{
			int i = 0;
			string optStr = null;

			// First we process application settings. They can only provide named arguments.
			if (settings != null) {
				foreach (void opt_loopVariable in namedParameters_) {
					opt = opt_loopVariable;
					if (settings.HasProp(opt.LongName)) {
						ProcessSettingsOption(opt, StringifySetting(settings.GetProp(opt.LongName)));
					}
				}
			}

			// Now we process command line arguments, which will override application settings.


			foreach (void o_loopVariable in namedParameterDictionary_.Values) {
				o = o_loopVariable;
				o.MustReset = true;
			}
			if (args == null) {
				arguments_ = Environment.GetCommandLineArgs;
			} else {
				arguments_ = args;
			}

			realMainArgumentConfig_ = (UnnamedParametersConfig)unnamedParametersConfig_.Clone;
			if (Information.UBound(arguments_) >= 1) {
				i = 1;
				do {
					optStr = arguments_(i);

					if (optStr.StartsWith("-", StringComparison.Ordinal)) {
						// Possible option
						switch (optStr.Length) {
							case 1:
								//Empty long option (-): Not admitted
								errorList_.Add(res.S_ERR_EMPTY_SHORT_OPTION + " (-)");
								break;
							case 2:
								if (optStr == "--") {
									//Empty long option (--): main arguments start
									i += 1;

									ProcessMain(ref i);
								} else {
									//Short option (-o); strip one dash and process
									ProcessOption(optStr.Substring(1), ref i);
								}
								break;
							default:
								if (optStr.StartsWith("--", StringComparison.Ordinal)) {
									//Long option (--longoption); strip two dashes and process
									ProcessOption(optStr.Substring(2), ref i);
								} else {
									//Short option with more than one letter (-option); strip one dash and process
									ProcessOption(optStr.Substring(1), ref i);
								}
								break;
						}
					} else {
						// Main arguments found
						if (realMainArgumentConfig_.Maximum.HasValue && realMainArgumentConfig_.Maximum.V == 0) {
							// No main arguments configured
							errorList_.Add(res.S_ERR_ARGS_WHEN_NOARGS);
						} else {
							ProcessMain(ref i);
						}
					}
				} while (!(i >= arguments_.Length || HasErrors()));

			}

			if (valuesStore_.MainValues.Count < realMainArgumentConfig_.Minimum || valuesStore_.MainValues.Count > realMainArgumentConfig_.Maximum) {
				// Incorrect number of main arguments
				errorList_.Add(res.S_ERR_NOARGS_WHEN_REQUIRED);
			}

			// Required named arguments check
			foreach (void par_loopVariable in namedParameters_) {
				par = par_loopVariable;
				if (par.IsRequired && !valuesStore_.ContainsKey(par.ShortName)) {
					errorList_.Add(string.Format(res.S_ERR_ARG_REQUIRED, par.LongName, par.ShortName));
				}
			}
		}

		#endregion

		#region " Auxiliary "

		protected void AddHelpOption()
		{
			NamedParameter helpopt = new NamedParameter(res.S_DEFAULT_HELP_SHORT, res.S_DEFAULT_HELP_LONG, res.S_DEFAULT_HELP_DESC, false) { MakesMainArgumentsIrrelevant = true };
			NamedParameter(helpopt);
		}

		public void Clean()
		{
			errorList_.Clear();
			passedParametersCount_ = 0;
			parameterLongNamesMaxLength_ = 0;
			parameterShortNamesMaxLength_ = 0;
		}

		/// <summary>
		/// Just adds the rest of the tokens to the collection of main arguments
		/// </summary>
		/// <param name="i"></param>
		/// <remarks></remarks>
		private void ProcessMain(ref int i)
		{
			while (!(i >= arguments_.Length)) {
				valuesStore_.AddMainValue(arguments_(i));
				i += 1;
			}
		}

		/// <summary>
		/// A setting can return in general an array of strings that corresponds to the
		/// subarguments of a named parameter. This functions returns that array from the
		/// object value.
		/// </summary>
		/// <param name="obj"></param>
		/// <returns></returns>
		/// <remarks></remarks>
		private IEnumerable<string> StringifySetting(object obj)
		{
			List<string> result = new List<string>();
			if (obj is Specialized.StringCollection) {
				foreach (void cad_loopVariable in (Specialized.StringCollection)obj) {
					cad = cad_loopVariable;
					result.Add(cad);
				}
			} else if (obj is bool) {
				if (Convert.ToBoolean(obj)) {
					string[] empty = {
						
					};
					return empty;
				} else {
					return null;
				}
			} else {
				result.Add(obj.ToString);
			}
			return result;
		}

		private void ProcessSettingsOption(NamedParameter o, IEnumerable<string> settingValue)
		{
			List<string> opSubargValues = new List<string>();

			if (settingValue == null) {
				return;
			}

			if (valuesStore_.ContainsKey(o.ShortName)) {
				// We admit more than one instance of a named parameter.
			} else {
				valuesStore_.AddParameter(o.ShortName, o.LongName);
			}

			passedParametersCount_ += 1;
			// If this option makes the main arguments irrelevant, we must ignore
			// them. To achieve this, we just change the configuration we will use later to check them.
			if (o.MakesMainArgumentsIrrelevant) {
				realMainArgumentConfig_ = new UnnamedParametersConfig(0, new UnnamedParameter("args", ""), {
					
				});
			}

			// Fill the subarguments to be processed.
			dynamic i = 0;
			while (!(i >= settingValue.Count || settingValue(i).Chars(0) == "-" || (opSubargValues.Count == o.SubArguments.Count && !o.AcceptsUnlimitedSubargs))) {
				opSubargValues.Add(settingValue(i));
				i += 1;
			}

			ProcessSubargs(o, opSubargValues);
		}


		/// <summary>
		///   Process a single option.
		/// </summary>
		/// <param name="op">Option to be analyzed, without hyphens ('-').</param>
		/// <param name="i">Current position of the </param>
		/// <remarks>
		/// <c>i</c> is pointing to the CL arg next to the
		/// current option, and will be updated to point to the next option (string 
		/// starting with hyphen).
		/// </remarks>
		/// <history>
		/// 	[icalvo]	22/12/2005	Created
		/// </history>
		private void ProcessOption(string op, ref int i)
		{
			NamedParameter o = default(NamedParameter);
			List<string> opSubargValues = new List<string>();
			i += 1;
			if (!namedParameterDictionary_.ContainsKey(op.ToLower)) {
				//inexistent option
				errorList_.Add(string.Format("{0}: {1}", res.S_ERR_OPTION_NOT_EXIST, op));
				return;
			}
			o = namedParameterDictionary_(op);

			// We must eat subargument values for this option until:
			// + We find the end of the arguments_.
			// + We find a 'value' that starts with '-';
			// + The option does not accept unlimited values and we have
			//   reached the count of defined subarguments for this option; or
			// One more token:

			while (!(i > Information.UBound(arguments_) || arguments_(i).Chars(0) == "-" || (opSubargValues.Count == o.SubArguments.Count && !o.AcceptsUnlimitedSubargs))) {
				// Fill the subarguments to be processed.
				opSubargValues.Add(arguments_(i));
				i += 1;
			}
			ProcessSettingsOption(o, opSubargValues);
		}

		private void ProcessSubargs(NamedParameter par, List<string> subargValues)
		{
			int i = 0;
			SubArgument subarg = default(SubArgument);
			string value = null;

			// Match required subarguments.

			// Obtain subarg and value
			if (par.SubArguments.Count > i) {
				subarg = par.SubArguments(i);
			} else {
				subarg = null;
			}
			if (subargValues.Count > i) {
				value = subargValues(i);
			} else {
				value = null;
			}

			// Loop until a null subarg, null value or not-required subarg is found.

			while (!(subarg == null || value == null || !subarg.IsRequired)) {
				// Add the value
				valuesStore_.AddValue(par.ShortName, par.LongName, value);

				// obtain next subarg and value
				i += 1;
				if (par.SubArguments.Count > i) {
					subarg = par.SubArguments(i);
				} else {
					subarg = null;
				}
				if (subargValues.Count > i) {
					value = subargValues(i);
				} else {
					value = null;
				}
			}

			// Check current situation: Is there a mismatch between required
			// subargs and values?
			if (subarg == null) {
				if (value != null) {
					ErrorList.AppendFormat(res.S_ERR_TOO_MANY_SUBARGS, par.LongName);
				}
				return;
			} else if (subarg.IsRequired) {
				ErrorList.AppendFormat(res.S_ERR_NOT_ENOUGH_SUBARGS, par.LongName);
				return;
			} else {
				if (value == null) {
					// There are no more values
					return;
				}
			}

			// Match optional / list subarguments
			if (subarg.IsOptional) {

				while (!(subarg == null || value == null)) {
					valuesStore_.AddValue(par.ShortName, par.LongName, value);
					i += 1;
					subarg = par.SubArguments.Count > i ? par.SubArguments(i) : null;
					value = subargValues.Count > i ? subargValues(i) : null;
				}

				if (subarg == null && (value != null)) {
					ErrorList.AppendFormat(res.S_ERR_TOO_MANY_SUBARGS, par.LongName);
				}
			} else {
				valuesStore_.AddValues(par.ShortName, par.LongName, subargValues.Subrange(i));
			}
		}

		#endregion

	}

}


//=======================================================
//Service provided by Telerik (www.telerik.com)
//Conversion powered by NRefactory.
//Twitter: @telerik
//Facebook: facebook.com/telerik
//=======================================================
