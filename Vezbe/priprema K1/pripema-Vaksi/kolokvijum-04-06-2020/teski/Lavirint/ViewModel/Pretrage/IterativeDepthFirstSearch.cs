using System;
using System.Collections.Generic;
using System.Text;
using System.Collections;
using System.Windows.Forms;

namespace Lavirint
{
    /// <summary>
    /// Klasa predstavnica DFS-a, samo dekomponovana po nivoima.
    /// </summary>
    class IterativeDeepFirstSeach
    {
        public State search(State pocetnoStanje, int maxDepth)
        {

            for (int level = 0; level < maxDepth; ++level) 
            {
                List<State> stanjaZaObradu = new List<State>();
                stanjaZaObradu.Add(pocetnoStanje);

                while (stanjaZaObradu.Count > 0)
                {
                    State trenutnoStanje = stanjaZaObradu[0];
                    stanjaZaObradu.Remove(trenutnoStanje);

                    if (trenutnoStanje.cost < level) 
                    {
                        stanjaZaObradu.InsertRange(0, trenutnoStanje.mogucaSledecaStanja());
                    }
                    else if (trenutnoStanje.cost > level)
                    {
                        continue;
                    }
                    else if (trenutnoStanje.cost == 1)
                    {
                        Main.allSearchStates.Add(trenutnoStanje); 
                        if (trenutnoStanje.isKrajnjeStanje() == true)
                        {
                            return trenutnoStanje;
                        }
                    }

                }

            }
            return null;
        }
    }
}
