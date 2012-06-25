Imports System.IO

Namespace Icm.IO

    Public Class FileSystemEnumerable
        Implements Generic.IEnumerable(Of FileSystemInfo)

        Protected basePath_ As DirectoryInfo

        Public Sub New(ByVal bp As String)
            basePath_ = New DirectoryInfo(bp)
        End Sub

        Public Sub New(ByVal bp As DirectoryInfo)
            basePath_ = bp
        End Sub

        Public Function GetEnumerator() As System.Collections.Generic.IEnumerator(Of System.IO.FileSystemInfo) Implements System.Collections.Generic.IEnumerable(Of System.IO.FileSystemInfo).GetEnumerator
            Return New FileSystemEnumerator(basePath_)
        End Function

        Public Function GetEnumerator1() As System.Collections.IEnumerator Implements System.Collections.IEnumerable.GetEnumerator
            Return Me.GetEnumerator
        End Function
    End Class

End Namespace
