using System;
using System.Windows.Forms;
using Lab5.Forms;

namespace Lab5
{
    // Точка входа в программу
    class Program
    {
        [STAThread]
        static void Main()
        {
            // Включаем визуальные стили Windows
            Application.EnableVisualStyles();
            
            // Запускаем приложение Windows Forms
            Application.Run(new MainForm());
        }
    }
}

