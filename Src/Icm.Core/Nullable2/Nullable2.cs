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

		private T value_;
		private bool hasStructValue_;

		private static readonly bool isClass_;
		static Nullable2()
		{
			isClass_ = typeof(T).IsClass;
		}

		public T Value {
			get {
				if (HasValue) {
					return value_;
				} else {
					throw new InvalidOperationException("Null value");
				}
			}
			set {
				value_ = value;
				hasStructValue_ = true;
			}
		}

		public T V {
			get { return Value; }
			set { this.Value = value; }
		}

		public bool HasValue {
			get { return isClass_ || hasStructValue_; }
		}


		public bool HasSomething {
			get {
				if (isClass_) {
					return value_ != null;
				} else {
					return hasStructValue_;
				}
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

//=======================================================
//Service provided by Telerik (www.telerik.com)
//Conversion powered by NRefactory.
//Twitter: @telerik
//Facebook: facebook.com/telerik
//=======================================================
