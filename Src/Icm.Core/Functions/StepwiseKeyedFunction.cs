using Microsoft.VisualBasic;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using Icm.Collections.Generic.StructKeyStructValue;

namespace Icm.Functions
{

    /// <summary>
    /// In a stepwise function, non-keyed values are equal to the value of the former key.
    /// There is always value because the constructor requires the value of TX infimum.
    /// </summary>
    /// <remarks></remarks>
    public class StepwiseKeyedFunction<TX, TY> : KeyedMathFunction<TX, TY> where TX : struct, IComparable<TX> where TY : struct, IComparable<TY>
    {

        public StepwiseKeyedFunction(TY initialValue, ITotalOrder<TX> otx, ITotalOrder<TY> oty, ISortedCollection<TX, TY> coll) : base(initialValue, otx, oty, coll)
        {
        }

        public override IMathFunction<TX, TY> EmptyClone()
        {
            return new StepwiseKeyedFunction<TX, TY>(V0(), TotalOrderTX, TotalOrderTY, KeyStore);
        }

        public override TY this[TX d] => KeyStore[KeyStore.KeyOrPrev(d).Value];

        public override TX? MinX(TX rangeStart, TX rangeEnd, ThresholdType tumbral, TY cantidad)
        {
            throw new NotImplementedException();
            // UNDONE: Implementar
            // TODO: Reimplementar
            //Dim result As FunctionPoint(Of TX, TY)

            //result = New FunctionPoint(Of TX, TY)(LstX, Me)

            //If Compare(V(rangeStart), tumbral, cantidad) Then
            //    result.X = rangeStart
            //End If
            //Dim idxfecha As Integer

            //idxfecha = KeyStore.IndexOfNextKey(rangeStart)
            //If idxfecha < KeyStore.Count Then
            //    Dim x As TX
            //    x = KeyStore.Keys(idxfecha)
            //    Do Until x.CompareTo(rangeEnd) >= 0 OrElse idxfecha = KeyStore.Count

            //        If Compare(V(x), tumbral, cantidad) Then
            //            If V(x).CompareTo(result.Y) < 0 Then
            //                result.X = x
            //            End If
            //        End If

            //        idxfecha += 1
            //        x = KeyStore.Keys(idxfecha)
            //    Loop
            //End If

            //Return result.X
        }

        public override FunctionPoint<TX, TY> MinXY(TX rangeStart, TX rangeEnd, ThresholdType tumbral, TY cantidad)
        {
            throw new NotImplementedException();
            // UNDONE: Implementar
            //Dim result As FunctionPoint(Of TX, TY)

            //result = New FunctionPoint(Of TX, TY)(LstX, Me)

            //If Compare(V(rangeStart), tumbral, cantidad) Then
            //    result.X = rangeStart
            //End If
            //Dim idxfecha As Integer

            //' TODO: Falta quedarse con el par (X, umbral) en caso de que lo crucemos y el umbral sea inferior
            //idxfecha = KeyStore.IndexOfNextKey(rangeStart)
            //If idxfecha < KeyStore.Count Then
            //    Dim x As TX
            //    x = KeyStore.Keys(idxfecha)
            //    Do Until x.CompareTo(rangeEnd) >= 0 OrElse idxfecha = KeyStore.Count

            //        If Compare(V(x), tumbral, cantidad) Then
            //            If V(x).CompareTo(result.Y) < 0 Then
            //                result.X = x
            //            End If
            //        End If

            //        idxfecha += 1
            //        x = KeyStore.Keys.ElementAtOrDefault(idxfecha)
            //    Loop
            //End If

            //Return result
        }

        public override FunctionPoint<TX, TY> MinXYu(TX rangeStart, TX rangeEnd, ThresholdType tumbral, Func<TX, TY> fnUmbral)
        {
            throw new NotImplementedException("FunciónClavesEscalonada.MínXY no implementado aún");
            // UNDONE: Implementar
        }

        public override FunctionPoint<TX, TY> MaxXY(TX rangeStart, TX rangeEnd, ThresholdType tumbral, TY cantidad)
        {
            throw new NotImplementedException();
            // UNDONE: Implementar
            //Dim result As FunctionPoint(Of TX, TY)

            //result = New FunctionPoint(Of TX, TY)(LstX, Me)

            //Dim idxfecha As Integer

            //idxfecha = KeyStore.IndexOfKeyOrNext(rangeStart)
            //If idxfecha < KeyStore.Count Then
            //    Dim x As TX
            //    Do Until x.CompareTo(rangeEnd) >= 0 OrElse idxfecha = KeyStore.Count
            //        Dim y As TY
            //        y = KeyStore(x)
            //        If Not Compare(y, tumbral, cantidad) Then
            //            y = cantidad
            //        End If

            //        If y.CompareTo(result.Y) < 0 Then
            //            result.X = x
            //        End If

            //        Select Case tumbral

            //        End Select
            //        idxfecha += 1
            //        x = KeyStore.ElementAtOrDefault(idxfecha).Key
            //    Loop
            //End If

            //Return result
        }

        public override FunctionPoint<TX, TY> MaxXYu(TX rangeStart, TX rangeEnd, ThresholdType tumbral, Func<TX, TY> fnUmbral)
        {
            throw new NotImplementedException("FunciónClavesEscalonada.MáxXY no implementado aún");
            // UNDONE: Implementar
        }

        public override FunctionPoint<TX, TY> FstXY(TX rangeStart, TX rangeEnd, ThresholdType tumbral, TY cantidad)
        {
            throw new NotImplementedException();
            // UNDONE: Implementar
            //Dim xy As New FunctionPoint(Of TX, TY)(rangeStart, Me)

            //xy.X = rangeStart
            //If Compare(xy.Y, tumbral, cantidad) Then
            //    Return xy
            //End If

            //Dim idx As Integer = KeyStore.IndexOfNextKey(rangeStart)
            //Dim encontrado As Boolean = False
            //Do Until idx = KeyStore.Count OrElse xy.X.CompareTo(rangeEnd) > 0
            //    xy.X = KeyStore.Keys(idx)
            //    If Compare(xy.Y, tumbral, cantidad) Then
            //        encontrado = True
            //        Exit Do
            //    End If
            //    idx += 1
            //Loop

            //If Not encontrado Then
            //    xy.X = rangeEnd
            //End If

            //If xy.X.Equals(GstX()) Then
            //    Return Nothing
            //Else
            //    Return xy
            //End If
        }


        public override FunctionPoint<TX, TY> FstXYu(TX rangeStart, TX rangeEnd, ThresholdType tumbral, Func<TX, TY> fnUmbral)
        {
            throw new NotImplementedException();
            // UNDONE: Implementar
            //Dim xy As New FunctionPoint(Of TX, TY)(rangeStart, Me)
            //xy.X = rangeStart
            //If Compare(xy.Y, tumbral, fnUmbral(xy.X)) Then
            //    Return xy
            //End If

            //Dim idx As Integer = KeyStore.IndexOfNextKey(rangeStart)
            //Dim encontrado As Boolean = False
            //Do Until idx = KeyStore.Count OrElse xy.X.CompareTo(rangeEnd) > 0
            //    xy.X = KeyStore.Keys(idx)
            //    If Compare(xy.Y, tumbral, fnUmbral(xy.X)) Then
            //        encontrado = True
            //        Exit Do
            //    End If
            //    idx += 1
            //Loop

            //If Not encontrado Then
            //    xy.X = rangeEnd
            //End If

            //If xy.X.Equals(GstX()) Then
            //    Return Nothing
            //Else
            //    Return xy
            //End If
        }

        public override FunctionPoint<TX, TY> LstXY(TX rangeStart, TX rangeEnd, ThresholdType tumbral, TY cantidad)
        {
            throw new NotImplementedException("FunciónClavesEscalonada.ÚltXY no implementado aún");
            // UNDONE: Implementar
        }

        public override FunctionPoint<TX, TY> LstXYu(TX rangeStart, TX rangeEnd, ThresholdType tumbral, Func<TX, TY> fnUmbral)
        {
            throw new NotImplementedException("FunciónClavesEscalonada.ÚltXY no implementado aún");
            // UNDONE: Implementar
        }

    }

}
