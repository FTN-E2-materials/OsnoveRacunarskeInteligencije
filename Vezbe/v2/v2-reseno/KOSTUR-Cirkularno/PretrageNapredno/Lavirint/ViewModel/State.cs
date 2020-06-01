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
            
            if(Main.lavirint.polja[cvorNaObradi.kordinataI, cvorNaObradi.kordinataJ] == 4 && !sledeceStanje.trenutniCvor.hesPokupljenihPlavih.Contains(100 * cvorNaObradi.kordinataI + cvorNaObradi.kordinataJ))
            {
                sledeceStanje.trenutniCvor.pokupljenaPlava = true;
                sledeceStanje.trenutniCvor.hesPokupljenihPlavih.Add(100 * cvorNaObradi.kordinataI + cvorNaObradi.kordinataJ);
                sledeceStanje.cena = -1000;                         // ne utice previse ali garantuje
                // da kad je u blizini plave kutije, onda je i pokupi
            }

            return sledeceStanje;
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
            return Main.krajnjiNode.Equals(this.trenutniCvor) && this.trenutniCvor.pokupljenaPlava;
        }

        #endregion
    }
}
