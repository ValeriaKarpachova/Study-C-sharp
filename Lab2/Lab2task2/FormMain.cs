using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.Windows.Forms;

namespace MyPaint 
{
    public partial class FormMain : Form
    {
        FigureManager manager = new FigureManager();
        private PenManager penManager;
       
        public FormMain()
        {
            InitializeComponent();
            penManager = new PenManager(); 
            CurrentPenColor();
        }
        private void toolStripButtonLine_Click(object sender, EventArgs e)
        {
            manager.State = State.BeginLine;
        }

        private void toolStripButtonRect_Click(object sender, EventArgs e)
        {
            manager.State = State.BeginRect;
        }

        private void toolStripButtonCircle_Click(object sender, EventArgs e)
        {
            manager.State = State.BeginCircle;
        }

        private void panel_Paint(object sender, PaintEventArgs e)
        {
            manager.Draw(e.Graphics);
        }

        private void openToolStripMenuItem_Click(object sender, EventArgs e)
        {
            openFileDialog.ShowDialog();
            Image img = Image.FromFile(openFileDialog.FileName);
            panel.BackgroundImage = img;
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void CurrentPenColor()
        {
            panelColor.BackColor = penManager.CurrentPen.Color;
        }

        private void panelColor_DoubleClick(object sender, EventArgs e)
        {
            penManager.ChangeColor();
            CurrentPenColor();
        }

       
        private void panel_MouseClick(object sender, MouseEventArgs e)
        {
            switch (manager.State)
            {
                case State.Default:
                    break;
                case State.Fill:
                    var figure = manager.GetFigureAt(e.X, e.Y);
                    if (figure != null && selectedFillColor != Color.Transparent)
                    {
                        figure.FillColor = selectedFillColor;
                        panel.Invalidate();  
                        manager.State = State.Default;
                    }
                    break;
                case State.BeginLine:
                    manager.CurrentFigure = new Line(e.X, e.Y, e.X, e.Y, penManager.CurrentPen);
                    manager.State = State.EndLine;
                    break;
                case State.EndLine:
                    var line = manager.CurrentFigure as Line;
                    line.X2 = e.X;
                    line.Y2 = e.Y;
                    manager.PushFigure();
                    manager.State = State.Default;
                    break;
                case State.BeginRect:
                    manager.CurrentFigure = new Rectangle(e.X, e.Y, e.X, e.Y, penManager.CurrentPen);
                    manager.State = State.EndRect;
                    break;
                case State.EndRect:
                    manager.PushFigure();
                    manager.State = State.Default;
                    break;
                case State.BeginCircle:
                    manager.CurrentFigure = new Circle(e.X, e.Y, 0, penManager.CurrentPen);
                    manager.State = State.EndCircle;
                    break;
                case State.EndCircle:
                    var circle = manager.CurrentFigure as Circle;
                    circle.R = Geometry.Dist(e.X, e.Y, circle.X, circle.Y);
                    manager.PushFigure();
                    manager.State = State.Default;
                    break;
                default:
                    break;
            }
            panel.Invalidate();
        }

        private void panel_MouseMove(object sender, MouseEventArgs e)
        {
            toolStripStatusLabelCoord.Text = $"{e.X} : {e.Y}";
            switch (manager.State)
            {
                case State.EndLine:
                    var line = manager.CurrentFigure as Line;
                    line.X2 = e.X;
                    line.Y2 = e.Y;
                    break;
                case State.EndRect:
                    var rect = manager.CurrentFigure as Rectangle;
                    rect.X2 = e.X;
                    rect.Y2 = e.Y;
                    break;
                case State.EndCircle:
                    var circle = manager.CurrentFigure as Circle;
                    circle.R = Geometry.Dist(e.X, e.Y, circle.X, circle.Y);
                    break;
                default:
                    break;
            }
            panel.Invalidate();
        }

        private void saveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            saveFileDialog.ShowDialog();
            if (saveFileDialog.FileName != "")
            {
                int width = panel.Size.Width;
                int height = panel.Size.Height;

                Bitmap bm = new Bitmap(width, height);
                panel.DrawToBitmap(bm, new System.Drawing.Rectangle(0, 0, width, height));

                bm.Save(saveFileDialog.FileName, ImageFormat.Png);
            }
        }

        private Color selectedFillColor = Color.Transparent;
        private void Fill_Click(object sender, EventArgs e)
        {
            ColorDialog colorDialog = new ColorDialog();
            if (colorDialog.ShowDialog() == DialogResult.OK)
            {
                selectedFillColor = colorDialog.Color;
                manager.State = State.Fill; 
            }
        }
    }
}
