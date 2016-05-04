using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
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
		public static void CopyTo(this object objSource, object objDest, IEnumerable<string> excludedMembers = null, IEnumerable<string> excludedTypes = null)
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
		public static void CopyTo<T>(this T objSource, T objDest, IEnumerable<string> excludedMembers = null, IEnumerable<string> excludedTypes = null)
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
		public static T Clone<T>(this T obj, IEnumerable<string> excludedMembers = null, IEnumerable<string> excludedTypes = null) where T : new()
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
		public static void CopyEntityTo<T>(this T objSource, T objDest, params string[] destKeyProperties)
		{
			CopyTo(objSource, objDest, excludedMembers: new[] {
				"EntityKey",
				"EntityState",
				"RelationshipManager"
			}.Union(destKeyProperties), excludedTypes: new[] { "ICollection" });
		}

		/// <summary>
		/// Copy (shallow) an Entity Framework proxy object.
		/// </summary>
		/// <param name="objSource"></param>
		/// <param name="objDest"></param>
		/// <param name="destKeyProperties"></param>
		/// <remarks></remarks>
		public static void CopyEntityTo(this object objSource, object objDest, params string[] destKeyProperties)
		{
			CopyTo(objSource, objDest, excludedMembers: new[] {
                "EntityKey",
				"EntityState",
				"RelationshipManager"
			}.Union(destKeyProperties), excludedTypes: new[] { "ICollection" });
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
			} else if (ObjectReflectionExtensions.HasProp(objSource, propName)) {
				if (!prop.GetIndexParameters().Any()) {
					Debug.Print("-- Copying property {0} with value [{1}] (old: [{2}])", propName, objSource.GetProp(propName), objDest.GetProp(propName));
                    objDest.SetProp(propName, objSource.GetProp(propName));
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
			} else if (objSource.HasField(fieldName)) {
				Debug.Print("-- Copying field {0} with value [{1}] (old: [{2}])", fieldName, objSource.GetField(fieldName), objDest.GetField(fieldName));
                objDest.SetField(fieldName, objSource.GetField(fieldName));
			} else {
				Debug.Print("---- Field {0} does not exist at source", fieldName);
			}
		}

		private static void CopyToAux(object objSource, object objDest, IEnumerable<string> excludedMembers, IEnumerable<string> excludedTypes)
		{
			var destFields = objDest.GetType().GetFields();
			excludedMembers = excludedMembers ?? new string [0];
			excludedTypes = excludedTypes ?? new string[0];
			foreach (var destField in destFields) {
				CopyField(objSource, objDest, destField, excludedMembers, excludedTypes);
			}
			var destProps = objDest.GetType().GetProperties();
			foreach (var destProp in destProps) {
				CopyProp(objSource, objDest, destProp, excludedMembers, excludedTypes);
			}
		}
	}
}