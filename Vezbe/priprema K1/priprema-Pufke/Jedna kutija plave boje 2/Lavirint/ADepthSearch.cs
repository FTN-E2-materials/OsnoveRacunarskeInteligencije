using System;
using System.Collections.Generic;
using System.Text;
using System.Collections;

namespace Lavirint
{
    class ADepthSearch
    {
        //TODO 5: Implementirati klasu koja kombinuje algoritme Prvi u dubinu i A*
        public State searchCombined(State pocetnoStanje)
        {
            List<State> stanjaNaObradi = new List<State>();
            Hashtable predjeniPut = new Hashtable();
            stanjaNaObradi.Add(pocetnoStanje);
            while (stanjaNaObradi.Count > 0)
            {
                State naObradi = stanjaNaObradi[0];

                if (!predjeniPut.ContainsKey(naObradi.GetHashCode()))
                {
                    Main.allSearchStates.Add(naObradi);
                    if (naObradi.kutija)
                    {
                        AStarSearch aZvezda = new AStarSearch();
                        return aZvezda.search(naObradi);
                    }
                    predjeniPut.Add(naObradi.GetHashCode(), null);
                    List<State> mogucaSledecaStanja = naObradi.mogucaSledecaStanja();
                    stanjaNaObradi.InsertRange(0, mogucaSledecaStanja);
                }
                stanjaNaObradi.Remove(naObradi);
            }
            return null;
        }
    }
}
