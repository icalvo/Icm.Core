Imports Icm.Collections.Generic.StructKeyStructValue
Namespace Icm.Functions

    ''' <summary>
    ''' In a stepwise function, non-keyed values are interpolated from the previous and
    ''' next key points.
    ''' Values between infimum and first key are equal to the first key value.
    ''' Values between last key and supreme are equal to the last key value.
    ''' </summary>
    ''' <remarks></remarks>
    Public Class InterpolatedKeyedFunction(Of TX As {Structure, IComparable(Of TX)}, TY As {Structure, IComparable(Of TY)})
        Inherits KeyedMathFunction(Of TX, TY)

        Public Sub New(ByVal initialValue As TY, ByVal otx As ITotalOrder(Of TX), ByVal oty As ITotalOrder(Of TY), ByVal coll As ISortedCollection(Of TX, TY))
            MyBase.New(initialValue, otx, oty, coll)
        End Sub

        Public Overrides Function EmptyClone() As IMathFunction(Of TX, TY)
            Return New InterpolatedKeyedFunction(Of TX, TY)(V0, TotalOrderTX, TotalOrderTY, KeyStore)
        End Function

        Default Overrides ReadOnly Property V(ByVal d As TX) As TY
            Get
                If KeyStore.ContainsKey(d) Then
                    Return KeyStore(d)
                End If

                Dim x1, fx1, x2, x3, fx3 As Long

                Dim baseX As TX
                Dim baseY As TY
                baseX = KeyStore.KeyOrPrev(d).Value
                baseY = KeyStore(KeyStore.KeyOrPrev(d).Value)

                x1 = X2Long(baseX)
                fx1 = Y2Long(baseY)

                x2 = X2Long(d)

                Dim nextKey = KeyStore.KeyOrNext(d)
                If Not nextKey.HasValue Then
                    Return Long2Y(fx1)
                Else
                    x3 = X2Long(nextKey.Value)
                    fx3 = Y2Long(KeyStore(nextKey.Value))

                    Return Long2Y(Icm.MathTools.LinearInterpolate(x1, x2, x3, fx1, fx3))
                End If
            End Get
        End Property

        Public Overrides Function MinXY(ByVal rangeStart As TX, ByVal rangeEnd As TX, ByVal tumbral As ThresholdType, ByVal cantidad As TY) As FunctionPoint(Of TX, TY)
            Dim it As New RangeIterator(Of TX, TY)(Me, rangeStart, rangeEnd, includeExtremes:=True)
            Dim result As FunctionPoint(Of TX, TY) = Nothing
            For Each pp In it
                Dim cumpleY1 As Boolean = Compare(pp.Y1, tumbral, cantidad)
                Dim cumpleY2 As Boolean = Compare(pp.Y2, tumbral, cantidad)
                Dim posibleMín As FunctionPoint(Of TX, TY) = Nothing
                If cumpleY1 AndAlso cumpleY2 Then
                    ' Ambos extremos están dentro del umbral. El posible mínimo es el que tenga menor Y.
                    If pp.Y1.CompareTo(pp.Y2) <= 0 Then
                        posibleMín = pp.P1
                    Else
                        posibleMín = pp.P2
                    End If
                ElseIf tumbral = ThresholdType.RightClosed OrElse tumbral = ThresholdType.RightOpen Then
                    If cumpleY1 Then
                        ' El posible mínimo es P1.
                        posibleMín = pp.P1
                    ElseIf cumpleY2 Then
                        ' El posible mínimo es P2.
                        posibleMín = pp.P2
                    End If
                Else
                    If cumpleY1 OrElse cumpleY2 Then
                        ' El posible mínimo es el cruce con el umbral.
                        posibleMín = InvInterpolate(pp, cantidad)
                    End If
                End If

                ' Si tenemos un posible mínimo, lo comparamos con nuestro mínimo actual
                If posibleMín.IsSomething AndAlso (result Is Nothing OrElse posibleMín.Y.CompareTo(result.Y) < 0) Then
                    result = posibleMín
                End If
            Next

            If result.IsSomething Then
                Debug.Assert(Compare(result.Y, tumbral, cantidad))
            End If

            Return result
        End Function

        Public Overrides Function MinXYu(ByVal rangeStart As TX, ByVal rangeEnd As TX, ByVal tumbral As ThresholdType, ByVal fnUmbral As Func(Of TX, TY)) As FunctionPoint(Of TX, TY)
            Throw New NotImplementedException("FunciónClavesInterpolada.MínXY no implementado aún") ' UNDONE: Implementar
        End Function

        Private Function InvInterpolate(ByVal pp As FunctionPointPair(Of TX, TY), ByVal cantidad As TY) As FunctionPoint(Of TX, TY)
            Dim xy As New FunctionPoint(Of TX, TY)(GstX, Me)
            Dim x1, x3, fx1, fx2, fx3 As Double
            Dim baseX As TX
            Dim baseY As TY

            baseX = pp.X1
            baseY = pp.Y1
            x1 = X2Long(pp.X1)
            x3 = X2Long(pp.X2)
            fx1 = Y2Long(pp.Y1)
            fx2 = Y2Long(cantidad)
            fx3 = Y2Long(pp.Y2)
            xy.X = Long2X(CLng(Icm.MathTools.InverseLinearInterpolate(x1, x3, fx1, fx2, fx3)))

            Return xy
        End Function

        Public Overrides Function MaxXY(ByVal rangeStart As TX, ByVal rangeEnd As TX, ByVal tumbral As ThresholdType, ByVal cantidad As TY) As FunctionPoint(Of TX, TY)
            Dim it As New RangeIterator(Of TX, TY)(Me, rangeStart, rangeEnd, includeExtremes:=True)
            Dim result As FunctionPoint(Of TX, TY) = Nothing
            Dim pp As FunctionPointPair(Of TX, TY) = Nothing
            For Each pp In it
                Dim cumpleY1 As Boolean = Compare(pp.Y1, tumbral, cantidad)
                Dim cumpleY2 As Boolean = Compare(pp.Y2, tumbral, cantidad)
                Dim posibleMáx As FunctionPoint(Of TX, TY) = Nothing
                If cumpleY1 AndAlso cumpleY2 Then
                    ' Ambos extremos están dentro del umbral. El posible máximo es el que tenga mayor Y.
                    If pp.Y1.CompareTo(pp.Y2) >= 0 Then
                        posibleMáx = pp.P1
                    Else
                        posibleMáx = pp.P2
                    End If
                ElseIf tumbral = ThresholdType.LeftOpen OrElse tumbral = ThresholdType.LeftClosed Then
                    If cumpleY1 Then
                        ' El posible máximo es P1.
                        posibleMáx = pp.P1
                    ElseIf cumpleY2 Then
                        ' El posible máximo es P2.
                        posibleMáx = pp.P2
                    End If
                Else
                    If cumpleY1 OrElse cumpleY2 Then
                        ' El posible máximo es el cruce con el umbral.
                        posibleMáx = InvInterpolate(pp, cantidad)
                    End If
                End If

                ' Si tenemos un posible máximo, lo comparamos con nuestro máximo actual
                If posibleMáx.IsSomething AndAlso (result Is Nothing OrElse posibleMáx.Y.CompareTo(result.Y) > 0) Then
                    result = posibleMáx
                End If
            Next

            If result.IsSomething Then
                Do Until Compare(result.Y, tumbral, cantidad) OrElse result.X.CompareTo(pp.X2) >= 0
                    result.X = TotalOrderTX.Next(result.X)
                Loop
                If result.X.CompareTo(pp.X2) > 0 Then
                    result.X = pp.X2
                End If
                Debug.Assert(Compare(result.Y, tumbral, cantidad))
            End If

            Return result
        End Function

        Public Overrides Function MaxXYu(ByVal rangeStart As TX, ByVal rangeEnd As TX, ByVal tumbral As ThresholdType, ByVal fnUmbral As Func(Of TX, TY)) As FunctionPoint(Of TX, TY)
            Throw New NotImplementedException("FunciónClavesInterpolada.MáxXY no implementado aún") ' UNDONE: Implementar
        End Function

        Public Overrides Function FstXY(ByVal rangeStart As TX, ByVal rangeEnd As TX, ByVal tumbral As ThresholdType, ByVal cantidad As TY) As FunctionPoint(Of TX, TY)
            Dim result As FunctionPoint(Of TX, TY) = Nothing

            ' Chequeamos antes con el máximo y el mínimo absolutos
            If Compare(AbsMaxXY.Y, tumbral, cantidad) AndAlso Compare(AbsMinXY.Y, tumbral, cantidad) Then
                result = New FunctionPoint(Of TX, TY)(rangeStart, Me)
            ElseIf Not Compare(AbsMaxXY.Y, tumbral, cantidad) AndAlso Not Compare(AbsMinXY.Y, tumbral, cantidad) Then
                result = Nothing
            Else
                Dim it As New RangeIterator(Of TX, TY)(Me, rangeStart, rangeEnd, includeExtremes:=True)
                Dim pp As FunctionPointPair(Of TX, TY) = Nothing
                For Each pp In it
                    If Compare(pp.Y1, tumbral, cantidad) Then
                        result = pp.P1
                        Exit For
                    ElseIf Compare(pp.Y2, tumbral, cantidad) Then
                        result = InvInterpolate(pp, cantidad)
                        Exit For
                    End If
                Next
                If result.IsSomething Then
                    Do Until Compare(result.Y, tumbral, cantidad) OrElse result.X.CompareTo(pp.X2) >= 0
                        result.X = TotalOrderTX.Next(result.X)
                    Loop
                    If result.X.CompareTo(pp.X2) > 0 Then
                        result.X = pp.X2
                    End If
                    Debug.Assert(Compare(result.Y, tumbral, cantidad))
                End If
            End If
            Debug.Assert(result Is Nothing OrElse Not result.X.Equals(GstX()))
            Return result
        End Function

        ''' <summary>
        ''' 
        ''' </summary>
        ''' <param name="rangeStart"></param>
        ''' <param name="rangeEnd"></param>
        ''' <param name="tumbral"></param>
        ''' <param name="fnUmbral"></param>
        ''' <returns></returns>
        ''' <remarks>La implementación actual no es del todo exacta puesto que sólo calcula el umbral en los
        ''' extremos de la línea temporal.</remarks>
        Public Overrides Function FstXYu(ByVal rangeStart As TX, ByVal rangeEnd As TX, ByVal tumbral As ThresholdType, ByVal fnUmbral As Func(Of TX, TY)) As FunctionPoint(Of TX, TY)
            Dim it As New RangeIterator(Of TX, TY)(Me, rangeStart, rangeEnd, includeExtremes:=True)
            Dim result As FunctionPoint(Of TX, TY) = Nothing

            For Each pp In it
                Dim U1, U2 As TY
                U1 = fnUmbral(pp.X1)
                U2 = fnUmbral(pp.X2)
                If Compare(pp.Y1, tumbral, U1) Then
                    result = pp.P1
                    Exit For
                ElseIf Compare(pp.Y2, tumbral, U2) Then
                    ' Hace falta

                    Dim baseX As TX
                    Dim baseY As TY
                    baseX = pp.X1
                    baseY = pp.Y1

                    Dim cruceEnX As TX

                    cruceEnX = Long2X(CruceX( _
                        X2Long(pp.X1), _
                        X2Long(pp.X2), _
                        Y2Long(pp.Y1), _
                        Y2Long(pp.Y2), _
                        Y2Long(U1), _
                        Y2Long(U2)))
                    result = New FunctionPoint(Of TX, TY)(cruceEnX, Me)
                    Exit For
                End If
            Next
            If result.IsSomething Then
                'Do Until Comparar(V(result.X), tumbral, fnUmbral(result.X))
                'result.X = OrdenTotalTX.AddDelta(result.X)
                'Loop

                'Debug.Assert(Comparar(result.Y, tumbral, fnUmbral(result.X)))
            End If
            Debug.Assert(result Is Nothing OrElse Not result.X.Equals(GstX()))
            Return result
        End Function


        ''' <summary>
        '''  ¿Se cruzan los segmentos A y B definidos por sus coordenadas?
        ''' </summary>
        ''' <param name="x1">Coordenada X del primer punto de ambos segmentos</param>
        ''' <param name="x2">Coordenada X del segundo punto de ambos segmentos</param>
        ''' <param name="y1A">Coordenada Y del primer punto del segmento A</param>
        ''' <param name="y2A">Coordenada Y del segundo punto del segmento A</param>
        ''' <param name="y1B">Coordenada Y del primer punto del segmento B</param>
        ''' <param name="y2B">Coordenada Y del segundo punto del segmento B</param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Private Shared Function CruceX( _
            ByVal x1 As Long, _
            ByVal x2 As Long, _
            ByVal y1A As Long, _
            ByVal y2A As Long, _
            ByVal y1B As Long, _
            ByVal y2B As Long) _
        As Long

            Dim result As Long

            Dim difY1, difY2, difX As Long


            difY1 = Math.Abs(y1A - y1B)
            difY2 = Math.Abs(y2A - y2B)
            difX = Math.Abs(x1 - x2)
            result = difY1 * difX \ (difY1 + difY2)

            Return result

        End Function

        Public Overrides Function LstXY(ByVal rangeStart As TX, ByVal rangeEnd As TX, ByVal tumbral As ThresholdType, ByVal cantidad As TY) As FunctionPoint(Of TX, TY)
            'Calculo los puntos anterior y posterior al rangeStart que haya en la lista
            Dim key1, key2 As TX?
            Dim existeDesde As Boolean = True
            Dim existeHasta As Boolean = True

            If Not KeyStore.ContainsKey(rangeStart) Then
                existeDesde = False
                KeyStore.Add(rangeStart, V(rangeStart))
            End If

            If Not KeyStore.ContainsKey(rangeEnd) Then
                existeHasta = False
                KeyStore.Add(rangeEnd, V(rangeEnd))
            End If
            key1 = rangeEnd
            key2 = KeyStore.Previous(rangeEnd).Value

            Dim xy As New FunctionPoint(Of TX, TY)(rangeStart, Me)

            'Si el valor f(rangeStart) está en el umbral ya he terminado porque ese será la x más pequeña que cumpla la condición
            If Compare(KeyStore(key1.Value), tumbral, cantidad) Then

                xy.X = rangeStart
                If Not existeDesde Then
                    KeyStore.Remove(rangeStart)
                End If
                If Not existeHasta Then
                    KeyStore.Remove(rangeEnd)
                End If
                Return xy
            End If
            'Si no es así tomo el punto x=rangeStart y=f(rangeStart) como punto inicial para la interpolación

            Do Until (Not key2.HasValue OrElse key2.Value.CompareTo(rangeEnd) = 0)
                'Ya sé que mi primer punto no está en el umbral, compruebo si el segundo lo está
                'Si lo está, calculo la x del punto en que la función corta el umbral
                If Compare(KeyStore(key2.Value), tumbral, cantidad) Then
                    xy = InvInterpolate(New FunctionPointPair(Of TX, TY)(Me, key1.Value, key2.Value), cantidad)
                    If Not existeDesde Then
                        KeyStore.Remove(rangeStart)
                    End If
                    If Not existeHasta Then
                        KeyStore.Remove(rangeEnd)
                    End If
                    Return xy
                Else
                    'Si el segundo punto no está en al umbral, paso al siguiente
                    key1 = KeyStore.Next(key1.Value)
                    key2 = KeyStore.Next(key2.Value)
                End If
            Loop

            'Me ha salido del rango y no he encontrado un punto que esté en el umbral
            'El primer punto está en el rango, el segundo no
            'Calculo el f(rangeEnd), si está en el rango calculo la x del punto en que la función corta el umbral
            'si no está no hay primero

            If Compare(KeyStore(key2.Value), tumbral, cantidad) Then
                xy = InvInterpolate(New FunctionPointPair(Of TX, TY)(Me, key1.Value, key2.Value), cantidad)
            End If
            If Not existeDesde Then
                KeyStore.Remove(rangeStart)
            End If
            If Not existeHasta Then
                KeyStore.Remove(rangeEnd)
            End If
            Return xy
            Throw New NotImplementedException("FunciónClavesEscalonada.ÚltXY no implementado aún") ' UNDONE: Implementar
        End Function

        Public Overrides Function LstXYu(ByVal rangeStart As TX, ByVal rangeEnd As TX, ByVal tumbral As ThresholdType, ByVal fnUmbral As Func(Of TX, TY)) As FunctionPoint(Of TX, TY)
            Throw New NotImplementedException("FunciónClavesInterpolada.ÚltXY no implementado aún") ' UNDONE: Implementar
        End Function

    End Class

End Namespace