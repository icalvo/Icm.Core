Imports Icm.Collections.Generic

<Category("Icm")>
<TestFixture()>
Public Class DictionaryExtensionsTest

    Public Class DictionaryExtensionsTestCases

        Shared ReadOnly target As IDictionary(Of String, String) = New Dictionary(Of String, String) From {
            {"doc", "Doc word"},
            {"xls", "Excel"},
            {"pdf", "Adobe PDF"},
            {"", "No extension"}
        }

        Shared converter As Converter(Of String, String) = Function(str) "-- " & str

        Shared ReadOnly Property GetItemOrDefaultCases As IEnumerable(Of TestCaseData)
            Get
                Dim result As New List(Of TestCaseData)
                result.Add(New TestCaseData(target, "doc", "DEFAULT").Returns("Doc word"))
                result.Add(New TestCaseData(target, "", "DEFAULT").Returns("No extension").SetName("EmptyKey"))
                result.Add(New TestCaseData(target, "ppt", "DEFAULT").Returns("DEFAULT").SetName("KeyNotFound"))
                result.Add(New TestCaseData(target, "ppt", Nothing).Returns(Nothing).SetName("KeyNotFoundDefaultNull"))
                result.Add(New TestCaseData(target, Nothing, "DEFAULT").Throws(GetType(ArgumentNullException)))

                Return result
            End Get
        End Property

        Shared ReadOnly Property GetItemOrDefaultCases_WithConverter As IEnumerable(Of TestCaseData)
            Get
                Dim result As New List(Of TestCaseData)
                result.Add(New TestCaseData(target, "doc", converter, "DEFAULT").Returns("-- Doc word"))
                result.Add(New TestCaseData(target, "", converter, "DEFAULT").Returns("-- No extension").SetName("EmptyKey"))
                result.Add(New TestCaseData(target, "ppt", converter, "DEFAULT").Returns("DEFAULT").SetName("KeyNotFound"))
                result.Add(New TestCaseData(target, "ppt", converter, Nothing).Returns(Nothing).SetName("KeyNotFoundDefaultNull"))
                result.Add(New TestCaseData(target, Nothing, converter, "DEFAULT").Throws(GetType(ArgumentNullException)))

                Return result
            End Get
        End Property

        Shared GetMergeCases As Object() = {
            New Object() {
                New Dictionary(Of String, String),
                New Dictionary(Of String, String),
                New Dictionary(Of String, String)
            },
            New Object() {
                New Dictionary(Of String, String) From {{"pdf", "Adobe PDF"}},
                New Dictionary(Of String, String) From {{"doc", "Microsoft Word"}, {"xls", "Microsoft Excel"}},
                New Dictionary(Of String, String) From {{"doc", "Microsoft Word"}, {"pdf", "Adobe PDF"}, {"xls", "Microsoft Excel"}}
            },
            New Object() {
                New Dictionary(Of String, String) From {{"pdf", "Adobe PDF"}, {"doc", "MSWORD"}},
                New Dictionary(Of String, String) From {{"doc", "Microsoft Word"}, {"xls", "Microsoft Excel"}},
                New Dictionary(Of String, String) From {{"doc", "Microsoft Word"}, {"pdf", "Adobe PDF"}, {"xls", "Microsoft Excel"}}
            }
        }

    End Class

    <TestCaseSource(GetType(DictionaryExtensionsTestCases), "GetItemOrDefaultCases")>
    Public Function ItemOrDefault(target As IDictionary(Of String, String), key As String, defaultResult As String) As String
        Return target.ItemOrDefault(key, defaultResult)
    End Function

    <TestCaseSource(GetType(DictionaryExtensionsTestCases), "GetItemOrDefaultCases_WithConverter")>
    Public Function ItemOrDefault_WithConverter(target As IDictionary(Of String, String), key As String, converter As Converter(Of String, String), defaultResult As String) As String
        Return target.ItemOrDefault(key, converter, defaultResult)
    End Function

    <TestCaseSource(GetType(DictionaryExtensionsTestCases), "GetMergeCases")>
    Public Sub Merge(target As IDictionary(Of String, String), other As IDictionary(Of String, String), expectedTarget As IDictionary(Of String, String))
        Dim cloneOther As New Dictionary(Of String, String)(other)

        target.Merge(other)

        Assert.That(other, [Is].EquivalentTo(cloneOther))
        Assert.That(target, [Is].EquivalentTo(expectedTarget))
    End Sub

End Class