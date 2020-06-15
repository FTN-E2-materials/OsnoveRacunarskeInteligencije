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
        public bool kutija1;
        public bool kutija2;
        public bool skupljeneObeKutije;


        public State sledeceStanje(int markI, int markJ)
        {
            State rez = new State();
            rez.markI = markI;
            rez.markJ = markJ;
            rez.parent = this;
            rez.cost = this.cost + 1;
            rez.kutija1 = this.kutija1;
            rez.kutija2 = this.kutija2;
            rez.skupljeneObeKutije = this.skupljeneObeKutije;
            return rez;
        }

        
        public List<State> mogucaSledecaStanja()
        {
            //TODO 1: Implementirati metodu tako da odredjuje dozvoljeno kretanje u lavirintu
            //TODO 2: Prosiriti metodu tako da se ne moze prolaziti kroz sive kutije
            List<State> rez = new List<State>();
     
            if (lavirint[markI, markJ] == 4 && Main.brojSkupljenihPlavihKutija == 0){
                 kutija1 = true;
                 Main.brojSkupljenihPlavihKutija++;
                 Main.pozicijaIkutije1 = markI;
                 Main.pozicijaJkutije1 = markJ;
            }

            if (lavirint[markI, markJ] == 4 && kutija1 == true && (markI != Main.pozicijaIkutije1 || markJ != Main.pozicijaJkutije1) )
            {
                kutija2 = true;

            }

            if (kutija1 && kutija2) {
                skupljeneObeKutije = true;
            }
      

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

            return rez;
        }

        public override int GetHashCode()
        {
            int code = 10 * markI + markJ; //Ako nemamo kutiju ti ce nam biti od 0-200
            if (this.kutija1 == true)
            {
                code += 1000;
            }
            if (this.kutija2 == true)
            {
                code += 2000;
            }
            if(skupljeneObeKutije == true)
            {
                code += 3000;
            }
            return code;


        }

        public bool isKrajnjeStanje()
        {  //Za skupljanje kutije smo dodali uslov "&& kutija"
            return Main.krajnjeStanje.markI == markI && Main.krajnjeStanje.markJ == markJ && skupljeneObeKutije;
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
