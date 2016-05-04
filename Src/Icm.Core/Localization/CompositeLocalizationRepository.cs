using System;
using System.Collections.Generic;
using System.Linq;

namespace Icm.Localization
{

	public class CompositeLocalizationRepository : ILocalizationRepository
	{


		private readonly IEnumerable<ILocalizationRepository> _subrepositories;
		public CompositeLocalizationRepository(IEnumerable<ILocalizationRepository> subrepositories)
		{
			if (subrepositories == null) {
				throw new ArgumentNullException(nameof(subrepositories));
			}

			if (!subrepositories.Any()) {
				throw new ArgumentException("msg", nameof(subrepositories));
			}

			_subrepositories = subrepositories.ToList();
		}

		public CompositeLocalizationRepository(params ILocalizationRepository[] subrepositories)
		{
			if (subrepositories == null) {
				throw new ArgumentNullException(nameof(subrepositories));
			}

			if (!subrepositories.Any()) {
				throw new ArgumentException("msg", nameof(subrepositories));
			}

			_subrepositories = subrepositories.ToList();
		}

		public string this[int lcid, string key] {
		    get
		    {
		        return _subrepositories
                    .Select(subrepository => subrepository[lcid, key])
                    .FirstOrDefault(result => result != null);
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
