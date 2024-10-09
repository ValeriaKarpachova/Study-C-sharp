using System;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

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
            double minX = double.Parse(textBoxMinX.Text);
            double maxX = double.Parse(textBoxMaxX.Text);

            // Выбор функции
            string selectedFunction = comboBox1.SelectedItem.ToString();
            Series series = new Series
            {
                ChartType = SeriesChartType.Line
            };

            // Построение графика в зависимости от выбора
            for (double x = minX; x <= maxX; x += 0.1)
            {
                double y = 0;
                switch (selectedFunction)
                {
                    case "Parabola":
                        y = x * x; // y = x^2
                        break;
                    case "Sin":
                        y = Math.Sin(x);
                        break;
                    case "Tan":
                        y = Math.Tan(x); // y = tan(x)
                        break;
                }
                series.Points.AddXY(x, y);
            }

            // Добавление серии на график
            chart1.Series.Add(series);
        }
    }
}
        
   
