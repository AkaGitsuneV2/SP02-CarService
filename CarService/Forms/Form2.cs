using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MetroFramework.Components;
using MetroFramework.Animation;
using CarService.Class;
using System.Threading;

namespace CarService.Forms
{
    public partial class Form2 : MetroFramework.Forms.MetroForm
    {
        public Form2()
        {
            InitializeComponent();
        }
        private int currentPage = 1;
        private int pageSize = 15;
        private int pageNumber = 1;
        private void Form2_Load(object sender, EventArgs e)
        {
            label1.Text = $@"Стр." + pageNumber.ToString();
            groupBox1.Visible = false;
        }

        private void metroButton1_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            //int count;
            //if (comboBox1.Text == "")
            //{
            //    MessageBox.Show("Выберете количество записей", "Внимание", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            //}
            //else
            //{
            //    count = Convert.ToInt32(comboBox1.Text);
            //    label2.Text = ClientsClass.SQLQuery(count, pageSize);
            //    groupBox1.Visible = true;
            //    PreviousBtn.Enabled = false;
        }
        //private bool HasNextPage()
        //{
        //    //int totalRecords = ClientsClass.GetTotalRecordsCount();  
        //    //int totalPages = (int)Math.Ceiling((double)totalRecords / pageSize);
        //    //return currentPage < totalPages;
        //}

        //private bool HasPreviousPage()
        //{
        //   // return currentPage > 1;
        //}

        
        private void UpdateLabelTable()
        {
            //label2.Text = ClientsClass.SQLQuery(currentPage, pageSize);
            //NextBtn.Enabled = HasNextPage();
            //PreviousBtn.Enabled = HasPreviousPage();
        }
        private void NextBtn_Click(object sender, EventArgs e)
        {
            //currentPage++;
            //pageNumber++;
            //label1.Text = $@"Стр." + pageNumber.ToString();
            //UpdateLabelTable();
        }

        private void PreviousBtn_Click(object sender, EventArgs e)
        {
            //try
            //{
            //    pageNumber--;
            //    currentPage--;
            //    if (pageNumber <= 1)
            //    {
            //        pageNumber = 1;
            //        label1.Text = $@"Стр." + pageNumber.ToString();
            //        UpdateLabelTable();
            //    }
            //}
            //catch (Exception ex)
            //{
            //    label2.Text = ex.Message;
            //}

        }

        private void Form2_FormClosed(object sender, FormClosedEventArgs e)
        {
            Form1 f1 = new Form1();
            f1.Show();
        }
    }
}
