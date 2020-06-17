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

        public double z { get; set; }

        public double w { get; set; }
        public Point(double x, double y, double z, double w)
        {
            this.x = x;
            this.y = y;
            this.z = z;
            this.w = w;
        }
    }
}
