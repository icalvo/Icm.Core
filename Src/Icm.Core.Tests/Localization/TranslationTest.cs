
using Microsoft.VisualBasic;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using Icm.Localization;
using Icm.Collections;

[TestFixture()]
public class TranslationFormatTest
{

	[Test()]
	public void TranslateTest()
	{
		DictionaryFixedCultureLocalizationRepository locrepo = new DictionaryFixedCultureLocalizationRepository {
			{
				"K1_2",
				"El procesador ha devuelto el error '{0}' a fecha de {1:dd/MM/yyyy}."
			},
			{
				"K2_2",
				"Informe Tipo {0}, Clase {1} ha caducado"
			},
			{
				"K3_1",
				"Recursos Humanos #{0}"
			}
		};
		dynamic det = PhrF("K1", PhrF("K2", PhrF("K3", 80), 22), 2/12/2012 12:00:00 AM);
		dynamic actual = det.Translate(locrepo);

		Assert.That(actual, Is.EqualTo("El procesador ha devuelto el error 'Informe Tipo Recursos Humanos #80, Clase 22 ha caducado' a fecha de 12/02/2012."));
	}

}


//=======================================================
//Service provided by Telerik (www.telerik.com)
//Conversion powered by NRefactory.
//Twitter: @telerik
//Facebook: facebook.com/telerik
//=======================================================
