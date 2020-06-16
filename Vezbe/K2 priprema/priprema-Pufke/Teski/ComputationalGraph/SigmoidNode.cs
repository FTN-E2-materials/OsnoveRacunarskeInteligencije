using System;

namespace ComputationalGraph
{//Kaze moze da dodje i tangens hiperbolicni ali to retko daju, i dace formulu
    //po kojoj treba da implementiramo forward i backward
    public class SigmoidNode
    {
        private double x;
        /// <summary>
        /// Sigmoid funkcija 1/1+e^-x
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        private double sigmoid(double x)
        {
            // TODO 5.1: Implementirati forward korak sigmoidnog cvora
            return 1.0 / (1 + Math.Exp(-x));
        }

        public SigmoidNode()
        {
            this.x = 0;
        }

        /// <summary>
        /// Na dobijeno x pozvati sigmoid funkciju
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        public double forward(double x)
        {
            this.x = x;
            return this.sigmoid(this.x);
        }

        /// <summary>
        /// z = 1/1+e^-x
        /// dz/dx = sigm(x)*(1-sigm(x))
        /// =>dx = dz * sigm(x)*(1-sigm(x))
        /// </summary>
        /// <param name="dz"></param>
        /// <returns></returns>
        public double backward(double dz)
        {
            // TODO 5.2: Implementirati backward korak sigmoidog cvora
            return dz * this.sigmoid(this.x) * (1.0 - this.sigmoid(this.x));
        }

        
    }
}
