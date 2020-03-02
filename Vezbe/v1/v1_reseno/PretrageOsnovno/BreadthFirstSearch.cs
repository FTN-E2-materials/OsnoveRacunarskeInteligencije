using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PretrageOsnovno
{
    class BreadthFirstSearch
    {
        public State Search(string startNodeName, string endNodeName)
        {
            // TODO 4: implementirati algoritam prvi u dubinu širinu
            Node startNode = Program.instance.graph[startNodeName];
            Node endNode = Program.instance.graph[endNodeName];

            List<State> zaObradu = new List<State>();
            zaObradu.Add(new State(startNode));

            while (zaObradu.Count > 0)
            {
                State naObradi = zaObradu[0];
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
    }
}
