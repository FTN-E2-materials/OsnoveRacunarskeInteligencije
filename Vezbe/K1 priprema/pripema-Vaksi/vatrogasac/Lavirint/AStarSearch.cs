using System;
using System.Collections.Generic;
using System.Text;
using System.Collections;

namespace Lavirint
{
    class AStarSearch
    {
        // Implementirati A* pretragu
        /*
         * Vodjena pretraga koja uzima najbolja resenje po heuristici.
         * Za pamcenje stanja opet koristimo Hastable
         */
        public State search(State pocetnoStanje)
        {
            List<State> stanjaZaObradu = new List<State>();
            Hashtable predjeniPut = new Hashtable();
            stanjaZaObradu.Add(pocetnoStanje);

            while (stanjaZaObradu.Count > 0)
            {
                State naObradi = getBest(stanjaZaObradu);

                if (!predjeniPut.ContainsKey(naObradi.GetHashCode()))
                {
                    Main.allSearchStates.Add(naObradi);
                    if (naObradi.isKrajnjeStanje())
                    {
                        return naObradi;
                    }
                    predjeniPut.Add(naObradi.GetHashCode(),null);
                    List<State> sledecaStanja = naObradi.mogucaSledecaStanja();

                    foreach (State s in sledecaStanja)
                    {
                        stanjaZaObradu.Add(s);
                    }
                }
                stanjaZaObradu.Remove(naObradi);
            }
            return null;
        }

        
        /*
         * Funkcija koja odredjuje rastojanje.
         * Trenutno na osnovu EUKLIDSKOG rastojanja 
         * od ciljnog stanja.
         */
        public double heuristicFunction(State s)
        {
            // TODO 8: Promeniti heuristiku tako da bez vode, izbegavamo vatru.
            double heuristika = Math.Sqrt(Math.Pow(s.markI - Main.krajnjeStanje.markI, 2) + Math.Pow(s.markJ - Main.krajnjeStanje.markJ, 2))+s.cost;

            if (s.skupljenaVoda)
                return heuristika;
            else
            {
                // Ako nismo skupili vodu, bezimo od vatre, odnosno pumpamo ka gore heuristiku ako je vatra blizu.
                foreach(int hashVatre in Main.vatre)
                {
                    int Y = hashVatre % 10;
                    int X = hashVatre / 10;
                    double udaljenostOdVatre = Math.Sqrt(Math.Pow(s.markI - X, 2) + Math.Pow(s.markJ - Y, 2));
                    if (udaljenostOdVatre == 0)
                        heuristika += 100000;
                    else
                        heuristika += 10 / udaljenostOdVatre;
                }
                
            }

            return heuristika;
        }

        public State getBest(List<State> stanja)
        {
            State rez = null;
            double min = Double.MaxValue;

            foreach (State s in stanja)
            {
                double h = heuristicFunction(s);
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
