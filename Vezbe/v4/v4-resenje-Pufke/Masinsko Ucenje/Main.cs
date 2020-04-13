using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Windows.Forms.DataVisualization.Charting;
using System.Threading;
using System.Runtime.Serialization.Formatters.Binary;

namespace Masinsko_Ucenje
{
    public partial class Main : Form
    {

        LinearRegression regression;
        KMeans kmeans;
        string[] lines;


        public Main()
        {
            InitializeComponent();

            lines = File.ReadAllLines(@"./../../data/skincancer.csv");
            lines = lines.Skip(1).ToArray(); // skip header row (State, Lat, Mort, Ocean, Long) preskacemo heder, trebaju nam samo podaci
        }

        private void btnLinearRegression_Click(object sender, EventArgs e)
        {
            regression = new LinearRegression(); //OVO DOLE MORA DA SE ZNA NA KOLOKVIJUMU (Linearna regresija kao najlaksi zadatak) 
            List<double> x = new List<double>(); //X je nezavisna promenljiva, y je zavisna promenlji, od x zavisi y
            List<double> y = new List<double>();

            // TODO 1: Ucitati i isparsirati skup podataka iz lines u x i y
            foreach (string line in lines)
            {
                string[] elements = line.Split(',');// Splitujemo csv fajl po zarezima
                x.Add(double.Parse(elements[1])); //uzimamo latitude atribut kao x vrednost (geografska pozicija jug-sever)
                y.Add(double.Parse(elements[2])); //uzimamo mortaliti atribut kao y vrednost (stopa smrtnosti)
            }

            // TODO 4.1: Izvršiti linearnu regresiju na primeru predviđanja stope smrtnosti od raka kože na osnovu geografske širine američkih država.


            // TODO 4.2: Izvršiti predikciju stope mortaliteta za vrednost geografske širine od tačno 37

            // draw regresiion line on a chart
            drawRegressionResults(x, y);
        }

        private void btnKmeans_Click(object sender, EventArgs e)
        {
            this.kmeans = new KMeans();
            clusteringHistory = new List<List<Cluster>>();

            List<Point> kmeansElements = new List<Point>();
            int k = Convert.ToInt32(tbK.Value);
            double toleracijaNaGresku = Convert.ToDouble(tbErr.Text);

            // TODO 8: Klasterizovati američke države na osnovu geografkse dužine i širine

            // draw clustering results on a chart
            drawClusteringResults();
        }

        private void btnDBScan_Click(object sender, EventArgs e)
        {

        }

        #region GUI_Functions
        private void drawRegressionResults(List<double> X, List<double> Y)
        {
            RegressionChart.Visible = true;
            ClusteringChart.Visible = false;
            RegressionChart.Series.Clear();

            Series diagramLimitsSeries = new Series("DiagramLimits");
            diagramLimitsSeries.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Point;
            diagramLimitsSeries.Points.AddXY(X.Min(), Y.Min());
            diagramLimitsSeries.Points.AddXY(X.Max(), Y.Max());
            diagramLimitsSeries.Points[0].IsEmpty = true;
            diagramLimitsSeries.Points[1].IsEmpty = true;
            diagramLimitsSeries.IsVisibleInLegend = false;
            RegressionChart.Series.Add(diagramLimitsSeries);
            RegressionChart.Update();


            // Create a point series.
            Series pointSeries = new Series("Tacke");
            pointSeries.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Point;
            RegressionChart.Series.Add(pointSeries);

            for (int i = 0; i < X.Count; i++)
            {
                pointSeries.Points.AddXY(X[i], Y[i]);
                //Thread.Sleep(5);
                RegressionChart.Update();
            }

            // Create a line series.
            string lineLabel = "";
            if (regression.n > 0)
                lineLabel = "Regresiona prava: y=" + Math.Round(regression.k,2) + "*x + " + Math.Round(regression.n,2);
            else
                lineLabel = "Regresiona prava: y=" + Math.Round(regression.k,2) + "*x - " + Math.Round(Math.Abs(regression.n),2);
            Series lineSeries = new Series(lineLabel);
            lineSeries.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Line;
            lineSeries.Points.AddXY(X.Max(), regression.n + X.Max() * regression.k);
            lineSeries.Points.AddXY(X.Min(), regression.n + X.Min() * regression.k);
            RegressionChart.Series.Add(lineSeries);
            RegressionChart.Update();
        }

        public static List<List<Cluster>> clusteringHistory = new List<List<Cluster>>();

        private void drawClusteringResults()
        {
            ClusteringChart.Visible = true;
            RegressionChart.Visible = false;

            foreach (List<Cluster> clusterList in clusteringHistory)
            {
                ClusteringChart.Series.Clear();

                // visualize each cluster
                for (int i = 0; i < kmeans.brojGrupa; i++ )
                {
                    // Create a point series.
                    Series pointSeries = new Series("Klaster " + i+1);
                    pointSeries.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Point;

                    // add cluster center and mark it differently
                    pointSeries.Points.AddXY(clusterList[i].centar.x, clusterList[i].centar.y);
                    pointSeries.Points[0].MarkerSize = pointSeries.Points[0].MarkerSize * 2;
                    pointSeries.Points[0].MarkerBorderColor = Color.Black;
                    pointSeries.Points[0].MarkerBorderWidth = 3;

                    for (int j = 0; j < clusterList[i].elementi.Count; j++)
                    {
                        pointSeries.Points.AddXY(clusterList[i].elementi[j].x, clusterList[i].elementi[j].y);
                    }
                    

                    ClusteringChart.Series.Add(pointSeries);
                }

                ClusteringChart.Update();
                Thread.Sleep(500);
                
            }
        }

        public static T DeepClone<T>(T obj)
        {
            using (var ms = new MemoryStream())
            {
                var formatter = new BinaryFormatter();
                formatter.Serialize(ms, obj);
                ms.Position = 0;

                return (T)formatter.Deserialize(ms);
            }
        }
        #endregion GUI_Functions

    }
}
