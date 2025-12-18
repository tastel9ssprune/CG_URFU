using OpenTK.Mathematics;
using OpenTK.Graphics.OpenGL;
using System;

namespace Lab7.Utils
{
    // Вспомогательный класс для работы с OpenGL
    // Здесь собраны функции для настройки OpenGL
    public static class OpenGLHelper
    {
        // Настраивает область отрисовки и проекцию
        // width, height - размеры окна
        public static void SetupViewport(int width, int height)
        {
            // Устанавливаем область отрисовки
            GL.Viewport(0, 0, width, height);
            
            // Переключаемся на матрицу проекции
            GL.MatrixMode(MatrixMode.Projection);
            GL.LoadIdentity();

            // Вычисляем соотношение сторон
            float aspectRatio = (float)width / height;
            
            // Создаем перспективную проекцию
            Matrix4 perspective = Matrix4.CreatePerspectiveFieldOfView(
                MathF.PI / 4.0f,  // Угол обзора 45 градусов
                aspectRatio,          // Соотношение сторон
                1f,                   // Ближняя плоскость отсечения
                5000000000f           // Дальняя плоскость отсечения
            );
            
            GL.MultMatrix(ref perspective);
            
            // Возвращаемся к матрице модели-вида
            GL.MatrixMode(MatrixMode.Modelview);
            GL.LoadIdentity();
        }

        // Очищает экран и настраивает базовые параметры
        public static void ClearScreen()
        {
            // Устанавливаем цвет фона (темно-синий)
            GL.ClearColor(0.1f, 0.2f, 0.5f, 0.0f);
            
            // Включаем тест глубины для правильного отображения 3D объектов
            GL.Enable(EnableCap.DepthTest);
            
            // Очищаем буферы цвета и глубины
            GL.Clear(ClearBufferMask.ColorBufferBit | ClearBufferMask.DepthBufferBit);
        }
    }
}

