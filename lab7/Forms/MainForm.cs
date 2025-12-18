using System;
using System.Windows.Forms;
using OpenTK.Mathematics;
using OpenTK.Graphics.OpenGL;
using OpenTK.WinForms;
using Lab7.Controllers;
using Lab7.Models;
using Lab7.Renderers;
using Lab7.Utils;

namespace Lab7.Forms
{
    // Главная форма приложения для отображения 3D дерева
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
        private float angleX = -90;
        private float angleY = 0;
        private float angleZ = 0;
        
        // Флаг что OpenGL инициализирован
        private bool loaded = false;
        
        // Контроллер камеры
        private CameraController camera = new CameraController();

        // Конструктор - создает форму и все элементы интерфейса
        public MainForm()
        {
            // Настраиваем форму
            this.Text = "Лабораторная работа 7 - 3D дерево";
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
            glControl.MouseMove += GlControl_MouseMove;
            glControl.KeyDown += GlControl_KeyDown;
            glControl.KeyUp += GlControl_KeyUp;
            
            // Включаем обработку клавиатуры
            this.KeyPreview = true;

            groupBoxGL.Controls.Add(glControl);

            // Добавляем группы на форму
            this.Controls.Add(groupBoxControls);
            this.Controls.Add(groupBoxGL);
        }

        // Обработчик загрузки OpenGL
        private void GlControl_Load(object sender, EventArgs e)
        {
            loaded = true;
            
            // Загружаем текстуру для земли
            try
            {
                FigureRenderer.GroundTextureId = TextureLoader.LoadTexture("grass.jpg");
            }
            catch
            {
                // Если текстура не найдена - продолжаем без нее
            }
            
            OpenGLHelper.SetupViewport(glControl.Width, glControl.Height);
        }

        // Обработчик изменения размера окна
        private void GlControl_Resize(object sender, EventArgs e)
        {
            if (!loaded) return;
            OpenGLHelper.SetupViewport(glControl.Width, glControl.Height);
            glControl.Invalidate();
        }

        // Обработчик отрисовки
        private void GlControl_Paint(object sender, PaintEventArgs e)
        {
            if (!loaded) return;

            // Очищаем экран
            OpenGLHelper.ClearScreen();
            
            // Настраиваем камеру
            var modelview = Matrix4.LookAt(
                camera.Position,
                camera.Target,
                Vector3.UnitY
            );
            GL.MatrixMode(MatrixMode.Modelview);
            GL.LoadMatrix(ref modelview);

            // Применяем повороты вокруг осей
            GL.Rotate(angleX, 1, 0, 0);
            GL.Rotate(angleY, 0, 1, 0);
            GL.Rotate(angleZ, 0, 0, 1);

            // Рисуем координатные оси
            FigureRenderer.DrawAxes();

            // Рисуем сетку земли с текстурой
            FigureRenderer.DrawTextureGrid(10, 100);

            // Рисуем дерево
            TreeGenerator.DrawTree(
                position: new Vector3(100, 0, 0),
                radius: 4f,
                height: 70f,
                segments: 7,
                depth: 9
            );

            // Показываем нарисованный кадр
            glControl.SwapBuffers();
        }

        // Обработчик движения мыши
        private void GlControl_MouseMove(object sender, MouseEventArgs e)
        {
            camera.MouseMove(e.X, e.Y);
            glControl.Invalidate();
        }

        // Обработчик нажатия клавиши
        private void GlControl_KeyDown(object sender, KeyEventArgs e)
        {
            camera.KeyDown(e.KeyCode);
            camera.Update();
            glControl.Invalidate();
        }

        // Обработчик отпускания клавиши
        private void GlControl_KeyUp(object sender, KeyEventArgs e)
        {
            camera.KeyUp(e.KeyCode);
            camera.Update();
            glControl.Invalidate();
        }

        // Обработчик нажатия на кнопку "Rotate"
        private void ButtonRotate_Click(object sender, EventArgs e)
        {
            float angle = (float)numericUpDown.Value;
            
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
            
            glControl.Invalidate();
        }

        // Обработчик нажатия на кнопку "Angle = 0"
        private void ButtonReset_Click(object sender, EventArgs e)
        {
            angleX = -90;
            angleY = 0;
            angleZ = 0;
            glControl.Invalidate();
        }
    }
}

