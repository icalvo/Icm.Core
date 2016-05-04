using Microsoft.VisualBasic;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Runtime.CompilerServices;

namespace Icm.Collections.Generic
{
    public static class SortedListExtensions
    {

        /// <summary>
        /// Enhanced "Item" method that prints the key in the exception message.
        /// </summary>
        /// <typeparam name="TKey"></typeparam>
        /// <typeparam name="TValue"></typeparam>
        /// <param name="dic"></param>
        /// <param name="key"></param>
        /// <returns></returns>
        /// <remarks></remarks>
        public static TValue It<TKey, TValue>(this IDictionary<TKey, TValue> dic, TKey key)
        {
            if (dic.ContainsKey(key))
            {
                return dic[key];
            }
            else
            {
                throw new ArgumentException($"The dictionary does not contain key [{key}]");
            }
        }

        /// <summary>
        ///  Returns the key or, if the key does not exist, the index in which the key would be inserted
        /// if you add it to the list. It can also be interpreted as the index of the next greater key on the list.
        /// </summary>
        /// <typeparam name="TKey">Type of keys</typeparam>
        /// <typeparam name="TValue">Type of values</typeparam>
        /// <param name="list">Sorted list</param>
        /// <param name="key">Searched key</param>
        /// <returns></returns>
        /// <remarks>Result will be list.Count if the key is greater than the greatest key of the list.</remarks>
        public static TKey? KeyOrNext<TKey, TValue>(this SortedList<TKey, TValue> list, TKey key) where TKey : struct
        {
            int index = 0;

            index = list.IndexOfKeyOrNext(key);

            if (index < list.Count)
            {
                return list.Keys[index];
            }
            else
            {
                return null;
            }
        }

        /// <summary>
        /// Returns the next key. Never returns the same key if it already exists. Returns Nothing if there
        /// isn't next key.
        /// </summary>
        /// <typeparam name="TKey"></typeparam>
        /// <typeparam name="TValue"></typeparam>
        /// <param name="list"></param>
        /// <param name="key"></param>
        /// <returns></returns>
        /// <remarks></remarks>
        public static TKey? NextKey<TKey, TValue>(this SortedList<TKey, TValue> list, TKey key) where TKey : struct
        {
            int index = 0;
            index = list.IndexOfNextKey(key);

            if (index < list.Count)
            {
                return list.Keys[index];
            }
            else
            {
                return null;
            }
        }
        
        public static Nullable2<TKey> NextKey2<TKey, TValue>(this SortedList<TKey, TValue> list, TKey key)
        {
            int index = 0;
            index = list.IndexOfNextKey(key);

            if (index < list.Count)
            {
                return list.Keys[index];
            }
            else
            {
                return default(Nullable2<TKey>);
            }
        }

        /// <summary>
        /// Returns the previous key. Never returns the same key if it already exists. Returns Nothing if there
        /// isn't previous key.
        /// </summary>
        /// <typeparam name="TKey"></typeparam>
        /// <typeparam name="TValue"></typeparam>
        /// <param name="list"></param>
        /// <param name="key"></param>
        /// <returns></returns>
        /// <remarks></remarks>
        public static TKey? PrevKey<TKey, TValue>(this SortedList<TKey, TValue> list, TKey key) where TKey : struct
        {
            int index = 0;
            index = list.IndexOfPrevKey(key);

            if (index >= 0)
            {
                return list.Keys[index];
            }
            else
            {
                return null;
            }
        }

        /// <summary>
        /// Returns the previous key, but NOT the same key if it already exists. Returns Nothing if there
        /// isn't previous key. Returns a <see cref="Nullable2(Of T)"></see>.
        /// </summary>
        /// <typeparam name="TKey"></typeparam>
        /// <typeparam name="TValue"></typeparam>
        /// <param name="list"></param>
        /// <param name="key"></param>
        /// <returns></returns>
        /// <remarks></remarks>
        public static Nullable2<TKey> PrevKey2<TKey, TValue>(this SortedList<TKey, TValue> list, TKey key)
        {
            int index = 0;
            index = list.IndexOfPrevKey(key);

            if (index >= 0)
            {
                return list.Keys[index];
            }
            else
            {
                return default(Nullable2<TKey>);
            }
        }

        /// <summary>
        ///  Returns the key or, if the key does not exist, the index of the former lesser key on the list.
        /// </summary>
        /// <typeparam name="TKey">Type of keys</typeparam>
        /// <typeparam name="TValue">Type of values</typeparam>
        /// <param name="list">Sorted list</param>
        /// <param name="key">Searched key</param>
        /// <returns></returns>
        /// <remarks>Result will be -1 if the key is less than the least key of the list.</remarks>
        public static TKey? KeyOrPrev<TKey, TValue>(this SortedList<TKey, TValue> list, TKey key) where TKey : struct
        {
            int index = 0;

            index = list.IndexOfKeyOrPrev(key);

            if (index >= 0)
            {
                return list.Keys[index];
            }
            else
            {
                return null;
            }
        }

        /// <summary>
        ///  Returns the index of the key or, if the key does not exist, the index in which the key would be inserted
        /// if you add it to the list. It can also be interpreted as the index of the next greater or equal key on the list.
        /// </summary>
        /// <typeparam name="TKey">Type of keys</typeparam>
        /// <typeparam name="TValue">Type of values</typeparam>
        /// <param name="list">Sorted list</param>
        /// <param name="key">Searched key</param>
        /// <returns></returns>
        /// <remarks>Result will be list.Count if the key is greater than the greatest key of the list.</remarks>
        public static int IndexOfKeyOrNext<TKey, TValue>(this SortedList<TKey, TValue> list, TKey key)
        {
            int index = 0;

            index = list.Keys.Search(key);

            if (index >= 0)
            {
                return index;
            }
            else
            {
                return (index ^ -1);
            }
        }


        /// <summary>
        ///  Returns the index of the next key. It can also be interpreted as the index of the next greater key on the list.
        /// </summary>
        /// <typeparam name="TKey">Type of keys</typeparam>
        /// <typeparam name="TValue">Type of values</typeparam>
        /// <param name="list">Sorted list</param>
        /// <param name="key">Searched key</param>
        /// <returns></returns>
        /// <remarks>Result will be list.Count if the key is greater than the greatest key of the list.</remarks>
        public static int IndexOfNextKey<TKey, TValue>(this SortedList<TKey, TValue> list, TKey key)
        {
            int index = 0;

            index = list.Keys.Search(key);

            if (index >= 0)
            {
                return index + 1;
            }
            else
            {
                return (index ^ -1);
            }
        }

        /// <summary>
        ///  Returns the index of the key or, if the key does not exist, the index of the former lesser key on the list.
        /// </summary>
        /// <typeparam name="TKey">Type of keys</typeparam>
        /// <typeparam name="TValue">Type of values</typeparam>
        /// <param name="list">Sorted list</param>
        /// <param name="key">Searched key</param>
        /// <returns></returns>
        /// <remarks>Result will be -1 if the key is less than the least key of the list.</remarks>
        public static int IndexOfKeyOrPrev<TKey, TValue>(this SortedList<TKey, TValue> list, TKey key)
        {
            int index = 0;

            index = list.Keys.Search(key);

            if (index >= 0)
            {
                return index;
            }
            else
            {
                return (index ^ -1) - 1;
            }
        }

        /// <summary>
        ///  Returns the index of the previous key. It can also be interpreted as the index of the former lesser key on the list.
        /// </summary>
        /// <typeparam name="TKey">Type of keys</typeparam>
        /// <typeparam name="TValue">Type of values</typeparam>
        /// <param name="list">Sorted list</param>
        /// <param name="key">Searched key</param>
        /// <returns></returns>
        /// <remarks>Result will be -1 if the key is less than the least key of the list.</remarks>
        public static int IndexOfPrevKey<TKey, TValue>(this SortedList<TKey, TValue> list, TKey key)
        {
            int index = 0;

            index = list.Keys.Search(key);

            if (index >= 0)
            {
                return index - 1;
            }
            else
            {
                return (index ^ -1) - 1;
            }
        }

        /// <summary>
        ///  Returns the value at the key or, if the key does not exist, the value of the former element on the list.
        /// </summary>
        /// <typeparam name="TKey">Type of keys</typeparam>
        /// <typeparam name="TValue">Type of values</typeparam>
        /// <param name="list">Sorted list</param>
        /// <param name="key">Searched key</param>
        /// <returns></returns>
        /// <remarks>Result will be Nothing if the key is less than the least key of the list.</remarks>
        public static TValue ValueOfKeyOrPrev<TKey, TValue>(this SortedList<TKey, TValue> list, TKey key)
        {
            int index = 0;

            index = list.IndexOfKeyOrPrev(key);

            if (index >= 0)
            {
                return list.Values[index];
            }
            else
            {
                return default(TValue);
            }
        }

        /// <summary>
        ///  Returns the value at the key or, if the key does not exist, the value of the next element on the list.
        /// </summary>
        /// <typeparam name="TKey">Type of keys</typeparam>
        /// <typeparam name="TValue">Type of values</typeparam>
        /// <param name="list">Sorted list</param>
        /// <param name="key">Searched key</param>
        /// <returns></returns>
        /// <remarks>Result will be null (Nothing) if the key is greater than the greatest key of the list.</remarks>
        public static TValue ValueOfKeyOrNext<TKey, TValue>(this SortedList<TKey, TValue> list, TKey key)
        {
            int index = 0;

            index = list.IndexOfKeyOrNext(key);

            if (index >= 0)
            {
                return list.Values[index];
            }
            else
            {
                return default(TValue);
            }
        }

        /// <summary>
        ///  Returns the key or, if the key does not exist, the index in which the key would be inserted
        /// if you add it to the list. It can also be interpreted as the index of the next greater key on the list.
        /// </summary>
        /// <typeparam name="TKey">Type of keys</typeparam>
        /// <typeparam name="TValue">Type of values</typeparam>
        /// <param name="list">Sorted list</param>
        /// <param name="key">Searched key</param>
        /// <returns></returns>
        /// <remarks>Result will be list.Count if the key is greater than the greatest key of the list.</remarks>
        public static TKey KeyOrNext<TKey, TValue>(this SortedList<TKey, TValue> list, TKey key, Func<TKey, TValue, bool> condition)
        {
            int index = 0;

            index = list.IndexOfKeyOrNext(key);
            while (!(index >= list.Count || condition(list.Keys[index], list.Values[index])))
            {
                index += 1;
            }

            if (index >= 0 || index <= list.Count)
            {
                return list.Keys[index];
            }
            else
            {
                return default(TKey);
            }
        }

        /// <summary>
        ///  Returns the key or, if the key does not exist, the index of the former lesser key on the list.
        /// </summary>
        /// <typeparam name="TKey">Type of keys</typeparam>
        /// <typeparam name="TValue">Type of values</typeparam>
        /// <param name="list">Sorted list</param>
        /// <param name="key">Searched key</param>
        /// <returns></returns>
        /// <remarks>Result will be -1 if the key is less than the least key of the list.</remarks>
        public static TKey KeyOrPrev<TKey, TValue>(this SortedList<TKey, TValue> list, TKey key, Func<TKey, TValue, bool> condition)
        {
            int index = 0;

            index = list.IndexOfKeyOrPrev(key);
            while (!(index >= list.Count || condition(list.Keys[index], list.Values[index])))
            {
                index -= 1;
            }

            if (index >= 0 || index <= list.Count)
            {
                return list.Keys[index];
            }
            else
            {
                return default(TKey);
            }
        }

        /// <summary>
        ///  Returns the index of the key or, if the key does not exist, the index in which the key would be inserted
        /// if you add it to the list. It can also be interpreted as the index of the next greater key on the list.
        /// </summary>
        /// <typeparam name="TKey">Type of keys</typeparam>
        /// <typeparam name="TValue">Type of values</typeparam>
        /// <param name="list">Sorted list</param>
        /// <param name="key">Searched key</param>
        /// <returns></returns>
        /// <remarks>Result will be list.Count if the key is greater than the greatest key of the list.</remarks>
        public static int IndexOfKeyOrNext<TKey, TValue>(this SortedList<TKey, TValue> list, TKey key, Func<TKey, TValue, bool> condition)
        {
            int index = 0;

            index = list.IndexOfKeyOrNext(key);
            while (!(index >= list.Count || condition(list.Keys[index], list.Values[index])))
            {
                index += 1;
            }

            if (index >= 0 || index <= list.Count)
            {
                return index;
            }
            else
            {
                return 0;
            }
        }

        /// <summary>
        ///  Returns the index of the key or, if the key does not exist, the index of the former lesser key on the list.
        /// </summary>
        /// <typeparam name="TKey">Type of keys</typeparam>
        /// <typeparam name="TValue">Type of values</typeparam>
        /// <param name="list">Sorted list</param>
        /// <param name="key">Searched key</param>
        /// <returns></returns>
        /// <remarks>Result will be -1 if the key is less than the least key of the list.</remarks>
        public static int IndexOfKeyOrPrev<TKey, TValue>(this SortedList<TKey, TValue> list, TKey key, Func<TKey, TValue, bool> condition)
        {
            int index = 0;

            index = list.IndexOfKeyOrPrev(key);
            while (!(index >= list.Count || condition(list.Keys[index], list.Values[index])))
            {
                index -= 1;
            }

            if (index >= 0 || index <= list.Count)
            {
                return index;
            }
            else
            {
                return 0;
            }
        }

        /// <summary>
        ///  Returns the value at the key or, if the key does not exist, the value of the former element on the list.
        /// </summary>
        /// <typeparam name="TKey">Type of keys</typeparam>
        /// <typeparam name="TValue">Type of values</typeparam>
        /// <param name="list">Sorted list</param>
        /// <param name="key">Searched key</param>
        /// <param name="condition"></param>
        /// <returns></returns>
        /// <remarks>Result will be Nothing if the key is less than the least key of the list.</remarks>
        public static TValue ValueOfKeyOrPrev<TKey, TValue>(this SortedList<TKey, TValue> list, TKey key, Func<TKey, TValue, bool> condition)
        {
            int index = 0;

            index = list.IndexOfKeyOrPrev(key);
            while (!(index >= list.Count || condition(list.Keys[index], list.Values[index])))
            {
                index -= 1;
            }

            if (index >= 0 || index <= list.Count)
            {
                return list.Values[index];
            }
            else
            {
                return default(TValue);
            }
        }

        /// <summary>
        ///  Returns the value at the key or, if the key does not exist, the value of the next element on the list.
        /// </summary>
        /// <typeparam name="TKey">Type of keys</typeparam>
        /// <typeparam name="TValue">Type of values</typeparam>
        /// <param name="list">Sorted list</param>
        /// <param name="key">Searched key</param>
        /// <param name="condition"></param>
        /// <returns></returns>
        /// <remarks>Result will be null (Nothing) if the key is greater than the greatest key of the list.</remarks>
        public static TValue ValueOfKeyOrNext<TKey, TValue>(this SortedList<TKey, TValue> list, TKey key, Func<TKey, TValue, bool> condition)
        {
            int index = 0;

            index = list.IndexOfKeyOrNext(key);
            while (!(index >= list.Count || condition(list.Keys[index], list.Values[index])))
            {
                index += 1;
            }

            if (index >= 0 || index <= list.Count)
            {
                return list.Values[index];
            }
            else
            {
                return default(TValue);
            }
        }

        /// <summary>
        /// Adds or modifies a value associated with a key.
        /// </summary>
        /// <typeparam name="TKey"></typeparam>
        /// <typeparam name="TValue"></typeparam>
        /// <param name="list"></param>
        /// <param name="key"></param>
        /// <param name="value"></param>
        /// <remarks>
        /// <para>Despite what is specified in IDictionary.Item, SortedList throws an exception if the key
        /// does not exist, instead of adding a new element. So this function recovers the original
        /// semantics.</para>
        /// </remarks>
        public static void ForceAdd<TKey, TValue>(this SortedList<TKey, TValue> list, TKey key, TValue value)
        {
            if (!list.ContainsKey(key))
            {
                list.Add(key, value);
            }
            else
            {
                list[key] = value;
            }
        }
    }
}
