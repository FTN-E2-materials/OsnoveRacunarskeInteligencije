using System;
using System.Collections.Generic;
using System.Text;

namespace Lavirint.Model.Kretanje.SahovskeFigure
{
    /// <summary>
    /// Sahovska figura kraljica
    /// </summary>
    public class Kraljica : Kretanje
    {
        public override int[,] getKretanjeFigure()
        {
            int [,] kretanje = { { 1, -1 }, { -1, -1 }, { 1, 0 }, { 1, 1 }, { -1, 1 }, { -1, 0 }, { 0, 1 }, { 0, -1 } };
            return kretanje;
        }
    }
}
