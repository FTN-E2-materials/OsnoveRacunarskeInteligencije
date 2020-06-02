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

        // TODO: Ovde odredjujem/dodajem atribute za moguce korake, atribute da li su kutije pokupljene i slicno.
        //public bool kutijaPokupljena;
        private static int[,] top = { { 0, 1 }, { 0, -1 }, { -1, 0 }, { 1, 0 } };



        // TODO: Ovde govorimo sta sledece stanje ima i sta nosi sa sobom
        // voditi da racuna da ono preuzme sve od prethodnog sto treba !
        public State sledeceStanje(int markI, int markJ)
        {
            State rez = new State();
            rez.markI = markI;
            rez.markJ = markJ;
            rez.parent = this;
            rez.cost = this.cost + 1;
            rez.level = this.level + 1;
            // TODO: Ovde recimo mozemo dodati da li je kutija pokupljena
            // Pa ako jeste onda atribut za indikaciju pokupljenosti kutije za ovo stanje stavimo na true

            return rez;
        }

        
        // TODO: Ovde odredjujemo moguca sledeca kretanja
        // Ako se nista posebno ne trazi, ovo je dovoljno.
        public List<State> mogucaSledecaStanja()
        {
            List<State> validnaSledecaStanja = new List<State>();
            int[,] koraci = null;
            bool jednoPoteznaFigura = true;                     // u zavisnosti mogucnosti kretanja figure, podesavam ovaj parametar

            //TODO: U zavisnosti od uslova menjam korake
            koraci = top;

            for(int i = 0; i < koraci.GetLength(0); i++)
            {
                // Broj koraka koji ima odredjena figura u ovom potezu
                int brojKoraka = 1;

                while (true)
                {
                    int novoI = markI + brojKoraka * koraci[i, 0];
                    int novoJ = markJ + brojKoraka * koraci[i, 1];

                    ++brojKoraka;

                    // Odma prekidam istrazivanje ako su kordinate nevalidne
                    if (!validneKordinate(novoI, novoJ))
                        break;
                    
                    // U suprotnosti ih dodajem kao sledeca validna stanja
                    validnaSledecaStanja.Add(sledeceStanje(novoI, novoJ));

                    // Restrikcija kretanja na jedan potez samo [za jedno potezne figure]
                    if (jednoPoteznaFigura)
                        break;

                }

            }
            return validnaSledecaStanja;
        }

        // TODO: Ovde odredjujemo koji je hash code
        public override int GetHashCode()
        {
            int hash = 10 * markI + markJ;
            return hash;
        }

        // TODO: Ovde menjamo kada se krajnje stanje uslovljava i zavisi od necega
        public bool isKrajnjeStanje()
        {
            return Main.krajnjeStanje.markI == markI && Main.krajnjeStanje.markJ == markJ;
        }


        // TODO: Ovde odredjujemo validne kordinate
        private bool validneKordinate(int kordI, int kordJ)
        {
            if (kordI < 0 || kordI >= Main.brojVrsta)
            {
                return false;
            }
            if (kordJ < 0 || kordJ >= Main.brojKolona)
            {
                return false;
            }

            // Branim prolazak kroz sivu [ 1 je reprezent sivog polja na tabli]
            if (lavirint[kordI, kordJ] == 1)
            {
                return false;
            }

            return true;
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
