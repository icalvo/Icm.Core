using System;
using System.ComponentModel;

namespace Icm.ComponentModel
{

	/// <summary>
	/// Represents a background worker which performs a series of steps. Between the
	/// steps, the execution can be interrupted, but not inside them. This allows for the
	/// inheritors to forget about all the worker issues.
	/// </summary>
	/// <remarks></remarks>
	public class StepBackgroundWorker<TState> where TState : IStepWorkState
	{

		public event EventHandler<EventArgs<TState>> ProgressChanged;
		public event EventHandler<EventArgs<TState>> Completed;
		public event EventHandler<EventArgs<TState>> Stopped;
		public event EventHandler<ErrorEventArgs<TState>> ErrorHappened;

		private BackgroundWorker _withEventsFieldWorker = new BackgroundWorker();
	    private readonly IStepWork<TState> _work;

	    private BackgroundWorker Worker
        {
			get { return _withEventsFieldWorker; }
			set {
				if (_withEventsFieldWorker != null) {
					_withEventsFieldWorker.Disposed -= worker__Disposed;
					_withEventsFieldWorker.DoWork -= worker__DoWork;
					_withEventsFieldWorker.ProgressChanged -= worker__ProgressChanged;
					_withEventsFieldWorker.RunWorkerCompleted -= worker__RunWorkerCompleted;
				}
				_withEventsFieldWorker = value;
				if (_withEventsFieldWorker != null) {
					_withEventsFieldWorker.Disposed += worker__Disposed;
					_withEventsFieldWorker.DoWork += worker__DoWork;
					_withEventsFieldWorker.ProgressChanged += worker__ProgressChanged;
					_withEventsFieldWorker.RunWorkerCompleted += worker__RunWorkerCompleted;
				}
			}
		}

	    public StepBackgroundWorker(IStepWork<TState> work)
		{
			Worker.WorkerSupportsCancellation = true;
			Worker.WorkerReportsProgress = true;
			this._work = work;

		}

		private void worker__Disposed(object sender, System.EventArgs e)
		{
			_work.Dispose();
		}

		private void worker__DoWork(object sender, DoWorkEventArgs e)
		{
			_work.StartExecution();
			do {
				if (_work.WorkIsDone()) {
					e.Cancel = false;
					e.Result = _work.StateData;
					break; // TODO: might not be correct. Was : Exit Do
				}

				_work.DoStep();
				_work.StateData.StepNumber += 1;
				Worker.ReportProgress(0, _work.StateData);
				if (_work.WorkIsDone()) {
					e.Cancel = false;
					e.Result = _work.StateData;
					break; // TODO: might not be correct. Was : Exit Do
				}

				if (Worker.CancellationPending) {
					e.Cancel = true;
					e.Result = _work.StateData;
					break; // TODO: might not be correct. Was : Exit Do
				}
			} while (true);
			_work.EndExecution();
		}

		public void RunAsync()
		{
			Worker.RunWorkerAsync(_work.StateData);
		}

		public void CancelAsync()
		{
			Worker.CancelAsync();
		}

		public void ToggleAsync()
		{
			if (IsBusy) {
				CancelAsync();
			} else {
				RunAsync();
			}
		}

		public bool IsBusy => Worker.IsBusy;

	    private void worker__ProgressChanged(object sender, ProgressChangedEventArgs e)
		{
		    ProgressChanged?.Invoke(sender, new EventArgs<TState>((TState)e.UserState));
		}

	    private void worker__RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
		{
			if (e.Error != null)
			{
			    ErrorHappened?.Invoke(sender, new ErrorEventArgs<TState>(_work.StateData, e.Error));
			}
			else if (e.Cancelled)
			{
			    Stopped?.Invoke(sender, new EventArgs<TState>(_work.StateData));
			}
			else
			{
			    Completed?.Invoke(sender, new EventArgs<TState>(_work.StateData));
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
