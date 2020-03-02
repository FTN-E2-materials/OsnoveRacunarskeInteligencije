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
            return null;
        }
    }
}
