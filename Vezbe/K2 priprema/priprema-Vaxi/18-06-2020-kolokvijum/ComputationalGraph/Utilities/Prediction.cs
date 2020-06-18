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
        /// U ovom zadatku je algoritam preko tp,tn,fp,fn.
        /// 
        /// </summary>
        /// <param name="fileDAO"></param>
        /// <param name="network"></param>
        /// <returns></returns>
        public void givePredicton(DAO.FileDAO fileDAO, NeuralNetwork network)
        {

            double TP = 0.0;
            double TN = 0.0;
            double FP = 0.0;
            double FN = 0.0;

            for (int i = 0; i < fileDAO.XTest.Count; ++i)
            {
                List<Double> prediction = network.predict(fileDAO.XTest[i]);

                Console.WriteLine("Real result:{0}, Predicted result {1}", fileDAO.YTest[i][0], prediction[0]);

                if(prediction[0] > 0.5 && fileDAO.YTest[i][0] == 1)
                {
                    TP += 1.0;
                }

                if (prediction[0] < 0.5 && fileDAO.YTest[i][0] == 0)
                {
                    TN += 1.0;
                }

                if (prediction[0] > 0.5 && fileDAO.YTest[i][0] == 0)
                {
                    FP += 1.0;
                }

                if (prediction[0] < 0.5 && fileDAO.YTest[i][0] == 1)
                {
                    FN += 1.0;
                }

                

                
            }

            double precision = TP / (TP + FP);
            double recall = TP / (TP + FN);
            double FL_score = (precision * recall) / (precision + recall);

            Console.WriteLine("precision: " + precision);
            Console.WriteLine("recal: " + recall);
            Console.WriteLine("FL score: " + FL_score);
            Console.ReadLine();
        }

        #endregion
    }
}
