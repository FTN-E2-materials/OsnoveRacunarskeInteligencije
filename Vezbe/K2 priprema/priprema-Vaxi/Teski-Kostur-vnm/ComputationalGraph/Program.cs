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
            
            // ulazi od col_1 do col_5
            indeksKolonaInputa.Add(1);
            indeksKolonaInputa.Add(2);
            indeksKolonaInputa.Add(3);
            indeksKolonaInputa.Add(4);

            List<int> indeksKolonaOutputa = new List<int>();
            // col_5 izlaz
            indeksKolonaOutputa.Add(5);

            FileDAO fileDAO = new FileDAO(indeksKolonaInputa, indeksKolonaOutputa, 0);
            // fileDAO.imaKategorickihAtributa
            // fileDAO.getKategorickeAtribute

            #endregion

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

                Console.WriteLine("Pravi rezultat u test podacima je: {0}, Po proracunu i predikciji dobijam {1}", fileDAO.YTest[i][0], prediction[0]);
                // pravi rezultat mi moze biti 0, 0.5 ili 1 pa onda ove sto sam dobio, moram pretvoriti u tip
                // i proveriti da li su odgovarajuceg tipa 

                double tip = -1;
                if(prediction[0] <= 0.33)
                {
                    tip = 0;
                }else if(prediction[0] > 0.33 && prediction[0] <= 0.66)
                {
                    tip = 0.5;
                }
                else
                {
                    tip = 1;
                }

                if (fileDAO.YTest[i][0] == tip)
                    ++ukupnoPogodjenih;

            }

            Console.Write("Pogodjeno {0} od {1} ", ukupnoPogodjenih, fileDAO.YTest.Count);
            Console.Write("Tacnost {0} %", ukupnoPogodjenih * 100 / fileDAO.YTest.Count);
            Console.ReadLine();

            #endregion
        }
    }
}
