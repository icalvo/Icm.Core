
using Microsoft.VisualBasic;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Text;
using System.Globalization;

namespace Icm.Compilation
{

	/// <summary>
	/// Compiles a function out of Visual Basic source code.
	/// </summary>
	/// <typeparam name="T"></typeparam>
	/// <remarks></remarks>
	public class VBCompiledFunction<T> : CompiledFunction<T>
	{

		public VBCompiledFunction()
		{
			Dictionary<string, string> providerOptions = new Dictionary<string, string>();
			#if FrameworkNet35
			providerOptions.Add("CompilerVersion", "v3.5");
			#endif
			#if FrameworkNet40
			providerOptions.Add("CompilerVersion", "v4.0");
			#endif
			#if FrameworkNet45
			providerOptions.Add("CompilerVersion", "v4.0");
			#endif
			CodeProvider = new VBCodeProvider(providerOptions);

			AddNamespace("System", "system.dll");
			AddNamespace("System.Xml", "system.xml.dll");
			AddNamespace("System.Data", "system.data.dll");
			AddNamespace("System.Linq", "system.core.dll");
			AddNamespace("Microsoft.VisualBasic", "system.dll");

			CompilerParameters.CompilerOptions = "/t:library";
			CompilerParameters.GenerateInMemory = true;
		}

		public override string GeneratedCode()
		{
			StringBuilder sb = new StringBuilder();

			// Add Imports
			foreach (void ns_loopVariable in Namespaces) {
				ns = ns_loopVariable;
				sb.AppendLine(string.Format(CultureInfo.InvariantCulture, "Imports {0}", ns));
			}

			// Build a little wrapper code, with our passed in code in the middle 
			sb.AppendLine("Namespace dValuate");
			sb.AppendLine(" Class EvalRunTime ");

			sb.AppendLine("  Public Function EvaluateIt( _");

			foreach (void p_loopVariable in Parameters.Take(Parameters.Count - 1)) {
				p = p_loopVariable;
				sb.AppendLine(string.Format(CultureInfo.InvariantCulture, "   ByVal {0} As {1}, _", p.Name, p.ArgType));
			}
			var _with1 = Parameters.Last;
			sb.AppendLine(string.Format(CultureInfo.InvariantCulture, "   ByVal {0} As {1} _", _with1.Name, _with1.ArgType));
			sb.AppendLine(string.Format(CultureInfo.InvariantCulture, "  ) As {0}", typeof(T).Name));
			sb.AppendLine(string.Format(CultureInfo.InvariantCulture, "    Return {0}", Code));
			sb.AppendLine("  End Function");
			sb.AppendLine(" End Class ");
			sb.AppendLine("End Namespace");

			return sb.ToString;
		}

	}

}

//=======================================================
//Service provided by Telerik (www.telerik.com)
//Conversion powered by NRefactory.
//Twitter: @telerik
//Facebook: facebook.com/telerik
//=======================================================
