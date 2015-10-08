
using Microsoft.VisualBasic;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
namespace Icm.Configuration
{
	public class TypedDictionarySectionHandler : System.Configuration.IConfigurationSectionHandler
	{

		public object Create(object parent, object configContext, System.Xml.XmlNode section)
		{

			Generic.Dictionary<string, TypedValue> ht = new Generic.Dictionary<string, TypedValue>();
			TypedValue tv = default(TypedValue);
			Type t = default(Type);
			foreach (Xml.XmlNode child in section.ChildNodes) {
				switch (child.Name) {
					case "add":
						if (child.Attributes("type") == null) {
							tv = new TypedValue(typeof(string), child.Attributes("value").Value);
						} else {
							t = Type.GetType("System." + child.Attributes("type").Value);
							tv = new TypedValue(t, Convert.ChangeType(child.Attributes("value").Value, Type.GetTypeCode(t), CultureInfo.InvariantCulture));
						}
						ht.Add(child.Attributes("key").Value, tv);
						break;
					case "#comment":
						break;
					// Ignore comments
					default:
						throw new InvalidOperationException(string.Format(CultureInfo.CurrentCulture, "<{0}>: Not valid in a hashtable", child.Name));
				}
			}

			return ht;
		}

	}
}

//=======================================================
//Service provided by Telerik (www.telerik.com)
//Conversion powered by NRefactory.
//Twitter: @telerik
//Facebook: facebook.com/telerik
//=======================================================
