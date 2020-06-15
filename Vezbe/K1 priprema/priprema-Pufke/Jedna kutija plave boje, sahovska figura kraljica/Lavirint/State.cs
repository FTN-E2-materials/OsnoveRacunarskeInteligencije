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
            //TODO 1: Implementirati metodu tako da odredjuje dozvoljeno kretanje u lavirintu
            //TODO 2: Prosiriti metodu tako da se ne moze prolaziti kroz sive kutije
            List<State> rez = new List<State>();

            //Dodajemo za PLAVU kutiju
            if (lavirint[markI, markJ] == 4)
            {
                kutija = true;
            }

            //Kretnja kao lovac
            for (int i = 1, j = 1; i <= Main.brojVrsta ; i += 1, j += 1)
            {
                int newMarkI = markI + i;
                int newMarkJ = markJ + j;

                if (newMarkI >= 0 && newMarkI < Main.brojVrsta)
                {
                    if (newMarkJ >= 0 && newMarkJ < Main.brojKolona)
                    {
                        if (lavirint[newMarkI, newMarkJ] == 1)
                        {
                            break;
                        }
                        if (lavirint[newMarkI, newMarkJ] != 1)
                        {
                            State novo = sledeceStanje(newMarkI, newMarkJ);
                            rez.Add(novo);
                        }
                    }
                }
            }

            for (int i = 1, j = 1; i <= Main.brojVrsta; i += 1, j += 1)
            {
                int newMarkI = markI + i;
                int newMarkJ = markJ - j;

                if (newMarkI >= 0 && newMarkI < Main.brojVrsta)
                {
                    if (newMarkJ >= 0 && newMarkJ < Main.brojKolona)
                    {
                        if (lavirint[newMarkI, newMarkJ] == 1)
                        {
                            break;
                        }
                        if (lavirint[newMarkI, newMarkJ] != 1)
                        {
                            State novo = sledeceStanje(newMarkI, newMarkJ);
                            rez.Add(novo);
                        }
                    }
                }
            }

            for (int i = 1, j = 1; i <= Main.brojVrsta; i += 1, j += 1)
            {
                int newMarkI = markI - i;
                int newMarkJ = markJ + j;

                if (newMarkI >= 0 && newMarkI < Main.brojVrsta)
                {
                    if (newMarkJ >= 0 && newMarkJ < Main.brojKolona)
                    {
                        if (lavirint[newMarkI, newMarkJ] == 1){
                            break;
                        }
                        if (lavirint[newMarkI, newMarkJ] != 1)
                        {
                            State novo = sledeceStanje(newMarkI, newMarkJ);
                            rez.Add(novo);
                        }
                    }
                }
            }

            for (int i = 1, j = 1; i <= Main.brojVrsta; i += 1, j += 1)
            {
                int newMarkI = markI - i;
                int newMarkJ = markJ - j;

                if (newMarkI >= 0 && newMarkI < Main.brojVrsta)
                {
                    if (newMarkJ >= 0 && newMarkJ < Main.brojKolona)
                    {
                        if (lavirint[newMarkI, newMarkJ] == 1)
                        {
                            break;
                        }
                        if (lavirint[newMarkI, newMarkJ] != 1)
                        {
                            State novo = sledeceStanje(newMarkI, newMarkJ);
                            rez.Add(novo);
                        }
                    }
                }
            }

            // + Kretnja kao top == Kraljica

            //Kretanje u pravcu dole
            for (int i = 1; i <= Main.brojVrsta; i += 1)
            {
                int newMarkI = markI + i;


                if (newMarkI >= 0 && newMarkI < Main.brojVrsta)
                {
                    if (markJ >= 0 && markJ < Main.brojKolona)
                    {
                        if (lavirint[newMarkI, markJ] == 1)
                        {
                            break;
                        }
                        if (lavirint[newMarkI, markJ] != 1)
                        {
                            State novo = sledeceStanje(newMarkI, markJ);
                            rez.Add(novo);
                        }
                    }
                }
            }

            for (int j = 1; j <= Main.brojVrsta; j += 1)
            {
                int newMarkJ = markJ - j;

                if (markI >= 0 && markI < Main.brojVrsta)
                {
                    if (newMarkJ >= 0 && newMarkJ < Main.brojKolona)
                    {
                        if (lavirint[markI, newMarkJ] == 1)
                        {
                            break;
                        }
                        if (lavirint[markI, newMarkJ] != 1)
                        {
                            State novo = sledeceStanje(markI, newMarkJ);
                            rez.Add(novo);
                        }
                    }
                }
            }

            for (int i = 1; i <= Main.brojVrsta; i += 1)
            {
                int newMarkI = markI - i;

                if (newMarkI >= 0 && newMarkI < Main.brojVrsta)
                {
                    if (markJ >= 0 && markJ < Main.brojKolona)
                    {
                        if (lavirint[newMarkI, markJ] == 1)
                        {
                            break;
                        }
                        if (lavirint[newMarkI, markJ] != 1)
                        {
                            State novo = sledeceStanje(newMarkI, markJ);
                            rez.Add(novo);
                        }
                    }
                }
            }

            for (int j = 1; j <= Main.brojVrsta; j += 1)
            {

                int newMarkJ = markJ + j;

                if (markI >= 0 && markI < Main.brojVrsta)
                {
                    if (newMarkJ >= 0 && newMarkJ < Main.brojKolona)
                    {
                        if (lavirint[markI, newMarkJ] == 1)
                        {
                            break;
                        }
                        if (lavirint[markI, newMarkJ] != 1)
                        {
                            State novo = sledeceStanje(markI, newMarkJ);
                            rez.Add(novo);
                        }
                    }
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
