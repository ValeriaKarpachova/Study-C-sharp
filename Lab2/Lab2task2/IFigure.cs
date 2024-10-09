using System.Drawing;

namespace MyPaint
{
    interface IFigure
    {
        Color FillColor { get; set; }

        bool ContainsPoint(int x, int y);
        void Draw(Graphics graphics);
    }
}