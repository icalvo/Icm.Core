using System;
using System.Collections.Generic;

namespace Icm.Collections.Generic.StructKeyStructValue
{

    /// <summary>
    /// Represents a generic sorted list of elements.
    /// </summary>
    /// <typeparam name="TKey"></typeparam>
    /// <typeparam name="TValue"></typeparam>
    /// <remarks>
    /// <para>In order to easily implement advanced storing schemes (a buckets system against
    /// a database for example), it is mandatory not to reference an underlying indexing system
    /// that associates consecutive integer numbers to the elements of the collection, something
    /// happening with IDictionary through the properties Keys and Values.</para>
    /// <para>Nevertheless, integer indexing is a very useful method for navigating these
    /// structure, so advanced alternatie methods must be provided. </para>
    /// </remarks>
    public interface ISortedCollection<TKey, TValue> where TKey : struct, IComparable<TKey> where TValue : struct
    {

        TKey? GetFreeKey(TKey desiredKey);
        /// <summary>
        /// Devuelve una representación de cadena de un intervalo de la línea de tiempo.
        /// Para imprimir los elementos se recurrirá a la función ToString.
        /// </summary>
        /// <param name="fromKey">Fecha inicial.</param>
        /// <param name="toKey">Fecha final.</param>
        /// <returns></returns>
        /// <remarks></remarks>
        string ToString(TKey fromKey, TKey toKey);
        string ToString();

        // Key navigation
        TKey? Next(TKey key);
        TKey? Previous(TKey key);
        TKey? KeyOrNext(TKey key);
        TKey? KeyOrPrev(TKey key);


        ITotalOrder<TKey> TotalOrder { get; }
        void Add(TKey key, TValue value);
        TValue this[TKey key] { get; set; }
        void Remove(TKey key);
        bool ContainsKey(TKey key);

        IEnumerable<Vector2<Tuple<TKey, TValue?>>> IntervalEnumerable(TKey? intStart, TKey? intEnd);
        IEnumerable<Vector2<Tuple<TKey, TValue?>>> IntervalEnumerable(Vector2<TKey?> intf);
        IEnumerable<Vector2<Tuple<TKey, TValue?>>> IntervalEnumerable();
        IEnumerable<Tuple<TKey, TValue?>> PointEnumerable(TKey? intStart, TKey? intEnd);
        IEnumerable<Tuple<TKey, TValue?>> PointEnumerable(Vector2<TKey?> intf);
        IEnumerable<Tuple<TKey, TValue?>> PointEnumerable();

        int Count();
    }

}
