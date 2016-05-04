using System;
using System.Collections.Generic;
using System.Xml;

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
			switch (section.Attributes?["type"].Value) {
				case "array":
					return BuildArray(section);
				case "hash":
					return BuildHash(section);
				default:
					throw new InvalidOperationException(section.Attributes?["type"].Value + ": Not valid element type");
			}
		}

		public IList<object> BuildArray(System.Xml.XmlNode section)
		{
			List<object> lo = new List<object>();

			foreach (XmlNode child in section.ChildNodes) {
				switch (child.Name) {
					case "add":
				        lo.Add(child.Attributes?["type"] == null 
                            ? child.Attributes?["value"].Value 
                            : ManageSection(child));
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
			var ht = new Dictionary<string, object>();
			foreach (XmlNode child in section.ChildNodes) {
				switch (child.Name) {
					case "add":
				        ht.Add(child.Attributes?["key"].Value,
				            child.Attributes?["type"] == null 
                            ? child.Attributes?["value"].Value 
                            : ManageSection(child));
				        break;
					case "remove":
				        if (ht.ContainsKey(child.Attributes["value"].Value))
                            ht.Remove(child.Attributes["value"].Value);
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