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
        public bool kutija3;
        public bool skupljeneSveKutije;
        public bool skupljeneSvePlave;
        public bool zutaKutija1;
        public bool zutaKutija2;


        public State sledeceStanje(int markI, int markJ)
        {
            State rez = new State();
            rez.markI = markI;
            rez.markJ = markJ;
            rez.parent = this;
            rez.cost = this.cost + 1;
            rez.kutija1 = this.kutija1;
            rez.kutija2 = this.kutija2;
            rez.kutija3 = this.kutija3;
            rez.skupljeneSvePlave = this.skupljeneSvePlave;
            rez.zutaKutija1 = this.zutaKutija1;
            rez.zutaKutija2 = this.zutaKutija2;
            rez.skupljeneSveKutije = this.skupljeneSveKutije;
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

            if (lavirint[markI, markJ] == 4 && kutija1 && markI != Main.pozicijaIkutije1 && markJ != Main.pozicijaJkutije1 &&  Main.brojSkupljenihPlavihKutija == 1)
            {
                kutija2 = true;
                Main.brojSkupljenihPlavihKutija++;
                Main.pozicijaIkutije2 = markI;
                Main.pozicijaJkutije2 = markJ;

            }

            if (lavirint[markI, markJ] == 4 && kutija1 && kutija2 && markI != Main.pozicijaIkutije1 && markJ != Main.pozicijaJkutije1 && markI != Main.pozicijaIkutije2 && markJ != Main.pozicijaJkutije2 )
            {
                Main.brojSkupljenihPlavihKutija++;
                kutija3 = true;
                
            }

            if (kutija1 && kutija2 && kutija3)
            {
                skupljeneSvePlave = true;
            }

            if (lavirint[markI, markJ] == 5 && Main.brojSkupljenihZutihKutija == 0 && skupljeneSvePlave)
            {
                zutaKutija1 = true;
                Main.brojSkupljenihZutihKutija++;
                Main.pozicijaIZutekutije1 = markI;
                Main.pozicijaJZutekutije1 = markJ;
            }

            if (lavirint[markI, markJ] == 5 && zutaKutija1  && markI != Main.pozicijaIZutekutije1 && markJ != Main.pozicijaJZutekutije1 && skupljeneSvePlave)
            {
                zutaKutija2 = true;
                Main.brojSkupljenihZutihKutija++;

            }

            if (skupljeneSvePlave  && zutaKutija1 && zutaKutija2) {
                skupljeneSveKutije = true;
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
            if ((markI > 0)&& (markJ > 0) && (lavirint[markI - 1, markJ - 1] != 1))
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
            if (this.kutija1)
            {
                code += 1000;
            }
            if (this.kutija2)
            {
                code += 2000;
            }
            if (this.kutija3)
            {
                code += 3000;
            }

           
            if (this.skupljeneSvePlave)
            {
                code += 6000;
            }     
            if (this.zutaKutija1)
            {
                code += 7000;
            }
            if (this.zutaKutija2)
            {
                code += 8000;
            }
            if (this.skupljeneSveKutije)
            {
                code += 9000;
            }

            return code;


        }

        public bool isKrajnjeStanje()
        {  //Za skupljanje kutije smo dodali uslov "&& kutija"
            return Main.krajnjeStanje.markI == markI && Main.krajnjeStanje.markJ == markJ && skupljeneSveKutije;
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
