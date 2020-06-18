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

        /// <summary>
        /// Niz redova koje smo ucitali iz fajla.
        /// </summary>
        private string[] _ucitaniRedovi;

        /// <summary>
        /// Indikator jedinstvenosti za svaki kategoricki
        /// atribut u podacima.
        /// </summary>
        private int _indeksKategorickihAtributa;

        /// <summary>
        /// Ideja je da imam listu kolona, gde svaka kolona predstavlja atribut za jedan kategoricki atribut
        /// primer, naidjem na tekstualni atribut, odma pravim kolonu za njega i stavljam 1 na njegov indeks
        /// njegov indeks predstavlja koji je to po redu kategoricki atribut.
        /// 
        /// Tako dobijam one hot. 
        /// 
        /// primer: naisao sam na 2 atrbiuta koja su tekstualna
        /// => pravim 2 kolone gde svaka kolona odgovara jednom atributu
        /// => postavljam indeks prvog u njegovu kolonu na prvo mesto u koloni
        /// => postavljam indeks drugog u njegovu kolonu na drugo mesto u koloni
        /// => dobio sam 2 nova inputa, tj 2 nove kolone koje su u formi isGood i isNoGood 
        /// 
        /// 
        /// </summary>
        private List<List<double>> _koloneKategorickihAtributa;

        /// <summary>
        /// Vodim racuna da imamo samo uniq kategoricke atribute ,
        /// kako ne bih napravio vise kolona za kategoricke atribute nego sto je dovoljno.
        /// 
        /// key: naziv kategorickog atributa
        /// value: nebitan
        /// </summary>
        private Dictionary<String, List<double>> _uniqKategoricniAtribut;

        /// <summary>
        /// Specijalno za ovaj zadatak delim kategorije u tipove.
        /// 
        /// Key: "ime tipa"
        /// value: njegova uniq vrednost
        /// </summary>
        private Dictionary<String, int> _tipovi;


        /// <summary>
        /// Indeks koji je vezan samo za ovaj zadatak
        /// i predstavlja indeks tipa.
        /// </summary>
        private int _indexTipa;

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

        public List<List<double>> KoloneKategorickihAtributa
        {
            get { return _koloneKategorickihAtributa; }
            set { _koloneKategorickihAtributa = value; }
        }

        public Dictionary<String, List<double>> UniqKategorickiAtributi
        {
            get { return _uniqKategoricniAtribut; }
            set { _uniqKategoricniAtribut = value; }
        }

        public Dictionary<String, int> Tipovi
        {
            get { return _tipovi; }
            set { _tipovi = value; }
        }

        public int IndexTipa
        {
            get { return _indexTipa; }
            set { _indexTipa = value; }
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
            UcitaniRedovi = UcitaniRedovi.Skip(1).ToArray(); // preskakanje naslova 

            KoloneKategorickihAtributa = new List<List<double>>();
            UniqKategorickiAtributi = new Dictionary<string, List<double>>();
            IndeksKategorickihAtributa = 0;                 // krecem od 0 indeksiranje kategorickih atributa

            Tipovi = new Dictionary<string, int>();
            IndexTipa = 0;
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
                    // TODO: KATEGORICKI ATRIBUT - zakomentarisem ukoliko ima previse kategorickih atributa, jer u tom slucaju nikad da se zavrsi jedna epoha a kamoli obucavanje citave mreze
                    //kategorickiAtribut(atributString);
                    IndeksKategorickihAtributa += 1;

                    // atributBroj = IndeksKategorickihAtributa;       // ostavljam i ovo za slucaj da imam previse kategorickih atributa pa je algoritam prespor
                    atributBroj = transformisiUBroj(atributString);
                }

                kolona.Add(atributBroj);
            }

            return kolona;
        }

        #endregion

        #region Kategoricki atribut

        /// <summary>
        /// Kada se pojavi kategoricki atribut:
        /// 
        /// 1. Napravim kolonu duzine koliko imam redova u tabeli
        /// 2. Na tekuce mesto(tekuci red) stavljam 1 za taj atribut
        /// </summary>
        /// <param name="atributString"></param>
        private void kategorickiAtribut(string atributString)
        {
            if (!UniqKategorickiAtributi.ContainsKey(atributString))
            {
                // pravim kolonu sa onoliko redova koliko ima redova ucitana tabela 
                // referenca: https://stackoverflow.com/questions/466946/how-to-initialize-a-listt-to-a-given-size-as-opposed-to-capacity
                List<double> novaKolonaKategorickogAtributa = new List<double>(new double[UcitaniRedovi.Length]);
                novaKolonaKategorickogAtributa[IndeksKategorickihAtributa] = 1.0;


                UniqKategorickiAtributi[atributString] = novaKolonaKategorickogAtributa;
                KoloneKategorickihAtributa.Add(novaKolonaKategorickogAtributa);
            }
            else
            {
                // dobavim kolonu za taj atribut 
                // i stavim u toj koloni na tekuci indeks keca 
                UniqKategorickiAtributi[atributString][IndeksKategorickihAtributa] = 1;
            }
        }

        #endregion

        #region Transformacija text atributa u broj

        private Double transformisiUBroj(String text)
        {
            //TODO: Resiti ovo, ovo sad samo svakom stringu poveca vrednost za 1, kao enumeracija
            if (!Tipovi.ContainsKey(text))
            {
                IndexTipa += 1;
                Tipovi[text] = IndexTipa;
            }
            return IndexTipa;
        }

        #endregion

        #region Provera postojanja kategorickih atributa

        #endregion

        #region Dobavljanje kategorickih atributa

        #endregion
    }
}
