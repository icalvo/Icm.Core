Imports Icm.Collections.Generic

Public Class DictionaryExtensionsTestCases

    Shared dic As IDictionary(Of String, String) = New Dictionary(Of String, String) From {
        {"doc", "Doc word"},
        {"xls", "Excel"},
        {"pdf", "Adobe PDF"},
        {"", "No extension"}
    }

    Shared converter As Converter(Of String, String) = Function(str) "-- " & str

    Shared ReadOnly Property GetCases As IEnumerable(Of TestCaseData)
        Get
            Dim result As New List(Of TestCaseData)
            result.Add(New TestCaseData(dic, "doc", "DEFAULT").Returns("Doc word"))
            result.Add(New TestCaseData(dic, "", "DEFAULT").Returns("No extension").SetName("EmptyKey"))
            result.Add(New TestCaseData(dic, "ppt", "DEFAULT").Returns("DEFAULT").SetName("KeyNotFound"))
            result.Add(New TestCaseData(dic, "ppt", Nothing).Returns(Nothing).SetName("KeyNotFoundDefaultNull"))
            result.Add(New TestCaseData(dic, Nothing, "DEFAULT").Throws(GetType(ArgumentNullException)))

            Return result
        End Get
    End Property

    Shared ReadOnly Property GetCases_WithConverter As IEnumerable(Of TestCaseData)
        Get
            Dim result As New List(Of TestCaseData)
            result.Add(New TestCaseData(dic, "doc", converter, "DEFAULT").Returns("-- Doc word"))
            result.Add(New TestCaseData(dic, "", converter, "DEFAULT").Returns("-- No extension").SetName("EmptyKey"))
            result.Add(New TestCaseData(dic, "ppt", converter, "DEFAULT").Returns("DEFAULT").SetName("KeyNotFound"))
            result.Add(New TestCaseData(dic, "ppt", converter, Nothing).Returns(Nothing).SetName("KeyNotFoundDefaultNull"))
            result.Add(New TestCaseData(dic, Nothing, converter, "DEFAULT").Throws(GetType(ArgumentNullException)))

            Return result
        End Get
    End Property
End Class

<Category("Icm")>
<TestFixture()>
Public Class DictionaryExtensionsTest

    <TestCaseSource(GetType(DictionaryExtensionsTestCases), "GetCases")>
    Public Function ItemOrDefault(target As IDictionary(Of String, String), key As String, defaultResult As String) As String
        Return target.ItemOrDefault(key, defaultResult)
    End Function

    <TestCaseSource(GetType(DictionaryExtensionsTestCases), "GetCases_WithConverter")>
    Public Function ItemOrDefault_WithConverter(target As IDictionary(Of String, String), key As String, converter As Converter(Of String, String), defaultResult As String) As String
        Return target.ItemOrDefault(key, converter, defaultResult)
    End Function


End Class