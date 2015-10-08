Imports System.Runtime.CompilerServices

Namespace Icm

    ''' <summary>
    ''' Set of currying functions. They follow the pattern:
    ''' CurryX(T1, ..., TN, TResult)(fn As Func(Of T1,...TN, TResult), vx As TX) As Func(Of T1, ...all minus TX..., TN, TResult)
    ''' </summary>
    ''' <remarks></remarks>
    Public Module CurryExtensions

        <Extension()>
        Public Function Curry1(Of T1, TResult)(fn As Func(Of T1, TResult), v1 As T1) As Func(Of TResult)
            Return Function() fn(v1)
        End Function

        <Extension()>
        Public Function Curry1(Of T1, T2, TResult)(fn As Func(Of T1, T2, TResult), v1 As T1) As Func(Of T2, TResult)
            Return Function(p2) fn(v1, p2)
        End Function

        <Extension()>
        Public Function Curry2(Of T1, T2, TResult)(fn As Func(Of T1, T2, TResult), v2 As T2) As Func(Of T1, TResult)
            Return Function(p1) fn(p1, v2)
        End Function

        <Extension()>
        Public Function Curry1(Of T1, T2, T3, TResult)(fn As Func(Of T1, T2, T3, TResult), v1 As T1) As Func(Of T2, T3, TResult)
            Return Function(p2, p3) fn(v1, p2, p3)
        End Function

        <Extension()>
        Public Function Curry2(Of T1, T2, T3, TResult)(fn As Func(Of T1, T2, T3, TResult), v2 As T2) As Func(Of T1, T3, TResult)
            Return Function(p1, p3) fn(p1, v2, p3)
        End Function

        <Extension()>
        Public Function Curry3(Of T1, T2, T3, TResult)(fn As Func(Of T1, T2, T3, TResult), v3 As T3) As Func(Of T1, T2, TResult)
            Return Function(p1, p2) fn(p1, p2, v3)
        End Function

        <Extension()>
        Public Function Curry1(Of T1, T2, T3, T4, TResult)(fn As Func(Of T1, T2, T3, T4, TResult), v1 As T1) As Func(Of T2, T3, T4, TResult)
            Return Function(p2, p3, p4) fn(v1, p2, p3, p4)
        End Function

        <Extension()>
        Public Function Curry2(Of T1, T2, T3, T4, TResult)(fn As Func(Of T1, T2, T3, T4, TResult), v2 As T2) As Func(Of T1, T3, T4, TResult)
            Return Function(p1, p3, p4) fn(p1, v2, p3, p4)
        End Function

        <Extension()>
        Public Function Curry3(Of T1, T2, T3, T4, TResult)(fn As Func(Of T1, T2, T3, T4, TResult), v3 As T3) As Func(Of T1, T2, T4, TResult)
            Return Function(p1, p2, p4) fn(p1, p2, v3, p4)
        End Function

        <Extension()>
        Public Function Curry4(Of T1, T2, T3, T4, TResult)(fn As Func(Of T1, T2, T3, T4, TResult), v4 As T4) As Func(Of T1, T2, T3, TResult)
            Return Function(p1, p2, p3) fn(p1, p2, p3, v4)
        End Function

        <Extension()>
        Public Function Curry1(Of T1, T2, T3, T4, T5, TResult)(fn As Func(Of T1, T2, T3, T4, T5, TResult), v1 As T1) As Func(Of T2, T3, T4, T5, TResult)
            Return Function(p2, p3, p4, p5) fn(v1, p2, p3, p4, p5)
        End Function

        <Extension()>
        Public Function Curry2(Of T1, T2, T3, T4, T5, TResult)(fn As Func(Of T1, T2, T3, T4, T5, TResult), v2 As T2) As Func(Of T1, T3, T4, T5, TResult)
            Return Function(p1, p3, p4, p5) fn(p1, v2, p3, p4, p5)
        End Function

        <Extension()>
        Public Function Curry3(Of T1, T2, T3, T4, T5, TResult)(fn As Func(Of T1, T2, T3, T4, T5, TResult), v3 As T3) As Func(Of T1, T2, T4, T5, TResult)
            Return Function(p1, p2, p4, p5) fn(p1, p2, v3, p4, p5)
        End Function

        <Extension()>
        Public Function Curry4(Of T1, T2, T3, T4, T5, TResult)(fn As Func(Of T1, T2, T3, T4, T5, TResult), v4 As T4) As Func(Of T1, T2, T3, T5, TResult)
            Return Function(p1, p2, p3, p5) fn(p1, p2, p3, v4, p5)
        End Function

        <Extension()>
        Public Function Curry5(Of T1, T2, T3, T4, T5, TResult)(fn As Func(Of T1, T2, T3, T4, T5, TResult), v5 As T5) As Func(Of T1, T2, T3, T4, TResult)
            Return Function(p1, p2, p3, p4) fn(p1, p2, p3, p4, v5)
        End Function

        <Extension()>
        Public Function Curry1(Of T1)(fn As Action(Of T1), v1 As T1) As Action
            Return Sub() fn(v1)
        End Function

        <Extension()>
        Public Function Curry1(Of T1, T2)(fn As Action(Of T1, T2), v1 As T1) As Action(Of T2)
            Return Sub(p2) fn(v1, p2)
        End Function

        <Extension()>
        Public Function Curry2(Of T1, T2)(fn As Action(Of T1, T2), v2 As T2) As Action(Of T1)
            Return Sub(p1) fn(p1, v2)
        End Function

        <Extension()>
        Public Function Curry1(Of T1, T2, T3)(fn As Action(Of T1, T2, T3), v1 As T1) As Action(Of T2, T3)
            Return Sub(p2, p3) fn(v1, p2, p3)
        End Function

        <Extension()>
        Public Function Curry2(Of T1, T2, T3)(fn As Action(Of T1, T2, T3), v2 As T2) As Action(Of T1, T3)
            Return Sub(p1, p3) fn(p1, v2, p3)
        End Function

        <Extension()>
        Public Function Curry3(Of T1, T2, T3)(fn As Action(Of T1, T2, T3), v3 As T3) As Action(Of T1, T2)
            Return Sub(p1, p2) fn(p1, p2, v3)
        End Function

        <Extension()>
        Public Function Curry1(Of T1, T2, T3, T4)(fn As Action(Of T1, T2, T3, T4), v1 As T1) As Action(Of T2, T3, T4)
            Return Sub(p2, p3, p4) fn(v1, p2, p3, p4)
        End Function

        <Extension()>
        Public Function Curry2(Of T1, T2, T3, T4)(fn As Action(Of T1, T2, T3, T4), v2 As T2) As Action(Of T1, T3, T4)
            Return Sub(p1, p3, p4) fn(p1, v2, p3, p4)
        End Function

        <Extension()>
        Public Function Curry3(Of T1, T2, T3, T4)(fn As Action(Of T1, T2, T3, T4), v3 As T3) As Action(Of T1, T2, T4)
            Return Sub(p1, p2, p4) fn(p1, p2, v3, p4)
        End Function

        <Extension()>
        Public Function Curry4(Of T1, T2, T3, T4)(fn As Action(Of T1, T2, T3, T4), v4 As T4) As Action(Of T1, T2, T3)
            Return Sub(p1, p2, p3) fn(p1, p2, p3, v4)
        End Function

        <Extension()>
        Public Function Curry1(Of T1, T2, T3, T4, T5)(fn As Action(Of T1, T2, T3, T4, T5), v1 As T1) As Action(Of T2, T3, T4, T5)
            Return Sub(p2, p3, p4, p5) fn(v1, p2, p3, p4, p5)
        End Function

        <Extension()>
        Public Function Curry2(Of T1, T2, T3, T4, T5)(fn As Action(Of T1, T2, T3, T4, T5), v2 As T2) As Action(Of T1, T3, T4, T5)
            Return Sub(p1, p3, p4, p5) fn(p1, v2, p3, p4, p5)
        End Function

        <Extension()>
        Public Function Curry3(Of T1, T2, T3, T4, T5)(fn As Action(Of T1, T2, T3, T4, T5), v3 As T3) As Action(Of T1, T2, T4, T5)
            Return Sub(p1, p2, p4, p5) fn(p1, p2, v3, p4, p5)
        End Function

        <Extension()>
        Public Function Curry4(Of T1, T2, T3, T4, T5)(fn As Action(Of T1, T2, T3, T4, T5), v4 As T4) As Action(Of T1, T2, T3, T5)
            Return Sub(p1, p2, p3, p5) fn(p1, p2, p3, v4, p5)
        End Function

        <Extension()>
        Public Function Curry5(Of T1, T2, T3, T4, T5)(fn As Action(Of T1, T2, T3, T4, T5), v5 As T5) As Action(Of T1, T2, T3, T4)
            Return Sub(p1, p2, p3, p4) fn(p1, p2, p3, p4, v5)
        End Function

    End Module

End Namespace
