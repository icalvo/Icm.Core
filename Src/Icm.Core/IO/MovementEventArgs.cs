using System;

namespace Icm.IO
{
	public class MovementEventArgs : EventArgs
	{
		protected string origen_;
		protected string destino_;
		public MovementEventArgs(string o, string d)
		{
			origen_ = o;
			destino_ = d;
		}
		public string Origen {
			get { return origen_; }
		}
		public string Destino {
			get { return destino_; }
		}
	}
}

//=======================================================
//Service provided by Telerik (www.telerik.com)
//Conversion powered by NRefactory.
//Twitter: @telerik
//Facebook: facebook.com/telerik
//=======================================================
