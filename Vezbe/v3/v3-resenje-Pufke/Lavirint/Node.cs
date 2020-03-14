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
            if (markI < 0 || markI >= Main.lavirint.brojVrsta)
            {
                return false;
            }

            if (markJ < 0 || markJ >= Main.lavirint.brojKolona)
            {
                return false;
            }

            if (Main.lavirint.polja[markI, markJ] == 1)
            {
                return false;
            }

            return true;
        }

        public Dictionary<Action, Node> getPossibleActions()
        {
            // TODO 1: Implementirati metodu koja odredjuje sve moguce akcije.
            Dictionary<Action, Node> retval = new Dictionary<Action, Node>();

            
            return retval;
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

        public double getReward()
        {
            // TODO 2: implementirati metodu za nagradu
            // ako je krajnje stanje, nagrada je 10, ako nije -1
            double retval = -1;

            
            return retval;
        }

        public bool isTerminalNode()
        {
            return this.Equals(Main.krajnjiNode);
        }
    }
}
