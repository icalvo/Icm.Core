Imports System.Runtime.CompilerServices
Imports System.Globalization

Namespace Icm
    Public Module LongExtensions

        ''' <summary>
        ''' Exponent prefix in the International System
        ''' </summary>
        ''' <param name="exponent"></param>
        ''' <returns></returns>
        ''' <remarks>
        ''' 
        ''' </remarks>
        Private Function Long1000ExponentPrefix(ByVal exponent As Integer) As String
            Dim units As String
            Select Case exponent
                Case -8
                    units = "yocto"
                Case -7
                    units = "zepto"
                Case -6
                    units = "atto"
                Case -5
                    units = "femto"
                Case -4
                    units = "pico"
                Case -3
                    units = "nano"
                Case -2
                    units = "micro"
                Case -1
                    units = "mili"
                Case 0
                    units = ""
                Case 1
                    units = "kilo"
                Case 2
                    units = "mega"
                Case 3
                    units = "giga"
                Case 4
                    units = "tera"
                Case 5
                    units = "peta"
                Case 6
                    units = "hexa"
                Case 7
                    units = "zeta"
                Case 8
                    units = "iota"
                Case Else
                    Throw New ArgumentOutOfRangeException("exponent", exponent, "Cannot handle exponents under -8 or over 8 for decimal units")
            End Select
            Return units
        End Function

        Private Function Long1024ExponentPrefix(ByVal exponent As Integer) As String
            Dim units As String
            Select Case exponent
                Case 0
                    units = ""
                Case 1
                    units = "kibi"
                Case 2
                    units = "mebi"
                Case 3
                    units = "gibi"
                Case 4
                    units = "tebi"
                Case 5
                    units = "pebi"
                Case 6
                    units = "hebi"
                Case 7
                    units = "zebi"
                Case 8
                    units = "iobi"
                Case Else
                    Throw New ArgumentOutOfRangeException("exponent", exponent, "Cannot handle exponents under 0 or over 8 for binary units")
            End Select
            Return units
        End Function

        Private Function Short1000ExponentPrefix(ByVal exponent As Integer) As String
            Dim units As String
            Select Case exponent
                Case -8
                    units = "y"
                Case -7
                    units = "z"
                Case -6
                    units = "a"
                Case -5
                    units = "f"
                Case -4
                    units = "p"
                Case -3
                    units = "n"
                Case -2
                    units = "µ"
                Case -1
                    units = "m"
                Case 0
                    units = ""
                Case 1
                    units = "K"
                Case 2
                    units = "M"
                Case 3
                    units = "G"
                Case 4
                    units = "T"
                Case 5
                    units = "P"
                Case 6
                    units = "H"
                Case 7
                    units = "Z"
                Case 8
                    units = "I"
                Case Else
                    Throw New ArgumentOutOfRangeException("exponent", exponent, "Cannot handle exponents under -8 or over 8 for decimal units")
            End Select
            Return units
        End Function

        Private Function Short1024ExponentPrefix(ByVal exponent As Integer) As String
            Dim units As String
            Select Case exponent
                Case 0
                    units = ""
                Case 1
                    units = "Ki"
                Case 2
                    units = "Mi"
                Case 3
                    units = "Gi"
                Case 4
                    units = "Ti"
                Case 5
                    units = "Pi"
                Case 6
                    units = "Hi"
                Case 7
                    units = "Zi"
                Case 8
                    units = "Ii"
                Case Else
                    Throw New ArgumentOutOfRangeException("exponent", exponent, "Cannot handle exponents under 0 or over 8 for binary units")
            End Select
            Return units
        End Function

        ''' <summary>
        '''   Returns a human-readable representation of a byte quantity.
        ''' </summary>
        ''' <param name="bytes"></param>
        ''' <param name="decimalUnits">Does the function use decimal powers (e.g. 1 KB =
        '''  1000 B) or binary powers (1 KiB = 1024 B)? By default it is True (decimal)
        ''' </param>
        ''' <param name="bigUnitNames">Does the function add the abbreviated unit symbol (MB)
        ''' or the complete unit name (megabyte)? By default it is False (abbr.)</param>
        ''' <param name="format">Format string for the number. Use it to limit precision
        ''' or adjust other formatting options. By default, it is "0.00".</param>
        ''' <returns>The </returns>
        ''' <remarks>
        '''     <para>The function will only admit a Long number. This is far outside
        ''' the scope of zetabytes and iotabytes, but hey, you never know...</para>
        ''' </remarks>
        ''' <history>
        ''' 	[icalvo]	14/11/2005	Created
        ''' </history>
        <Extension()>
        Public Function HumanFileSize(
              ByVal bytes As Long,
              ByVal decimalUnits As Boolean,
              ByVal bigUnitNames As Boolean,
              ByVal format As String) As String

            Return HumanUnit(bytes, 0, decimalUnits, bigUnitNames, "B", "byte", "bytes", format)
        End Function


        ''' <summary>
        '''   Returns a human-readable representation of a quantity with units.
        ''' </summary>
        ''' <param name="mantissa"></param>
        ''' <param name="addedExponent">If you want to multiply the mantissa by 1000 (or 1024 if
        ''' decimalUnits is False) powers this quantity.
        ''' It is the way to go for quantities between 0 and 1, using negative exponents.</param>
        ''' <param name="decimalUnits">Does the function use decimal powers (e.g. 1 Km =
        '''  1000 m) or binary powers (1 KiB = 1024 B)? By default it is True (decimal)
        ''' </param>
        ''' <param name="bigUnitNames">Does the function add the abbreviated unit symbol (mm)
        ''' or the complete unit name (millimeter)? By default it is False (abbr.)</param>
        ''' <param name="smallUnitName">Abbreviated unit symbol (e.g. m)</param>
        ''' <param name="bigUnitNameSingular">Unit name, singular (e.g. meter)</param>
        ''' <param name="bigUnitNamePlural">Unit name, singular (e.g. meters)</param>
        ''' <param name="numberFormat">Format string for the number. Use it to limit precision
        ''' or adjust other formatting options. By default, it is "0.00".</param>
        ''' <returns></returns>
        ''' <remarks>
        '''     <para></para>
        ''' </remarks>
        ''' <history>
        ''' 	[icalvo]	14/11/2005	Created
        ''' </history>
        <Extension()>
        Public Function HumanUnit(
              ByVal mantissa As Long,
              addedExponent As Integer,
              ByVal decimalUnits As Boolean,
              ByVal bigUnitNames As Boolean,
              smallUnitName As String,
              bigUnitNameSingular As String,
              bigUnitNamePlural As String,
              ByVal numberFormat As String) As String

            Dim formattedNumber As String
            Dim units As String
            Dim prefix As String

            If mantissa < 0 Then
                Throw New ArgumentOutOfRangeException("quantity", mantissa, "quantity should be a positive number")
            End If

            Dim divisor As Integer
            Dim exponent As Integer

            If decimalUnits Then
                divisor = 1000
            Else
                divisor = 1024
            End If

            If mantissa = 0 Then
                exponent = 0
                formattedNumber = (0).ToString(numberFormat, CultureInfo.CurrentCulture)
            Else
                exponent = CInt(Math.Floor(Math.Log(mantissa) / Math.Log(divisor))) + addedExponent
                formattedNumber = (mantissa / divisor ^ Math.Min(8, exponent)).ToString(numberFormat, CultureInfo.CurrentCulture)
            End If

            If bigUnitNames Then
                If formattedNumber = "1" Then
                    units = bigUnitNameSingular
                Else
                    units = bigUnitNamePlural
                End If
            Else
                units = smallUnitName
            End If

            If bigUnitNames Then
                If decimalUnits Then
                    prefix = Long1000ExponentPrefix(exponent)
                Else
                    prefix = Long1024ExponentPrefix(exponent)
                End If
            Else
                If decimalUnits Then
                    prefix = Short1000ExponentPrefix(exponent)
                Else
                    prefix = Short1024ExponentPrefix(exponent)
                End If
            End If

            Return String.Format("{0} {1}{2}", formattedNumber, prefix, units)
        End Function


        ''' <summary>
        '''   Returns a human-readable representation of a byte quantity.
        ''' </summary>
        ''' <param name="bytes"></param>
        ''' <returns>The </returns>
        ''' <remarks>
        '''     <para>Equals to HumanFileSize(bytes, decimalUnits:=True, bigUnitNames:=False, format:="0.00").</para>
        ''' </remarks>
        ''' <history>
        ''' 	[icalvo]	14/11/2005	Created
        ''' </history>
        <Extension()>
        Public Function HumanFileSize(ByVal bytes As Long) As String
            Return HumanFileSize(bytes, decimalUnits:=True, bigUnitNames:=False, format:="0.00")
        End Function

    End Module
End Namespace
