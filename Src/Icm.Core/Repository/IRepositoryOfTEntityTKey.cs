
using Microsoft.VisualBasic;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
namespace Icm.Data
{

	public interface IRepository<TEntity, TKey> : IEnumerable<TEntity>
	{

		void Add(TEntity entity);
		TEntity GetById(TKey key);

		bool Contains(TEntity entity);

		bool ContainsId(TKey key);


		void Delete(TEntity entity);
	}

}

//=======================================================
//Service provided by Telerik (www.telerik.com)
//Conversion powered by NRefactory.
//Twitter: @telerik
//Facebook: facebook.com/telerik
//=======================================================
