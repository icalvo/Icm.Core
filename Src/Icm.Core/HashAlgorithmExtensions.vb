Imports System.IO
Imports System.Security.Cryptography
Imports System.Text
Imports System.Runtime.CompilerServices

Namespace Icm.Security.Criptography

    '''<summary>
    ''' Extensions for HashAlgorithm. StringHash returns a string
    ''' with the same format as Paul Johnston's JavaScript MD5 implementation
    ''' (http://pajhome.org.uk/crypt/md5), so his md5.js can be directly used
    ''' with this class to implement CHAP or other challenge-response protocols.
    ''' </summary>
    Public Module HashAlgorithmExtensions

        <Extension()>
        Function ByteHash(ByVal hashAlgorithm As HashAlgorithm, ByVal s As String) As Byte()
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
        Public Function ByteHash(ByVal hashAlgorithm As HashAlgorithm, ByVal ParamArray data() As Byte) As Byte()
            Dim result As Byte()

            result = hashAlgorithm.ComputeHash(data)
            Return result
        End Function

        <Extension()>
        Public Function ByteHash(ByVal hashAlgorithm As HashAlgorithm, ByVal fs As Stream) As Byte()
            Dim result As Byte()

            result = hashAlgorithm.ComputeHash(fs)
            Return result
        End Function

        <Extension()>
        Public Function StringHash(ByVal hashAlgorithm As HashAlgorithm, ByVal s As String) As String
            Return hashAlgorithm.ByteToString(hashAlgorithm.ByteHash(s))
        End Function

        <Extension()>
        Public Function StringHash(ByVal hashAlgorithm As HashAlgorithm, ByVal ParamArray b As Byte()) As String
            Return hashAlgorithm.ByteToString(hashAlgorithm.ByteHash(b))
        End Function

        <Extension()>
        Public Function StringHash(ByVal hashAlgorithm As HashAlgorithm, ByVal fs As Stream) As String
            Return hashAlgorithm.ByteToString(hashAlgorithm.ByteHash(fs))
        End Function

        <Extension()>
        Public Function Match(ByVal hashAlgorithm As HashAlgorithm, ByVal fs1 As Stream, ByVal fs2 As Stream) As Boolean
            Dim hash1, hash2 As Byte()
            Dim i As Integer
            Dim result As Boolean
            hash1 = hashAlgorithm.ByteHash(fs1)
            hash2 = hashAlgorithm.ByteHash(fs2)
            result = True
            For i = 0 To UBound(hash1)
                If hash1(i) <> hash2(i) Then
                    result = False
                    Exit For
                End If
            Next
            Return result
        End Function

        <Extension()>
        Public Function ByteToString(ByVal hashAlgorithm As HashAlgorithm, ByVal ParamArray hash As Byte()) As String
            Dim result As New StringBuilder
            Dim i As Integer

            For i = 0 To UBound(hash)
                result.Append(hashAlgorithm.ByteToHex(hash(i)))
            Next

            Return result.ToString
        End Function

        <Extension()>
        Private Function ByteToHex(ByVal hashAlgorithm As HashAlgorithm, ByVal b As Byte) As String
            ' Devuelve la cadena hexadecimal correspondiente a
            ' un byte, en minúscula y con longitud 2. P. Ej.
            ' ByteToHex(2) = "02"
            ' ByteToHex(254) = "fe"

            Return New String("0"c, 2 - Hex(b).Length) & Hex(b).ToLower
        End Function

    End Module

End Namespace
