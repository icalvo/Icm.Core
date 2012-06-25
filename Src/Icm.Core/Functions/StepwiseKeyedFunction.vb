Imports Icm.Collections.Generic.StructKeyStructValue

Namespace Icm.Functions

    ''' <summary>
    ''' In a stepwise function, non-keyed values are equal to the value of the former key.
    ''' There is always value because the constructor requires the value of TX infimum.
    ''' </summary>
    ''' <remarks></remarks>
    Public Class StepwiseKeyedFunction(Of TX As {Structure, IComparable(Of TX)}, TY As {Structure, IComparable(Of TY)})
        Inherits KeyedMathFunction(Of TX, TY)

        Public Sub New(ByVal initialValue As TY, ByVal otx As ITotalOrder(Of TX), ByVal oty As ITotalOrder(Of TY), ByVal coll As ISortedCollection(Of TX, TY))
            MyBase.New(initialValue, otx, oty, coll)
        End Sub

        Public Overrides Function EmptyClone() As IMathFunction(Of TX, TY)
            Return New StepwiseKeyedFunction(Of TX, TY)(V0, TotalOrderTX, TotalOrderTY, KeyStore)
        End Function

        Default Overrides ReadOnly Property V(ByVal d As TX) As TY
            Get
                Return KeyStore(KeyStore.KeyOrPrev(d).Value)
            End Get
        End Property

        Public Overrides Function MinX(ByVal rangeStart As TX, ByVal rangeEnd As TX, ByVal tumbral As ThresholdType, ByVal cantidad As TY) As TX?
            Throw New NotImplementedException ' UNDONE: Implementar
            ' TODO: Reimplementar
            'Dim result As FunctionPoint(Of TX, TY)

            'result = New FunctionPoint(Of TX, TY)(LstX, Me)

            'If Compare(V(rangeStart), tumbral, cantidad) Then
            '    result.X = rangeStart
            'End If
            'Dim idxfecha As Integer

            'idxfecha = KeyStore.IndexOfNextKey(rangeStart)
            'If idxfecha < KeyStore.Count Then
            '    Dim x As TX
            '    x = KeyStore.Keys(idxfecha)
            '    Do Until x.CompareTo(rangeEnd) >= 0 OrElse idxfecha = KeyStore.Count

            '        If Compare(V(x), tumbral, cantidad) Then
            '            If V(x).CompareTo(result.Y) < 0 Then
            '                result.X = x
            '            End If
            '        End If

            '        idxfecha += 1
            '        x = KeyStore.Keys(idxfecha)
            '    Loop
            'End If

            'Return result.X
        End Function

        Public Overrides Function MinXY(ByVal rangeStart As TX, ByVal rangeEnd As TX, ByVal tumbral As ThresholdType, ByVal cantidad As TY) As FunctionPoint(Of TX, TY)
            Throw New NotImplementedException ' UNDONE: Implementar
            'Dim result As FunctionPoint(Of TX, TY)

            'result = New FunctionPoint(Of TX, TY)(LstX, Me)

            'If Compare(V(rangeStart), tumbral, cantidad) Then
            '    result.X = rangeStart
            'End If
            'Dim idxfecha As Integer

            '' TODO: Falta quedarse con el par (X, umbral) en caso de que lo crucemos y el umbral sea inferior
            'idxfecha = KeyStore.IndexOfNextKey(rangeStart)
            'If idxfecha < KeyStore.Count Then
            '    Dim x As TX
            '    x = KeyStore.Keys(idxfecha)
            '    Do Until x.CompareTo(rangeEnd) >= 0 OrElse idxfecha = KeyStore.Count

            '        If Compare(V(x), tumbral, cantidad) Then
            '            If V(x).CompareTo(result.Y) < 0 Then
            '                result.X = x
            '            End If
            '        End If

            '        idxfecha += 1
            '        x = KeyStore.Keys.ElementAtOrDefault(idxfecha)
            '    Loop
            'End If

            'Return result
        End Function

        Public Overrides Function MinXYu(ByVal rangeStart As TX, ByVal rangeEnd As TX, ByVal tumbral As ThresholdType, ByVal fnUmbral As Func(Of TX, TY)) As FunctionPoint(Of TX, TY)
            Throw New NotImplementedException("FunciónClavesEscalonada.MínXY no implementado aún") ' UNDONE: Implementar
        End Function

        Public Overrides Function MaxXY(ByVal rangeStart As TX, ByVal rangeEnd As TX, ByVal tumbral As ThresholdType, ByVal cantidad As TY) As FunctionPoint(Of TX, TY)
            Throw New NotImplementedException ' UNDONE: Implementar
            'Dim result As FunctionPoint(Of TX, TY)

            'result = New FunctionPoint(Of TX, TY)(LstX, Me)

            'Dim idxfecha As Integer

            'idxfecha = KeyStore.IndexOfKeyOrNext(rangeStart)
            'If idxfecha < KeyStore.Count Then
            '    Dim x As TX
            '    Do Until x.CompareTo(rangeEnd) >= 0 OrElse idxfecha = KeyStore.Count
            '        Dim y As TY
            '        y = KeyStore(x)
            '        If Not Compare(y, tumbral, cantidad) Then
            '            y = cantidad
            '        End If

            '        If y.CompareTo(result.Y) < 0 Then
            '            result.X = x
            '        End If

            '        Select Case tumbral

            '        End Select
            '        idxfecha += 1
            '        x = KeyStore.ElementAtOrDefault(idxfecha).Key
            '    Loop
            'End If

            'Return result
        End Function

        Public Overrides Function MaxXYu(ByVal rangeStart As TX, ByVal rangeEnd As TX, ByVal tumbral As ThresholdType, ByVal fnUmbral As Func(Of TX, TY)) As FunctionPoint(Of TX, TY)
            Throw New NotImplementedException("FunciónClavesEscalonada.MáxXY no implementado aún") ' UNDONE: Implementar
        End Function

        Public Overrides Function FstXY(ByVal rangeStart As TX, ByVal rangeEnd As TX, ByVal tumbral As ThresholdType, ByVal cantidad As TY) As FunctionPoint(Of TX, TY)
            Throw New NotImplementedException ' UNDONE: Implementar
            'Dim xy As New FunctionPoint(Of TX, TY)(rangeStart, Me)

            'xy.X = rangeStart
            'If Compare(xy.Y, tumbral, cantidad) Then
            '    Return xy
            'End If

            'Dim idx As Integer = KeyStore.IndexOfNextKey(rangeStart)
            'Dim encontrado As Boolean = False
            'Do Until idx = KeyStore.Count OrElse xy.X.CompareTo(rangeEnd) > 0
            '    xy.X = KeyStore.Keys(idx)
            '    If Compare(xy.Y, tumbral, cantidad) Then
            '        encontrado = True
            '        Exit Do
            '    End If
            '    idx += 1
            'Loop

            'If Not encontrado Then
            '    xy.X = rangeEnd
            'End If

            'If xy.X.Equals(GstX()) Then
            '    Return Nothing
            'Else
            '    Return xy
            'End If
        End Function


        Public Overrides Function FstXYu(ByVal rangeStart As TX, ByVal rangeEnd As TX, ByVal tumbral As ThresholdType, ByVal fnUmbral As Func(Of TX, TY)) As FunctionPoint(Of TX, TY)
            Throw New NotImplementedException ' UNDONE: Implementar
            'Dim xy As New FunctionPoint(Of TX, TY)(rangeStart, Me)
            'xy.X = rangeStart
            'If Compare(xy.Y, tumbral, fnUmbral(xy.X)) Then
            '    Return xy
            'End If

            'Dim idx As Integer = KeyStore.IndexOfNextKey(rangeStart)
            'Dim encontrado As Boolean = False
            'Do Until idx = KeyStore.Count OrElse xy.X.CompareTo(rangeEnd) > 0
            '    xy.X = KeyStore.Keys(idx)
            '    If Compare(xy.Y, tumbral, fnUmbral(xy.X)) Then
            '        encontrado = True
            '        Exit Do
            '    End If
            '    idx += 1
            'Loop

            'If Not encontrado Then
            '    xy.X = rangeEnd
            'End If

            'If xy.X.Equals(GstX()) Then
            '    Return Nothing
            'Else
            '    Return xy
            'End If
        End Function

        Public Overrides Function LstXY(ByVal rangeStart As TX, ByVal rangeEnd As TX, ByVal tumbral As ThresholdType, ByVal cantidad As TY) As FunctionPoint(Of TX, TY)
            Throw New NotImplementedException("FunciónClavesEscalonada.ÚltXY no implementado aún") ' UNDONE: Implementar
        End Function

        Public Overrides Function LstXYu(ByVal rangeStart As TX, ByVal rangeEnd As TX, ByVal tumbral As ThresholdType, ByVal fnUmbral As Func(Of TX, TY)) As FunctionPoint(Of TX, TY)
            Throw New NotImplementedException("FunciónClavesEscalonada.ÚltXY no implementado aún") ' UNDONE: Implementar
        End Function

    End Class

End Namespace
