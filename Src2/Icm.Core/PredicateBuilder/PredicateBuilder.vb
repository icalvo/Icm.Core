Imports System.Linq.Expressions
Imports System.Runtime.CompilerServices

Namespace Icm
    ''' <summary>
    ''' Enables the efficient, dynamic composition of query predicates.
    ''' </summary>
    ''' <remarks>Pete Montgomery's Universal Predicate Builder. See https://petemontgomery.wordpress.com/2011/02/10/a-universal-predicatebuilder/ for mor information.</remarks>
    Public Module PredicateBuilder

        ''' <summary>
        ''' Creates a predicate that evaluates to true.
        ''' </summary>
        Public Function [True](Of T)() As Expression(Of Func(Of T, Boolean))
            Return Function(param) True
        End Function

        ''' <summary>
        ''' Creates a predicate that evaluates to false.
        ''' </summary>
        Public Function [False](Of T)() As Expression(Of Func(Of T, Boolean))
            Return Function(param) False
        End Function

        ''' <summary>
        ''' Creates a predicate expression from the specified lambda expression.
        ''' </summary>
        Public Function Create(Of T)(predicate As Expression(Of Func(Of T, Boolean))) As Expression(Of Func(Of T, Boolean))
            Return predicate
        End Function

        ''' <summary>
        ''' Combines the first predicate with the second using the logical "and".
        ''' </summary>
        <Extension>
        Public Function [And](Of T)(first As Expression(Of Func(Of T, Boolean)), second As Expression(Of Func(Of T, Boolean))) As Expression(Of Func(Of T, Boolean))
            Return first.Compose(second, AddressOf Expression.AndAlso)
        End Function

        ''' <summary>
        ''' Combines the first predicate with the second using the logical "or".
        ''' </summary>
        <Extension>
        Public Function [Or](Of T)(first As Expression(Of Func(Of T, Boolean)), second As Expression(Of Func(Of T, Boolean))) As Expression(Of Func(Of T, Boolean))
            Return first.Compose(second, AddressOf Expression.OrElse)
        End Function

        ''' <summary>
        ''' Negates the predicate.
        ''' </summary>
        <Extension>
        Public Function [Not](Of T)(expr As Expression(Of Func(Of T, Boolean))) As Expression(Of Func(Of T, Boolean))
            Dim negated = Expression.Not(expr.Body)
            Return Expression.Lambda(Of Func(Of T, Boolean))(negated, expr.Parameters)
        End Function

        ''' <summary>
        ''' Combines the first expression with the second using the specified merge function.
        ''' </summary>
        <Extension>
        Private Function Compose(Of T)(first As Expression(Of T), second As Expression(Of T), merge As Func(Of Expression, Expression, Expression)) As Expression(Of T)
            ' zip parameters (map from parameters of second to parameters of first)
            Dim map = first.Parameters.Select(Function(f, i) New With { _
                f, _
                Key .s = second.Parameters(i) _
            }).ToDictionary(Function(p) p.s, Function(p) p.f)

            ' replace parameters in the second lambda expression with the parameters in the first
            Dim secondBody = ParameterRebinder.ReplaceParameters(map, second.Body)

            ' create a merged lambda expression with parameters from the first expression
            Return Expression.Lambda(Of T)(merge(first.Body, DirectCast(secondBody, Expression)), first.Parameters)
        End Function

        Private Class ParameterRebinder
            Inherits ExpressionVisitor

            Private ReadOnly map As Dictionary(Of ParameterExpression, ParameterExpression)

            Private Sub New(map As Dictionary(Of ParameterExpression, ParameterExpression))
                Me.map = If(map, New Dictionary(Of ParameterExpression, ParameterExpression)())
            End Sub

            Public Shared Function ReplaceParameters(map As Dictionary(Of ParameterExpression, ParameterExpression), exp As Expression) As Expression
                Return New ParameterRebinder(map).Visit(exp)
            End Function

            Protected Overrides Function VisitParameter(p As ParameterExpression) As Expression
                Dim replacement As ParameterExpression = Nothing

                If map.TryGetValue(p, replacement) Then
                    p = replacement
                End If

                Return MyBase.VisitParameter(p)
            End Function
        End Class

    End Module

End Namespace
