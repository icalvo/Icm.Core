Imports System.Globalization

Namespace Icm.MathTools
    Public Class NumberFiles

        ''' <summary>
        ''' Reads a simple text file of real numbers with the usual
        ''' (invariant culture) admitted formats. Ignores line comments starting with "#".
        ''' </summary>
        ''' <param name="fn"></param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Shared Function ReadArrayFile(ByVal fn As String) As Double()
            Dim sr As New System.IO.StreamReader(fn)
            Dim line As String
            Dim l As New Generic.List(Of Double)
            Dim d As Double
            line = sr.ReadLine()

            Do Until line Is Nothing
                If Not line.StartsWith("#", StringComparison.Ordinal) Then
                    If Double.TryParse(line, _
                            NumberStyles.Float, _
                            CultureInfo.InvariantCulture, _
                            d) Then
                        l.Add(d)
                    End If
                End If
                line = sr.ReadLine()
            Loop
            sr.Close()
            Return l.ToArray
        End Function

        ''' <summary>
        '''  Reads a simple file with a multidimensional array of numbers.
        ''' </summary>
        ''' <param name="fn">File name</param>
        ''' <param name="numbersep">Separator for numbers</param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Shared Function ReadMatrixFile(ByVal fn As String, ByVal numbersep As Char) As Double(,)
            Dim sr As New System.IO.StreamReader(fn)
            Dim line As String
            Dim l As New Generic.List(Of Double())
            Dim lineList As New Generic.List(Of Double)
            Dim splitted() As String
            Dim length1 As Integer = 0
            Dim length2 As Integer = 0
            Dim d As Double
            line = sr.ReadLine()

            Do Until line Is Nothing
                If Not line.StartsWith("#", StringComparison.Ordinal) Then
                    lineList.Clear()
                    splitted = line.Split(numbersep)
                    For Each ds As String In splitted
                        If Double.TryParse(ds, _
                            NumberStyles.Float, _
                            CultureInfo.InvariantCulture, _
                            d) Then
                            lineList.Add(d)
                        End If
                    Next
                    If lineList.Count > length2 Then
                        length2 = lineList.Count
                    End If
                    l.Add(lineList.ToArray)
                    length1 += 1
                End If
                line = sr.ReadLine()
            Loop
            sr.Close()

            Dim result = DirectCast(Array.CreateInstance(GetType(Double), length1, length2), Double(,))

            Dim i, j As Integer
            i = 0
            For Each da As Double() In l
                j = 0
                For Each d In da
                    result(i, j) = d
                    j += 1
                Next
                i += 1
            Next
            Return result
        End Function
    End Class
End Namespace
