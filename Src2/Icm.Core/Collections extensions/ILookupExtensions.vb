Imports System.Runtime.CompilerServices
Imports System.Linq

Namespace Icm
    Public Module ILookupExtensions

        ''' <summary>
        ''' ItemOrDefault implementation for ILookup.
        ''' </summary>
        ''' <typeparam name="K"></typeparam>
        ''' <typeparam name="V"></typeparam>
        ''' <param name="o"></param>
        ''' <param name="key"></param>
        ''' <param name="del"></param>
        ''' <param name="ifdonot"></param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        <Extension()>
        Function ItemOrDefault(Of K, V)(ByVal o As ILookup(Of K, V), ByVal key As K, ByVal del As Converter(Of V, V), ByVal ifdonot As V) As V
            If o.Contains(key) Then
                Return del(CType(o(key), V))
            Else
                Return ifdonot
            End If
        End Function

        ''' <summary>
        ''' ItemOrDefault implementation for ILookup.
        ''' </summary>
        ''' <typeparam name="K"></typeparam>
        ''' <typeparam name="V"></typeparam>
        ''' <param name="o"></param>
        ''' <param name="key"></param>
        ''' <param name="ifdonot"></param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        <Extension()>
        Function ItemOrDefault(Of K, V)(ByVal o As ILookup(Of K, V), ByVal key As K, ByVal ifdonot As V) As V
            If o.Contains(key) Then
                Return CType(o(key), V)
            Else
                Return ifdonot
            End If
        End Function

    End Module
End Namespace
