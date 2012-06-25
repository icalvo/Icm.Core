Imports System.Runtime.CompilerServices

Namespace Icm

    Public Module BooleanExtensions

        ''' <summary>
        ''' Converts a boolean to 0 or 1.
        ''' </summary>
        ''' <param name="b">Boolean to convert</param>
        ''' <returns>0 if b is false, 1 otherwise</returns>
        ''' <remarks></remarks>
        <Extension()>
        Public Function ToInteger(ByVal b As Boolean) As Integer
            If b Then
                Return 1
            Else
                Return 0
            End If
        End Function

        ''' <summary>
        ''' Tristate If for nullable booleans.
        ''' </summary>
        ''' <typeparam name="T"></typeparam>
        ''' <param name="bool"></param>
        ''' <param name="truePart">Value returned if bool is true</param>
        ''' <param name="falsePart">Value returned if bool is false</param>
        ''' <param name="nullPart">Value returned if bool has no value</param>
        ''' <returns></returns>
        ''' <remarks></remarks>
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
