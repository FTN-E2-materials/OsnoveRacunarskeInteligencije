using System;
using System.Collections.Generic;
using System.Text;

namespace Lavirint
{
    public class Node
    {
        public int markI, markJ;
        public bool kutija;//dodali smo kutiju u node da bi znali jel sadrzi kutiju ili ne
        public Node(int markI, int markJ)
        {
            this.markI = markI;
            this.markJ = markJ;
            this.kutija = false; //Inicijalno postavljamo kutiju na false kad pravimo cvor
        }

        private bool validCoords(int markI, int markJ)
        {
            // TODO 2: Implementirati logiku za validna/nevalidna stanja iz zabranu prolaska kroz zidove
            if (markI < 0 || markJ < 0 || markI>= Main.lavirint.brojVrsta || markJ >= Main.lavirint.brojKolona)
            {
                return false;
            }
            
            //slucaj kada je naisao na zid, zid ima vrednost 1, zelena kockica tj pocetna kockica 2, crvena 3 itd...
            if(Main.lavirint.polja[markI,markJ] == 1)
            {
                return false;
            }
            return true;
        }

        //Metoda vratii dozvoljeno kretanje za prisledjeni node
        public List<Node> getLinkedNodes()
        {
            // TODO 1: Implementirati metodu tako da odredjuje dozvoljeno kretanje u lavirintu.
            List<Node> nextNodes = new List<Node>();
            int[,] movement = new int[,] { { 0, 1 }, { 0, -1 }, { 1, 0 }, { -1, 0 } };//{markI,markJ}
            //desno,levo,gore,dole

            //plava kutija ima vrednost 4, ovo smo dodali da bi markirali da smo pokupili kutiju
            #region kutija
            if(Main.lavirint.polja[markI,markJ] == 4)
            {
                kutija = true;
            }
            #endregion

            for (int i = 0; i < movement.GetLength(0); i++)
            {
                int newMarkI = movement[i, 0] + this.markI;
                int newMarkJ = movement[i, 1] + this.markJ;

                if (validCoords(newMarkI, newMarkJ))
                {
                    nextNodes.Add(new Node(newMarkI,newMarkJ));
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
            //sada kada imamo plavu kutiju, morai smo da ovo proslirimo sa node.kutija
            return this.markI == node.markI && this.markJ == node.markJ && node.kutija;
        }

        public override int GetHashCode()
        {
            return 100 * this.markI + this.markJ;
        }
    }
}
