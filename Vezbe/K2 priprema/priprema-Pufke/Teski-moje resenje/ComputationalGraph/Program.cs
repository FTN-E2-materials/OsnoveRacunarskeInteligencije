using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Threading;

namespace ComputationalGraph
{
    class Program
    {
        enum Header
        {
            SNO, Actual, Pred, Alive, Plod, Name, Title, Male, Culture, dateOfBirth, dateOfDeath, mother, father, heir, house, spouse, book1, book2,
            book3, book4, book5, isAliveMother, isAliveFather, isAliveHeir, isAliveSpouse, isMarried, isNobleD, age, numDeathRelations, boolDeathRelations, isPopular, Popularity, isAliveD
        }



        static void Main(string[] args)
        {
            //Kreiranje neuronske mreze
            NeuralNetwork network = new NeuralNetwork();
            network.Add(new NeuralLayer(6, 3, "sigmoid"));
            network.Add(new NeuralLayer(3, 2, "sigmoid"));
            network.Add(new NeuralLayer(2, 1, "sigmoid"));

            List<int> pol = new List<int>();
            List<double> popularnost = new List<double>();
            List<int> brojPojavljivanja = new List<int>();
            List<int> isNoble = new List<int>();
            List<int> ubijenoUOkolini = new List<int>();
            List<int> pripadaPorodici = new List<int>();
            List<int> isAlive = new List<int>();
            Dictionary<String, int> houseMap = new Dictionary<String, int>();

            String[] dataLines = File.ReadAllLines(@"./../../dataset.csv");
       
            dataLines = dataLines.Skip(1).ToArray(); //Skipujemo heder u CSV fajlu




            foreach (String linija in dataLines)
            {
                string[] parts = linija.Split(',');
                pol.Add(int.Parse(parts[(int)Header.Male]));
                popularnost.Add(ConvertToDouble(parts[(int)Header.Popularity]));


                int firstBook = int.Parse((parts[(int)Header.book1]));
                int secondBook = int.Parse((parts[(int)Header.book2]));
                int thirdBook = int.Parse((parts[(int)Header.book3]));
                int fourthBook = int.Parse((parts[(int)Header.book4]));
                int fifthBook = int.Parse((parts[(int)Header.book5]));

                int brojPojava = firstBook + secondBook + thirdBook + fourthBook + fifthBook;

                brojPojavljivanja.Add(brojPojava);


                isNoble.Add(int.Parse(parts[(int)Header.isNobleD]));
                ubijenoUOkolini.Add(int.Parse(parts[(int)Header.numDeathRelations]));

                String porodica = parts[(int)Header.house];

                if (!porodica.Equals(""))
                {
                    if (!houseMap.ContainsKey(porodica))
                    {
                        houseMap.Add(porodica, houseMap.Count);

                        pripadaPorodici.Add(houseMap.Count);
                    }
                    else
                    {
                        pripadaPorodici.Add(houseMap[porodica]);
                    }
                }
                else
                {
                    pripadaPorodici.Add(houseMap.Count);
                }

                isAlive.Add(int.Parse((parts[(int)Header.isAliveD])));
            }
            //Noramalizujemo podatke 

            List<double> brojPojavljivanjaNormalizovani = normalizacijaInt(brojPojavljivanja);
            List<double> ubijenoUOkoliniNormalizovani = normalizacijaInt(ubijenoUOkolini);
            List<double> pripadaPorodiciNormalizovani = normalizacijaInt(pripadaPorodici);


            List<List<double>> X = new List<List<double>>();
            List<List<double>> Y = new List<List<double>>();

            // prolazimo kroz sve likove
            for (int i = 389; i < 1946; i++)
            {
                double[] xTemp = { pol[i], popularnost[i], brojPojavljivanjaNormalizovani[i], isNoble[i], ubijenoUOkoliniNormalizovani[i], pripadaPorodiciNormalizovani[i] };
                X.Add(xTemp.ToList());


                double[] yTemp = { (double)isAlive[i] };
                Y.Add(yTemp.ToList());
            }
     
            Console.WriteLine("Obuka pocela");
            network.fit(X, Y, 0.1, 0.9, 500);

            Console.WriteLine("Gotova obuka");

            double dobrih = 0;
            for (int i = 0; i < 389; i++)
            {
                double[] x1 = { pol[i], popularnost[i], brojPojavljivanjaNormalizovani[i], isNoble[i], ubijenoUOkoliniNormalizovani[i], pripadaPorodiciNormalizovani[i] };
                int iAmAlive = -1;
                if (network.predict(x1.ToList())[0] < 0.5)
                {
                    iAmAlive = 0;
                }
                else
                {
                    iAmAlive = 1;
                }
                if (isAlive[i] == iAmAlive)
                {
                    dobrih++;
                }
            }
            Console.WriteLine("Dobrih: " + dobrih + "/389");
            double procenattacnosti = (dobrih / 389) * 100;
            Console.WriteLine("Tačnost je = " + procenattacnosti + " %");
            Console.ReadLine();



             List<double> normalizacijaInt(List<int> listaVrednosti)
            {
                List<double> noramlizovanaLista = new List<double>();

                double dataMax = listaVrednosti.Max();
                double dataMin = listaVrednosti.Min();
              //  Console.WriteLine("Maksimalna vrendost je" + dataMax);
                //Console.WriteLine("Minimalna vrendost je" + dataMin);

                foreach (double vrednost in listaVrednosti)
                {
                    noramlizovanaLista.Add((vrednost - dataMin) / (dataMax - dataMin));
                   // Console.WriteLine("Minimalna vrendost je" + (vrednost - dataMin) / (dataMax - dataMin));
                }
                return noramlizovanaLista;
            }

             double ConvertToDouble(string s)
            {
                char systemSeparator = Thread.CurrentThread.CurrentCulture.NumberFormat.CurrencyDecimalSeparator[0];
                double result = 0;
                try
                {
                    if (s != null)
                        if (!s.Contains(","))
                            result = double.Parse(s, CultureInfo.InvariantCulture);
                        else
                            result = Convert.ToDouble(s.Replace(".", systemSeparator.ToString()).Replace(",", systemSeparator.ToString()));
                }
                catch (Exception e)
                {
                    try
                    {
                        result = Convert.ToDouble(s);
                    }
                    catch
                    {
                        try
                        {
                            result = Convert.ToDouble(s.Replace(",", ";").Replace(".", ",").Replace(";", "."));
                        }
                        catch
                        {
                            throw new Exception("Wrong string-to-double format");
                        }
                    }
                }
                return result;
            }

        }
    }
}
