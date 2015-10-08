using System;

namespace Icm.MathTools
{
	/// <summary>
	///  Module for easy statistical data generation. Not safe for multithreading,
	/// though, you should use one <see cref="StatisticsGenerator"></see> for each thread in order to
	/// avoid problems.
	/// </summary>
	/// <remarks></remarks>
	public static class STAT
	{

		private static Random shrng = new Random();
		public static void ChangeSeed(int newseed)
		{
			shrng = new Random(newseed);
		}

		public STAT()
		{
			shrng = new Random();
		}

		public static double Uniform01()
		{

			double result = shrng.NextDouble();

			return result;
		}

		public static double Uniform(double min, double max)
		{
			return Uniform01() * (max - min) + min;
		}

	}
}

//=======================================================
//Service provided by Telerik (www.telerik.com)
//Conversion powered by NRefactory.
//Twitter: @telerik
//Facebook: facebook.com/telerik
//=======================================================
