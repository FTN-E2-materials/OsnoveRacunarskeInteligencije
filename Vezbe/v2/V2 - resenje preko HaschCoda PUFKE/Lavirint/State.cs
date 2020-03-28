using System;
using System.Collections.Generic;
using System.Text;

namespace Lavirint
{
    public class State
    {
        public static int[,] lavirint;
        State parent;
        public int markI, markJ; //vrsta i kolona
        public double cost;
        public bool kutija;

        public State sledeceStanje(int markI, int markJ)
        {
            State rez = new State();
            rez.markI = markI;
            rez.markJ = markJ;
            rez.parent = this;
            rez.cost = this.cost + 1;
            rez.kutija = this.kutija;
            return rez;
        }

        
        public List<State> mogucaSledecaStanja()
        {
            //TODO 1: Implementirati metodu tako da odredjuje dozvoljeno kretanje u lavirintu
            //TODO 2: Prosiriti metodu tako da se ne moze prolaziti kroz sive kutije
            List<State> rez = new List<State>();

          

            if ((markJ > 0) && (lavirint[markI, markJ - 1] != 1))
            {
                rez.Add(sledeceStanje(markI, markJ - 1));
            }

            if ((markJ < Main.brojKolona - 1) && (lavirint[markI, markJ + 1] != 1))
            {
                rez.Add(sledeceStanje(markI, markJ + 1));
            }

            if ((markI > 0) && (lavirint[markI - 1, markJ] != 1))
            {
                rez.Add(sledeceStanje(markI - 1, markJ));
            }

            if ((markI < Main.brojVrsta - 1) && (lavirint[markI + 1, markJ] != 1))
            {
                rez.Add(sledeceStanje(markI + 1, markJ));
            }

            return rez;
        }

        public override int GetHashCode()
        {
            return 100 * markI + markJ;
        }

        public bool isKrajnjeStanje()
        {
            return Main.krajnjeStanje.markI == markI && Main.krajnjeStanje.markJ == markJ;
        }

        public List<State> path()
        {
            List<State> putanja = new List<State>();
            State tt = this;
            while (tt != null)
            {
                putanja.Insert(0, tt);
                tt = tt.parent;
            }
            return putanja;
        }

        
    }
}
