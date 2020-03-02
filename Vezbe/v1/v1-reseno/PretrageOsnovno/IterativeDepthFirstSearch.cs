using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections;

namespace PretrageOsnovno
{
    class IterativeDepthFirstSearch
    {
        private const int MaxLevel = 10000;

        // TODO 6: implementirati algoritam iterativni prvi u dubinu
        public State Search(string startNodeName, string endNodeName)
        {
            Node startNode = Program.instance.graph[startNodeName];
            Node endNode = Program.instance.graph[endNodeName];

            for (int level = 0; level < MaxLevel; level++)
            {
                // copy-paste dfs
                List<State> zaObradu = new List<State>();
                zaObradu.Add(new State(startNode));

                while (zaObradu.Count > 0)
                {
                    State naObradi = zaObradu[0];
                    zaObradu.Remove(naObradi);
                    // proverimo naObradi.Level > level? 
                    if (naObradi.Level > level)
                        continue;
                    // ako jeste treba da ga preskocim
                    // ako nije nastavljamo dalje
                    if (naObradi.Node.Name == endNode.Name)
                    {
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
                }
            }
            return null;
        }
    }
}
