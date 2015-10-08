using System.Collections.Generic;

namespace Icm.Collections.Generic
{

	public class ForcedDictionary<K, V> : Dictionary<K, V>
	{

		public new V this[K key] {
			get {
				if (!base.ContainsKey(key)) {
					base.Add(key, null);
				}
				return base.Item(key);
			}
			set {
				if (base.ContainsKey(key)) {
					base.Item(key) = value;
				} else {
					base.Add(key, value);
				}

			}
		}
	}
}

//=======================================================
//Service provided by Telerik (www.telerik.com)
//Conversion powered by NRefactory.
//Twitter: @telerik
//Facebook: facebook.com/telerik
//=======================================================
