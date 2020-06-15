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
        //private static int [,] koraci = { { 0, 1 }, { 0, -1 }, { -1, 0 }, { 1, 0 } };
        private static int[,] konj = { { -1, -2 }, { -2, -1 }, { -2, 1 }, { -1, 2 }, { 1, 2 }, { 2, 1 }, { 2, -1 }, { 1, -2 } };
        private static int[,] kraljica = { { 1, 1 }, { -1, 1 }, { 1, -1 }, { -1, -1 }, { 1, 0 }, { -1, 0 }, { 0, 1 }, { 0, -1 } };
        private List<int> sakupljeneKutije = new List<int>();
        private int osvojenoBodova = 0;

        public static int bodoviZaPlavu = 1;
        public static int bodoviZaZutu = 1;
        public static int bodoviZaLjubicastu = 1;

        private int pokupljenoZaredomZutih = 0;
        private int pokupljenoZaredomPlavih = 0;
        

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

            rez.sakupljeneKutije.AddRange(this.sakupljeneKutije);
            rez.osvojenoBodova = this.osvojenoBodova;
            rez.pokupljenoZaredomZutih = this.pokupljenoZaredomZutih;
            rez.pokupljenoZaredomPlavih = this.pokupljenoZaredomPlavih;

            // PLAVA
            if(lavirint[markI, markJ] == 4 && !this.sakupljeneKutije.Contains(10 * markI + markJ))
            {
                rez.sakupljeneKutije.Add(10 * markI + markJ);
                rez.osvojenoBodova += bodoviZaPlavu;
                
                rez.pokupljenoZaredomZutih = 0;
                if (this.pokupljenoZaredomZutih >= 2)
                {
                    rez.cost -= 2;                      // stimulisem da se kupi plava
                }

                if(this.pokupljenoZaredomPlavih >= 1)
                {
                    rez.osvojenoBodova += 1;            //dodatni poen jer je i prethodna bila plava
                }

                rez.pokupljenoZaredomPlavih++;
            }

            // ZUTA
            if (lavirint[markI, markJ] == 5 && !this.sakupljeneKutije.Contains(10 * markI + markJ) && (this.pokupljenoZaredomZutih < 2))
            {   
                // napomena: da bi pokupili ovu kutiju mora biti manje od 2 prethodno pokupljenih zaredom zutih kutija
                rez.sakupljeneKutije.Add(10 * markI + markJ);
                rez.osvojenoBodova += bodoviZaZutu;
                rez.pokupljenoZaredomZutih++;

                rez.pokupljenoZaredomPlavih = 0;
            }

            // LJUBICASTA
            if(lavirint[markI, markJ] == 6 && !this.sakupljeneKutije.Contains(10 * markI + markJ))
            {
                rez.sakupljeneKutije.Add(10 * markI + markJ);
                rez.osvojenoBodova += bodoviZaLjubicastu;
                rez.pokupljenoZaredomZutih = 0;
                rez.pokupljenoZaredomPlavih = 0;

                if (this.pokupljenoZaredomZutih >= 1)
                {
                    rez.osvojenoBodova += 2;            // nagrada jer je prethodno bila zuta [ ukupno 3 boda ]
                }

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
            bool kretanjeKonja;
            if(this.osvojenoBodova < 5)
            {
                koraci = kraljica;
                kretanjeKonja = false;
            }
            else
            {
                koraci = konj;
                kretanjeKonja = true;
            }

            for(int i = 0; i < koraci.GetLength(0); i++)
            {
                int brojKoraka = 1;
                while (true)
                {
                    
                    int novoI = markI + brojKoraka * koraci[i, 0];
                    int novoJ = markJ + brojKoraka * koraci[i, 1];
                    ++brojKoraka;

                    if (!validneKordinate(novoI, novoJ))
                        break;

                    validnaSledecaStanja.Add(sledeceStanje(novoI, novoJ));

                    if (kretanjeKonja)
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
            foreach (int hashPokupljeneKutije in this.sakupljeneKutije)
            {
                hash += nivoFrekvencije + hashPokupljeneKutije;
                nivoFrekvencije += 100;
            }
            return hash;
        }

        // TODO: Ovde menjamo kada se krajnje stanje uslovljava i zavisi od necega
        public bool isKrajnjeStanje()
        {
            if(this.osvojenoBodova < 5)
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
