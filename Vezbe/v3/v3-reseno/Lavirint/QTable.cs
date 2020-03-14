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
                Dictionary<Action, double> qValues = observation.getPossibleActions()
                    .ToDictionary(kp => kp.Key, kp => 0.0);
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
                // act1: value
                // act2: value
                // act3: value
                double maxQValue = stateAction.Values.Max();
                // filter samo koje imaju value == max
                // act1: value == max
                // act2: value == max
                // act3: value == max
                Dictionary<Action, double> bestActions = stateAction.Where(kp => kp.Value == maxQValue)
                    .ToDictionary(kp => kp.Key, kp => kp.Value);
                // act1: value == max
                // act3: value == max
                int randomIndex = rnd.Next(0, bestActions.Count);
                // na slucajan nacin odabrati akciju ako ih ima vise sa value == max
                // [act1, act3] => list[randomIndex]
                return bestActions.Keys.ToList()[randomIndex];
            }
            else
            {
                Dictionary<Action, Node> possibleActions = observation.getPossibleActions();
                // TODO 4 izabrati akciju na slucajan nacin
                int randomIndex = rnd.Next(0, possibleActions.Count);
                return possibleActions.Keys.ToList()[randomIndex];
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

            double qPredict = this.qTable[currentNode.GetHashCode()][action]; //Q(s,a)
            double qTarget = 0;

            if (nextNode.isTerminalNode())
            {
                qTarget = reward; // Next state is terminal
            } else
            {
                // r + gama * max(Q(s', a'))
                qTarget = reward + this.gamma * this.qTable[nextNode.GetHashCode()].Values.Max();
            }

            // TODO 5 implementirati formulu za update vrednosti
            // Q(s,a) += alpha [qTarget - qPredict]
            this.qTable[currentNode.GetHashCode()][action] += this.learningRate * (qTarget - qPredict);
        }
    }
}
