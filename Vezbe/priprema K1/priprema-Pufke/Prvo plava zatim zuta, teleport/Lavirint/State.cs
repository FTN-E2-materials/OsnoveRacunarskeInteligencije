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

        public State sledeceStanje(int markI, int markJ)
        {
            State rez = new State();
            rez.markI = markI;
            rez.markJ = markJ;
            rez.parent = this;
            rez.cost = this.cost + 1;
            rez.kutija1 = this.kutija1;
            rez.kutija2 = this.kutija2;
            return rez;
        }

        
        public List<State> mogucaSledecaStanja()
        {
            //TODO 1: Implementirati metodu tako da odredjuje dozvoljeno kretanje u lavirintu
            //TODO 2: Prosiriti metodu tako da se ne moze prolaziti kroz sive kutije
            List<State> rez = new List<State>();

            //Dodajemo za prvu PLAVU kutiju
            if(lavirint[markI, markJ] == 4){
                kutija1 = true;
            }
            //Dodajemo za drugu ZUTU kutiju
            if (lavirint[markI, markJ] == 5 && kutija1)
            {
               kutija2 = true;
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

            //u ovoj for petlji proveravamo da li smo naisli na teleport
            for (int i = 0; i < Main.teleport.Count; i++)
            {    // da li imamo teleport
                if (markI == Main.teleport[i].markI && markJ == Main.teleport[i].markJ) 
                {
                    State novo = bestBestExit(markI, markJ, i);
                    rez.Add(novo);
                }
            }

            return rez;
        }

        //Ova funkcija nam vraca polje koje je nabolje, tj najblize cilju
        public State bestBestExit(int markI, int markJ, int index)
        {
            int pamtii = 0;
            double min = Math.Sqrt(Math.Pow(Main.teleport[0].markI - Main.krajnjeStanje.markI, 2) + Math.Pow(Main.teleport[0].markJ - Main.krajnjeStanje.markJ, 2));
            //prodji kroz sve ulaze i proveri da li je najblizi cilju
            for (int i = 1; i < Main.teleport.Count; i++)
            {//racunamo euklidsko rastojanje izmedju cilja i teleporta
                double temp = Math.Sqrt(Math.Pow(Main.teleport[i].markI - Main.krajnjeStanje.markI, 2) + Math.Pow(Main.teleport[i].markJ - Main.krajnjeStanje.markJ, 2));
                if (temp < min && index != i)
                {
                    min = temp;
                    pamtii = i;
                }
            }
            State s = sledeceStanje(Main.teleport[pamtii].markI, Main.teleport[pamtii].markJ);
            return s;
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
                code += 10000;
            }
            return code;
        }

        public bool isKrajnjeStanje()
        { //Proveravamo i da li smo pokupili kutiju to je uslov && kutija
            return Main.krajnjeStanje.markI == markI && Main.krajnjeStanje.markJ == markJ && kutija1 && kutija2;
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
