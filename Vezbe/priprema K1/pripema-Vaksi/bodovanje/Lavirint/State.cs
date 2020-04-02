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

        // TODO 2: Definisati moguca kretanja naseg robota.
        private static int[,] konj = { { -1, -2 }, { -2, -1 }, { -2, 1 }, { -1, 2 }, { 1, 2 }, { 2, 1 }, { 2, -1 }, { 1, -2 } };
        private static int[,] top = { { 0, 1 }, { 0, -1 }, { -1, 0 }, { 1, 0 } };                       // uz ogranicenje na max 2 poteza ! [ tako trazi zadatak ]

        // TODO 3: Omoguciti vodjenje evidencije o skupljenim kutijama i o broju sakupljenih bodova.
        private List<int> skupljeneKutije = new List<int>();        // cuvamo kutiju na hesh-u 10 * x_kord + y_kord
       
        private int sakupljenoBodova = 0;

        public static int poenaZaZutu = 3;
        public static int poenaZaPlavu = 1;


        public State sledeceStanje(int markI, int markJ)
        {
            State rez = new State();
            rez.markI = markI;
            rez.markJ = markJ;
            rez.parent = this;
            rez.cost = this.cost + 1;
            rez.level = this.level + 1;

            rez.sakupljenoBodova = this.sakupljenoBodova;       // trebmo preneti i broj bodova na sledeca stanja
            // TODO 4: Implementirati kupljenje kutija.

            foreach(int kutija in this.skupljeneKutije)
            {
                rez.skupljeneKutije.Add(kutija);
            }

            if (lavirint[markI, markJ] == 4 && !skupljeneKutije.Contains(markI * 10 + markJ))
            {
                //nadjena nova plava kutija
                rez.sakupljenoBodova += poenaZaPlavu;
                rez.skupljeneKutije.Add(markI * 10 + markJ);
            }

            if (lavirint[markI, markJ] == 5 && skupljeneKutije.Contains(markI * 10 + markJ) == false)
            {
                //nadjena nova zuta kutija
                rez.sakupljenoBodova += poenaZaZutu;
                rez.skupljeneKutije.Add(markI * 10 + markJ);
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
            int[,] koraci = null;
            bool kretanjeKonja = false;

            if (sakupljenoBodova < 6)
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
                int brojKoraka = 1;                         // inicijalan broj koraka koji figura obavlja
                while (true)                        
                {
                    // novoI je staroI + koliko zelimo koraka ka novom pravcu
                    int novoI = markI + brojKoraka * koraci[i, 0];
                    int novoJ = markJ + brojKoraka * koraci[i, 1];
                    brojKoraka++;

                    // Ako kordinate nisu validne zavrsavamo istrazivanje za ovo stanje
                    if (!validneKordinate(novoI, novoJ))
                        break;

                    // Inace, to stanje je validno za igru
                    State novoStanje = sledeceStanje(novoI, novoJ);
                    validnaSledecaStanja.Add(novoStanje);


                    // Posto konj moze da igra samo jedno polje kao i kralj [ jednu iteraciju progresa na tabli ]
                    if (kretanjeKonja)
                        break;

                    // Posto top moze u ovoj varijanti samo dva polja da se krece
                    if (!kretanjeKonja && brojKoraka>=2)
                        break;
                }

            }


            return validnaSledecaStanja;
        }

        public override int GetHashCode()
        {
            // TODO 5: Omoguciti prolazak kroz isto polje ako smo sad pokupili kutiju.

            // Dozvola vracanja po istom polju samo ako imamo kutiju(pokupili smo je)
            // hash - predstvlja jedinstvenu vrednost jednog polja u matrici
            int hash = 10 * markI + markJ;
            int koeficijentHesha = 100;
           foreach(int kutija in skupljeneKutije)
            {
                hash += kutija * koeficijentHesha;
                koeficijentHesha *= 100;
            }
            return hash;

        }

        public bool isKrajnjeStanje()
        {
            // TODO 6: Prosiriti metodu provere da li je krajnje stanje, da moze biti samo akoje poslednja kutija plava.
            if (skupljeneKutije.Count > 0)
            {
                bool krajnjeStanje = Main.krajnjeStanje.markI == markI && Main.krajnjeStanje.markJ == markJ && sakupljenoBodova >= 11;
                // provera da li je zadnja plava
                int poslednja = skupljeneKutije[skupljeneKutije.Count - 1];
                int j = poslednja % 10;
                int i = poslednja / 10;
                if (lavirint[i, j] == 4)
                    return krajnjeStanje;
                else
                    return false;
            }
            else
                return false;
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
