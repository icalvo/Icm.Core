
using Microsoft.VisualBasic;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
namespace Icm
{

	public class EventArgs<T> : EventArgs
	{

		private readonly T data_;
		public T Data {
			get { return data_; }
		}

		public EventArgs(T _data)
		{
			data_ = _data;
		}

	}

}

//=======================================================
//Service provided by Telerik (www.telerik.com)
//Conversion powered by NRefactory.
//Twitter: @telerik
//Facebook: facebook.com/telerik
//=======================================================
