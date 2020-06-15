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
        public bool skupljenaMunicija;

        private List<int> listaSakupljenihKutija = new List<int>();
      
        public State sledeceStanje(int markI, int markJ)
        {
            State rez = new State();
            rez.markI = markI;
            rez.markJ = markJ;
            rez.parent = this;
            rez.cost = this.cost + 1;
            rez.skupljenaMunicija = this.skupljenaMunicija;
            rez.listaSakupljenihKutija.AddRange(this.listaSakupljenihKutija);
            return rez;
        }

        //Perica mora da proba sve prepreke pre cilja 
        public List<State> mogucaSledecaStanja()
        {
            List<State> rez = new List<State>();

            if (lavirint[markI, markJ] == 4 && !this.listaSakupljenihKutija.Contains(100 * markI + markJ))
            {
                this.skupljenaMunicija = true;
                this.listaSakupljenihKutija.Add(100 * markI + markJ);
            }




            // Racunamo rastojanje od protivnika



            if (Main.protivniciBroj > 0)
            {
                foreach (State protivnik in Main.protivnici)
                {
                    double rastojanje = Math.Sqrt(Math.Pow(markI - protivnik.markI, 2) + Math.Pow(markJ - protivnik.markJ, 2));


                    if (rastojanje > 3.7 && !this.skupljenaMunicija)  //NE PRILAZI PROTIVNIKU , kocka 5x5
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

                    if (this.skupljenaMunicija)  //NE PRILAZI PROTIVNIKU , kocka 5x5
                    {
                        if (lavirint[markI, markJ] == 5 && !this.listaSakupljenihKutija.Contains(100 * markI + markJ))
                        {
                            this.skupljenaMunicija = false;
                            Console.WriteLine("Pogodjen protivnik na polju[" + markI + "," + markJ + "] ");
                            //Main.protivnici.Remove(protivnik);
                            lavirint[markI, markJ] = 0;
                            // protivnik.markI = 20;
                            //   protivnik.markJ = 20;
                            Main.protivniciBroj--;


                            this.listaSakupljenihKutija.Add(100 * markI + markJ);
                        }
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
                    }
                }
            }
            else
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
            int code = 10 * markI + markJ;
           
            foreach (int codeSakupljenogPolja in this.listaSakupljenihKutija){
                code += codeSakupljenogPolja;
            }

            return code;
        }

        public bool isKrajnjeStanje()
        {

            if (Main.protivniciBroj > 0)
            {


                foreach (State protivnik in Main.protivnici)
                {
                    double rastojanje = Math.Sqrt(Math.Pow(Main.krajnjeStanje.markI - protivnik.markI, 2) + Math.Pow(Main.krajnjeStanje.markJ - protivnik.markJ, 2));
                    if (rastojanje < 3.7)  //NE PRILAZI PROTIVNIKU , kocka 5x5
                    {
                        return false;
                    }
                }
            }
            else
            {
                return Main.krajnjeStanje.markI == markI && Main.krajnjeStanje.markJ == markJ;
            }
        
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
