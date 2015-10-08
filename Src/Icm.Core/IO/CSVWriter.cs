
using Microsoft.VisualBasic;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.IO;
using System.Text;

namespace Icm.IO
{

	/// <summary>
	/// Class to write an Excel-type CSV (comma-separated values) file
	/// </summary>
	/// <remarks></remarks>
	public class CSVWriter : IDisposable
	{


		private readonly TextWriter _tw;
		public CSVWriter(TextWriter tw)
		{
			_tw = tw;
		}

		public void AddHeader(params string[] fields)
		{
			foreach (void field_loopVariable in fields) {
				field = field_loopVariable;
				_tw.Write(field + ";");
			}
			_tw.WriteLine();
		}

		public void AddHeaderPart(params string[] fields)
		{
			foreach (void field_loopVariable in fields) {
				field = field_loopVariable;
				_tw.Write("{0};", field);
			}
		}

		public void AddString(string val)
		{
			_tw.Write("\"{0}\";", val.Replace("\"", "\"\""));
		}

		public void AddNumber(int val)
		{
			_tw.Write("{0};", val);
		}

		public void AddNumber(double val)
		{
			_tw.Write("{0};", val);
		}

		public void AddDate(System.DateTime val)
		{
			if (val == System.DateTime.MinValue) {
				AddNull();
			} else {
				_tw.Write("{0:dd/MM/yyyy HH:mm:ss};", val);
			}
		}
		public void AddDate(System.DateTime val, string format)
		{
			if (val == System.DateTime.MinValue) {
				AddNull();
			} else {
				_tw.Write(string.Format(CultureInfo.InvariantCulture, "{{0:{0}}};", format), val);
			}
		}
		public void AddNull()
		{
			_tw.Write(";");
		}

		public void NewLine()
		{
			_tw.WriteLine();
		}
		#region "IDisposable Support"
			// Para detectar llamadas redundantes
		private bool disposedValue;

		// IDisposable
		protected virtual void Dispose(bool disposing)
		{
			if (!this.disposedValue) {
				if (disposing) {
					_tw.Close();
				}

			}
			this.disposedValue = true;
		}

		// Visual Basic agregó este código para implementar correctamente el modelo descartable.
		public void Dispose()
		{
			// No cambie este código. Coloque el código de limpieza en Dispose (ByVal que se dispone como Boolean).
			Dispose(true);
			GC.SuppressFinalize(this);
		}
		#endregion

	}

}

//=======================================================
//Service provided by Telerik (www.telerik.com)
//Conversion powered by NRefactory.
//Twitter: @telerik
//Facebook: facebook.com/telerik
//=======================================================
