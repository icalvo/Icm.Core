
using Microsoft.VisualBasic;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using Microsoft.CSharp;
using System.Text;
using System.Globalization;

namespace Icm.Compilation
{

	/// <summary>
	/// Compiles a function out of C# source code.
	/// </summary>
	/// <typeparam name="T"></typeparam>
	/// <remarks></remarks>
	public class CSharpCompiledFunction<T> : CompiledFunction<T>
	{

		public CSharpCompiledFunction()
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
			CodeProvider = new CSharpCodeProvider(providerOptions);

			AddNamespace("System", "system.dll");
			AddNamespace("System.Xml", "system.xml.dll");
			AddNamespace("System.Data", "system.data.dll");
			AddNamespace("System.Linq", "system.core.dll");
			AddNamespace("Microsoft.CSharp", "system.dll");

			CompilerParameters.CompilerOptions = "/t:library";
			CompilerParameters.GenerateInMemory = true;
		}

		public override string GeneratedCode()
		{
			StringBuilder sb = new StringBuilder();

			// Imports
			foreach (void ns_loopVariable in Namespaces) {
				ns = ns_loopVariable;
				sb.AppendLine(string.Format(CultureInfo.InvariantCulture, "import {0}", ns));
			}

			// Build a little wrapper code, with our passed in code in the middle 
			sb.AppendLine("namespace dValuate {");
			sb.AppendLine(" class EvalRunTime {");

			sb.AppendLine(string.Format(CultureInfo.InvariantCulture, "  {0} EvaluateIt(", typeof(T).Name));

			foreach (void p_loopVariable in Parameters.Take(Parameters.Count - 1)) {
				p = p_loopVariable;
				sb.AppendLine(string.Format(CultureInfo.InvariantCulture, "   {1} {0},", p.Name, p.ArgType));
			}
			var _with1 = Parameters.Last;
			sb.AppendLine(string.Format(CultureInfo.InvariantCulture, "   {1} {0}", _with1.Name, _with1.ArgType));
			sb.AppendLine("  )");
			sb.AppendLine(string.Format(CultureInfo.InvariantCulture, "    return {0};", Code));
			sb.AppendLine("  }");
			sb.AppendLine(" }");
			sb.AppendLine("}");

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
