Imports System.IO

Namespace Icm.IO
    Public Class FileSystemEnumerator
        Implements Generic.IEnumerator(Of FileSystemInfo)
        Protected basePath_ As DirectoryInfo
        Protected queue_ As Generic.Queue(Of FileSystemInfo)
        Public Sub New(ByVal bp As String)
            Me.New(New DirectoryInfo(bp))
        End Sub
        Public Sub New(ByVal bp As DirectoryInfo)
            basePath_ = bp
            queue_ = New Generic.Queue(Of FileSystemInfo)()
            Reset()
        End Sub
        Public Sub Reset() Implements System.Collections.IEnumerator.Reset
            queue_.Clear()
            queue_.Enqueue(basePath_)
        End Sub
        ' IDisposable
        Protected Overridable Sub Dispose(ByVal disposing As Boolean)
        End Sub
        ' This code added by Visual Basic to correctly implement the disposable pattern.
        Public Sub Dispose() Implements IDisposable.Dispose
            ' Do not change this code.  Put cleanup code in Dispose(ByVal disposing As Boolean) above.
            Dispose(True)
            GC.SuppressFinalize(Me)
        End Sub
        Public ReadOnly Property Current() As FileSystemInfo Implements System.Collections.Generic.IEnumerator(Of FileSystemInfo).Current
            Get
                Return queue_.Peek
            End Get
        End Property
        Public ReadOnly Property Current1() As Object Implements System.Collections.IEnumerator.Current
            Get
                Return Me.Current
            End Get
        End Property
        Public Function MoveNext() As Boolean Implements System.Collections.IEnumerator.MoveNext
            If queue_.Count = 0 Then
                Return False
            Else
                Dim fsi As FileSystemInfo
                fsi = queue_.Dequeue
                Dim di = TryCast(fsi, DirectoryInfo)
                If di IsNot Nothing Then
                    Try
                        If di.Exists Then
                            For Each fsi2 As FileSystemInfo In di.GetFileSystemInfos
                                queue_.Enqueue(fsi2)
                            Next
                        Else
                            ' Directory does not exist
                        End If
                    Catch ex As Exception
                    End Try
                End If
                Return queue_.Count <> 0
            End If
        End Function
    End Class
End Namespace
