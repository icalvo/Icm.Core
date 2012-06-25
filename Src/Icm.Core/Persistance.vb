Imports System.Reflection

Namespace Icm.Persistance

    Public NotInheritable Class Repository

        Private Shared singleton_ As Repository
        Private cache_ As New Generic.Dictionary(Of Integer, Persistent)

        Private Sub New()
        End Sub

        Public Shared Function GetSingleton() As Repository
            If singleton_ Is Nothing Then
                singleton_ = New Repository
            End If
            Return singleton_
        End Function

        Public Function GetObject(ByVal pid As Integer) As Persistent
            If cache_.ContainsKey(pid) Then
                Return cache_(pid)
            Else
                Return Nothing
            End If
        End Function
    End Class

    Public MustInherit Class Persistent
        Implements System.Xml.Serialization.IXmlSerializable

        Public Sub New(ByVal id As Integer)
            PID = id
        End Sub

        Public Sub Initialize()
            Repository.GetSingleton()
        End Sub


        Protected PID As Integer

        Public Function GetSchema() As System.Xml.Schema.XmlSchema Implements System.Xml.Serialization.IXmlSerializable.GetSchema
            Return Nothing
        End Function

        Public Sub ReadXml(ByVal reader As System.Xml.XmlReader) Implements System.Xml.Serialization.IXmlSerializable.ReadXml
        End Sub

        Public Sub WriteXml(ByVal writer As System.Xml.XmlWriter) Implements System.Xml.Serialization.IXmlSerializable.WriteXml
            writer.WriteRaw(String.Format("<{0} id=""{1}"">", Me.GetType.Name, PID))
            For Each fi As FieldInfo In Me.GetType().GetFields(BindingFlags.NonPublic Or BindingFlags.Public Or BindingFlags.Instance)
                If (fi.FieldType.IsPrimitive And fi.Name <> "PID") OrElse _
                    fi.FieldType.Name = "String" Then
                    writer.WriteRaw(String.Format("  <{0} type=""{1}"">{2}</{0}>", fi.Name, fi.FieldType.Name, CStr(fi.GetValue(Me))))

                ElseIf fi.FieldType.IsSubclassOf(GetType(Persistent)) Then
                    Dim p As Persistent
                    p = CType(fi.GetValue(Me), Persistent)
                    writer.WriteRaw(String.Format("  <{0} type=""reference"" refid=""{1}"" type=""{2}"" />", fi.Name, p.PID, fi.GetValue(Me).GetType.Name))
                ElseIf fi.FieldType.IsSubclassOf(GetType(ICollection)) Then
                    writer.WriteRaw(String.Format("  <{0} type=""{1}"" />", fi.Name, fi.GetValue(Me).GetType.Name))
                    Dim c As ICollection
                    c = CType(fi.GetValue(Me), System.Collections.ICollection)
                    Dim o As Object
                    For Each o In c
                        If o.GetType.IsSubclassOf(GetType(Persistent)) Then
                            writer.WriteRaw(String.Format("  <reference refid=""{0}"" type=""{1}"" />", CType(o, Persistent).PID, o.GetType.Name))
                        End If
                    Next
                End If
            Next
            writer.WriteRaw(String.Format("</{0}>", Me.GetType.Name))
        End Sub
    End Class

    Public Class Autor
        Inherits Persistent

        Public Sub New(ByVal id As Integer)
            MyBase.New(id)
        End Sub

        Protected nombre_ As String
        Protected añoNacimiento_ As Integer

    End Class

End Namespace
