namespace Icm
{
	/// <summary>
	///   Shared incremental counter.
	/// </summary>
	/// <remarks>
	/// </remarks>
	/// <history>
	/// 	[icalvo]	15/09/2005	Created
	/// 	[icalvo]	05/04/2005	Removed FormatFile from Icm.Tools
	///     [icalvo]    05/04/2005  Documentation
	/// </history>
	public class SharedCounter
	{
		private SharedCounter()
		{
		}

		/// <summary>
		///     Shared counter for providing incremental unique (per execution) numbers.
		/// </summary>
		/// <history>
		/// 	[icalvo]	15/09/2005	Created
		///     [icalvo]    05/04/2005  Documentation
		/// </history>

		private static int counter_;
		/// <summary>
		///     New unique (per execution) number.
		/// </summary>
		/// <returns>A new unique (per execution) number.</returns>
		/// <remarks>
		///   Be careful; since the counter is shared, two consecutive calls to
		/// NextNumber could not provide consecutive numbers. NextNumber is only
		/// guaranteed to provide unique (not necessarily consecutive) numbers.
		/// </remarks>
		/// <history>
		/// 	[icalvo]	15/09/2005	Created
		///     [icalvo]    05/04/2005  Documentation
		/// </history>
		public static int NextNumber()
		{
			counter_ += 1;
			return counter_;
		}

	}
}

//=======================================================
//Service provided by Telerik (www.telerik.com)
//Conversion powered by NRefactory.
//Twitter: @telerik
//Facebook: facebook.com/telerik
//=======================================================
