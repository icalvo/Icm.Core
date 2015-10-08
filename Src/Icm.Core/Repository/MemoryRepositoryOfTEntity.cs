
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

	public class MemoryRepository<TEntity> : MemoryRepository<TEntity, int>, IRepository<TEntity>
	{

		/// <summary>
		/// Initializes a new instance of the MemoryRepository class.
		/// </summary>
		/// <param name="idFunction"></param>
		public MemoryRepository(Expression<Func<TEntity, int>> idFunction) : base(idFunction)
		{
		}

		/// <summary>
		/// Initializes a new instance of the MemoryRepository class.
		/// </summary>
		/// <param name="store"></param>
		/// <param name="idFunction"></param>
		public MemoryRepository(HashSet<TEntity> store, Expression<Func<TEntity, int>> idFunction) : base(store, idFunction)
		{
		}

	}

}

//=======================================================
//Service provided by Telerik (www.telerik.com)
//Conversion powered by NRefactory.
//Twitter: @telerik
//Facebook: facebook.com/telerik
//=======================================================
