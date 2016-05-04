using System.Text;

namespace Icm
{

	public static class Tools
	{


		/// <summary>
		/// String join function that accepts a ParamArray of strings.
		/// </summary>
		/// <param name="sep"></param>
		/// <param name="strs"></param>
		/// <returns></returns>
		/// <remarks>Ignores null and empty strings.</remarks>
		public static string JoinStr(string sep, params string[] strs)
		{
			StringBuilder sb = new StringBuilder();
			int i = strs.GetLowerBound(0);
			bool firstOne = true;
			do
			{
			    if (i > strs.GetUpperBound(0)) {
					return sb.ToString();
				}

			    if (string.IsNullOrEmpty(strs[i])) {
			        i += 1;
			    } else {
			        if (firstOne)
                    {
			            sb.Append(strs[i]);
			            firstOne = false;
			        } else {
			            sb.Append(sep + strs[i]);
			        }

			        i += 1;
			    }
			} while (true);
		}
	}
}