using System;
using System.Runtime.CompilerServices;
using System.Globalization;

namespace Icm
{

	/// <summary>
	/// Utility class with String-related functions.
	/// </summary>
	/// <remarks>
	/// </remarks>
	/// <history>
	/// 	[icalvo]    19/08/2004	Created
	/// </history>
	public static class StringExtensions
	{

		/// <summary>
		/// Substring function with PHP <a href="http://php.net/manual/es/function.substr.php">substr</a> syntax.
		/// </summary>
		/// <param name="s">Original string</param>
		/// <param name="startIdx">Start index, that can be negative.</param>
		/// <param name="length">Length, that can be negative.</param>
		/// <returns></returns>
		/// <remarks>Where PHP's substr returns FALSE, this function throws an ArgumentOutOfRangeException</remarks>
		public static string Substr(this string s, int startIdx, int length)
		{
			int startIdx2 = 0;
			int length2 = 0;
			if (startIdx < 0) {
				startIdx2 = s.Length + startIdx;
				if (startIdx2 < 0) {
					throw new ArgumentOutOfRangeException("startIdx", startIdx, "Negative startIdx less than -s.Length");
				}
			} else {
				startIdx2 = startIdx;
			}
			if (length < 0) {
				length2 = s.Length - startIdx2 + length;
				if (length2 < 0) {
					throw new ArgumentOutOfRangeException("length", length, "Negative length less than available");
				}
			} else {
				length2 = length;
			}
			return s.Substring(startIdx2, length2);
		}

		/// <summary>
		/// Substring between two indices
		/// </summary>
		/// <param name="s"></param>
		/// <param name="startIdx"></param>
		/// <param name="endIdx"></param>
		/// <returns></returns>
		/// <remarks></remarks>
		public static string Med(this string s, int startIdx, int endIdx)
		{
			return s.Substring(startIdx, endIdx - startIdx + 1);
		}

		/// <summary>
		/// Skips some characters from the start and some from the end of a string.
		/// </summary>
		/// <param name="s"></param>
		/// <param name="startLength"></param>
		/// <param name="endLength"></param>
		/// <returns></returns>
		/// <remarks></remarks>
		public static string SkipBoth(this string s, int startLength, int endLength)
		{
			return s.Substring(startLength, s.Length - startLength - endLength);
		}

		/// <summary>
		/// Similar to VB6 Left function.
		/// </summary>
		/// <param name="s"></param>
		/// <param name="length"></param>
		/// <returns></returns>
		/// <remarks></remarks>
		public static string Left(this string s, int length)
		{
			return s.Substring(0, length);
		}

		/// <summary>
		/// Similar to VB6 Right function.
		/// </summary>
		/// <param name="s"></param>
		/// <param name="length"></param>
		/// <returns></returns>
		/// <remarks></remarks>
		public static string Right(this string s, int length)
		{
			return s.Substring(s.Length - length);
		}

		/// <summary>
		/// Abbreviation of 'StartsWith AndAlso EndsWith'
		/// </summary>
		/// <param name="s"></param>
		/// <param name="startS"></param>
		/// <param name="endS"></param>
		/// <returns></returns>
		/// <remarks></remarks>
		public static bool SurroundedBy(this string s, string startS, string endS)
		{
			return s.StartsWith(startS) && s.EndsWith(endS);
		}

		/// <summary>
		///   Creates a new string by repeating count times the string s.
		/// </summary>
		/// <param name="s">String to be repeated.</param>
		/// <param name="count"># of times to repeat s.</param>
		/// <returns>New string created by repeating s.</returns>
		/// <remarks>
		/// </remarks>
		/// <history>
		/// 	[icalvo]	26/11/2004	Created
		///     [icalvo]    31/03/2005  Documented
		/// </history>
		public static string Repeat(this string s, int count)
		{
			if (count == 0) {
				return "";
			}
			System.Text.StringBuilder sb = new System.Text.StringBuilder(s.Length * count);
			for (int i = 1; i <= count; i++) {
				sb.Append(s);
			}
			return sb.ToString();
		}

		/// <summary>
		///  Creates a new string by converting the first char of s to upper case.
		/// </summary>
		/// <param name="s">Original string.</param>
		/// <returns>New string equal to s except the first char to upper case.</returns>
		/// <remarks>
		/// Tested successfully with accented characters and strings number
		/// </remarks>
		/// <history>
		/// 	[icalvo]	19/08/2004	Created
		///     [icalvo]    31/03/2005  Documented
		/// </history>
		public static string ToUpperFirst(this string s)
		{
		    if (string.IsNullOrEmpty(s)) {
				return "";
			}

		    return char.ToUpper(s[0], CultureInfo.CurrentCulture) + s.Substring(1, s.Length - 1);
		}

	    /// <summary>
		/// If string is String.Empty, return a substitution
		/// </summary>
		/// <param name="s"></param>
		/// <param name="emptyString"></param>
		/// <returns></returns>
		/// <remarks></remarks>
		public static string IfEmpty(this string s, string emptyString)
		{
			return s != null && s == string.Empty ? emptyString : s;
		}

		/// <summary>
		/// Is the string equal to String.Empty?
		/// </summary>
		/// <param name="s"></param>
		/// <returns></returns>
		/// <remarks></remarks>
		public static bool IsEmpty(this string s)
		{
			return s != null && s == string.Empty;
		}
	}
}
