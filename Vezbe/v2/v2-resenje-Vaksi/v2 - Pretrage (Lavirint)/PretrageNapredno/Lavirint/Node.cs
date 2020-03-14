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
            /*
             * Ideja je da ako su kordinate negativne ili vece od mogucih da je to nevalino stanje.
             */
            if (markI < 0 || markJ < 0 || markI >= Main.lavirint.brojVrsta || markJ >= Main.lavirint.brojKolona)
            {
                return false;
            }

            return true;
        }

        public List<Node> getLinkedNodes()
        {
            // TODO 1: Implementirati metodu tako da odredjuje dozvoljeno kretanje u lavirintu.
            List<Node> nextNodes = new List<Node>();
            /*
             *  Ideja je da imam matricu sa mogucim progresom{ desno, levo, gore, dole}.
             *  Prolazimo kroz svaki moguci potez i proveravamo da li je validan, ako
             *  je potez validan, dodajemo ga u listu mogucih cvorova za odigrati.
             */
            int[,] potezi = new int[,] { { 0, 1 }, { 0, -1 }, { 1, 0 }, { -1, 0 } };
            int brojPoteza = potezi.GetLength(0);

            for(int i = 0; i < brojPoteza; i++)
            {   // iz svakog reda uzimam x,y kordinatu i dodajem joj kordinate u kojima smo trenutno
                // i proverim da li je to validno polje
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
