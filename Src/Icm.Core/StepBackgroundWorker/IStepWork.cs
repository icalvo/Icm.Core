using System;

namespace Icm.ComponentModel
{

	public interface IStepWork<TState> : IDisposable where TState : IStepWorkState
	{

		/// <summary>
		/// This variable holds data with the execution state of the work.
		/// It contains the necessary input and output data.
		/// </summary>
		/// <value></value>
		/// <returns></returns>
		/// <remarks></remarks>

		TState StateData { get; set; }
		/// <summary>
		/// Method executed before starting to execute steps.
		/// </summary>
		/// <remarks>
		/// Use StateData.StepNumber = 0 to identify the initial call and place
		/// work-wide initializations.
		/// </remarks>
		void StartExecution();

		/// <summary>
		/// Do one step of work.
		/// </summary>
		/// <remarks>
		/// To signal an unrecoverable error, throw an exception.
		/// </remarks>
		void DoStep();

		/// <summary>
		/// Method executed after executing steps.
		/// </summary>
		/// <remarks>
		/// Use WorkIsDone to identify if it is the last call or just a stop.
		/// </remarks>
		void EndExecution();

		/// <summary>
		/// Is the whole work done?
		/// </summary>
		/// <returns></returns>
		/// <remarks></remarks>
		bool WorkIsDone();
	}

}