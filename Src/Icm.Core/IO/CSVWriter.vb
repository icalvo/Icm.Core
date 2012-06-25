Imports System.IO
Imports System.Text

Namespace Icm.IO

    Public Class CSVWriter
        Implements IDisposable

        Private ReadOnly _tw As TextWriter

        Public Sub New(ByVal fn As String)
            _tw = New StreamWriter(fn, False, Encoding.GetEncoding("iso-8859-1"))
        End Sub

        Public Sub AddHeader(ByVal ParamArray fields() As String)
            For Each field In fields
                _tw.Write(field & ";")
            Next
            _tw.WriteLine()
        End Sub

        Public Sub AddHeaderPart(ByVal ParamArray fields() As String)
            For Each field In fields
                _tw.Write(field & ";")
            Next
        End Sub

        Public Sub AddString(ByVal val As String)
            _tw.Write("""{0}"";", val.Replace("""", """"""))
        End Sub

        Public Sub AddNumber(ByVal val As Integer)
            _tw.Write(val & ";")
        End Sub

        Public Sub AddNumber(ByVal val As Double)
            _tw.Write(val & ";")
        End Sub

        Public Sub AddDate(ByVal val As Date)
            If val = Date.MinValue Then
                AddNull()
            Else
                _tw.Write(val.ToString("dd/MM/yyyy HH:mm:ss") & ";")
            End If
        End Sub
        Public Sub AddDate(ByVal val As Date, format As String)
            If val = Date.MinValue Then
                AddNull()
            Else
                _tw.Write(val.ToString(format) & ";")
            End If
        End Sub
        Public Sub AddNull()
            _tw.Write(";")
        End Sub

        Public Sub NewLine()
            _tw.WriteLine()
        End Sub
#Region "IDisposable Support"
        Private disposedValue As Boolean ' Para detectar llamadas redundantes

        ' IDisposable
        Protected Overridable Sub Dispose(ByVal disposing As Boolean)
            If Not Me.disposedValue Then
                If disposing Then
                    _tw.Close()
                End If

            End If
            Me.disposedValue = True
        End Sub

        ' Visual Basic agregó este código para implementar correctamente el modelo descartable.
        Public Sub Dispose() Implements IDisposable.Dispose
            ' No cambie este código. Coloque el código de limpieza en Dispose (ByVal que se dispone como Boolean).
            Dispose(True)
            GC.SuppressFinalize(Me)
        End Sub
#End Region

    End Class

End Namespace
