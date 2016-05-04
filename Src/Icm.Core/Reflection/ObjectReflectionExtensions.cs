using Microsoft.VisualBasic;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Reflection;

namespace Icm.Reflection
{

    public static class ObjectReflectionExtensions
    {

        /// <summary>
        /// Gets a field value of an object, given a call chain on another object that ends up in that field.
        /// </summary>
        /// <typeparam name="TField">Type of the field</typeparam>
        /// <param name="obj">Object</param>
        /// <param name="callChain">Name of the field</param>
        /// <returns></returns>
        /// <remarks></remarks>
        public static TField GetField<TField>(this object obj, string callChain)
        {
            return (TField)GetMemberAux(obj, callChain, MemberTypes.Field);
        }

        /// <summary>
        /// Gets a field value of an object, given a call chain on another object that ends up in that field.
        /// </summary>
        /// <param name="obj">Object</param>
        /// <param name="callChain">Name of the property</param>
        /// <returns>Value of the final element of the call chain</returns>
        /// <remarks>
        /// <example>
        /// <code>
        /// Dim str As String = "hello"
        /// Dim fmtlen As String
        /// fmtlen = str.GetField("")
        /// </code>
        /// </example>
        /// This overload return an untyped result. Use it only when you do not know or do not need it.
        /// </remarks>
        public static object GetField(this object obj, string callChain)
        {
            return GetMemberAux(obj, callChain, MemberTypes.Field);
        }

        /// <summary>
        /// Sets a field of an object, given a call chain on another object that ends up in that field.
        /// </summary>
        /// <typeparam name="TField">Type of the object</typeparam>
        /// <param name="obj">Object</param>
        /// <param name="callChain">Name of the field</param>
        /// <param name="value">New value</param>
        /// <remarks></remarks>
        public static void SetField<TField>(this TField obj, string callChain, object value)
        {
            SetMemberAux(obj, callChain, value, MemberTypes.Field);
        }

        /// <summary>
        /// Has the object a field with the given name
        /// </summary>
        /// <param name="obj"></param>
        /// <param name="TMember"></param>
        /// <returns></returns>
        /// <remarks></remarks>
        public static bool HasField(this object obj, string TMember)
        {
            if (obj == null)
                return false;
            var fi = obj.GetType().GetField(TMember);
            return fi != null;
        }

        /// <summary>
        /// Has the type a field with the given name
        /// </summary>
        /// <param name="fieldName"></param>
        /// <returns></returns>
        /// <remarks></remarks>
        public static bool HasField<T>(string fieldName)
        {
            var fi = typeof(T).GetField(fieldName);
            return fi != null;
        }

        /// <summary>
        /// Gets a property value of an object, given the property name
        /// </summary>
        /// <typeparam name="TProp">Type of the property</typeparam>
        /// <param name="obj">Object</param>
        /// <param name="propName">Name of the property</param>
        /// <returns></returns>
        /// <remarks></remarks>
        public static TProp GetProp<TProp>(this object obj, string propName)
        {
            return (TProp)GetMemberAux(obj, propName, MemberTypes.Property, null);
        }

        /// <summary>
        /// Gets a property value of an object, given the property name
        /// </summary>
        /// <typeparam name="TProp">Type of the property</typeparam>
        /// <param name="obj">Object</param>
        /// <param name="propName">Name of the property</param>
        /// <returns></returns>
        /// <remarks></remarks>
        public static TProp GetProp<TProp>(this object obj, string propName, object index)
        {
            return (TProp)GetMemberAux(obj, propName, MemberTypes.Property, index);
        }
        /// <summary>
        /// Gets a property value of an object, given the property name
        /// </summary>
        /// <param name="obj">Object</param>
        /// <param name="propName">Name of the property</param>
        /// <returns>Value of property with name propName for the object obj</returns>
        /// <remarks>
        /// This overload return an untyped result. Use it only when you do not know or do not need it.
        /// </remarks>
        public static object GetProp(this object obj, string propName)
        {
            return GetMemberAux(obj, propName, MemberTypes.Property);
        }

        /// <summary>
        /// Gets an indexed property value of an object, given the property name
        /// </summary>
        /// <param name="obj">Object</param>
        /// <param name="propName">Name of the property</param>
        /// <param name="index">Index</param>
        /// <returns>Value of property with name propName for the object obj</returns>
        /// <remarks>
        /// This overload return an untyped result. Use it only when you do not know or do not need it.
        /// </remarks>
        public static object GetProp(this object obj, string propName, object index)
        {
            return GetMemberAux(obj, propName, MemberTypes.Property, index);
        }

        /// <summary>
        /// Sets a property of an object, given the property name
        /// </summary>
        /// <typeparam name="T">Type of the object</typeparam>
        /// <param name="obj">Object</param>
        /// <param name="propName">Name of the property</param>
        /// <param name="value">New value</param>
        /// <remarks></remarks>
        public static void SetProp<T>(this T obj, string propName, object value)
        {
            SetMemberAux(obj, propName, value, MemberTypes.Property);
        }

        /// <summary>
        /// Sets an indexed property of an object, given the property name
        /// </summary>
        /// <typeparam name="T">Type of the object</typeparam>
        /// <param name="obj">Object</param>
        /// <param name="propName">Name of the property</param>
        /// <param name="index">Index</param>
        /// <param name="value">New value</param>
        /// <remarks></remarks>
        public static void SetProp<T>(this T obj, string propName, object index, object value)
        {
            SetMemberAux(obj, propName, value, MemberTypes.Property, index);
        }

        /// <summary>
        /// Sets a double-indexed property of an object, given the property name
        /// </summary>
        /// <typeparam name="T">Type of the object</typeparam>
        /// <param name="obj">Object</param>
        /// <param name="propName">Name of the property</param>
        /// <param name="index1">First index</param>
        /// <param name="index2">Second index</param>
        /// <param name="value">New value</param>
        /// <remarks></remarks>
        public static void SetProp<T>(this T obj, string propName, object index1, object index2, object value)
        {
            SetMemberAux(obj, propName, value, MemberTypes.Property, 
                index1,
				index2);
        }


        /// <summary>
        /// Has the type a property with the given name
        /// </summary>
        /// <param name="propName"></param>
        /// <returns></returns>
        /// <remarks></remarks>
        public static bool HasProp(this object obj, string propName)
        {
            return obj?.GetType().GetProperty(propName) != null;
        }

        /// <summary>
        /// Is the type's property with the given name readable
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="propName"></param>
        /// <returns></returns>
        /// <remarks></remarks>
        public static bool IsPropReadable<T>(this T obj, string propName)
        {
            return IsPropReadable<T>(propName);
        }

        /// <summary>
        /// Is the type's property with the given name readable
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="propName"></param>
        /// <returns></returns>
        /// <remarks></remarks>
        public static bool IsPropWritable<T>(this T obj, string propName)
        {
            return IsPropWritable<T>(propName);
        }

        /// <summary>
        /// Has the type a property with the given name
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="propName"></param>
        /// <returns></returns>
        /// <remarks></remarks>
        public static bool TypeHasProp<T>(string propName)
        {
            var pi = typeof(T).GetProperty(propName);
            return pi != null;
        }

        /// <summary>
        /// Is the type's property with the given name readable
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="propName"></param>
        /// <returns></returns>
        /// <remarks></remarks>
        public static bool IsPropReadable<T>(string propName)
        {
            var pi = typeof(T).GetProperty(propName);
            if (pi == null)
                throw new ArgumentException(string.Format(My.Resources.Reflection.PropertyX0DoesNotExistInTypeX1, propName, typeof(T).Name), "propName");
            return pi.CanRead;
        }

        /// <summary>
        /// Is the type's property with the given name readable
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="propName"></param>
        /// <returns></returns>
        /// <remarks></remarks>
        public static bool IsPropWritable<T>(string propName)
        {
            var pi = typeof(T).GetProperty(propName);
            if (pi == null)
                throw new ArgumentException(string.Format(My.Resources.Reflection.PropertyX0DoesNotExistInTypeX1, propName, typeof(T).Name), "propName");
            return pi.CanWrite;
        }

        /// <summary>
        /// Call the given Sub (C#: void function) with the given parameters.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="obj"></param>
        /// <param name="subName"></param>
        /// <param name="params"></param>
        /// <remarks></remarks>
        public static void CallSub<T>(this T obj, string subName, params object[] @params)
        {
            // TODO: Allow overloads. The same algorithm for method call matching
            // TODO: Allow shared subs
            if (obj == null)
                throw new ArgumentNullException("No se puede obtener un método de un objeto nulo");
            var mi = obj.GetType().GetMethod(subName);
            if (mi == null)
                throw new ArgumentException(string.Format("El método {0} no existe en el tipo {1}", subName, obj.GetType().Name));
            mi.Invoke(obj, @params);
        }

        /// <summary>
        /// Has obj the given method's name
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="obj"></param>
        /// <param name="subName"></param>
        /// <returns></returns>
        /// <remarks></remarks>
        public static bool HasMethod<T>(this T obj, string subName)
        {
            // TODO: Allow overloads. The same algorithm for method call matching
            if (obj == null)
                throw new ArgumentNullException("No se puede obtener un método de un objeto nulo");
            var mi = obj.GetType().GetMethod(subName);
            return mi == null;
        }

        /// <summary>
        /// Call the function with given name on an object
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="obj"></param>
        /// <param name="funcName"></param>
        /// <param name="params"></param>
        /// <returns></returns>
        /// <remarks></remarks>
        public static object CallFunc<T>(this T obj, string funcName, params object[] @params)
        {
            // TODO: Allow overloads. The same algorithm for method call matching
            // TODO: Allow shared funcs
            if (obj == null)
                throw new ArgumentNullException("Cannot obtain a method from a null object");
            var mi = obj.GetType().GetMethod(funcName);
            if (mi == null)
                throw new ArgumentException(string.Format("Method {0} does not exist in type {1}", funcName, obj.GetType().Name));
            return mi.Invoke(obj, @params);
        }


        /// <summary>
        /// Gets a property value of an object, given the property name
        /// </summary>
        /// <typeparam name="TProp">Type of the property</typeparam>
        /// <param name="obj">Object</param>
        /// <param name="propName">Name of the property</param>
        /// <returns></returns>
        /// <remarks></remarks>
        public static TProp GetMember<TProp>(this object obj, string propName)
        {
            return (TProp)GetMemberAux(obj, propName, default(MemberTypes));
        }

        /// <summary>
        /// Gets a property value of an object, given the property name
        /// </summary>
        /// <typeparam name="TProp">Type of the property</typeparam>
        /// <param name="obj">Object</param>
        /// <param name="propName">Name of the property</param>
        /// <returns></returns>
        /// <remarks></remarks>
        public static TProp GetMember<TProp>(this object obj, string propName, params object[] args)
        {
            return (TProp)GetMemberAux(obj, propName, MemberTypes.All, args);
        }

        /// <summary>
        /// Gets an indexed property value of an object, given the property name
        /// </summary>
        /// <param name="obj">Object</param>
        /// <param name="propName">Name of the property</param>
        /// <param name="args">Index</param>
        /// <returns>Value of property with name propName for the object obj</returns>
        /// <remarks>
        /// This overload return an untyped result. Use it only when you do not know or do not need it.
        /// </remarks>
        public static object GetMember(this object obj, string propName, params object[] args)
        {
            return GetMemberAux(obj, propName, MemberTypes.All, args);
        }

        /// <summary>
        /// Sets a member of an object, given a call chain on another object that ends up in that member.
        /// </summary>
        /// <typeparam name="TMember">Type of the object</typeparam>
        /// <param name="obj">Object</param>
        /// <param name="callChain">Name of the field</param>
        /// <param name="value">New value</param>
        /// <remarks></remarks>
        public static void SetMember<TMember>(TMember obj, string callChain, object value, params object[] args)
        {
            SetMemberAux(obj, callChain, value, MemberTypes.All, args);
        }

        private static void SetMemberAux(object obj, string propName, object value, MemberTypes reqMemberType, params object[] args)
        {
            if (obj == null)
                throw new ArgumentNullException("obj");

            var callChain = propName.Split('.');

            var result = callChain
                .Take(callChain.Length - 1)
                .Aggregate(obj, (current, callItem) => GetSingleMember(current, callItem, MemberTypes.All));
            // For the last call item, use the args
            SetSingleMember(result, callChain.Last(), value, args);
        }

        private static object GetMemberAux(object obj, string propName, MemberTypes reqMemberType, params object[] args)
        {
            if (obj == null)
                throw new ArgumentNullException("obj");

            var callChain = propName.Split('.');

            var result = callChain
                .Take(callChain.Length - 1)
                .Aggregate(obj, (current, callItem) => GetSingleMemberWithArrayAccess(current, callItem, MemberTypes.All));
            // For the last call item, use the args
            return GetSingleMemberWithArrayAccess(result, callChain.Last(), reqMemberType, args);
        }

        private static object GetSingleMemberWithArrayAccess(object obj, string callItem, MemberTypes reqMemberType, params object[] args)
        {
            if (callItem.EndsWith("]"))
            {
                var idxOpen = callItem.IndexOf("[");
                var callItemClean = callItem.Substring(0, idxOpen);
                var index = callItem.Substring(idxOpen + 1, callItem.Length - idxOpen - 2);


                if (reqMemberType.HasFlag(MemberTypes.Property))
                {
                    var piCallItem = obj.GetType().GetProperty(callItemClean);

                    if (piCallItem != null)
                    {
                        var indices = index.Split(',');
                        var propParams = piCallItem.GetIndexParameters();
                        if (propParams.Length == indices.Length)
                        {
                            var zip = propParams.Zip(
                                indices, 
                                (parinfo, idx) => Convert.ChangeType(idx.Trim(), parinfo.ParameterType));

                            return GetSingleMember(obj, callItemClean, MemberTypes.Property, zip.ToArray());
                        }
                    }
                }

                var result = GetSingleMember(obj, callItemClean, reqMemberType, args);

                if (result == null)
                {
                    throw new NullReferenceException("[] accessor on null reference");
                }

                var type = result.GetType();
                // Case 1: Array
                if (type.IsArray)
                {
                    return GetSingleMember(result, "GetValue", MemberTypes.Method, Convert.ToInt32(index));
                }
                // Case 2: Default property
                var defPropAttr = type.GetAttribute<DefaultMemberAttribute>(inherit: true);
                if (defPropAttr != null)
                {
                    var propName = defPropAttr.MemberName;
                    var defProp = type.GetProperty(propName);
                    var propParams = defProp.GetIndexParameters();
                    var indices = index.Split(',');

                    IEnumerable<object> zip = null;

                    zip = propParams.Zip(indices, (parinfo, idx) => Convert.ChangeType(idx.Trim(), parinfo.ParameterType));


                    return GetSingleMember(result, propName, MemberTypes.Property, zip.ToArray());
                }
                // Case 3: IEnumerable
                var list = result as IEnumerable;
                if (list != null)
                {
                    return list.Cast<object>().ElementAt(Convert.ToInt32(index));
                }
                throw new ArgumentException($"Cannot apply [] operator to {callItemClean}");
            }
            else
            {
                return GetSingleMember(obj, callItem, reqMemberType, args);
            }
        }

        private static object GetSingleMember(object obj, string callItem, MemberTypes reqMemberType, params object[] args)
        {

            try
            {
                if (reqMemberType.HasFlag(MemberTypes.Property))
                {
                    var pi = obj.GetType().GetProperty(callItem);
                    if (pi != null)
                    {
                        return pi.GetValue(obj, args);
                    }
                }

                if (reqMemberType.HasFlag(MemberTypes.Field))
                {
                    var fi = obj.GetType().GetField(callItem);
                    if (fi != null)
                    {
                        return fi.GetValue(obj);
                    }
                }

                if (reqMemberType.HasFlag(MemberTypes.Method))
                {
                    MethodInfo fni;
                    if (args == null || !args.Any())
                    {
                        fni = obj.GetType().GetMethod(callItem, new Type[0]);
                    }
                    else
                    {
                        fni = obj.GetType().GetMethod(callItem, args.Select(arg => arg.GetType()).ToArray());
                    }

                    if (fni != null)
                    {
                        return fni.Invoke(obj, args);
                    }
                }
            }
            catch (TargetInvocationException ex)
            {
                if (ex.InnerException == null)
                {
                    throw;
                }
                else
                {
                    throw ex.InnerException;
                }
            }

            throw new ArgumentException(string.Format(My.Resources.Reflection.PropertyX0DoesNotExistInTypeX1, callItem, obj.GetType().Name), "propName");
        }

        private static void SetSingleMember(object result, string callItem, object value, params object[] args)
        {
            var pi = result.GetType().GetProperty(callItem);
            if (pi != null)
            {
                var targetType = pi.PropertyType.IsNullable() ? Nullable.GetUnderlyingType(pi.PropertyType) : pi.PropertyType;
                object convertedValue = null;
                if (targetType.IsEnum)
                {
                    convertedValue = Enum.ToObject(targetType, value);
                }
                else
                {
                    convertedValue = value == null ? null : Convert.ChangeType(value, targetType);
                }
                pi.SetValue(result, convertedValue, args);
                return;
            }
            var fi = result.GetType().GetField(callItem);
            if (fi != null)
            {
                var targetType = fi.FieldType.IsNullable() ? Nullable.GetUnderlyingType(fi.FieldType) : fi.FieldType;
                object convertedValue = null;
                if (targetType.IsEnum)
                {
                    convertedValue = Enum.ToObject(targetType, value);
                }
                else
                {
                    convertedValue = value == null ? null : Convert.ChangeType(value, targetType);
                }
                fi.SetValue(result, convertedValue);
                return;
            }
            var fni = result.GetType().GetMethod(callItem);
            if (fni != null)
            {
                fni.Invoke(result, new[] { value }.Union(args).ToArray());
                return;
            }

            throw new ArgumentException(string.Format(My.Resources.Reflection.PropertyX0DoesNotExistInTypeX1, callItem, result.GetType().Name), "propName");
        }


    }
}
