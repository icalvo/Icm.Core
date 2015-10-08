namespace Icm.MathTools
{

	public class LimitedQueue<T>
	{


		private Generic.LinkedList<T> store_;

		private int limit_;
		public LimitedQueue(int limit)
		{
			store_ = new Generic.LinkedList<T>();
			limit_ = limit;
		}

		public void Enqueue(T o)
		{
			if (store_.Count == limit_) {
				Dequeue();
			}
			store_.AddLast(o);
		}

		public void Dequeue()
		{
			store_.RemoveFirst();
		}

		public T Head()
		{
			return store_.First.Value;
		}

		public T Tail()
		{
			return store_.Last.Value;
		}
	}
}

//=======================================================
//Service provided by Telerik (www.telerik.com)
//Conversion powered by NRefactory.
//Twitter: @telerik
//Facebook: facebook.com/telerik
//=======================================================
