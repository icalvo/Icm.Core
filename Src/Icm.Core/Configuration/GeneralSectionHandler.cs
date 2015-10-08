
using Microsoft.VisualBasic;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;

namespace Icm.Configuration
{
	public class GeneralSectionHandler : System.Configuration.IConfigurationSectionHandler
	{

		public object Create(object parent, object configContext, System.Xml.XmlNode section)
		{
			return ManageSection(section);
		}

		/// <summary>
		///   Manages a section.
		/// </summary>
		/// <param name="section"></param>
		/// <returns></returns>
		/// <remarks>
		///   Depending on the section type it treats it as an array or as a table.
		/// </remarks>
		/// <history>
		/// 	[icalvo]	09/03/2006	Created
		/// </history>
		public object ManageSection(System.Xml.XmlNode section)
		{
			switch (section.Attributes("type").Value) {
				case "array":
					return BuildArray(section);
				case "hash":
					return BuildHash(section);
				default:
					throw new InvalidOperationException(section.Attributes("type").Value + ": Not valid element type");
			}
		}

		public IList<object> BuildArray(System.Xml.XmlNode section)
		{
			Generic.List<object> lo = new Generic.List<object>();

			foreach (Xml.XmlNode child in section.ChildNodes) {
				switch (child.Name) {
					case "add":
						if (child.Attributes("type") == null) {
							lo.Add(child.Attributes("value").Value);
						} else {
							lo.Add(ManageSection(child));
						}
						break;
					case "#comment":
						break;
					// Ignore comments
					default:
						throw new InvalidOperationException("<" + child.Name + ">: Not valid in an array");
				}
			}

			return lo;
		}

		public IDictionary<string, object> BuildHash(System.Xml.XmlNode section)
		{
			Generic.Dictionary<string, object> ht = new Generic.Dictionary<string, object>();
			foreach (Xml.XmlNode child in section.ChildNodes) {
				switch (child.Name) {
					case "add":
						if (child.Attributes("type") == null) {
							ht.Add(child.Attributes("key").Value, child.Attributes("value").Value);
						} else {
							ht.Add(child.Attributes("key").Value, ManageSection(child));
						}
						break;
					case "remove":
						ht.Remove(child.Attributes("value").Value);
						break;
					case "#comment":
						break;
					// Ignore comments
					default:
						throw new InvalidOperationException("<" + child.Name + ">: Not valid in a hashtable");
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
