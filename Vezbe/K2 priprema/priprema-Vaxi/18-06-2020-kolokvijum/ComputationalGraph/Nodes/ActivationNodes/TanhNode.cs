using ComputationalGraph.Nodes.ActivationNodes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ComputationalGraph.Nodes
{
    /// <summary>
    /// Aktivaciona funkcija hiperbolicnog tangesa.
    /// 
    /// slika: http://prntscr.com/t11dlw
    /// </summary>
    public class TanhNode : ActivationNode
    {
        /// <summary>
        /// Inputi (ulazi) u aktivacionu funkciju
        /// </summary>
        private double x;

        public TanhNode()
        {
            this.x = 0;
        }

        /// <summary>
        /// Na dobijeno x pozvati tanh aktivacionu funkciju
        /// </summary>
        /// <param name="x"> input </param>
        /// <returns></returns>
        public double forward(double x)
        {
            this.x = x;
            return this.tanh(this.x);
        }


        /// <summary>
        /// z = (exp(x) - exp(-x)) / (exp(x) + exp(-x))
        /// dz/dx = pow(sech,2) [slika: http://prntscr.com/t11q0b ]
        /// => dx = dz * pow(sech,2)
        /// </summary>
        /// <param name="dz"></param>
        /// <returns></returns>
        public double backward(double dz)
        {
            return dz * Math.Pow( this.sech(this.x),2 );
        }


        #region Tanges hiperbolicni

        /// <summary>
        /// Funkcija tangesa hiperbolicnog
        /// 
        /// slika: http://prntscr.com/t11dlw
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        private double tanh(double x)
        {
            return (Math.Exp(x) - Math.Exp(-x)) / (Math.Exp(x) + Math.Exp(-x));
        }

        #endregion

        #region Hiperbolicni sekant

        /// <summary>
        /// Sekant hiperbolicni
        /// 
        /// slika: http://prntscr.com/t11lqv
        /// referenca: https://en.wikipedia.org/wiki/Hyperbolic_functions
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        private double sech(double x)
        {
            return 2.0 / ( Math.Exp(x) / Math.Exp(-x));
        }

        #endregion
    }
}
