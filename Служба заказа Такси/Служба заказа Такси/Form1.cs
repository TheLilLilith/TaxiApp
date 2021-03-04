using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace Служба_заказа_Такси
{
    public partial class Form1 : Form
    {
        int a = 0;
        int i;
        bool di;
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            textBox2.UseSystemPasswordChar = true;
        }

        private void button1_Click(object sender, EventArgs e)
        {
          //  try
            {
                SqlConnection con = new SqlConnection(Properties.Settings.Default.connString);
                SqlDataAdapter sda = new SqlDataAdapter("Select Count(*) From Пользователи where Номер_Телефона = '" + maskedTextBox1.Text + "' and Пароль = '" + textBox2.Text + "'", con);
                DataTable dt = new DataTable();
                sda.Fill(dt);
                if (dt.Rows[0][0].ToString() == "1")
                {
                    con.Open();

                    checking();
                    if (di == false)
                    {
                        checking2();
                    }
                }
                else
                {
                    MessageBox.Show("Неверное имя пользователя или пароль!", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    a++;
                    if (a == 3)
                    {
                        MessageBox.Show("Вы ввели логин или пароль неверно 3 раза. Повторите попытку через минуту.", "Предупреждение", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        maskedTextBox1.Enabled = false;
                        textBox2.Enabled = false;
                        button1.Enabled = false;
                        label4.Visible = true;
                        i = 59;
                        label4.Text = "Повторите попытку через: " + i.ToString();
                        timer1.Interval = 1000;
                        timer1.Enabled = true;
                        timer1.Start();
                        a = 0;
                    }
                }
            }
        //    catch
            {
                MessageBox.Show("Дальнейшая работа невозможна, так как нет подключения к базе данных!", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void checking()
        {
            SqlConnection con = new SqlConnection(Properties.Settings.Default.connString);
            con.Open();
            SqlCommand sqlCmd2 = new SqlCommand("SELECT Роль from Пользователи where Номер_Телефона = '" + maskedTextBox1.Text + "' and Пароль = '" + textBox2.Text + "'", con);
            sqlCmd2.ExecuteNonQuery();
            SqlDataReader reader2 = sqlCmd2.ExecuteReader();
            reader2.Read();
            if (reader2["Роль"].ToString() == "Диспетчер")
            {
                di = true;
                MessageBox.Show("Добро пожаловать!");
                Program.auth = "7";
                this.Close();
            }
            else
            {
                di = false;
            }
        }
        private void checking2()
        {
            SqlConnection con = new SqlConnection(Properties.Settings.Default.connString);
            con.Open();
            SqlCommand sqlCmd = new SqlCommand("SELECT ФИО from Пользователи where Номер_Телефона = '" + maskedTextBox1.Text + "' and Пароль = '" + textBox2.Text + "'", con);
            sqlCmd.ExecuteNonQuery();
            SqlDataReader reader = sqlCmd.ExecuteReader();
            reader.Read();

            string a = reader["ФИО"].ToString();
            Program.fio = a;
            Program.nomer = maskedTextBox1.Text;
            MessageBox.Show("Добро пожаловать, " + a + "!");

            Program.auth = "3";
            this.Close();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            label4.Text = "Повторите попытку через: " + (--i).ToString();
            if (i == 0)
            {
                timer1.Stop();
                label4.Visible = false;
                maskedTextBox1.Enabled = true;
                textBox2.Enabled = true;
                button1.Enabled = true;
            }
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {    
           textBox2.UseSystemPasswordChar = !textBox2.UseSystemPasswordChar;
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Program.auth = "2";
            this.Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            DialogResult res = MessageBox.Show("Вы действительно хотите выйти?", "Подтверждение", MessageBoxButtons.YesNo);
            if (res == DialogResult.Yes)
            {
                Environment.Exit(1);
            }
        }
    }
}

