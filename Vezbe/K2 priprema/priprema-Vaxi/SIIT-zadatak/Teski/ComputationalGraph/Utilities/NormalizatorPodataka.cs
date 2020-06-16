using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ComputationalGraph.Utilities
{
    /// <summary>
    /// Normalizacija podataka u domen [0-1]
    /// FUNKCIONISE SAMO PO MIN-MAX PRINCIPU,
    /// nije jos reseno da radi i za negativne.
    /// 
    /// referenca: referenca: https://stats.stackexchange.com/questions/70801/how-to-normalize-data-to-0-1-range
    /// </summary>
    public class NormalizatorPodataka
    {
        // TODO: RESITI DA NORMALIZUJE I ZA NEGATIVNE VREDNOSTI
        // TODO: RESITI DA NORMALIZUJE I ZA DOSTA MALE VREDNOSTI
        public NormalizatorPodataka()
        {

        }

        #region Normalizacija podataka

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
