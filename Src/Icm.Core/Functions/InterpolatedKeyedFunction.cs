using System;
using System.Diagnostics;
using Icm.Collections.Generic.StructKeyStructValue;

namespace Icm.Functions
{

    /// <summary>
    /// In a stepwise function, non-keyed values are interpolated from the previous and
    /// next key points.
    /// Values between infimum and first key are equal to the first key value.
    /// Values between last key and supreme are equal to the last key value.
    /// </summary>
    /// <remarks></remarks>
    public class InterpolatedKeyedFunction<TX, TY> : KeyedMathFunction<TX, TY> where TX : struct, IComparable<TX> where TY : struct, IComparable<TY>
    {

        public InterpolatedKeyedFunction(TY initialValue, ITotalOrder<TX> otx, ITotalOrder<TY> oty, ISortedCollection<TX, TY> coll) : base(initialValue, otx, oty, coll)
        {
        }

        public override IMathFunction<TX, TY> EmptyClone()
        {
            return new InterpolatedKeyedFunction<TX, TY>(V0(), TotalOrderTX, TotalOrderTY, KeyStore);
        }

        public override TY this[TX d]
        {
            get
            {
                if (KeyStore.ContainsKey(d))
                {
                    return KeyStore[d];
                }

                TX baseX = default(TX);
                TY baseY = default(TY);
                baseX = KeyStore.KeyOrPrev(d).Value;
                baseY = KeyStore[KeyStore.KeyOrPrev(d).Value];

                var x1 = X2Long(baseX);
                var fx1 = Y2Long(baseY);

                var x2 = X2Long(d);

                var nextKey = KeyStore.KeyOrNext(d);
                if (!nextKey.HasValue)
                {
                    return Long2Y(fx1);
                }
                else
                {
                    long x3 = X2Long(nextKey.Value);
                    long fx3 = Y2Long(KeyStore[nextKey.Value]);

                    return Long2Y(Icm.MathTools.MathModule.LinearInterpolate(x1, x2, x3, fx1, fx3));
                }
            }
        }

        public override FunctionPoint<TX, TY> MinXY(TX rangeStart, TX rangeEnd, ThresholdType tumbral, TY cantidad)
        {
            RangeIterator<TX, TY> it = new RangeIterator<TX, TY>(this, rangeStart, rangeEnd, includeExtremes: true);
            FunctionPoint<TX, TY> result = null;
            foreach (var pp in it)
            {
                bool cumpleY1 = Compare(pp.Y1, tumbral, cantidad);
                bool cumpleY2 = Compare(pp.Y2, tumbral, cantidad);
                FunctionPoint<TX, TY> posibleM�n = null;
                if (cumpleY1 && cumpleY2)
                {
                    // Ambos extremos est�n dentro del umbral. El posible m�nimo es el que tenga menor Y.
                    if (pp.Y1.CompareTo(pp.Y2) <= 0)
                    {
                        posibleM�n = pp.P1;
                    }
                    else
                    {
                        posibleM�n = pp.P2;
                    }
                }
                else if (tumbral == ThresholdType.RightClosed || tumbral == ThresholdType.RightOpen)
                {
                    if (cumpleY1)
                    {
                        // El posible m�nimo es P1.
                        posibleM�n = pp.P1;
                    }
                    else if (cumpleY2)
                    {
                        // El posible m�nimo es P2.
                        posibleM�n = pp.P2;
                    }
                }
                else
                {
                    if (cumpleY1 || cumpleY2)
                    {
                        // El posible m�nimo es el cruce con el umbral.
                        posibleM�n = InvInterpolate(pp, cantidad);
                    }
                }

                // Si tenemos un posible m�nimo, lo comparamos con nuestro m�nimo actual
                if (posibleM�n.IsSomething() && (result == null || posibleM�n.Y.CompareTo(result.Y) < 0))
                {
                    result = posibleM�n;
                }
            }

            if (result.IsSomething())
            {
                Debug.Assert(Compare(result.Y, tumbral, cantidad));
            }

            return result;
        }

        public override FunctionPoint<TX, TY> MinXYu(TX rangeStart, TX rangeEnd, ThresholdType tumbral, Func<TX, TY> fnUmbral)
        {
            throw new NotImplementedException("Funci�nClavesInterpolada.M�nXY no implementado a�n");
            // UNDONE: Implementar
        }

        private FunctionPoint<TX, TY> InvInterpolate(FunctionPointPair<TX, TY> pp, TY cantidad)
        {
            var xy = new FunctionPoint<TX, TY>(GstX(), this);
            double x1 = X2Long(pp.X1);
            double x3 = X2Long(pp.X2);
            double fx1 = Y2Long(pp.Y1);
            double fx2 = Y2Long(cantidad);
            double fx3 = Y2Long(pp.Y2);
            xy.X = Long2X(Convert.ToInt64(Icm.MathTools.MathModule.InverseLinearInterpolate(x1, x3, fx1, fx2, fx3)));

            return xy;
        }

        public override FunctionPoint<TX, TY> MaxXY(TX rangeStart, TX rangeEnd, ThresholdType tumbral, TY cantidad)
        {
            RangeIterator<TX, TY> it = new RangeIterator<TX, TY>(this, rangeStart, rangeEnd, includeExtremes: true);
            FunctionPoint<TX, TY> result = null;
            FunctionPointPair<TX, TY> lastpp = null;
            foreach (var pp in it)
            {
                lastpp = pp;
                bool cumpleY1 = Compare(pp.Y1, tumbral, cantidad);
                bool cumpleY2 = Compare(pp.Y2, tumbral, cantidad);
                FunctionPoint<TX, TY> posibleM�x = null;
                if (cumpleY1 && cumpleY2)
                {
                    // Ambos extremos est�n dentro del umbral. El posible m�ximo es el que tenga mayor Y.
                    if (pp.Y1.CompareTo(pp.Y2) >= 0)
                    {
                        posibleM�x = pp.P1;
                    }
                    else
                    {
                        posibleM�x = pp.P2;
                    }
                }
                else if (tumbral == ThresholdType.LeftOpen || tumbral == ThresholdType.LeftClosed)
                {
                    if (cumpleY1)
                    {
                        // El posible m�ximo es P1.
                        posibleM�x = pp.P1;
                    }
                    else if (cumpleY2)
                    {
                        // El posible m�ximo es P2.
                        posibleM�x = pp.P2;
                    }
                }
                else
                {
                    if (cumpleY1 || cumpleY2)
                    {
                        // El posible m�ximo es el cruce con el umbral.
                        posibleM�x = InvInterpolate(pp, cantidad);
                    }
                }

                // Si tenemos un posible m�ximo, lo comparamos con nuestro m�ximo actual
                if (posibleM�x.IsSomething() && (result == null || posibleM�x.Y.CompareTo(result.Y) > 0))
                {
                    result = posibleM�x;
                }
            }

            if (result.IsSomething())
            {
                while (!(Compare(result.Y, tumbral, cantidad) || result.X.CompareTo(lastpp.X2) >= 0))
                {
                    result.X = TotalOrderTX.Next(result.X);
                }
                if (result.X.CompareTo(lastpp.X2) > 0)
                {
                    result.X = lastpp.X2;
                }
                Debug.Assert(Compare(result.Y, tumbral, cantidad));
            }

            return result;
        }

        public override FunctionPoint<TX, TY> MaxXYu(TX rangeStart, TX rangeEnd, ThresholdType tumbral, Func<TX, TY> fnUmbral)
        {
            throw new NotImplementedException("Funci�nClavesInterpolada.M�xXY no implementado a�n");
            // UNDONE: Implementar
        }

        public override FunctionPoint<TX, TY> FstXY(TX rangeStart, TX rangeEnd, ThresholdType tumbral, TY cantidad)
        {
            FunctionPoint<TX, TY> result = null;

            // Chequeamos antes con el m�ximo y el m�nimo absolutos
            if (Compare(AbsMaxXY().Y, tumbral, cantidad) && Compare(AbsMinXY().Y, tumbral, cantidad))
            {
                result = new FunctionPoint<TX, TY>(rangeStart, this);
            }
            else if (!Compare(AbsMaxXY().Y, tumbral, cantidad) && !Compare(AbsMinXY().Y, tumbral, cantidad))
            {
                result = null;
            }
            else
            {
                RangeIterator<TX, TY> it = new RangeIterator<TX, TY>(this, rangeStart, rangeEnd, includeExtremes: true);
                FunctionPointPair<TX, TY> lastpp = null;
                foreach (var pp in it)
                {
                    lastpp = pp;
                    if (Compare(pp.Y1, tumbral, cantidad))
                    {
                        result = pp.P1;
                        break; // TODO: might not be correct. Was : Exit For
                    }
                    else if (Compare(pp.Y2, tumbral, cantidad))
                    {
                        result = InvInterpolate(pp, cantidad);
                        break; // TODO: might not be correct. Was : Exit For
                    }
                }
                if (result.IsSomething())
                {
                    while (!(Compare(result.Y, tumbral, cantidad) || result.X.CompareTo(lastpp.X2) >= 0))
                    {
                        result.X = TotalOrderTX.Next(result.X);
                    }
                    if (result.X.CompareTo(lastpp.X2) > 0)
                    {
                        result.X = lastpp.X2;
                    }
                    Debug.Assert(Compare(result.Y, tumbral, cantidad));
                }
            }
            Debug.Assert(result == null || !result.X.Equals(GstX()));
            return result;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="rangeStart"></param>
        /// <param name="rangeEnd"></param>
        /// <param name="thrType"></param>
        /// <param name="fnThreshold"></param>
        /// <returns></returns>
        /// <remarks>La implementaci�n actual no es del todo exacta puesto que s�lo calcula el umbral en los
        /// extremos de la l�nea temporal.</remarks>
        public override FunctionPoint<TX, TY> FstXYu(TX rangeStart, TX rangeEnd, ThresholdType thrType, Func<TX, TY> fnThreshold)
        {
            RangeIterator<TX, TY> it = new RangeIterator<TX, TY>(this, rangeStart, rangeEnd, includeExtremes: true);
            FunctionPoint<TX, TY> result = null;

            foreach (var pp in it)
            {
                TY U1 = default(TY);
                TY U2 = default(TY);
                U1 = fnThreshold(pp.X1);
                U2 = fnThreshold(pp.X2);
                if (Compare(pp.Y1, thrType, U1))
                {
                    result = pp.P1;
                    break; // TODO: might not be correct. Was : Exit For
                }
                else if (Compare(pp.Y2, thrType, U2))
                {
                    TX cruceEnX = default(TX);

                    cruceEnX = Long2X(CruceX(X2Long(pp.X1), X2Long(pp.X2), Y2Long(pp.Y1), Y2Long(pp.Y2), Y2Long(U1), Y2Long(U2)));
                    result = new FunctionPoint<TX, TY>(cruceEnX, this);
                    break; // TODO: might not be correct. Was : Exit For
                }
            }
            if (result.IsSomething())
            {
                //Do Until Comparar(V(result.X), tumbral, fnUmbral(result.X))
                //result.X = OrdenTotalTX.AddDelta(result.X)
                //Loop

                //Debug.Assert(Comparar(result.Y, tumbral, fnUmbral(result.X)))
            }
            Debug.Assert(result == null || !result.X.Equals(GstX()));
            return result;
        }


        /// <summary>
        ///  �Se cruzan los segmentos A y B definidos por sus coordenadas
        /// </summary>
        /// <param name="x1">Coordenada X del primer punto de ambos segmentos</param>
        /// <param name="x2">Coordenada X del segundo punto de ambos segmentos</param>
        /// <param name="y1A">Coordenada Y del primer punto del segmento A</param>
        /// <param name="y2A">Coordenada Y del segundo punto del segmento A</param>
        /// <param name="y1B">Coordenada Y del primer punto del segmento B</param>
        /// <param name="y2B">Coordenada Y del segundo punto del segmento B</param>
        /// <returns></returns>
        /// <remarks></remarks>
        private static long CruceX(long x1, long x2, long y1A, long y2A, long y1B, long y2B)
        {
            var difY1 = Math.Abs(y1A - y1B);
            var difY2 = Math.Abs(y2A - y2B);
            var difX = Math.Abs(x1 - x2);
            var result = difY1 * difX / (difY1 + difY2);

            return result;

        }

        public override FunctionPoint<TX, TY> LstXY(TX rangeStart, TX rangeEnd, ThresholdType tumbral, TY cantidad)
        {
            //Calculo los puntos anterior y posterior al rangeStart que haya en la lista
            TX? key1 = default(TX);
            TX? key2 = default(TX);
            bool existeDesde = true;
            bool existeHasta = true;

            if (!KeyStore.ContainsKey(rangeStart))
            {
                existeDesde = false;
                KeyStore.Add(rangeStart, this[rangeStart]);
            }

            if (!KeyStore.ContainsKey(rangeEnd))
            {
                existeHasta = false;
                KeyStore.Add(rangeEnd, this[rangeEnd]);
            }
            key1 = rangeEnd;
            key2 = KeyStore.Previous(rangeEnd).Value;

            FunctionPoint<TX, TY> xy = new FunctionPoint<TX, TY>(rangeStart, this);

            //Si el valor f(rangeStart) est� en el umbral ya he terminado porque ese ser� la x m�s peque�a que cumpla la condici�n

            if (Compare(KeyStore[key1.Value], tumbral, cantidad))
            {
                xy.X = rangeStart;
                if (!existeDesde)
                {
                    KeyStore.Remove(rangeStart);
                }
                if (!existeHasta)
                {
                    KeyStore.Remove(rangeEnd);
                }
                return xy;
            }
            //Si no es as� tomo el punto x=rangeStart y=f(rangeStart) como punto inicial para la interpolaci�n

            while (!((!key2.HasValue || key2.Value.CompareTo(rangeEnd) == 0)))
            {
                //Ya s� que mi primer punto no est� en el umbral, compruebo si el segundo lo est�
                //Si lo est�, calculo la x del punto en que la funci�n corta el umbral
                if (Compare(KeyStore[key2.Value], tumbral, cantidad))
                {
                    xy = InvInterpolate(new FunctionPointPair<TX, TY>(this, key1.Value, key2.Value), cantidad);
                    if (!existeDesde)
                    {
                        KeyStore.Remove(rangeStart);
                    }
                    if (!existeHasta)
                    {
                        KeyStore.Remove(rangeEnd);
                    }
                    return xy;
                }
                else
                {
                    //Si el segundo punto no est� en al umbral, paso al siguiente
                    key1 = KeyStore.Next(key1.Value);
                    key2 = KeyStore.Next(key2.Value);
                }
            }

            //Me ha salido del rango y no he encontrado un punto que est� en el umbral
            //El primer punto est� en el rango, el segundo no
            //Calculo el f(rangeEnd), si est� en el rango calculo la x del punto en que la funci�n corta el umbral
            //si no est� no hay primero

            if (Compare(KeyStore[key2.Value], tumbral, cantidad))
            {
                xy = InvInterpolate(new FunctionPointPair<TX, TY>(this, key1.Value, key2.Value), cantidad);
            }
            if (!existeDesde)
            {
                KeyStore.Remove(rangeStart);
            }
            if (!existeHasta)
            {
                KeyStore.Remove(rangeEnd);
            }
            return xy;
        }

        public override FunctionPoint<TX, TY> LstXYu(TX rangeStart, TX rangeEnd, ThresholdType tumbral, Func<TX, TY> fnUmbral)
        {
            throw new NotImplementedException("Funci�nClavesInterpolada.�ltXY no implementado a�n");
        }

    }

}
