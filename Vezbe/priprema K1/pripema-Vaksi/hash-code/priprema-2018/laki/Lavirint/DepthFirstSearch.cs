using System;
using System.Collections.Generic;
using System.Text;
using System.Collections;
using System.Windows.Forms;

namespace Lavirint
{
    class DepthFirstSearch
    {
        //TODO 4: Implementirati pretragu prvi u dubinu - DFS
        /*
         * Slepa pretraga
         * Pretraga se koristi kada znamo da je resenje daleko od pocetnog stanja 
         * (tj. do krajnjeg stanja je portrebno preci veliki broj stanja)
         * Nije zagarantovano da daje optimalno resenje.
         * 
         */
        public State search(State pocetnoStanje)
        {
            /*
             * Strukturu koju simulramo je LIFO[last in, first out]
             * , dodajemo na pocetak i uklanjamosa kraja.
             * 
             * Hastable predjeniPut nam sluzi za pamcenje puta-stanja(polja)
             * u kojima je robot vec bio.
             */
            List<State> stanjaNaObradi = new List<State>();
            stanjaNaObradi.Add(pocetnoStanje);

            Hashtable predjeniPut = new Hashtable();
            
            while (stanjaNaObradi.Count > 0)
            {
                State naObradi = stanjaNaObradi[0];
                
                /*
                 * Obezbedjujemo da posetimo samo ona stanja koja nisu
                 * posecivana.
                 */
                if(!predjeniPut.ContainsKey(naObradi.GetHashCode()))
                {
                    Main.allSearchStates.Add(naObradi);             // sluzi za prikaz u debug rezimu
                    if (naObradi.isKrajnjeStanje())                 // ako smo dosli do kraja vratimo to stanje
                    {
                        return naObradi;
                    }
                    predjeniPut.Add(naObradi.GetHashCode(),null);   // dodajemo da bi znali da smo posetili ovo stanje(polje)
                    // Sva moguca sledeca stanja se dodaju na pocetak liste
                    List<State> mogucaSledecaStanja = naObradi.mogucaSledecaStanja();
                    stanjaNaObradi.InsertRange(0, mogucaSledecaStanja);
                }
                // Uklanjamo trenutno stanje.
                stanjaNaObradi.Remove(naObradi);
            }
            return null;
        }
    }
}
