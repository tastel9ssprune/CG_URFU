using System;
using System.Windows.Forms;
using Lab6.Forms;

namespace Lab6
{
    // Точка входа в программу
    static class Program
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

