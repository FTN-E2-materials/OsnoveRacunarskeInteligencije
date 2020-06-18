using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ComputationalGraph.Utilities
{
    public class Score
    {
        public Score()
        {

        }

        public void scoreModel(DAO.FileDAO fileDAO, NeuralNetwork network)
        {
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
        }
    }
}
