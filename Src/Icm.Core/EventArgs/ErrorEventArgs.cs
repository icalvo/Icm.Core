
using Microsoft.VisualBasic;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
namespace Icm
{

	public class ErrorEventArgs<T> : EventArgs<T>
	{

		private Exception exception_;
		public Exception Exception {
			get { return exception_; }
			set { exception_ = value; }
		}

		public ErrorEventArgs(T _data, Exception _exception) : base(_data)
		{
			exception_ = _exception;
		}

	}

}

//=======================================================
//Service provided by Telerik (www.telerik.com)
//Conversion powered by NRefactory.
//Twitter: @telerik
//Facebook: facebook.com/telerik
//=======================================================
