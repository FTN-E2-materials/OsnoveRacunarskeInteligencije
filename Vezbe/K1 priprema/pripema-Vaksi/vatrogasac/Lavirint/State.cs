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

        // TODO 3.1: Definisati moguce nacine kretanja
        public static int[,] lovac = { { 1, 1 }, { -1, 1 }, { 1, -1 }, { -1, -1 } };
        public static int[,] kralj = { { 1, 1 }, { -1, 1 }, { 1, -1 }, { -1, -1 }, { 1, 0 }, { -1, 0 }, { 0, 1 }, { 0, -1 } };

        // TODO 2.2: Definisati promenljive za rukovanjem gasenja vatri.
        public bool skupljenaVoda = false;
        private List<int> ugaseneVatre = new List<int>();

        public State sledeceStanje(int markI, int markJ)
        {
            State rez = new State();
            rez.markI = markI;
            rez.markJ = markJ;
            rez.parent = this;
            rez.cost = this.cost + 1;
            rez.level = this.level + 1;

            // TODO 4: Provera da li je voda skupljena.
            if(this.skupljenaVoda || (lavirint[markI,markJ] == 4))
            {
                rez.skupljenaVoda = true;
            }

            // TODO 5: Ugasiti vatru ako je voda skupljena.
            if (rez.skupljenaVoda)
            {
                // Preuzimamo sve prethodno ugasene vatre u igri.
                foreach(int ugasenaVatra in this.ugaseneVatre)
                {
                    rez.ugaseneVatre.Add(ugasenaVatra);
                }

                // Ako naidjemo na novu, i nju cemo sad ugasiti.
                if(lavirint[markI,markJ] == 5 && !rez.ugaseneVatre.Contains(markI*10 + markJ))
                {
                    rez.ugaseneVatre.Add(markI * 10 + markJ);
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

            // TODO 3.2: Implementirati varijabilnost kretanja tokom igre.
            List<State> validnaSledecaStanja = new List<State>();
            int[,] koraci = null;                                   // u odredjenom trenutku se menja kretanje
            bool kretanjeKralja = false;                            // na pocetku se ne krece kao kralj

            if (skupljenaVoda)
            {
                koraci = kralj;
                kretanjeKralja = true;
            }
            else
            {
                koraci = lovac;
            }


            for(int i = 0; i < koraci.GetLength(0); i++)
            {
                /*
                 * Lovac kao i top ima mogucnost prelaska visa polja od jednom, sve dok 
                 * mu na prepreku ne stanu granice table.
                 */
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


                    // Posto kralj moze da igra samo jedno polje kao i konj [ jednu iteraciju progresa na tabli ]
                    if (kretanjeKralja)
                        break;
                }
                

            }

            return validnaSledecaStanja;
        }

        public override int GetHashCode()
        {
            // TODO 7: Definisati novi hash za polja.
            // hash - predstvlja jedinstvenu vrednost jednog polja u matrici
            int hash = 10 * markI + markJ;

            if (skupljenaVoda)
                hash += 100;                // polje ima posebnu drugi hash- moze se preko njega ako je pokupio vodu


            // Svako polje iz ugaseneVatre sad ima novi hash-> slobodan je prolaz preko njega
            int koeficijentHesha = 1000;      // koeficijent koji sluzi za menjanje hasha pozicijama gde su vatre bile
            foreach(int heshVatre in ugaseneVatre)
            {
                hash += heshVatre * koeficijentHesha;
                koeficijentHesha *= 100;
            }

            return hash;

        }

        public bool isKrajnjeStanje()
        {
            // TODO 6: Definisati novo krajnje stanje.

            // Vatrogasac ne moze kuci ako nisu sve vatre ugasene
            if(Main.vatre.Count != ugaseneVatre.Count)
                return false;
            
            // Inace, kao i obicno, kordinate moraju biti krajnje kordiante - crveno polje
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
