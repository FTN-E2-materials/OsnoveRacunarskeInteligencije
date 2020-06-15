using System;
using System.Collections.Generic;
using System.Text;

namespace Lavirint
{
    public class State
    {
        public static int[,] lavirint;
        State parent;
        public int markI, markJ;
        public double cost;
        public bool isprobanoBlato;
        public bool isprobanaZica;
        public int brojSkokova = 0;
        private List<int> listaSakupljenihKutija = new List<int>();
      
        public State sledeceStanje(int markI, int markJ)
        {
            State rez = new State();
            rez.markI = markI;
            rez.markJ = markJ;
            rez.parent = this;
            rez.cost = this.cost + 1;
            rez.isprobanoBlato = this.isprobanoBlato;
            rez.isprobanaZica = this.isprobanaZica;
            rez.brojSkokova = this.brojSkokova;
            rez.listaSakupljenihKutija.AddRange(this.listaSakupljenihKutija);
            return rez;
        }

        //Perica mora da proba sve prepreke pre cilja 
        public List<State> mogucaSledecaStanja()
        {
            List<State> rez = new List<State>();

            //Logika za blato
            if (lavirint[markI, markJ] == 4 && !this.listaSakupljenihKutija.Contains(100 * markI + markJ)){
                 this.isprobanoBlato = true;
                 this.listaSakupljenihKutija.Add(100 * markI + markJ);
            }

            //Logika za Zicu
            if (lavirint[markI, markJ] == 5 && !this.listaSakupljenihKutija.Contains(100 * markI + markJ))
            {
                this.isprobanaZica = true;
                this.listaSakupljenihKutija.Add(100 * markI + markJ);
            }

            if (brojSkokova < 3) {//Kretnja kao Konj skakac
                brojSkokova++;
                    for (int i = -2; i <= 2; i += 4) {                                       //Ove dve for petlje nam daju kombinaciju kretanja levo desno za dva polja i gore dole za jedno polje
                        int newMarkI = markI + i;
                        for (int j = -1; j <= 1; j += 2) {
                            int newMarkJ = markJ + j;
                            if (newMarkI >= 0 && newMarkI < Main.brojVrsta) {
                                if (newMarkJ >= 0 && newMarkJ < Main.brojKolona){
                                    if (lavirint[newMarkI, newMarkJ] != 1) {
                                        rez.Add(sledeceStanje(newMarkI, newMarkJ));
                                    }
                                }
                            }
                        }
                    }

                    for (int j = -2; j <= 2; j += 4){                                        //Ove dve for petlje nam daju kombinaciju kretanja gore dole za dva polja i levo desno za jedno polje
                        int newMarkJ = markJ + j;
                        for (int i = -1; i <= 1; i += 2){
                            int newMarkI = markI + i;
                            if (newMarkI >= 0 && newMarkI < Main.brojVrsta){
                                if (newMarkJ >= 0 && newMarkJ < Main.brojKolona){
                                    if (lavirint[newMarkI, newMarkJ] != 1) {
                                        rez.Add(sledeceStanje(newMarkI, newMarkJ));
                                    }
                                }
                            }
                        }
                    }
            }else { //Lagana kretnja gore dole levo desno
         
                    if ((markJ > 0) && (lavirint[markI, markJ - 1] != 1)){
                        rez.Add(sledeceStanje(markI, markJ - 1));
                    }

                    if ((markJ < Main.brojKolona - 1) && (lavirint[markI, markJ + 1] != 1)){
                        rez.Add(sledeceStanje(markI, markJ + 1));
                    }

                    if ((markI > 0) && (lavirint[markI - 1, markJ] != 1)){
                        rez.Add(sledeceStanje(markI - 1, markJ));
                    }

                    if ((markI < Main.brojVrsta - 1) && (lavirint[markI + 1, markJ] != 1)){
                        rez.Add(sledeceStanje(markI + 1, markJ));
                    }
            }
           

            return rez;
        }

        public override int GetHashCode()
        {
            int code = 10 * markI + markJ;
           
            foreach (int codeSakupljenogPolja in this.listaSakupljenihKutija){
                code += codeSakupljenogPolja;
            }

            return code;
        }

        public bool isKrajnjeStanje()
        { 
            return Main.krajnjeStanje.markI == markI && Main.krajnjeStanje.markJ == markJ && this.isprobanaZica && this.isprobanoBlato;
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
