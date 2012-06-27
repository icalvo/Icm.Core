Imports System.Globalization

Namespace Icm.Localization

    ''' <summary>
    '''  Esta clase proporciona los métodos adecuados para gestionar
    '''  cadenas de localización.
    ''' </summary>
    ''' <remarks>
    ''' Es una clase de utilidades, por lo que todos sus miembros son compartidos.
    ''' </remarks>
    ''' <history>
    ''' 	[icalvo]	25/04/2005	Creado
    ''' </history>
    Public Interface ILocalizationRepository

        Default ReadOnly Property Item(ByVal key As String, ByVal ParamArray args() As Object) As String
        ReadOnly Property [Get](ByVal lcid As Integer, ByVal key As String, ByVal ParamArray args() As Object) As String
    End Interface
End Namespace

