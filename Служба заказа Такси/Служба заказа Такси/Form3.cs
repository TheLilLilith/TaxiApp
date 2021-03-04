using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Служба_заказа_Такси
{
    public partial class Form3 : Form
    {
        public Form3()
        {
            InitializeComponent();
        }

        private void Form3_Load(object sender, EventArgs e)
        {
            
        }

        private void button4_Click(object sender, EventArgs e)
        {
            DialogResult res = MessageBox.Show("Вы действительно хотите выйти в меню авторизации?", "Подтверждение", MessageBoxButtons.YesNo);
            if (res == DialogResult.Yes)
            {
                Program.auth = "1";
                this.Close();
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Program.auth = "4";
            this.Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Program.auth = "5";
            this.Close();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Program.auth = "6";
            this.Close();
        }
    }
}
