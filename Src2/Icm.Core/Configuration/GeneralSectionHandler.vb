
Namespace Icm.Configuration
    Public Class GeneralSectionHandler
        Implements System.Configuration.IConfigurationSectionHandler

        Public Function Create(ByVal parent As Object, ByVal configContext As Object, ByVal section As System.Xml.XmlNode) As Object Implements System.Configuration.IConfigurationSectionHandler.Create
            Return ManageSection(section)
        End Function

        ''' <summary>
        '''   Manages a section.
        ''' </summary>
        ''' <param name="section"></param>
        ''' <returns></returns>
        ''' <remarks>
        '''   Depending on the section type it treats it as an array or as a table.
        ''' </remarks>
        ''' <history>
        ''' 	[icalvo]	09/03/2006	Created
        ''' </history>
        Function ManageSection(ByVal section As System.Xml.XmlNode) As Object
            Select Case section.Attributes("type").Value
                Case "array"
                    Return BuildArray(section)
                Case "hash"
                    Return BuildHash(section)
                Case Else
                    Throw New InvalidOperationException(section.Attributes("type").Value & ": Not valid element type")
            End Select
        End Function

        Function BuildArray(ByVal section As System.Xml.XmlNode) As IList(Of Object)
            Dim lo As New Generic.List(Of Object)

            For Each child As Xml.XmlNode In section.ChildNodes
                Select Case child.Name
                    Case "add"
                        If child.Attributes("type") Is Nothing Then
                            lo.Add(child.Attributes("value").Value)
                        Else
                            lo.Add(ManageSection(child))
                        End If
                    Case "#comment"
                        ' Ignore comments
                    Case Else
                        Throw New InvalidOperationException("<" & child.Name & ">: Not valid in an array")
                End Select
            Next

            Return lo
        End Function

        Function BuildHash(ByVal section As System.Xml.XmlNode) As IDictionary(Of String, Object)
            Dim ht As New Generic.Dictionary(Of String, Object)
            For Each child As Xml.XmlNode In section.ChildNodes
                Select Case child.Name
                    Case "add"
                        If child.Attributes("type") Is Nothing Then
                            ht.Add(child.Attributes("key").Value, child.Attributes("value").Value)
                        Else
                            ht.Add(child.Attributes("key").Value, ManageSection(child))
                        End If
                    Case "remove"
                        ht.Remove(child.Attributes("value").Value)
                    Case "#comment"
                        ' Ignore comments
                    Case Else
                        Throw New InvalidOperationException("<" & child.Name & ">: Not valid in a hashtable")
                End Select
            Next

            Return ht
        End Function

    End Class
End Namespace
