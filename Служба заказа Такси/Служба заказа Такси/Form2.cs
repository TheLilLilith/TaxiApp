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
    public partial class Form2 : Form
    {
        public Form2()
        {
            InitializeComponent();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Program.auth = "1";
            this.Close();
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            if ((textBox1.Text.Length > 0) && (textBox2.Text.Length > 0) && (maskedTextBox1.Text.Length > 0))
            {
                if (textBox2.Text.Length > 5)
                {
                    try
                    {
                        string query = "INSERT INTO Пользователи(Номер_Телефона, ФИО, Пароль) " +
                         "Values('" + maskedTextBox1.Text + "', '" + textBox1.Text + "', '" + textBox2.Text + "' ) ";

                        using (SqlConnection connection = new SqlConnection(Properties.Settings.Default.connString))
                        {
                            using (SqlCommand command = new SqlCommand(query, connection))
                            {
                                connection.Open();
                                command.ExecuteNonQuery();
                                MessageBox.Show("Регистрация прошла успешно!", "Оповещение");
                                Program.auth = "1";
                                this.Close();
                            }
                        }
                    }
                    catch
                    {

                    }
                }
                else
                {
                    MessageBox.Show("Пароль должен состоять из 6 символов.","Ошибка");
                }
               
            }
            else
            {
                MessageBox.Show("Введены не все данные", "Ошибка");
            }
        }
    }
}
