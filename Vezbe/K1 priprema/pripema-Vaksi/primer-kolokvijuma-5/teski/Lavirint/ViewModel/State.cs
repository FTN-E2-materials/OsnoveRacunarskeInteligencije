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
        private static int[,] kralj = { { 1, 1 }, { -1, 1 }, { 1, -1 }, { -1, -1 }, { 1, 0 }, { -1, 0 }, { 0, 1 }, { 0, -1 } };
        private static int[,] top = { { 0, 1 }, { 0, -1 }, { -1, 0 }, { 1, 0 } };

        private List<int> pokupljenjeKutije = new List<int>();

        private bool rolamDzipom;

        private List<Senzor> senzoriIgre;

        private bool zabranaKretanja = false;

        private bool podAlarmom;
        private Senzor aktivanSenzor;                                       // kako bih znao u kom sam senzoru

        private List<int> deaktivatori = new List<int>();                   // potrebno ih je 2 pokupiti da bi ugasio alarm

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
            rez.pokupljenjeKutije.AddRange(this.pokupljenjeKutije);
            rez.rolamDzipom = this.rolamDzipom;
            
            // Postavim senzore, a potom preuzmem stanja senzora iz prethodnog poteza
            rez.senzoriIgre = postaviSenzore();
            if (!(preuzmiPrethodneSenzore() is null))
                rez.senzoriIgre = preuzmiPrethodneSenzore();

            rez.podAlarmom = this.podAlarmom;

            // preuzimanje senzora ako imamo
            if(!(this.aktivanSenzor is null))
            {
                rez.aktivanSenzor = new Senzor(this.aktivanSenzor.KordinataX, this.aktivanSenzor.KordinataY);
            }
            

            rez.deaktivatori.AddRange(this.deaktivatori);

            // Dzip
            if(lavirint[markI, markJ] == 4)
            {
                rez.rolamDzipom = true;
            }


            // Mocvara
            if(lavirint[markI, markJ] == 5)
            {
                // Ako nemam dzip, tesko se krecem po mocvari
                if(!rez.rolamDzipom)
                    rez.cost += 2;
            }

            // Senzori
            foreach (Senzor senzor in rez.senzoriIgre)
            {
                if (!senzor.Aktivan)
                    continue;

                List<int> poljaPodSenzorom = senzor.getPoljaPodSenzorom();

                if (!rez.podAlarmom)
                {   // ako nisam pod alarmom, znaci mogu upasti u alarm
                    // i lagano se krecem gde hocu

                    if (poljaPodSenzorom.Contains(10 * markI + markJ))
                    {
                        // Upao sam u alarm (reon senzora)
                        // Sada treba da pokupim 2 deaktivaciona dugmica i izadjem odavde

                        // Kako bih zabranio da izlazi van opsega alarma dok ne pokupi
                        // deaktivatore, dizem fleg podAlarmom 
                        rez.podAlarmom = true;
                        rez.aktivanSenzor = new Senzor(senzor.KordinataX, senzor.KordinataY);
                        //rez.zabranaKretanja = true;
                    }
                }
                else
                {   // definitivno sam pod alarmom
                    // i validne kordinate su samo kordinate ovog alarma

                    // Ako se nalazimo u alarmu koji nije zapravo aktivan, preskacemo taj alarm
                    if (!(senzor.KordinataX == rez.aktivanSenzor.KordinataX && senzor.KordinataY == rez.aktivanSenzor.KordinataY))
                        continue;

                    // Ako je kordinata neka koja nije iz polja pod senzorom, nju necemo dodati
                    if (!poljaPodSenzorom.Contains(10 * markI + markJ))
                        return null;

                    // Deaktivatori alarma
                    if (lavirint[markI, markJ] == 7 && !this.deaktivatori.Contains(10 * markI + markJ))
                    {
                        // Kada ih sakupim 2 mogu izaci iz reona alarma
                        rez.deaktivatori.Add(10 * markI + markJ);
                        rez.pokupljenjeKutije.Add(10 * markI + markJ);
                        if(rez.deaktivatori.Count == 2)
                        {
                            rez.deaktivatori = new List<int>();
                            rez.aktivanSenzor = null;
                            senzor.Aktivan = false;
                            rez.podAlarmom = false;
                            
                        }
                    }
                    
 
                }

                

            }

            if (rez.podAlarmom)
            {
                zabranaKretanja = !zabranaKretanja;
            }


            return rez;
        }

        
        // TODO: Ovde odredjujemo moguca sledeca kretanja
        // Ako se nista posebno ne trazi, ovo je dovoljno.
        public List<State> mogucaSledecaStanja()
        {

            
            List<State> validnaSledecaStanja = new List<State>();

            if (!(zabranaKretanja && podAlarmom))
            {

                int[,] koraci = null;
                bool jednoPoteznaFigura = true;                     // u zavisnosti mogucnosti kretanja figure, podesavam ovaj parametar

                //TODO: U zavisnosti od uslova menjam korake
                if (this.rolamDzipom)
                {
                    koraci = top;
                    jednoPoteznaFigura = false;
                }
                else
                {
                    koraci = kralj;
                    jednoPoteznaFigura = true;
                }


                for (int i = 0; i < koraci.GetLength(0); i++)
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

            }
            else
            {
                // ako je zabranjeno kretanje, validno stanje je samo trenutno 
                validnaSledecaStanja.Add(this);
            }
            
            return validnaSledecaStanja;
        }

        // TODO: Ovde odredjujemo koji je hash code
        public override int GetHashCode()
        {
            int hash = 10 * markI + markJ;
            int nivoFrekvencije = 100;
            foreach (int hashPokupljeneKutije in this.pokupljenjeKutije)
            {
                hash += nivoFrekvencije + hashPokupljeneKutije;
                nivoFrekvencije += 100;
            }

            if (podAlarmom)
            {
                hash += 1000;
            }

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


        private List<Senzor> postaviSenzore()
        {
            List<Senzor> senzori = new List<Senzor>();
            foreach (Senzor protivnik in Main.senzori)
            {
                Senzor noviProtivnik = new Senzor(protivnik.KordinataX, protivnik.KordinataY, true, 2);
                senzori.Add(noviProtivnik);

            }
            return senzori;

        }

        private List<Senzor> preuzmiPrethodneSenzore()
        {
            List<Senzor> senzori = new List<Senzor>();
            if (this.senzoriIgre is null)
                return null;
            foreach (Senzor protivnik in this.senzoriIgre)
            {
                Senzor noviProtivnik = new Senzor(protivnik.KordinataX, protivnik.KordinataY, protivnik.Aktivan, protivnik.ReonSenzora);
                senzori.Add(noviProtivnik);

            }
            return senzori;

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
