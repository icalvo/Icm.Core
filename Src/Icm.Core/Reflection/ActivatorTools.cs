
using Microsoft.VisualBasic;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
namespace Icm.Reflection
{
	/// <summary>
	/// Utility functions to manage Activator class.
	/// </summary>
	/// <remarks></remarks>
	public static class ActivatorTools
	{

		/// <summary>
		/// Creates an instance of a type.
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <param name="args"></param>
		/// <returns></returns>
		/// <remarks>
		/// This is a shortcut for Activator.CreateInstance that returns a typed object.
		/// Use:
		/// <example>Dim obj = CreateInstance(Of MyClass)(arg1, arg2, ...)</example>
		/// Instead of:
		/// <example>Dim obj = DirectCast(Activator.CreateInstance(GetType(MyClass), arg1, arg2, ...), MyClass)</example>
		/// </remarks>
		public static T CreateInstance<T>(params object[] args)
		{
			return (T)Activator.CreateInstance(typeof(T), args);
		}

		/// <summary>
		/// Gets all the implementors of T  in the current AppDomain.
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <returns></returns>
		/// <remarks>
		/// This method returns all the concrete classes that derive from T (including T) that 
		/// can be found on the current AppDomain assemblies.
		/// </remarks>
		public static IEnumerable<Type> GetAllImplementors<T>()
		{
			return GetAllImplementors<T>(AppDomain.CurrentDomain.GetAssemblies);
		}

		/// <summary>
		/// Gets all the implementors of T in the current AppDomain.
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <param name="assemblies"></param>
		/// <returns></returns>
		/// <remarks>
		/// This method returns all the concrete classes that derive from T (including T) that 
		/// can be found on the passed assembly list.
		/// </remarks>
		public static IEnumerable<Type> GetAllImplementors<T>(IEnumerable<System.Reflection.Assembly> assemblies)
		{
			dynamic type = typeof(T);
			return assemblies.SelectMany(s =>
			{
				try {
					return s.GetTypes();
				} catch (Exception ex) {
					return new Type[];
				}
			}).Where(p => type.IsAssignableFrom(p) && !p.IsAbstract);
		}

		/// <summary>
		/// Gets all the implementors of T in a given assembly.
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <param name="assembly"></param>
		/// <returns></returns>
		/// <remarks>
		/// This method returns all the concrete classes that derive from T (including T) that 
		/// can be found on the passed assembly.
		/// </remarks>
		public static IEnumerable<Type> GetAllImplementors<T>(System.Reflection.Assembly assembly)
		{
			return GetAllImplementors<T>({ assembly });
		}

		/// <summary>
		/// Gets a list of instances of all implementors of T in the current AppDomain.
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <param name="args"></param>
		/// <returns></returns>
		/// <remarks></remarks>
		public static IEnumerable<T> GetInstanceListOfAllImplementors<T>(params object[] args)
		{
			return GetInstanceDictionaryOfAllImplementors<T>(args).Values;
		}

		/// <summary>
		/// Gets a dictionary (key: type name) of all implementors of T in the current AppDomain.
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <param name="args"></param>
		/// <returns></returns>
		/// <remarks></remarks>
		public static Dictionary<string, T> GetInstanceDictionaryOfAllImplementors<T>(params object[] args)
		{
			dynamic types = GetAllImplementors<T>();

			Dictionary<string, T> instances = new Dictionary<string, T>();
			foreach (void type_loopVariable in types) {
				type = type_loopVariable;
				instances.Add(type.Name, (T)Activator.CreateInstance(type, args));
			}

			return instances;
		}

		/// <summary>
		/// Gets a list of instances of all implementors of T in the passed assembly.
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <param name="args"></param>
		/// <param name="assembly"></param>
		/// <returns></returns>
		/// <remarks></remarks>
		public static IEnumerable<T> GetInstanceListOfAllImplementors<T>(System.Reflection.Assembly assembly, params object[] args)
		{
			return GetInstanceDictionaryOfAllImplementors<T>({ assembly }, args).Values;
		}

		/// <summary>
		/// Gets a list of instances of all implementors of T in the passed assembly list.
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <param name="args"></param>
		/// <returns></returns>
		/// <remarks></remarks>
		public static IEnumerable<T> GetInstanceListOfAllImplementors<T>(IEnumerable<System.Reflection.Assembly> assemblies, params object[] args)
		{
			return GetInstanceDictionaryOfAllImplementors<T>(assemblies, args).Values;
		}

		public static Dictionary<string, T> GetInstanceDictionaryOfAllImplementors<T>(System.Reflection.Assembly assembly, params object[] args)
		{
			return GetInstanceDictionaryOfAllImplementors<T>({ assembly }, args);
		}

		/// <summary>
		/// Gets a dictionary (key: type name) of all implementors of T in the passed assembly list.
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <param name="args"></param>
		/// <returns></returns>
		/// <remarks></remarks>
		public static Dictionary<string, T> GetInstanceDictionaryOfAllImplementors<T>(IEnumerable<System.Reflection.Assembly> assemblies, params object[] args)
		{
			dynamic types = GetAllImplementors<T>(assemblies);

			Dictionary<string, T> instances = new Dictionary<string, T>();
			foreach (void type_loopVariable in types) {
				type = type_loopVariable;
				instances.Add(type.Name, (T)Activator.CreateInstance(type, args));
			}

			return instances;
		}


	}

}

//=======================================================
//Service provided by Telerik (www.telerik.com)
//Conversion powered by NRefactory.
//Twitter: @telerik
//Facebook: facebook.com/telerik
//=======================================================
