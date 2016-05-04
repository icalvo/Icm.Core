using System;
using System.Configuration;

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
		    return ConfigurationManager.GetSection(key);
		}
	}
}