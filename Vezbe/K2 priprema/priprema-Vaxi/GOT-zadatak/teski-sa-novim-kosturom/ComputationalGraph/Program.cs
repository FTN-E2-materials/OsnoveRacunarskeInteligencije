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
            #region Ucitavanje data seta

            List<int> indeksKolonaInputa = new List<int>();

            // popularity
            indeksKolonaInputa.Add(31);
            
            //male
            indeksKolonaInputa.Add(7);

            // books
            indeksKolonaInputa.Add(16);
            indeksKolonaInputa.Add(17);
            indeksKolonaInputa.Add(18);
            indeksKolonaInputa.Add(19);
            indeksKolonaInputa.Add(20);

            // isNoble
            indeksKolonaInputa.Add(26);

            // numDeadRelations
            indeksKolonaInputa.Add(28);

            // house
            indeksKolonaInputa.Add(14);

            List<int> indeksKolonaOutputa = new List<int>();
            // isAlive
            indeksKolonaOutputa.Add(32);

            FileDAO fileDAO = new FileDAO(indeksKolonaInputa, indeksKolonaOutputa, 20);

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
            network.Add(new NeuralLayer(X[0].Count, 2, "sigmoid")); // X[0], ali moze i X[bilo koji idx], bitno je da tako dobijem broj ulaza 
            network.Add(new NeuralLayer(2, 1, "sigmoid"));

            #endregion

            #region Fitovanje

            Console.WriteLine("Obuka pocela.");
            network.fit(X, Y, 0.1, 0.9, 500);
            Console.WriteLine("Kraj obuke.");

            #endregion

            #region Skor koji postize mreza

            Score score = new Score();
            score.giveMeScore(fileDAO, network);

            #endregion
        }

    }
}
