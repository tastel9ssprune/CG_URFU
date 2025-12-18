using OpenTK;
using OpenTK.Graphics.OpenGL;
using System;

namespace Lab2.Utils
{
    // Вспомогательный класс для работы с OpenGL
    // Здесь собраны функции для настройки и рисования
    public static class OpenGLHelper
    {
        // Настраивает вид камеры в OpenGL
        // Это нужно чтобы правильно отображать объекты в 3D пространстве
        public static void SetupView()
        {
            // Создаем матрицу вида - она определяет откуда смотрим на сцену
            Matrix4 modelview = Matrix4.LookAt(Vector3.Zero, Vector3.UnitZ, Vector3.UnitY);
            GL.MatrixMode(MatrixMode.Modelview);
            GL.LoadMatrix(ref modelview);
        }

        // Настраивает проекцию - как 3D объекты превращаются в 2D изображение
        public static void SetupProjection(int width, int height)
        {
            // Создаем перспективную проекцию с углом обзора 45 градусов
            Matrix4 projection = Matrix4.CreatePerspectiveFieldOfView(
                (float)Math.PI / 4, 
                width / (float)height, 
                1.0f, 
                64.0f
            );
            GL.MatrixMode(MatrixMode.Projection);
            GL.LoadMatrix(ref projection);
        }

        // Очищает экран перед рисованием нового кадра
        public static void ClearScreen()
        {
            GL.Clear(ClearBufferMask.ColorBufferBit | ClearBufferMask.DepthBufferBit);
        }
    }
}

