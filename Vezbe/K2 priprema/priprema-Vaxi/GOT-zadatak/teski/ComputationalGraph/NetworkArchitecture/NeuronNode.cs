using System;
using System.Collections.Generic;
using System.Linq;

namespace ComputationalGraph
{
    public class NeuronNode
    {
        public int n_inputs { get; set; }
        public List<MultiplyNode> multiplyNodes;
        public SumNode sumNode;
        public SigmoidNode activation_node;
        public List<double> previous_deltas;
        public List<List<double>> gradients;
        private static GaussianRandom grnd = new GaussianRandom(); //gausova raspodela za random vrednosti

        /// <summary>
        /// https://en.wikipedia.org/wiki/Artificial_neuron
        /// ulaz1*tezina1
        /// ulaz1*tezina2
        /// bias(+1*tezina)
        /// sve se sabere
        /// provuce se kroz aktivacionu funkciju
        /// </summary>
        /// <param name="n_inputs"></param>
        /// <param name="activation"> String naziv aktivacione funkcije </param>
        public NeuronNode(int n_inputs, string activation)
        {
            this.n_inputs = n_inputs;
            this.multiplyNodes = new List<MultiplyNode>(); //for inputs and weights
            this.sumNode = new SumNode();                  //for sum of inputs*weights
            this.previous_deltas = new List<double>();     //vrednost delti u prosloj iteraciji
            this.gradients = new List<List<double>>();     //lokalni gradijenti
            MultiplyNode mulNode;
            
            //init inputs
            //collect inputs and corresponding weights
            for (int i = 0; i < this.n_inputs; i++)
            {
                mulNode = new MultiplyNode();
                double b = grnd.NextGaussian(0.3, 0.5);
                mulNode.x = new List<double>() { 1.0, b };
                this.multiplyNodes.Add(mulNode);
                previous_deltas.Add(0.0);
            }

            //init bias node and weight
            mulNode = new MultiplyNode();
            double m = grnd.NextGaussian(0.0, 0.01);
            mulNode.x = new List<double>() { 1.0, m };
            this.multiplyNodes.Add(mulNode);
            previous_deltas.Add(0.0);

            // TODO: Ovde dodajem ako dodajem neku drugu aktivacionu funkciju
            /*
             * if else(activation.Equals("TangesHiperbolic").....
             */
            if (activation.Equals("sigmoid"))
                this.activation_node = new SigmoidNode();
            else
                throw new NotImplementedException("Activation function is not supported");

        }
        /// <summary>
        /// Pomnoziti sve ulaze sa tezinama.
        /// Sabrati sve rezultate mnozaca. Kao povratnu vrednost
        /// vratiti vrednost aktivacione funkije za vrednost suma 
        /// svih mnozaca
        /// </summary>
        /// <param name="x">x is a vector of inputs</param>
        /// <returns></returns>
        public double forward(List<double> x)
        {
            List<double> ulazi = new List<double>(x);
            
            // Ubacimo prvo bijas 
            double bias = 1.0;
            ulazi.Add(bias); 

            List<double> rezultatSvihMnozaca = new List<double>();
            //TODO 6: Izracunati vrednost na izlazu vestackog neurona
            for (int i = 0; i < ulazi.Count; i++)
            {
                // posto x[1] daje tezinu
                double prethodnaTezina = this.multiplyNodes[i].x[1];

                double[] inputZaMnozac = { ulazi[i], prethodnaTezina};


                double proizvodUlazaITezine = this.multiplyNodes[i].forward(inputZaMnozac.ToList());
                rezultatSvihMnozaca.Add(proizvodUlazaITezine);
            }

            double sumaMnozaca = this.sumNode.forward(rezultatSvihMnozaca);

            // dobijena vrednost se propusti kroz aktivacionu funkciju
            double izlazAktivacioneFunkcije = this.activation_node.forward(sumaMnozaca);
            
            return izlazAktivacioneFunkcije;    // izlaz aktivacione funkcije predstavlja i izlaz neurona
        }

        /// <summary>
        /// Greska se propagira u nazad kroz aktivacionu funkciju,
        /// preko sabiraca, do svakog pojedinacnog mnozaca
        /// </summary>
        /// <param name="dz"></param>
        /// <returns></returns>
        public List<double> backward(List<double> dz)
        {
            //greska svakog ulaza
            List<double> dw = new List<double>();   // w - tezine
            List<double> dx = new List<double>();   // x - inicijalni ulazi u neuron
            double backward_signal = dz.Sum();

            //TODO 7: Izvrsiti propagaciju signala u nazad, prvo kroz aktivacionu funkciju,
            //        onda kroz sabirac pa kroz svaki pojedinacan mnozac
            backward_signal = this.activation_node.backward(backward_signal);

            List<double> inputiSabiraca = this.sumNode.backward(backward_signal);

            // sad je potrebno i za svaki taj input da uradimo backward pass
            for (int i = 0; i < inputiSabiraca.Count; i++)
            {
                dw.Add(this.multiplyNodes[i].backward(inputiSabiraca[i])[1]);   // 1 - ti element predstavlja tezinu
                dx.Add(this.multiplyNodes[i].backward(inputiSabiraca[i])[0]);   // 0 - ti element predstavlja input
            }

            /*
             * Punimo matricu gradijenata za batch
             * 
             * link: https://en.wikipedia.org/wiki/Gradient_descent
             * df/dx1, df/dx2...
             */
            this.gradients.Add(dw);

            return dx;
        }

        public void updateWeights(double learning_rate, double momentum)
        {
            for (int i = 0; i < multiplyNodes.Count; i++)
            {
                
                List<double> grad = this.gradients.Select(gradient => gradient[i]).ToList();
                double meanGradient = grad.Sum() / (double) this.gradients.Count;
                double delta = learning_rate * meanGradient + momentum * this.previous_deltas[i];
                this.previous_deltas[i] = delta;
                this.multiplyNodes[i].x[1] -= delta; //koriguj tezine 
            }
            //reset gradijenata
            this.gradients.Clear();
        }
    }
}
