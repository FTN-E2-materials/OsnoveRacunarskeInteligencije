using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Lavirint
{
    public class Lavirint
    {
        public int brojVrsta, brojKolona;
        public int[,] polja;


        public Lavirint(int brojVrsta, int brojKolona)
        {
            this.brojVrsta = brojVrsta;
            this.brojKolona = brojKolona;

            this.polja = new int[this.brojVrsta, this.brojKolona];
        }

        public void sacuvajLavirint()
        {
            TextWriter tw = new StreamWriter("../../lavirint.txt");

            tw.WriteLine(this.brojKolona);
            tw.WriteLine(this.brojVrsta);
            for (int i = 0; i < this.brojVrsta; i++)
            {
                for (int j = 0; j < this.brojKolona; j++)
                {
                    tw.WriteLine(this.polja[i, j]);
                }
            }
            tw.Close();
        }

        public void ucitajLavirint()
        {
            TextReader tw = new StreamReader("../../lavirint.txt");
            this.brojKolona = Convert.ToInt32(tw.ReadLine());
            this.brojVrsta = Convert.ToInt32(tw.ReadLine());
            for (int i = 0; i < this.brojVrsta; i++)
            {
                for (int j = 0; j < this.brojKolona; j++)
                {
                    int tt = Convert.ToInt32(tw.ReadLine());
                    this.polja[i, j] = tt;
                }
            }
            tw.Close();
        }
    }
}
