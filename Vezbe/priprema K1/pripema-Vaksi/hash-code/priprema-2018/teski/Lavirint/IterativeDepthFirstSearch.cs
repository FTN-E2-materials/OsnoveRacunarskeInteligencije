using System;
using System.Collections.Generic;
using System.Text;
using System.Collections;
using System.Windows.Forms;

namespace Lavirint
{
    class IterativeDeepFirstSeach
    {
        // Implementirati iteraivnu prvi u dubinu pretragu - IterativeDFS
        /*
         * Pretrazi se prosledjuje pocetno stanje i maksimalna dubina do koje 
         * zelimo da pretraga ide.
         * 
         * Pretrazujemo nivo po nivo.
         */
        public State search(State pocetnoStanje, int maxDepth)
        {
            //Pretrazuje se nivo po nivo -> stanja u istom nivou imaju jednak put do sebe
            for (int level = 0; level < maxDepth; ++level) // i - trenutni nivo koji se pretrazuje
            {
                List<State> stanjaZaObradu = new List<State>();
                stanjaZaObradu.Add(pocetnoStanje);

                while (stanjaZaObradu.Count > 0)
                {
                    State trenutnoStanje = stanjaZaObradu[0];
                    stanjaZaObradu.Remove(trenutnoStanje);

                    if (trenutnoStanje.cost < level) //ako je na manjem nivou dubine, razvija se
                    {
                        stanjaZaObradu.InsertRange(0, trenutnoStanje.mogucaSledecaStanja());
                    }
                    else if (trenutnoStanje.cost > level) //ako je na vecem nivou dubine, odbacuje se
                    {
                        continue;
                    }
                    else if (trenutnoStanje.cost == 1)//ako je na nivou i, proverava se
                    {
                        Main.allSearchStates.Add(trenutnoStanje); //za prikaz u debug
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
