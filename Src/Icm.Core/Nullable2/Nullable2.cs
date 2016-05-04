using System;

namespace Icm
{
	/// <summary>
	/// Represents a nullable entity that, unlike Nullable(Of T), can enclose either a class or a struct.
	/// </summary>
	/// <typeparam name="T"></typeparam>
	/// <remarks>
	/// <para>For enclosed structs, Nullable2 works exactly as Nullable(Of T).</para>
	/// <para>For enclosed class, HasValue always returns True, since Nothing is a valid "state" for
	/// a class. You may need to HasSomething instead, which returns the same as HasValue for struct-T
	/// but returns False for class-T if the value is Nothing.</para>
	/// </remarks>
	public struct Nullable2<T>
	{
		private T _value;
		private bool _hasStructValue;

		private static readonly bool IsClass = typeof(T).IsClass;
        
		public T Value
        {
			get
			{
			    if (HasValue)
                {
					return _value;
				}

			    throw new InvalidOperationException("Null value");
			}
		    set
            {
				_value = value;
				_hasStructValue = true;
			}
		}

		public T V
        {
			get { return Value; }
			set { this.Value = value; }
		}

		public bool HasValue => IsClass || _hasStructValue;

	    public bool HasSomething {
			get
			{
			    if (IsClass) {
					return _value != null;
				}

			    return _hasStructValue;
			}
	    }

		public static implicit operator T(Nullable2<T> d)
		{
			return d.Value;
		}

		public static implicit operator Nullable2<T>(T b)
		{
			return new Nullable2<T> { Value = b };
		}

	}

}