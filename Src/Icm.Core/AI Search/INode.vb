
Namespace Icm.Collections.Generic

    Public Interface INode
        Property Clave As String
        Property Padre As INode
        Property g As Integer
        Function h() As Integer
        Function ObtenerSucesores() As List(Of INode)
        Property Sucesores As List(Of INode)
        Function EsMeta() As Boolean
        Function Coste(ByVal otro As INode) As Integer
    End Interface
End Namespace