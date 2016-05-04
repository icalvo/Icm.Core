using System.Collections.Generic;

namespace Icm.Collections.Generic.General
{

	/// <summary>
	/// In order to use a RepositorySortedCollection, an implementation of this interface must be provided.
	/// It is a simple CRUD repository, except for the reading methods.
	/// </summary>
	/// <typeparam name="TKey"></typeparam>
	/// <typeparam name="TValue"></typeparam>
	/// <remarks></remarks>
	public interface ISortedCollectionRepository<TKey, TValue>
	{

		IEnumerable<Pair<TKey, TValue>> GetRange(TKey rangeStart, TKey rangeEnd);
		Nullable2<TKey> GetNext(TKey key);
		Nullable2<TKey> GetPrevious(TKey key);

		void Add(TKey key, TValue val);
		void Update(TKey key, TValue val);
		void Remove(TKey key);
		int Count();
	}

}

//=======================================================
//Service provided by Telerik (www.telerik.com)
//Conversion powered by NRefactory.
//Twitter: @telerik
//Facebook: facebook.com/telerik
//=======================================================
