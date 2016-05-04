using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace Icm.Collections.Generic
{

	/// <summary>
	/// Priority queue with N possible priorities. You can introduce elements
	/// with a given priority.
	/// </summary>
	/// <typeparam name="T"></typeparam>
	/// <remarks></remarks>
	public class PriorityQueue<T>
	{

		private List<Queue<T>> store_;

		private int count_;
		/// <summary>
		///  Build a new priority queue with a given maximum priority.
		/// </summary>
		/// <param name="maxprio">Zero-based maximum priority.</param>
		/// <remarks>Remember that priorities start with 0.</remarks>
		public PriorityQueue(int maxprio)
		{
			Debug.Assert(maxprio >= 0, "The maximum priority must be greater or equal than zero");
			store_ = new List<Queue<T>>(maxprio);
			for (int i = 0; i <= maxprio; i++) {
				store_.Add(new Queue<T>());
			}
			count_ = 0;
		}

		/// <summary>
		///  Enqueue an element with the lowest priority.
		/// </summary>
		/// <param name="val">Enqueued element.</param>
		/// <remarks></remarks>
		public void Enqueue(T val)
		{
			store_[0].Enqueue(val);
			count_ += 1;
		}

		/// <summary>
		///  Enqueue an element with the given priority.
		/// </summary>
		/// <param name="prio">Priority with which the element will be enqueued.</param>
		/// <param name="val">Enqueued element.</param>
		/// <remarks></remarks>
		public void Enqueue(int prio, T val)
		{
			Debug.Assert(prio >= 0, "The maximum priority must be greater or equal than zero");
			Debug.Assert(prio < store_.Count - 1, "The priority is greater than the maximum priority defined for this queue");

			store_[prio].Enqueue(val);
			count_ += 1;
		}

		/// <summary>
		///  Dequeue and return the first element in the queue with the given priority.
		/// </summary>
		/// <param name="prio">A priority.</param>
		/// <returns>The dequeued element.</returns>
		/// <remarks>This function modifies the object. It is protected because it can break the class invariant.</remarks>
		protected T Dequeue(int prio)
		{
			Debug.Assert(prio >= 0, "The maximum priority must be greater or equal than zero");
			Debug.Assert(prio < store_.Count - 1, "The priority is greater than the maximum priority defined for this queue");

		    count_ -= 1;
		    return store_[prio].Dequeue();
		}

		/// <summary>
		///  Dequeue the first element of the queue of greatest priority.
		/// </summary>
		/// <returns>The dequeued element.</returns>
		/// <remarks>This function modifies the object.</remarks>
		public T Dequeue()
		{
			for (int i = store_.Count - 1; i >= 0; i += -1) {
				if (store_[i].Count > 0) {
					count_ -= 1;
					return store_[i].Dequeue();
				}
			}
			throw new InvalidOperationException("The priority queue is empty");
		}

		/// <summary>
		///  Gets the number of elements actually contained in the PriorityQueue.
		/// </summary>
		/// <value>The number of elements actually contained in the PriorityQueue.</value>
		/// <returns></returns>
		/// <remarks></remarks>
		public int Count {
			get { return count_; }
		}

		/// <summary>
		///  Wipes the contents.
		/// </summary>
		/// <remarks></remarks>
		public void Clear()
		{
			foreach (Queue<T> q in store_) {
				q.Clear();
			}
			count_ = 0;
		}
	}

}

//=======================================================
//Service provided by Telerik (www.telerik.com)
//Conversion powered by NRefactory.
//Twitter: @telerik
//Facebook: facebook.com/telerik
//=======================================================
