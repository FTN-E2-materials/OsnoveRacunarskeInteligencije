using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lavirint
{
    public enum Action
    {
        UP = 0,
        DOWN = 1,
        LEFT = 2,
        RIGHT = 3
    }

    public class QTable
    {
        private double learningRate;
        private double gamma;
        private double epsilon;

        private Dictionary<int, Dictionary<Action, double>> qTable;

        private Random rnd = new Random(42);

        public QTable(double learningRate, double gamma, double epsilon)
        {
            this.learningRate = learningRate;
            this.gamma = gamma;
            this.epsilon = epsilon;

            this.qTable = new Dictionary<int, Dictionary<Action, double>>();
        }
        /// <summary>
        /// Provera da li stanje postoji u Q tabeli. Ako stanje ne postoji, dodati ga u tabelu. 
        /// Prilikom dodavanja vrednosti za akcije staviti na 0.0
        /// </summary>
        /// <param name="observation"></param>
        private void checkStateExists(Node observation)
        {
            if (!this.qTable.ContainsKey(observation.GetHashCode()))
            {
                Dictionary<Action, double> qValues = observation.getPossibleActions().ToDictionary(kp => kp.Key, kp => 0.0);
                this.qTable.Add(observation.GetHashCode(), qValues);
            }
        }

        /// <summary>
        /// Izbor akcije na osnovu trenutnog stanja (obzervacije). Na slucajan nacin biramo da li eksploatisemo Q tabelu,
        /// tj ono sto smo naucili ili istrazujemo prostor ucenja igranjem slucajne akcije. 
        /// </summary>
        /// <param name="observation"></param>
        /// <returns></returns>
        public Action chooseAction(Node observation)
        {
            this.checkStateExists(observation);
           
            if (rnd.NextDouble() < this.epsilon)
            {
                Dictionary<Action, double> stateAction = this.qTable[observation.GetHashCode()];
                // TODO 3 izabrati najbolju akciju za trenutno stanje
                return 0;
            }
            else
            {
                Dictionary<Action, Node> possibleActions = observation.getPossibleActions();
                // TODO 4 izabrati akciju na slucajan nacin
                return 0;
            }
        }
        /// <summary>
        /// Izmena vrednosti Q table.
        /// Parametri(s,a,r,s')
        /// Formula za izmenu:
        /// Q(s,a) = Q(s,a) + alpha [r + gama * max(Q(s', a') - Q(s, a)]
        /// s = s'
        /// </summary>
        /// <param name="currentNode"></param>
        /// <param name="action"></param>
        /// <param name="reward"></param>
        /// <param name="nextNode"></param>
        public void learn(Node currentNode, Action action, double reward, Node nextNode)
        {
            this.checkStateExists(nextNode);

            double qPredict = this.qTable[currentNode.GetHashCode()][action];
            double qTarget = 0;

            if (nextNode.isTerminalNode())
            {
                qTarget = reward; // Next state is terminal
            } else
            {
                qTarget = reward + this.gamma * this.qTable[nextNode.GetHashCode()].Values.Max();
            }

            // TODO 5 implementirati formulu za update vrednosti
            
        }
    }
}
