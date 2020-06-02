using Lavirint.Model;
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

        private List<int> pokupljeneKutije = new List<int>();
        private bool pokupljenaZastavica = false;

        private int pokupljenoPlavih = 0;                               // plava kutija predstavlja municiju

        private List<Senzor> mojiProtivnici;
        
        private List<Senzor> postaviMojeProtivnike()
        {
            List<Senzor> protivnici = new List<Senzor>();
            foreach (Senzor protivnik in Main.protivnici)
            {
                Senzor noviProtivnik = new Senzor(protivnik.KordinataX, protivnik.KordinataY, true, 2);
                protivnici.Add(noviProtivnik);

            }
            return protivnici;

        }

        private List<Senzor> preuzmiMojePrethodneProtivnike()
        {
            List<Senzor> protivnici = new List<Senzor>();
            if (this.mojiProtivnici is null)
                return null;
            foreach (Senzor protivnik in this.mojiProtivnici)
            {
                Senzor noviProtivnik = new Senzor(protivnik.KordinataX, protivnik.KordinataY, protivnik.Aktivan, protivnik.ReonSenzora);
                protivnici.Add(noviProtivnik);

            }
            return protivnici;

        }

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
            rez.pokupljenaZastavica = this.pokupljenaZastavica;
            rez.pokupljenoPlavih = this.pokupljenoPlavih;
            rez.mojiProtivnici = postaviMojeProtivnike();
            if(!(preuzmiMojePrethodneProtivnike() is null))
                rez.mojiProtivnici = preuzmiMojePrethodneProtivnike();

            foreach (Senzor protivnik in rez.mojiProtivnici)
            {
                // Samo su bitni aktivirani senzori(zivi neprijatelji)
                if (!protivnik.Aktivan)
                    continue;

                List<int> poljaPodSenzorom = protivnik.getPoljaPodSenzorom();
                if(poljaPodSenzorom.Contains(10*markI + markJ))
                {   // ovo stanje ne moze biti u opticaju jer se nalazi u reonu senzora !
                    return null;            // zbog toga vracam null
                }
                
            }

            /*
             * Da bih ubio neprijatelja, moram imati bar jednu plavu
             * i mora mi biti na dijagonali, vertikali, horizontali
             */

            // ZASTAVICA
            if(lavirint[markI, markJ] == 3 && !this.pokupljeneKutije.Contains(10 * markI + markJ))
            {
                rez.pokupljeneKutije.Add(10 * markI + markJ);
                rez.pokupljenaZastavica = true;
            }

            // MUNICIJA
            if (lavirint[markI, markJ] == 4 && !this.pokupljeneKutije.Contains(10 * markI + markJ))
            {
                rez.pokupljeneKutije.Add(10 * markI + markJ);
                rez.pokupljenoPlavih++;
            }

            // Atentat
            if (rez.pokupljenoPlavih >= 1 && neprijateljNaNisanu())
            {
                foreach (Senzor protivnik in rez.mojiProtivnici)
                {
                    // TODO: Ovde optimizovati koga ce da ubije
                    // TODO: Ovde resiti kad sme da puca
                    // odnosno ko mu najvise smeta do cilja
                    protivnik.Aktivan = false;
                    break;
                }
                rez.pokupljenoPlavih--;
                pucaj(markI, markI);
            }


            return rez;
        }

        private void pucaj(int i, int j)
        {
            Console.WriteLine("\n------- BAM --------");
            Console.WriteLine("\nPuc puc, ozezi ozezi");
            Console.WriteLine("Pucao sa kordinata: " + "(" + i + ", " + j + ")");
            Console.WriteLine("\n------- Dead -------\n");
        }

        private bool neprijateljNaNisanu()
        {
            return true;
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
                    State validnoStanje = new State();
                    validnoStanje = sledeceStanje(novoI, novoJ);

                    // Kako bih obisao zapravo samo ona na koja mogu stati
                    // voditi racuna o ovome !
                    if (!(validnoStanje is null))
                        validnaSledecaStanja.Add(validnoStanje);
                    else
                        break;

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
            int nivoFrekvencije = 100;
            foreach (int hashPokupljeneKutije in this.pokupljeneKutije)
            {
                hash += nivoFrekvencije + hashPokupljeneKutije;
                nivoFrekvencije += 100;
            }
            return hash;
        }

        // TODO: Ovde menjamo kada se krajnje stanje uslovljava i zavisi od necega
        public bool isKrajnjeStanje()
        {
            if (!this.pokupljenaZastavica) { 
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
