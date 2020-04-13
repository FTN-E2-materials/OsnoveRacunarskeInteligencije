using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Masinsko_Ucenje
{
    public class KMeans
    {
        public List<Point> elementi = new List<Point>();
        public List<Cluster> grupe = new List<Cluster>();
        public int brojGrupa = 0;
        Random rnd = new Random();

        public void podeliUGrupe(int brojGrupa, double errT)
        {
            this.brojGrupa = brojGrupa;
            if (brojGrupa == 0) return;
            //------------  inicijalizacija -------------
            for (int i = 0; i < brojGrupa; i++)
            {
                // TODO 5: na slucajan nacin inicijalizovati centre grupa
                int ii = rnd.Next(elementi.Count);
                Cluster grupa = new Cluster(); //Pravimo novu grupu, odredjena je sa centrom i sa elementima koji joj pripadaju
                grupa.centar = elementi[ii];
                grupe.Add(grupa);
            }
            //------------- iterativno racunanje centara ---
            //Odnosno ovde cemo da racunamo koliko su nam elementi blizu centara, i da dodeljujemo elemente koji su najblize
            //tim centrima, i da pomeramo te centre
            for (int it = 0; it < 1000; it++)
            {
                foreach (Cluster grupa in grupe)//Ispraznimo sve elemente
                    grupa.elementi = new List<Point>();

                foreach (Point cc in elementi)
                {
                    int najblizaGrupa = 0;
                    //Ova ovde logika trazi minimalno rastojanje u odnosu na sve centre
                    for (int i = 0; i < brojGrupa; i++)
                    {
                        if (grupe[najblizaGrupa].rastojanje(cc) >
                            grupe[i].rastojanje(cc))
                        {
                            najblizaGrupa = i;
                        }
                    }
                    grupe[najblizaGrupa].elementi.Add(cc);//Smestimo sve grupe 
                }
                double err = 0;
                //Racunamo koliko su nam se centri pomerili
                for (int i = 0; i < brojGrupa; i++)
                {//Funkcija za toleranciju, tj koliko ih pustamo da se pomeraju
                    err += grupe[i].pomeriCentar(); //Ako se pomeraju puno idemo na novu iteraciju
                }
                if (err < errT)
                    break;//Ako se pomeraju manje od dozvoljenog to zanci da zavrsavamo algoritam

                Main.clusteringHistory.Add(Main.DeepClone(this.grupe));
            }
        }
    }

    [Serializable]
    public class Cluster
    {
        public Point centar = new Point(0,0);
        public List<Point> elementi = new List<Point>();

        public double rastojanje(Point c)
        {   // TODO 6: implementirati funkciju rastojanja
            //Menhetn rastojanje, u sustini ova funkcija nam gore daje informaciju koliko su slicni element c i nas centar
            //Gore kad odredjujemo nase grupe trazimo da je arasdtojanje sto manje, i onda dodelimo toj grupi
            return Math.Abs(c.x - centar.x) + Math.Abs(c.y - centar.y);

        }

        public double pomeriCentar()
        {   // TODO 7: implemenitrati funkciju koja pomera centre klastera
            double sX = 0;
            double sY = 0;
            double retVal = 0;
            //Treba da izracunamo srednju vrednost za x i srednju vrednost za y i da vratimo kolika je razliak izmedju starog i novog centra
            foreach( Point c in elementi)
            {
                sX += c.x;
                sY += c.y;
            }
            int n = elementi.Count();
            if(n != 0)
            {
                //novi centar = srednja vrednost svih tacaka grupe
                Point nCentar = new Point((sX / n), (sY / n));
                //Rastojanje starog i novog centra
                retVal = rastojanje(nCentar);
                centar = nCentar; // pregazimo statri centar novim centrom
            }
            return retVal;
        }
    }
}
