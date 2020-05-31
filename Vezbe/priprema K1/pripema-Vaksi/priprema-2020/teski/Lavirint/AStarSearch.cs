using System;
using System.Collections.Generic;
using System.Text;
using System.Collections;
using System.Windows.Forms;

namespace Lavirint
{
    class AStarSearch
    {
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
                //MessageBox.Show(naObradi.GetHashCode().ToString());
                // Zelim da ako smo na ovom putu PRVI PUT, tek onda ga obradimo
                if (!predjeniPut.ContainsKey(naObradi.GetHashCode()))
                {

                    // da ga dodam u sva obidjenja stanja
                    Main.allSearchStates.Add(naObradi);
                    if (naObradi.isKrajnjeStanje())
                    {
                        return naObradi;
                    }
                    // da ga dodam u predjeni put
                    predjeniPut.Add(naObradi.GetHashCode(),null);
                    List<State> sledecaStanja = naObradi.mogucaSledecaStanja();

                    // i da njegova moguca sledeca stanja takodje dodam kao stanja za obradu
                    foreach (State s in sledecaStanja)
                    {
                        stanjaZaObradu.Add(s);
                    }
                }
                //else
                //{
                //    MessageBox.Show(naObradi.GetHashCode().ToString());    
                //}
                // kada ga obradim, uklonim ga iz liste stanja za obradu
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
            return Math.Sqrt(Math.Pow(s.markI - Main.krajnjeStanje.markI, 2) + Math.Pow(s.markJ - Main.krajnjeStanje.markJ, 2))+s.cost;
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
