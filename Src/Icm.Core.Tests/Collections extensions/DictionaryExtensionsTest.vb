Imports Icm.Collections.Generic

'''<summary>
'''This is a test class for DictionaryExtensions and is intended
'''to contain all DictionaryExtensions Unit Tests
'''</summary>
<TestFixture(), Category("Icm")>
Public Class DictionaryExtensionsTest

    <Test(), Category("Icm")>
    Public Sub ItemOrDefaultTest()

        'Caso 1 EXISTE
        Dim actual As String
        Dim dic As New Dictionary(Of String, String)
        Dim expected As String

        dic.Add("doc", "Doc word")
        actual = dic.ItemOrDefault("doc", "Doc word")
        expected = "Doc word"
        Assert.AreEqual(expected, actual)


        'Caso 2 NO EXISTE
        dic.Add("bmp", "dibujo")
        actual = dic.ItemOrDefault("bms", "No existe")
        expected = "No existe"
        Assert.AreEqual(expected, actual)


    End Sub
End Class