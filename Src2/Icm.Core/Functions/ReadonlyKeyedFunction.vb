Namespace Icm.Functions


    ''' <summary>
    ''' FunciónClavesSóloLectura es un intermediario que permite acceso de sólo lectura a cualquier objeto que herede
    ''' de FunciónClavesBase.
    ''' </summary>
    ''' <typeparam name="TX"></typeparam>
    ''' <typeparam name="TY"></typeparam>
    ''' <remarks>
    ''' <para>Esta función deberá usarse cada vez que queramos manejar un objeto de tipo FunciónClavesBase sólo para
    ''' obtener datos y no para escribir o borrar claves. Para ello, pasaremos ese objeto a su constructor y utilizaremos
    ''' la instancia de FunciónClavesSóloLectura en lugar del original. Como FunciónClavesSóloLectura hereda de
    ''' FunciónClavesBase, podemos emplear todos los métodos públicos de esta última clase.</para>
    ''' <code>
    ''' Dim original As FunciónClaves(Of Date, Double)
    ''' 
    ''' Dim intermediarioSóloLectura As New FunciónClavesSóloLectura(Of Date, Double)(original)
    ''' 
    ''' ' Estas llamadas producen fallos de compilación, pues FunciónClavesSóloLectura no dispone de ellas:
    ''' intermediarioSóloLectura.AñadirClave(#01/02/2008#, 45.2)
    ''' intermediarioSóloLectura.EliminarClave(#01/02/2008#)
    ''' </code>
    ''' </remarks>
    Public NotInheritable Class ReadonlyKeyedFunction(Of TX As {Structure, IComparable(Of TX)}, TY As {Structure, IComparable(Of TY)})
        Inherits BaseKeyedMathFunction(Of TX, TY)

        Private fc_ As IKeyedMathFunction(Of TX, TY)

        Public Sub New(ByVal fc As IKeyedMathFunction(Of TX, TY))
            MyBase.New(fc)
            fc_ = fc
        End Sub


        Default Public Overrides ReadOnly Property V(ByVal d As TX) As TY
            Get
                Return fc_.V(d)
            End Get
        End Property

        Public Overrides Function EmptyClone() As IMathFunction(Of TX, TY)
            Return fc_.EmptyClone
        End Function

        Public Overloads Overrides Function MaxXY(ByVal rangeStart As TX, ByVal rangeEnd As TX, ByVal tumbral As ThresholdType, ByVal cantidad As TY) As FunctionPoint(Of TX, TY)
            Return fc_.MaxXY(rangeStart, rangeEnd, tumbral, cantidad)
        End Function

        Public Overrides Function MaxXYu(ByVal rangeStart As TX, ByVal rangeEnd As TX, ByVal tumbral As ThresholdType, ByVal fnUmbral As Func(Of TX, TY)) As FunctionPoint(Of TX, TY)
            Return fc_.MaxXYu(rangeStart, rangeEnd, tumbral, fnUmbral)
        End Function

        Public Overloads Overrides Function MinXY(ByVal rangeStart As TX, ByVal rangeEnd As TX, ByVal tumbral As ThresholdType, ByVal cantidad As TY) As FunctionPoint(Of TX, TY)
            Return fc_.MinXY(rangeStart, rangeEnd, tumbral, cantidad)
        End Function

        Public Overrides Function MinXYu(ByVal rangeStart As TX, ByVal rangeEnd As TX, ByVal tumbral As ThresholdType, ByVal fnUmbral As Func(Of TX, TY)) As FunctionPoint(Of TX, TY)
            Return fc_.MinXYu(rangeStart, rangeEnd, tumbral, fnUmbral)
        End Function

        Public Overloads Overrides Function FstXYu(ByVal rangeStart As TX, ByVal rangeEnd As TX, ByVal tumbral As ThresholdType, ByVal fnUmbral As System.Func(Of TX, TY)) As FunctionPoint(Of TX, TY)
            Return fc_.FstXYu(rangeStart, rangeEnd, tumbral, fnUmbral)
        End Function

        Public Overloads Overrides Function FstXY(ByVal rangeStart As TX, ByVal rangeEnd As TX, ByVal tumbral As ThresholdType, ByVal cantidad As TY) As FunctionPoint(Of TX, TY)
            Return fc_.FstXY(rangeStart, rangeEnd, tumbral, cantidad)
        End Function

        Public Overloads Overrides Function LstXY(ByVal rangeStart As TX, ByVal rangeEnd As TX, ByVal tumbral As ThresholdType, ByVal cantidad As TY) As FunctionPoint(Of TX, TY)
            Return fc_.LstXY(rangeStart, rangeEnd, tumbral, cantidad)
        End Function

        Public Overrides Function LstXYu(ByVal rangeStart As TX, ByVal rangeEnd As TX, ByVal tumbral As ThresholdType, ByVal fnUmbral As Func(Of TX, TY)) As FunctionPoint(Of TX, TY)
            Return fc_.LstXYu(rangeStart, rangeEnd, tumbral, fnUmbral)
        End Function

        Public Overrides Function AbsMaxXY() As FunctionPoint(Of TX, TY)
            Return fc_.AbsMaxXY
        End Function

        Public Overrides Function AbsMinXY() As FunctionPoint(Of TX, TY)
            Return fc_.AbsMinXY
        End Function
    End Class

End Namespace
