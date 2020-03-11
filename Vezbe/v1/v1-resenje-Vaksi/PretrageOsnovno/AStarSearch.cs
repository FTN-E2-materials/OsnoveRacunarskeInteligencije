using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PretrageOsnovno
{
    class AStarSearch
    {
        public State Search(string startNodeName, string endNodeName)
        {
            // TODO 7: implementirati algoritam A*
            Node startNode = Program.instance.graph[startNodeName];
            Node endNode = Program.instance.graph[endNodeName];

            List<State> zaObradu = new List<State>();
            zaObradu.Add(new State(startNode));

            while (zaObradu.Count > 0)
            {
                State naObradi = getBest(zaObradu);
                //Console.WriteLine(naObradi.Node.Name + ":" + (naObradi.Cost + naObradi.Node.Heuristic));
                zaObradu.Remove(naObradi);
                // proverite da li je na obradi endNode
                if (naObradi.Node.Name == endNode.Name)
                {
                    // ako jeste vrati naObradi
                    return naObradi;
                }
                else
                {
                    List<State> mogucaSledecaStanja = naObradi.children();
                    //zaObradu.AddRange(mogucaSledecaStanja);
                    foreach (State next in mogucaSledecaStanja)
                    {
                        zaObradu.Add(next);
                    }
                }

            }

            return null;
        }
        public State getBest(List<State> stanja)
        {
            State rez = null;
            double min = Double.MaxValue;

            foreach (State s in stanja)
            {
                double h = s.Node.Heuristic + s.Cost;
                if (h < min)
                {
                    min = h;
                    rez = s;
                }
            }
            return rez;
        }
    }
}
