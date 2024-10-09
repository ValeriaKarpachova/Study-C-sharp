using Lab1task2.Models;
using System;
using System.IO;
using System.Windows.Forms;

namespace Lab1task2
{
    public partial class Form1 : Form
    {
        private ContactsRepo contactsRepo = new ContactsRepo();

        public Form1()
        {
            InitializeComponent();
        }

        private void openFile_Click(object sender, EventArgs e)
        {
            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
            contactsRepo.LoadFromFile(openFileDialog.FileName);
            
            
            dataGridView.Rows.Clear();

                foreach (var contact in contactsRepo.GetAll())
                {
                  dataGridView.Rows.Add(contact.FullName, contact.Surname, contact.Number, contact.Address);
                }
            }
        }

        private void saveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (saveFileDialog.ShowDialog() == DialogResult.OK)
            {
                
                contactsRepo = new ContactsRepo();

                for (int i = 0; i < dataGridView.Rows.Count - 1; i++)
                {
                    var row = dataGridView.Rows[i];
                    if (CellValid(row.Cells["FirstName"]) &&
                        CellValid(row.Cells["Surname"]) &&
                        CellValid(row.Cells["Number"]) &&
                        CellValid(row.Cells["Address"])) 
                    {
                        var contact = new Contact
                        {
                            FullName = row.Cells["FirstName"].Value.ToString(),
                            Surname = row.Cells["Surname"].Value.ToString(),
                            Number = row.Cells["Number"].Value.ToString(),
                            Address = row.Cells["Address"].Value.ToString(),
                        };

                        contactsRepo.Add(contact);
                    }
                }
                contactsRepo.SaveToFile(saveFileDialog.FileName);
            }
        }
        private bool CellValid(DataGridViewCell cell)
        {
            return cell != null && cell.Value != null;
        }

        private void eToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
