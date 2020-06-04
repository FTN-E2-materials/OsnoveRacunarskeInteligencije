using System;
using System.Collections.Generic;
using System.Text;
using System.Collections;
using System.Windows.Forms;

namespace Lavirint
{
    /// <summary>
    /// Klasa predstavnica slepe pretrage koja NE DAJE OPTIMALNO resenje.
    /// 
    /// Koristimo je kada znamo da je resenje daleko od pocetnog stnja.
    /// 
    /// LIFO princip.
    /// </summary>
    class DepthFirstSearch
    {
        public State search(State pocetnoStanje)
        {
            List<State> stanjaNaObradi = new List<State>();
            stanjaNaObradi.Add(pocetnoStanje);

            Hashtable vecPosecenaStanja = new Hashtable();
            
            while (stanjaNaObradi.Count > 0)
            {
                State naObradi = stanjaNaObradi[0];

                if(!vecPosecenaStanja.ContainsKey(naObradi.GetHashCode()))
                {
                    Main.allSearchStates.Add(naObradi);             
                    if (naObradi.isKrajnjeStanje())                 
                    {
                        return naObradi;
                    }
                    vecPosecenaStanja.Add(naObradi.GetHashCode(),null);   
                    
                    List<State> mogucaSledecaStanja = naObradi.mogucaSledecaStanja();
                    stanjaNaObradi.InsertRange(0, mogucaSledecaStanja);
                }
                
                stanjaNaObradi.Remove(naObradi);
            }
            return null;
        }
    }
}
