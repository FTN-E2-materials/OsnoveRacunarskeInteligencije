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
            double sumaProizvodaXY = 0;
            double sumaX = 0;
            double sumaY = 0;
            double sumaXNaKvadrat = 0;

            for(int i = 0; i < x.Length; i++)
            {
                sumaProizvodaXY += x[i] * y[i];
                sumaX += x[i];                  // mogli smo jednostavno x.Sum(); ali kad vec
                sumaY += y[i];                  // iteriramo mozemo i ovde to uraditi 
                sumaXNaKvadrat += x[i] * x[i];
            }

            this.k = (x.Length * sumaProizvodaXY - sumaX * sumaY) / (x.Length * sumaXNaKvadrat - sumaX * sumaX);
            this.n = (sumaY - this.k * sumaX) / x.Length;
	    }

        public double predict(double x)
        {   
            // TODO 3: Implementirati funkciju predict koja na osnovu x vrednosti vraca
            // predvinjenu vrednost y
            return k * x + n;

        }
    }
}
