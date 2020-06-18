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

            // Gender
            indeksKolonaInputa.Add(1);

            // Married
            indeksKolonaInputa.Add(2);
            
            // education
            indeksKolonaInputa.Add(4);

            // ApplicantIncome
            indeksKolonaInputa.Add(6);

            //CoapplicantIncome
            indeksKolonaInputa.Add(7);

            //LoanAmount
            indeksKolonaInputa.Add(8);

            List<int> indeksKolonaOutputa = new List<int>();

            // Loan_Status
            indeksKolonaOutputa.Add(12);

            FileDAO fileDAO = new FileDAO(indeksKolonaInputa, indeksKolonaOutputa, 30); // 30 jer toliko zelim za test podatke 

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
            network.Add(new NeuralLayer(X[0].Count, 2, "tanh")); // X[0], ali moze i X[bilo koji idx], bitno je da tako dobijem broj ulaza 
            network.Add(new NeuralLayer(2, 1, "tanh"));

            #endregion

            #region Fitovanje

            Console.WriteLine("Obuka pocela.");
            network.fit(X, Y, 0.1, 0.9, 300);
            Console.WriteLine("Kraj obuke.");

            #endregion

            #region Skor koji postize mreza

            Score score = new Score();
            score.giveMeScore(fileDAO, network);

            #endregion
        }

    }
}
