using System;
using System.Runtime.Serialization;

namespace Icm.CommandLineTools
{
	public class UndefinedOptionException : Exception
	{


		private string _s;
		public UndefinedOptionException() : base("Undefined option")
		{
		}

		public UndefinedOptionException(string opt) : base("Undefined option " + opt)
		{
			_s = opt;
		}

		public UndefinedOptionException(string message, Exception innerException) : base(message, innerException)
		{

		}

		protected UndefinedOptionException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}

	}

}

//=======================================================
//Service provided by Telerik (www.telerik.com)
//Conversion powered by NRefactory.
//Twitter: @telerik
//Facebook: facebook.com/telerik
//=======================================================
