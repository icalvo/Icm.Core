using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;

namespace Icm.MathTools
{
	public class NumberFiles
	{

		/// <summary>
		/// Reads a simple text file of real numbers with the usual
		/// (invariant culture) admitted formats. Ignores line comments starting with "#".
		/// </summary>
		/// <param name="fn"></param>
		/// <returns></returns>
		/// <remarks></remarks>
		public static double[] ReadArrayFile(string fn)
		{
			System.IO.StreamReader sr = new System.IO.StreamReader(fn);

		    var l = new List<double>();
		    var line = sr.ReadLine();

			while (line != null)
            {
				if (!line.StartsWith("#", StringComparison.Ordinal))
				{
				    double d;
				    if (double.TryParse(line, NumberStyles.Float, CultureInfo.InvariantCulture, out d)) {
						l.Add(d);
					}
				}
                line = sr.ReadLine();
			}
			sr.Close();
			return l.ToArray();
		}

		/// <summary>
		///  Reads a simple file with a multidimensional array of numbers.
		/// </summary>
		/// <param name="fn">File name</param>
		/// <param name="numbersep">Separator for numbers</param>
		/// <returns></returns>
		/// <remarks></remarks>
		public static double[,] ReadMatrixFile(string fn, char numbersep)
		{
			System.IO.StreamReader sr = new System.IO.StreamReader(fn);
		    var l = new List<double[]>();
			var lineList = new List<double>();
		    int length1 = 0;
			int length2 = 0;
			double d = 0;
			var line = sr.ReadLine();

			while (line != null) {
				if (!line.StartsWith("#", StringComparison.Ordinal)) {
					lineList.Clear();
					var splitted = line.Split(numbersep);
				    lineList.AddRange(
				        splitted.Where(ds => double.TryParse(ds, NumberStyles.Float, CultureInfo.InvariantCulture, out d))
				            .Select(ds => d));
				    if (lineList.Count > length2) {
						length2 = lineList.Count;
					}
					l.Add(lineList.ToArray());
					length1 += 1;
				}
				line = sr.ReadLine();
			}
			sr.Close();

			var result = new double[length1, length2];

		    var i = 0;
			foreach (double[] da in l) {
				var j = 0;
				foreach (double d2 in da) {
					result[i, j] = d2;
					j += 1;
				}
				i += 1;
			}
			return result;
		}
	}
}

//=======================================================
//Service provided by Telerik (www.telerik.com)
//Conversion powered by NRefactory.
//Twitter: @telerik
//Facebook: facebook.com/telerik
//=======================================================
