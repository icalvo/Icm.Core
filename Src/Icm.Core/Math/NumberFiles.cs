
using Microsoft.VisualBasic;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Globalization;

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
			string line = null;
			Generic.List<double> l = new Generic.List<double>();
			double d = 0;
			line = sr.ReadLine();

			while (!(line == null)) {
				if (!line.StartsWith("#", StringComparison.Ordinal)) {
					if (double.TryParse(line, NumberStyles.Float, CultureInfo.InvariantCulture, d)) {
						l.Add(d);
					}
				}
				line = sr.ReadLine();
			}
			sr.Close();
			return l.ToArray;
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
			string line = null;
			Generic.List<double[]> l = new Generic.List<double[]>();
			Generic.List<double> lineList = new Generic.List<double>();
			string[] splitted = null;
			int length1 = 0;
			int length2 = 0;
			double d = 0;
			line = sr.ReadLine();

			while (!(line == null)) {
				if (!line.StartsWith("#", StringComparison.Ordinal)) {
					lineList.Clear();
					splitted = line.Split(numbersep);
					foreach (string ds in splitted) {
						if (double.TryParse(ds, NumberStyles.Float, CultureInfo.InvariantCulture, d)) {
							lineList.Add(d);
						}
					}
					if (lineList.Count > length2) {
						length2 = lineList.Count;
					}
					l.Add(lineList.ToArray);
					length1 += 1;
				}
				line = sr.ReadLine();
			}
			sr.Close();

			dynamic result = (double[,])Array.CreateInstance(typeof(double), length1, length2);

			int i = 0;
			int j = 0;
			i = 0;
			foreach (double[] da in l) {
				j = 0;
				foreach (double d_loopVariable in da) {
					d = d_loopVariable;
					result(i, j) = d;
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
