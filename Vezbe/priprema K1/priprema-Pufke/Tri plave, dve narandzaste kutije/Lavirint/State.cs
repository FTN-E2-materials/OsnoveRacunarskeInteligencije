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
        public bool plavaKutija;
        public bool narandzastaKutija;

        public State sledeceStanje(int markI, int markJ)
        {
            State rez = new State();
            rez.markI = markI;
            rez.markJ = markJ;
            rez.parent = this;
            rez.cost = this.cost + 1;
            rez.plavaKutija = this.plavaKutija;
            rez.narandzastaKutija = this.narandzastaKutija;

            return rez;
        }

        //TODO mogucaSledecaStanja() funckija
        public List<State> mogucaSledecaStanja()
        { 
            List<State> rez = new List<State>();
         
            //Dodajemo za PLAVU kutiju
            if (lavirint[markI, markJ] == 4)
            {
                if (Main.plavaBrojac == 3)
                {
                    this.plavaKutija = true;

                }
                if (Main.plavaBrojac < 3)
                {

                    Main.plavaBrojac++;
                    if (Main.plavaBrojac == 3)
                    {
                        this.plavaKutija = true;
                    }
                }
            }

            //Za narandzastu kutiju
            if (lavirint[markI, markJ] == 5)
            {
                if (Main.narandzastaBrojac == 2)
                {
                    this.narandzastaKutija = true;
                }

                if (Main.narandzastaBrojac < 2)
                {
                    Main.narandzastaBrojac++;
                    if (Main.narandzastaBrojac == 2)
                    {
                        this.narandzastaKutija = true;
                    }
                }
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

            return rez;
        }

        public override int GetHashCode()
        {
            int code = 10 * markI + markJ; //Ako nemamo kutiju ti ce nam biti od 0-200
            if (Main.plavaBrojac == 0)
            {
                code += 1000;
            }
            if (Main.plavaBrojac == 1)
            {
                code += 2000;
            }
            if (Main.plavaBrojac == 2)
            {
                code += 3000;
            }
            if (Main.plavaBrojac == 3)
            {
                code += 4000;
            }
            if (Main.narandzastaBrojac == 0)
            {
                code += 5000;
            }
            if (Main.narandzastaBrojac == 1)
            {
                code += 6000;
            }
            if (Main.narandzastaBrojac == 2)
            {
                code += 7000;
            }
            if (Main.narandzastaBrojac == 2 && Main.plavaBrojac == 3)
            {
                code += 8000;
            }
            return code;

        }

        public bool isKrajnjeStanje()
        { //Proveravamo i da li smo pokupili kutiju to je uslov && kutija
            Console.WriteLine( plavaKutija);
            return Main.krajnjeStanje.markI == markI && Main.krajnjeStanje.markJ == markJ && plavaKutija && narandzastaKutija;
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
