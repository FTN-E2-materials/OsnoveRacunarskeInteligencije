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
            prediction.givePredicton(fileDAO, network);

        }

        #endregion

    }
}
