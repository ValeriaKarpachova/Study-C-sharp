using System.Drawing;

namespace MyPaint
{
    class Circle : IFigure
    {
        public int X { get; set; }
        public int Y { get; set; }
        public int R { get; set; }
        public Pen Pen { get; set; }
        public Color FillColor { get; set; } = Color.Transparent;

        public Circle(int x, int y, int r, Pen currentPen)
        {
            X = x;
            Y = y;
            R = r;
            Pen = new Pen(currentPen.Color);
        }

        public void Draw(Graphics graphics)
        {
            graphics.DrawEllipse(Pen, X - R, Y - R, 2 * R, 2 * R);
            if (FillColor != Color.Transparent)
            {
                using (Brush brush = new SolidBrush(FillColor))
                {
                    graphics.FillEllipse(brush, X - R, Y - R, R * 2, R * 2);
                }
            }
            graphics.DrawEllipse(Pen, X - R, Y - R, R * 2, R * 2);
        }

        public bool ContainsPoint(int x, int y)
        {
            // Проверка, находится ли точка внутри окружности
            int dx = x - X;
            int dy = y - Y;
            return dx * dx + dy * dy <= R * R;
        }
    }
}