Imports Icm.Collections.Generic.StructKeyStructValue

Namespace Icm.Functions

    ''' <summary>
    ''' Functions by key that allow modification of key points (add and delete)
    ''' </summary>
    ''' <typeparam name="TX">Domain</typeparam>
    ''' <typeparam name="TY">Image</typeparam>
    ''' <remarks></remarks>
    Public MustInherit Class KeyedMathFunction(Of TX As {Structure, IComparable(Of TX)}, TY As {Structure, IComparable(Of TY)})
        Inherits BaseKeyedMathFunction(Of TX, TY)

        Protected Sub New(ByVal initialValue As TY, ByVal otx As ITotalOrder(Of TX), ByVal oty As ITotalOrder(Of TY), ByVal coll As ISortedCollection(Of TX, TY))
            MyBase.New(otx, oty, coll)
            KeyStore.Add(LstX, initialValue)
        End Sub

        Public Sub Forzar(ByVal d As TX, ByVal v As TY)
            KeyStore(d) = v
        End Sub

        ' This introduces one interpolated function point as a key.
        Public Sub Forzar(ByVal d As TX)
            KeyStore(d) = V(d)
        End Sub

    End Class

End Namespace
