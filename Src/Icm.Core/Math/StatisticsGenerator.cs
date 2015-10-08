using System;
using System.Collections.Generic;
using System.Diagnostics;
namespace Icm.MathTools
{
	/// <summary>
	/// 
	/// </summary>
	/// <remarks></remarks>
	public class StatisticsGenerator
	{


		private Random rng = new Random();

		public LimitedQueue<double> LastFour = new LimitedQueue<double>(4);
		#region " Constructors "


		public StatisticsGenerator()
		{
		}

		public StatisticsGenerator(int sem)
		{
			ChangeSeed(sem);
		}

		#endregion

		#region " Generic sample generation "

		public delegate double Function1Double(double a);
		public delegate double Function2Double(double a, double b);
		public delegate double Function3Double(double a, double b, double c);
		public delegate double Function4Double(double a, double b, double c, double d);

		#endregion

		#region " Normal "

		private int NormalUsed_ = -1;

		private double NormalY_ = 0.0;
		/// <summary>
		/// Samples the standard normal probability distribution.
		/// </summary>
		/// <returns>A random value that follows N(9,1).</returns>
		/// <remarks>
		///    <para>The standard normal probability distribution function (PDF) has 
		///    mean 0 and standard deviation 1.</para>
		///    <para>The Box-Muller method is used, which is efficient, but 
		///    generates two values at a time.</para>
		/// </remarks>
		/// <history>
		/// [John Burkardt]	18/09/2004	Created in C#
		/// [icalvo]		19/08/2004	Translated to VB.NET
		/// </history>
		public double Normal01Sample()
		{
			double r1 = 0;
			double r2 = 0;
			double x = 0;

			if (NormalUsed_ == -1) {
				NormalUsed_ = 0;
			}
			//
			//  If we've used an even number of values so far, generate two more, return one,
			//  and save one.
			//

			if (NormalUsed_ % 2 == 0) {
				do {
					r1 = Uniform01();
					if ((r1 != 0)) {
						break; // TODO: might not be correct. Was : Exit Do
					}

				} while (true);
				r2 = Uniform01();
				x = Math.Sqrt(-2 * Math.Log(r1)) * Math.Cos(2 * Math.PI * r2);
				NormalY_ = Math.Sqrt(-2 * Math.Log(r1)) * Math.Sin(2 * Math.PI * r2);
			} else {
				x = NormalY_;
			}
			NormalUsed_ += 1;
			return x;

		}

		/// <summary>
		/// Samples the general normal probability distribution.
		/// </summary>
		/// <param name="mean">A random value that follows N(mean,dev)</param>
		/// <param name="dev"></param>
		/// <returns>
		///    <para>The Box-Muller method is used, which is efficient, but 
		///    generates two values at a time.</para>
		/// </returns>
		/// <remarks></remarks>
		/// <history>
		/// [icalvo]		07/04/2008	Created
		/// </history>
		public double NormalSample(double mean, double dev)
		{
			return mean + Math.Sqrt(dev) * Normal01Sample();
		}

		/// <summary>
		/// Peter Acklam's algorithm for the inverse normal cumulative distribution function.
		/// </summary>
		/// <param name="p">Probability</param>
		/// <returns></returns>
		/// <remarks>
		/// <para>See http://home.online.no/~pjacklam/notes/invnorm/index.html for more information.</para>
		/// <para>Implemented in VB.NET by Geoffrey C. Barnes, Ph.D. Fels Center of Government and Jerry Lee Center of Criminology University of Pennsylvania.</para>
		/// </remarks>
		public static double NormalCDFInv(double p)
		{

			double q = 0;
			double r = 0;

			//Coefficients in rational approximations.

			const double A1 = -39.6968302866538;
			const double A2 = 220.946098424521;
			const double A3 = -275.928510446969;
			const double A4 = 138.357751867269;
			const double A5 = -30.6647980661472;
			const double A6 = 2.50662827745924;

			const double B1 = -54.4760987982241;
			const double B2 = 161.585836858041;
			const double B3 = -155.698979859887;
			const double B4 = 66.8013118877197;
			const double B5 = -13.2806815528857;

			const double C1 = -0.00778489400243029;
			const double C2 = -0.322396458041136;
			const double C3 = -2.40075827716184;
			const double C4 = -2.54973253934373;
			const double C5 = 4.37466414146497;
			const double C6 = 2.93816398269878;

			const double D1 = 0.00778469570904146;
			const double D2 = 0.32246712907004;
			const double D3 = 2.445134137143;
			const double D4 = 3.75440866190742;

			//Define break-points.

			const double P_LOW = 0.02425;
			const double P_HIGH = 1 - P_LOW;


			if (p > 0 && p < P_LOW) {
				//Rational approximation for lower region.

				q = Math.Sqrt(-2 * Math.Log(p));

				return (((((C1 * q + C2) * q + C3) * q + C4) * q + C5) * q + C6) / ((((D1 * q + D2) * q + D3) * q + D4) * q + 1);


			} else if (p >= P_LOW && p <= P_HIGH) {
				//Rational approximation for central region.

				q = p - 0.5;
				r = q * q;

				return (((((A1 * r + A2) * r + A3) * r + A4) * r + A5) * r + A6) * q / (((((B1 * r + B2) * r + B3) * r + B4) * r + B5) * r + 1);


			} else if (p > P_HIGH && p < 1) {
				//Rational approximation for upper region.

				q = Math.Sqrt(-2 * Math.Log(1 - p));

				return -(((((C1 * q + C2) * q + C3) * q + C4) * q + C5) * q + C6) / ((((D1 * q + D2) * q + D3) * q + D4) * q + 1);


			} else {
				throw new ArgumentOutOfRangeException("p", "Probability must be > 0 and < 1");

			}

		}

		#endregion

		#region " Weibull "

		public static double WeibullCDFInv(double cdf, double offset, double lambda, double k)
		{
			if (cdf < 0 || 1 < cdf) {
				throw new ArgumentOutOfRangeException("cdf", "CDF out of (0, 1)!");
			}
			if (k == 0) {
				throw new ArgumentOutOfRangeException("k", "k = 0 so the result will be infinity!");
			}
			return offset + lambda * Math.Pow(-Math.Log(1 - cdf), 1 / k);
		}

		public double WeibullSample(double offset, double lambda, double k)
		{
			return WeibullCDFInv(Uniform01(), offset, lambda, k);
		}
		#endregion

		#region " Uniform "

		// Uniform deviate in the integer interval [min, max]
		public int IntUniform(int min, int max)
		{
			double r = Uniform01();
			int result = Convert.ToInt32(System.Math.Floor(r * (max - min + 1))) + min;

			return result;
		}

		public static double[] Accumulate(IEnumerable<double> freqArray)
		{
			Debug.Assert(freqArray != null);
			Debug.Assert(freqArray.Count > 0);

			double[] a = new double[freqArray.Count];

			a(0) = freqArray(0);

			for (i = 1; i <= freqArray.Count - 1; i++) {
				a(i) = a(i - 1) + freqArray(i);
			}

			return a;
		}

		/// <summary>
		///  Samples a discrete PDF given by an array.
		/// </summary>
		/// <param name="freqArray"></param>
		/// <returns></returns>
		/// <remarks></remarks>
		public int PDFSample(double[] freqArray)
		{
			double U = Uniform01();
			double accum = 0;

			for (i = 0; i <= Information.UBound(freqArray); i++) {
				accum += freqArray(i);
				if (U < accum) {
					return i;
				}
			}
			return Information.UBound(freqArray);
		}

		/// <summary>
		///  Samples a discrete CDF given by an array.
		/// </summary>
		/// <param name="accumArray"></param>
		/// <returns></returns>
		/// <remarks></remarks>
		public int CDFSample(double[] accumArray)
		{
			double U = Uniform01();

			for (i = 0; i <= Information.UBound(accumArray); i++) {
				if (U < accumArray(i)) {
					return i;
				}
			}
			return Information.UBound(accumArray);
		}

		public double PDFSample(double[] freqArray, double[] values)
		{
			double U = Uniform01();
			double accum = 0;

			for (i = 0; i <= Information.UBound(freqArray); i++) {
				accum += freqArray(i);
				if (U < accum) {
					return values(i);
				}
			}
			return values(Information.UBound(freqArray));
		}

		public T CDFSample<T>(double[] accumArray, IEnumerable<T> values)
		{
			double U = Uniform01();

			for (i = 0; i <= Information.UBound(accumArray); i++) {
				if (U < accumArray(i)) {
					return values(i);
				}
			}
			return values(Information.UBound(accumArray));
		}

		public void ChangeSeed(int newseed)
		{
			rng = new Random(newseed % (int.MaxValue - 1));
		}

		// Uniform deviate in the real interval [0.0, 1.0)
		public double Uniform01()
		{

			double result = rng.NextDouble();
			LastFour.Enqueue(result);

			return result;
		}


		/// <summary>
		/// Uniform deviate in the real interval [min, max)
		/// </summary>
		/// <param name="min"></param>
		/// <param name="max"></param>
		/// <returns></returns>
		/// <remarks>
		/// </remarks>
		/// <history>
		/// 	[icalvo]	09/03/2006	Created
		/// </history>
		public double Uniform(double min, double max)
		{
			return Uniform01() * (max - min) + min;
		}

		/// <summary>
		///   The random round function returns for a given x a random 
		/// integer variable between floor(x) and ceiling(x), with mean = x.
		/// </summary>
		/// <param name="mean">Desired mean</param>
		/// <returns>Random variable</returns>
		/// <remarks>
		///  Let A be an array of RandomRound(x) results.
		///  The greater A is, the closer its mean with x.
		/// </remarks>
		/// <history>
		/// 	[icalvo]	09/03/2006	Created
		/// </history>
		public int RandomRound(double mean)
		{
			int floor = Convert.ToInt32(System.Math.Floor(mean));
			int ceiling = Convert.ToInt32(System.Math.Ceiling(mean));
			double sample = Uniform01();
			if (sample < ceiling - mean) {
				return ceiling;
			} else {
				return floor;
			}
		}

		#endregion

		#region " Exponential "

		public double Exponential01Sample()
		{
			return -System.Math.Log(1 - Uniform01());
		}

		public double ExponentialSample(double off, double lambda)
		{
			return ExponentialCDFInv(Uniform01(), off, lambda);
		}

		public static double ExponentialCDFInv(double cdf, double off, double lambda)
		{
			double result = 0;

			if (cdf < 0.0 || 1.0 < cdf) {
				throw new ArgumentOutOfRangeException("CDF value is not in [0,1] (" + cdf + ")");
			}

			if (lambda <= 0.0) {
				throw new ArgumentOutOfRangeException("Exponential is not defined for lambda <= 0.0 (" + lambda + ")");
			}

			result = off - lambda * Math.Log(1.0 - cdf);

			return result;
		}

		#endregion

		#region " Erlang "

		public double ErlangSample(double off, double mean, int K)
		{
			double result = 0;

			result = off;

			for (int i = 1; i <= K; i++) {
				result += ExponentialSample(0.0, mean);
			}

			return result / K;
		}

		#endregion

	}
}

//=======================================================
//Service provided by Telerik (www.telerik.com)
//Conversion powered by NRefactory.
//Twitter: @telerik
//Facebook: facebook.com/telerik
//=======================================================
