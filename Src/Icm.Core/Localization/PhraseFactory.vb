Imports System.Runtime.CompilerServices

Namespace Icm.Localization

    Public Module PhraseFactory

        Public Function PhrF(key As String, ParamArray args() As Object) As PhraseFormat
            Return New PhraseFormat(key, args)
        End Function

        Public Function PhrAnd(ParamArray args() As Object) As PhraseAnd
            Return New PhraseAnd(args)
        End Function

        <Extension()>
        Public Function TransF(repo As ILocalizationRepository, key As String, ParamArray args() As Object) As String
            Return PhrF(key, args).Translate(repo)
        End Function

        <Extension()>
        Public Function TransAnd(repo As ILocalizationRepository, ParamArray args() As Object) As String
            Return PhrAnd(args).Translate(repo)
        End Function

        Public Function TransF(key As String, ParamArray args() As Object) As String
            Return Icm.Ninject.Instance(Of ILocalizationRepository).TransF(key, args)
        End Function

        Public Function TransAnd(ParamArray args() As Object) As String
            Return Icm.Ninject.Instance(Of ILocalizationRepository).TransAnd(args)
        End Function


    End Module

End Namespace
