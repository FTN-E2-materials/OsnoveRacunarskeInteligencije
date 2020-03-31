using System;
using System.Collections.Generic;
using System.Text;
using System.Collections;
using System.Windows.Forms;

namespace Lavirint
{
    class BreadthFirstSearch
    {   
        // Implementirati pretragu prvi u sirinu - BFS
        /*
         * Slepa pretraga koja daje optimalno resenje.
         * Koristicemo strukturu FIFO[first in, first out],
         * odnosno skdamo sa pocetka i dodajemo na kraj.
         */
        public State search(State pocetnoStanje)
        {
            List<State> stanjaNaObradi = new List<State>();                     // simuliramo FIFO
            Hashtable predjeniPut = new Hashtable();                            // za pamcenje stanja u kojima je robot bio
            stanjaNaObradi.Add(pocetnoStanje);
            while (stanjaNaObradi.Count > 0)
            {
                State naObradi = stanjaNaObradi[0];

                if (!predjeniPut.ContainsKey(naObradi.GetHashCode()))           // obradjujemo samo ne posecena stanja
                {
                    Main.allSearchStates.Add(naObradi);                         // sluzi za prikaz u debug rezimu
                    if (naObradi.isKrajnjeStanje())                             // zavrsavamo ako je stanje krajnje
                    {
                        return naObradi;
                    }
                    predjeniPut.Add(naObradi.GetHashCode(), null);              // belezimo da smo obisli ovo stanje
                    List<State> mogucaSledecaStanja = naObradi.mogucaSledecaStanja();
                    stanjaNaObradi.AddRange(mogucaSledecaStanja);               // sva moguca sledeca stanja se dodaju na
                }                                                               // pocetak liste
                stanjaNaObradi.Remove(naObradi);                                // uklanjamo trenutno stanje.
            }
            return null;
        }
    }
}
