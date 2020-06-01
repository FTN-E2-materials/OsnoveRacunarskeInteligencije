using System;
using System.Collections.Generic;
using System.Text;

namespace Lavirint
{
    public class Node
    {
        #region Atributi
        public int kordinataI, kordinataJ;
        public bool pokupljenaPlava;
        public List<int> hesPokupljenihPlavih;
        #endregion

        #region Konstruktori
        public Node(int markI, int markJ, List<int> hesPlavih)
        {
            this.kordinataI = markI;
            this.kordinataJ = markJ;
            this.pokupljenaPlava = false;
            this.hesPokupljenihPlavih = hesPlavih;
        }
        #endregion

        #region Dobavljanje sledecih validnih cvorova

        public List<Node> getSledeciValidniCvorovi()
        {
            // TODO 1: Implementirati metodu tako da odredjuje dozvoljeno kretanje u lavirintu.
            List<Node> sledeciValidniCvorovi = new List<Node>();
            int[,] movements = new int[,] { { 0, 1 }, { 0, -1 }, { 1, 0 }, { -1, 0 } };

            for (int i = 0; i < movements.GetLength(0); ++i)
            {
                int newMarkI = movements[i, 0] + this.kordinataI;
                int newMarkJ = movements[i, 1] + this.kordinataJ;

                if (validneKordinate(newMarkI, newMarkJ))
                {
                    sledeciValidniCvorovi.Add(new Node(newMarkI, newMarkJ, hesPokupljenihPlavih));
                }

            }

            return sledeciValidniCvorovi;
        }

        #endregion

        #region Validne kordinate
        private bool validneKordinate(int markI, int markJ)
        {
            // TODO 2: Implementirati logiku za validna/nevalidna stanja iz zabranu prolaska kroz zidove
            if (markI < 0 || markJ < 0 || markI >= Main.lavirint.brojVrsta || markJ >= Main.lavirint.brojKolona)
            {
                return false;
            }

            if (Main.lavirint.polja[markI, markJ] == 1)
            {
                return false;
            }
            return true;
        }
        #endregion

        #region Overide-ovanje Equals-a i HashCoda

        public override int GetHashCode()
        {
            int hash = 100 * this.kordinataI + this.kordinataJ;

            int nivoFrekvencije = 1000;

            foreach (int hesPokupljeneKutije in hesPokupljenihPlavih)
            {
                hash += nivoFrekvencije + hesPokupljeneKutije;
            }

            return hash;
        }

        public override bool Equals(object obj)
        {
            if (obj == null || GetType() != obj.GetType())
            {
                return false;
            }

            Node node = (Node)obj;
            return this.kordinataI == node.kordinataI && this.kordinataJ == node.kordinataJ;
        }

        #endregion
    }
}
