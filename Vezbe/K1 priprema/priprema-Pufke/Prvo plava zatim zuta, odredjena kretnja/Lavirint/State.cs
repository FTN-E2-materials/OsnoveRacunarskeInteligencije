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

            if (kutija1 == false && kutija2 == false)
            {
                for (int i = -2; i <= 2; i += 4)
                {                                       //Ove dve for petlje nam daju kombinaciju kretanja levo desno za dva polja i gore dole za jedno polje
                    int newMarkI = markI + i;
                    for (int j = -1; j <= 1; j += 2)
                    {
                        int newMarkJ = markJ + j;
                        if (newMarkI >= 0 && newMarkI < Main.brojVrsta)
                        {
                            if (newMarkJ >= 0 && newMarkJ < Main.brojKolona)
                            {
                                if (lavirint[newMarkI, newMarkJ] != 1)
                                {
                                    State novo = sledeceStanje(newMarkI, newMarkJ);
                                    rez.Add(novo);
                                }
                            }
                        }
                    }
                }

                for (int j = -2; j <= 2; j += 4)
                {                                        //Ove dve for petlje nam daju kombinaciju kretanja gore dole za dva polja i levo desno za jedno polje
                    int newMarkJ = markJ + j;
                    for (int i = -1; i <= 1; i += 2)
                    {
                        int newMarkI = markI + i;
                        if (newMarkI >= 0 && newMarkI < Main.brojVrsta)
                        {
                            if (newMarkJ >= 0 && newMarkJ < Main.brojKolona)
                            {
                                if (lavirint[newMarkI, newMarkJ] != 1)
                                {
                                    State novo = sledeceStanje(newMarkI, newMarkJ);
                                    rez.Add(novo);
                                }
                            }
                        }
                    }
                }
            }
            else {
                //Kretanje u pravcu goredesno 
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
