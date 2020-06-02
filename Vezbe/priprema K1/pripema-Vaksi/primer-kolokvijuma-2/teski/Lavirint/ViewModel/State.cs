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
        private static int[,] kraljica = { { 1, 1 }, { -1, 1 }, { 1, -1 }, { -1, -1 }, { 1, 0 }, { -1, 0 }, { 0, 1 }, { 0, -1 } };
        private static int[,] kralj = { { 1, 1 }, { -1, 1 }, { 1, -1 }, { -1, -1 }, { 1, 0 }, { -1, 0 }, { 0, 1 }, { 0, -1 } };

        private List<int> pokupljeneKutije = new List<int>();
        private int pokupljenoPlavih = 0;
        private int pokupljenoZutih = 0;


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
            rez.pokupljeneKutije.AddRange(this.pokupljeneKutije);
            rez.pokupljenoPlavih = this.pokupljenoPlavih;
            rez.pokupljenoZutih = this.pokupljenoZutih;

            if(lavirint[markI, markJ] == 4 && !this.pokupljeneKutije.Contains(10 * markI + markJ))
            {
                // Dozvola kupljenja samo za razliku manju/jednaku od 1, tj kako se ne bi desilo da imamo 2 i onda dodamo
                // pa imamo 3 vise plave [ zabranjeno po pravilu igre ]
                // sada razlika moze otici max na 2 [ sto je trazeno u pravilu igre ]
                if( (rez.pokupljenoPlavih - rez.pokupljenoZutih) <= 1)
                {
                    rez.pokupljeneKutije.Add(10 * markI + markJ);
                    rez.pokupljenoPlavih++;
                }
                else
                {
                    return null;
                }
                
            }

            if (lavirint[markI, markJ] == 5 && !this.pokupljeneKutije.Contains(10 * markI + markJ))
            {
                // Dozvola kupljenja samo u slucaju kada je razlika veca jednaka 2
                // jer se onda razlika smanjuje na 1 posto kupimo zutu
                if ((rez.pokupljenoPlavih - rez.pokupljenoZutih) >= 2)
                {
                    rez.pokupljeneKutije.Add(10 * markI + markJ);
                    rez.pokupljenoZutih++;
                }
                else
                {
                    return null;
                }

            }
            
            return rez;
        }

        
        // TODO: Ovde odredjujemo moguca sledeca kretanja
        // Ako se nista posebno ne trazi, ovo je dovoljno.
        public List<State> mogucaSledecaStanja()
        {
            List<State> validnaSledecaStanja = new List<State>();
            int[,] koraci = null;
            bool jednoPoteznaFigura = false;

            //TODO: U zavisnosti od uslova menjam korake
            if (this.pokupljenoZutih >= 1)
            {
                koraci = kralj;
                jednoPoteznaFigura = true;
            }
            else
            {
                koraci = kraljica;
            }
            

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
                    State validnoStanje = new State();
                    validnoStanje = sledeceStanje(novoI, novoJ);

                    // Kako bih obisao zapravo samo ona na koja mogu stati
                    // voditi racuna o ovome !
                    if (!(validnoStanje is null))
                        validnaSledecaStanja.Add(validnoStanje);
                    else
                        break;

                    // Kraj igre kada je u pitanju samo jedno potezna figura
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
            int nivoFrekvencije = 100;
            foreach (int hashPokupljeneKutije in this.pokupljeneKutije)
            {
                hash += nivoFrekvencije + hashPokupljeneKutije;
                nivoFrekvencije += 1000;
            }
            return hash;
        }

        // TODO: Ovde menjamo kada se krajnje stanje uslovljava i zavisi od necega
        public bool isKrajnjeStanje()
        {
            // Ako imam vise zutih ili jednako to nije definitivno valjano stanje za kraj
            // razlika mora biti bar 1 da bi bio kraj
            if((this.pokupljenoPlavih - this.pokupljenoZutih) <= 0)
            {
                return false;
            }

            // Bar jedna zuta mora biti da bi bilo true
            if(this.pokupljenoZutih <= 0)
            {
                return false;
            }

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
