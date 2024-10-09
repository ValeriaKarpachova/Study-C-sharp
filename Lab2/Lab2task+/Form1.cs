using System;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;

namespace Lab2task_
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            chart1.Series.Clear();

            double minX = double.Parse(textBoxMinX.Text);
            double maxX = double.Parse(textBoxMaxX.Text);

          
            string selectedFunction = comboBox1.SelectedItem.ToString();
            Series series = new Series
            {
                ChartType = SeriesChartType.Line
            };

            for (double x = minX; x <= maxX; x += 0.1)
            {
                double y = 0;
                switch (selectedFunction)
                {
                    case "Parabola":
                        y = x * x;
                        break;
                    case "Sin":
                        y = Math.Sin(x);
                        break;
                    case "Tan":
                        y = Math.Tan(x); 
                        break;
                    case "Hyperbole":
                        if (x != 0) 
                        {
                            y = 1 / x; 
                        }
                        break;
                }
                series.Points.AddXY(x, y);
            }
            chart1.Series.Add(series);
        }
    }
}
        
   
