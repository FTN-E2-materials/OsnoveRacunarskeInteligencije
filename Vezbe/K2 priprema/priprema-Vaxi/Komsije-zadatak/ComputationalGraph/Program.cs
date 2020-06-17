using ComputationalGraph.DAO;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace ComputationalGraph
{
    /// <summary>
    /// Najcesce u nasem slucaju promenjivi deo aplikacije,
    /// zato sto je nas model black box koji je potrebno samo
    /// podesavati van kutije.
    /// </summary>
    class Program
    {
        static void Main(string[] args)
        {
            #region Biranje kolona za ucitavanje

            List<int> indeksKolonaInputa = new List<int>();

            /*
                lotArea [4]
                yearBuilt [19]
                Neighborhood [12]
                Bldgtype [15]
             */
            indeksKolonaInputa.Add(4);
            indeksKolonaInputa.Add(19);
            indeksKolonaInputa.Add(12);
            indeksKolonaInputa.Add(15);

            List<int> indeksKolonaOutputa = new List<int>();

            // SalePrice[80]
            indeksKolonaOutputa.Add(80);

            #endregion

            int velicinaBlokaValidacije = 25;

            // Blok predstavlja velicinu bloka u cross validaciji koju uzimamo za test podatke
            for(int blok = 0; blok <= 75; blok += velicinaBlokaValidacije)
            {
                FileDAO fileDAO = new FileDAO(indeksKolonaInputa, indeksKolonaOutputa, blok, blok+25);



                #region Kreiranje neuronske mreze

                NeuralNetwork network = new NeuralNetwork();
                network.Add(new NeuralLayer(indeksKolonaInputa.Count, 2, "sigmoid"));
                network.Add(new NeuralLayer(2, 1, "sigmoid"));

                #endregion

                #region Vektori za inpute

                List<List<double>> X = new List<List<double>>();
                X = fileDAO.X;

                #endregion

                #region Vektori za outpute

                List<List<double>> Y = new List<List<double>>();
                Y = fileDAO.Y;

                #endregion

                #region Fitovanje

                Console.WriteLine("Obuka pocela.");
                network.fit(X, Y, 0.1, 0.9, 500);
                Console.WriteLine("Kraj obuke.");

                #endregion

                #region Predikcija

                int ukupnoPogodjenih = 0;
                for (int i = 0; i < fileDAO.XTest.Count; ++i)
                {
                    List<Double> prediction = network.predict(fileDAO.XTest[i]);

                    //Console.WriteLine("Pravi rezultat u test podacima je: {0}, Po proracunu i predikciji dobijam {1}", fileDAO.YTest[i][0], prediction[0]);


                    // Podesavam koliko tolerisem da cena ide gore dole [ posto sam u domenu [0-1] normalizovao , 0.1 je 10% , 0.2 je 20 %
                    // voditi racuna da ako stavimo 0.2 da ce on tolerisati i za gore i za dole po 20%, sto je 40 % tolerancije
                    double tolerancija = 0.2;

                    if (prediction[0] <= fileDAO.YTest[i][0] + tolerancija && prediction[0] >= fileDAO.YTest[i][0] - tolerancija)
                        ++ukupnoPogodjenih;

                }

                Console.Write("Pogodjeno {0} od {1} ", ukupnoPogodjenih, fileDAO.YTest.Count);
                Console.Write("Tacnost {0} % \n\n", ukupnoPogodjenih * 100 / fileDAO.YTest.Count);

                #endregion
            }

            Console.ReadLine();

            
        }
    }
}
