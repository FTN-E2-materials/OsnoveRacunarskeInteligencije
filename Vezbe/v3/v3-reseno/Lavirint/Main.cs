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
        public static int BROJ_VRSTA = 5;
        public static int BROJ_KOLONA = 5;
        public static int episode = 0;

        public static double LEARNING_RATE = 0.01;
        public static double GAMMA = 0.9;
        public static double EPSILON = 0.9;

         public static Lavirint lavirint;

        public Main()
        {
            InitializeComponent();
            inicijalizacijaPretrage();
            qTable = new QTable(LEARNING_RATE, GAMMA, EPSILON);
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            lavirint.sacuvajLavirint();
        }

        private void btnLoad_Click(object sender, EventArgs e)
        {
            this.SuspendLayout();
            lavirint.ucitajLavirint();
            this.inicijalizacijaPretrage();
            qTable = new QTable(LEARNING_RATE, GAMMA, EPSILON);
            this.ResumeLayout(false);
            displayPanel1.Refresh();
        }

        public static ReinforcementLearning rl;
        public static QTable qTable;
        public static Node pocetniNode = null;
        public static Node krajnjiNode = null;

        private void inicijalizacijaPretrage() {
            displayPanel1.resetLavirintPoruke();
            displayPanel1.resetLavirintPoseceno();
            displayPanel1.resetAgents();
            rl = new ReinforcementLearning(displayPanel1);
            

            for (int i = 0; i < lavirint.brojVrsta; i++)
            {
                for (int j = 0; j < lavirint.brojKolona; j++)
                {
                    int tt = lavirint.polja[i,j];
                    if (tt == 2) { // POCETNO STANJE
                        pocetniNode = new Node(i, j);
                        displayPanel1.iconI = i;
                        displayPanel1.iconJ = j;
                    }else if (tt == 3)
                    { // KRAJNJE STANJE
                        krajnjiNode = new Node(i, j);
                    }
                }
            }
            lblStatus.Text = "Episode: " + episode;
            lblStatus.BackColor = Color.Transparent;
        }
        
        

        private void btnPrviUSirinu_Click(object sender, EventArgs e)
        {
            inicijalizacijaPretrage();
            rl.runEpisode(qTable);
            ++episode;
        }
        
    }
}
