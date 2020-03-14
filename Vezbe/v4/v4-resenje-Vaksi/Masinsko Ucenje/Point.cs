using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Masinsko_Ucenje
{
    [Serializable]
    public class Point
    {
        public double x { get; set; }
        public double y { get; set; }

        public Point(double x, double y)
        {
            this.x = x;
            this.y = y;
        }
    }
}
