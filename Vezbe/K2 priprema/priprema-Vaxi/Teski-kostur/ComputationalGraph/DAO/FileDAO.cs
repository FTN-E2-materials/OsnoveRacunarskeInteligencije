using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Threading;
using System.Runtime.Serialization.Formatters.Binary;

namespace ComputationalGraph.DAO
{
    /// <summary>
    /// Pristup fajlovima, i ucitavanja iz njih.
    /// Pattern: Data Access Object
    /// </summary>
    public class FileDAO
    {
        #region Atributi

        private string[] _ucitaniRedovi;

        /// <summary>
        /// Ideja je da predstavlja kolekciju za 'one hot'
        /// 
        /// key: uneti kategoricki atribut [ TEXT ]
        /// value: isTajKey
        /// 
        /// Primer  {
        ///             "srbija": [ 1 ]
        ///             "grcka" : [ 0, 1] 
        ///         }
        /// 
        /// Koliko imam elemenata u dict, na to mesto
        /// stavljam 1 kod novog clana u dict
        ///         {
        ///             "gruzija" : [ 0, 0, 1]
        ///         }
        ///         
        /// Na kraju bi trebalo proci kroz citav dict i svakome
        /// value koji nema elemenata koliko i dict dodati dopunu
        /// do tog broja elemenata(naravno nulama)
        /// 
        /// </summary>
        private Dictionary<string,bool> _kategorickiAtributi;

        public Dictionary<string,bool> KategorickiAtributi
        {
            get { return _kategorickiAtributi; }
            set { _kategorickiAtributi = value; }
        }

        

        #endregion

        #region Propertiji

        public string[] UcitaniRedovi
        {
            get { return _ucitaniRedovi; }
            set { _ucitaniRedovi = value; }
        }

        #endregion

        public FileDAO()
		{
            UcitaniRedovi = File.ReadAllLines(@"./../../data/gotdata.csv");
            UcitaniRedovi = UcitaniRedovi.Skip(1).ToArray(); // skip header row (State, Lat, Mort, Ocean, Long)

            List<double> X = new List<double>();
            X = ucitajPodatkeIzKolone(9);
            X = normalizujPodatke(X);

            List<double> Y = new List<double>();
            Y = ucitajPodatkeIzKolone(13);
            Y = normalizujPodatke(Y);



        }


        #region Ucitavanje podataka

        /// <summary>
        /// Ucitavanje podataka iz datoteke za prosledjenu kolonu
        /// </summary>
        /// <param name="brojKoloneZaUcitavanje"></param>
        /// <returns></returns>
        private List<double> ucitajPodatkeIzKolone(int brojKoloneZaUcitavanje)
        {
            List<double> kolona = new List<double>();
            
            foreach (string trenutniRed in UcitaniRedovi)
            {
                string[] atributi = trenutniRed.Split(',');                 // csv file split by , (comma)
                string atributString = atributi[brojKoloneZaUcitavanje];

                double atributBroj = 0;

                /*
                 * NAPOMENA: TryParse ce postaviti na 0 atributBroj ukoliko nije moguce parsirati,
                 * time dobijam da u mojoj ucitanoj koloni nema PRAZNIH POLJA !!
                 */
                bool isNumeric = double.TryParse(atributString, out atributBroj);

                if (!isNumeric)
                {
                    // znamo da je unos problematicni kategoricni string
                    // pa je potrebno to resiti transformacijom u broj
                    atributBroj = transformisiUBroj(atributString);
                }

                kolona.Add(atributBroj);      
            }

            return kolona;
        }

        #endregion

        #region Transformacija text atributa u broj
        
        private Double transformisiUBroj(String text)
        {

            return 6.9;
        }

        #endregion

        #region Provera postojanja kategorickih atributa

        #endregion

        #region Dobavljanje kategorickih atributa

        #endregion

        #region Normalizacija podataka

        /// <summary>
        /// Normalizacija podataka
        /// 
        /// referenca: https://stats.stackexchange.com/questions/70801/how-to-normalize-data-to-0-1-range
        /// </summary>
        private List<double> normalizujPodatke(List<double> nenormalizovaniPodaci)
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
