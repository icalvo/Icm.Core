Imports System.IO
Imports System.Security.Cryptography
Imports System.Text
Imports System.Runtime.CompilerServices

Namespace Icm.Security.Criptography

    '''<summary>
    ''' Extensions for HashAlgorithm. StringHash returns a string
    ''' with the same format as Paul Johnston's JavaScript MD5 implementation
    ''' (http://pajhome.org.uk/crypt/md5), so his md5.js can be directly used
    ''' toghether with this class to implement CHAP or other challenge-response protocols.
    ''' </summary>
    Public Module HashAlgorithmExtensions

        <Extension()>
        Function ComputeHash(ByVal hashAlgorithm As HashAlgorithm, ByVal s As String) As Byte()
            Dim bytes(s.Length - 1) As Byte
            Dim c As Char
            Dim i As Integer = 0

            For Each c In s
                bytes(i) = CByte(Asc(c))
                i += 1
            Next
            Return hashAlgorithm.ComputeHash(bytes)
        End Function

        <Extension()>
        Public Function StringHash(ByVal hashAlgorithm As HashAlgorithm, ByVal s As String) As String
            Return hashAlgorithm.ComputeHash(s).ToHex()
        End Function

        <Extension()>
        Public Function StringHash(ByVal hashAlgorithm As HashAlgorithm, ByVal ParamArray b As Byte()) As String
            Return hashAlgorithm.ComputeHash(b).ToHex()
        End Function

        <Extension()>
        Public Function StringHash(ByVal hashAlgorithm As HashAlgorithm, ByVal fs As Stream) As String
            Return hashAlgorithm.ComputeHash(fs).ToHex()
        End Function

        ''' <summary>
        ''' Hexadecimal string corresponding to a byte array.
        ''' </summary>
        ''' <param name="hash"></param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        <Extension()>
        Public Function ToHex(ByVal hash As Byte()) As String
            Dim result As New StringBuilder

            For Each bytenumber In hash
                result.Append(bytenumber.ToHex())
            Next

            Return result.ToString
        End Function

        ''' <summary>
        ''' Hexadecimal string corresponding to a byte, lower case and zero padded.
        ''' </summary>
        ''' <param name="b"></param>
        ''' <returns></returns>
        ''' <remarks>
        ''' For example 2 => "02" and 254 => "fe".
        ''' </remarks>
        <Extension()>
        Private Function ToHex(ByVal b As Byte) As String
            Return New String("0"c, 2 - Hex(b).Length) & Hex(b).ToLower
        End Function

    End Module

End Namespace
