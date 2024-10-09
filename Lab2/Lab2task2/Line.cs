using System;
using System.Drawing;

namespace MyPaint
{
    class Line : IFigure
    {
        public int X1 { get; set; }
        public int Y1 { get; set; }
        public int X2 { get; set; }
        public int Y2 { get; set; }
        public Pen Pen { get; set; }
        public Color FillColor { get; set; } = Color.Transparent;

        public Line(int x1, int y1, int x2, int y2, Pen currentPen)
        {
            X1 = x1;
            Y1 = y1; 
            X2 = x2;
            Y2 = y2;
            Pen = new Pen(currentPen.Color);
        }

        public void Draw(Graphics graphics)
        {
            graphics.DrawLine(Pen, X1, Y1, X2, Y2);
        }

        public bool ContainsPoint(int x, int y)
        {
            const int tolerance = 2; // Допуск в 2 пикселя для проверки близости к линии

            // Вычисляем расстояние от точки (x, y) до линии
            double distance = DistanceFromPointToLine(x, y, X1, Y1, X2, Y2);
            return distance <= tolerance;
        }

        private double DistanceFromPointToLine(int x, int y, int x1, int y1, int x2, int y2)
        {
            double A = x - x1;
            double B = y - y1;
            double C = x2 - x1;
            double D = y2 - y1;

            double dot = A * C + B * D;
            double len_sq = C * C + D * D;
            double param = (len_sq != 0) ? dot / len_sq : -1;

            double xx, yy;

            if (param < 0)
            {
                xx = x1;
                yy = y1;
            }
            else if (param > 1)
            {
                xx = x2;
                yy = y2;
            }
            else
            {
                xx = x1 + param * C;
                yy = y1 + param * D;
            }

            double dx = x - xx;
            double dy = y - yy;
            return Math.Sqrt(dx * dx + dy * dy);
        }
    }
}

