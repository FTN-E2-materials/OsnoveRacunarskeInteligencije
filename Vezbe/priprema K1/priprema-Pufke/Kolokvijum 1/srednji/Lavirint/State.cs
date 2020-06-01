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

            if (skupljenBrojPoena < 10) {
                //Kretnja
                for (int i = 1, j = 1; i <= Main.brojVrsta; i += 1, j += 1)
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
            }
            if (skupljenBrojPoena >= 10)
            {
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
