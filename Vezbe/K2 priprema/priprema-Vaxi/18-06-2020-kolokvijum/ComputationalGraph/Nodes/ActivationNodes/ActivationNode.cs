using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ComputationalGraph.Nodes.ActivationNodes
{
    /// <summary>
    /// Aktivacioni node ili ti aktivaciona funkcija.
    /// </summary>
    public interface ActivationNode
    {

        /// <summary>
        /// Forward pass aktivacione funkcije
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        double forward(double x);

        /// <summary>
        /// Backward pass aktivacione funkcije.
        /// 
        /// z = aktivacionaFunkcija
        /// dz/dx = izvodAktivacioneFunkcije
        /// => dx = dz * izvodAktivacioneFunkcije
        /// </summary>
        /// <param name="dz"></param>
        /// <returns></returns>
        double backward(double dz);

    }
}
