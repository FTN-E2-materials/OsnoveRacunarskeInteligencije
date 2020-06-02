using System;
using System.Collections.Generic;
using System.Text;

namespace Lavirint.Model
{
    /// <summary>
    /// Klasa koja reprezentuje model senzora koji se moze
    /// podesavati u zavisnosti potreba igre
    /// </summary>
    public class Senzor
    {
        #region Atributi + property
        private static int reonSenzora = 2;
        private int kordinataX;
        private int kordinataY;

        private bool _aktivan;

        public bool Aktivan
        {
            get { return _aktivan; }
            set { _aktivan = value; }
        }

        #endregion

        #region Konstruktori
        public Senzor(int x, int y)
        {
            this.kordinataX = x;
            this.kordinataY = y;
            Aktivan = false;
        }

        public Senzor(int x, int y, bool statusAktivacije)
        {
            this.kordinataX = x;
            this.kordinataY = y;
            Aktivan = statusAktivacije;
        }
        #endregion

        /// <summary>
        /// Dobavljam sva polja koja su u reonu senzora.
        /// </summary>
        /// <returns> lista polja pod senzorom </returns>
        public List<int> getPoljaPodSenzorom()
        {
            List<int> poljaPodSenzorom = new List<int>();

            for (int i = -reonSenzora; i <= reonSenzora; ++i)
            {
                int novoX = kordinataX + i;

                //Provera da li je x kordinata u reonu table citave igre
                if (novoX < 0 || novoX >= Main.brojVrsta)
                    continue;

                for (int j = -reonSenzora; j <= reonSenzora; ++j)
                {
                    int novoY = kordinataY + j;

                    //Provera da li je y kordinata u reonu table citave igre
                    if (novoY < 0 || novoY >= Main.brojKolona)
                        continue;

                    //Kada dodje do ovog dela, kordinata se sigurno nalazi u reonu senzora
                    poljaPodSenzorom.Add(novoX * 10 + novoY);
                }

            }

            return poljaPodSenzorom;
        }
    }
}
