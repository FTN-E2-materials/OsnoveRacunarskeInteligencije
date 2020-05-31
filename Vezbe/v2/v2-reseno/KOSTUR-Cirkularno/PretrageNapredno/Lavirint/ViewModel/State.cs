using System;
using System.Collections.Generic;
using System.Text;

namespace Lavirint
{
    public class State
    {
        #region Atributi

        public State roditeljskoStanje;
        public Node trenutniCvor;
        public double cena;
        public int level;

        #endregion

        #region Sledeca moguca stanja

        public List<State> getMogucaSledecaStanja()
        {
            List<State> mogucaSledecaStanja = new List<State>();
            foreach (Node cvorNaObradi in this.trenutniCvor.getSledeciValidniCvorovi())
            {
                mogucaSledecaStanja.Add(this.getSledeceStanje(cvorNaObradi));
            }
            return mogucaSledecaStanja;
        }

        private State getSledeceStanje(Node cvorNaObradi)
        {
            State sledeceStanje = new State();
            sledeceStanje.trenutniCvor = cvorNaObradi;
            sledeceStanje.roditeljskoStanje = this;
            sledeceStanje.cena = this.cena + 1;
            sledeceStanje.level = this.level + 1;
            
            if(Main.lavirint.polja[cvorNaObradi.markI, cvorNaObradi.markJ] == 4)
            {
                sledeceStanje.trenutniCvor.kutija = true;
                sledeceStanje.cena = -1000;
            }
            return sledeceStanje;
        }

        #endregion

        #region Provera cirkularnosti

        public bool isCirkularnaPutanja()
        {
            // TODO: Mozda ovo resiti preko Dictionarija kako bi bilo brze

            // TODO 3: proveriti da li trenutno stanje odgovara poziciji koja je vec vidjena u grani pretrazivanja
            State trenutnoStanje = this.roditeljskoStanje;

            while (trenutnoStanje != null)
            {
                if (this.trenutniCvor.Equals(trenutnoStanje.trenutniCvor))
                {
                    return true;
                }
                trenutnoStanje = trenutnoStanje.roditeljskoStanje;
            }
            return false;
        }

        #endregion

        #region Dobavljanje putanje

        public List<State> getPutanja()
        {
            List<State> putanja = new List<State>();
            State pomocnoStanje = this;
            while (pomocnoStanje != null)
            {
                putanja.Insert(0, pomocnoStanje);
                pomocnoStanje = pomocnoStanje.roditeljskoStanje;
            }
            return putanja;
        }

        #endregion

        #region Provera krajnjeg stanja
        public bool isKrajnjeStanje()
        {
            //return this.trenutniCvor.Equals(Main.krajnjiNode) && this.trenutniCvor.kutija;
            return Main.krajnjiNode.Equals(this.trenutniCvor);
        }

        #endregion
    }
}
