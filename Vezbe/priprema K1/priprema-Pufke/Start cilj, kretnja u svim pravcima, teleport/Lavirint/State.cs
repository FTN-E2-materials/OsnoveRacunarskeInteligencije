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


        public State sledeceStanje(int markI, int markJ)
        {
            State rez = new State();
            rez.markI = markI;
            rez.markJ = markJ;
            rez.parent = this;
            rez.cost = this.cost + 1;
            return rez;
        }

        
        public List<State> mogucaSledecaStanja()
        {
            List<State> rez = new List<State>();

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
            //Kretnja dole desno
            if ((markI < Main.brojVrsta - 1) && (lavirint[markI + 1, markJ +1] != 1) && (markJ < Main.brojKolona - 1))
            {
                rez.Add(sledeceStanje(markI + 1, markJ+1));
            }
            //Kretnja gore levo
            if ((markI > 0) && (lavirint[markI - 1, markJ - 1] != 1) && (markJ > 0))
            {
                rez.Add(sledeceStanje(markI - 1, markJ - 1));
            }
            //Kretanje gore desno (MarkI smanjivati, markJ povecavati)
            if ((markI > 0) && (lavirint[markI - 1, markJ +1] != 1) && (markJ < Main.brojKolona - 1))
            {
                rez.Add(sledeceStanje(markI - 1, markJ +1));
            }
            //Kretnja dole levo (MarkI povecavati, MarkJ smanjivati)
            if ((markI < Main.brojVrsta - 1) && (lavirint[markI + 1, markJ - 1] != 1) && (markJ > 0))
            {
                rez.Add(sledeceStanje(markI + 1, markJ-1));
            }


            //u ovoj for petlji proveravamo da li smo naisli na teleport
            for (int i = 0; i < Main.teleport.Count; i++)
            {    // da li imamo teleport
                if (markI == Main.teleport[i].markI && markJ == Main.teleport[i].markJ)
                {
                    for (int j = 0; j < Main.teleport.Count; j++)
                    {
                        State s = sledeceStanje(Main.teleport[j].markI, Main.teleport[j].markJ);
                        rez.Add(s);
                    }
                }
            }

            return rez;
        }

        public override int GetHashCode()
        {
            int code = 10 * markI + markJ; 
            return code; 
        }

        public bool isKrajnjeStanje()
        { //Proveravamo i da li smo pokupili kutiju to je uslov && kutija
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
