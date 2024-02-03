using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using CarService.Class;
using MetroFramework.Animation;
using MetroFramework.Components;

namespace CarService.Forms
{
    public partial class Form3 : MetroFramework.Forms.MetroForm
    {
        private int currentPage = 1;
        private int recordsPerPage = 10;
        
        private int pageNumber = 1;

        public Form3()
        {
            InitializeComponent();
        }
        private void UpdateDataGridView()
        {
            int offset = (currentPage - 1) * recordsPerPage;
            ClientsClass.GetTableClient(offset, recordsPerPage);
            dataGridView1.DataSource = DBConnection.dtClients;
            UpdatePageInfo();
        }
        private void UpdatePageInfo()
        {
            int totalRecords = ClientsClass.GetTotalRecordsCount();
            int startRecord = (currentPage - 1) * recordsPerPage + 1;
            int endRecord = Math.Min(startRecord + recordsPerPage - 1, totalRecords);

            label2.Text = $"{startRecord} - {endRecord} из {totalRecords}";
        }
        private void Form3_Load(object sender, EventArgs e)
        {
            PreviousBtn.Enabled = false;
            NextBtn.Enabled = false;

            ClientsClass.GetFullTable();
            dataGridView1.DataSource = DBConnection.dtClients;
        }

        private void button6_Click(object sender, EventArgs e)
        {

        }
        
        private void PreviousBtn_Click(object sender, EventArgs e)
        {
            if (pageNumber != 0)
            {
                pageNumber--;
                currentPage--;
                UpdateDataGridView();
            }
            else
            {
                pageNumber = 1;
                PreviousBtn.Enabled = false;
            }
        }

        private void NextBtn_Click(object sender, EventArgs e)
        {
            if (dataGridView1.Rows.Count != 0)
            {
                currentPage++;
                pageNumber++;
                UpdateDataGridView();
                PreviousBtn.Enabled = true;
            }
            else
            {
                NextBtn.Enabled = false;
            }
           
        }

        private void Form3_FormClosed(object sender, FormClosedEventArgs e)
        {
            Form1 f1 = new Form1();
            this.Hide();
            f1.Show();
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            recordsPerPage = Convert.ToInt32(comboBox1.SelectedItem.ToString());
            currentPage = 1;
            NextBtn.Enabled = true;

            UpdateDataGridView();
        }
    }
}
