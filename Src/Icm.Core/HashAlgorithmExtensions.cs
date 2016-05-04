using System;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;

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
		public static byte[] ComputeHash(this HashAlgorithm hashAlgorithm, string s)
		{
		    byte[] bytes = s.Cast<byte>().ToArray();
			return hashAlgorithm.ComputeHash(bytes);
		}
        
		public static string StringHash(this HashAlgorithm hashAlgorithm, string s)
		{
			return hashAlgorithm.ComputeHash(s).ToHex();
		}
        
		public static string StringHash(this HashAlgorithm hashAlgorithm, params byte[] b)
		{
			return hashAlgorithm.ComputeHash(b).ToHex();
		}
        
		public static string StringHash(this HashAlgorithm hashAlgorithm, Stream fs)
		{
			return hashAlgorithm.ComputeHash(fs).ToHex();
		}

		/// <summary>
		/// Hexadecimal string corresponding to a byte array.
		/// </summary>
		/// <param name="hash"></param>
		/// <returns></returns>
		/// <remarks></remarks>
		public static string ToHex(this byte[] hash)
		{
			StringBuilder result = new StringBuilder();

			foreach (var bytenumber in hash) {
				result.AppendFormat("{0:x2}", bytenumber);
			}

			return result.ToString();
		}
	}

}