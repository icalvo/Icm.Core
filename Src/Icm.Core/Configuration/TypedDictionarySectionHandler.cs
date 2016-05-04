using System;
using System.Collections.Generic;
using System.Globalization;
using System.Xml;

namespace Icm.Configuration
{
	public class TypedDictionarySectionHandler : System.Configuration.IConfigurationSectionHandler
	{

		public object Create(object parent, object configContext, System.Xml.XmlNode section)
		{
            
			var ht = new Dictionary<string, TypedValue>();
		    foreach (XmlNode child in section.ChildNodes) {
				switch (child.Name) {
					case "add":
				        TypedValue tv;
				        if (child.Attributes["type"] == null) {
							tv = new TypedValue(typeof(string), child.Attributes["value"].Value);
						} else
				        {
				            var t = Type.GetType("System." + child.Attributes["type"].Value);
				            tv = new TypedValue(t, Convert.ChangeType(child.Attributes["value"].Value, Type.GetTypeCode(t), CultureInfo.InvariantCulture));
				        }
				        ht.Add(child.Attributes["key"].Value, tv);
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
