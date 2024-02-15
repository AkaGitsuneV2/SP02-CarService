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
using static System.Windows.Forms.VisualStyles.VisualStyleElement.ListView;

namespace CarService.Forms
{
    public partial class Form3 : MetroFramework.Forms.MetroForm
    {
        private int currentPage = 1;
        private int recordsPerPage = 2000;
        
        public static string id;
        public static string count;

        private string firstName;
        private string lastName;
        private string patronymic;
        private string birthday;
        private string email;
        private string phone;
        private string gender;

        private int pageNumber = 1;
        private int sort;
        public Form3()
        {
            InitializeComponent();
        }
        private void UpdateDataGridView()
        {
            if (comboBox2.Text == "")
            {
                int offset = (currentPage - 1) * recordsPerPage;
                string firstName = textBox1.Text.Trim();
                string email = textBox2.Text.Trim();
                string phone = textBox3.Text.Trim();

                ClientsClass.GetTableClientWithSearch(firstName, email, phone, "", recordsPerPage, offset);
                UpdatePageInfo();
            }
            else
            {
                int offset = (currentPage - 1) * recordsPerPage;
                string firstName = textBox1.Text.Trim();
                string email = textBox2.Text.Trim();
                string phone = textBox3.Text.Trim();
                string selectedGender = comboBox2.SelectedItem.ToString();
                string genderCode = SelectGendeMethod(selectedGender);
                ClientsClass.GetTableClientWithSearch(firstName, email, phone, genderCode, recordsPerPage, offset);
                UpdatePageInfo();
            }
        }
        private void UpdatePageInfo()
        {
            int totalRecords = ClientsClass.GetTotalRecordsCount();
            int startRecord = (currentPage - 1) * recordsPerPage + 1;
            int endRecord = Math.Min(startRecord + recordsPerPage - 1, totalRecords);

            label2.Text = $"{startRecord} - {endRecord} из {totalRecords}";
        }
        private string SelectGendeMethod(string a)
        {
            switch (a)
            {
                case "Мужской":
                    return "м";
                case "Женский":
                    return "ж";
                default:
                    return "";
            }
        }
        private void Form3_Load(object sender, EventArgs e)
        {
            #region Аналог placeholder

            label5.Text = "Поиск по ФИО";
            label6.Text = "Поиск по Email";
            label7.Text = "Поиск по Телефону";
            
            #endregion

            PreviousBtn.Enabled = false;
            NextBtn.Enabled = false;

            ClientsClass.GetFullTable();
            dataGridView1.DataSource = DBConnection.dtClients;

            string a = ClientsClass.Count();

            label2.Text = "Отображается " + a + " из " + a + " записей";
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
            switch (comboBox1.SelectedItem.ToString())
            {
                case "10":
                    recordsPerPage = 10;
                    break;
                case "50":
                    recordsPerPage = 50;
                    break;
                case "200":
                    recordsPerPage = 200;
                    break;
                default:
                    recordsPerPage = 2000;
                    break;
            }
            currentPage = 1;
            NextBtn.Enabled = true;

            UpdateDataGridView();
        }

        private void label5_Click(object sender, EventArgs e)
        {
            textBox1.Focus();
        }

        private void label6_Click(object sender, EventArgs e)
        {
            textBox2.Focus();
        }

        private void label7_Click(object sender, EventArgs e)
        {
            textBox3.Focus();
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            label5.Visible = string.IsNullOrEmpty(textBox1.Text);

            currentPage = 1;
            UpdateDataGridView();
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {
            label6.Visible = string.IsNullOrEmpty(textBox2.Text);
            currentPage = 1;
            UpdateDataGridView();
        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {
            label7.Visible = string.IsNullOrEmpty(textBox3.Text);
            currentPage = 1;
            UpdateDataGridView();
        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            UpdateDataGridView();
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            id = dataGridView1.CurrentRow.Cells[0].Value.ToString();
            count = dataGridView1.CurrentRow.Cells[10].Value.ToString();
            ClientsClass.GetTableTag(id);
            dataGridView2.DataSource = DBConnection.dtTag;

            firstName = dataGridView1.CurrentRow.Cells[3].Value.ToString();
            lastName = dataGridView1.CurrentRow.Cells[2].Value.ToString();
            patronymic = dataGridView1.CurrentRow.Cells[4].Value.ToString();
            birthday = dataGridView1.CurrentRow.Cells[5].Value.ToString();
            phone = dataGridView1.CurrentRow.Cells[6].Value.ToString() ;
            email = dataGridView1.CurrentRow.Cells[8].Value.ToString();
            gender = dataGridView1.CurrentRow.Cells[1].Value.ToString();
        }

        private Color SetBackgroundColor(Color color)
        {
            int b = (int)(color.R * 0.299 + color.R * 0.587 + color.G * 0.114);
            return b > 128 ? Color.Black : Color.White;
        }

        private void dataGridView2_RowPrePaint(object sender, DataGridViewRowPrePaintEventArgs e)
        {
            int colorColumnIndex = 2;
            DataGridViewRow row = dataGridView2.Rows[e.RowIndex];

            if (colorColumnIndex >= 0 && colorColumnIndex < row.Cells.Count)
            {
                string hexColor = row.Cells[colorColumnIndex].Value.ToString();

                if (!string.IsNullOrEmpty(hexColor))
                {
                    Color color = ColorTranslator.FromHtml(hexColor);
                    row.DefaultCellStyle.BackColor = color;
                    row.DefaultCellStyle.ForeColor = SetBackgroundColor(color);
                    dataGridView2.ClearSelection();
                }
            }
        }

        private void Form3_Activated(object sender, EventArgs e)
        {
        }

        private void ApplySort()
        {
            ClientsClass.Sort(sort);

            UpdatePageInfo();
        }
        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {
            sort = 1;
            ApplySort();
        }

        private void radioButton2_CheckedChanged(object sender, EventArgs e)
        {
            sort = 2;
            ApplySort();
        }

        private void radioButton3_CheckedChanged(object sender, EventArgs e)
        {
            sort = 3;
            ApplySort();

        }

        private void button5_Click(object sender, EventArgs e)
        {
            ClientsClass.HappyBirthday();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            Form2 f2 = new Form2();
            f2.DisplayLabelList(id,count);
            f2.ShowDialog();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Hide();
            Form4Insert f4 = new Form4Insert();
            f4.dataGridView2.Visible = false;
            f4.button1.Visible = true;
            f4.button6.Visible = false;
            f4.button2.Visible = false;
            f4.button3.Visible = true;
            f4.label1.Visible = false;
            f4.textBox1.Visible = false;
            f4.ShowDialog();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Hide();
            string r = ClientsClass.GetPhotoPath(id);
            Form4Insert f4 = new Form4Insert();
            if (gender == "ж")
                f4.radioButton2.Checked = true;
            else
                f4.radioButton1.Checked = true;
            f4.label1.Visible = true;
            f4.pictureBox1.Load(r);
            f4.textBox1.Visible = true;
            f4.textBox1.Text = id;
            f4.textBox2.Text = firstName;
            f4.textBox3.Text = lastName;
            f4.textBox4.Text = patronymic;
            f4.textBox5.Text = email;
            f4.textBox6.Text = phone;
            f4.button6.Visible = true;
            f4.button1.Visible=false;
            f4.dataGridView2.Visible = true;
            f4.dateTimePicker1.Text = birthday;
            f4.ShowDialog();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            DialogResult res = MessageBox.Show("Вы уверены?", "Удалить запись", MessageBoxButtons.YesNo,MessageBoxIcon.Question);
            if (res == DialogResult.Yes)
            {
                bool success = ClassRefactorClients.DeleteClient(id);
                if (success)
                {
                    UpdateDataGridView();
                }
            }
        }
    }
}
