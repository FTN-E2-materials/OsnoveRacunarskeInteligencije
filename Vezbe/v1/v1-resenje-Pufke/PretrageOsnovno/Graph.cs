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
         
           //Prvo formiramo sve cvorove 
           foreach(string line in linesNodes)
            {
                //splitujemo po : da bi dobili ime cvora i heuristiku
                string[] splitted = line.Split(':');
                string name = splitted[0];
                string heuristicString = splitted[1];

                //heuristilku konvertujemo u double 
                double heuristic = double.Parse(heuristicString);

                //Sada imamo sve potrebne informacije i mozemo da kreiramo Node 
                Node node = new Node(name, heuristic);

                //Sada kad smo kreirali cvor mozemo da ga dodamo u graf
                //To uradimo jednostavnim dodavanjem cvora u Dictionary
                graph.Add(node.Name, node);
            }

           //Sada povezujemo te cvorove koje smo dodali 
           foreach(string line in linesLinks)
            {
                //splitujemo po : put:NS,BG,90
                string[] splitted1 = line.Split(':');
                //zatim moramo da splitujemo i po , 
                string[] splitted2 = splitted1[1].Split(',');
                //Sada dobijemo start,end,cena i sve je tip podataka string
                string name = splitted2[0];
                string start = splitted2[0];
                string end = splitted2[1];
                string cenaStr = splitted2[2];
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
