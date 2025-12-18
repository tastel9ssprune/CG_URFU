using System;
using System.Windows.Forms;
using OpenTK;
using OpenTK.Graphics.OpenGL;
using Lab6.Renderers;
using Lab6.Utils;

namespace Lab6.Forms
{
    // Главная форма приложения для рисования 3D фигур
    public partial class MainForm : Form
    {
        // Компонент для отображения OpenGL
        private GLControl glControl;
        
        // Элементы управления
        private ComboBox comboBoxAxis;
        private NumericUpDown numericUpDown;
        private Button buttonRotate;
        private Button buttonReset;
        
        // Углы поворота вокруг осей
        private float angleX = 0;
        private float angleY = 0;
        private float angleZ = 0;
        
        // Флаг что OpenGL инициализирован
        private bool loaded = false;

        // Конструктор - создает форму и все элементы интерфейса
        public MainForm()
        {
            // Настраиваем форму
            this.Text = "Лабораторная работа 6 - 3D фигуры";
            this.Width = 800;
            this.Height = 600;

            // Создаем группу для элементов управления
            var groupBoxControls = new GroupBox 
            { 
                Text = "Управление", 
                Left = 10, 
                Top = 10, 
                Width = 200, 
                Height = 500 
            };
            
            // Создаем группу для области рисования
            var groupBoxGL = new GroupBox 
            { 
                Text = "Рисование", 
                Left = 220, 
                Top = 10, 
                Width = 550, 
                Height = 500 
            };

            // Создаем выпадающий список для выбора оси вращения
            comboBoxAxis = new ComboBox 
            { 
                Left = 10, 
                Top = 30, 
                Width = 100 
            };
            comboBoxAxis.Items.AddRange(new string[] { "X", "Y", "Z" });
            comboBoxAxis.SelectedIndex = 0;
            groupBoxControls.Controls.Add(comboBoxAxis);

            // Создаем поле для ввода угла поворота
            numericUpDown = new NumericUpDown 
            { 
                Left = 10, 
                Top = 70, 
                Width = 100, 
                Value = 3 
            };
            groupBoxControls.Controls.Add(numericUpDown);

            // Создаем кнопку для поворота
            buttonRotate = new Button 
            { 
                Left = 10, 
                Top = 110, 
                Width = 180, 
                Height = 30, 
                Text = "Rotate" 
            };
            buttonRotate.Click += ButtonRotate_Click;
            groupBoxControls.Controls.Add(buttonRotate);

            // Создаем кнопку для сброса углов
            buttonReset = new Button 
            { 
                Left = 10, 
                Top = 150, 
                Width = 180, 
                Height = 30, 
                Text = "Angle = 0" 
            };
            buttonReset.Click += ButtonReset_Click;
            groupBoxControls.Controls.Add(buttonReset);

            // Создаем компонент OpenGL
            glControl = new GLControl 
            { 
                Left = 10, 
                Top = 20, 
                Width = 520, 
                Height = 460, 
                BackColor = System.Drawing.Color.Black 
            };
            
            // Подключаем обработчики событий
            glControl.Load += GlControl_Load;
            glControl.Paint += GlControl_Paint;
            glControl.Resize += GlControl_Resize;
            groupBoxGL.Controls.Add(glControl);

            // Добавляем группы на форму
            this.Controls.Add(groupBoxControls);
            this.Controls.Add(groupBoxGL);
        }

        // Обработчик загрузки OpenGL
        // Вызывается когда OpenGL готов к работе
        private void GlControl_Load(object sender, EventArgs e)
        {
            loaded = true;
            OpenGLHelper.SetupViewport(glControl.Width, glControl.Height);
        }

        // Обработчик изменения размера окна
        // Нужно пересчитать проекцию под новый размер
        private void GlControl_Resize(object sender, EventArgs e)
        {
            if (!loaded) return;
            OpenGLHelper.SetupViewport(glControl.Width, glControl.Height);
            glControl.Invalidate();  // Перерисовываем
        }

        // Обработчик отрисовки
        // Здесь рисуем все фигуры
        private void GlControl_Paint(object sender, PaintEventArgs e)
        {
            if (!loaded) return;

            // Очищаем экран и настраиваем базовые параметры
            OpenGLHelper.ClearScreen();
            
            // Настраиваем камеру
            OpenGLHelper.SetupCamera();

            // Применяем повороты вокруг осей
            GL.Rotate(angleX, 1, 0, 0);  // Поворот вокруг оси X
            GL.Rotate(angleY, 0, 1, 0);  // Поворот вокруг оси Y
            GL.Rotate(angleZ, 0, 0, 1);  // Поворот вокруг оси Z

            // Рисуем координатные оси
            FigureRenderer.DrawAxes();

            // Рисуем поверхность трилистника
            // Это красивая параметрическая поверхность
            FigureRenderer.DrawTrefoilSurface(0.004f, PrimitiveType.LineLoop);

            // Можно раскомментировать для рисования других фигур:
            // FigureRenderer.DrawCube(new Vector3(1.5f, 0f, -6f), 70.0f, PrimitiveType.Quads, Color.White);
            // FigureRenderer.DrawCone(new Vector3(-3.0f, -2.0f, -6f), 50.5f, 100.5f, 20, PrimitiveType.LineLoop, Color.Red);

            // Показываем нарисованный кадр
            glControl.SwapBuffers();
        }

        // Обработчик нажатия на кнопку "Rotate"
        // Поворачивает фигуру вокруг выбранной оси
        private void ButtonRotate_Click(object sender, EventArgs e)
        {
            // Получаем значение угла из поля ввода
            float angle = (float)numericUpDown.Value;
            
            // Поворачиваем вокруг выбранной оси
            switch (comboBoxAxis.Text)
            {
                case "X": 
                    angleX = (angleX + angle) % 360; 
                    break;
                case "Y": 
                    angleY = (angleY + angle) % 360; 
                    break;
                case "Z": 
                    angleZ = (angleZ + angle) % 360; 
                    break;
            }
            
            // Перерисовываем
            glControl.Invalidate();
        }

        // Обработчик нажатия на кнопку "Angle = 0"
        // Сбрасывает все углы поворота
        private void ButtonReset_Click(object sender, EventArgs e)
        {
            angleX = 0;
            angleY = 0;
            angleZ = 0;
            glControl.Invalidate();
        }
    }
}

