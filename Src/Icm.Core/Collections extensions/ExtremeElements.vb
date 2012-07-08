Namespace Icm.Collections

    ''' <summary>
    ''' List of elements that reach an extreme value, and the extreme value itself.
    ''' </summary>
    ''' <typeparam name="TElement"></typeparam>
    ''' <typeparam name="TExtreme"></typeparam>
    ''' <remarks></remarks>
    Public Class ExtremeElements(Of TElement, TExtreme)
        Property List As IEnumerable(Of TElement)
        Property Extreme As TExtreme

        ''' <summary>
        ''' Initializes a new instance of the ExtremeElements class.
        ''' </summary>
        Public Sub New(ByVal list As IEnumerable(Of TElement), ByVal extreme As TExtreme)
            Me.List = list
            Me.Extreme = extreme
        End Sub
    End Class

End Namespace
