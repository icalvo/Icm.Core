
using Microsoft.VisualBasic;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Runtime.CompilerServices;

namespace Icm.Collections.Generic
{
	public static class AaSearch
	{

		[Extension()]
		public static int f(INode n)
		{
			return n.g + n.h;
		}

		public static INode AaSearch(INode root)
		{
			HashSet<INode> visited = new HashSet<INode> { root };
			HashSet<INode> evaluated = new HashSet<INode>();

			root.g = 0;

			while (!(visited.Count == 0)) {
				dynamic current = visited.MinEntity(n => n.f);

				if (current.IsGoal) {
					return current;
				}
				visited.Remove(current);
				evaluated.Add(current);
				foreach (void neighbor_loopVariable in current.Neighbors) {
					neighbor = neighbor_loopVariable;
					if (evaluated.Contains(neighbor)) {
						continue;
					}
					dynamic tentative_g = current.g + current.Distance(neighbor);

					if (visited.Contains(neighbor) || tentative_g <= neighbor.g) {
						neighbor.CameFrom = current;
						neighbor.g = tentative_g;
						if (!visited.Contains(neighbor)) {
							visited.Add(neighbor);
						}

					}

				}
			}
			return null;
		}

	}
}

//=======================================================
//Service provided by Telerik (www.telerik.com)
//Conversion powered by NRefactory.
//Twitter: @telerik
//Facebook: facebook.com/telerik
//=======================================================
