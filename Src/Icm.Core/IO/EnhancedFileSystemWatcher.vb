Imports System.IO

Namespace Icm.IO

    Public Class EnhancedFileSystemWatcher
        Inherits FileSystemWatcher

        Protected Property LastDeletion() As FileSystemEventArgs

        Protected Property LastDeletionDate() As Date

        Protected Property Ahora() As Date

        Public Event Creado As EventHandler(Of FileSystemEventArgs)
        Public Event Modificado As EventHandler(Of FileSystemEventArgs)
        Public Event Borrado As EventHandler(Of FileSystemEventArgs)
        Public Event Renombrado As EventHandler(Of RenamedEventArgs)
        Public Event Movido As EventHandler(Of MovementEventArgs)

        Public Sub New(ByVal path As String)
            MyBase.New(path)
            tBorrado_ = New System.Timers.Timer(15) With {.AutoReset = False}
            AddHandler tBorrado_.Elapsed, AddressOf ChequearÚltimoBorrado
        End Sub

        Private Sub base_Changed(ByVal sender As Object, ByVal e As FileSystemEventArgs) Handles MyBase.Changed, MyBase.Created, MyBase.Deleted
            Ahora = Now
            Debug.WriteLine(String.Format("Changed {0} {1}", e.ChangeType, e.FullPath))
            Select Case e.ChangeType
                Case WatcherChangeTypes.Deleted, WatcherChangeTypes.Created
                    ChequearMovimiento(e)
                Case WatcherChangeTypes.Changed
                    RaiseEvent Modificado(sender, e)
                Case WatcherChangeTypes.Renamed, WatcherChangeTypes.All
                Case Else
            End Select
        End Sub

        Private Sub base_Renamed(ByVal sender As Object, ByVal e As RenamedEventArgs) Handles MyBase.Renamed
            RaiseEvent Renombrado(sender, e)
        End Sub

        Private ReadOnly tBorrado_ As System.Timers.Timer

        Private Sub ChequearÚltimoBorrado(ByVal s As Object, ByVal e As System.Timers.ElapsedEventArgs)
            If Not LastDeletion Is Nothing Then
                RaiseEvent Borrado(Me, LastDeletion)
                LastDeletion = Nothing
            End If
        End Sub

        Private Sub ChequearMovimiento(ByVal e As FileSystemEventArgs)
            Select Case e.ChangeType
                Case WatcherChangeTypes.Deleted
                    tBorrado_.Stop()
                    ' Si tenemos otro borrado deberemos lanzar
                    ' el evento del mismo (aunque eso suponga perder
                    ' un posible movimiento).
                    If Not LastDeletion Is Nothing Then
                        RaiseEvent Borrado(Me, LastDeletion)
                    End If
                    ' Ahora anotamos el evento de borrado actual
                    LastDeletion = e
                    LastDeletionDate = Now
                    ' Ponemos en marcha el temporizador. Si se trata
                    ' realmente de un borrado, se disparará.
                    tBorrado_.Start()
                Case WatcherChangeTypes.Created
                    tBorrado_.Stop()
                    If Ahora.Subtract(LastDeletionDate).Milliseconds < 10 Then
                        If Not LastDeletion Is Nothing Then
                            RaiseEvent Movido(Me, New MovementEventArgs(LastDeletion.FullPath, e.FullPath))
                        End If
                        LastDeletion = Nothing
                    Else
                        If Not LastDeletion Is Nothing Then
                            RaiseEvent Borrado(Me, LastDeletion)
                            LastDeletion = Nothing
                        End If
                        RaiseEvent Creado(Me, e)
                    End If
                Case WatcherChangeTypes.Renamed, WatcherChangeTypes.Changed, WatcherChangeTypes.All
                Case Else
            End Select
        End Sub


    End Class

End Namespace