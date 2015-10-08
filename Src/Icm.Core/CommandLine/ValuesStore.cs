
using Microsoft.VisualBasic;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using res = My.Resources.CommandLineTools;

namespace Icm.CommandLineTools
{

	public class ValuesStore
	{

		private readonly Dictionary<string, List<string>> _store = new Dictionary<string, List<string>>();

		private readonly List<string> _mainStore = new List<string>();
		public void Clear()
		{
			_store.Clear();
			_mainStore.Clear();
		}

		public bool ContainsKey(string s)
		{
			return _store.ContainsKey(s);
		}

		public void AddParameter(string shortName, string longName)
		{
			if (shortName == null) {
				throw new ArgumentNullException("shortName");
			}
			if (longName == null) {
				throw new ArgumentNullException("longName");
			}
			if (shortName.Length >= longName.Length) {
				throw new ArgumentException(string.Format(res.S_ERR_SHORT_LONGER_THAN_LONG, shortName, longName));
			}
			_store.Add(shortName, new List<string>());
			_store.Add(longName, new List<string>());
		}

		public void RemoveValues(string shortName, string longName)
		{
			_store(shortName).Clear();
			_store(longName).Clear();
		}

		public void AddValue(string shortName, string longName, string val)
		{
			_store(shortName).Add(val);
			_store(longName).Add(val);
		}

		public void AddValues(string shortName, string longName, IEnumerable<string> vals)
		{
			_store(shortName).AddRange(vals);
			_store(longName).AddRange(vals);
		}

		public void AddMainValue(string val)
		{
			_mainStore.Add(val);
		}

		public void AddMainValues(IEnumerable<string> vals)
		{
			_mainStore.AddRange(vals);
		}

		public ICollection<string> Values {
			get {
				if (_store.ContainsKey(s)) {
					return _store(s).AsReadOnly;
				} else {
					return (new List<string>()).AsReadOnly;
				}
			}
		}

		public ICollection<string> MainValues {
			get { return _mainStore.AsReadOnly; }
		}
	}
}

//=======================================================
//Service provided by Telerik (www.telerik.com)
//Conversion powered by NRefactory.
//Twitter: @telerik
//Facebook: facebook.com/telerik
//=======================================================
