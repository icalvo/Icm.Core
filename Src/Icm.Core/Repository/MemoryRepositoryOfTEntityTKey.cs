
using Microsoft.VisualBasic;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using Icm.Reflection;
using System.Linq.Expressions;

namespace Icm.Data
{
	public class MemoryRepository<TEntity, TKey> : IRepository<TEntity, TKey>
	{

		protected ICollection<TEntity> store;

		private readonly Expression<Func<TEntity, TKey>> _idFunction;
		/// <summary>
		/// Initializes a new instance of the MemoryRepository class.
		/// </summary>
		/// <param name="idFunction"></param>
		public MemoryRepository(Expression<Func<TEntity, TKey>> idFunction)
		{
			store = new HashSet<TEntity>();
			_idFunction = idFunction;
		}

		/// <summary>
		/// Initializes a new instance of the MemoryRepository class.
		/// </summary>
		/// <param name="store"></param>
		/// <param name="idFunction"></param>
		public MemoryRepository(ICollection<TEntity> store, Expression<Func<TEntity, TKey>> idFunction)
		{
			this.store = store;
			_idFunction = idFunction;
		}

		public void Load(IEnumerable<TEntity> entities)
		{
			store = new HashSet<TEntity>(entities);
		}

		public void Add(TEntity entity)
		{
			store.Add(entity);
		}

		public void Delete(TEntity entity)
		{
			store.Remove(entity);
		}

		private Expression<Func<TEntity, bool>> IdEqualsExpression(TKey key)
		{
			// Going from _idFunction, that has the form "Function(x) x.Id",
			// to "Function(x) x.Id = VALUE" (where VALUE is the supplied key)

			// So first we build the equality expression "x.Id = VALUE":
			dynamic equalExpr = Expression.Equal(_idFunction.Body, Expression.Constant(key));

			// Then we create a new lambda with the equality expression as body and the same parameter of _idFunction.
			return Expression.Lambda<Func<TEntity, bool>>(equalExpr, _idFunction.Parameters.First);
		}

		public virtual TEntity GetById(TKey key)
		{
			return store.AsQueryable().SingleOrDefault(IdEqualsExpression(key));
		}

		public IEnumerator<TEntity> GetEnumerator()
		{
			return store.GetEnumerator;
		}

		public IEnumerator GetEnumerator1()
		{
			return store.GetEnumerator;
		}
		IEnumerator IEnumerable.GetEnumerator()
		{
			return GetEnumerator1();
		}

		public bool Contains(TEntity entity)
		{
			return store.Contains(entity);
		}

		public bool ContainsId(TKey key)
		{
			return store.AsQueryable().Any(IdEqualsExpression(key));
		}
	}

}

//=======================================================
//Service provided by Telerik (www.telerik.com)
//Conversion powered by NRefactory.
//Twitter: @telerik
//Facebook: facebook.com/telerik
//=======================================================
