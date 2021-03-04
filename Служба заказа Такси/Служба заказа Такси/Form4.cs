using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Collections.ObjectModel;
using System.Data.SqlClient;

namespace Служба_заказа_Такси
{
    public partial class Form4 : Form
    {
        List<string> shirotnaya = new List<string> { "Широтная", "Федюнинского", "Пермякова", "30 лет победы", "Мельникайте", "Монтажников" };
        List<string> kalininsky = new List<string> { "Ямская", "Барнаульская", "Авторемонтная", "Полевая", "Московский тракт", "Червишевский тракт", "Объездная дорога" };
        List<string> centralniy = new List<string> { "Щербакова", "Ветеранов труда", "Алебашевская", "Дружбы", "Магистральная", "Григория Алексеева", "Салаирский тракт" };
        List<string> leninskiy = new List<string> { "Харьковская", "Домбовская", "Мельникайте", "50 лет Октября", "Тобольский тракт", "Республики" };
        public Form4()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Program.auth = "3";
            this.Close();
        }

        private void Form4_Load(object sender, EventArgs e)
        {
            bindings();

        }
        private void bindings()
        {
            foreach (var item in shirotnaya)
                comboBox1.Items.Add(item);
            foreach (var item in leninskiy)
                comboBox1.Items.Add(item);
            foreach (var item in centralniy)
                comboBox1.Items.Add(item);
            foreach (var item in kalininsky)
                comboBox1.Items.Add(item);
            foreach (var item in shirotnaya)
                comboBox2.Items.Add(item);
            foreach (var item in leninskiy)
                comboBox2.Items.Add(item);
            foreach (var item in centralniy)
                comboBox2.Items.Add(item);
            foreach (var item in kalininsky)
                comboBox2.Items.Add(item);

        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if ((comboBox1.Text == "Широтная") || (comboBox1.Text == "Федюнинского") || (comboBox1.Text == "Пермякова") || (comboBox1.Text == "30 лет победы") || (comboBox1.Text == "Мельникайте") || (comboBox1.Text == "Монтажников"))
            {
                textBox1.Text = "Восточный";
            }
            if ((comboBox1.Text == "Республики") || (comboBox1.Text == "Харьковская") || (comboBox1.Text == "Домбовская") || (comboBox1.Text == "Мельникайте") || (comboBox1.Text == "50 лет Октября") || (comboBox1.Text == "Тобольский тракт"))
            {
                textBox1.Text = "Ленинский";
            }
            if ((comboBox1.Text == "Ямская") || (comboBox1.Text == "Барнаульская") || (comboBox1.Text == "Авторемонтная") || (comboBox1.Text == "Полевая") || (comboBox1.Text == "Московский тракт") || (comboBox1.Text == "Червишевсикй тракт") || (comboBox1.Text == "Объездная дорога"))
            {
                textBox1.Text = "Калининский";
            }
            if ((comboBox1.Text == "Щербакова") || (comboBox1.Text == "Ветеранов труда") || (comboBox1.Text == "Алебашевская") || (comboBox1.Text == "Дружбы") || (comboBox1.Text == "Магистральная") || (comboBox1.Text == "Григория Алексеева") || (comboBox1.Text == "Салаирский тракт"))
            {
                textBox1.Text = "Центральный";
            }
        }
        private void comboBox1_SelectedItem(object sender, EventArgs e)
        {
        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            if ((comboBox2.Text == "Широтная") || (comboBox2.Text == "Федюнинского") || (comboBox2.Text == "Пермякова") || (comboBox2.Text == "30 лет победы") || (comboBox2.Text == "Мельникайте") || (comboBox2.Text == "Монтажников"))
            {
                textBox2.Text = "Восточный";
            }
            if ((comboBox2.Text == "Республики") || (comboBox2.Text == "Харьковская") || (comboBox2.Text == "Домбовская") || (comboBox2.Text == "Мельникайте") || (comboBox2.Text == "50 лет Октября") || (comboBox2.Text == "Тобольский тракт"))
            {
                textBox2.Text = "Ленинский";
            }
            if ((comboBox2.Text == "Ямская") || (comboBox2.Text == "Барнаульская") || (comboBox2.Text == "Авторемонтная") || (comboBox2.Text == "Полевая") || (comboBox2.Text == "Московский тракт") || (comboBox2.Text == "Червишевсикй тракт") || (comboBox2.Text == "Объездная дорога"))
            {
                textBox2.Text = "Калининский";
            }
            if ((comboBox2.Text == "Щербакова") || (comboBox2.Text == "Ветеранов труда") || (comboBox2.Text == "Алебашевская") || (comboBox2.Text == "Дружбы") || (comboBox2.Text == "Магистральная") || (comboBox2.Text == "Григория Алексеева") || (comboBox2.Text == "Салаирский тракт"))
            {
                textBox2.Text = "Центральный";
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if ((comboBox1.Text.Length > 0) && (comboBox2.Text.Length > 0) && (textBox1.Text.Length > 0) && (textBox2.Text.Length > 0) && (textBox3.Text.Length > 0) && (textBox4.Text.Length > 0))
            {
                try
                {
                    string query = "INSERT INTO Заявки(ФИО_Клиента, Номер_Клиента, Время_Заявки, Улица_Посадки, Район_Посадки, НомерДомаПосадки, Улица_Высадки, Район_Высадки, НомерДомаВысадки, Статус) " +
                     "Values('" + Program.fio + "', '" + Program.nomer + "', '" + DateTime.Now.ToString() + "', '" + comboBox1.Text + "', '" + textBox1.Text + "', '" + textBox3.Text + "', '" + comboBox2.Text + "', '" + textBox2.Text + "', '" + textBox4.Text + "', 'В обработке' ) ";

                    using (SqlConnection connection = new SqlConnection(Properties.Settings.Default.connString))
                    {
                        using (SqlCommand command = new SqlCommand(query, connection))
                        {
                            connection.Open();
                            command.ExecuteNonQuery();
                        }
                    }
                    MessageBox.Show("Вы успешно оформили заявку. Ожидайте подтверждение оператора в статусе заказа в меню Активные заказы", "Оповещение",MessageBoxButtons.OK,MessageBoxIcon.Information);
                    if (textBox1.Text != textBox2.Text)
                    {
                        Program.plusprise += 50;
                    }
                    if (textBox1.Text == textBox2.Text)
                    {
                        Program.plusprise = 0;
                    }
                    Program.auth = "3";
                    this.Close();
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
    }
}
