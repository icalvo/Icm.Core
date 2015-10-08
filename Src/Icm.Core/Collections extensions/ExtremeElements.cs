using System.Collections.Generic;

namespace Icm.Collections
{

	/// <summary>
	/// List of elements that reach an extreme value, and the extreme value itself.
	/// </summary>
	/// <typeparam name="TElement"></typeparam>
	/// <typeparam name="TExtreme"></typeparam>
	/// <remarks></remarks>
	public class ExtremeElements<TElement, TExtreme>
	{
		public IEnumerable<TElement> List { get; set; }
		public TExtreme Extreme { get; set; }

		/// <summary>
		/// Initializes a new instance of the ExtremeElements class.
		/// </summary>
		public ExtremeElements(IEnumerable<TElement> list, TExtreme extreme)
		{
			this.List = list;
			this.Extreme = extreme;
		}
	}

}

//=======================================================
//Service provided by Telerik (www.telerik.com)
//Conversion powered by NRefactory.
//Twitter: @telerik
//Facebook: facebook.com/telerik
//=======================================================
