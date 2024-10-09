using System.Collections.Generic;
using System.Drawing;

namespace MyPaint
{
    enum State
    {
        Default,
        Fill,
        BeginLine,
        EndLine,
        BeginRect,
        EndRect,
        BeginCircle,
        EndCircle
    }

    class FigureManager
    {
        public List<IFigure> Figures { get; set; }
        public State State { get; set; }
        public IFigure CurrentFigure { get; set; }
        public Color FillColor { get; internal set; }

        public FigureManager()
        {
            Figures = new List<IFigure>();
        }

        public void Draw(Graphics graphics)
        {
            foreach (var fig in Figures)
            {
                fig.Draw(graphics);
            }
            if (CurrentFigure != null)
            {
                CurrentFigure.Draw(graphics);
            }
        }

        public IFigure GetFigureAt(int x, int y)
        {
            for (int i = Figures.Count - 1; i >= 0; i--)
            {
                var figure = Figures[i];
                if (figure.ContainsPoint(x, y))
                {
                    return figure;
                }
            }
            return null;
        }


        public void PushFigure()
        {
            Figures.Add(CurrentFigure);
            CurrentFigure = null;
        }
    }
}
