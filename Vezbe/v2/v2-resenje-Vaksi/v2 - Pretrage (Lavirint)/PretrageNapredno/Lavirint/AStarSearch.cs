using System;
using System.Collections.Generic;
using System.Text;
using System.Collections;

namespace Lavirint
{
    class AStarSearch
    {
        public State search(State pocetnoStanje)
        {
            // TODO 5.1: Implementirati algoritam vodjene pretrage A*
            List<State> stanjaNaObradi = new List<State>();
            stanjaNaObradi.Add(pocetnoStanje);

            while(stanjaNaObradi.Count > 0)
            {
                State naObradi = getBest(stanjaNaObradi);
                if (!naObradi.cirkularnaPutanja())
                {
                    Main.allSearchStates.Add(naObradi);             // dodajemo polja koja su proveravana
                    if (naObradi.isKrajnjeStanje())
                    {
                        return naObradi;
                    }
                    List<State> mogucaSledecaStanja = naObradi.mogucaSledecaStanja();
                    stanjaNaObradi.AddRange(mogucaSledecaStanja);   // dodajemo listu elemenata u listu
                }
                stanjaNaObradi.Remove(naObradi);
            }
            return null;
        }

        
        public double heuristicFunction(State s)
        {
            // TODO 5.2: Implementirati heuristicku funkciju (funkcija odredjuje rastojanje)
            return Math.Sqrt(Math.Pow(s.node.markI - Main.krajnjiNode.markI, 2)
                + Math.Pow(s.node.markJ - Main.krajnjiNode.markJ, 2));
        }

        // Funkcija koja od svih stanja vraca ono najbolje.
        public State getBest(List<State> stanja)
        {
            State rez = null;
            double min = Double.MaxValue;

            foreach (State s in stanja)
            {   // u nasem slucaju najmanja vrednost je najbolja
                double h = heuristicFunction(s) + s.cost;
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
