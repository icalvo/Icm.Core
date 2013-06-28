Namespace Icm.Functions
    ''' <summary>
    ''' This class represents a mathematic function with domain at TX and range at TY.
    ''' Either TX and TY have total order, defined by the properties TotalOrderTX and TotalOrderTY.
    ''' </summary>
    ''' <typeparam name="TX"></typeparam>
    ''' <typeparam name="TY"></typeparam>
    ''' <remarks></remarks>
    Public MustInherit Class MathFunction(Of TX As {Structure, IComparable(Of TX)}, TY As {Structure, IComparable(Of TY)})
        Implements IMathFunction(Of TX, TY)

        Protected Sub New(ByVal otx As ITotalOrder(Of TX), ByVal oty As ITotalOrder(Of TY))
            totalOrderTX_ = otx
            totalOrderTY_ = oty
        End Sub

        ''' <summary>
        ''' Clone of the function with the same value at inf TX.
        ''' </summary>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public MustOverride Function EmptyClone() As IMathFunction(Of TX, TY) Implements IMathFunction(Of TX, TY).EmptyClone

#Region " Attributes "

        ''' <summary>
        ''' Nombre de la línea de valores.
        ''' </summary>
        ''' <remarks></remarks>
        Private name_ As String

        Private totalOrderTX_ As ITotalOrder(Of TX)
        Private totalOrderTY_ As ITotalOrder(Of TY)

#End Region


#Region " Abstract "

        ''' <summary>
        '''  Value of function in x
        ''' </summary>
        ''' <param name="x"></param>
        ''' <value></value>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Default MustOverride ReadOnly Property V(ByVal x As TX) As TY Implements IMathFunction(Of TX, TY).V

        Public MustOverride Function AbsMinXY() As FunctionPoint(Of TX, TY) Implements IMathFunction(Of TX, TY).AbsMinXY
        Public MustOverride Function AbsMaxXY() As FunctionPoint(Of TX, TY) Implements IMathFunction(Of TX, TY).AbsMaxXY

        Public MustOverride Function MinXY(ByVal rangeStart As TX, ByVal rangeEnd As TX, ByVal thrType As ThresholdType, ByVal threshold As TY) As FunctionPoint(Of TX, TY) Implements IMathFunction(Of TX, TY).MinXY
        Public MustOverride Function MinXYu(ByVal rangeStart As TX, ByVal rangeEnd As TX, ByVal thrType As ThresholdType, ByVal fnThreshold As Func(Of TX, TY)) As FunctionPoint(Of TX, TY) Implements IMathFunction(Of TX, TY).MinXYu
        Public MustOverride Function MaxXY(ByVal rangeStart As TX, ByVal rangeEnd As TX, ByVal thrType As ThresholdType, ByVal threshold As TY) As FunctionPoint(Of TX, TY) Implements IMathFunction(Of TX, TY).MaxXY
        Public MustOverride Function MaxXYu(ByVal rangeStart As TX, ByVal rangeEnd As TX, ByVal thrType As ThresholdType, ByVal fnThreshold As Func(Of TX, TY)) As FunctionPoint(Of TX, TY) Implements IMathFunction(Of TX, TY).MaxXYu
        Public MustOverride Function FstXY(ByVal rangeStart As TX, ByVal rangeEnd As TX, ByVal thrType As ThresholdType, ByVal threshold As TY) As FunctionPoint(Of TX, TY) Implements IMathFunction(Of TX, TY).FstXY
        Public MustOverride Function FstXYu(ByVal rangeStart As TX, ByVal rangeEnd As TX, ByVal thrType As ThresholdType, ByVal fnThreshold As Func(Of TX, TY)) As FunctionPoint(Of TX, TY) Implements IMathFunction(Of TX, TY).FstXYu
        Public MustOverride Function LstXY(ByVal rangeStart As TX, ByVal rangeEnd As TX, ByVal thrType As ThresholdType, ByVal threshold As TY) As FunctionPoint(Of TX, TY) Implements IMathFunction(Of TX, TY).LstXY
        Public MustOverride Function LstXYu(ByVal rangeStart As TX, ByVal rangeEnd As TX, ByVal thrType As ThresholdType, ByVal fnThreshold As Func(Of TX, TY)) As FunctionPoint(Of TX, TY) Implements IMathFunction(Of TX, TY).LstXYu

#End Region

        Public Function GetRange(ByVal rangeStart As TX, ByVal rangeEnd As TX) As MathFunctionRange(Of TX, TY) Implements IMathFunction(Of TX, TY).GetRange
            Return New MathFunctionRange(Of TX, TY)(Me, rangeStart, rangeEnd)
        End Function

        ''' <summary>
        ''' Max({y IN TY})
        ''' </summary>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Function Gst() As TY Implements IMathFunction(Of TX, TY).Gst
            Return TotalOrderTY.Greatest
        End Function

        ''' <summary>
        ''' Min({y IN TY})
        ''' </summary>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Function Lst() As TY Implements IMathFunction(Of TX, TY).Lst
            Return TotalOrderTY.Least
        End Function

        ''' <summary>
        ''' Max({x IN TX})
        ''' </summary>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Function GstX() As TX Implements IMathFunction(Of TX, TY).GstX
            Return TotalOrderTX.Greatest
        End Function


        ''' <summary>
        ''' Min({x IN TX})
        ''' </summary>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Function LstX() As TX Implements IMathFunction(Of TX, TY).LstX
            Return TotalOrderTX.Least
        End Function

        Public Function MinXY(ByVal rangeStart As TX, ByVal rangeEnd As TX) As FunctionPoint(Of TX, TY) Implements IMathFunction(Of TX, TY).MinXY
            Return MinXY(rangeStart, rangeEnd, ThresholdType.RightOpen, Gst)
        End Function

        Public Function MaxXY(ByVal rangeStart As TX, ByVal rangeEnd As TX) As FunctionPoint(Of TX, TY) Implements IMathFunction(Of TX, TY).MaxXY
            Return MaxXY(rangeStart, rangeEnd, ThresholdType.RightOpen, Gst)
        End Function

        Public Function FstXY(ByVal rangeStart As TX, ByVal rangeEnd As TX) As FunctionPoint(Of TX, TY) Implements IMathFunction(Of TX, TY).FstXY
            Return FstXY(rangeStart, rangeEnd, ThresholdType.RightOpen, Gst)
        End Function

        Public Function LstXY(ByVal rangeStart As TX, ByVal rangeEnd As TX) As FunctionPoint(Of TX, TY) Implements IMathFunction(Of TX, TY).LstXY
            Return LstXY(rangeStart, rangeEnd, ThresholdType.RightOpen, Gst)
        End Function

        ReadOnly Property TotalOrderTX() As ITotalOrder(Of TX) Implements IMathFunction(Of TX, TY).OrdenTotalTX
            Get
                Return totalOrderTX_
            End Get
        End Property

        ReadOnly Property TotalOrderTY() As ITotalOrder(Of TY) Implements IMathFunction(Of TX, TY).OrdenTotalTY
            Get
                Return totalOrderTY_
            End Get
        End Property

        Public Function X2Long(ByVal x As TX) As Long Implements IMathFunction(Of TX, TY).X2Long
            Return TotalOrderTX.T2Long(x)
        End Function

        Public Function Long2X(ByVal d As Long) As TX Implements IMathFunction(Of TX, TY).Long2X
            Return TotalOrderTX.Long2T(d)
        End Function

        Public Function Y2Long(ByVal y As TY) As Long Implements IMathFunction(Of TX, TY).Y2Long
            Return TotalOrderTY.T2Long(y)
        End Function

        Public Function Long2Y(ByVal d As Long) As TY Implements IMathFunction(Of TX, TY).Long2Y
            Return TotalOrderTY.Long2T(d)
        End Function

        ''' <summary>
        ''' Es el valor correspondiente al primer lugar dentro del rango y que cumple el umbral dado.
        ''' </summary>
        ''' <returns></returns>
        ''' <remarks>
        ''' Si tumbral = Superior:
        '''   F(Min({x EN [rangeStart,rangeEnd] / F(x) LEQ umbral))
        ''' Si tumbral = Inferior:
        '''   F(Min({x EN [rangeStart,rangeEnd] / F(x) GEQ umbral))
        ''' </remarks>
        Public Overridable Function Fst(ByVal rangeStart As TX, ByVal rangeEnd As TX, ByVal tumbral As ThresholdType, ByVal cantidad As TY) As TY? Implements IMathFunction(Of TX, TY).Fst
            Dim pnt = FstXY(rangeStart, rangeEnd, tumbral, cantidad)
            If pnt Is Nothing Then
                Return Nothing
            Else
                Return pnt.Y
            End If
        End Function
        ''' <summary>
        ''' Si tumbral = Superior:
        '''   F(Max({x EN [rangeStart,rangeEnd] / F(x) LEQ umbral))
        ''' Si tumbral = Inferior:
        '''   F(Max({x EN [rangeStart,rangeEnd] / F(x) GEQ umbral))
        ''' </summary>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Overridable Function Lst(ByVal rangeStart As TX, ByVal rangeEnd As TX, ByVal tumbral As ThresholdType, ByVal cantidad As TY) As TY Implements IMathFunction(Of TX, TY).Lst
            Dim pnt = LstXY(rangeStart, rangeEnd, tumbral, cantidad)
            If pnt Is Nothing Then
                Return Nothing
            Else
                Return pnt.Y
            End If
        End Function

        ''' <summary>
        ''' Valor mínimo alcanzado en el rango
        ''' </summary>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Overridable Function Min(ByVal rangeStart As TX, ByVal rangeEnd As TX) As TY Implements IMathFunction(Of TX, TY).Min
            Return MinXY(rangeStart, rangeEnd, ThresholdType.RightOpen, Gst).Y
        End Function

        ''' <summary>
        ''' Valor mínimo alcanzado en el rango
        ''' </summary>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Overridable Function Max(ByVal rangeStart As TX, ByVal rangeEnd As TX) As TY Implements IMathFunction(Of TX, TY).Max
            Return MaxXY(rangeStart, rangeEnd, ThresholdType.RightOpen, Gst).Y
        End Function

        Public Overridable Function FstX(ByVal rangeStart As TX, ByVal rangeEnd As TX, ByVal tumbral As ThresholdType, ByVal cantidad As TY) As TX? Implements IMathFunction(Of TX, TY).FstX
            Dim pnt = FstXY(rangeStart, rangeEnd, tumbral, cantidad)
            If pnt Is Nothing Then
                Return Nothing
            Else
                Return pnt.X
            End If
        End Function

        Public Overridable Function FstXu(ByVal rangeStart As TX, ByVal rangeEnd As TX, ByVal tumbral As ThresholdType, ByVal fnUmbral As Func(Of TX, TY)) As TX? Implements IMathFunction(Of TX, TY).FstXu
            Dim pnt = FstXYu(rangeStart, rangeEnd, tumbral, fnUmbral)
            If pnt Is Nothing Then
                Return Nothing
            Else
                Return pnt.X
            End If
        End Function

        Public Overridable Function LstX(ByVal rangeStart As TX, ByVal rangeEnd As TX, ByVal tumbral As ThresholdType, ByVal cantidad As TY) As TX? Implements IMathFunction(Of TX, TY).LstX
            Dim pnt = LstXY(rangeStart, rangeEnd, tumbral, cantidad)
            If pnt Is Nothing Then
                Return Nothing
            Else
                Return pnt.X
            End If
        End Function

        ''' <summary>
        ''' Lugar donde se alcanza el valor máximo en el rango
        ''' </summary>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Overridable Function MaxX(ByVal rangeStart As TX, ByVal rangeEnd As TX, ByVal tumbral As ThresholdType, ByVal cantidad As TY) As TX? Implements IMathFunction(Of TX, TY).MaxX
            Dim pnt = MaxXY(rangeStart, rangeEnd, tumbral, cantidad)
            If pnt Is Nothing Then
                Return Nothing
            Else
                Return pnt.X
            End If
        End Function

        ''' <summary>
        ''' Lugar donde se alcanza el valor mínimo en el rango
        ''' </summary>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Overridable Function MinX(ByVal rangeStart As TX, ByVal rangeEnd As TX, ByVal tumbral As ThresholdType, ByVal cantidad As TY) As TX? Implements IMathFunction(Of TX, TY).MinX
            Dim pnt = MinXY(rangeStart, rangeEnd, tumbral, cantidad)
            If pnt Is Nothing Then
                Return Nothing
            Else
                Return pnt.X
            End If
        End Function

        Public Overridable Function MaxX(ByVal rangeStart As TX, ByVal rangeEnd As TX) As TX? Implements IMathFunction(Of TX, TY).MaxX
            Return MaxX(rangeStart, rangeEnd, ThresholdType.RightOpen, Lst)
        End Function

        Public Overridable Function MinX(ByVal rangeStart As TX, ByVal rangeEnd As TX) As TX? Implements IMathFunction(Of TX, TY).MinX
            Return MinX(rangeStart, rangeEnd, ThresholdType.LeftOpen, Gst)
        End Function

        Public Function V0() As TY Implements IMathFunction(Of TX, TY).V0
            Return V(LstX)
        End Function

        Public Function VInf() As TY Implements IMathFunction(Of TX, TY).VInf
            Return V(GstX)
        End Function


        Protected Function Compare(ByVal y As TY?, ByVal threshold As ThresholdType, ByVal umbral As TY) As Boolean
            If y.HasValue Then
                Select Case threshold
                    Case ThresholdType.LeftOpen
                        Return y.Value.CompareTo(umbral) > 0
                    Case ThresholdType.LeftClosed
                        Return y.Value.CompareTo(umbral) >= 0
                    Case ThresholdType.RightOpen
                        Return y.Value.CompareTo(umbral) < 0
                    Case ThresholdType.RightClosed
                        Return y.Value.CompareTo(umbral) <= 0
                    Case Else
                        Throw New ArgumentException("threshold", "threshold")
                End Select
            Else
                Return False
            End If
        End Function
    End Class

End Namespace
