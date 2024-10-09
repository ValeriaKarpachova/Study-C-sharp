using System;
using System.Drawing;


namespace MyPaint
{
    internal class Rectangle : IFigure
    {
        public int X1 { get; set; }
        public int Y1 { get; set; }
        public int X2 { get; set; }
        public int Y2 { get; set; }
        public Pen Pen { get; set;}
        public Color FillColor { get; set; } = Color.Transparent;

        public Rectangle(int x1, int y1, int x2, int y2, Pen currentPen)
        {
            X1 = x1;
            Y1 = y1;
            X2 = x2;
            Y2 = y2;
            Pen = new Pen(currentPen.Color);
        }

        public void Draw(Graphics graphics)
        {
            graphics.DrawLine(Pen, X1, Y1, X2, Y1);
            graphics.DrawLine(Pen, X2, Y1, X2, Y2);
            graphics.DrawLine(Pen, X2, Y2, X1, Y2);
            graphics.DrawLine(Pen, X1, Y2, X1, Y1);

            if (FillColor != Color.Transparent)
            {
                using (Brush brush = new SolidBrush(FillColor))
                {
                    graphics.FillRectangle(brush, Math.Min(X1, X2), Math.Min(Y1, Y2), Math.Abs(X2 - X1), Math.Abs(Y2 - Y1));
                }
            }

            graphics.DrawRectangle(Pen, Math.Min(X1, X2), Math.Min(Y1, Y2), Math.Abs(X2 - X1), Math.Abs(Y2 - Y1));

        }

        public bool ContainsPoint(int x, int y)
        {
            // Проверка, находится ли точка внутри прямоугольника
            return x >= Math.Min(X1, X2) && x <= Math.Max(X1, X2) && y >= Math.Min(Y1, Y2) && y <= Math.Max(Y1, Y2);
        }
    }
}
