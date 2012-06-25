
Namespace Icm.Configuration

    Public Class TypedValue
        Public ReadOnly Type As Type
        Public ReadOnly Value As Object

        Public Sub New(ByVal t As Type, ByVal val As Object)
            Debug.Assert(val Is Nothing OrElse val.GetType Is t)
            Type = t
            Value = val
        End Sub
    End Class

End Namespace
