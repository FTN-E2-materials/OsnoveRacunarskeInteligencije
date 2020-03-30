using System;
using System.Collections.Generic;
using System.Drawing;
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
        public bool pokupljenaPlava;
        public bool pokupljenaNarandzasta;
        // blokiranje mogucnosti da robot kada izadje iz portala, odmah prodje nazad kroz taj portal
        public bool izasaoIzPortala = false;

       


        
        public State sledeceStanje(int markI, int markJ)
        {
            State rez = new State();
            rez.markI = markI;
            rez.markJ = markJ;
            rez.parent = this;
            rez.cost = this.cost + 1;
            rez.level = this.level + 1;


            // TODO 3: Implementirati proveru da li su trazene kutije pokupljenje. 

            // skupljamo plavu prvo
            if(this.pokupljenaPlava || (lavirint[rez.markI, rez.markJ] == 4)){
                rez.pokupljenaPlava = true;
            }

            // skupljamo narandzastu, ali samo ako je plava vec pokupljena !
            if (rez.pokupljenaPlava)
            {
                if(this.pokupljenaNarandzasta || (lavirint[rez.markI,rez.markJ] == 5))
                {
                    rez.pokupljenaNarandzasta = true;
                }
            }
            else
            {
                rez.pokupljenaNarandzasta = false;
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

            // TODO 4: Implementirati metodu koja odredjuje dozvoljena kretanja u lavirintu.
            /*
             * Ova ideja je zasnovana na tome da robot moze da bira hoce li da prodje kroz portal,
             * tj. kada je na portalu, moze i samo da ga predje.( kao u igrici Mario, da upadne u portal
             * ili da nastavi dalje)
             */
            List<State> validnaSledecaStanja = new List<State>();

            for(int i = 0; i < koraci.GetLength(0); i++)
            {
                int novoI = markI + koraci[i, 0];
                int novoJ = markJ + koraci[i, 1];

                if (validneKordinate(novoI, novoJ))
                {
                    /* Dodajemo sad ovo jer moramo postaviti da nije izasao iz portala bas ovde
                     * a ne u metodi sledeceStanje[kako ne bi za svako sledeceStanje stavljali
                     * da nije izasaoIzPortala].
                    */
                    State novoStanje = sledeceStanje(novoI, novoJ);
                    novoStanje.izasaoIzPortala = false;
                    validnaSledecaStanja.Add(novoStanje);
                }

                // Prolazak kroz portal.
                if(lavirint[markI,markJ] == 6 && izasaoIzPortala == false)
                {
                    // Moguca sledeca stanja su svi portali osim njega samog
                    foreach(Point point in Main.portali)
                    {
                        // Preskacemo portal kroz koji smo usli.
                        if(markI == point.X && markJ == point.Y)
                        {
                            continue;
                        }

                        State novoStanje = sledeceStanje(point.X, point.Y);
                        novoStanje.izasaoIzPortala = true;
                        validnaSledecaStanja.Add(novoStanje);

                    }
                }
            }

            return validnaSledecaStanja;
        }

        public override int GetHashCode()
        {
            // TODO 5: Hesirati polja kad imamo odredjenu kutiju
            // kako bi odredjeno polje mogao opet da prodje kad ide sa kutijom(pokupljenom kutijom)
            // hash - predstvlja jedinstvenu vrednost jednog polja u matrici
            int hash = 100 * markI + markJ;
            if (pokupljenaPlava)
                hash += 1000;
            if (pokupljenaNarandzasta)
                hash += 10000;

            return hash;

        }

        public bool isKrajnjeStanje()
        {
            // TODO 6: Izmena provere da li je krajnje stanje.
            return Main.krajnjeStanje.markI == markI && Main.krajnjeStanje.markJ == markJ && pokupljenaPlava && pokupljenaNarandzasta;
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
