
using Microsoft.VisualBasic;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
namespace Icm.Text.RegularExpressions
{

	public static class UsualRegularExpressions
	{
		public const string regexReal = "[+-]?\\d+([\\.,]\\d*)?";
		public const string regexInteger = "[+-]?\\d+";
		public const string regexOctal = "[+-]?[0-7]+";

		public const string regexHexadecimal = "[+-]?[0-9a-fA-F]+";
		public const string DateOnlyValidator = "\\d?\\d/\\d?\\d/([1-9]\\d)?\\d\\d";
		public const string DateAndTimeValidator = "\\d?\\d/\\d?\\d/(\\d\\d)?\\d\\d\\s+\\d?\\d:\\d\\d";
		public const string TimeOnlyValidator = "\\d?\\d:\\d\\d";

		public const string DateOrTimeValidator = "\\d?\\d/\\d?\\d/(\\d\\d)?\\d\\d(\\s+\\d?\\d:\\d\\d)?";
		public const string RealValidator = "^[+-]?\\d+([.,]\\d*)?$";

		public const string IntegerValidator = "\\d+";
	}


}


//=======================================================
//Service provided by Telerik (www.telerik.com)
//Conversion powered by NRefactory.
//Twitter: @telerik
//Facebook: facebook.com/telerik
//=======================================================
