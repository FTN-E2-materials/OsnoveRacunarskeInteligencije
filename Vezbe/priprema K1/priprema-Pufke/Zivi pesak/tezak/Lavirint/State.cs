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
        public bool zutaSkupljena;
        private List<int> listaSakupljenihKutija = new List<int>();
        public double brojZivota = 0;//Broj skupljenih plavih kutija
        public double brojZgazenogZivogPeska = 0;//Broj zgazenih brao polja, kod kraljcie se racuna samo poslednje 
        public State sledeceStanje(int markI, int markJ)
        {
            State rez = new State();
            rez.markI = markI;
            rez.markJ = markJ;
            rez.parent = this;
            rez.cost = this.cost + 1;
            rez.zutaSkupljena = this.zutaSkupljena;
            rez.brojZivota = this.brojZivota;
            rez.brojZgazenogZivogPeska = this.brojZgazenogZivogPeska;
            rez.listaSakupljenihKutija.AddRange(this.listaSakupljenihKutija);
            return rez;
        }


        public List<State> mogucaSledecaStanja()
        {

            List<State> rez = new List<State>();

            //Logika za zutu kutiju
            if (lavirint[markI, markJ] == 5 && !this.listaSakupljenihKutija.Contains(100 * markI + markJ) && brojZgazenogZivogPeska - brojZivota <= 3)
            {
                zutaSkupljena = true;
                this.listaSakupljenihKutija.Add(100 * markI + markJ);

            }

            //Logika za plavu kutiju, zivot
            if (lavirint[markI, markJ] == 4 && !this.listaSakupljenihKutija.Contains(100 * markI + markJ))
            {
                brojZivota++;
                this.listaSakupljenihKutija.Add(100 * markI + markJ);
            }

            //Logika za braon kutiju, zivi pesak
            if (lavirint[markI, markJ] == 6 && !this.listaSakupljenihKutija.Contains(100 * markI + markJ))
            {
                brojZgazenogZivogPeska++;
                this.listaSakupljenihKutija.Add(100 * markI + markJ);
            }

            if (lavirint[markI, markJ] != 6 && lavirint[markI, markJ] != 1)
            {
                brojZgazenogZivogPeska = 0;
            }

          
                if (!zutaSkupljena)
                {//Krece se kao konj
                    #region Krece se kao konj
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
                    #endregion
                }
                else
                {//krece se kao kraljica
             
                    #region kretnja kao kraljica
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
                    #endregion
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
            Console.WriteLine(brojZgazenogZivogPeska - brojZivota);
            return Main.krajnjeStanje.markI == markI && Main.krajnjeStanje.markJ == markJ && this.zutaSkupljena == true && brojZgazenogZivogPeska - brojZivota <= 3;
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
