using System.Drawing;
using System.Windows.Forms;

namespace MyPaint
{
    public class PenManager
    {
        private Pen currentPen;

        public PenManager()
        {
            currentPen = new Pen(Color.Black, 3);
        }

        public Pen CurrentPen => currentPen;

        public void ChangeColor()
        {
            using (ColorDialog colorDialog = new ColorDialog())
            {
                if (colorDialog.ShowDialog() == DialogResult.OK)
                {
                    currentPen.Color = colorDialog.Color; 
                }
            }
        }

    }
}
