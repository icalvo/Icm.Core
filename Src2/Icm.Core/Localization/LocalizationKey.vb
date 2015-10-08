Imports System.Globalization

Namespace Icm.Localization
    Public Structure LocalizationKey
        Property Keyword As String
        Property LCID As Integer

        Public Sub New(keyword As String, lcid As Integer)
            Me.Keyword = keyword
            Me.LCID = lcid
        End Sub
    End Structure
End Namespace
