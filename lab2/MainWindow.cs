using OpenTK;
using OpenTK.Graphics;
using OpenTK.Graphics.OpenGL;
using OpenTK.Input;
using System;
using Lab2.Shapes;
using Lab2.Utils;

namespace Lab2
{
    // Главное окно приложения
    // Наследуется от GameWindow из OpenTK
    public class MainWindow : GameWindow
    {
        // Фигуры которые будем рисовать
        private Triangle triangle;
        private Square square;
        private Circle circle;

        // Конструктор - создает окно с заданными размерами
        public MainWindow()
            : base(800, 600, GraphicsMode.Default, "Лабораторная работа 2")
        {
            // Включаем вертикальную синхронизацию для плавной анимации
            VSync = VSyncMode.On;
        }

        // Вызывается при загрузке окна
        // Здесь настраиваем OpenGL
        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            
            // Включаем тест глубины - чтобы правильно отображать 3D объекты
            GL.Enable(EnableCap.DepthTest);
            
            // Создаем фигуры которые будем рисовать
            triangle = new Triangle();
            square = new Square();
            circle = new Circle(
                new Vector3(0.0f, 0.0f, 4.0f),  // Центр круга
                1.0f,                            // Радиус
                new Vector3(1.0f, 0.5f, 0.2f),   // Оранжевый цвет
                50                               // 50 сторон для плавного круга
            );
        }

        // Вызывается при изменении размера окна
        // Нужно пересчитать проекцию под новый размер
        protected override void OnResize(EventArgs e)
        {
            base.OnResize(e);
            
            // Устанавливаем область отрисовки на весь клиентский прямоугольник
            GL.Viewport(ClientRectangle.X, ClientRectangle.Y, ClientRectangle.Width, ClientRectangle.Height);
            
            // Настраиваем проекцию под новый размер окна
            OpenGLHelper.SetupProjection(Width, Height);
        }

        // Вызывается каждый кадр для обновления логики
        protected override void OnUpdateFrame(FrameEventArgs e)
        {
            base.OnUpdateFrame(e);
            
            // Если нажата клавиша Escape - закрываем окно
            if (Keyboard.GetState()[Key.Escape])
                Exit();
        }

        // Вызывается каждый кадр для отрисовки
        // Здесь рисуем все фигуры
        protected override void OnRenderFrame(FrameEventArgs e)
        {
            base.OnRenderFrame(e);
            
            // Очищаем экран перед рисованием нового кадра
            OpenGLHelper.ClearScreen();
            
            // Настраиваем вид камеры
            OpenGLHelper.SetupView();
            
            // Рисуем все фигуры
            square.Render();
            triangle.Render();
            circle.Render();
            
            // Показываем нарисованный кадр
            SwapBuffers();
        }

        // Точка входа в программу
        [STAThread]
        static void Main()
        {
            // Создаем и запускаем окно
            using (MainWindow window = new MainWindow())
            {
                // Запускаем с частотой 30 кадров в секунду
                window.Run(30.0);
            }
        }
    }
}

