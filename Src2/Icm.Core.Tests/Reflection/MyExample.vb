Imports Icm.Reflection


Public Class MyExample
    Public myfield As String = "mytest"
    Public mynumber As Integer = 7

    Property MyProp As String = "mytestp"
    Property MyPropNumber As Integer = 11

    Property ArrayProp As String() = {"a", "b"}

    Property IndexedProp(a As Integer) As String
        Get
            Return "<<" & a & ">>"
        End Get
        Set(value As String)

        End Set
    End Property

    Property IndexedProp2(a As Integer, b As String) As String
        Get
            Return "<<" & a & ";" & b & ">>"
        End Get
        Set(value As String)

        End Set
    End Property

    Property DictPropStr As New Dictionary(Of String, String) From {
        {"key1", "value1"},
        {"key2", "value2"}
    }

    Property DictPropInt As New Dictionary(Of Integer, String) From {
        {1234, "value1i"},
        {4567, "value2i"}
    }

    Property DictPropDate As New Dictionary(Of Date, String) From {
        {DateSerial(2012, 1, 2), "value1d"},
        {DateSerial(2013, 4, 7), "value2d"}
    }

End Class
