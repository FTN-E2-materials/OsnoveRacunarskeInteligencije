using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace ComputationalGraph
{
    class DataManipulation
    { //test i train podaci treba da budu podeljeni u odnosu 20% test, 80% train
        private String[] textLines;
        private String[] hederiKolona;
        public List<List<double>> test = new List<List<double>>();
        public List<List<double>> testY = new List<List<double>>();
        public List<List<double>> train = new List<List<double>>();
        public List<List<double>> trainY = new List<List<double>>();
        
        private Dictionary<String, double> houses = new Dictionary<String, double>();

        public void readDataFromDataSet()
        {
            textLines = File.ReadAllLines(@"./../../dataset.csv");
            hederiKolona = textLines[0].Split(',');
            textLines = textLines.Skip(1).ToArray(); //Skipujemo heder u CSV fajlu

            int GranicaZaDeljenjePodataka = textLines.Length * 20 / 100;
            int index = 0;

            for (int i = 0; i < textLines.Length; ++i)
            {
                String line = textLines[i];
                String[] tokens = line.Split(',');

                //isAlive se predvidja
                index = findhederiKolona("isAlive");
                double isAlive = Double.Parse(tokens[index]);

                //polja koja se koriste kao ulazi
                index = findhederiKolona("male");
                double male = Double.Parse(tokens[index]);
                index = findhederiKolona("popularity");
                double popularity = Double.Parse(tokens[index]);
                index = findhederiKolona("isNoble");
                double isNoble = Double.Parse(tokens[index]);
                index = findhederiKolona("numDeadRelations");
                double nomDeadRelations = Double.Parse(tokens[index]);
                double books = bookCount(tokens);
                index = findhederiKolona("house");
                double house = whatsMyHouse(tokens[index]);

                if (i > GranicaZaDeljenjePodataka)
                {
                    //dodaj u train
                    trainY.Add(new List<double>() { isAlive });
                    train.Add(new List<double>() { male, popularity, isNoble, nomDeadRelations, books, house });
                }
                else
                {
                    //dodaj u test
                    testY.Add(new List<double>() { isAlive });
                    test.Add(new List<double>() { male, popularity, isNoble, nomDeadRelations, books, house });
                }

            }

            normalize(test);
            normalize(train);
        }
        private double whatsMyHouse(String house)
        {
            if (houses.ContainsKey(house))
            {
                return houses[house];
            }
            else
            {
                //kuca nije u recniku, dodaj je
                houses.Add(house, houses.Count);
                return houses[house];
            }
        }


            //Funckija koja broji u koliko knjiga se javlja
        private double bookCount(String[] tokens)
        {
            double ret = 0;

            int index = findhederiKolona("book1");
            ret += Double.Parse(tokens[index]);

            index = findhederiKolona("book2");
            ret += Double.Parse(tokens[index]);

            index = findhederiKolona("book3");
            ret += Double.Parse(tokens[index]);

            index = findhederiKolona("book4");
            ret += Double.Parse(tokens[index]);

            index = findhederiKolona("book5");
            ret += Double.Parse(tokens[index]);

            return ret;
        }

        //Funckija koja vraca broj kolone na osnovu njenog naziva
        private int findhederiKolona(String title)
        {
            for (int i = 0; i < hederiKolona.Length; ++i)
            {
                if (title.Equals(hederiKolona[i]))
                {
                    return i;
                }
            }

            return -1;
        }
        //normalizacija svih podataka -> svi se svode na [0,1]
        //formula: zi = (xi - min(x)) / (max(x) - min(x))
        //x - vektor svih vrednosti nekog ulaza (npr col_1 je x)
        //xi - vrednost i-tog elementa u vektor x
        //zi - normalizovana vrednost x
        public void normalize(List<List<double>> input)
        {
            //trazenje min i max za svaku kolonu
            //mins sadrzi minimum svake kolone, maxs maksimum
            List<double> mins = new List<double>();
            List<double> maxs = new List<double>();
            for (int i = 0; i < input[0].Count; ++i)
            {
                mins.Add(input[0][i]);
                maxs.Add(input[0][i]);
            }

            for (int i = 1; i < input.Count; ++i)
            {
                for (int j = 0; j < input[i].Count; ++j)
                {
                    if (input[i][j] < mins[j])
                    {
                        mins[j] = input[i][j];
                    }

                    if (input[i][j] > maxs[j])
                    {
                        maxs[j] = input[i][j];
                    }
                }
            }

            //normalizacija vrednosti
            for (int i = 0; i < input.Count; ++i)
            {
                for (int j = 0; j < input[i].Count; ++j)
                {
                    input[i][j] = (input[i][j] - mins[j]) / (maxs[j] - mins[j]);
                }
            }
        }
    }
}
