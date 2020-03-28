using System;
using System.Collections.Generic;
using System.Text;
using System.Collections;

namespace Lavirint
{
    class AStarSearch
    {
        //Razlika izmedju slepih pretraga i A* je sto smo kod njih uzimali prvi element a sada cemo najbolji getBest()
       //A* trazi najkraci i optimalan put
        public State search(State pocetnoStanje)
        {
            // TODO 5.1: Implementirati algoritam vodjene pretrage A*
            List<State> stanjaNaObradi = new List<State>();
            stanjaNaObradi.Add(pocetnoStanje);
            while (stanjaNaObradi.Count > 0)
            {
                State naObradi = getBest(stanjaNaObradi);

                if (!naObradi.cirkularnaPutanja())
                {
                    Main.allSearchStates.Add(naObradi);
                    if (naObradi.isKrajnjeStanje())
                    {
                        return naObradi;
                    }
                    List<State> mogucaSledecaStanja = naObradi.mogucaSledecaStanja();
                    stanjaNaObradi.AddRange(mogucaSledecaStanja);
                }
                stanjaNaObradi.Remove(naObradi);
            }

            return null;
        }

        //Procena koliko je neki cvor dobar
        //Euklidsko rastojanje od krajnjeg cvora i tog trenutnog cvora
        //Procena koliko je neki cvor dobar
        public double heuristicFunction(State s)
        {
            // TODO 5.2: Implementirati heuristicku funkciju (funkcija odredjuje rastojanje)
            return Math.Sqrt(Math.Pow(s.node.markI - Main.krajnjiNode.markI, 2)
               + Math.Pow(s.node.markJ - Main.krajnjiNode.markJ, 2));
        }

        //sad trazimo najbolji po heuristika + cena
        //Trazenje moinimuma, trazimo element gde je heuristika i
        //cena najmanja i njega vratimo kao element
        public State getBest(List<State> stanja)
        {
            State rez = null;
            double min = Double.MaxValue;

            foreach(State s in stanja)
            {
                double h = heuristicFunction(s) + s.cost;
                if(h< min)
                {
                    min = h;
                    rez = s;
                }

            }
            return rez;
        }
       
    }
}
