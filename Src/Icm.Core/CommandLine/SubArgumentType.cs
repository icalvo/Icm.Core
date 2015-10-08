namespace Icm.CommandLineTools
{
	/// <summary>
	///  Types of subarguments.
	/// </summary>
	/// <remarks>
	/// <list>
	/// <item>Required: The subargument is required</item>
	/// <item>Optional: The subargument is optional</item>
	/// <item>List: An indefinite list of subarguments is admitted (including the empty list).</item>
	/// </list></remarks>

	public enum SubArgumentType
	{
		Required = 1,
		Optional = 2,
		List = 3
	}
}

//=======================================================
//Service provided by Telerik (www.telerik.com)
//Conversion powered by NRefactory.
//Twitter: @telerik
//Facebook: facebook.com/telerik
//=======================================================
