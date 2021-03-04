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
    public partial class Form7 : Form
    { 
        public Form7()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            DialogResult res = MessageBox.Show("Выйти в меню авторизации?", "Подтверждение", MessageBoxButtons.YesNo);
            if (res == DialogResult.Yes)
            {
                Program.auth = "1";
                this.Close();
            }
        }

        private void Form7_Load(object sender, EventArgs e)
        {
            groupBox1.Visible = false;
            groupBox2.Visible = false;
            voditeli();
            zayavki();
            gridsize();
            timer1.Start();
        }
        private void voditeli()
        {
            using (SqlConnection conn = new SqlConnection(Properties.Settings.Default.connString))
            {

                conn.Open();
                string query = "select ФИО, ГосНомер from Водители";
                SqlCommand cmd = new SqlCommand(query, conn);
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataSet ds = new DataSet();
                da.Fill(ds);
                for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                {
                    comboBox1.Items.Add(ds.Tables[0].Rows[i][0] + ", " + ds.Tables[0].Rows[i][1]);
                }
            }

        }
        private void zayavki()
        {
            SqlConnection con = new SqlConnection();
            con.ConnectionString = Properties.Settings.Default.connString;
            con.Open();
            SqlDataAdapter adap = new SqlDataAdapter("select Код_Заявки as 'Код заявки', Статус, ФИО_Клиента as 'ФИО Клиента', Номер_Клиента as 'Номер клиента', Время_Заявки as 'Дата, время заявки', Улица_Посадки as 'Улица посадки', Район_Посадки as 'Район посадки', НомерДомаПосадки as 'Дом посадки', Улица_Высадки as 'Улица высадки', Район_Высадки as 'Район высадки', НомерДомаВысадки as 'Дом высадки' from Заявки where Статус = 'В обработке'", con);
            DataSet ds = new System.Data.DataSet();
            adap.Fill(ds, "Заявки");
            dataGridView1.DataSource = ds.Tables[0];
        }
        private void gridsize()
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

        private void button2_Click(object sender, EventArgs e)
        {
            groupBox1.Visible = false;
            groupBox2.Visible = false;
            SqlConnection con = new SqlConnection();
            con.ConnectionString = Properties.Settings.Default.connString;
            con.Open();
            SqlDataAdapter adap = new SqlDataAdapter("select Код_Водителя as 'Код водителя', ФИО, ГосНомер, МаркаАвто, ЦветАвто, Статус from Водители", con);
            DataSet ds = new System.Data.DataSet();
            adap.Fill(ds, "Водители");
            dataGridView1.DataSource = ds.Tables[0];
        }

        private void button3_Click(object sender, EventArgs e)
        {
            zayavki();
            groupBox1.Visible = !groupBox1.Visible;
            groupBox2.Visible = false;
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                textBox1.Text = dataGridView1.Rows[e.RowIndex].Cells[0].FormattedValue.ToString();
                textBox2.Text = dataGridView1.Rows[e.RowIndex].Cells[0].FormattedValue.ToString();
                textBox3.Text = dataGridView1.Rows[e.RowIndex].Cells[3].FormattedValue.ToString();
                textBox4.Text = dataGridView1.Rows[e.RowIndex].Cells[5].FormattedValue.ToString();
                textBox5.Text = dataGridView1.Rows[e.RowIndex].Cells[6].FormattedValue.ToString();
                textBox6.Text = dataGridView1.Rows[e.RowIndex].Cells[8].FormattedValue.ToString();
            }
            catch
            {

            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            if ((textBox1.Text.Length > 0) && (comboBox1.Text.Length > 0))
            {
                try
                {
                    string query = "INSERT INTO Обслуживание_Заявок(Код_Заявки, Код_Водителя, Дата_Приема_Заявки) " +
                     "Values('" + textBox1.Text + "', '" + (comboBox1.SelectedIndex + 1) + "', '" + DateTime.Now.ToString() + "' ) ";

                    using (SqlConnection connection = new SqlConnection(Properties.Settings.Default.connString))
                    {
                        using (SqlCommand command = new SqlCommand(query, connection))
                        {
                            connection.Open();
                            command.ExecuteNonQuery();
                            zayavki();
                        }
                    }
                    MessageBox.Show("Вы успешно назначили водителя.", "Оповещение", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    groupBox1.Visible = false;
                    updatevoditel();
                    string query2 = "update Заявки set Статус = 'Водитель отправлен' where Код_Заявки = '" + textBox1.Text + "'";

                    using (SqlConnection connection = new SqlConnection(Properties.Settings.Default.connString))
                    {
                        using (SqlCommand command = new SqlCommand(query2, connection))
                        {
                            connection.Open();
                            command.ExecuteNonQuery();
                            zayavki();
                        }
                    }
                }
                catch
                {
                    MessageBox.Show("Неверный ввод данных!", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                MessageBox.Show("Введены не все данные!", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        
        }
        private void updatevoditel()
        {
            string query2 = "update Водители set Статус = 'Активен' where Код_Водителя = '" + (comboBox1.SelectedIndex + 1) + "'";

            using (SqlConnection connection = new SqlConnection(Properties.Settings.Default.connString))
            {
                using (SqlCommand command = new SqlCommand(query2, connection))
                {
                    connection.Open();
                    command.ExecuteNonQuery();
                }
            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            groupBox1.Visible = false;
            groupBox2.Visible = false;
            SqlConnection con = new SqlConnection();
            con.ConnectionString = Properties.Settings.Default.connString;
            con.Open();
            SqlDataAdapter adap = new SqlDataAdapter("select Заявки.Код_Заявки as 'Код заявки', ФИО_Клиента as 'ФИО Клиента', Номер_Клиента as 'Номер клиента', Водители.ФИО as 'ФИО Водителя', Водители.ГосНомер, Улица_Посадки as 'Улица посадки', Район_Посадки as 'Район посадки', НомерДомаПосадки as 'Дом посадки', Улица_Высадки as 'Улица высадки', Район_Высадки as 'Район высадки', НомерДомаВысадки as 'Дом высадки', Обслуживание_Заявок.Дата_Приема_Заявки as 'Дата Заявки', Обслуживание_Заявок.Время_Начала as 'Время начала', Обслуживание_Заявок.Дата_Окончания as 'Дата окончания', Обслуживание_Заявок.Время_Окончания as 'Время окончания', Обслуживание_Заявок.Купон, Обслуживание_Заявок.Цена, Заявки.Статус from Заявки inner join Обслуживание_Заявок on Заявки.Код_Заявки = Обслуживание_Заявок.Код_Заявки inner join Водители on Обслуживание_Заявок.Код_Водителя = Водители.Код_Водителя", con);
            DataSet ds = new System.Data.DataSet();
            adap.Fill(ds, "Заявки");
            dataGridView1.DataSource = ds.Tables[0];
        }

        private void button6_Click(object sender, EventArgs e)
        {
            zayavki();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            label3.Text = DateTime.Now.ToString("dd-MMM-yyyy hh:mm:ss");
        }

        private void button8_Click(object sender, EventArgs e)
        {
            groupBox1.Visible = false;
            groupBox2.Visible = !groupBox2.Visible;
            redact();
        }
        private void redact()
        {
            SqlConnection con = new SqlConnection();
            con.ConnectionString = Properties.Settings.Default.connString;
            con.Open();
            SqlDataAdapter adap = new SqlDataAdapter("select Заявки.Код_Заявки as 'Код заявки', ФИО_Клиента as 'ФИО Клиента', Номер_Клиента as 'Номер клиента', Улица_Посадки as 'Улица посадки', Район_Посадки as 'Район посадки', НомерДомаПосадки as 'Дом посадки', Улица_Высадки as 'Улица высадки', Район_Высадки as 'Район высадки', НомерДомаВысадки as 'Дом высадки',  Обслуживание_Заявок.Купон, Обслуживание_Заявок.Цена, Заявки.Статус  from Заявки inner join Обслуживание_Заявок on Заявки.Код_Заявки = Обслуживание_Заявок.Код_Заявки where Заявки.Статус = 'В обработке' or Заявки.Статус = 'Водитель отправлен' or Заявки.Статус = 'Активно'", con);
            DataSet ds = new System.Data.DataSet();
            adap.Fill(ds, "Заявки");
            dataGridView1.DataSource = ds.Tables[0];
        }

        private void button9_Click(object sender, EventArgs e)
        {
            try
            {
                SqlConnection con = new SqlConnection();
                con.ConnectionString = Properties.Settings.Default.connString;
                
                SqlCommand cmd = new SqlCommand("update Заявки set Улица_Посадки = @Vid, НомерДомаПосадки = @home, Улица_Высадки = @ss, НомерДомаВысадки = @da where Код_Заявки = @id", con);
                con.Open();
                cmd.Parameters.AddWithValue("@id", textBox2.Text);
                cmd.Parameters.AddWithValue("@vid", textBox3.Text);
                cmd.Parameters.AddWithValue("@home", textBox4.Text);
                cmd.Parameters.AddWithValue("@ss", textBox5.Text);
                cmd.Parameters.AddWithValue("@da", textBox6.Text);
                cmd.ExecuteNonQuery();
                con.Close();
                MessageBox.Show("Данные успешно обновлены!");
                redact();
            }
            catch
            {
                MessageBox.Show("Данные не удалось обновить.");
            }
        }
    }
 }

