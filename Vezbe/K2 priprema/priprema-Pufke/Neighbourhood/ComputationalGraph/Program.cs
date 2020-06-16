using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace ComputationalGraph
{
    class Program
    {
        enum Header
        {
            Id, MSSubClass,  MSZoning,    LotFrontage, LotArea, Street,
            Alley,   LotShape,    LandContour, Utilities,   LotConfig,   LandSlope,   Neighborhood,
            Condition1,  Condition2,  BldgType,    HouseStyle,  OverallQual, OverallCond,
            YearBuilt,   YearRemodAdd,    RoofStyle,   RoofMatl,    Exterior1st, Exterior2nd,
            MasVnrType,  MasVnrArea,  ExterQual,   ExterCond,   Foundation,  BsmtQual ,   BsmtCond,
            BsmtExposure,    BsmtFinType1,    BsmtFinSF1,  BsmtFinType2,    BsmtFinSF2,
            BsmtUnfSF, TotalBsmtSF, Heating, HeatingQC, CentralAir, Electrical, FirstFlrSF,
            SecondndFlrSF,    LowQualFinSF,    GrLivArea,   BsmtFullBath,    BsmtHalfBath,    FullBath,
            HalfBath,    BedroomAbvGr,    KitchenAbvGr,    KitchenQual, TotRmsAbvGrd,    Functional,
            Fireplaces,  FireplaceQu, GarageType,  GarageYrBlt, GarageFinish,    GarageCars,
            GarageArea,  GarageQual,  GarageCond,  PavedDrive,  WoodDeckSF,  OpenPorchSF, EnclosedPorch,
            SsnPorch,   ScreenPorch, PoolArea,    PoolQC,  Fence,   MiscFeature ,MiscVal, MoSold ,
            YrSold, SaleType, SaleCondition, SalePrice

        }
        static void Main(string[] args)
        {
            NeuralNetwork network = new NeuralNetwork();
            network.Add(new NeuralLayer(4, 3, "sigmoid"));
            network.Add(new NeuralLayer(3, 2, "sigmoid"));
            network.Add(new NeuralLayer(2, 1, "sigmoid"));

            List<int> lotArea = new List<int>();
            List<int> yearBuilt = new List<int>();
            Dictionary<String, int> mapNeighbourhood = new Dictionary<String, int>();
            List<int> listNeighbourhood = new List<int>();
            List<double> listBldgType = new List<double>();//5 razlicitih vrsta
            List<int> salePrice = new List<int>();
         
            String[] dataLines = File.ReadAllLines(@"./../../train.csv");
            dataLines = dataLines.Skip(1).ToArray();

            foreach (String linija in dataLines)
            {
                string[] parts = linija.Split(',');
                lotArea.Add(int.Parse(parts[(int)Header.LotArea]));
                yearBuilt.Add(int.Parse(parts[(int)Header.YearBuilt]));

                String neighbourhood = parts[(int)Header.Neighborhood];
                if (!neighbourhood.Equals(""))
                {
                    if (!mapNeighbourhood.ContainsKey(neighbourhood))
                    {
                        mapNeighbourhood.Add(neighbourhood, mapNeighbourhood.Count);
                        listNeighbourhood.Add(mapNeighbourhood.Count);
                    }
                    else
                    {
                        listNeighbourhood.Add(mapNeighbourhood[neighbourhood]);
                    }
                }
                else
                {
                    listNeighbourhood.Add(mapNeighbourhood.Count);
                }

                //listBldgType
                String BldgType = parts[(int)Header.BldgType];
                if (BldgType.Equals("1Fam"))
                {
                    listBldgType.Add(0);
                }
                else if (BldgType.Equals("2fmCon"))
                {
                    listBldgType.Add(0.25);
                }
                else if (BldgType.Equals("Duplex"))
                {
                    listBldgType.Add(0.5);
                }
                else if (BldgType.Equals("Twnhs"))
                {
                    listBldgType.Add(0.75);
                }
                else if(BldgType.Equals("TwnhsE"))
                {
                    listBldgType.Add(1);
                }

                salePrice.Add(int.Parse(parts[(int)Header.SalePrice]));
            }
            List<double> lotAreaNormalizovani = normalizacijaInt(lotArea);
            List<double> yearBuiltNormalizovani = normalizacijaInt(yearBuilt);
            List<double> salePriceNormalizovani = normalizacijaInt(salePrice);
            List<double> listNeighbourhoodNormalizovani = normalizacijaInt(listNeighbourhood);

            List<List<double>> X = new List<List<double>>();
            List<List<double>> Y = new List<List<double>>();

            // prolazimo kroz sve likove
            for (int i = 292; i < 1459; i++)
            {
                double[] xTemp = { lotAreaNormalizovani[i], yearBuiltNormalizovani[i], listNeighbourhoodNormalizovani[i], listBldgType[i] };
                X.Add(xTemp.ToList());


                double[] yTemp = { (double)salePriceNormalizovani[i] };
                Y.Add(yTemp.ToList());
            }

            Console.WriteLine("Obuka pocela");
            network.fit(X, Y, 0.1, 0.9, 500);

            Console.WriteLine("Gotova obuka");

            int tacnoPrediktovanih = 0;
            for (int i = 0; i < 292; i++)
            {
                double[] x1 = { lotAreaNormalizovani[i], yearBuiltNormalizovani[i], listNeighbourhoodNormalizovani[i], listBldgType[i] };
                double predictedPrice = network.predict(x1.ToList())[0];
                double realPrice = salePriceNormalizovani[i];
                Console.WriteLine("prediktovana vrednost je " + predictedPrice + "  stvarna je " + salePriceNormalizovani[i]);
                double tacnost = (predictedPrice * 100) / realPrice;
                Console.WriteLine(tacnost);
                if (tacnost >= 80 && tacnost <= 120)
                {
                    tacnoPrediktovanih++;
                }
            }
            Console.WriteLine("Tacnost: " + (tacnoPrediktovanih * 100) / 292 + "%");
            Console.ReadLine();









            List<double> normalizacijaInt(List<int> listaVrednosti)
            {
                List<double> noramlizovanaLista = new List<double>();

                double dataMax = listaVrednosti.Max();
                double dataMin = listaVrednosti.Min();
                //  Console.WriteLine("Maksimalna vrendost je" + dataMax);
                //Console.WriteLine("Minimalna vrendost je" + dataMin);

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
