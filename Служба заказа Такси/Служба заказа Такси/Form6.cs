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
    public partial class Form6 : Form
    {
        public Form6()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Program.auth = "3";
            this.Close();
        }

        private void Form6_Load(object sender, EventArgs e)
        {
            zayavki();
            gridsize();
        }
        private void zayavki()
        {
            SqlConnection con = new SqlConnection();
            con.ConnectionString = Properties.Settings.Default.connString;
            con.Open();
            SqlDataAdapter adap = new SqlDataAdapter("select Обслуживание_Заявок.Дата_Приема_Заявки as 'Дата заявки', Обслуживание_Заявок.Время_Начала as 'Время начала', Обслуживание_Заявок.Дата_Окончания as 'Дата Окончания', Обслуживание_Заявок.Время_Окончания as 'Время Окончания', Улица_Посадки as 'Улица посадки', Район_Посадки as 'Район посадки', НомерДомаПосадки as 'Дом посадки', Улица_Высадки as 'Улица высадки', Район_Высадки as 'Район высадки', НомерДомаВысадки as 'Дом высадки', Заявки.Статус, Обслуживание_Заявок.Купон,Обслуживание_Заявок.Цена, Водители.ФИО as 'ФИО Водителя', Водители.ГосНомер from Заявки  join Обслуживание_Заявок on Обслуживание_Заявок.Код_Заявки = Заявки.Код_Заявки join Водители on Водители.Код_Водителя = Обслуживание_Заявок.Код_Водителя where Заявки.Статус = 'Завершено'  and Номер_Клиента = '" + Program.nomer + "'", con);
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
    }
}
