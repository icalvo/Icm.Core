
using Microsoft.VisualBasic;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using Icm.Localization;

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

		private System.DateTime start_;

		private System.DateTime end_;
		public System.DateTime Start {
			get { return start_; }
		}


		public System.DateTime End {
			get { return end_; }
			set { SetInterval(start_, value); }
		}

		public DateTimeInterval(System.DateTime i, System.DateTime f)
		{
			SetInterval(i, f);
		}

		public DateTimeInterval(System.DateTime i, TimeSpan dur)
		{
			SetInterval(i, dur);
		}

		public DateTimeInterval(System.DateTime point)
		{
			SetInterval(point, point);
		}

		public TimeSpan Duration {
			get { return End.Subtract(Start); }
		}

		public void StartWith(System.DateTime newStart)
		{
			Offset(newStart.Subtract(Start));
		}

		public void Offset(TimeSpan offsetSpan)
		{
			System.DateTime newEnd = default(System.DateTime);
			if (System.DateTime.MaxValue.Subtract(end_) > offsetSpan) {
				newEnd = end_.Add(offsetSpan);
			} else {
				newEnd = System.DateTime.MaxValue;
			}
			SetInterval(start_.Add(offsetSpan), newEnd);
		}

		public void SetDuration(TimeSpan newDuration)
		{
			if (newDuration < TimeSpan.Zero)
				throw new ArgumentOutOfRangeException("Duration must not be negative");

			SetInterval(start_, start_.Add(newDuration));
		}

		/// <summary>
		/// This is the only method that actually modifies the values of the interval.
		/// Every other modification method use it, directly or indirectly.
		/// </summary>
		/// <param name="startDate"></param>
		/// <param name="endDate"></param>
		/// <remarks></remarks>
		public void SetInterval(System.DateTime startDate, System.DateTime endDate)
		{
			if (startDate > endDate)
				throw new ArgumentException("Start date must be less or equal than end date");
			if (startDate == System.DateTime.MaxValue)
				throw new ArgumentOutOfRangeException("Start date must not be Date.MaxValue");

			start_ = startDate;
			end_ = endDate;
		}

		public void SetInterval(System.DateTime startDate, TimeSpan dur)
		{
			if (dur < TimeSpan.Zero)
				throw new ArgumentOutOfRangeException("Duration must not be negative");
			SetInterval(startDate, startDate.Add(dur));
		}

		public void SetPoint(System.DateTime pointDate)
		{
			SetInterval(pointDate, pointDate);
		}

		public virtual Phrase Description()
		{
			if (End == System.DateTime.MaxValue) {
				return PhrF("from {0:dd/MM/yyyy HH:mm:ss}, indefinitely", Start);
			} else if (End == System.DateTime.MinValue) {
				return PhrF("{0:dd/MM/yyyy HH:mm:ss}", Start);
			} else {
				return PhrF("between {0:dd/MM/yyyy HH:mm:ss} and {1:dd/MM/yyyy HH:mm:ss} ({2})", Start, End, Duration.ToAbbrev);
			}
		}

		public bool Contains(System.DateTime aDate)
		{
			return Start <= aDate && aDate < End;
		}

	}
}

//=======================================================
//Service provided by Telerik (www.telerik.com)
//Conversion powered by NRefactory.
//Twitter: @telerik
//Facebook: facebook.com/telerik
//=======================================================
