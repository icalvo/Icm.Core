namespace Icm.Collections.Generic
{

	/// <summary>
	/// Divides the domain of T into partitions each identified by an integer number.
	/// </summary>
	/// <typeparam name="T"></typeparam>
	/// <remarks></remarks>
	public interface IPeriodManager<T>
	{

		int Period(T obj);

		T PeriodStart(int period);

	}
}