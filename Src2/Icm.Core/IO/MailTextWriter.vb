Imports System.Net.Mail
Imports System.Text
Imports System.IO

Namespace Icm.Net.Mail

    Public Class MailTextWriter
        Inherits TextWriter

        Protected message_ As MailMessage
        Protected smtp_ As New SmtpClient
        Protected sb_ As StringBuilder

        Public Sub New()
            message_ = New MailMessage
            sb_ = New StringBuilder
        End Sub

        ReadOnly Property Message() As MailMessage
            Get
                Return message_
            End Get
        End Property

        ReadOnly Property SMTP() As SmtpClient
            Get
                Return smtp_
            End Get
        End Property

        Public Overloads Overrides Sub Write(ByVal c As Char)
            sb_.Append(c)
        End Sub

        Public Overloads Overrides Sub Write(ByVal s As String)
            sb_.Append(s)
        End Sub

        Public Sub Send()
            message_.Body = sb_.ToString
            SMTP.Send(message_)
            sb_.Length = 0
        End Sub

        Public Sub Send(ByVal header As String, ByVal footer As String)
            message_.Body = header & sb_.ToString & footer
            SMTP.Send(message_)
            sb_.Length = 0
        End Sub

        Public Overrides ReadOnly Property Encoding() As Encoding
            Get
                Return Encoding.Default
            End Get
        End Property
    End Class

End Namespace
