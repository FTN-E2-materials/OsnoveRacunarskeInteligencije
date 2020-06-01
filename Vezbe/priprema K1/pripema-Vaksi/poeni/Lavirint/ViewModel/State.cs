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

        private List<int> sakupljeneKutije = new List<int>();

        private static int[,] konj = { { -1, -2 }, { -2, -1 }, { -2, 1 }, { -1, 2 }, { 1, 2 }, { 2, 1 }, { 2, -1 }, { 1, -2 } };
        private static int[,] top = { { 0, 1 }, { 0, -1 }, { -1, 0 }, { 1, 0 } };

        private int sakupljenoBodova = 0;

        public static int poenaZaZutu = 3;
        public static int poenaZaPlavu = 1;

        private bool poslednjaPlava;

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
            rez.sakupljenoBodova = this.sakupljenoBodova;
            rez.sakupljeneKutije.AddRange(this.sakupljeneKutije);
            rez.poslednjaPlava = this.poslednjaPlava;

            if(lavirint[markI, markJ] == 4 && !this.sakupljeneKutije.Contains(10*markI + markJ))
            {
                rez.sakupljeneKutije.Add(10 * markI + markJ);
                rez.sakupljenoBodova += poenaZaPlavu;
                rez.poslednjaPlava = true;
            }

            if (lavirint[markI, markJ] == 5 && !this.sakupljeneKutije.Contains(10 * markI + markJ))
            {
                rez.sakupljeneKutije.Add(10 * markI + markJ);
                rez.sakupljenoBodova += poenaZaZutu;
                rez.cost = -1000;
                rez.poslednjaPlava = false;
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
            int[,] koraci = null;
            bool kretanjeKonja = false;

            if(this.sakupljenoBodova > 6)
            {
                koraci = konj;
                kretanjeKonja = true;
            }
            else
            {
                koraci = top;
            }

            

            for(int i = 0; i < koraci.GetLength(0); i++)
            {
                int brojKoroka = 1;                                 // inicijalni broj koraka koji figura obavlja
                while (true)
                {
                    ++brojKoroka;
                    int novoI = markI + koraci[i, 0];
                    int novoJ = markJ + koraci[i, 1];

                    // Ako kordinate nisu validne zavrsavamo istrazivanje za ovo stanje
                    if (!validneKordinate(novoI, novoJ))            
                        break;

                    validnaSledecaStanja.Add(sledeceStanje(novoI, novoJ));

                    // Posto konj moze da igra samo jedno polje kao i kralj [ jednu iteraciju progresa na tabli ]
                    if (kretanjeKonja)
                        break;

                    // Ako je u pitanju kretanje topa i broj koraka je veci/jednak od 2, tu zavrsavamo potez [ takvo pravilo igre - top samo 2 poteza moze ]
                    if (!kretanjeKonja && brojKoroka >= 2)
                        break;
                }

            }
            return validnaSledecaStanja;
        }

        // TODO: Ovde odredjujemo koji je hash code
        public override int GetHashCode()
        {
            int hash = 10 * markI + markJ;
            int nivoFrekvencije = 100;
            foreach (int hashPokupljenogPolja in this.sakupljeneKutije)
            {
                hash += nivoFrekvencije + hashPokupljenogPolja;
                nivoFrekvencije += 100;

            }
            return hash;
        }

        // TODO: Ovde menjamo kada se krajnje stanje uslovljava i zavisi od necega
        public bool isKrajnjeStanje()
        {
            if(this.sakupljenoBodova != 11)
            {
                return false;
            }

            // Poslednja kutija da bude plava !
            if (!poslednjaPlava)
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
