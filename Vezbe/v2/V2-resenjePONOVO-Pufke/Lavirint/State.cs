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
            //takodje moramo da uvedemo tu novu kutiju i ovde
            rez.node.kutija = this.node.kutija;
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
            return Main.krajnjiNode.Equals(this.node) && this.node.kutija;
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

        //Krecemo se kroz rodjitelje od noda , kroz sve perente i proveravamo da li smo vec bili u tom stejtu
        //Ovo moze dugo da traje postoje alternativna resenja
        public bool cirkularnaPutanja()
        {
            // TODO 3: proveriti da li trenutno stanje odgovara poziciji koja je vec vidjena u grani pretrazivanja
            State currentState = this.parent;
            while (currentState != null)                        //Posto imamo ubacenu sada i plavu kutiju moramo da pored metode Equals utvrdimo i da li se te kutije poklapaju
            {                                                   //this.node.kutija == currentState.node.kutija
                if (this.node.Equals(currentState.node) && this.node.kutija == currentState.node.kutija)
                {
                    return true;
                }
                currentState = currentState.parent;
            }
            return false;
        }
    }
}
