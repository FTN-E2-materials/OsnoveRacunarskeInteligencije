using System;
using System.Collections.Generic;
using System.Text;
using System.Collections;
using System.Net;
using System.Collections.Specialized;

namespace Lavirint
{
    class AStarSearch
    {
        public State search(State pocetnoStanje)
        {
            // TODO 5.1: Implementirati algoritam vodjene pretrage A*
            List<State> stanjaNaObradi = new List<State>();
            OrderedDictionary predjeniPut = new OrderedDictionary();
            stanjaNaObradi.Add(pocetnoStanje);
            while (stanjaNaObradi.Count > 0)
            {
                State naObradi = getBest(stanjaNaObradi);

                if (!predjeniPut.Contains(naObradi))
                {
                    predjeniPut.Add(naObradi, null);
                    Main.allSearchStates.Add(naObradi);
                    if (naObradi.isKrajnjeStanje())
                    {
                        return naObradi;
                    }
                    List<State> mogucaSledecaStanja = naObradi.getMogucaSledecaStanja();
                    stanjaNaObradi.AddRange(mogucaSledecaStanja);
                }

                //if (!naObradi.isCirkularnaPutanja())
                //{
                //    Main.allSearchStates.Add(naObradi);
                //    if (naObradi.isKrajnjeStanje())
                //    {
                //        return naObradi;
                //    }
                //    List<State> mogucaSledecaStanja = naObradi.getMogucaSledecaStanja();
                //    stanjaNaObradi.AddRange(mogucaSledecaStanja);
                //}
                stanjaNaObradi.Remove(naObradi);
            }

            return null;
        }

        public double heuristicFunction(State s)
        {
            return Math.Sqrt(Math.Pow(s.trenutniCvor.markI - Main.krajnjiNode.markI, 2)
                + Math.Pow(s.trenutniCvor.markJ - Main.krajnjiNode.markJ, 2));
        }

        public State getBest(List<State> stanja)
        {
            State rez = null;
            double min = Double.MaxValue;

            foreach (State s in stanja)
            {
                double h = heuristicFunction(s)+  s.cena;
                if (h < min)
                {
                    min = h;
                    rez = s;
                }
            }
            return rez;
        }
    }
}
