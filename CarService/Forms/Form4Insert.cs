using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using CarService.Class;
using MetroFramework.Components;

namespace CarService.Forms
{
    public partial class Form4Insert : MetroFramework.Forms.MetroForm
    {
        public Form4Insert()
        {
            InitializeComponent();
        }
        private string firstName;
        private string lastName;
        private string patronymic;
        private string birthday;
        public static string registrationDate;
        private string email;
        private string phone;
        private string gender = "м";
        public static string photo, photo2;
        private void Form4Insert_Load(object sender, EventArgs e)
        {
            if (dataGridView2.Visible == true)
            {
                ClientsClass.GetTableTag(Form3.id);
                dataGridView2.DataSource = DBConnection.dtTag;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            if (ofd.ShowDialog() == DialogResult.OK)
            {
                pictureBox1.Load(ofd.FileName);
                photo = ofd.FileName;
                photo2 = ofd.FileName;
                photo = photo.Replace(@"\", @"\\");
            }
        }

        //Добавление
        private void button5_Click(object sender, EventArgs e)
        {
            if (textBox2.Text == "" || textBox3.Text == "" || textBox4.Text == "" || textBox5.Text == "" || textBox6.Text == "" || phone == "")
            {
                MessageBox.Show("Заполните все поля!","Внимание",MessageBoxButtons.OK,MessageBoxIcon.Information);
            }
            else if(IsValidEmail(textBox5.Text) == false)
            {
                MessageBox.Show("Некорректный email","Внимание",MessageBoxButtons.OK,MessageBoxIcon.Information);
            }
            else
            {
                firstName = textBox2.Text;
                lastName = textBox3.Text;
                patronymic = textBox4.Text;
                email = textBox5.Text;
                phone = textBox6.Text;
                birthday = dateTimePicker1.Value.ToString("yyyy-MM-dd");
                DateTime dt = DateTime.Now;
                registrationDate = dt.ToString("yyyy-MM-dd");

                ClassRefactorClients.InsertClient(firstName, lastName, patronymic, email, phone, birthday, registrationDate, gender, photo);
                this.Close();
                Form3 f3 = new Form3();
                f3.Show();
            }
        }
        private bool IsValidEmail(string email)
        {
            try
            {
                MailAddress mail = new MailAddress(email);
                return true;
            }
            catch (FormatException)
            {
                return false;
            }
        }
        private void textBox2_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsLetter(e.KeyChar) && !char.IsControl(e.KeyChar))
                e.Handled = true;
        }

        private void textBox3_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsLetter(e.KeyChar) && !char.IsControl(e.KeyChar))
                e.Handled = true;
        }

        private void textBox4_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsLetter(e.KeyChar) && !char.IsControl(e.KeyChar))
                e.Handled = true;
        }

        private void textBox6_KeyPress(object sender, KeyPressEventArgs e)
        {
            if(!char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar)) e.Handled = true;
        }

        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {
            gender = "м";
        }

        private void radioButton2_CheckedChanged(object sender, EventArgs e)
        {
            gender = "ж";
        }

        private void button6_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            if (ofd.ShowDialog() == DialogResult.OK)
            {
                pictureBox1.Load(ofd.FileName);
                photo = ofd.FileName;
                photo2 = ofd.FileName;
                photo = photo.Replace(@"\", @"\\");
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            this.Close();
            Form3 f3 = new Form3();
            f3.Show();
        }


        private void toolStripMenuItem1_Click(object sender, EventArgs e)
        {
            Tag tag = new Tag();
            tag.ShowDialog();
        }
        private void toolStripMenuItem2_Click(object sender, EventArgs e)
        {
            DialogResult res = MessageBox.Show("Убрать тег клиента?","???Вы уверены???", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (res == DialogResult.Yes)
            {
                bool success = ClassRefactorClients.DeleteTagClient(Form3.id, tag);
                if (success)
                {
                    ClientsClass.GetTableTag(Form3.id);
                    dataGridView2.DataSource = DBConnection.dtTag;
                }
            }
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
        public string tag;
        private void dataGridView2_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            tag = dataGridView2.CurrentRow.Cells[0].Value.ToString();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (textBox2.Text == "" || textBox3.Text == "" || textBox4.Text == "" || textBox5.Text == "" || textBox6.Text == "" || phone == "")
            {
                MessageBox.Show("Заполните все поля!", "Внимание", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else if (IsValidEmail(textBox5.Text) == false)
            {
                MessageBox.Show("Некорректный email", "Внимание", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                string id = textBox1.Text;
                firstName = textBox2.Text;
                lastName = textBox3.Text;
                patronymic = textBox4.Text;
                email = textBox5.Text;
                phone = textBox6.Text;
                birthday = dateTimePicker1.Value.ToString("yyyy-MM-dd");

                ClassRefactorClients.EditClient(id, firstName, lastName, patronymic,email, phone,birthday,gender,photo);
                this.Close();
            }
        }
    }
}
