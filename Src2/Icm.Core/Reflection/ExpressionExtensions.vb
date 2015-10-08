Imports System.Linq.Expressions
Imports System.Runtime.CompilerServices

Namespace Icm.Reflection

    Public Module ExpressionExtensions

        ''' <summary>
        ''' Gets the name of a class member invoked in a expression.
        ''' </summary>
        ''' <typeparam name="T"></typeparam>
        ''' <param name="action"></param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        <Extension()>
        Public Function GetInfo(Of T As Class)(action As Expression(Of Func(Of T, Object))) As String
            Dim expression = GetMemberInfo(action)
            Return expression.Member.Name
        End Function

        Private Function GetMemberInfo(method As Expression) As MemberExpression
            Dim lambda = TryCast(method, LambdaExpression)
            If lambda Is Nothing Then
                Throw New ArgumentNullException("method")
            End If

            Dim memberExpr As MemberExpression = Nothing

            Select Case lambda.Body.NodeType
                Case ExpressionType.Convert
                    memberExpr = TryCast(DirectCast(lambda.Body, UnaryExpression).Operand, MemberExpression)
                Case ExpressionType.MemberAccess
                    memberExpr = TryCast(lambda.Body, MemberExpression)
                Case Else
                    Throw New ArgumentException("The expression is not of type Convert or MemberAccess", "method")
            End Select

            Return memberExpr
        End Function
    End Module

End Namespace
