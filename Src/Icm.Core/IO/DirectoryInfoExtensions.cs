using System.IO;
using System.Runtime.CompilerServices;

namespace Icm.IO
{
	public static class DirectoryInfoExtensions
	{

		/// <summary>
		/// Builds a FieldInfo representing a file inside a DirectoryInfo object.
		/// </summary>
		/// <param name="di"></param>
		/// <param name="relativeFilename"></param>
		/// <returns></returns>
		/// <remarks></remarks>
		[Extension()]
		public static FileInfo GetFile(DirectoryInfo di, string relativeFilename)
		{
			return new FileInfo(Path.Combine(di.FullName, relativeFilename));
		}

	}
}

//=======================================================
//Service provided by Telerik (www.telerik.com)
//Conversion powered by NRefactory.
//Twitter: @telerik
//Facebook: facebook.com/telerik
//=======================================================
