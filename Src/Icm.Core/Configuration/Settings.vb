
Namespace Icm.Configuration
    Public Module Settings

        ''' <summary>
        '''     Obtains an object from the app config file.
        ''' </summary>
        ''' <param name="key">Section or section group.</param>
        ''' <returns>Cadena correspondiente a la clave proporcionada.</returns>
        ''' <remarks>
        '''    Devuelve la propia clave si no se encuentra el valor en el fichero.
        ''' </remarks>
        ''' <history>
        ''' 	[icalvo]	28/02/2006	Created
        ''' </history>
        Function GetCfg(ByVal key As String) As Object
            Try
                Return System.Configuration.ConfigurationManager.GetSection(key)
            Catch ex As Exception
                Throw
            End Try
        End Function

    End Module
End Namespace
