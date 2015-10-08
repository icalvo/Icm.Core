using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;
using System.Runtime.CompilerServices;

namespace Icm.Security.Cryptography
{

	///<summary>
	/// Extensions for HashAlgorithm. StringHash returns a string
	/// with the same format as Paul Johnston's JavaScript MD5 implementation
	/// (http://pajhome.org.uk/crypt/md5), so his md5.js can be directly used
	/// toghether with this class to implement CHAP or other challenge-response protocols.
	/// </summary>
	public static class HashAlgorithmExtensions
	{

		[Extension()]
		public static byte[] ComputeHash(HashAlgorithm hashAlgorithm, string s)
		{
			byte[] bytes = new byte[s.Length];
			char c = '\0';
			int i = 0;

			foreach (char c_loopVariable in s) {
				c = c_loopVariable;
				bytes(i) = Convert.ToByte(Strings.Asc(c));
				i += 1;
			}
			return hashAlgorithm.ComputeHash(bytes);
		}

		[Extension()]
		public static string StringHash(HashAlgorithm hashAlgorithm, string s)
		{
			return hashAlgorithm.ComputeHash(s).ToHex();
		}

		[Extension()]
		public static string StringHash(HashAlgorithm hashAlgorithm, params byte[] b)
		{
			return hashAlgorithm.ComputeHash(b).ToHex();
		}

		[Extension()]
		public static string StringHash(HashAlgorithm hashAlgorithm, Stream fs)
		{
			return hashAlgorithm.ComputeHash(fs).ToHex();
		}

		/// <summary>
		/// Hexadecimal string corresponding to a byte array.
		/// </summary>
		/// <param name="hash"></param>
		/// <returns></returns>
		/// <remarks></remarks>
		[Extension()]
		public static string ToHex(byte[] hash)
		{
			StringBuilder result = new StringBuilder();

			foreach (void bytenumber_loopVariable in hash) {
				bytenumber = bytenumber_loopVariable;
				result.Append(bytenumber.ToHex());
			}

			return result.ToString;
		}

		/// <summary>
		/// Hexadecimal string corresponding to a byte, lower case and zero padded.
		/// </summary>
		/// <param name="b"></param>
		/// <returns></returns>
		/// <remarks>
		/// For example 2 => "02" and 254 => "fe".
		/// </remarks>
		[Extension()]
		private static string ToHex(byte b)
		{
			return new string('0', 2 - Conversion.Hex(b).Length) + Conversion.Hex(b).ToLower;
		}

	}

}

//=======================================================
//Service provided by Telerik (www.telerik.com)
//Conversion powered by NRefactory.
//Twitter: @telerik
//Facebook: facebook.com/telerik
//=======================================================
