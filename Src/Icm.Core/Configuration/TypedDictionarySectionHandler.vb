Namespace Icm.Configuration
    Public Class TypedDictionarySectionHandler
        Implements System.Configuration.IConfigurationSectionHandler

        Public Function Create(ByVal parent As Object, ByVal configContext As Object, ByVal section As System.Xml.XmlNode) As Object Implements System.Configuration.IConfigurationSectionHandler.Create

            Dim ht As New Generic.Dictionary(Of String, TypedValue)
            Dim tv As TypedValue
            Dim t As Type
            For Each child As Xml.XmlNode In section.ChildNodes
                Select Case child.Name
                    Case "add"
                        If child.Attributes("type") Is Nothing Then
                            tv = New TypedValue(GetType(String), child.Attributes("value").Value)
                        Else
                            t = Type.GetType("System." & child.Attributes("type").Value)
                            tv = New TypedValue(t, Convert.ChangeType(child.Attributes("value").Value, Type.GetTypeCode(t)))
                        End If
                        ht.Add(child.Attributes("key").Value, tv)
                    Case "#comment"
                        ' Ignore comments
                    Case Else
                        Throw New Exception("<" & child.Name & ">: Not valid in a hashtable")
                End Select
            Next

            Return ht
        End Function

    End Class
End Namespace
