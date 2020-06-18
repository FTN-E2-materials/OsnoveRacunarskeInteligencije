using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ComputationalGraph.Utilities
{
    /// <summary>
    /// Normalizacija podataka u custom domenu [a-b]
    /// 
    /// reference: 
    /// 1. https://stats.stackexchange.com/questions/70801/how-to-normalize-data-to-0-1-range
    /// 2. https://stats.stackexchange.com/questions/178626/how-to-normalize-data-between-1-and-1
    /// </summary>
    public class NormalizatorPodataka
    {
        // TODO: RESITI DA NORMALIZUJE I ZA NEGATIVNE VREDNOSTI
        // TODO: RESITI DA NORMALIZUJE I ZA DOSTA MALE VREDNOSTI
        public NormalizatorPodataka()
        {

        }

        #region Normalizacija podataka [0-1] domen

        /// <summary>
        /// Normalizacija podataka
        /// 
        /// referenca: https://stats.stackexchange.com/questions/70801/how-to-normalize-data-to-0-1-range
        /// </summary>
        public List<double> normalizujPodatke(List<double> nenormalizovaniPodaci)
        {
            List<double> normalizovaniPodaci = new List<double>();

            double min;
            double max;

            min = nadjiMin(nenormalizovaniPodaci);
            max = nadjiMax(nenormalizovaniPodaci);

            foreach (double tempNum in nenormalizovaniPodaci)
            {
                double normalizovanBroj = (tempNum - min) / (max - min);
                normalizovaniPodaci.Add(normalizovanBroj);
            }

            return normalizovaniPodaci;
        }

        #endregion

        #region Normalizacija podataka [a-b] domen

        /// <summary>
        /// Normalizacija podataka na interval [a-b]
        /// 
        /// referenca: https://stats.stackexchange.com/questions/178626/how-to-normalize-data-between-1-and-1
        /// formula: http://prntscr.com/t1167a
        /// </summary>
        /// <param name="nenormalizovaniPodaci"></param>
        /// <param name="a"> pocetak intervala normalizacije </param>
        /// <param name="b"> kraj intervala normalizacije </param>
        /// <returns></returns>
        public List<double> normalizujPodatkeNadIntervalom(List<double> nenormalizovaniPodaci,double a, double b)
        {

            List<double> normalizovaniPodaci = new List<double>();

            double min;
            double max;

            min = nadjiMin(nenormalizovaniPodaci);
            max = nadjiMax(nenormalizovaniPodaci);

            foreach (double tempNum in nenormalizovaniPodaci)
            {
                double normalizovanBroj = (b-a)*((tempNum - min) / (max - min)) + a;
                normalizovaniPodaci.Add(normalizovanBroj);
            }

            return normalizovaniPodaci;
        }
        #endregion

        #region Pronalazenje min i max vrednosti
        private double nadjiMin(List<double> brojevi)
        {
            double min = Double.MaxValue;

            foreach (double tempNum in brojevi)
            {
                if (tempNum < min)
                    min = tempNum;
            }

            return min;
        }

        private double nadjiMax(List<double> brojevi)
        {
            double max = Double.MinValue;

            foreach (double tempNum in brojevi)
            {
                if (tempNum > max)
                    max = tempNum;
            }

            return max;
        }



        #endregion
    }
}
