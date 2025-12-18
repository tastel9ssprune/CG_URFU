using OpenTK;
using OpenTK.Graphics;
using OpenTK.Graphics.OpenGL;
using OpenTK.Input;
using System;
using Lab4.Fractals;
using Lab4.Utils;

namespace Lab4
{
    // Главное окно приложения для отображения фракталов
    // Наследуется от GameWindow из OpenTK
    public class MainWindow : GameWindow
    {
        // Глубина рекурсии для треугольника Серпинского
        private int sierpinskiDepth = 5;

        // Конструктор - создает окно с заданными размерами
        public MainWindow()
            : base(800, 600, GraphicsMode.Default, "Лабораторная работа 4 - Фракталы")
        {
            // Включаем вертикальную синхронизацию для плавной анимации
            VSync = VSyncMode.On;
        }

        // Вызывается при загрузке окна
        // Здесь настраиваем OpenGL
        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            
            // Устанавливаем цвет фона (белый)
            GL.ClearColor(1.0f, 1.0f, 1.0f, 1.0f);
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
        // Здесь рисуем фракталы
        protected override void OnRenderFrame(FrameEventArgs e)
        {
            base.OnRenderFrame(e);
            
            // Очищаем экран перед рисованием нового кадра
            OpenGLHelper.ClearScreen();
            
            // Настраиваем вид камеры
            OpenGLHelper.SetupView();
            
            // Рисуем треугольник Серпинского
            SierpinskiTriangle.Render(sierpinskiDepth);
            
            // Можно раскомментировать для рисования множества Мандельброта
            // MandelbrotSet.Render(800, 600, 100);
            
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

