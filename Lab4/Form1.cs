using Bogus;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace Lab4task123
{

    public partial class Form1 : Form
    {

        private string connectionStr = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=""C:\Git Repositories\Study-C-sharp локальный\Lab4\Lab4task1\Cities.mdf"";Integrated Security=True";

        public Form1()
        {
            InitializeComponent();
        }

        private void button_Click_1(object sender, EventArgs e)
        {

            using (SqlConnection con = new SqlConnection(connectionStr))
            {
                con.Open();
                string sqlStr = "SELECT CityName, Population, Region, Sights FROM Cities WHERE 1=1";
                var parameters = new List<SqlParameter>();

                if (!string.IsNullOrWhiteSpace(maskedTextBox.Text))
                {
                    sqlStr += " AND Population <= @Population";
                    parameters.Add(new SqlParameter("@Population", int.Parse(maskedTextBox.Text)));
                }

                if (comboBox1.SelectedItem != null)
                {
                    string selectedFunction = comboBox1.SelectedItem.ToString();
                    sqlStr += " AND Region = @Region";
                    parameters.Add(new SqlParameter("@Region", selectedFunction));
                }

                if (!string.IsNullOrWhiteSpace(maskedTextBox1.Text))
                {
                    string selectedWord = maskedTextBox1.Text;
                    sqlStr += " AND Sights LIKE @Sights";
                    parameters.Add(new SqlParameter("@Sights", "%" + selectedWord + "%"));
                }

                SqlCommand cmd = new SqlCommand(sqlStr, con);
                cmd.Parameters.AddRange(parameters.ToArray());

                SqlDataAdapter dataAdapter = new SqlDataAdapter(cmd);
                System.Data.DataSet queryResult = new System.Data.DataSet();
                dataAdapter.Fill(queryResult);
                dataGridView.DataSource = queryResult.Tables[0];
            }
        }

        private void GenerateData(int count)
        {
            var faker = new Faker("en");
            var cities = new Faker<City>()
                .RuleFor(c => c.CityName, f => faker.Address.City())
                .RuleFor(c => c.Population, f => faker.Random.Number(50000, 5000000))
                .RuleFor(c => c.Region, f => UkrainianRegions.Regions[faker.Random.Number(0, UkrainianRegions.Regions.Length - 1)]) 
                .RuleFor(c => c.Sights, f => faker.Lorem.Sentence(3));

            using (SqlConnection con = new SqlConnection(connectionStr))
            {
                con.Open();
                foreach (var city in cities.Generate(count))
                {
                    string sql = "INSERT INTO Cities (CityName, Population, Region, Sights) VALUES (@CityName, @Population, @Region, @Sights)";
                    using (SqlCommand cmd = new SqlCommand(sql, con))
                    {
                        cmd.Parameters.AddWithValue("@CityName", city.CityName);
                        cmd.Parameters.AddWithValue("@Population", city.Population);
                        cmd.Parameters.AddWithValue("@Region", city.Region);
                        cmd.Parameters.AddWithValue("@Sights", city.Sights);
                        cmd.ExecuteNonQuery();
                    }
                }
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            int count = 100; 
            GenerateData(count);
            button_Click_1(null, null);
        }

        private void maskedTextBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsLetter(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        private void maskedTextBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
            }
        }

    }
}

