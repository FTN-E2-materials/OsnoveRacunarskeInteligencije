﻿using System;
using System.Collections.Generic;
using System.Text;
using System.Collections;
using System.Windows.Forms;
using System.Security.Policy;

namespace Lavirint
{
    class DepthFirstSearch
    {
        public State search(State pocetnoStanje)
        {
            List<State> stanjaNaObradi = new List<State>();
            Hashtable predjeniPut = new Hashtable();

            stanjaNaObradi.Add(pocetnoStanje);
            while (stanjaNaObradi.Count > 0)
            {
                State naObradi = stanjaNaObradi[0];

                if (!predjeniPut.Contains(naObradi.trenutniCvor.GetHashCode()))
                {
                    predjeniPut.Add(naObradi.trenutniCvor.GetHashCode(), null);
                    Main.allSearchStates.Add(naObradi);

                    if (naObradi.isKrajnjeStanje())
                    {
                        return naObradi;
                    }

                    List<State> mogucaSledecaStanja = naObradi.getMogucaSledecaStanja();
                    stanjaNaObradi.InsertRange(0, mogucaSledecaStanja);
                }
                stanjaNaObradi.Remove(naObradi);
            }
            return null;
        }
    }
}
