using Microsoft.VisualBasic;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using Icm.Collections.Generic.StructKeyStructValue;

namespace Icm.Functions
{
    public interface IKeyedMathFunction<TX, TY> : IMathFunction<TX, TY> where TX : struct, IComparable<TX> where TY : struct, IComparable<TY>
    {

        RangeIterator<TX, TY> Range(TX rangeStart, TX rangeEnd, bool includeExtremes);
        RangeIterator<TX, TY> RangeFrom(TX rangeStart, bool includeExtremes);
        RangeIterator<TX, TY> RangeTo(TX rangeEnd, bool includeExtremes);
        RangeIterator<TX, TY> TotalRange(bool includeExtremes);
        TX? PreviousOrLst(TX d);
        ISortedCollection<TX, TY> KeyStore { get; }
    }
}
