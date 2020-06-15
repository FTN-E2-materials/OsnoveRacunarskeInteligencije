using System;
using System.Collections.Generic;
using System.Linq;

namespace ComputationalGraph
{
    class Program
    {
        static void Main(string[] args)
        {
            DataManipulation dm = new DataManipulation();
            dm.readDataFromDataSet();

            NeuralNetwork network = new NeuralNetwork();
            network.Add(new NeuralLayer(6, 3, "sigmoid"));
            network.Add(new NeuralLayer(3, 2, "sigmoid"));
            network.Add(new NeuralLayer(2, 1, "sigmoid"));


            Console.WriteLine("Obuka pocela.");
            network.fit(dm.train, dm.trainY, 0.1, 0.9, 500);
            Console.WriteLine("Kraj obuke.");

            int pogodak = 0;
            for (int i = 0; i < dm.test.Count; ++i)
            {
                List<Double> prediction = network.predict(dm.test[i]);
                double alive = 0;
                if (prediction[0] > 0.5)
                {
                    alive = 1;
                }

                Console.WriteLine("Real result:{0}, Predicted result {1}", dm.testY[i][0], prediction[0]);

                if (alive == dm.testY[i][0])
                {
                    ++pogodak;
                }

            }

            Console.Write("Pogodjeno {0} od {1} ", pogodak, dm.testY.Count);
            Console.Write("Tacnost {0} %", pogodak * 100 / dm.testY.Count);
            Console.ReadLine();
        }
    }
}
