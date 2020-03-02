using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections;

namespace PretrageOsnovno
{
    class DepthFirstSearch
    {
        public State Search(string startNodeName, string endNodeName)
        {
            // TODO 3: implementirati algoritam prvi u dubinu 
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
                    //zaObradu.InsertRange(0, mogucaSledecaStanja);
                    foreach (State next in mogucaSledecaStanja)
                    {
                        zaObradu.Insert(0, next);
                    }
                }
               
                // ako nije, pokupi listu moguci stanja .childred()
                // dodaj u zaObradu sve te elemente na pocetak

            }
            
             return null;
        }
    }
}
