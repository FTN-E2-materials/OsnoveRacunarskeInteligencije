﻿using System;
using System.Collections.Generic;
using System.Text;

namespace Lavirint
{
    public class Node
    {
        public int markI, markJ;
        public bool kutija;

        public Node(int markI, int markJ)
        {
            this.markI = markI;
            this.markJ = markJ;
            this.kutija = false;
        }

        private bool validCoords(int markI, int markJ)
        {
            // TODO 2: Implementirati logiku za validna/nevalidna stanja iz zabranu prolaska kroz zidove
            if (markI < 0 || markJ < 0 || markI >= Main.lavirint.brojVrsta || markJ >= Main.lavirint.brojKolona)
            {
                return false;
            }

            if (Main.lavirint.polja[markI, markJ] == 1)
            {
                return false;
            }
            return true;
        }

        public List<Node> getLinkedNodes()
        {
            // TODO 1: Implementirati metodu tako da odredjuje dozvoljeno kretanje u lavirintu.
            List<Node> nextNodes = new List<Node>();
            int[,] movements = new int[,] { { 0, 1 }, { 0, -1 }, { 1, 0 }, { -1, 0 } };

            for (int i = 0; i < movements.GetLength(0); ++i)
            {
                int newMarkI = movements[i, 0] + this.markI;
                int newMarkJ = movements[i, 1] + this.markJ;

                if (validCoords(newMarkI, newMarkJ))
                {
                    nextNodes.Add(new Node(newMarkI, newMarkJ));
                }

            }

            return nextNodes;
        }

        public override bool Equals(object obj)
        {
            if (obj == null || GetType() != obj.GetType())
            {
                return false;
            }

            Node node = (Node)obj;
            return this.markI == node.markI && this.markJ == node.markJ;
        }

        public override int GetHashCode()
        {
            return 100 * this.markI + this.markJ;
        }
    }
}
