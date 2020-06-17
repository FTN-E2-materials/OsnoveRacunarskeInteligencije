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

	    public void fit(double[] x, double[] y) {
            // TODO 2: implementirati fit funkciju koja odredjuje parametre k i n
            // y = kx + n
            double suma_proizvoda_x_y = 0;
            double suma_x = 0; // moze kao x.Sum();
            double suma_y = 0; // moze kao y.Sum();
            double suma_x_na_kvadrat = 0;

            for (int i = 0; i < x.Length; i++)
            {
                suma_proizvoda_x_y += x[i] * y[i];
                suma_x += x[i];
                suma_y += y[i];
                suma_x_na_kvadrat += x[i] * x[i];
            }

            this.k = (x.Length * suma_proizvoda_x_y - suma_x * suma_y) / (x.Length * suma_x_na_kvadrat - suma_x * suma_x);
            this.n = (suma_y - this.k * suma_x) / x.Length;
        }

        public double predict(double x)
        {   
            // TODO 3: Implementirati funkciju predict koja na osnovu x vrednosti vraca
            // predvinjenu vrednost y
            return k * x + n;
        }
    }
}
