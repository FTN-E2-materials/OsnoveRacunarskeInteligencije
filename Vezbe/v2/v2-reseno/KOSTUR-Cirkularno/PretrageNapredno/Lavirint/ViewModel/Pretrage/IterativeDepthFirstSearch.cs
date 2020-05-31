using System;
using System.Collections.Generic;
using System.Text;
using System.Collections;
using System.Windows.Forms;

namespace Lavirint
{
    class IterativeDeepFirstSeach
    {
        public State search(State pocetnoStanje, int maxDepth)
        {
            for (int level = 0; level < maxDepth; level++)
            {
                List<State> stanjaNaObradi = new List<State>();
                stanjaNaObradi.Add(pocetnoStanje);
                while (stanjaNaObradi.Count > 0)
                {
                    State naObradi = stanjaNaObradi[0];
                    stanjaNaObradi.Remove(naObradi);

                    if (naObradi.level > level)
                        continue;

                    if (!naObradi.isCirkularnaPutanja())
                    {
                        Main.allSearchStates.Add(naObradi);
                        if (naObradi.isKrajnjeStanje())
                        {
                            return naObradi;
                        }
                        List<State> mogucaSledecaStanja = naObradi.getMogucaSledecaStanja();
                        stanjaNaObradi.InsertRange(0, mogucaSledecaStanja);
                    }
                    
                }
            }
            return null;
        }
    }
}
