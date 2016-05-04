using System.Runtime.CompilerServices;

namespace Icm
{

	public static class BooleanExtensions
	{

		/// <summary>
		/// Converts a boolean to 0 or 1.
		/// </summary>
		/// <param name="b">Boolean to convert</param>
		/// <returns>0 if b is false, 1 otherwise</returns>
		/// <remarks></remarks>
		public static int ToInteger(this bool b)
		{
			if (b) {
				return 1;
			} else {
				return 0;
			}
		}

		/// <summary>
		/// Tristate If for nullable booleans.
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <param name="bool"></param>
		/// <param name="truePart">Value returned if bool is true</param>
		/// <param name="falsePart">Value returned if bool is false</param>
		/// <param name="nullPart">Value returned if bool has no value</param>
		/// <returns></returns>
		/// <remarks></remarks>
		public static T IfN<T>(this bool? @bool, T truePart, T falsePart, T nullPart)
		{
			if (@bool.HasValue) {
				if (@bool.Value) {
					return truePart;
				} else {
					return falsePart;
				}
			} else {
				return nullPart;
			}
		}
	}
}
