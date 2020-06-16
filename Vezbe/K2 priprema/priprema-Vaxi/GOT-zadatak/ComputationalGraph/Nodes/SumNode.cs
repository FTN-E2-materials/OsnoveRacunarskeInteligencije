using System.Collections.Generic;
using System.Linq;

namespace ComputationalGraph
{
    public class SumNode
    {
        /// <summary>
        /// Svi ulazni parametri sabiraca, moze ih biti vise
        /// za razliku od mnozaca gde imamo samo 2 ulaza.
        /// </summary>
        private List<double> x;

        public SumNode()
        {
            x = new List<double>();
        }
        /// <summary>
        /// Suma svih elemenata
        /// </summary>
        /// <param name="ulazniParametriSabiraca"></param>
        /// <returns></returns>
        public double forward(List<double> ulazniParametriSabiraca)
        {
            this.x = ulazniParametriSabiraca;
            //TODO 1: implementirati forward funckiju za sum node

            // sabirac vraca samo sumu 
            return ulazniParametriSabiraca.Sum();
        }
        /// <summary>
        /// Izvod funkcije po svakom elementu
        /// z - output 
        /// z = x + y 
        /// Moramo da vidimo koliko ulazi uticu na izlaz, te 
        /// to radimo preko izvoda.
        /// dz/dx = 1 => dx = dz
        /// dz/dy = 1 => dy = dz
        /// </summary>
        /// <param name="dz"></param>
        /// <returns>
        ///     Koliko imamo inputa, toliko cemo vratiti
        ///     dz-tova u nizu.
        /// </returns>
        public List<double> backward(double dz)
        {
            //TODO 2: implementirati backward funkciju za sum node

            /*
             * Isto kao da smo trcali kroz x.size() i za svaki
             * input dodavali dz u povratnu listu.
             * 
             * A moze i na ovaj nacin preko c#, gde iz niza x(ulazi)
             * selektujem sve elemente xx takve da svaki taj xx bude dz
             * i to dodam u povratnu listu.
             */
            return x.Select(xx => xx = dz).ToList();
        }
    }
}
