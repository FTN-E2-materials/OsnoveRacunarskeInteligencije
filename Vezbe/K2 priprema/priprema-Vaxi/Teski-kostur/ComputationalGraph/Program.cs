using ComputationalGraph.DAO;
using System;
using System.Collections.Generic;
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
            FileDAO fileDAO = new FileDAO();
            // fileDAO.imaKategorickihAtributa
            // fileDAO.getKategorickeAtribute

            #region Kreiranje neuronske mreze

            NeuralNetwork network = new NeuralNetwork();
            network.Add(new NeuralLayer(2, 2, "sigmoid"));
            network.Add(new NeuralLayer(2, 1, "sigmoid"));

            #endregion

            #region Vektori za inpute

            double[] x1 = { 0.0, 0.0 };
            double[] x2 = { 0.0, 1.0 };
            double[] x3 = { 1.0, 0.0 };
            double[] x4 = { 1.0, 1.0 };
            List<List<double>> X = new List<List<double>>() { x1.ToList(), x2.ToList(), x3.ToList(),x4.ToList()};

            #endregion

            #region Vektori za outpute

            double[] y1 = { 0.0 };
            double[] y2 = { 1.0 };
            double[] y3 = { 1.0 };
            double[] y4 = { 0.0 };
            List<List<double>> Y = new List<List<double>>() { y1.ToList(), y2.ToList(), y3.ToList(), y4.ToList() };

            #endregion

            #region Fitovanje

            network.fit(X, Y, 0.1, 0.9, 10000);

            #endregion

            #region Predikcija


            /*
             * Posto imamo samo jedan izlaz, predict ce nam vratiti vektor od jednog elementa,
             * da imamo vise izlaza, uzimali bi koji nam treba izlaz tj. [brojIzlaza]
             */
            Console.WriteLine(network.predict(x1.ToList())[0]);
            Console.WriteLine(network.predict(x2.ToList())[0]);
            Console.WriteLine(network.predict(x3.ToList())[0]);
            Console.WriteLine(network.predict(x4.ToList())[0]);
            Console.ReadLine();

            #endregion
        }
    }
}
