using System.Collections.Generic;
using Microsoft.CSharp;
using System.Text;
using System.Globalization;
using System.Linq;

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
			foreach (var ns in Namespaces) {
				sb.AppendLine(string.Format(CultureInfo.InvariantCulture, "import {0}", ns));
			}

			// Build a little wrapper code, with our passed in code in the middle 
			sb.AppendLine("namespace dValuate {");
			sb.AppendLine(" class EvalRunTime {");

			sb.AppendLine(string.Format(CultureInfo.InvariantCulture, "  {0} EvaluateIt(", typeof(T).Name));

			foreach (var p in Parameters.Take(Parameters.Count - 1)) {
				sb.AppendLine(string.Format(CultureInfo.InvariantCulture, "   {1} {0},", p.Name, p.ArgType));
			}

			var with1 = Parameters.Last();
			sb.AppendLine(string.Format(CultureInfo.InvariantCulture, "   {1} {0}", with1.Name, with1.ArgType));
			sb.AppendLine("  )");
			sb.AppendLine(string.Format(CultureInfo.InvariantCulture, "    return {0};", Code));
			sb.AppendLine("  }");
			sb.AppendLine(" }");
			sb.AppendLine("}");

			return sb.ToString();
		}

	}

}