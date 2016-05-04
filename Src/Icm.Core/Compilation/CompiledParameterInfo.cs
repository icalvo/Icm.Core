namespace Icm.Compilation
{

	public abstract class CompiledParameterInfo
	{
	    public string Name { get; protected set; }

	    public abstract string ArgType { get; }

	}

	public class CompiledParameterInfo<T> : CompiledParameterInfo
	{


		private static readonly string _argType;
		static CompiledParameterInfo()
		{
			_argType = typeof(T).Name;
		}

		public CompiledParameterInfo(string name)
		{
			this.Name = name;
		}

		public override string ArgType => _argType;
	}

}