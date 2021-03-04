using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Служба_заказа_Такси
{
    public partial class Form5 : Form
    {
        public Form5()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Program.auth = "3";
            this.Close();
        }

        private void Form5_Load(object sender, EventArgs e)
        {
            groupBox1.Visible = false;
            button5.Visible = false;
            messagedrive();
            timer1.Start();
            zayavki();
            gridsize();
            maskedTextBox1.Visible = false;
            int totalRows = dataGridView1.Rows.Count;

        }
        private void zayavki()
        {
            SqlConnection con = new SqlConnection();
            con.ConnectionString = Properties.Settings.Default.connString;
            con.Open();
            SqlDataAdapter adap = new SqlDataAdapter("select Время_Заявки as 'Дата, время заявки', Улица_Посадки as 'Улица посадки', Район_Посадки as 'Район посадки', НомерДомаПосадки as 'Дом посадки', Улица_Высадки as 'Улица высадки', Район_Высадки as 'Район высадки', НомерДомаВысадки as 'Дом высадки', Статус from Заявки where Статус = 'В обработке' or Статус = 'Активно' or Статус = 'Водитель отправлен' and Номер_Клиента = '" + Program.nomer + "'", con);
            DataSet ds = new System.Data.DataSet();
            adap.Fill(ds, "Заявки");
            dataGridView1.DataSource = ds.Tables[0];
        }
        private void messagedrive()
        {
            try
            {
                SqlConnection con3 = new SqlConnection(Properties.Settings.Default.connString);
                con3.Open();
                SqlCommand sqlCmd3 = new SqlCommand("SELECT Статус from Заявки where Номер_Клиента = '" + Program.nomer + "' and Статус = 'В обработке'", con3);
                sqlCmd3.ExecuteNonQuery();
                SqlDataReader reader3 = sqlCmd3.ExecuteReader();
                reader3.Read();
                if (reader3["Статус"].ToString() == "В обработке")
                {
                    zayavki();
                }
            }
           catch
            {
                try
                {
                    SqlConnection con = new SqlConnection(Properties.Settings.Default.connString);
                    con.Open();
                    SqlCommand sqlCmd1 = new SqlCommand("SELECT Статус from Заявки where Номер_Клиента = '" + Program.nomer + "' and Статус = 'Активно'", con);
                    sqlCmd1.ExecuteNonQuery();
                    SqlDataReader reader1 = sqlCmd1.ExecuteReader();
                    reader1.Read();
                    if (reader1["Статус"].ToString() == "Активно")
                    {
                        button5.Visible = true;
                    }
                }
                catch
                {
                    try
                    {
                        SqlConnection con2 = new SqlConnection(Properties.Settings.Default.connString);
                        con2.Open();
                        SqlCommand sqlCmd2 = new SqlCommand("SELECT Статус from Заявки where Номер_Клиента = '" + Program.nomer + "' and Статус = 'Водитель отправлен'", con2);
                        sqlCmd2.ExecuteNonQuery();
                        SqlDataReader reader2 = sqlCmd2.ExecuteReader();
                        reader2.Read();
                        if (reader2["Статус"].ToString() == "Водитель отправлен")
                        {
                            groupBox1.Visible = true;
                            using (SqlConnection conn = new SqlConnection(Properties.Settings.Default.connString))
                            {

                                conn.Open();
                                string query = "select Водители.ГосНомер, Водители.МаркаАвто, Водители.ЦветАвто, Водители.ФИО from Обслуживание_Заявок inner join Водители on Обслуживание_Заявок.Код_Водителя = Водители.Код_Водителя";
                                SqlCommand cmd = new SqlCommand(query, conn);
                                SqlDataAdapter da = new SqlDataAdapter(cmd);
                                DataSet ds = new DataSet();
                                da.Fill(ds);
                                groupBox1.Visible = true;
                                for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                                {
                                    label2.Text = "К вам был отправлен автомобиль " + ds.Tables[0].Rows[i][0] + ", Марка " + ds.Tables[0].Rows[i][1] + ", Цвет " + ds.Tables[0].Rows[i][2] + ", Водитель: " + ds.Tables[0].Rows[i][3];
                                }
                                label3.Text = "Итоговая цена за поездку: " + Program.plusprise.ToString() + " рублей.";
                            }
                        }

                    }
                    catch
                    {
                        MessageBox.Show("Нет активных заказов.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        Program.auth = "3";
                        this.Close();
                    }
                }
            }
        }
        private void gridsize()
        {
            try
            {
                for (int i = 0; i < dataGridView1.Columns.Count - 1; i++)
                {
                    dataGridView1.Columns[i].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
                }
                dataGridView1.Columns[dataGridView1.Columns.Count - 1].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;

                for (int i = 0; i < dataGridView1.Columns.Count; i++)
                {
                    int colw = dataGridView1.Columns[i].Width;
                    dataGridView1.Columns[i].AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
                    dataGridView1.Columns[i].Width = colw;

                }
            }
            catch
            {

            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            zayavki();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            label1.Text = DateTime.Now.ToString("dd-MMM-yyyy hh:mm:ss");
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox1.Checked)
            {
                maskedTextBox1.Visible = true;
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            try
            {
                SqlConnection con = new SqlConnection(Properties.Settings.Default.connString);
                SqlDataAdapter sda = new SqlDataAdapter("Select Count(*) From Купоны where Купон = '" + maskedTextBox1.Text + "'", con);
                DataTable dt = new DataTable();
                sda.Fill(dt);
                if (dt.Rows[0][0].ToString() == "1")
                {
                    string query2 = "update Обслуживание_Заявок set Купон = '" + maskedTextBox1.Text + "' from Обслуживание_Заявок inner join Заявки on Заявки.Код_Заявки = Обслуживание_Заявок.Код_Заявки where Номер_Клиента = '" + Program.nomer + "'";

                    using (SqlConnection connection = new SqlConnection(Properties.Settings.Default.connString))
                    {
                        using (SqlCommand command = new SqlCommand(query2, connection))
                        {
                            connection.Open();
                            command.ExecuteNonQuery();
                        }
                    }
                    MessageBox.Show("Вы успешно активировали купон со скидкой 15%", "Оповещение", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    Program.plusprise = Program.plusprise - ((15 * Program.plusprise) / 100);
                    Program.coupon = maskedTextBox1.Text;
                    maskedTextBox1.Visible = false;
                    checkBox1.Visible = false;
                    button3.Visible = false;
                    messagedrive();
                }
            }
            catch
            {
                MessageBox.Show("Данный купон невалиден!", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            try
            {
                string query = "UPDATE Обслуживание_Заявок SET Обслуживание_Заявок.Цена = '" + Program.plusprise + "', Обслуживание_Заявок.Время_Начала = '" + (DateTime.Now.ToString("HH:mm:ss")) + "' from Обслуживание_Заявок inner join Заявки on Обслуживание_Заявок.Код_Заявки = Заявки.Код_Заявки where Заявки.Номер_Клиента = '" + Program.nomer  + "' and Заявки.Статус = 'Водитель отправлен'";
                using (SqlConnection connection = new SqlConnection(Properties.Settings.Default.connString))
                {
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        connection.Open();
                        command.ExecuteNonQuery();
                    }
                }
                MessageBox.Show("Нажмите на кнопку по завершении поездки.", "Оповещение", MessageBoxButtons.OK, MessageBoxIcon.Information);
                groupBox1.Visible = false;
                button5.Visible = true;
            }
            catch
            {
                MessageBox.Show("Неверный ввод данных!", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            string query2 = "update Заявки set Статус = 'Активно' where Номер_Клиента = '" + Program.nomer + "' and Статус = 'Водитель отправлен'";

            using (SqlConnection connection = new SqlConnection(Properties.Settings.Default.connString))
            {
                using (SqlCommand command = new SqlCommand(query2, connection))
                {
                    connection.Open();
                    command.ExecuteNonQuery();
                }
            }
            groupBox1.Visible = false;
            zayavki();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            
            string query3 = "update Обслуживание_Заявок set Дата_Окончания = '" + DateTime.Now.ToString("yyyy-MM-dd") + "', Время_Окончания = '" + DateTime.Now.ToString("h: mm:ss") + "' from Обслуживание_Заявок inner join Заявки on Заявки.Код_Заявки = Обслуживание_Заявок.Код_Заявки where Номер_Клиента = '" + Program.nomer + "' and Статус = 'Активно'";

            using (SqlConnection connection = new SqlConnection(Properties.Settings.Default.connString))
            {
                using (SqlCommand command = new SqlCommand(query3, connection))
                {
                    connection.Open();
                    command.ExecuteNonQuery();
                }
            }
            string query = "update Водители set Статус = 'Свободен' from Водители join Обслуживание_Заявок on Обслуживание_Заявок.Код_Водителя = Водители.Код_Водителя join Заявки on Заявки.Код_Заявки = Обслуживание_Заявок.Код_Заявки where Номер_Клиента = '" + Program.nomer + "'";
            using (SqlConnection connection = new SqlConnection(Properties.Settings.Default.connString))
            {
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    connection.Open();
                    command.ExecuteNonQuery();
                }
            }
            string query2 = "update Заявки set Статус = 'Завершено' where Номер_Клиента = '" + Program.nomer + "' and Статус = 'Активно'";

            using (SqlConnection connection = new SqlConnection(Properties.Settings.Default.connString))
            {
                using (SqlCommand command = new SqlCommand(query2, connection))
                {
                    connection.Open();
                    command.ExecuteNonQuery();
                }
            }
            button5.Visible = false;
            groupBox1.Visible = false;
            MessageBox.Show("Благодарим за использование наших услуг!");
            zayavki();
            Program.auth = "6";
            this.Close();
        }
    }
}
