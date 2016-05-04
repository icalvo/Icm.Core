using System;
using System.CodeDom.Compiler;
using System.Runtime.Serialization;

namespace Icm.Compilation
{

	[Serializable]
	public class CompileException : Exception
	{


		public CompilerErrorCollection ErrorList { get; }
		public CompileException() : base("Error compiling expression")
		{
			ErrorList = new CompilerErrorCollection();
		}

		public CompileException(CompilerErrorCollection errorList, Exception inner) : base("Error compiling expression", inner)
		{

			this.ErrorList = errorList;
		}
		public CompileException(string message) : base(message)
		{
			ErrorList = new CompilerErrorCollection();
		}
		public CompileException(string message, Exception innerException) : base(message, innerException)
		{
			ErrorList = new CompilerErrorCollection();
		}
		protected CompileException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			ErrorList = new CompilerErrorCollection();
		}

	}

}