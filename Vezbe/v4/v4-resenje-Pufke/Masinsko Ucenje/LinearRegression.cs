using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Masinsko_Ucenje
{
    public class LinearRegression
    {
        public double k { get; set; }
        public double n { get; set; }

        //Funkcija fit je ustvari funkcija iz PDF fajla, gde dobijamo parametre a i b nakon izvodjenja, nju cemo implementirati
	    public void fit(double[] x, double[] y) {
            // TODO 2: implementirati fit funkciju koja odredjuje parametre k i n
            // y = kx + n
            double suma_proizvoda_x_y = 0;
            double suma_x = 0;
            double suma_y = 0;
            double suma_x_na_kvadrat = 0;

            for( int i = 0; i < x.Length; i++)
            {
                suma_proizvoda_x_y += x[i] * y[i];
                suma_x += x[i];
                suma_y += y[i];
                suma_x_na_kvadrat += x[i] * x[i];
            }
            //k je ustvari a u onoj formuli, n je b , x.Lenght je n u fomruli
            this.k = (x.Length * suma_proizvoda_x_y - suma_x * suma_y) / (x.Length * suma_x_na_kvadrat - suma_x * suma_x);
            this.n = (suma_y - this.k * suma_x) / x.Length;
	    }

        public double predict(double x)
        {   
            // TODO 3: Implementirati funkciju predict koja na osnovu x vrednosti vraca
            // predvinjenu vrednost y
            return k * x + n; // sad kad imamo gore implementirano izracunavanje k i n, lako kreiramo formulu y = kx + n 
            //i ona nam vraca predikciju vrednosti y za nepoznato x koje prosledjujemo
        }
    }
}
