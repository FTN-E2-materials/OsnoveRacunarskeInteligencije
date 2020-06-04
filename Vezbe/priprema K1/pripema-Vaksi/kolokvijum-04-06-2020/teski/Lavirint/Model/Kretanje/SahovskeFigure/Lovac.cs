using System;
using System.Collections.Generic;
using System.Text;

namespace Lavirint.Model.Kretanje.SahovskeFigure
{
    /// <summary>
    /// Sahovska figura lovac
    /// </summary>
    public class Lovac : Kretanje
    {
        public override int[,] getKretanjeFigure()
        {
            int [,] kretanje = { { 1, 1 }, { -1, 1 }, { 1, -1 }, { -1, -1 } };
            return kretanje;
        }
    }
}
