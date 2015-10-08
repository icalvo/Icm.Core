
using Microsoft.VisualBasic;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;

namespace Icm.Configuration
{
	public static class Settings
	{

		/// <summary>
		///     Obtains an object from the app config file.
		/// </summary>
		/// <param name="key">Section or section group.</param>
		/// <returns>Cadena correspondiente a la clave proporcionada.</returns>
		/// <remarks>
		///    Devuelve la propia clave si no se encuentra el valor en el fichero.
		/// </remarks>
		/// <history>
		/// 	[icalvo]	28/02/2006	Created
		/// </history>
		public static object GetCfg(string key)
		{
			try {
				return System.Configuration.ConfigurationManager.GetSection(key);
			} catch (Exception ex) {
				throw;
			}
		}

	}
}

//=======================================================
//Service provided by Telerik (www.telerik.com)
//Conversion powered by NRefactory.
//Twitter: @telerik
//Facebook: facebook.com/telerik
//=======================================================
