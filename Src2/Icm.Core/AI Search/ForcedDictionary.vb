
Namespace Icm.Collections.Generic

    Public Class ForcedDictionary(Of K, V)
        Inherits Dictionary(Of K, V)

        Default Shadows Property Item(ByVal key As K) As V
            Get
                If Not MyBase.ContainsKey(key) Then
                    MyBase.Add(key, Nothing)
                End If
                Return MyBase.Item(key)
            End Get
            Set(ByVal value As V)
                If MyBase.ContainsKey(key) Then
                    MyBase.Item(key) = value
                Else
                    MyBase.Add(key, value)
                End If

            End Set
        End Property
    End Class
End Namespace
