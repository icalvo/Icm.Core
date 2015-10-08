using System.Collections.Generic;

namespace Icm.Collections.Generic
{

	public interface INode
	{
		string Value { get; set; }
		INode CameFrom { get; set; }
		int g { get; set; }
		int h();

		IEnumerable<INode> Neighbors { get; set; }
		bool IsGoal();
		int Distance(INode other);
	}
}

//=======================================================
//Service provided by Telerik (www.telerik.com)
//Conversion powered by NRefactory.
//Twitter: @telerik
//Facebook: facebook.com/telerik
//=======================================================
