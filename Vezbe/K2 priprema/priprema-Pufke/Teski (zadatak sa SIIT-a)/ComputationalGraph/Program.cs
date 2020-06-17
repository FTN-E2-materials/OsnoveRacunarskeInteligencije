using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Threading;

namespace ComputationalGraph
{
    class Program
    {
        enum Header
        {
            id, col_1, col_2, col_3, col_4, col_5

        }
        static void Main(string[] args)
        {
            //Kreiranje neuronske mreze
            NeuralNetwork network = new NeuralNetwork();
            network.Add(new NeuralLayer(4, 3, "sigmoid"));
            network.Add(new NeuralLayer(3, 2, "sigmoid"));
            network.Add(new NeuralLayer(2, 1, "sigmoid"));


            List<double> col_1 = new List<double>();
            List<double> col_2 = new List<double>();
            List<double> col_3 = new List<double>();
            List<double> col_4 = new List<double>();
            List<double> col_5 = new List<double>();

            List<double> col_1Test = new List<double>();
            List<double> col_2Test = new List<double>();
            List<double> col_3Test = new List<double>();
            List<double> col_4Test = new List<double>();
            List<double> col_5Test = new List<double>();


            String[] dataLines = File.ReadAllLines(@"./../../train.csv");

            dataLines = dataLines.Skip(1).ToArray(); //Skipujemo heder u CSV fajlu

            foreach (String linija in dataLines)
            {
                string[] parts = linija.Split(',');
                col_1.Add(double.Parse(parts[(int)Header.col_1], CultureInfo.InvariantCulture));
                col_2.Add(double.Parse(parts[(int)Header.col_2], CultureInfo.InvariantCulture));
                col_3.Add(double.Parse(parts[(int)Header.col_3], CultureInfo.InvariantCulture));
                col_4.Add(double.Parse(parts[(int)Header.col_4], CultureInfo.InvariantCulture));
                
                String type = (parts[(int)Header.col_5]);
                if (type.Equals("type_1"))
                {
                    col_5.Add(0);
                }
                else if (type.Equals("type_2"))
                {
                    col_5.Add(0.5);
                }
                else
                {
                    col_5.Add(1);
                }

            }

            List<double> col_1Normalizovani = normalizacijaInt(col_1);
            List<double> col_2Normalizovani = normalizacijaInt(col_2);
            List<double> col_3Normalizovani = normalizacijaInt(col_3);
            List<double> col_4Normalizovani = normalizacijaInt(col_4);


            List<List<double>> X = new List<List<double>>();
            List<List<double>> Y = new List<List<double>>();

            for (int i = 0; i < dataLines.Length;  i++)
            {
                double[] xTemp = { col_1Normalizovani[i], col_2Normalizovani[i], col_3Normalizovani[i], col_4Normalizovani[i]};
                X.Add(xTemp.ToList());


                double[] yTemp = { col_5[i] };
                Y.Add(yTemp.ToList());
            }

            Console.WriteLine("Training started");
            network.fit(X, Y, 0.1, 0.9, 500);
            Console.WriteLine("Training ended");

            // String[] dataLinesTest = File.ReadAllLines(@"./../../test.csv");
            String[] dataLinesTest = File.ReadAllLines(@"./../../test.csv");
            dataLinesTest = dataLinesTest.Skip(1).ToArray(); //Skipujemo heder u CSV fajlu
      


            foreach (String linija in dataLinesTest)
            {
                string[] parts = linija.Split(',');

                col_1Test.Add(double.Parse(parts[(int)Header.col_1], CultureInfo.InvariantCulture));
                col_2Test.Add(double.Parse(parts[(int)Header.col_2], CultureInfo.InvariantCulture));
                col_3Test.Add(double.Parse(parts[(int)Header.col_3], CultureInfo.InvariantCulture));
                col_4Test.Add(double.Parse(parts[(int)Header.col_4], CultureInfo.InvariantCulture));
              //  Console.WriteLine(double.Parse(parts[(int)Header.col_4], CultureInfo.InvariantCulture));
                String type = (parts[(int)Header.col_5]);
                if (type.Equals("type_1"))
                {
                    col_5Test.Add(0);
                }
                else if (type.Equals("type_2")) { 
                    
                    col_5Test.Add(0.5);
                }
                else
                {
                    col_5Test.Add(1);
                }

            }
            List<double> col_1TestNormalizovani = normalizacijaInt(col_1Test);
            List<double> col_2TestNormalizovani = normalizacijaInt(col_2Test);
            List<double> col_3TestNormalizovani = normalizacijaInt(col_3Test);
            List<double> col_4TestNormalizovani = normalizacijaInt(col_4Test);
          /*  foreach (double d in col_4TestNormalizovani)
            {
                Console.WriteLine(d);
            }*/
            double dobrih = 0;
            double broj = 0;
            for (int i = 0; i < dataLinesTest.Length; i++)
            {
                double[] x1 = { col_1TestNormalizovani[i], col_2TestNormalizovani[i], col_3TestNormalizovani[i], col_4TestNormalizovani[i] };
                double type = 0;
                if (network.predict(x1.ToList())[0] < 0.25)
                {
                    type = 0;
                }
                else if(network.predict(x1.ToList())[0] > 0.25 && network.predict(x1.ToList())[0] < 0.75)
                {
                    type = 0.5;
                }
                else
                {
                    type = 1;
                }
                if (col_5Test[i] == type)
                {
                    dobrih++;
                 
                }
                Console.WriteLine("PRediktovana vrednost je " + network.predict(x1.ToList())[0]);
                Console.WriteLine("Stvarna vrednost je " + col_5Test[i]);
                broj++;
            }

            Console.WriteLine("Dobro prediktovanih je  " + dobrih + " od "+ broj);
            double procenattacnosti = (dobrih / broj) * 100;
            Console.WriteLine("Tačnost je = " + procenattacnosti + " %");
            Console.ReadLine();



            // Pomocne funckije 
            List<double> normalizacijaInt(List<double> listaVrednosti)
            {
                List<double> noramlizovanaLista = new List<double>();

                double dataMax = listaVrednosti.Max();
                double dataMin = listaVrednosti.Min();

                foreach (double vrednost in listaVrednosti)
                {
                    noramlizovanaLista.Add((vrednost - dataMin) / (dataMax - dataMin));
                    // Console.WriteLine("Minimalna vrendost je" + (vrednost - dataMin) / (dataMax - dataMin));
                }
                return noramlizovanaLista;
            }
          
            
        }
    }
}
