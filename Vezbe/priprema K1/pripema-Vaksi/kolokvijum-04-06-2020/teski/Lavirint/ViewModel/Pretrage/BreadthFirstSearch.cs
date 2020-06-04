using System;
using System.Collections.Generic;
using System.Text;
using System.Collections;
using System.Windows.Forms;

namespace Lavirint
{
    /// <summary>
    /// Klasa predstavnica slepe pretrage koja nam daje OPTIMALNO resenje.
    /// 
    /// FIFO princip
    /// </summary>
    class BreadthFirstSearch
    {   
        public State search(State pocetnoStanje)
        {
            List<State> stanjaNaObradi = new List<State>();                     
            Hashtable vecPosecenaStanja = new Hashtable();   
            
            stanjaNaObradi.Add(pocetnoStanje);

            while (stanjaNaObradi.Count > 0)
            {
                State naObradi = stanjaNaObradi[0];

                if (!vecPosecenaStanja.ContainsKey(naObradi.GetHashCode()))           
                {
                    Main.allSearchStates.Add(naObradi);                         
                    if (naObradi.isKrajnjeStanje())                            
                    {
                        return naObradi;
                    }
                    vecPosecenaStanja.Add(naObradi.GetHashCode(), null);              
                    List<State> mogucaSledecaStanja = naObradi.mogucaSledecaStanja();
                    stanjaNaObradi.AddRange(mogucaSledecaStanja);               
                }                                                               
                stanjaNaObradi.Remove(naObradi);                                
            }
            return null;
        }
    }
}
