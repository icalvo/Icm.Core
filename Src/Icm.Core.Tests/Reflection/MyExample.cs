
using Microsoft.VisualBasic;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using Icm.Reflection;


public class MyExample
{
	public string myfield = "mytest";

	public int mynumber = 7;
	public string MyProp { get; set; }
	public int MyPropNumber { get; set; }

	public string[] ArrayProp { get; set; }

	public string IndexedProp {
		get { return "<<" + a + ">>"; }

		set { }
	}

	public string IndexedProp2 {
		get { return "<<" + a + ";" + b + ">>"; }

		set { }
	}

	public Dictionary<string, string> DictPropStr { get; set; }

	public Dictionary<int, string> DictPropInt { get; set; }

	public Dictionary<System.DateTime, string> DictPropDate { get; set; }

}

//=======================================================
//Service provided by Telerik (www.telerik.com)
//Conversion powered by NRefactory.
//Twitter: @telerik
//Facebook: facebook.com/telerik
//=======================================================
