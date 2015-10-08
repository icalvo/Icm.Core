
using Microsoft.VisualBasic;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
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

		private BackgroundWorker withEventsField_worker_ = new BackgroundWorker();
		private BackgroundWorker worker_ {
			get { return withEventsField_worker_; }
			set {
				if (withEventsField_worker_ != null) {
					withEventsField_worker_.Disposed -= worker__Disposed;
					withEventsField_worker_.DoWork -= worker__DoWork;
					withEventsField_worker_.ProgressChanged -= worker__ProgressChanged;
					withEventsField_worker_.RunWorkerCompleted -= worker__RunWorkerCompleted;
				}
				withEventsField_worker_ = value;
				if (withEventsField_worker_ != null) {
					withEventsField_worker_.Disposed += worker__Disposed;
					withEventsField_worker_.DoWork += worker__DoWork;
					withEventsField_worker_.ProgressChanged += worker__ProgressChanged;
					withEventsField_worker_.RunWorkerCompleted += worker__RunWorkerCompleted;
				}
			}
		}

		private IStepWork<TState> work_;

		public StepBackgroundWorker(IStepWork<TState> _work)
		{
			worker_.WorkerSupportsCancellation = true;
			worker_.WorkerReportsProgress = true;
			work_ = _work;

		}

		private void worker__Disposed(object sender, System.EventArgs e)
		{
			work_.Dispose();
		}

		private void worker__DoWork(object sender, DoWorkEventArgs e)
		{
			work_.StartExecution();
			do {
				if (work_.WorkIsDone) {
					e.Cancel = false;
					e.Result = work_.StateData;
					break; // TODO: might not be correct. Was : Exit Do
				}
				work_.DoStep();
				work_.StateData.StepNumber += 1;
				worker_.ReportProgress(0, work_.StateData);
				if (work_.WorkIsDone) {
					e.Cancel = false;
					e.Result = work_.StateData;
					break; // TODO: might not be correct. Was : Exit Do
				}
				if (worker_.CancellationPending) {
					e.Cancel = true;
					e.Result = work_.StateData;
					break; // TODO: might not be correct. Was : Exit Do
				}
			} while (true);
			work_.EndExecution();
		}

		public void RunAsync()
		{
			worker_.RunWorkerAsync(work_.StateData);
		}

		public void CancelAsync()
		{
			worker_.CancelAsync();
		}

		public void ToggleAsync()
		{
			if (IsBusy) {
				CancelAsync();
			} else {
				RunAsync();
			}
		}

		public bool IsBusy {
			get { return worker_.IsBusy; }
		}

		private void worker__ProgressChanged(object sender, ProgressChangedEventArgs e)
		{
			if (ProgressChanged != null) {
				ProgressChanged(sender, new EventArgs<TState>((TState)e.UserState));
			}
		}

		private void worker__RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
		{
			if (e.Error != null) {
				if (ErrorHappened != null) {
					ErrorHappened(sender, new ErrorEventArgs<TState>(work_.StateData, e.Error));
				}
			} else if (e.Cancelled) {
				if (Stopped != null) {
					Stopped(sender, new EventArgs<TState>(work_.StateData));
				}
			} else {
				if (Completed != null) {
					Completed(sender, new EventArgs<TState>(work_.StateData));
				}
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
