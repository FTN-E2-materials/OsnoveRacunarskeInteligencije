using System.Collections.Generic;
using System.Linq;

namespace ComputationalGraph
{
    /// <summary>
    /// NAPOMENA: Ovaj mnozac je ogranicen
    /// na to da imamo samo 2 elementa.
    /// </summary>
    public class MultiplyNode
    {
        /// <summary>
        /// Niz koji sadrzi ulaz i tezinu,
        /// 0-ti element je ULAZ
        /// 1-ti je njegova TEZINA
        /// </summary>
        public List<double> x;

        public MultiplyNode()
        {
            x = new List<double>();
            x.Add(0.0);
            x.Add(0.0);
        }
        /// <summary>
        /// Mnozenje ulaza sa tezinom
        /// </summary>
        /// <param name="x"></param>
        /// <returns>Proizvod ulaza i tezine</returns>
        public double forward(List<double> x)
        {
            this.x = x;

            double input = this.x[0];
            double tezina = this.x[1];
            return input * tezina;
        }

        /// <summary>
        /// z = x*y 
        /// dz/dx = y => dx = dz*y
        /// dz/dy = x => dy = dz*x
        /// 
        /// dx - gradijent za x, predstavlja koliko je INPUT uticao na gresku 
        /// dy - gradijent za y, predstavlja koliko je TEZINA uticala na gresku
        /// </summary>
        /// <param name="dz">izvod </param>
        /// <returns>[dx, dy]</returns>
        public List<double> backward(double dz)
        {
            double[] retval = { dz * this.x[1], dz * this.x[0] };
            return retval.ToList();
        }
    }
}
