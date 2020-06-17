using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;

namespace ComputationalGraph.Utilities
{

    /// <summary>
    /// Ucitavamo podatke direktno iz fajlova
    /// </summary>
    public class DataReader
    {

        #region Atributi

        private string[] _ucitaniRedovi;

        /// <summary>
        /// Indikator jedinstvenosti za svaki kategoricki
        /// atribut u podacima.
        /// </summary>
        private int _indeksKategorickihAtributa;

        /// <summary>
        /// Key: "ime tipa"
        /// value: njegova uniq vrednost
        /// </summary>
        private Dictionary<String,int> _kategorickiAtributi;



        #endregion

        #region Propertiji 

        public string[] UcitaniRedovi
        {
            get { return _ucitaniRedovi; }
            set { _ucitaniRedovi = value; }
        }

        public int IndeksKategorickihAtributa
        {
            get { return _indeksKategorickihAtributa; }
            set { _indeksKategorickihAtributa = value; }
        }


        public Dictionary<String, int> KategorickiAtributi
        {
            get { return _kategorickiAtributi; }
            set { _kategorickiAtributi = value; }
        }
        #endregion

        /// <summary>
        /// Proslediti naziv fajla koji iscitavamo i dobijamo onda rukovanje
        /// tim fajlom
        /// </summary>
        /// <param name="nazivFajlaZaCitanje"></param>
        public DataReader(String nazivFajlaZaCitanje)
        {
            String path = "./../../data/" + nazivFajlaZaCitanje;
            UcitaniRedovi = File.ReadAllLines(path);
            UcitaniRedovi = UcitaniRedovi.Skip(1).ToArray(); // skip header row (State, Lat, Mort, Ocean, Long)

            IndeksKategorickihAtributa = 0;                 // krecem od 0 indeksiranje kategorickih atributa, inc na svaku pojavu jedno kategorickog atributa
            KategorickiAtributi = new Dictionary<string, int>();
        }

        #region Ucitavanje podataka

        /// <summary>
        /// Ucitavanje podataka iz datoteke za prosledjenu kolonu
        /// </summary>
        /// <param name="brojKoloneZaUcitavanje"></param>
        /// <returns></returns>
        public List<double> ucitajPodatkeIzKolone(int brojKoloneZaUcitavanje)
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
                    //TODO: Resiti ove kategoricke atribute !!!

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
            //TODO: Resiti ovo, ovo sad samo svakom stringu poveca vrednost za 1, kao enumeracija
            if (!KategorickiAtributi.ContainsKey(text))
            {
                IndeksKategorickihAtributa += 1;
                KategorickiAtributi[text] = IndeksKategorickihAtributa;
            }
            return IndeksKategorickihAtributa;
        }

        #endregion

        #region Provera postojanja kategorickih atributa

        #endregion

        #region Dobavljanje kategorickih atributa

        #endregion
    }
}
