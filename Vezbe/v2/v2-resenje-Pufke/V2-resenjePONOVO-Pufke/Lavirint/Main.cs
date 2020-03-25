using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Xml.Serialization;
using System.Collections;
using System.Diagnostics;
using System.Threading;

namespace Lavirint
{
    public partial class Main : Form
    {
        public static int BROJ_VRSTA = 10;
        public static int BROJ_KOLONA = 10;

        public static Lavirint lavirint;
        public static List<State> allSearchStates;

        public Main()
        {
            InitializeComponent();
            inicijalizacijaPretrage();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            lavirint.sacuvajLavirint();
        }

        private void btnLoad_Click(object sender, EventArgs e)
        {
            this.SuspendLayout();
            lavirint.ucitajLavirint();
            this.ResumeLayout(false);
            displayPanel1.Refresh();
        }

        public static State pocetnoStanje = null;
        public static Node krajnjiNode = null;

        // atributi za interaktivnu igricu
        public static Node manualRobotPozicija = null;
        public static List<State> minimaxAgentiPocetno = new List<State>();


        private void inicijalizacijaPretrage(bool multipleAgents=false) {
            displayPanel1.resetLavirintPoruke();
            displayPanel1.resetLavirintPoseceno();
            displayPanel1.resetAgents();
            allSearchStates = new List<State>();
            minimaxAgentiPocetno = new List<State>();
            for (int i = 0; i < lavirint.brojVrsta; i++)
            {
                for (int j = 0; j < lavirint.brojKolona; j++)
                {
                    int tt = lavirint.polja[i,j];
                    if (tt == 2) { // POCETNO STANJE
                        pocetnoStanje = new State();
                        pocetnoStanje.node = new Node(i, j);
                        manualRobotPozicija = new Node(i, j);
                        displayPanel1.iconI = i;
                        displayPanel1.iconJ = j;
                    }else if (tt == 3)
                    { // KRAJNJE STANJE
                        krajnjiNode = new Node(i, j);
                    }
                    else if (tt == 4 && multipleAgents)
                    {
                        // pocetne pozicije za min-max agente
                        State minMaxAgent = new State();
                        minMaxAgent.node = new Node(i, j);
                        displayPanel1.agentPositions.Add(new List<int>() { i,j });
                        minimaxAgentiPocetno.Add(minMaxAgent);
                    }
                }
            }
            lblStatus.Text = "-------------------------------";
            lblStatus.BackColor = Color.Transparent;
        }

        List<State> resenje = new List<State>();
        private void btnResenje_Click(object sender, EventArgs e)
        {
            displayPanel1.resetLavirintPoseceno();
            displayPanel1.resetLavirintPoruke();
            // nacrtati resenje
            int i = 0;
            foreach (State r in resenje)
            {
                displayPanel1.lavirintPoruke[r.node.markI][r.node.markJ] += " " + i;
                i++;
            }
            displayPanel1.Refresh();
        }

        private void btnPrviUDubinu_Click(object sender, EventArgs e)
        {
            inicijalizacijaPretrage();
            DepthFirstSearch dfs = new DepthFirstSearch();
            State sp = pocetnoStanje;
            State solution = dfs.search(sp);
            if (solution != null)
            {
                resenje = solution.path();
            }
            displayPanel1.Refresh();
        }

        private void btnPrviUSirinu_Click(object sender, EventArgs e)
        {
            inicijalizacijaPretrage();
            BreadthFirstSearch bfs = new BreadthFirstSearch();
            State sp = pocetnoStanje;
            State solution = bfs.search(sp);
            if (solution != null)
            {
                resenje = solution.path();
            }
            displayPanel1.Refresh();
        }

        private void btnIterativniPrviUDubinu_Click(object sender, EventArgs e)
        {
            inicijalizacijaPretrage();
            IterativeDeepFirstSeach id = new IterativeDeepFirstSeach();
            State s = pocetnoStanje;
            //s.depth = 0;
            for (int i = 0; i < 40; i++)
            {
                State solution = id.search(s, i);
                if (solution != null)
                {
                    resenje = solution.path();
                    displayPanel1.Refresh();
                    return;
                }
            }
            displayPanel1.Refresh();
          

        }

        private void btnAStar_Click(object sender, EventArgs e)
        {
            inicijalizacijaPretrage();
            AStarSearch astar = new AStarSearch();
            State sp = pocetnoStanje;
            State solution = astar.search(sp);
            if (solution != null)
            {
                resenje = solution.path();
            }
            displayPanel1.Refresh();
        }

        private void ADepth_Click(object sender, EventArgs e)
        {
            //TODO 7: Dopuniti metodu pozivima odgovarajucih metoda iz klase napisane u prethodnom koraku
            inicijalizacijaPretrage();
            ADepthSearch aDepth = new ADepthSearch();
            
            displayPanel1.Refresh();
        }

        private void btnMiniMax_Click(object sender, EventArgs e)
        {
            inicijalizacijaPretrage();
            MinMaxSearch minmax = new MinMaxSearch();
            State sp = pocetnoStanje;
            State solution = minmax.search(sp);
            if (solution != null)
            {
                resenje = solution.path();
            }
            displayPanel1.Refresh();
        }

        private void showSearchPath_Click(object sender, EventArgs e)
        {
            displayPanel1.resetLavirintPoseceno();
            foreach (State state in allSearchStates)
            {
                displayPanel1.resetLavirintPoruke();
                displayPanel1.poseceno(state);
                int i = 0;
                foreach (State r in state.path())
                {
                    displayPanel1.lavirintPoruke[r.node.markI][r.node.markJ] += " " + i;
                    i++;
                }
                displayPanel1.moveIcon(state.node.markI - displayPanel1.iconI, state.node.markJ - displayPanel1.iconJ);
                displayPanel1.Refresh();
                Thread.Sleep(50);
            }
        }

        #region Interaktivni robotic

        Node ciljManualRobotica = null;
        object agentUpdateLock = new object();
        bool gameFinished = false;
        int agentSteps = 1;
        string interaktivnaPretraga = null;

        private void btnMiniMaxGame_Click(object sender, EventArgs e)
        {
            interaktivnaPretraga = "minimax";
            inicijalizacijaPretrage(true);
            displayPanel1.Focus();

            ciljManualRobotica = Main.krajnjiNode;
            gameFinished = false;
            agentSteps = 1;
            // start a game
            gameSpeedTimer.Start();
            
        }

        private void btnAStarGame_Click(object sender, EventArgs e)
        {
            interaktivnaPretraga = "A*";
            inicijalizacijaPretrage(true);
            displayPanel1.Focus();

            ciljManualRobotica = Main.krajnjiNode;
            gameFinished = false;
            agentSteps = 1;
            // start a game
            gameSpeedTimer.Start();

        }

        private void gameSpeedTimer_Tick(object sender, EventArgs e)
        {
            if (Monitor.TryEnter(agentUpdateLock))
            {
                try
                {
                    // agenti traze naseg robotica, tako da je ciljno stanje u stvari pozicija robotica
                    Node trenutnaPozicijaRobotica = manualRobotPozicija;
                    Main.krajnjiNode = trenutnaPozicijaRobotica;
                    displayPanel1.Refresh();

                    // pokreni pretragu za svakog inteligentnog agenta i nadji najbolji sledeci potez za svakog od njih
                    for (int agentIndex = 0; agentIndex < minimaxAgentiPocetno.Count; agentIndex++)
                    {
                        State sp = minimaxAgentiPocetno[agentIndex];
                        State solution = null;
                        if (interaktivnaPretraga == "A*")
                        {
                            AStarSearch pretraga = new AStarSearch();
                            solution = pretraga.search(sp);
                        }
                        else if (interaktivnaPretraga == "minimax")
                        {
                            MinMaxSearch pretraga = new MinMaxSearch();
                            solution = pretraga.search(sp);
                        }

                        if (solution != null)
                        {
                            try
                            {
                                List<State> putanja = solution.path();
                                State sledeciPotez = putanja[1];
                                sledeciPotez.parent = null;

                                displayPanel1.moveAgentIcon(agentIndex, sledeciPotez.node.markI - displayPanel1.agentPositions[agentIndex][0], sledeciPotez.node.markJ - displayPanel1.agentPositions[agentIndex][1]);
                                // sledeca pretraga ce krenuti od novog stanja
                                minimaxAgentiPocetno[agentIndex] = sledeciPotez;
                                if (Main.manualRobotPozicija.markI == sledeciPotez.node.markI && Main.manualRobotPozicija.markJ == sledeciPotez.node.markJ)
                                {
                                    gameFinished = true;
                                    lblStatus.Text = "Protivnik je pobedio. :( Vise srece drugi put.";
                                    lblStatus.BackColor = Color.Red;

                                }
                            }
                            catch (Exception exc) { gameFinished = true; }
                        }
                    }

                    if (Main.manualRobotPozicija.markI == ciljManualRobotica.markI && Main.manualRobotPozicija.markJ == ciljManualRobotica.markJ && gameSpeedTimer.Enabled)
                    {
                        gameSpeedTimer.Enabled = false;
                        displayPanel1.Refresh();
                        gameFinished = true;
                        lblStatus.Text = "Vi ste pobedili! :) Cestitamo!";
                        lblStatus.BackColor = Color.Green;
                    }
                    Main.krajnjiNode = ciljManualRobotica;
                    displayPanel1.Refresh();
                }
                finally
                {
                    
                    if(gameFinished)
                        gameSpeedTimer.Enabled = false;
                    agentSteps += 1;
                    Monitor.Exit(agentUpdateLock);
                }

            }
        }

        #endregion Interaktivni robotic

        
    }
}
