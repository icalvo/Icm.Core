Imports Icm.Localization
Imports Icm.Collections

<TestFixture>
Public Class TranslationFormatTest

    <Test>
    Public Sub TranslateTest()
        Dim locrepo As New DictionaryFixedCultureLocalizationRepository From {
                                                        {"K1_2", "El procesador ha devuelto el error '{0}' a fecha de {1:dd/MM/yyyy}."},
                                                        {"K2_2", "Informe Tipo {0}, Clase {1} ha caducado"},
                                                        {"K3_1", "Recursos Humanos #{0}"}
                                                      }
        Dim det = PhrF("K1", PhrF("K2", PhrF("K3", 80), 22), #2/12/2012#)
        Dim actual = det.Translate(locrepo)

        Assert.That(actual, [Is].EqualTo("El procesador ha devuelto el error 'Informe Tipo Recursos Humanos #80, Clase 22 ha caducado' a fecha de 12/02/2012."))
    End Sub

End Class

