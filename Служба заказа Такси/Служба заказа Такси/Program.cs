using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Служба_заказа_Такси
{
    static class Program
    {
        internal static string auth = "0";
        internal static int plusprise = 125;
        internal static string fio;
        internal static string nomer;
        internal static string coupon;
        /// <summary>
        /// Главная точка входа для приложения.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Form1());
            
            for (int i = 0; i > -1; i++)
            {
                if (auth == "1")
                {
                    Application.Run(new Form1());
                }
                if (auth == "2")
                {
                    Application.Run(new Form2());
                }

                if (auth == "3")
                {
                    Application.Run(new Form3());
                }
                if (auth == "4")
                {
                    Application.Run(new Form4());
                }
                if (auth == "5")
                {
                    Application.Run(new Form5());
                }
                if (auth == "6")
                {
                    Application.Run(new Form6());
                }
                if (auth == "7")
                {
                    Application.Run(new Form7());
                }
            }
        }
    }
}
