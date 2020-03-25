using System;
using System.Collections.Generic;
using System.Text;

namespace Lavirint
{
    public class State //Klasa State modeluje jedno stanje u lavirintu i sadrzi sve informacije koje su nam potrebne
    {
        public State parent;     // Ko je roditeljski state
        public Node node;        // O kom cvoru je rec
        public double cost;      // Cena
        public int level;        // i nivo
        public bool kutija;



        public State sledeceStanje(Node node)
        {
            State rez = new State();
            rez.node = node;
            rez.parent = this;
            rez.cost = this.cost + 1;
            rez.level = this.level + 1;
            rez.kutija = this.kutija;
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
            State currentState = this.parent;
            while(currentState != null)
            {
                if(this.node.Equals(currentState.node) && this.kutija == currentState.kutija)
                {
                    return true;
                }
                currentState = currentState.parent;
            }
            return false;
        }
    }
}
