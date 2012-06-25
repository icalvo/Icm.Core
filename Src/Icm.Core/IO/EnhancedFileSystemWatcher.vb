Imports System.IO

Namespace Icm.IO

    Public Class EnhancedFileSystemWatcher
        Inherits FileSystemWatcher

        Protected Property �ltimoBorrado() As FileSystemEventArgs

        Protected Property Fecha�ltimoBorrado() As Date

        Protected Property Ahora() As Date

        Public Event Creado(ByVal sender As Object, ByVal e As FileSystemEventArgs)
        Public Event Modificado(ByVal sender As Object, ByVal e As FileSystemEventArgs)
        Public Event Borrado(ByVal sender As Object, ByVal e As FileSystemEventArgs)
        Public Event Renombrado(ByVal sender As Object, ByVal e As RenamedEventArgs)
        Public Event Movido(ByVal sender As Object, ByVal e As MovementEventArgs)

        Public Sub New(ByVal path As String)
            MyBase.New(path)
            tBorrado_ = New System.Timers.Timer(15) With {.AutoReset = False}
            AddHandler tBorrado_.Elapsed, AddressOf Chequear�ltimoBorrado
        End Sub

        Private Sub base_Changed(ByVal sender As Object, ByVal e As FileSystemEventArgs) Handles MyBase.Changed, MyBase.Created, MyBase.Deleted
            Ahora = Now
            Debug.WriteLine("Changed {0} {1}", e.ChangeType, e.FullPath)
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

        Private Sub Chequear�ltimoBorrado(ByVal s As Object, ByVal e As System.Timers.ElapsedEventArgs)
            If Not �ltimoBorrado Is Nothing Then
                RaiseEvent Borrado(Me, �ltimoBorrado)
                �ltimoBorrado = Nothing
            End If
        End Sub

        Private Sub ChequearMovimiento(ByVal e As FileSystemEventArgs)
            Select Case e.ChangeType
                Case WatcherChangeTypes.Deleted
                    tBorrado_.Stop()
                    ' Si tenemos otro borrado deberemos lanzar
                    ' el evento del mismo (aunque eso suponga perder
                    ' un posible movimiento).
                    If Not �ltimoBorrado Is Nothing Then
                        RaiseEvent Borrado(Me, �ltimoBorrado)
                    End If
                    ' Ahora anotamos el evento de borrado actual
                    �ltimoBorrado = e
                    Fecha�ltimoBorrado = Now
                    ' Ponemos en marcha el temporizador. Si se trata
                    ' realmente de un borrado, se disparar�.
                    tBorrado_.Start()
                Case WatcherChangeTypes.Created
                    tBorrado_.Stop()
                    If Ahora.Subtract(Fecha�ltimoBorrado).Milliseconds < 10 Then
                        If Not �ltimoBorrado Is Nothing Then
                            RaiseEvent Movido(Me, New MovementEventArgs(�ltimoBorrado.FullPath, e.FullPath))
                        End If
                        �ltimoBorrado = Nothing
                    Else
                        If Not �ltimoBorrado Is Nothing Then
                            RaiseEvent Borrado(Me, �ltimoBorrado)
                            �ltimoBorrado = Nothing
                        End If
                        RaiseEvent Creado(Me, e)
                    End If
                Case WatcherChangeTypes.Renamed, WatcherChangeTypes.Changed, WatcherChangeTypes.All
                Case Else
            End Select
        End Sub


    End Class

End Namespace