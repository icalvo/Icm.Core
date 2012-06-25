Imports System.Runtime.CompilerServices

Namespace Icm

    Public Module BooleanExtensions

        <Extension()> _
        Public Function ToInteger(ByVal b As Boolean) As Integer
            If b Then
                Return 1
            Else
                Return 0
            End If
        End Function

        <Extension()>
        Public Function IfN(Of T)(ByVal bool As Boolean?, ByVal truePart As T, ByVal falsePart As T, ByVal nullPart As T) As T
            If bool.HasValue Then
                If bool.V Then
                    Return truePart
                Else
                    Return falsePart
                End If
            Else
                Return nullPart
            End If
        End Function
    End Module
End Namespace
