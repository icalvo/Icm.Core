using System;

namespace Icm.IO
{
	public class MovementEventArgs : EventArgs
	{
	    public MovementEventArgs(string o, string d)
		{
			Origen = o;
			Destino = d;
		}

        public string Origen { get; }
	    public string Destino { get; }
	}
}
