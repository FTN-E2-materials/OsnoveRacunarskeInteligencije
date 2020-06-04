using System;
using System.Collections.Generic;
using System.Text;

namespace Lavirint.Model.Kretanje.SahovskeFigure
{
    /// <summary>
    /// Sahovska figura konj ali radi uctivosti
    /// nazvacu je skakac.
    /// </summary>
    public class Skakac : Kretanje
    {
        public override int[,] getKretanjeFigure()
        {
            int [,] kretanje = { { -1, -2 }, { -2, -1 }, { -2, 1 }, { -1, 2 }, { 1, 2 }, { 2, 1 }, { 2, -1 }, { 1, -2 } };
            return kretanje;
        }
    }
}
