Imports System.Runtime.CompilerServices
Imports Icm.Collections
Namespace Icm.Collections.Generic
    Public Module IDictionaryExtensions

        ''' <summary>
        ''' Merges two dictionaries.
        ''' </summary>
        ''' <typeparam name="K"></typeparam>
        ''' <typeparam name="V"></typeparam>
        ''' <param name="dic"></param>
        ''' <param name="other"></param>
        ''' <remarks>The first dictionary is modified; when a key exists in both of them, the 
        ''' value of the second dictionary is chosen.</remarks>
        <Extension()>
        Public Sub Merge(Of K, V)(ByVal dic As IDictionary(Of K, V), ByVal other As IDictionary(Of K, V))
            For Each k In other.Keys
                dic(k) = other(k)
            Next
        End Sub

        ''' <summary>
        ''' If the given key exists, returns the corresponding value transformed by the given function.
        ''' Otherwise, returns the given default value.
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
        Function ItemOrDefault(Of K, V)(ByVal o As IDictionary(Of K, V), ByVal key As K, ByVal del As Converter(Of V, V), ByVal ifdonot As V) As V
            If o.ContainsKey(key) Then
                Return del(o(key))
            Else
                Return ifdonot
            End If
        End Function

        ''' <summary>
        ''' If the given key exists, returns the corresponding value. Otherwise, returns the given default value.
        ''' </summary>
        ''' <typeparam name="K"></typeparam>
        ''' <typeparam name="V"></typeparam>
        ''' <param name="o"></param>
        ''' <param name="key"></param>
        ''' <param name="ifdonot"></param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        <Extension()> _
        Function ItemOrDefault(Of K, V)(ByVal o As IDictionary(Of K, V), ByVal key As K, ByVal ifdonot As V) As V
            If o.ContainsKey(key) Then
                Return o(key)
            Else
                Return ifdonot
            End If
        End Function

    End Module
End Namespace
