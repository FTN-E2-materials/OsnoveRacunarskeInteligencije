using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Threading;
using System.Runtime.Serialization.Formatters.Binary;
using ComputationalGraph.Utilities;

namespace ComputationalGraph.DAO
{
    /// <summary>
    /// Pristup fajlovima, i ucitavanja iz njih.
    /// Pattern: Data Access Object
    /// </summary>
    public class FileDAO
    {
        #region Atributi

        
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

        /// <summary>
        /// Normalizuje prosledjene neuredjene podatke
        /// u lepo domenovane podatke[0-1]
        /// </summary>
        private NormalizatorPodataka _normalizacija;

        /// <summary>
        /// Predstavlja listu kolona koje smo ucitali
        /// 
        /// Kolone koje predstavljaju labele ulaznih atributa !
        /// </summary>
        private List<List<double>> _ulazneKoloneTrainSeta;

        /// <summary>
        /// Predstavlja listu ucitanih kolona izlaznog atributa,
        /// ne Y nego samo listu kolona izlaznih atributa.
        /// </summary>
        private List<List<double>> _izlazneKoloneTrainSeta;

        /// <summary>
        /// Predstavlja listu kolona koje smo ucitali iz test seta
        /// 
        /// Kolone koje predstavljaju labele ulaznih atributa !
        /// </summary>
        private List<List<double>> _ulazneKoloneTestSeta;

        /// <summary>
        /// Predstavlja listu ucitanih kolona izlaznog atributa,
        /// ne YTest nego samo listu kolona izlaznih atributa koje smo ucitali
        /// </summary>
        private List<List<double>> _izlazneKoloneTestSeta;

        /// <summary>
        /// Iscitavanje podataka iz csv fajla zadatom u citacu.
        /// </summary>
        private DataReader _citacPodataka;

        /// <summary>
        /// Citac test podataka ukoliko imam zaseban fajl za test podatke
        /// </summary>
        private DataReader _citacTestPodataka;

        /// <summary>
        /// Svi INPUTI koji se dovode na ulaz mreze
        /// </summary>
        private List<List<double>> _X;

        /// <summary>
        /// Svi OUTPUT-i koje se nalaze na izlazu mreze
        /// </summary>
        private List<List<double>> _Y;

        /// <summary>
        /// Inputi koje cuvamo samo za testiranje podataka
        /// kako ne bi ipak overfitovali budemo li na svim podacima
        /// fitovali tj trenirali !
        /// </summary>
        private List<List<double>> _Xtest;

        /// <summary>
        /// Outputi koje cuvamo samo za testiranje modela.
        /// Isto kao i XTest
        /// </summary>
        private List<List<double>> _Ytest;


        #endregion

        #region Propertiji

        public Dictionary<string, bool> KategorickiAtributi
        {
            get { return _kategorickiAtributi; }
            set { _kategorickiAtributi = value; }
        }

        public NormalizatorPodataka Normalizacija
        {
            get { return _normalizacija; }
            set { _normalizacija = value; }
        }

        public List<List<double>> UlazneKoloneTrainSeta
        {
            get { return _ulazneKoloneTrainSeta; }
            set { _ulazneKoloneTrainSeta = value; }
        }

        public List<List<double>> IzlazneKoloneTrainSeta
        {
            get { return _izlazneKoloneTrainSeta; }
            set { _izlazneKoloneTrainSeta = value; }
        }

        public List<List<double>> IzlazneKoloneTestSeta
        {
            get { return _izlazneKoloneTestSeta; }
            set { _izlazneKoloneTestSeta = value; }
        }

        public List<List<double>> UlazneKoloneTestSeta
        {
            get { return _ulazneKoloneTestSeta; }
            set { _ulazneKoloneTestSeta = value; }
        }

        public DataReader CitacTrainPodataka
        {
            get { return _citacPodataka; }
            set { _citacPodataka = value; }
        }

        public DataReader CitacTestPodataka
        {
            get { return _citacTestPodataka; }
            set { _citacTestPodataka = value; }
        }

        public List<List<double>> X
        {
            get { return _X; }
            set { _X = value; }
        }

        public List<List<double>> Y
        {
            get { return _Y; }
            set { _Y = value; }
        }

        public List<List<double>> YTest
        {
            get { return _Ytest; }
            set { _Ytest = value; }
        }

        public List<List<double>> XTest
        {
            get { return _Xtest; }
            set { _Xtest = value; }
        }
        
        #endregion

        /// <summary>
        /// Proslediti indekse kolona za ucitavanje kako bi setovali X inpute,
        /// a proslediti indekse kolona koje predstavljaju izlaze kako bi setovali Y odnosno
        /// OUTPUT-e mreze.
        /// 
        /// NAPOMENA: Ukoliko su train i test set odvojeni fajlovi, procenat test podataka treba da bude 0 !!!
        /// Da ne bi dosli u situaciju da iz train seta uzimamo podatke i cuvamo ih kao ulaze/izlaze u test setu.
        /// </summary>
        /// <param name="indeksiKolonaZaUcitavanjeInputa"> indeksi kolona koje zelimo ucitati iz data seta ( a predstavljaju ulazne atribute u mrezu ) </param>
        /// <param name="indeksiKolonaZaUcitavanjeOutputa"> indeksi kolona koje zelimo ucitati iz data seta (a predstavljaju izlazne atribute iz mreze) </param>
        /// <param name="procenatTestPodataka"> Procenat koliko zelimo da imamo testnih podataka od svih ucitanih </param>"
        public FileDAO(List<int> indeksiKolonaZaUcitavanjeInputa, List<int> indeksiKolonaZaUcitavanjeOutputa, int procenatTestPodataka)
        {

            UlazneKoloneTrainSeta = new List<List<double>>();
            IzlazneKoloneTrainSeta = new List<List<double>>();
            UlazneKoloneTestSeta = new List<List<double>>();
            IzlazneKoloneTestSeta = new List<List<double>>();


            Normalizacija = new NormalizatorPodataka();
            CitacTrainPodataka = new DataReader("train.csv");
            //CitacTestPodataka = new DataReader("test.csv");


            X = new List<List<double>>();
            Y = new List<List<double>>();

            XTest = new List<List<double>>();
            YTest = new List<List<double>>();


            // TRAIN podaci
            UlazneKoloneTrainSeta = ucitajUlazneKolone(indeksiKolonaZaUcitavanjeInputa, CitacTrainPodataka);
            odrediInpute(procenatTestPodataka);

            IzlazneKoloneTrainSeta = ucitajIzlazneKolone(indeksiKolonaZaUcitavanjeOutputa, CitacTrainPodataka);
            odrediOutpute(procenatTestPodataka);

            // TEST podaci - OVDE IH NEMAM U POSEBNOM FAJLU NEGO SVE U TRAINU
            //UlazneKoloneTestSeta = ucitajUlazneKolone(indeksiKolonaZaUcitavanjeInputa, CitacTestPodataka);
            //odrediInputeTestSeta();

            //IzlazneKoloneTestSeta = ucitajIzlazneKolone(indeksiKolonaZaUcitavanjeOutputa, CitacTestPodataka);
            //odrediOutputeTestSeta();

        }


        #region Ucitavanje svih kolona

        /// <summary>
        /// Ucitavanje svih trazenih ulaznih kolona po prosledjenim indeksima
        /// tih kolona u tabeli.
        /// </summary>
        /// <param name="brojKolonaZaUcitavanjeInputa"></param>
        /// <param name="citacPodataka"> citac koji moze biti citac train seta podataka, test seta podataka, ako nam je sve u jednom, onda prosledjujemo citac kao train set</param>
        /// <returns></returns>
        private List<List<double>> ucitajUlazneKolone(List<int> brojKolonaZaUcitavanjeInputa, DataReader citacPodataka)
        {
            List<List<double>> ulazneKolone = new List<List<double>>();

            foreach (int kolona in brojKolonaZaUcitavanjeInputa)
            {
                List<double> podaciKolone = citacPodataka.ucitajPodatkeIzKolone(kolona);
                podaciKolone = Normalizacija.normalizujPodatkeNadIntervalom(podaciKolone,0,1);

                ulazneKolone.Add(podaciKolone);
            }

            return ulazneKolone;
        }


        /// <summary>
        /// Ucitavanje svih trazenih izlaznih kolona ( atributa)
        /// sa prosledjenim indeksima tih kolona u tabeli
        /// </summary>
        /// <param name="brojKolonaZaUcitanjeOutputa"></param>
        private List<List<double>> ucitajIzlazneKolone(List<int> brojKolonaZaUcitanjeOutputa, DataReader citacPodataka)
        {
            List<List<double>> izlazne = new List<List<double>>();

            foreach (int kolona in brojKolonaZaUcitanjeOutputa)
            {
                List<double> podaciKolone = citacPodataka.ucitajPodatkeIzKolone(kolona);
                podaciKolone = Normalizacija.normalizujPodatkeNadIntervalom(podaciKolone,0,1);

                izlazne.Add(podaciKolone);
            }

            return izlazne;
        }
        #endregion

        #region Odredjivanje inputa na osnovu ucitanih ulaznih kolona

        /// <summary>
        /// Inicijalizacija X inputa u neuronskoj mrezi.
        /// 
        /// Voditi racuna da bude tek nakon ucitavanja kolona ulaza.
        /// 
        /// Ideja: 
        /// Prodjem kroz svaki red ucitanih ulaznih kolona
        /// i dodam taj red (sample of row, tj tu jednu kombinaciju ulaza)
        /// u listu svih ulaza X (red je takodje lista pa je X zato lista listi)
        /// </summary>
        /// <param name="procenatTestPodataka"> Procenat koliko zelimo da bude test podataka iz data seta </param>
        private void odrediInpute(int procenatTestPodataka)
        {
            int ukupnoEntitetaSistema = CitacTrainPodataka.UcitaniRedovi.Length;
            int indeksPocetkaTestPodataka = ukupnoEntitetaSistema * procenatTestPodataka / 100;

            // CitacPodataka.UcitaniRedovi.Length; jer za svaki input imam jednu kolonu, te to predstavlja dimenziju X [inputa]
            for (int indeksReda = 0; indeksReda < ukupnoEntitetaSistema; indeksReda++)
            {
                List<double> sample = new List<double>();           // jedan sample/ kombinacija INPUTA u mrezu
                foreach (List<double> kolona in UlazneKoloneTrainSeta)
                {
                    sample.Add(kolona[indeksReda]);
                }
                if(indeksReda < indeksPocetkaTestPodataka)
                {
                    // Nakon sto prodjem kroz jedan red/sample, dodam ga u listu TRAIN INPUTA
                    XTest.Add(sample);
                }
                else
                {
                    // Nakon sto prodjem kroz jedan red/sample, dodam ga u listu INPUTA
                    X.Add(sample);
                }
                

            }
        }

        #endregion

        #region Odredjivanje outputa na osnovu ucitanih izlaznih kolona

        /// <summary>
        /// Inicijalizacija Y outputa u neuronskoj mrezi.
        /// 
        /// Voditi racuna da bude tek nakon sto ucitamo kolone koje predstavljaju izlaze.
        /// 
        /// Ideja je kao i kod odredjivanja inputa samo sad za outpute
        /// </summary>
        /// <param name="procenatTestPodataka"> Procenat koliko zelimo da bude test podataka iz data seta </param>
        private void odrediOutpute(int procenatTestPodataka)
        {
            int ukupnoEntitetaSistema = CitacTrainPodataka.UcitaniRedovi.Length;
            int indeksPocetkaTestPodataka = ukupnoEntitetaSistema * procenatTestPodataka / 100;

            for (int indeksReda = 0; indeksReda < CitacTrainPodataka.UcitaniRedovi.Length; indeksReda++)
            {
                List<double> sample = new List<double>();           // jedan sample/ kombinacija OUTPUT-a na izlazu mreze
                foreach (List<double> kolona in IzlazneKoloneTrainSeta)
                {
                    sample.Add(kolona[indeksReda]);
                }

                if(indeksReda < indeksPocetkaTestPodataka)
                {
                    // Nakon sto prodjem kroz jedan red/sample, dodam ga u listu kombinacija OUTPUTA
                    YTest.Add(sample);
                }
                else
                {
                    // Nakon sto prodjem kroz jedan red/sample, dodam ga u listu kombinacija OUTPUTA
                    Y.Add(sample);
                }

                

            }
        }


        #endregion

        #region Odredjivanje inputa TEST podataka na osnovu ucitanih ulaznih kolona iz TEST fajla

        /// <summary>
        /// Odredjivanje X inputa testa kada imamo zasebno test fajla 
        /// </summary>
        private void odrediInputeTestSeta()
        {

            // CitacPodataka.UcitaniRedovi.Length; jer za svaki input imam jednu kolonu, te to predstavlja dimenziju X [inputa]
            for (int indeksReda = 0; indeksReda < CitacTestPodataka.UcitaniRedovi.Length; indeksReda++)
            {
                List<double> sample = new List<double>();           // jedan sample/ kombinacija INPUTA u mrezu
                foreach (List<double> kolona in UlazneKoloneTestSeta)
                {
                    sample.Add(kolona[indeksReda]);
                }

                // Nakon sto prodjem kroz jedan red/sample, dodam ga u listu TEST INPUTA
                XTest.Add(sample);

            }
        }

        #endregion

        #region Odredjivanje outputa TEST podataka na osnovu ucitanih izlaznih kolona iz TEST fajla

        /// <summary>
        /// Odredjivanje output-a odnosno YTesta.
        /// 
        /// Posto znam da odredjujem outpute test seta samo je potrebno
        /// dodati taj sample u YTest
        /// </summary>
        private void odrediOutputeTestSeta()
        {

            for (int indeksReda = 0; indeksReda < CitacTestPodataka.UcitaniRedovi.Length; indeksReda++)
            {
                List<double> sample = new List<double>();           // jedan sample/ kombinacija OUTPUT-a na izlazu mreze
                foreach (List<double> kolona in IzlazneKoloneTestSeta)
                {
                    sample.Add(kolona[indeksReda]);
                }
                
                YTest.Add(sample);

            }
        }

        #endregion
    }
}
