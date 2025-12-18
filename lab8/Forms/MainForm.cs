using System;
using System.Drawing;
using System.Windows.Forms;
using Lab8.Renderers;
using Lab8.Controllers;

namespace Lab8.Forms
{
    // Главная форма приложения для raycasting
    public partial class MainForm : Form
    {
        // Компонент для отображения изображения
        private PictureBox pictureBox;
        
        // Рендерер для трассировки лучей
        private RayTracer render;

        // Конструктор - создает форму и инициализирует рендерер
        public MainForm()
        {
            // Настраиваем форму
            this.Text = "Лабораторная работа 8 - Raycasting 3D";
            this.Width = 1200;
            this.Height = 600;

            // Создаем компонент для отображения изображения
            pictureBox = new PictureBox();
            pictureBox.Width = 800;
            pictureBox.Height = 600;
            this.Controls.Add(pictureBox);

            // Создаем рендерер
            render = new RayTracer(pictureBox.Width, pictureBox.Height);

            // При загрузке формы рендерим сцену
            this.Load += (s, e) =>
                pictureBox.Image = render.RenderScene();

            // Подключаем обработчики клавиатуры
            this.KeyDown += MainForm_KeyDown;
            this.KeyUp += MainForm_KeyUp;
            
            // Включаем обработку клавиатуры
            this.KeyPreview = true;

            // Добавляем UI для отображения информации о камере
            AddCameraUI();
            UpdateCameraText();
        }

        // Обработчик нажатия клавиши
        private void MainForm_KeyDown(object sender, KeyEventArgs e)
        {
            KeyController.KeyDown(e.KeyCode);
            pictureBox.Image = render.RenderScene();
            UpdateCameraText();
        }

        // Обработчик отпускания клавиши
        private void MainForm_KeyUp(object sender, KeyEventArgs e)
        {
            KeyController.KeyUp(e.KeyCode);
            pictureBox.Image = render.RenderScene();
            UpdateCameraText();
        }
    }
}

