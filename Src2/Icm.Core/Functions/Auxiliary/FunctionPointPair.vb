Namespace Icm.Functions

    Public Class FunctionPointPair(Of TX As {Structure, IComparable(Of TX)}, TY As {Structure, IComparable(Of TY)})
        Inherits Tuple(Of FunctionPoint(Of TX, TY), FunctionPoint(Of TX, TY))

        ReadOnly Property X1() As TX
            Get
                Return Item1.X
            End Get
        End Property

        ReadOnly Property Y1() As TY
            Get
                Return Item1.Y
            End Get
        End Property

        ReadOnly Property X2() As TX
            Get
                Return Item2.X
            End Get
        End Property

        ReadOnly Property Y2() As TY
            Get
                Return Item2.Y
            End Get
        End Property

        ReadOnly Property P1() As FunctionPoint(Of TX, TY)
            Get
                Return Item1
            End Get
        End Property

        ReadOnly Property P2() As FunctionPoint(Of TX, TY)
            Get
                Return Item2
            End Get
        End Property

        Public Sub New(ByVal fun As IMathFunction(Of TX, TY))
            MyBase.New( _
            New FunctionPoint(Of TX, TY)(Nothing, fun), _
            New FunctionPoint(Of TX, TY)(Nothing, fun))
        End Sub

        Public Sub New(ByVal fun As IMathFunction(Of TX, TY), ByVal px1 As TX, ByVal px2 As TX)
            MyBase.New( _
            New FunctionPoint(Of TX, TY)(px1, fun), _
            New FunctionPoint(Of TX, TY)(px2, fun))
        End Sub

    End Class

End Namespace
