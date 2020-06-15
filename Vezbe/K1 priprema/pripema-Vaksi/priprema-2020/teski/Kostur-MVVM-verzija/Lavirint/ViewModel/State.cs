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
        private static int [,] koraci = { { 0, 1 }, { 0, -1 }, { -1, 0 }, { 1, 0 } };
        public List<int> pokupljenePlave = new List<int>();
        public List<int> pokupljeneNarandzaste = new List<int>();

        public static int potrebnoPlavih = 3;
        public static int potrebnoNarandzastih = 2;

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
            rez.pokupljenePlave.AddRange(this.pokupljenePlave);
            rez.pokupljeneNarandzaste.AddRange(this.pokupljeneNarandzaste);

            if(lavirint[markI, markJ] == 4 && ! this.pokupljenePlave.Contains(10*markI + markJ))
            {
                rez.pokupljenePlave.Add(10 * markI + markJ);
            }

            if(rez.pokupljenePlave.Count >= potrebnoPlavih && lavirint[markI, markJ] == 5 && !this.pokupljeneNarandzaste.Contains(10*markI + markJ))
            {
                rez.pokupljeneNarandzaste.Add(10 * markI + markJ);
                
            }

            if(lavirint[markI, markJ] == 6)
            {
                rez.cost = 1000000;
            }



            return rez;
        }

        // TODO: Ovde odredjujemo validne kordinate
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
        
        // TODO: Ovde odredjujemo moguca sledeca kretanja
        // Ako se nista posebno ne trazi, ovo je dovoljno.
        public List<State> mogucaSledecaStanja()
        {
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

        // TODO: Ovde odredjujemo koji je hash code
        public override int GetHashCode()
        {
            int hash = 10 * markI + markJ;
            int nivoFrekvencije = 100;
            foreach (int hashPokupljeneKutije in this.pokupljenePlave)
            {
                hash += nivoFrekvencije + hashPokupljeneKutije;
                nivoFrekvencije += 1000;
            }

            foreach(int hashPokupljeneKutije in this.pokupljeneNarandzaste)
            {
                hash += nivoFrekvencije + hashPokupljeneKutije;
                nivoFrekvencije += 1000;
            }

            return hash;
        }

        // TODO: Ovde menjamo kada se krajnje stanje uslovljava i zavisi od necega
        public bool isKrajnjeStanje()
        {
            if(this.pokupljenePlave.Count< potrebnoPlavih)
            {
                return false;
            }
            if(this.pokupljeneNarandzaste.Count < potrebnoNarandzastih)
            {
                return false;
            }

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
