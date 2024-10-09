using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Lab1Task_
{
    public partial class Form1 : Form
    {
        private List<PictureBox> cars;
        private Random random;
        private const int finishLine = 900; // Координата финиша
        private Timer raceTimer;

        public Form1()
        {
            InitializeComponent();
            random = new Random();
            InitializeRaceCars();
            InitializeTimer();
        }
       
        private void InitializeRaceCars()
        {
            cars = new List<PictureBox>
            {
                new PictureBox{Size = new Size(60, 30),BackColor = Color.Red,Location = new Point(10, 170),Image = Image.FromFile("C:/Users/user/Desktop/1693037277_grizly-club-p-kartinki-mashinka-sverkhu-bez-fona-7000000000000.png")},
                new PictureBox{Size = new Size(60, 30),BackColor = Color.Blue,Location = new Point(10, 235),Image = Image.FromFile("C:/Users/user/Desktop/1697749345_flomaster-top-p-avto-risunok-sverkhu-instagram-420000000000.png")},
                new PictureBox{Size = new Size(60, 30),BackColor = Color.Orange,Location = new Point(10, 300),Image = Image.FromFile("C:/Users/user/Desktop/1693037277_grizly-club-p-kartinki-mashinka-sverkhu-bez-fona-80000000.png")}
            };

            foreach (var car in cars)
            {
                Controls.Add(car);
            }
        }
        
        private void InitializeTimer()
        {
            raceTimer = new Timer();
            raceTimer.Interval = 100; 
            raceTimer.Tick += timer1_Tick;
        }

        private void Start_Click(object sender, EventArgs e)
        {
            raceTimer.Start(); // Запуск гонки
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            foreach (var car in cars)
            {
                int moveDistance = random.Next(5, 15); 
                car.Left += moveDistance;

                if (car.Left >= finishLine)
                {
                    raceTimer.Stop(); 
                    MessageBox.Show($"Переможець: {car.BackColor.Name} машина!");
                    return;
                }
            }
        }
    }
}
