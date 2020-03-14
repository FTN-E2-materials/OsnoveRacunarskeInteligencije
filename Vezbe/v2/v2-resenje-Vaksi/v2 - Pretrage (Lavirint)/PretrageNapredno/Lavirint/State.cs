using System;
using System.Collections.Generic;
using System.Text;

namespace Lavirint
{
    public class State
    {
        public State parent;
        public Node node;
        public double cost;
        public int level;

        public State sledeceStanje(Node node)
        {
            State rez = new State();
            rez.node = node;
            rez.parent = this;
            rez.cost = this.cost + 1;
            rez.level = this.level + 1;
            return rez;
        }


        public List<State> mogucaSledecaStanja()
        {
            List<State> rez = new List<State>();
            foreach (Node nextNode in this.node.getLinkedNodes())
            {
                rez.Add(this.sledeceStanje(nextNode));
            }
            return rez;
        }

        public bool isKrajnjeStanje()
        {
            return Main.krajnjiNode.Equals(this.node);
        }

        public List<State> path()
        {
            List<State> putanja = new List<State>();
            State tt = this;
            while (tt != null)
            {
                putanja.Insert(0, tt);
                tt = tt.parent;
            }
            return putanja;
        }

        public bool cirkularnaPutanja()
        {
            // TODO 3: proveriti da li trenutno stanje odgovara poziciji koja je vec vidjena u grani pretrazivanja
            State trenutnoStanje = this.parent;
            while(trenutnoStanje != null)
            {   // iteriramo kroz sva stanja dok ne dodjemo do korena, tj do stanja
                // kome je roditelj null(koren)
                if (this.node.Equals(trenutnoStanje.node))
                {   // znaci da imamo imamo cirkularnu putanju
                    return true;
                }   // a ako nemamo prelazimo na sledece stanje u hijerarhiji
                trenutnoStanje = trenutnoStanje.parent;
            }

            return false;
        }
    }
}
