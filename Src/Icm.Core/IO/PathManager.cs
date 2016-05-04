using System;

namespace Icm.IO
{
	/// <summary>
	/// PathManager is an enhanced manager for joining paths.
	/// </summary>
	/// <remarks>
	/// <para>With the instance you can combine path chunks. You can pass to Combine()
	/// more than two path chunks. Doesn't matter if the chunks end with the separator or
	/// not, Combine will add the separator when necessary.</para>
	/// <para>Combine returns String.Empty if no paths are provided.</para>
	/// </remarks>
	public class PathManager
	{

		private PathManager(char sep)
		{
			Separator = sep;
		}

		public char Separator { get; set; }

		public string Combine(params string[] paths)
		{
			string pathResult = "";
			if (paths == null || paths.Length == 0)
            {
				return pathResult;
			}

			pathResult = paths[0];
			for (int i = 1; i <= paths.Length - 1; i++)
            {
				if (pathResult.EndsWith(Separator.ToString(), StringComparison.Ordinal))
                {
					pathResult += paths[i];
				}
                else
                {
					pathResult += Separator + paths[i];
				}
			}

			return pathResult;
		}
	}
}