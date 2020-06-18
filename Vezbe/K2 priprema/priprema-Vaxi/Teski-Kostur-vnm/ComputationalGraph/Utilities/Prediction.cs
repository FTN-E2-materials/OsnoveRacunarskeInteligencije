using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ComputationalGraph.Utilities
{
    /// <summary>
    /// Algoritam na osnovu koga odredjujem predikciju 
    /// i broj pogodataka.
    /// </summary>
    public class Prediction
    {
        public Prediction()
        {

        }


        #region Algoritam odredjivanja broja pogodataka

        /// <summary>
        /// 
        /// Metoda koja daje broj pogodjenih predikcija,
        /// koja zavisi od algoritma odredjivanja da li je nesto
        /// bio ili nije bio pogodak.
        /// 
        /// </summary>
        /// <param name="fileDAO"></param>
        /// <param name="network"></param>
        /// <returns></returns>
        public int giveMeNumberOfHits(DAO.FileDAO fileDAO, NeuralNetwork network)
        {
            int hit = 0;
            for (int i = 0; i < fileDAO.XTest.Count; ++i)
            {
                List<Double> prediction = network.predict(fileDAO.XTest[i]);

                double tip = -1;
                if(prediction[0] < 0.33)
                {
                    tip = 0.0;
                }else if(prediction[0] >= 0.33 && prediction[0] < 0.66)
                {
                    tip = 0.5;
                }
                else
                {
                    tip = 1;
                }

                Console.WriteLine("Real result:{0}, Predicted result {1}", fileDAO.YTest[i][0], prediction[0]);

                if (tip == fileDAO.YTest[i][0])
                {
                    ++hit;
                }

            }

            return hit;
        }

        #endregion
    }
}
