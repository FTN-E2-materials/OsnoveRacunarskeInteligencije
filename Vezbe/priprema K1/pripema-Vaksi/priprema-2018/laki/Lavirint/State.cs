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
        public int level;
        // TODO 1: Omoguciti kretanje robota kao sahovska figura konj.
        public bool kutijaPokupljena;
        private static int [,] koraci = { { -1, -2 }, { -2, -1 }, { -2, 1 }, { -1, 2 }, { 1, 2 }, { 2, 1 }, { 2, -1 }, { 1, -2 } };


        
        public State sledeceStanje(int markI, int markJ)
        {
            State rez = new State();
            rez.markI = markI;
            rez.markJ = markJ;
            rez.parent = this;
            rez.cost = this.cost + 1;
            rez.level = this.level + 1;
            // TODO 8.1: Da li je kutija pokupljena
            rez.kutijaPokupljena = this.kutijaPokupljena;
            if(lavirint[markI,markJ] == 4)
            {   // kutija je pokupljena ako smo obisli 4-rku odnosno plavo polje.
                rez.kutijaPokupljena = true;
            }
            return rez;
        }

        private bool validneKordinate(int kordI, int kordJ)
        {
            if(kordI<0 || kordI >= Main.brojVrsta)
            {
                return false;
            }
            if(kordJ<0 || kordJ >= Main.brojKolona)
            {
                return false;
            }
            /*
             * Posto smo zavrsili sa proverom za izlazak van opsega table,
             * ogranicavamo da nije moguce prolaziti kroz sivu[vrednost polja 1]
             * kutiju.
             * 
             */
            if(lavirint[kordI,kordJ] == 1)
            {
                return false;
            }

            return true;
        }

        public List<State> mogucaSledecaStanja()
        {

            //TODO 2: Implementirati metodu tako da odredjuje moguca sledeca stanja
            List<State> validnaSledecaStanja = new List<State>();

            for(int i = 0; i < koraci.GetLength(0); i++)
            {
                int novoI = markI + koraci[i, 0];
                int novoJ = markJ + koraci[i, 1];

                if (validneKordinate(novoI, novoJ))
                {
                    validnaSledecaStanja.Add(sledeceStanje(novoI, novoJ));
                }

            }



            

            return validnaSledecaStanja;
        }

        public override int GetHashCode()
        {
            // TODO 8.2: Dozvola vracanja po istom polju samo ako imamo kutiju(pokupili smo je)
            // hash - predstvlja jedinstvenu vrednost jednog polja u matrici
            int hash = 100 * markI + markJ;
            if (kutijaPokupljena)
            {   // ako je kutija pokupljena znaci da imamo novi hash za to polje
                // defakto, to polje sada dobija novi hash i preko njega se moze preci 
                hash = hash + 1000;
            }

            return hash;

        }

        public bool isKrajnjeStanje()
        {
            return Main.krajnjeStanje.markI == markI && Main.krajnjeStanje.markJ == markJ && kutijaPokupljena;
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
