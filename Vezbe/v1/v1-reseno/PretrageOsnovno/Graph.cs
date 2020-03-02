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

            // formiramo sve cvorove
            foreach (string line in linesNodes)
            {
                // splitujete po : 
                string[] splitted = line.Split(':');
                // dobijete name i heuristiku
                string name = splitted[0];
                string heurStr = splitted[1];
                // heuristiku u dobule
                double heuristic = double.Parse(heurStr);
                // kreirati node = new Node(name, heuristic);
                Node node = new Node(name, heuristic);
                // dodati u graf key: name , value: node
                graph.Add(node.Name, node);
            }

            // linkovi
            foreach (string line in linesLinks)
            {
                // splitujete po : put:NS,BG,90
                string[] splittedLine = line.Split(':');
                // pa jos po ,
                string[] splittedLink = splittedLine[1].Split(',');
                //  start, end, cena -sve je string
                string name = splittedLine[0];
                string start = splittedLink[0];
                string end = splittedLink[1];
                string cenaStr = splittedLink[2];
                double cena = double.Parse(cenaStr);

                Node startNode = graph[start];
                Node endNode = graph[end];
                // Kreirate link
                Link link = new Link(startNode, endNode, name, cena);
                // i dodate ga u start node
                startNode.Links.Add(link);
            }
            
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
