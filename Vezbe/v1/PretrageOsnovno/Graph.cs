using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PretrageOsnovno
{
    class Graph
    {
        public Dictionary<string, Node> graph = null;

        public Graph(string[] linesNodes, string[] linesLinks)
        {
            graph = new Dictionary<string, Node>();
            formGraph(linesNodes, linesLinks);
        }

        // TODO 1 : implementirati metodu formiranja grafa
        private void formGraph(string[] linesNodes, string[] linesLinks)
        {
            
        }

        #region ispis
        public void printGraph()
        {
            StringBuilder sb = new StringBuilder();
            foreach (KeyValuePair<string, Node> kvp in graph)
            {
                foreach (Link link in kvp.Value.Links)
                {
                    Console.WriteLine(link.Name + ":" + link.StartNode + "," + link.EndNode + "," + link.Cost);
                }
            }
        }
        #endregion
    }
}
