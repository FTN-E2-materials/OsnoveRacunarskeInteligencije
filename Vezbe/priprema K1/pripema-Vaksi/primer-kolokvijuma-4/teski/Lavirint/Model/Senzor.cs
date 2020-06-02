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
        #region Atributi

        private int _reonSenzora;
        private int _kordinataX;
        private int _kordinataY;
        private bool _aktivan;

        #endregion

        #region Properties

        public int ReonSenzora
        {
            get { return _reonSenzora; }
            set { _reonSenzora = value; }
        }

        public int KordinataX
        {
            get { return _kordinataX; }
            set { _kordinataX = value; }
        }

        public int KordinataY
        {
            get { return _kordinataY; }
            set { _kordinataY = value; }
        }

        public bool Aktivan
        {
            get { return _aktivan; }
            set { _aktivan = value; }
        }

        #endregion

        #region Konstruktori
        public Senzor(int x, int y)
        {
            ReonSenzora = 2;
            KordinataX = x;
            KordinataY = y;
            Aktivan = false;
        }
        public Senzor(int x, int y, int reonSenzora)
        {
            ReonSenzora = reonSenzora;
            KordinataX = x;
            KordinataY = y;
            Aktivan = false;
        }

        public Senzor(int x, int y, bool statusAktivacije)
        {
            ReonSenzora = 2;
            KordinataX = x;
            KordinataY = y;
            Aktivan = statusAktivacije;
        }

        public Senzor(int x, int y, bool statusAktivacije, int reonSenzora)
        {
            ReonSenzora = reonSenzora;
            KordinataX = x;
            KordinataY = y;
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

            for (int i = -ReonSenzora; i <= ReonSenzora; ++i)
            {
                int novoX = KordinataX + i;

                //Provera da li je x kordinata u reonu table citave igre
                if (novoX < 0 || novoX >= Main.brojVrsta)
                    continue;

                for (int j = -ReonSenzora; j <= ReonSenzora; ++j)
                {
                    int novoY = KordinataY + j;

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
