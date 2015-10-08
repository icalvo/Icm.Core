using System;
using System.Collections;
using System.Collections.Generic;

namespace Icm.Collections.Generic
{

	/// <summary>
	/// Transforms an IEnumerator(Of T) into an IEnumerator(Of Vector2(Of T)) which enumerates
	/// the consecutive pairs of the original IEnumerator.
	/// </summary>
	/// <remarks>
	/// <para>For example, if the original IEnumerator contains three elements a, b, c, this enumerator
	/// will have the following two elements: (a, b), (b, c).</para>
	/// <para>If the original IEnumerator contains just one element x, this class will return a single
	/// pair (x, x).</para>
	/// <para>This enumerator will NOT dispose the original enumerator.</para>
	/// </remarks>
	public class PairEnumerator<T> : IEnumerator<Vector2<T>>, IEnumerable<Vector2<T>>
	{

		private Vector2<T> current_;

		private readonly IEnumerator<T> pointIter_;
		public PairEnumerator(IEnumerator<T> _pointIter)
		{
			pointIter_ = _pointIter;
		}

		public Vector2<T> Current {
			get {
				if (current_ == null) {
					throw new InvalidOperationException("Enumerator has not been reset");
				}
				return current_;
			}
		}

		public object Current1 {
			get {
				if (current_ == null) {
					throw new InvalidOperationException("Enumerator has not been reset");
				}
				return current_;
			}
		}
		object System.Collections.IEnumerator.Current {
			get { return Current1; }
		}

		public bool MoveNext()
		{
			T item2 = default(T);

			if (current_ == null) {
				pointIter_.MoveNext();
				dynamic item1 = pointIter_.Current;
				if (pointIter_.MoveNext) {
					item2 = pointIter_.Current;
				} else {
					item2 = item1;
				}
				current_ = new Vector2<T>(item1, item2);
			} else {
				if (pointIter_.MoveNext) {
					item2 = pointIter_.Current;
					current_ = new Vector2<T>(current_.Item2, item2);
				} else {
					current_ = null;
					return false;
				}
			}
			return true;
		}


		public void Reset()
		{
			current_ = null;
		}

			// To detect redundant calls
		private bool disposedValue = false;

		// IDisposable
		protected virtual void Dispose(bool disposing)
		{
			if (!this.disposedValue) {
				if (disposing) {
					// free other state (managed objects).
				}

				// free your own state (unmanaged objects).
				// set large fields to null.
			}
			this.disposedValue = true;
		}

		#region " IDisposable Support "
		// This code added by Visual Basic to correctly implement the disposable pattern.
		public void Dispose()
		{
			// Do not change this code.  Put cleanup code in Dispose(ByVal disposing As Boolean) above.
			Dispose(true);
			GC.SuppressFinalize(this);
		}
		#endregion

		public IEnumerator<Vector2<T>> GetEnumerator()
		{
			return this;
		}

		public IEnumerator GetEnumerator1()
		{
			return this;
		}
		IEnumerator System.Collections.IEnumerable.GetEnumerator()
		{
			return GetEnumerator1();
		}
	}
}

//=======================================================
//Service provided by Telerik (www.telerik.com)
//Conversion powered by NRefactory.
//Twitter: @telerik
//Facebook: facebook.com/telerik
//=======================================================
