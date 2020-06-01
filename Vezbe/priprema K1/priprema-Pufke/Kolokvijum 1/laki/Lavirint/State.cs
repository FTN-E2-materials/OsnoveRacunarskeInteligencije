using System;
using System.Collections.Generic;
using System.Text;

namespace Lavirint
{
    public class State
    {
        public static int[,] lavirint; //Staticka promenljiva koaj opisuje ceo nas lavirint i ne moramo vise da pristupamo Mainu svaki put
        State parent;
        public int markI, markJ; //vrsta i kolona
        public double cost;
        public bool kutija;

        public State sledeceStanje(int markI, int markJ)
        {
            State rez = new State();
            rez.markI = markI;
            rez.markJ = markJ;
            rez.parent = this;
            rez.cost = this.cost + 1;
            rez.kutija = this.kutija;
            return rez;
        }

        
        public List<State> mogucaSledecaStanja()
        {

            List<State> rez = new List<State>();

            //Dodajemo za PLAVU kutiju
            if(lavirint[markI, markJ] == 4){
                kutija = true;
            }

            if (kutija) //Kretanja jedno polje dijagonalno nakon skupljene kutije
            {
                //Kretnja dole desno
                if ((markI < Main.brojVrsta - 1) && (markJ < Main.brojKolona - 1) && (lavirint[markI + 1, markJ + 1] != 1))
                {
                    rez.Add(sledeceStanje(markI + 1, markJ + 1));
                }
                //Kretnja gore levo
                if ((markI > 0) && (markJ > 0) && (lavirint[markI - 1, markJ - 1] != 1))
                {
                    rez.Add(sledeceStanje(markI - 1, markJ - 1));
                }
                //Kretanje gore desno (MarkI smanjivati, markJ povecavati)
                if ((markI > 0) && (markJ < Main.brojKolona - 1) && (lavirint[markI - 1, markJ + 1] != 1))
                {
                    rez.Add(sledeceStanje(markI - 1, markJ + 1));
                }
                //Kretnja dole levo (MarkI povecavati, MarkJ smanjivati)
                if ((markI < Main.brojVrsta - 1) && (markJ > 0) && (lavirint[markI + 1, markJ - 1] != 1))
                {
                    rez.Add(sledeceStanje(markI + 1, markJ - 1));
                }
            }

            if (!kutija) //Kretanja jedno polje gore-dole-levo-desno pre skupljene kutije
            {
                if ((markJ > 0) && (lavirint[markI, markJ - 1] != 1))
                {
                    rez.Add(sledeceStanje(markI, markJ - 1));
                }

                if ((markJ < Main.brojKolona - 1) && (lavirint[markI, markJ + 1] != 1))
                {
                    rez.Add(sledeceStanje(markI, markJ + 1));
                }

                if ((markI > 0) && (lavirint[markI - 1, markJ] != 1))
                {
                    rez.Add(sledeceStanje(markI - 1, markJ));
                }

                if ((markI < Main.brojVrsta - 1) && (lavirint[markI + 1, markJ] != 1))
                {
                    rez.Add(sledeceStanje(markI + 1, markJ));
                }
            }

            return rez;
        }

        public override int GetHashCode()
        {
            int code = 10 * markI + markJ; //Ako nemamo kutiju ti ce nam biti od 0-200
            return kutija ? code + 1000 : code; //Ako imamo kutiju taj kod nam ide dalje 
        }

        public bool isKrajnjeStanje()
        { //Proveravamo i da li smo pokupili kutiju to je uslov && kutija
            return Main.krajnjeStanje.markI == markI && Main.krajnjeStanje.markJ == markJ && kutija;
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
