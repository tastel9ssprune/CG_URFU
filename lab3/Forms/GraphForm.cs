using System;
using System.Windows.Forms;
using OxyPlot;
using OxyPlot.WindowsForms;
using Lab3.Utils;

namespace Lab3.Forms
{
    // Главная форма приложения для построения графиков
    public class GraphForm : Form
    {
        // Поля ввода для формул функций
        private TextBox inputFunction1;
        private TextBox inputFunction2;
        
        // Кнопка для построения графиков
        private Button drawButton;
        
        // Компонент для отображения графика
        private PlotView plotView;

        // Конструктор - создает форму и все элементы интерфейса
        public GraphForm()
        {
            // Настраиваем форму
            this.Text = "Лабораторная работа 3 - Графики функций";
            this.Width = 800;
            this.Height = 600;

            // Создаем метки для полей ввода
            var label1 = new Label 
            { 
                Left = 10, 
                Top = 10, 
                Width = 100, 
                Text = "Функция 1:" 
            };
            
            var label2 = new Label 
            { 
                Left = 10, 
                Top = 60, 
                Width = 100, 
                Text = "Функция 2:" 
            };

            // Создаем поля ввода для формул
            // В них можно вводить математические выражения с переменной x
            inputFunction1 = new TextBox 
            { 
                Left = 10, 
                Top = 30, 
                Width = 760, 
                Text = "Sqrt(1 - Pow(Abs(x) - 1, 2)) * 3 + 16" 
            };
            
            inputFunction2 = new TextBox 
            { 
                Left = 10, 
                Top = 80, 
                Width = 760, 
                Text = "Acos(-Abs(x / 2)) * 5" 
            };

            // Создаем компонент для отображения графика
            plotView = new PlotView
            {
                Top = 150, 
                Left = 10, 
                Width = 760, 
                Height = 400
            };

            // Создаем кнопку для построения графиков
            drawButton = new Button
            {
                Left = 10, 
                Top = 110, 
                Width = 180, 
                Height = 30, 
                Text = "Нарисовать график"
            };
            
            // Подключаем обработчик нажатия на кнопку
            drawButton.Click += DrawButton_Click;

            // Добавляем все элементы на форму
            Controls.Add(label1);
            Controls.Add(label2);
            Controls.Add(inputFunction1);
            Controls.Add(inputFunction2);
            Controls.Add(drawButton);
            Controls.Add(plotView);
        }

        // Обработчик нажатия на кнопку "Нарисовать график"
        // Создает новый график с функциями из полей ввода
        private void DrawButton_Click(object sender, EventArgs e)
        {
            // Создаем новую модель графика
            var model = new PlotModel 
            { 
                Title = "Графики функций" 
            };

            // Добавляем первую функцию на график
            // Используем синий цвет и круглые маркеры
            GraphBuilder.AddFunction(
                model, 
                inputFunction1.Text, 
                OxyColors.Blue, 
                MarkerType.Circle
            );

            // Добавляем вторую функцию на график
            // Используем красный цвет и ромбовидные маркеры
            GraphBuilder.AddFunction(
                model, 
                inputFunction2.Text, 
                OxyColors.Red, 
                MarkerType.Diamond
            );

            // Настраиваем оси графика
            GraphBuilder.SetupAxes(model, "X", "Y");

            // Отображаем график на форме
            plotView.Model = model;
        }
    }
}

