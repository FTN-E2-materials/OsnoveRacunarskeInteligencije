using ComputationalGraph.DAO;
using ComputationalGraph.Utilities;
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
            #region Podesavanja koje cemo kolone ucitavati

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


            #region Vektori za inpute

            List<List<double>> X = new List<List<double>>();
            X = fileDAO.X;

            #endregion

            #region Vektori za outpute

            List<List<double>> Y = new List<List<double>>();
            Y = fileDAO.Y;

            #endregion

            #region Kreiranje neuronske mreze

            NeuralNetwork network = new NeuralNetwork();
            network.Add(new NeuralLayer(X[0].Count, 4, "sigmoid")); // X[0], ali moze i X[bilo koji idx], bitno je da tako dobijem broj ulaza 
            network.Add(new NeuralLayer(4, 2, "sigmoid"));
            network.Add(new NeuralLayer(2, 1, "sigmoid"));

            #endregion

            #region Fitovanje

            Console.WriteLine("Obuka pocela.");
            network.fit(X, Y, 0.1, 0.9, 10);
            Console.WriteLine("Kraj obuke.");

            #endregion

            #region Predikcija

            Score score = new Score();

            score.scoreModel(fileDAO, network);

            #endregion

        }

        
    }
}
