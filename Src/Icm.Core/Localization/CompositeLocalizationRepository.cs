using System;
using System.Collections.Generic;

namespace Icm.Localization
{

	public class CompositeLocalizationRepository : ILocalizationRepository
	{


		private readonly IEnumerable<ILocalizationRepository> _subrepositories;
		public CompositeLocalizationRepository(IEnumerable<ILocalizationRepository> subrepositories)
		{
			if (subrepositories == null) {
				throw new ArgumentNullException("subrepositories");
			}

			if (subrepositories.Count == 0) {
				throw new ArgumentException("subrepositories");
			}

			_subrepositories = subrepositories.ToList();
		}

		public CompositeLocalizationRepository(params ILocalizationRepository[] subrepositories)
		{
			if (subrepositories == null) {
				throw new ArgumentNullException("subrepositories");
			}

			if (subrepositories.Count == 0) {
				throw new ArgumentException("subrepositories");
			}

			_subrepositories = subrepositories.ToList();
		}

		public string this[int lcid, string key] {
			get {
				foreach (void subrepository_loopVariable in _subrepositories) {
					subrepository = subrepository_loopVariable;
					string result = subrepository.ItemForCulture(lcid, key);
					if (result != null) {
						return result;
					}
				}

				return null;
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
