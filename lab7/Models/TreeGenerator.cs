using System;
using System.Drawing;
using OpenTK.Mathematics;
using OpenTK.Graphics.OpenGL;
using Lab7.Renderers;

namespace Lab7.Models
{
    // Класс для генерации и рисования 3D дерева
    // Дерево строится рекурсивно из цилиндров
    public static class TreeGenerator
    {
        // Рисует дерево рекурсивно
        // position - позиция основания дерева
        // radius - радиус ствола
        // height - высота ствола
        // segments - количество сегментов для цилиндра
        // depth - глубина рекурсии (сколько уровней веток)
        // iter - номер итерации (для определения цвета листьев)
        public static void DrawTree(Vector3 position, float radius, float height, int segments, int depth, int iter = 0)
        {
            // Если достигли максимальной глубины - рисуем листья
            if (depth <= 1)
            {
                // Первая итерация - большие листья
                if (iter == 1)
                    FigureRenderer.DrawCube(Vector3.Zero, 10, OpenTK.Graphics.OpenGL.PrimitiveType.Quads, Color.FromArgb(60, 0, 200, 0));
                else
                    // Остальные - маленькие листья
                    FigureRenderer.DrawCube(Vector3.Zero, 2, OpenTK.Graphics.OpenGL.PrimitiveType.Quads, Color.FromArgb(178, 0, 120, 0));
                return;
            }

            // Сохраняем текущую матрицу
            GL.PushMatrix();
            GL.Translate(position);

            // Рисуем ствол или ветку (цилиндр)
            FigureRenderer.DrawCylinder(
                center: Vector3.Zero,
                radius: radius,
                height: height,
                segments: segments,
                drawType: OpenTK.Graphics.OpenGL.PrimitiveType.Quads,
                color: Color.SaddleBrown,  // Коричневый цвет для ствола
                rotateX: 0,
                rotateY: 0
            );

            // Позиция вершины ствола/ветки
            Vector3 top = new Vector3(0, 0, height);

            // Количество веток
            int branches = 3;
            
            // Углы наклона веток
            var anglesX = new int[] { 30, -25, 10 };
            var anglesY = new int[] { 20, -15, -30 };
            
            // Рисуем ветки
            for (int i = 0; i < branches; i++)
            {
                // Вычисляем параметры новой ветки
                float newHeight = height * (0.6f + 1 * 0.2f);  // Высота немного меньше
                float newRadius = radius * 0.7f;                // Радиус тоже меньше

                // Сохраняем матрицу и поворачиваем для ветки
                GL.PushMatrix();
                GL.Translate(top);
                GL.Rotate(anglesX[i], 1, 0, 0);
                GL.Rotate(anglesY[i], 0, 1, 0);

                // Рекурсивно рисуем ветку
                DrawTree(Vector3.Zero, newRadius, newHeight, segments, depth - 1, i);
                GL.PopMatrix();
            }

            // Восстанавливаем матрицу
            GL.PopMatrix();
        }
    }
}

