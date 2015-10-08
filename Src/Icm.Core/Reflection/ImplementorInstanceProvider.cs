
using Microsoft.VisualBasic;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
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

		private readonly Dictionary<string, T> implementorInstances_;
		public ImplementorInstanceProvider()
		{
			implementorInstances_ = new Dictionary<string, T>();
			CreateList();
			AppDomain.CurrentDomain.AssemblyLoad += new AssemblyLoadEventHandler(AssemblyLoaded);
		}

		public List<T> ImplementorsInstances {
			get { return implementorInstances_.Values.ToList; }
		}

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
			dynamic newInstances = GetInstanceDictionaryOfAllImplementors<T>(assembly);
			foreach (void inst_loopVariable in newInstances) {
				inst = inst_loopVariable;
				if (implementorInstances_.ContainsKey(inst.Key)) {
					implementorInstances_(inst.Key) = inst.Value;
				} else {
					implementorInstances_.Add(inst.Key, inst.Value);
				}
			}
		}

	}

}

//=======================================================
//Service provided by Telerik (www.telerik.com)
//Conversion powered by NRefactory.
//Twitter: @telerik
//Facebook: facebook.com/telerik
//=======================================================
