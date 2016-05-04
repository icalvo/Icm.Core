using System;
using System.Diagnostics;
using Icm.Localization;
using static Icm.Localization.PhraseFactory;

namespace Icm
{

	/// <summary>
	/// Defines a non-negative interval between two given dates.
	/// </summary>
	/// <remarks>
	/// Every modification that produces a negative interval will throw an exception.
	/// </remarks>
	[DebuggerDisplay("{Description()}")]
	public class DateTimeInterval
	{
	    private DateTime end_;
		public DateTime Start { get; private set; }


	    public DateTime End {
			get { return end_; }
			set { SetInterval(Start, value); }
		}

		public DateTimeInterval(DateTime i, DateTime f)
		{
			SetInterval(i, f);
		}

		public DateTimeInterval(DateTime i, TimeSpan dur)
		{
			SetInterval(i, dur);
		}

		public DateTimeInterval(DateTime point)
		{
			SetInterval(point, point);
		}

		public TimeSpan Duration => End.Subtract(Start);

	    public void StartWith(DateTime newStart)
		{
			Offset(newStart.Subtract(Start));
		}

		public void Offset(TimeSpan offsetSpan)
		{
			DateTime newEnd = default(DateTime);
			if (DateTime.MaxValue.Subtract(end_) > offsetSpan) {
				newEnd = end_.Add(offsetSpan);
			} else {
				newEnd = DateTime.MaxValue;
			}
			SetInterval(Start.Add(offsetSpan), newEnd);
		}

		public void SetDuration(TimeSpan newDuration)
		{
			if (newDuration < TimeSpan.Zero)
				throw new ArgumentOutOfRangeException(nameof(newDuration), "Duration must not be negative");

			SetInterval(Start, Start.Add(newDuration));
		}

		/// <summary>
		/// This is the only method that actually modifies the values of the interval.
		/// Every other modification method use it, directly or indirectly.
		/// </summary>
		/// <param name="startDate"></param>
		/// <param name="endDate"></param>
		/// <remarks></remarks>
		public void SetInterval(DateTime startDate, DateTime endDate)
		{
			if (startDate > endDate)
				throw new ArgumentException("Start date must be less or equal than end date");
			if (startDate == DateTime.MaxValue)
				throw new ArgumentOutOfRangeException(nameof(startDate), "Start date must not be Date.MaxValue");

			Start = startDate;
			end_ = endDate;
		}

		public void SetInterval(DateTime startDate, TimeSpan dur)
		{
			if (dur < TimeSpan.Zero)
				throw new ArgumentOutOfRangeException("Duration must not be negative");
			SetInterval(startDate, startDate.Add(dur));
		}

		public void SetPoint(DateTime pointDate)
		{
			SetInterval(pointDate, pointDate);
		}

		public virtual Phrase Description()
		{
		    if (End == DateTime.MaxValue) {
				return PhrF("from {0:dd/MM/yyyy HH:mm:ss}, indefinitely", Start);
			}
		    if (End == DateTime.MinValue) {
		        return PhrF("{0:dd/MM/yyyy HH:mm:ss}", Start);
		    }
		    return PhrF("between {0:dd/MM/yyyy HH:mm:ss} and {1:dd/MM/yyyy HH:mm:ss} ({2})", Start, End, Duration.ToAbbrev());
		}

	    public bool Contains(DateTime aDate)
		{
			return Start <= aDate && aDate < End;
		}
	}
}