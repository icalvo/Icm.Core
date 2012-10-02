Imports System.Runtime.CompilerServices

Namespace Icm.Localization
    Public Module TranslationFactory

        Public Function TransF(key As String, ParamArray args() As Object) As TranslationFormat
            Return New TranslationFormat(key, args)
        End Function

        Public Function TransAnd(ParamArray args() As Object) As TranslationAnd
            Return New TranslationAnd(args)
        End Function

    End Module

End Namespace
