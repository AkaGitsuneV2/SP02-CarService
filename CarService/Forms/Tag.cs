using CarService.Class;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CarService.Forms
{
    public partial class Tag : MetroFramework.Forms.MetroForm
    {
        public Tag()
        {
            InitializeComponent();
        }

        private void Tag_Load(object sender, EventArgs e)
        {
            ClassRefactorClients.GetTagTable();
            comboBox1.DataSource = ClassRefactorClients.dtTags;
            comboBox1.ValueMember = "ID";
            comboBox1.DisplayMember = "Title";

        }

        private void button1_Click(object sender, EventArgs e)
        {
            bool success = ClassRefactorClients.AppendTag(Form3.id, comboBox1.SelectedValue.ToString());
            if (success)
            {
                ClientsClass.GetTableTag(Form3.id);
                Form4Insert form4Insert = new Form4Insert();
                form4Insert.dataGridView2.DataSource = DBConnection.dtTag;
            }
            this.Close();
        }
    }
}
