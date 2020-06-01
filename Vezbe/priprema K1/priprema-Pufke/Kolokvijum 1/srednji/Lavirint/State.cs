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
        public int skupljenBrojPoena;
        private List<int> listaSakupljenihKutija = new List<int>();
      
        public State sledeceStanje(int markI, int markJ)
        {
            State rez = new State();
            rez.markI = markI;
            rez.markJ = markJ;
            rez.parent = this;
            rez.cost = this.cost + 1;
            rez.skupljenBrojPoena = this.skupljenBrojPoena;
            rez.listaSakupljenihKutija.AddRange(this.listaSakupljenihKutija);
            return rez;
        }

        
        public List<State> mogucaSledecaStanja()
        {

            List<State> rez = new List<State>();

                //Logika za kutije
                if (lavirint[markI, markJ] == 4 && !this.listaSakupljenihKutija.Contains(100 * markI + markJ))
                {
                    this.skupljenBrojPoena += 3;
                    this.listaSakupljenihKutija.Add(100 * markI + markJ);
                }

                if (lavirint[markI, markJ] == 5 && !this.listaSakupljenihKutija.Contains(100 * markI + markJ))
                {
                  this.skupljenBrojPoena += 1;
                  this.listaSakupljenihKutija.Add(100 * markI + markJ);
                }
            
                //Kretnja
                if ((markJ > 0) && (lavirint[markI, markJ - 1] != 1))
                {
                    rez.Add(sledeceStanje(markI, markJ - 1));
                }

                if ((markJ < Main.brojKolona - 1) && (lavirint[markI, markJ + 1] != 1) )
                {
                    rez.Add(sledeceStanje(markI, markJ + 1));
                }

                if ((markI > 0) && (lavirint[markI - 1, markJ] != 1) )
                {
                    rez.Add(sledeceStanje(markI - 1, markJ));
                }

                if ((markI < Main.brojVrsta - 1) && (lavirint[markI + 1, markJ] != 1) )
                {
                    rez.Add(sledeceStanje(markI + 1, markJ));
                }
         

            return rez;
        }

        public override int GetHashCode()
        {
            int code = 10 * markI + markJ;
         
            foreach (int codeSakupljenogPolja in this.listaSakupljenihKutija)
            {
                code += codeSakupljenogPolja;
            }
            return code;
        }

        public bool isKrajnjeStanje()
        { 
            return Main.krajnjeStanje.markI == markI && Main.krajnjeStanje.markJ == markJ && this.skupljenBrojPoena ==10;
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
