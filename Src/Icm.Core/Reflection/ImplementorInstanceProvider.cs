using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Icm.Reflection
{

	/// <summary>
	/// Provider of instances of a given type.
	/// </summary>
	/// <typeparam name="T"></typeparam>
	/// <remarks>
	/// <para>This class provides a list of instances of every type that implements T (one
	/// for each concrete type).</para>
	/// <para>In contrast to <see cref="ActivatorTools"></see>, the list autoupdates each 
	/// time a new assembly is loaded in the current AppDomain.</para>
	/// <para>However it has a limitation in that it only creates types with a default
	/// constructor (no arguments), because it cannot be provided with the necesary arguments.</para>
	/// </remarks>
	public class ImplementorInstanceProvider<T>
	{
		/// <summary>
		/// A list of instance providers that are available.
		/// </summary>

		private readonly Dictionary<string, T> _implementorInstances;
		public ImplementorInstanceProvider()
		{
			_implementorInstances = new Dictionary<string, T>();
			CreateList();
			AppDomain.CurrentDomain.AssemblyLoad += AssemblyLoaded;
		}

		public List<T> ImplementorsInstances => _implementorInstances.Values.ToList();

	    /// <summary>
		/// Updates the list of instance providers with any found in the newly loaded assembly.
		/// </summary>
		/// <param name="sender">The object that sent the event.</param>
		/// <param name="args">The event arguments.</param>
		private void AssemblyLoaded(object sender, AssemblyLoadEventArgs args)
		{
			UpdateList(args.LoadedAssembly);
		}
		/// <summary>
		/// Fills the list with currently loaded assemblies.
		/// </summary>
		private void CreateList()
		{
			foreach (Assembly assembly in AppDomain.CurrentDomain.GetAssemblies()) {
				UpdateList(assembly);
			}
		}

		/// <summary>
		/// Updates the list of instance providers with the ones found in the given assembly.
		/// </summary>
		/// <param name="assembly">The assembly with which the list of instance providers will be updated.</param>
		private void UpdateList(Assembly assembly)
		{
			var newInstances = ActivatorTools.GetInstanceDictionaryOfAllImplementors<T>(assembly);
			foreach (var inst in newInstances) {
				if (_implementorInstances.ContainsKey(inst.Key)) {
					_implementorInstances[inst.Key] = inst.Value;
				} else {
					_implementorInstances.Add(inst.Key, inst.Value);
				}
			}
		}
	}
}
