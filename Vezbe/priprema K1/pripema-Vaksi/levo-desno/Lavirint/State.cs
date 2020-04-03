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

        // Obicno kretanje gore dole levo desno
        private static int[,] koraci = { { 0, 1 }, { 0, -1 }, { -1, 0 }, { 1, 0 } };

        // TODO 3.1: Omoguciti vodjenje evidencije o prikupljenim kutijama
        private List<int> skupljenaPolja = new List<int>();


        public State sledeceStanje(int markI, int markJ)
        {
            State rez = new State();
            rez.markI = markI;
            rez.markJ = markJ;
            rez.parent = this;
            rez.cost = this.cost + 1;
            rez.level = this.level + 1;

            // TODO 3.2: Implementirati kupljenje kutija

            //preuzmi stare
            foreach (int item in this.skupljenaPolja)
            {
                rez.skupljenaPolja.Add(item);
            }
            //proveri da li je novo polje obavezno
            if (lavirint[markI, markJ] == 4)
            {
                if (markJ <= Main.pocetnoStanje.markJ) //obavezno polje je na levoj strani
                {
                    //skupi ga, ako je novo
                    if (rez.skupljenaPolja.Contains(markI * 10 + markJ) == false)
                    {
                        rez.skupljenaPolja.Add(markI * 10 + markJ);
                    }
                }
                else //obavezno polje je na desnoj strani
                {
                    //proveri da li su skupljena sva na levoj strani
                    if (rez.skupljenaPolja.Count >= Main.levo)
                    {
                        //ako jesu skupljena, skupi polje ako je novo
                        if (rez.skupljenaPolja.Contains(markI * 10 + markJ) == false)
                        {
                            rez.skupljenaPolja.Add(markI * 10 + markJ);
                        }
                    }
                }
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

            // Implementirati metodu tako da odredjuje moguca sledeca stanja
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
            // TODO 4: Prosiriti metodu da nam omoguci ponovni prolazak kroz polje.
            // Dozvola vracanja po istom polju samo ako imamo kutiju(pokupili smo je)
            // hash - predstvlja jedinstvenu vrednost jednog polja u matrici
            int hash = 100 * markI + markJ;
            int koeficijentHesha = 100;

            // oznacavanje kutija koje su koriscenje
            foreach(int hesh in this.skupljenaPolja)
            {
                hash += hesh * koeficijentHesha;
                koeficijentHesha *= 100;
            }
            
            return hash;

        }

        public bool isKrajnjeStanje()
        {
            // TODO 5: Prosiriti metodu da oznacava kraj samo ako su pokupljene sve kutije.
            return Main.krajnjeStanje.markI == markI && Main.krajnjeStanje.markJ == markJ && skupljenaPolja.Count == Main.obaveznaPolja.Count;
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
