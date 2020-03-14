using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Lavirint
{
    public class ReinforcementLearning
    {
        public int episode = 0;

        DisplayPanel displayPanel;

        public ReinforcementLearning(DisplayPanel displayPanel)
        {
            this.displayPanel = displayPanel;
        }

        private void paint(Node observation)
        {
            displayPanel.iconI = observation.markI;
            displayPanel.iconJ = observation.markJ;

            displayPanel.Refresh();
            Thread.Sleep(100);
        }
        
        public void runEpisode(QTable qTable)
        {
            Node observation = Main.pocetniNode;

            while(true)
            {
                Action action = qTable.chooseAction(observation);
                Node nextObservation = observation.getPossibleActions()[action];

                paint(nextObservation);

                double reward = nextObservation.getReward();
                qTable.learn(observation, action, reward, nextObservation);

                if (nextObservation.isTerminalNode())
                {
                    break;
                }

                observation = nextObservation;
            }
        }
    }
}
