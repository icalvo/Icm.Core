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

		private static Random _shrng = new Random();

        public static void ChangeSeed(int newseed)
		{
			_shrng = new Random(newseed);
		}

		public static double Uniform01()
		{
			double result = _shrng.NextDouble();
			return result;
		}

		public static double Uniform(double min, double max)
		{
			return Uniform01() * (max - min) + min;
		}

	}
}