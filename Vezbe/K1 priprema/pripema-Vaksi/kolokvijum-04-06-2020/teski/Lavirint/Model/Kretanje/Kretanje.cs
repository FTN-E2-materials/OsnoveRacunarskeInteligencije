using System;
using System.Collections.Generic;
using System.Text;

namespace Lavirint.Model.Kretanje
{
    /// <summary>
    /// Klasa predstavnica trenutnog moguceg kretanja.
    /// Sve njene naslednice moraju overajdovati svoje kretanje.
    /// </summary>
    public abstract class Kretanje
    {
        /// <summary>
        /// Odredjivanje moguceg kretanja odredjenog igraca.
        /// </summary>
        /// <returns></returns>
        public abstract int[,] getKretanjeFigure();
    }
}
