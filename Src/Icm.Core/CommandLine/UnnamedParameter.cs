namespace Icm.CommandLineTools
{
	/// <summary>
	///  Unnamed parameter.
	/// </summary>
	/// <remarks></remarks>
	public class UnnamedParameter
	{

		private readonly string _description;

		private readonly string _name;
		public UnnamedParameter(string name, string description)
		{
			_name = name;
			_description = description;
		}

		/// <summary>
		///  Long option (for example --help)
		/// </summary>
		/// <value></value>
		/// <returns></returns>
		/// <remarks></remarks>
		public string Name {
			get { return _name; }
		}

		/// <summary>
		/// Description of this option.
		/// </summary>
		/// <value></value>
		/// <returns></returns>
		/// <remarks></remarks>
		public string Description {
			get { return _description; }
		}

	}

}

//=======================================================
//Service provided by Telerik (www.telerik.com)
//Conversion powered by NRefactory.
//Twitter: @telerik
//Facebook: facebook.com/telerik
//=======================================================
