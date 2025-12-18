using OpenTK;
using OpenTK.Graphics.OpenGL;
using System;

namespace Lab4.Utils
{
    // Вспомогательный класс для работы с OpenGL
    // Здесь собраны функции для настройки OpenGL
    public static class OpenGLHelper
    {
        // Настраивает вид для 2D рисования
        // Для 2D просто загружаем единичную матрицу
        public static void SetupView()
        {
            // Для 2D используем единичную матрицу - без преобразований
            GL.MatrixMode(MatrixMode.Modelview);
            GL.LoadIdentity();
        }

        // Настраивает ортографическую проекцию для 2D рисования
        // Это нужно для фракталов которые рисуются в 2D координатах
        public static void SetupProjection(int width, int height)
        {
            // Создаем ортографическую проекцию для 2D
            // Координаты от -1 до 1 по обеим осям
            Matrix4 projection = Matrix4.CreateOrthographic(2.0f, 2.0f, -1.0f, 1.0f);
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

