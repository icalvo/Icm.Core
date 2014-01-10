Imports Icm.Collections

Namespace Icm.Localization

    ''' <summary>
    ''' A Phrase encapsulates a hierarchical phrase structure without having to
    ''' translate anything beforehand, using a key and arrays of child values (which can be literals
    ''' or other phrases). This allows to build complex strings without depending on a
    ''' concrete repository of strings, and translating them only where the repository is most 
    ''' appropriately available.
    ''' </summary>
    ''' <remarks>
    ''' Use the PhraseFactory.Phr functions to abbreviate construction of phrases.
    ''' </remarks>
    Public Class PhraseFormat
        Inherits Phrase

        Property Key As String

        Private _arguments As List(Of Object)

        ReadOnly Property Arguments As ICollection(Of Object)
            Get
                Return _arguments
            End Get
        End Property

        Public Sub New(key As String, ParamArray args() As Object)
            Me.Key = key
            If args Is Nothing Then
                _arguments = New List(Of Object)()
            Else
                _arguments = New List(Of Object)(args)
            End If
        End Sub

        Public Sub New(key As String, args As IEnumerable(Of Object))
            Me.Key = key
            If args Is Nothing Then
                _arguments = New List(Of Object)()
            Else
                _arguments = New List(Of Object)(args)
            End If
        End Sub

        Public Overrides Function Translate(ByVal lcid As Integer, locRepo As ILocalizationRepository) As String
            Dim claveConsulta As String
            Dim clave = Key
            Dim args = Arguments.Select(Function(trans) TranslateObject(locRepo, trans)).ToArray
            ' La clave de la BD lleva un sufijo con el número de parámetros
            If args IsNot Nothing AndAlso args.Count > 0 Then
                claveConsulta = String.Format("{0}_{1}", clave, args.Count)
            Else
                claveConsulta = clave
            End If

            Dim translatedString = locRepo.ItemForCulture(lcid, claveConsulta)

            If translatedString Is Nothing Then

                translatedString = String.Format("{0}-{1}", clave, lcid)
                If args IsNot Nothing AndAlso args.Count > 0 Then
                    args.Select(Function(obj) obj.ToString).JoinStr(",")
                    translatedString &= String.Format("({0})", args.Select(Function(obj) String.Format("'{0}'", obj)).JoinStr(","))
                End If
                Return translatedString
            Else
                If args IsNot Nothing AndAlso args.Length > 0 Then
                    Return String.Format(translatedString, args)
                Else
                    Return translatedString
                End If
            End If
        End Function

    End Class

End Namespace
