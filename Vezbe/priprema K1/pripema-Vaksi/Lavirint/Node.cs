using System;
using System.Collections.Generic;
using System.Text;

namespace Lavirint
{
    public class Node
    {
        public int markI, markJ;

        public Node(int markI, int markJ)
        {
            this.markI = markI;
            this.markJ = markJ;
        }

        private bool validCoords(int markI, int markJ)
        {
            // TODO 2: Implementirati logiku za validna/nevalidna stanja iz zabranu prolaska kroz zidove
            if (markI < 0 || markJ < 0 || markI >= Main.lavirint.brojVrsta || markJ >= Main.lavirint.brojKolona)
            {
                return false;
            }

            if (Main.lavirint.polja[markI, markJ] == 1)
            {   // siva kutija je na tom polju
                return false;
            }

            return true;
        }

        public List<Node> getLinkedNodes()
        {
            // TODO 1: Implementirati metodu tako da odredjuje dozvoljeno kretanje u lavirintu.
            List<Node> nextNodes = new List<Node>();
            int[,] potezi = new int[,] { { 0, 1 },{ 0, -1 },{ 1, 0 },{ 1, 0 },{ -1, 0 },{ 1, 1 },{ 1, -1 },{ -1, 1 },{ -1, -1 } };
            int brojPoteza = potezi.GetLength(0);
            
            for (int i = 0; i < brojPoteza; i++)
            {
                int nextMarkI = potezi[i, 0] + this.markI;
                int nextMarkJ = potezi[i, 1] + this.markJ;

                if (validCoords(nextMarkI, nextMarkJ))
                {
                    nextNodes.Add(new Node(nextMarkI, nextMarkJ));
                }
            }
            
            return nextNodes;
        }

        public override bool Equals(object obj)
        {
            if (obj == null || GetType() != obj.GetType())
            {
                return false;
            }

            Node node = (Node)obj;
            return this.markI == node.markI && this.markJ == node.markJ;
        }

        public override int GetHashCode()
        {
            return 100 * this.markI + this.markJ;
        }
    }
}
