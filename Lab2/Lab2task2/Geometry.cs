using System;

namespace MyPaint
{
    class Geometry
    {
        public static int Dist(int x1, int y1, int x2, int y2)
        {
            var dx = x2 - x1;
            var dy = y2 - y1;
            return (int)Math.Sqrt(dx * dx + dy * dy);
        }
    }
}