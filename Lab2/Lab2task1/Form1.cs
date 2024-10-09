using System.Drawing;
using System.Windows.Forms;

namespace Lab2task1
{
    public partial class Form1 : Form
    {
        Pen pen;
        public Form1()
        {
            InitializeComponent();
            pen = new Pen(Color.BlueViolet, 3);
        }

        private void Form1_Paint(object sender, PaintEventArgs e)
        {
            e.Graphics.DrawLine(pen, 90, 30, 90, 50);
            e.Graphics.DrawLine(pen, 80, 40, 100, 40);
            e.Graphics.DrawLine(pen, 130, 30, 130, 60);
            e.Graphics.DrawPolygon(pen, new Point[] {
                new Point(80, 60),
                new Point(80, 75),
                new Point(180, 75),
                new Point(180, 60)});
            e.Graphics.DrawLine(pen, 80, 90, 180, 90);
            e.Graphics.DrawLine(pen, 130, 90, 130, 120);
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            pen.Dispose();
        }
    }
}
