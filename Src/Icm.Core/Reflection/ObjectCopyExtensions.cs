
using Microsoft.VisualBasic;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Runtime.CompilerServices;

namespace Icm.Reflection
{

	public static class ObjectCopyExtensions
	{

		/// <summary>
		/// Copy (shallow) the properties of an object to another.
		/// </summary>
		/// <param name="objSource"></param>
		/// <param name="objDest"></param>
		/// <param name="excludedMembers"></param>
		/// <param name="excludedTypes"></param>
		/// <remarks></remarks>
		[Extension()]
		public static void CopyTo(object objSource, object objDest, IEnumerable<string> excludedMembers = null, IEnumerable<string> excludedTypes = null)
		{
			CopyToAux(objSource, objDest, excludedMembers, excludedTypes);
		}

		/// <summary>
		/// Copy (shallow) the properties of an object to another.
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <param name="objSource"></param>
		/// <param name="objDest"></param>
		/// <param name="excludedMembers"></param>
		/// <param name="excludedTypes"></param>
		/// <remarks></remarks>
		[Extension()]
		public static void CopyTo<T>(T objSource, T objDest, IEnumerable<string> excludedMembers = null, IEnumerable<string> excludedTypes = null)
		{
			CopyToAux(objSource, objDest, excludedMembers, excludedTypes);
		}

		/// <summary>
		/// Clone an object with a shallow copy.
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <param name="obj"></param>
		/// <param name="excludedMembers"></param>
		/// <param name="excludedTypes"></param>
		/// <returns></returns>
		/// <remarks></remarks>
		[Extension()]
		public static T Clone<T>(T obj, IEnumerable<string> excludedMembers = null, IEnumerable<string> excludedTypes = null) where T : new()
		{
			T result = new T();

			CopyToAux(obj, result, excludedMembers, excludedTypes);

			return result;
		}

		/// <summary>
		/// Copy (shallow) an Entity Framework proxy object.
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <param name="objSource">Source object</param>
		/// <param name="objDest">Target object</param>
		/// <param name="destKeyProperties"></param>
		/// <remarks></remarks>
		[Extension()]
		public static void CopyEntityTo<T>(T objSource, T objDest, params string[] destKeyProperties)
		{
			CopyTo(objSource, objDest, excludedMembers: {
				"EntityKey",
				"EntityState",
				"RelationshipManager"
			}.Union(destKeyProperties), excludedTypes: { "ICollection" });
		}

		/// <summary>
		/// Copy (shallow) an Entity Framework proxy object.
		/// </summary>
		/// <param name="objSource"></param>
		/// <param name="objDest"></param>
		/// <param name="destKeyProperties"></param>
		/// <remarks></remarks>
		[Extension()]
		public static void CopyEntityTo(object objSource, object objDest, params string[] destKeyProperties)
		{
			CopyTo(objSource, objDest, excludedMembers: {
				"EntityKey",
				"EntityState",
				"RelationshipManager"
			}.Union(destKeyProperties), excludedTypes: { "ICollection" });
		}

		/// <summary>
		/// Copy a property of an object to the same named property of another object
		/// </summary>
		/// <param name="objSource"></param>
		/// <param name="objDest"></param>
		/// <param name="prop"></param>
		/// <param name="excludedProperties"></param>
		/// <param name="excludedTypes"></param>
		/// <remarks></remarks>
		private static void CopyProp(object objSource, object objDest, System.Reflection.PropertyInfo prop, IEnumerable<string> excludedProperties, IEnumerable<string> excludedTypes)
		{
			string propName = prop.Name;
			if (excludedProperties.Contains(propName)) {
				Debug.Print("---- Property {0} excluded", propName);
			} else if (excludedTypes.Any(exclType => prop.PropertyType.Name.StartsWith(exclType))) {
				Debug.Print("---- Property {0} excluded for being of type {1}", propName, prop.PropertyType.Name);
			} else if (HasProp(objSource, propName)) {
				if (prop.GetIndexParameters.Count == 0) {
					Debug.Print("-- Copying property {0} with value [{1}] (old: [{2}])", propName, GetProp(objSource, propName), GetProp(objDest, propName));
					SetProp(objDest, propName, GetProp(objSource, propName));
				} else {
					Debug.Print("---- Property {0} is indexed", propName);
				}
			} else {
				Debug.Print("---- Property {0} does not exist at source", propName);
			}
		}


		/// <summary>
		/// Copy a property of an object to the same named property of another object
		/// </summary>
		/// <param name="objSource"></param>
		/// <param name="objDest"></param>
		/// <param name="field"></param>
		/// <param name="excludedFields"></param>
		/// <param name="excludedTypes"></param>
		/// <remarks></remarks>
		private static void CopyField(object objSource, object objDest, System.Reflection.FieldInfo field, IEnumerable<string> excludedFields, IEnumerable<string> excludedTypes)
		{
			string fieldName = field.Name;
			if (excludedFields.Contains(fieldName)) {
				Debug.Print("---- Field {0} excluded", fieldName);
			} else if (excludedTypes.Any(exclType => field.FieldType.Name.StartsWith(exclType))) {
				Debug.Print("---- Field {0} excluded for being of type {1}", fieldName, field.FieldType.Name);
			} else if (HasField(objSource, fieldName)) {
				Debug.Print("-- Copying field {0} with value [{1}] (old: [{2}])", fieldName, GetField(objSource, fieldName), GetField(objDest, fieldName));
				SetField(objDest, fieldName, GetField(objSource, fieldName));
			} else {
				Debug.Print("---- Field {0} does not exist at source", fieldName);
			}
		}

		private static void CopyToAux(object objSource, object objDest, IEnumerable<string> excludedMembers, IEnumerable<string> excludedTypes)
		{
			dynamic destFields = objDest.GetType.GetFields;
			excludedMembers = excludedMembers ?? {
				
			};
			excludedTypes = excludedTypes ?? {
				
			};
			foreach (void destField_loopVariable in destFields) {
				destField = destField_loopVariable;
				CopyField(objSource, objDest, destField, excludedMembers, excludedTypes);
			}
			dynamic destProps = objDest.GetType.GetProperties();
			foreach (void destProp_loopVariable in destProps) {
				destProp = destProp_loopVariable;
				CopyProp(objSource, objDest, destProp, excludedMembers, excludedTypes);
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
