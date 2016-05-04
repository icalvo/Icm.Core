using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.CodeDom.Compiler;
using System.Reflection;
using System.Globalization;

namespace Icm.Compilation
{

    /// <summary>
    /// Compiles a function out of the source code.
    /// </summary>
    /// <typeparam name="T">Function return type</typeparam>
    /// <remarks>
    /// Use property <see cref="CompiledFunction{T}.Code" /> to establish the code of
    /// the function.
    /// Use the method <see cref="CompiledFunction{T}.AddParameter{TParam}" />  to establish the
    /// parameters of the function.
    /// Use <see cref="CompiledFunction{T}.CompileAsExpression" /> to compile 
    /// a function that returns the given code. This method returns False if
    /// the compilation fails. Use property <see cref="CompiledFunction{T}.CompilerErrors" />
    /// to obtain the compilation errors.
    /// Use <see cref=" CompiledFunction{T}.Evaluate">Evaluate</see> to execute the function
    /// with some given arguments which must be type compatible with the previously configured
    /// parameters.
    /// </remarks>
    public abstract class CompiledFunction<T> : IDisposable
	{

		private static readonly Dictionary<string, object> Instances = new Dictionary<string, object>();
        private readonly List<CompiledParameterInfo> _parameters = new List<CompiledParameterInfo>();

		private readonly List<CompilerError> _compilerErrors = new List<CompilerError>();
		protected CompilerParameters CompilerParameters { get; set; }
		protected CodeDomProvider CodeProvider { get; set; }

		protected IList<string> Namespaces { get; } = new List<string>();

        private object _execInstance;

		private MethodInfo _methodInfo;

		public IList<CompiledParameterInfo> Parameters => _parameters;

        public string Code { get; set; }


		public void AddParameter<TParam>(string n)
		{
			Parameters.Add(new CompiledParameterInfo<TParam>(n));
		}

		public void AddNamespace(string nspace, string assm)
		{
			if (!CompilerParameters.ReferencedAssemblies.Contains(assm)) {
				CompilerParameters.ReferencedAssemblies.Add(assm);
			}
			Namespaces.Add(nspace);

		}

		public abstract string GeneratedCode();

		/// <summary>
		///  Code is used as an VB expression that the compiled function will
		/// directly return.
		/// </summary>
		/// <remarks></remarks>
		public void CompileAsExpression()
		{
		    Type type;

		    var code = GeneratedCode();

			CompilerErrors.Clear();

			if (Instances.ContainsKey(code)) {
				_execInstance = Instances[code];
				type = _execInstance.GetType();
				_methodInfo = type.GetMethod("EvaluateIt");

			} else
			{
			    // Compile and get results 
			    var compilerResults = CodeProvider.CompileAssemblyFromSource(CompilerParameters, code);

			    // Check for compile time errors 
				if (compilerResults.Errors.Count != 0) {
					foreach (CompilerError compileError in compilerResults.Errors) {
						CompilerErrors.Add(compileError);
					}

					throw new CompileException(compilerResults.Errors, null);
				} else {
					// No Errors On Compile, so continue to process...

					var assembly = compilerResults.CompiledAssembly;
					_execInstance = assembly.CreateInstance("dValuate.EvalRunTime");
					Instances.Add(code, _execInstance);
					type = _execInstance?.GetType();
					_methodInfo = type?.GetMethod("EvaluateIt");
				}
			}
		}

		/// <summary>
		/// Compilation errors.
		/// </summary>
		/// <value></value>
		/// <returns></returns>
		/// <remarks></remarks>
		public IList<CompilerError> CompilerErrors {
			get { return _compilerErrors; }
		}

		/// <summary>
		/// Evaluate the compiled function.
		/// </summary>
		/// <param name="args"></param>
		/// <returns></returns>
		/// <remarks>
		/// You must call CompileAsExpression before.
		/// </remarks>
		public T Evaluate(params object[] args)
		{
		    try
			{
			    return (T)_methodInfo.Invoke(_execInstance, args);
			}
			catch (TargetInvocationException ex) {
				// Runtime exception caused by the compiled code
				Debug.WriteLine(ex.InnerException.Message);
				throw new InvalidOperationException(string.Format(CultureInfo.CurrentCulture, "{0}: {1}", Code, ex.InnerException.Message), ex.InnerException);
			} catch (Exception ex) {
				// Some other weird error 
				Debug.WriteLine(ex.Message);
				throw;
			}
		}

		public void Dispose()
		{
			Dispose(true);
			GC.SuppressFinalize(this);
		}

	    private void Dispose(bool disposing)
	    {
	        if (!disposing) return;
	        if (CodeProvider == null) return;
	        CodeProvider.Dispose();
	        CodeProvider = null;
	    }
	}

	public static class CompiledFunctionFactories
	{

		public static CompiledFunction<T> CreateCompiledFunction<T>(string lang)
		{
			switch (lang) {
				case "VisualBasic":
					return new VBCompiledFunction<T>();
				default:
					throw new ArgumentException(string.Format(CultureInfo.CurrentCulture, "Language {0} not supported", lang));
			}
		}

	}
}