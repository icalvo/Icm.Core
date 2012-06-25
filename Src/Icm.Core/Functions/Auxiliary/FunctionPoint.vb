Namespace Icm.Functions

    Public Class FunctionPoint(Of TX As {Structure, IComparable(Of TX)}, TY As {Structure, IComparable(Of TY)})
        Private x_ As TX
        Private y_ As TY
        Private ReadOnly función_ As IMathFunction(Of TX, TY)

        Property X() As TX
            Get
                Return x_
            End Get
            Set(ByVal value As TX)
                x_ = value
                y_ = MathFunction(x_)
            End Set
        End Property

        ReadOnly Property Y() As TY
            Get
                Return y_
            End Get
        End Property

        ReadOnly Property MathFunction() As IMathFunction(Of TX, TY)
            Get
                Return función_
            End Get
        End Property

        Public Sub New(ByVal px As TX, ByVal f As IMathFunction(Of TX, TY))
            función_ = f
            X = px
        End Sub

    End Class

End Namespace
