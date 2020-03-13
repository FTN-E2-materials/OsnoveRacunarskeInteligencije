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

            for(int level = 0; level < MaxLevel; level++)
            {
                //Isti kod kao kod DFS-a
                List<State> zaObradu = new List<State>();
                zaObradu.Add(new State(startNode));

                while(zaObradu.Count > 0)
                {
                    State naObradi = zaObradu[0];
                    zaObradu.Remove(naObradi);
                    //Proveravamo da li je na obradi lvl > lvl
                    if (naObradi.Level > level)
                        continue;
                    //Ako jeste preskacemo ga ako nije nastavljamo dalje 
                    if(naObradi.Node.Name == endNode.Name)
                    {
                        return naObradi;
                    }
                    else
                    {
                        List<State> mogucaSledecaStanja = naObradi.children();
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
