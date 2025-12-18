using OpenTK;
using OpenTK.Graphics.OpenGL;
using System;

namespace Lab6.Utils
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
                MathHelper.PiOver4,  // Угол обзора 45 градусов
                aspectRatio,          // Соотношение сторон
                1f,                   // Ближняя плоскость отсечения
                5000000000f           // Дальняя плоскость отсечения
            );
            
            GL.MultMatrix(ref perspective);
            
            // Возвращаемся к матрице модели-вида
            GL.MatrixMode(MatrixMode.Modelview);
            GL.LoadIdentity();
        }

        // Настраивает вид камеры
        // Устанавливает позицию камеры и точку, на которую она смотрит
        public static void SetupCamera()
        {
            // Создаем матрицу вида
            // Камера находится в точке (-300, 300, 200)
            // Смотрит на точку (0, 0, 0)
            // Вектор "вверх" направлен по оси Z
            var modelview = Matrix4.LookAt(
                new Vector3(-300, 300, 200),  // Позиция камеры
                new Vector3(0, 0, 0),         // Точка, на которую смотрим
                new Vector3(0, 0, 1)          // Направление "вверх"
            );

            GL.MatrixMode(MatrixMode.Modelview);
            GL.LoadMatrix(ref modelview);
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

