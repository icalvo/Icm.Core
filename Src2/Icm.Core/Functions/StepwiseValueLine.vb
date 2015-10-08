Imports Icm.Collections.Generic.StructKeyStructValue

Namespace Icm.Functions


    ''' <summary>
    ''' Stepwise keyed function with Date key and Double value.
    ''' </summary>
    ''' <remarks></remarks>
    Public Class StepwiseValueLine
        Inherits StepwiseKeyedFunction(Of Date, Double)

        Public Sub New(ByVal initialValue As Double, ByVal coll As ISortedCollection(Of Date, Double))
            MyBase.New(initialValue, New DateTotalOrder, New DoubleTotalOrder, coll)
        End Sub

        Public Overrides Function EmptyClone() As IMathFunction(Of Date, Double)
            Return New StepwiseValueLine(V0, KeyStore)
        End Function

    End Class

End Namespace
