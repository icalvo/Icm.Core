using System.Collections.Generic;

namespace Icm.MathTools
{

	public class LimitedQueue<T>
	{
		private readonly LinkedList<T> _store;
		private readonly int _limit;

        public LimitedQueue(int limit)
		{
			_store = new LinkedList<T>();
			_limit = limit;
		}

		public void Enqueue(T o)
		{
			if (_store.Count == _limit) {
				Dequeue();
			}
			_store.AddLast(o);
		}

		public void Dequeue()
		{
			_store.RemoveFirst();
		}

		public T Head()
		{
			return _store.First.Value;
		}

		public T Tail()
		{
			return _store.Last.Value;
		}
	}
}