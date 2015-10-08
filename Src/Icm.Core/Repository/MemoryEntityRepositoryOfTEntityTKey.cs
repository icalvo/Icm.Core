using System.Collections;
using System.Collections.Generic;

namespace Icm.Data
{

	public class MemoryEntityRepository<TEntity, TKey> : IRepository<TEntity, TKey> where TEntity : IEntity<TKey>
	{


		protected Dictionary<TKey, TEntity> store;
		/// <summary>
		/// Initializes a new instance of the MemoryRepository class.
		/// </summary>
		public MemoryEntityRepository()
		{
			store = new Dictionary<TKey, TEntity>();
		}

		/// <summary>
		/// Initializes a new instance of the MemoryRepository class.
		/// </summary>
		/// <param name="items"></param>
		public MemoryEntityRepository(ICollection<TEntity> items)
		{
			Load(items);
		}

		public void Load(IEnumerable<TEntity> items)
		{
			this.store = new Dictionary<TKey, TEntity>();
			foreach (void item_loopVariable in items) {
				item = item_loopVariable;
				Add(item);
			}
		}

		public void Add(TEntity entity)
		{
			store.Add(entity.Id, entity);
		}

		public void Delete(TEntity entity)
		{
			store.Remove(entity.Id);
		}

		public virtual TEntity GetById(TKey id)
		{
			return store(id);
		}

		public IEnumerator<TEntity> GetEnumerator()
		{
			return store.Values.GetEnumerator;
		}

		public IEnumerator GetEnumerator1()
		{
			return store.Values.GetEnumerator;
		}
		IEnumerator IEnumerable.GetEnumerator()
		{
			return GetEnumerator1();
		}

		public bool Contains(TEntity entity)
		{
			return store.ContainsKey(entity.Id);
		}

		public bool ContainsId(TKey key)
		{
			return store.ContainsKey(key);
		}
	}

}

//=======================================================
//Service provided by Telerik (www.telerik.com)
//Conversion powered by NRefactory.
//Twitter: @telerik
//Facebook: facebook.com/telerik
//=======================================================
