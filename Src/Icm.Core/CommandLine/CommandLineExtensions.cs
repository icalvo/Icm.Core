
using Microsoft.VisualBasic;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using res = My.Resources.CommandLineTools;
using Icm.Collections;
using Icm.ColorConsole;

namespace Icm.CommandLineTools
{

	public static class CommandLineExtensions
	{

		/// <summary>
		///  Prints the set of instructions for a CommandLine.
		/// </summary>
		/// <remarks></remarks>
		[Extension()]
		public static void Instructions(CommandLine cmdline)
		{
			NamedParameter o = default(NamedParameter);

			Console.Write("{0}: ", res.S_HELP_USAGE, System.Diagnostics.Process.GetCurrentProcess.ProcessName);

			using (new ColorSetting(ConsoleColor.Cyan)) {
				Console.Write(System.Diagnostics.Process.GetCurrentProcess.ProcessName);

				foreach (void param_loopVariable in cmdline.NamedParameters) {
					param = param_loopVariable;
					if (param.IsRequired) {
						Console.Write(" --{0}", param.LongName);
					} else {
						Console.Write(" [--{0}", param.LongName);
					}
					foreach (void subarg_loopVariable in param.SubArguments) {
						subarg = subarg_loopVariable;
						if (subarg.IsRequired) {
							Console.Write(" " + subarg.Name);
						} else {
							Console.Write(" [{0}]", subarg.Name);
						}
					}
					if (!param.IsRequired) {
						Console.Write("]");
					}

				}
			}
			using (new ColorSetting(ConsoleColor.Green)) {

				var _with1 = cmdline.MainParameters;
				for (i = 1; i <= _with1.Minimum; i++) {
					Console.Write(" {0}", _with1.GetArgumentName(i).Name);
				}
				if (_with1.Maximum.HasValue) {
					for (i = _with1.Minimum + 1; i <= _with1.Maximum.V; i++) {
						Console.Write(" [{0}]", _with1.GetArgumentName(i).Name);
					}
				} else {
					Console.Write(" {{{0}}}", _with1.UnboundArgumentsName.Name);
				}
			}
			using (new ColorSetting(ConsoleColor.White)) {
				Console.WriteLine();
				Console.WriteLine();
				Console.Write("{0}:", res.S_OPTIONS);
				Console.WriteLine();

				foreach ( o in cmdline.NamedParameters) {
					List<string> subargs = new List<string>();

					foreach (SubArgument s in o.SubArguments) {
						if (s.IsRequired) {
							subargs.Add(s.Name);
						} else if (s.IsOptional) {
							subargs.Add(string.Format("[{0}]", s.Name));
						} else if (s.IsList) {
							subargs.Add(string.Format("{{{0}}}", s.Name));
						}
					}
					using (new ColorSetting(ConsoleColor.Cyan)) {
						Console.WriteLine("  -{0} {1}", o.ShortName, subargs.JoinStr(" "));
						Console.WriteLine("  --{0} {1}", o.LongName, subargs.JoinStr(" "));
					}
					Console.WriteLine("      {0}{1}", o.IsRequired ? res.S_HELP_REQUIRED : "", o.Description);
				}

				Console.WriteLine();
				var _with2 = cmdline.MainParameters;
				foreach (void key_loopVariable in _with2.ParamDictionary.Keys) {
					key = key_loopVariable;
					using (new ColorSetting(ConsoleColor.Green)) {
						Console.WriteLine("  {0}", key);
					}
					Console.WriteLine("      {0}", _with2.ParamDictionary(key).Description);
				}
				Console.WriteLine();
				Console.WriteLine(res.S_SUBARGUMENTS_SEPARATED);
			}

		}

		/// <summary>
		///  Prints the set of values for a CommandLine.
		/// </summary>
		/// <remarks></remarks>
		[Extension()]
		public static void Print(CommandLine cmdline)
		{
			using (new ColorSetting(ConsoleColor.Cyan)) {
				foreach (void param_loopVariable in cmdline.NamedParameters) {
					param = param_loopVariable;
					if (cmdline.IsPresent(param.ShortName)) {
						ColorConsole.ColorConsole.Write(ConsoleColor.Cyan, "{0}=", param.LongName);
						ColorConsole.ColorConsole.WriteLine(ConsoleColor.White, "({0})", cmdline.GetValues(param.ShortName).JoinStr("", "\"", ",", "\"", ""));
					} else {
						ColorConsole.ColorConsole.Write(ConsoleColor.Cyan, "{0}=", param.LongName);
						ColorConsole.ColorConsole.WriteLine(ConsoleColor.White, "UNDEFINED");
					}
				}
			}
			using (new ColorSetting(ConsoleColor.Green)) {

				Console.WriteLine(cmdline.MainValues.JoinStr("(", "\"", ",", "\"", ")"));
			}

			Console.WriteLine();

		}

		[Extension()]
		public static int? GetInteger(CommandLine cmdline, string key)
		{
			if (cmdline.IsPresent(key)) {
				return Convert.ToInt32(cmdline.GetValue(key));
			} else {
				return null;
			}
		}
	}

}

//=======================================================
//Service provided by Telerik (www.telerik.com)
//Conversion powered by NRefactory.
//Twitter: @telerik
//Facebook: facebook.com/telerik
//=======================================================
