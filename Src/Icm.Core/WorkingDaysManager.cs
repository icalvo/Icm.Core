
using Microsoft.VisualBasic;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
namespace Icm
{

	/// <summary>
	///     Working days manager.
	/// </summary>
	/// <remarks>
	///  This class does calculations with working days. When the weekly holidays
	/// and the holiday dates are properly configured, it can say whether a date
	/// is working or not, find the next/previous working day and add/substract
	/// working days to a given working day.
	/// </remarks>
	/// <history>
	/// 	[icalvo]	20/04/2005	Created
	/// </history>
	public class WorkingDaysManager
	{
		private readonly IEnumerable<System.DateTime> dayHolidays_;

		private readonly IEnumerable<DayOfWeek> weeklyHolidays_;
		/// <summary>
		///  
		/// </summary>
		/// <remarks>
		/// Stablishes saturdays and sundays as holidays.
		/// </remarks>
		/// <history>
		/// 	[icalvo]	20/04/2005	Created
		/// </history>
		public WorkingDaysManager()
		{
			dayHolidays_ = new List<System.DateTime>();
			weeklyHolidays_ = new List<DayOfWeek> {
				DayOfWeek.Saturday,
				DayOfWeek.Sunday
			};
		}

		/// <summary>
		///  
		/// </summary>
		/// <remarks>
		/// 
		/// </remarks>
		/// <history>
		/// 	[icalvo]	20/04/2005	Created
		/// </history>
		public WorkingDaysManager(IEnumerable<System.DateTime> dayHol, IEnumerable<DayOfWeek> weekHol)
		{
			dayHolidays_ = dayHol;
			weeklyHolidays_ = weekHol;
		}
		/// <summary>
		///     List of holiday dates.
		/// </summary>
		/// <value>List of holiday dates.</value>
		/// <remarks>
		///   Should contains only Date-compatible items.
		/// </remarks>
		/// <history>
		/// 	[icalvo]	20/04/2005	Created
		/// </history>
		public IEnumerable<System.DateTime> DayHolidays {
			get { return dayHolidays_; }
		}

		/// <summary>
		///     List of weekly holidays.
		/// </summary>
		/// <value>List of weekly holidays.</value>
		/// <remarks>
		///   Should contains only System.DayOfWeek items.
		/// </remarks>
		/// <history>
		/// 	[icalvo]	20/04/2005	Created
		/// </history>
		public IEnumerable<DayOfWeek> WeeklyHolidays {
			get { return weeklyHolidays_; }
		}

		/// <summary>
		/// Is the given date a working day?
		/// </summary>
		/// <param name="d"></param>
		/// <returns></returns>
		/// <remarks></remarks>
		public bool IsWorking(System.DateTime d)
		{
			return !dayHolidays_.Contains(d) & !weeklyHolidays_.Contains(d.DayOfWeek);
		}

		/// <summary>
		/// Get the next working day greater OR EQUAL to the given date.
		/// </summary>
		/// <param name="d"></param>
		/// <returns></returns>
		/// <remarks></remarks>
		public System.DateTime NextWorkingDay(System.DateTime d)
		{
			System.DateTime result = default(System.DateTime);
			result = d;

			// Advance until we find a working day. Don't advance
			// if d is already a working day.
			while (!(IsWorking(result))) {
				result = result.AddDays(1);
			}


			return result;

		}

		/// <summary>
		/// Get the previous working day less OR EQUAL to the given date.
		/// </summary>
		/// <param name="d">Day</param>
		/// <returns></returns>
		/// <remarks>
		///  
		/// </remarks>
		/// <history>
		/// 	[icalvo]	30/05/2005	Created
		/// </history>
		public System.DateTime PrevWorkingDay(System.DateTime d)
		{

			System.DateTime result = default(System.DateTime);
			result = d;

			// Advance until we find a working day. Don't advance
			// if d is already a working day.
			while (!(IsWorking(result))) {
				result = result.AddDays(-1);
			}

			return result;

		}

		/// <summary>
		///  Adds/substracts working days to a given working day.
		/// </summary>
		/// <param name="d">A working day</param>
		/// <param name="n"></param>
		/// <returns></returns>
		/// <remarks>
		///   This method will raise a
		/// </remarks>
		/// <history>
		/// 	[icalvo]	30/05/2005	Created
		/// </history>
		public System.DateTime AddDays(System.DateTime d, int n)
		{
			Debug.Assert(IsWorking(d), "Cannot add/substract working days to a holiday date", d + " is a holiday date");

			System.DateTime result = default(System.DateTime);
			result = d;

			// Advance (in the direction of n) until we count n working days.
			int added = 0;
			while (!(added == n)) {
				result = result.AddDays(Math.Sign(n));
				if (IsWorking(result)) {
					added += Math.Sign(n);
				}
			}

			return result;
		}

		/// <summary>
		/// Same as AddDays but admits holiday dates. In that case, it starts adding working dates to the working date next to the given holiday.
		/// </summary>
		/// <param name="d"></param>
		/// <param name="n"></param>
		/// <returns></returns>
		/// <remarks></remarks>
		public System.DateTime AddDays2(System.DateTime d, int n)
		{
			if (IsWorking(d)) {
				return AddDays(d, n);
			} else {
				return AddDays(NextWorkingDay(d), n);
			}
		}

	}

}

//=======================================================
//Service provided by Telerik (www.telerik.com)
//Conversion powered by NRefactory.
//Twitter: @telerik
//Facebook: facebook.com/telerik
//=======================================================
