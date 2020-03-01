using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PretrageOsnovno
{
    class State
    {
        public State Parent { get; set; }
        public Node Node { get; set; }
        public double Cost { get; set; }
        public int Level { get; set; }

        public State(Node node)
        {
            this.Node = node;
            this.Level = 1;
            this.Cost = 0;
        }

        public State(Node node, int level, double cost)
        {
            this.Node = node;
            this.Level = level;
            this.Cost = cost;
        }

        // konstruise moguce sledece stanje na osnovu cvora u grafu
        public State NextState(Node node, double cost)
        {
            State nextState = new State(node, this.Level + 1, cost);
            nextState.Node = node;
            nextState.Parent = this;
            return nextState;
        }

        // metoda odredjuje sledeca stanja u koja je moguce preci iz trenutnog stanja
        public List<State> children()
        {
            List<State> children = new List<State>();

            // TODO 2: pronaci moguca sledeca stanja na osnovu veza koje vode iz cvora u kom je trenutno stanje

            return children;
        }

        public override int GetHashCode()
        {
            return Node.GetHashCode() * 100;
        }

        public List<State> path()
        {
            List<State> path = new List<State>();
            State tt = this;
            while (tt != null)
            {
                path.Insert(0, tt);
                tt = tt.Parent;
            }
            return path;
        }
    }
}
