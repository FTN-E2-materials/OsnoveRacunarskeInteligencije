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
            #region Ucitavanje data seta

            List<int> indeksKolonaInputa = new List<int>();
            
            // ulazi za pojavljivanje u knjigama
            indeksKolonaInputa.Add(16);
            indeksKolonaInputa.Add(17);
            indeksKolonaInputa.Add(18);
            indeksKolonaInputa.Add(19);
            indeksKolonaInputa.Add(20);

            // muski ili zensko ulaz
            indeksKolonaInputa.Add(7);
            // popularnost ulaz
            indeksKolonaInputa.Add(31);

            // plemicko poreklo ulaz
            indeksKolonaInputa.Add(26);

            // num dead relations ulaz
            indeksKolonaInputa.Add(28);

            // porodica kojoj pripada
            indeksKolonaInputa.Add(14);

            List<int> indeksKolonaOutputa = new List<int>();
            // isAlive ulaz
            indeksKolonaOutputa.Add(32);

            FileDAO fileDAO = new FileDAO(indeksKolonaInputa, indeksKolonaOutputa, 20);
            // fileDAO.imaKategorickihAtributa
            // fileDAO.getKategorickeAtribute

            #endregion

            #region Kreiranje neuronske mreze

            NeuralNetwork network = new NeuralNetwork();
            network.Add(new NeuralLayer(indeksKolonaInputa.Count, 4, "sigmoid"));
            network.Add(new NeuralLayer(4, 2, "sigmoid"));
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

            int pogodak = 0;
            for (int i = 0; i < fileDAO.XTest.Count; ++i)
            {
                List<Double> prediction = network.predict(fileDAO.XTest[i]);
                double alive = 0;
                if (prediction[0] > 0.5)
                {
                    alive = 1;
                }

                Console.WriteLine("Real result:{0}, Predicted result {1}", fileDAO.YTest[i][0], prediction[0]);

                if (alive == fileDAO.YTest[i][0])
                {
                    ++pogodak;
                }

            }

            Console.Write("Pogodjeno {0} od {1} ", pogodak, fileDAO.YTest.Count);
            Console.Write("Tacnost {0} %", pogodak * 100 / fileDAO.YTest.Count);
            Console.ReadLine();

            #endregion

        }
    }
}
