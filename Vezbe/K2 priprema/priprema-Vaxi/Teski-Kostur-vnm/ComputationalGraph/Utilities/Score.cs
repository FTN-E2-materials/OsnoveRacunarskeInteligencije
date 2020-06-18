using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ComputationalGraph.Utilities
{
    /// <summary>
    /// Klasa koje odredjuje koliki je skor trenutno istrenirane mreze,
    /// i ispisuje ga.
    /// 
    /// U njoj vrsim pozivanje predikcije na osnovu koje i ispisujem skor.
    /// </summary>
    public class Score
    {

        public Score()
        {

        }

        #region Odredjivanje skora

        public void giveMeScore(DAO.FileDAO fileDAO, NeuralNetwork network)
        {
            Prediction prediction = new Prediction();
            int brojPogodaka = prediction.giveMeNumberOfHits(fileDAO, network);

            writeScore(fileDAO, brojPogodaka);
        }

        #endregion

        #region Ispis skora 

        /// <summary>
        /// Metoda koja sluzi samo za ispis skora
        /// </summary>
        /// <param name="fileDAO"></param>
        /// <param name="brojPogodaka"></param>
        private static void writeScore(DAO.FileDAO fileDAO, int brojPogodaka)
        {
            // Console.Write("Pogodjeno {0} od {1} ", pogodak, fileDAO.YTest.Count);
            Console.Write("Score {0} %", brojPogodaka * 100 / fileDAO.YTest.Count);
            Console.ReadLine();
        }

        #endregion 

    }
}
