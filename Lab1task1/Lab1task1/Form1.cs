using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Lab1task1
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void textBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            TextBox textBox = (TextBox)sender;
            if (char.IsControl(e.KeyChar))
                return;
            if (e.KeyChar.Equals('-') && textBox.Text.Length == 0)
                return;
            if (e.KeyChar.Equals('.') && !textBox.Text.Contains('.'))
                return;
            if (char.IsDigit(e.KeyChar))
                return;
            e.Handled = true;
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            try
            {
                double x = double.Parse(textBox1.Text);
                double y = double.Parse(textBox2.Text);
                double z = double.Parse(textBox3.Text);

                double result = 0;
                for (int i = 1; i <= x; i++)
                {
                    for (int j = 2; j <= y; j++)
                    {
                        result += (z + x) / (i + y * z);
                    }
                }
                label5.Text = String.Format("{0:f3}", result);
            }
           
            catch (Exception)
            {
                MessageBox.Show("Перевірте введені дані", "Помилка");
            }
        }
    }
}
