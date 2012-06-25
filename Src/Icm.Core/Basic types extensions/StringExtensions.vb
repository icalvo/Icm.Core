Imports System.Runtime.CompilerServices
Imports System.Globalization

Namespace Icm

    ''' <summary>
    ''' Utility class with String-related functions.
    ''' </summary>
    ''' <remarks>
    ''' </remarks>
    ''' <history>
    ''' 	[icalvo]    19/08/2004	Created
    ''' </history>
    Public Module StringExtensions

        ''' <summary>
        ''' Substring function with PHP <a href="http://php.net/manual/es/function.substr.php">substr</a> syntax.
        ''' </summary>
        ''' <param name="s"></param>
        ''' <param name="startIdx"></param>
        ''' <param name="length"></param>
        ''' <returns></returns>
        ''' <remarks>Where PHP's substr returns FALSE, this function throws an ArgumentOutOfRangeException</remarks>
        <Extension()>
        Public Function Substr(ByVal s As String, ByVal startIdx As Integer, ByVal length As Integer) As String
            Dim startIdx2 As Integer
            Dim length2 As Integer
            If startIdx < 0 Then
                startIdx2 = s.Length + startIdx
                If startIdx2 < 0 Then
                    Throw New ArgumentOutOfRangeException("startIdx", startIdx, "Negative startIdx less than -s.Length")
                End If
            Else
                startIdx2 = startIdx
            End If
            If length < 0 Then
                length2 = s.Length - startIdx2 + length
                If length2 < 0 Then
                    Throw New ArgumentOutOfRangeException("length", length, "Negative length less than available")
                End If
            Else
                length2 = length
            End If
            Return s.Substring(startIdx2, length2)
        End Function

        ''' <summary>
        ''' Substring between two indices
        ''' </summary>
        ''' <param name="s"></param>
        ''' <param name="startIdx"></param>
        ''' <param name="endIdx"></param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        <Extension()>
        Public Function Med(ByVal s As String, ByVal startIdx As Integer, ByVal endIdx As Integer) As String
            Return s.Substring(startIdx, endIdx - startIdx + 1)
        End Function

        ''' <summary>
        ''' Skips some characters from the start and some from the end of a string.
        ''' </summary>
        ''' <param name="s"></param>
        ''' <param name="startLength"></param>
        ''' <param name="endLength"></param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        <Extension()>
        Public Function SkipBoth(ByVal s As String, ByVal startLength As Integer, ByVal endLength As Integer) As String
            Return s.Substring(startLength, s.Length - startLength - endLength)
        End Function

        ''' <summary>
        ''' Similar to VB6 Left function.
        ''' </summary>
        ''' <param name="s"></param>
        ''' <param name="length"></param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        <Extension()>
        Public Function Left(ByVal s As String, ByVal length As Integer) As String
            Return s.Substring(0, length)
        End Function

        ''' <summary>
        ''' Similar to VB6 Right function.
        ''' </summary>
        ''' <param name="s"></param>
        ''' <param name="length"></param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        <Extension()>
        Public Function Right(ByVal s As String, ByVal length As Integer) As String
            Return s.Substring(s.Length - length)
        End Function

        ''' <summary>
        ''' Abbreviation of 'StartsWith AndAlso EndsWith'
        ''' </summary>
        ''' <param name="s"></param>
        ''' <param name="startS"></param>
        ''' <param name="endS"></param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        <Extension()>
        Public Function SurroundedBy(ByVal s As String, ByVal startS As String, ByVal endS As String) As Boolean
            Return s.StartsWith(startS) AndAlso s.EndsWith(endS)
        End Function

        ''' <summary>
        '''   Creates a new string by repeating count times the string s.
        ''' </summary>
        ''' <param name="s">String to be repeated.</param>
        ''' <param name="count"># of times to repeat s.</param>
        ''' <returns>New string created by repeating s.</returns>
        ''' <remarks>
        ''' </remarks>
        ''' <history>
        ''' 	[icalvo]	26/11/2004	Created
        '''     [icalvo]    31/03/2005  Documented
        ''' </history>
        <Extension()>
        Public Function Repeat(ByVal s As String, ByVal count As Integer) As String
            If count = 0 Then
                Return ""
            End If
            Dim sb As New System.Text.StringBuilder(s.Length * count)
            For i As Integer = 1 To count
                sb.Append(s)
            Next
            Return sb.ToString
        End Function

        ''' <summary>
        '''  Creates a new string by converting the first char of s to upper case.
        ''' </summary>
        ''' <param name="s">Original string.</param>
        ''' <returns>New string equal to s except the first char to upper case.</returns>
        ''' <remarks>
        ''' Tested successfully with accented characters and strings number
        ''' </remarks>
        ''' <history>
        ''' 	[icalvo]	19/08/2004	Created
        '''     [icalvo]    31/03/2005  Documented
        ''' </history>
        <Extension()>
        Public Function ToUpperFirst(ByVal s As String) As String
            If s = Nothing OrElse s = "" Then
                Return ""
            Else
                Return Char.ToUpper(s.Chars(0), CultureInfo.CurrentCulture) & s.Substring(1, s.Length - 1)
            End If
        End Function

        ''' <summary>
        ''' 
        ''' </summary>
        ''' <param name="s"></param>
        ''' <param name="sa"></param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        <Extension()>
        Public Function IsOneOf(Of T)(ByVal s As T, ByVal ParamArray sa As T()) As Boolean
            If sa Is Nothing Then
                Return False
            End If
            Return sa.Contains(s)
        End Function

        <Extension()>
        Public Function IfEmpty(ByVal s As String, ByVal emptyString As String) As String
            If s = "" Then
                Return emptyString
            Else
                Return s
            End If
        End Function
        <Extension()>
        Public Function IsEmpty(ByVal s As String) As Boolean
            If s = "" Then
                Return True
            Else
                Return False
            End If
        End Function
    End Module

End Namespace
