using System;
using System.Windows.Forms;
using Lab3.Forms;

namespace Lab3
{
    // Точка входа в программу
    class Program
    {
        [STAThread]
        static void Main()
        {
            // Запускаем приложение Windows Forms
            Application.Run(new GraphForm());
        }
    }
}

