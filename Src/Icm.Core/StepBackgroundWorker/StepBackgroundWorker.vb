Imports System.ComponentModel

Namespace Icm.ComponentModel

    ''' <summary>
    ''' Represents a background worker which performs a series of steps. Between the
    ''' steps, the execution can be interrupted, but not inside them. This allows for the
    ''' inheritors to forget about all the worker issues.
    ''' </summary>
    ''' <remarks></remarks>
    Public Class StepBackgroundWorker(Of TState As IStepWorkState)

        Public Event ProgressChanged As EventHandler(Of EventArgs(Of TState))
        Public Event Completed As EventHandler(Of EventArgs(Of TState))
        Public Event Stopped As EventHandler(Of EventArgs(Of TState))
        Public Event ErrorHappened As EventHandler(Of ErrorEventArgs(Of TState))

        Private WithEvents worker_ As New BackgroundWorker
        Private work_ As IStepWork(Of TState)

        Public Sub New(ByVal _work As IStepWork(Of TState))

            worker_.WorkerSupportsCancellation = True
            worker_.WorkerReportsProgress = True
            work_ = _work

        End Sub

        Private Sub worker__Disposed(ByVal sender As Object, ByVal e As System.EventArgs) Handles worker_.Disposed
            work_.Dispose()
        End Sub

        Private Sub worker__DoWork(ByVal sender As Object, ByVal e As DoWorkEventArgs) Handles worker_.DoWork
            work_.StartExecution()
            Do
                If work_.WorkIsDone Then
                    e.Cancel = False
                    e.Result = work_.StateData
                    Exit Do
                End If
                work_.DoStep()
                work_.StateData.StepNumber += 1
                worker_.ReportProgress(0, work_.StateData)
                If work_.WorkIsDone Then
                    e.Cancel = False
                    e.Result = work_.StateData
                    Exit Do
                End If
                If worker_.CancellationPending Then
                    e.Cancel = True
                    e.Result = work_.StateData
                    Exit Do
                End If
            Loop
            work_.EndExecution()
        End Sub

        Public Sub RunAsync()
            worker_.RunWorkerAsync(work_.StateData)
        End Sub

        Public Sub CancelAsync()
            worker_.CancelAsync()
        End Sub

        Public Sub ToggleAsync()
            If IsBusy Then
                CancelAsync()
            Else
                RunAsync()
            End If
        End Sub

        ReadOnly Property IsBusy As Boolean
            Get
                Return worker_.IsBusy
            End Get
        End Property

        Private Sub worker__ProgressChanged(ByVal sender As Object, ByVal e As ProgressChangedEventArgs) Handles worker_.ProgressChanged
            RaiseEvent ProgressChanged(sender, New EventArgs(Of TState)(CType(e.UserState, TState)))
        End Sub

        Private Sub worker__RunWorkerCompleted(ByVal sender As Object, ByVal e As RunWorkerCompletedEventArgs) Handles worker_.RunWorkerCompleted
            If e.Error IsNot Nothing Then
                RaiseEvent ErrorHappened(sender, New ErrorEventArgs(Of TState)(work_.StateData, e.Error))
            ElseIf e.Cancelled Then
                RaiseEvent Stopped(sender, New EventArgs(Of TState)(work_.StateData))
            Else
                RaiseEvent Completed(sender, New EventArgs(Of TState)(work_.StateData))
            End If
        End Sub
    End Class

End Namespace
