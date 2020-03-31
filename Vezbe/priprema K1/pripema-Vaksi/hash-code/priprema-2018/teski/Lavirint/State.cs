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

        // Omoguciti kretanje u svim pravcima oko svoje ose.
        private static int[,] koraci = { { 1, 0 }, { 1, 1 }, { 1, -1 }, { -1, 1 }, { 0, -1 }, { 0, 1 }, { -1, -1 }, { -1, 0 } };

        // TODO 2: Omoguciti vodjenje evidencije o plavim i narandzastim kutijama.
        private List<int> plaveKutije = new List<int>();        // cuvamo kutiju na hesh-u 10 * x_kord + y_kord
        private List<int> narandzasteKutije = new List<int>();  // cuvamo kutiju na hesh-u 10 * x_kord + y_kord
        public static int potrebnoPlavih = 3;
        public static int potrebnoNarandzastih = 2;
        /*
         * U svakom elementu liste npr plaveKutije imamo hesh koji predstavlja to polje.
         * Svako polje ima svoj hesh.
         */

        


        public State sledeceStanje(int markI, int markJ)
        {
            State rez = new State();
            rez.markI = markI;
            rez.markJ = markJ;
            rez.parent = this;
            rez.cost = this.cost + 1;
            rez.level = this.level + 1;

            // TODO 4: Implementirati kupljenje kutija.


            // TODO 100: probati ovo prebaciti da imamo neku static public koju cemo samo dekrementovati 
            // dok se ne smanji na nulu i to je to.

            // Cuvamo skupljene plave kutije u igri, za svako sledece stanje.
            foreach (int hesh in this.plaveKutije)
            {
                rez.plaveKutije.Add(hesh);

            }

            
            // Provera da li je trenutno polje nova plava kutija
            if( lavirint[markI, markJ] == 4 && !rez.plaveKutije.Contains(10*markI +markJ))
            {  // u pitanju je polj 4(plava) i hesh te tog polja nije vec u plavimKutijama.
               // da ne bi pokupili istu kutiju vise puta !
                rez.plaveKutije.Add(10 * markI + markJ);

            }

            // TODO 4.1: Omoguciti redosled kupljenja [ plave pre narandzastih ]
            // Ako su plave skupljene, tek onda omoguciti skupljanje narandzastih.
            if(rez.plaveKutije.Count >= potrebnoPlavih)
            {
                // Cuvamo hesh prethodno skupljenih narandzastih kutija
                // i tako za svako sledece stanje.
                foreach(int hesh in this.narandzasteKutije)
                {
                    rez.narandzasteKutije.Add(hesh);
                }

                // Proveravamo da li je trenutno polje nova narandzasta kutija.
                if(lavirint[markI,markJ] == 5 && !rez.narandzasteKutije.Contains(10 * markI + markJ))
                {  // ako je u pitanju naradzasta i nije vec pokupljena, mozemo je dodati
                    rez.narandzasteKutije.Add(10 * markI + markJ);
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
            // TODO 3: Implementirati metodu tako da odredjuje moguca sledeca stanja
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
            // TODO 5: Omoguciti prolazak kroz isto polje ako smo sad pokupili kutiju.

            // Dozvola vracanja po istom polju samo ako imamo kutiju(pokupili smo je)
            // hash - predstvlja jedinstvenu vrednost jednog polja u matrici
            int hash = 10 * markI + markJ;
            int koeficijentHesha = 10;
            /*
             * Sada imamo ukupno 5 kutija. Sto znaci, da
             * moramo omoguciti da kroz neko polje mozemo proci
             * 5 puta. To cemo omoguciti tako sto, za svaku kutiju
             * koju je pokupio, mi promenimo hash za to polje.
             * Odnosno svakom novom kutijom on dobija mogucnost 
             * prolaska kroz to polje.
             * 
             */
            // Oznacavamo kutije koje su koriscene.
            foreach (int polje in this.plaveKutije)
            {
                hash += polje * koeficijentHesha;
                koeficijentHesha *= 10;
            }
            foreach(int polje in this.narandzasteKutije)
            {
                hash += polje * koeficijentHesha;
                koeficijentHesha *= 10;
            }


            return hash;

        }

        public bool isKrajnjeStanje()
        {
            // TODO 6: Prosiriti proveru za krajnje stanje.
            if (plaveKutije.Count < potrebnoPlavih)
                return false;
            if (narandzasteKutije.Count < potrebnoNarandzastih)
                return false;
           
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
