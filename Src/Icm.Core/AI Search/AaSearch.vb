Imports System.Runtime.CompilerServices

Namespace Icm.Collections.Generic
    Public Module AaSearch

        <Extension()>
        Function f(ByVal n As INode) As Integer
            Return n.g + n.h
        End Function

        Public Function AaSearch(ByVal raíz As INode) As INode
            Dim ABIERTA As New Dictionary(Of String, INode) From {{raíz.Clave, raíz}}
            Dim CERRADA As New Dictionary(Of String, INode)

            raíz.g = 0

            Do Until ABIERTA.Count = 0
                Dim m = ABIERTA.MinEntity(Function(n) n.Value.f).Value
                If m.EsMeta Then Return m
                Dim sucesores = m.ObtenerSucesores
                For Each np In sucesores
                    np.Padre = m
                    np.g = m.g + m.Coste(np)
                    If ABIERTA.ContainsKey(np.Clave) Then
                        Dim n = ABIERTA(np.Clave)
                        If np.g < n.g Then
                            n.Padre = m
                            n.g = np.g
                        End If
                    ElseIf CERRADA.ContainsKey(np.Clave) Then
                        Dim n = CERRADA(np.Clave)
                        If np.g < n.g Then
                            ' Recorrer en profundidad los descendientes de n

                        End If
                    Else
                        ABIERTA.Add(np.Clave, np)
                        m.Sucesores.Add(np)
                    End If

                Next
            Loop
            Throw New NotImplementedException ' UNDONE: Implementar
        End Function
    End Module
End Namespace
