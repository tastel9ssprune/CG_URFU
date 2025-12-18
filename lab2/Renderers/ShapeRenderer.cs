using OpenTK;
using OpenTK.Graphics.OpenGL;
using System;

namespace Lab2.Renderers
{
    // Класс для рисования различных фигур через OpenGL
    // Здесь собраны все функции рисования
    public static class ShapeRenderer
    {
        // Рисует произвольную фигуру по заданным точкам
        // points - массив точек фигуры
        // colors - массив цветов для каждой точки
        // count - количество точек
        // primitive - тип примитива (треугольники, линии и т.д.)
        public static void DrawShape(Vector3[] points, Vector3[] colors, int count, PrimitiveType primitive)
        {
            // Начинаем рисование примитива
            GL.Begin(primitive);
            
            // Проходим по всем точкам и рисуем их
            for (int i = 0; i < count; i++)
            {
                // Устанавливаем цвет для текущей точки
                GL.Color3(colors[i]);
                // Рисуем вершину в заданной точке
                GL.Vertex3(points[i]);
            }
            
            // Заканчиваем рисование
            GL.End();
        }

        // Рисует правильный многоугольник с разными цветами для каждой вершины
        // center - центр многоугольника
        // radius - радиус (расстояние от центра до вершин)
        // colors - массив цветов для вершин
        // sides - количество сторон
        // mode - режим рисования (заливка, контур и т.д.)
        public static void DrawPolygon(Vector3 center, float radius, Vector3[] colors, int sides, PrimitiveType mode)
        {
            GL.Begin(mode);
            
            // Вычисляем координаты каждой вершины многоугольника
            for (int i = 0; i < sides; i++)
            {
                // Вычисляем угол для текущей вершины
                // Распределяем вершины равномерно по кругу
                float angle = i * 2.0f * (float)Math.PI / sides;
                
                // Вычисляем координаты вершины используя тригонометрию
                float x = center.X + radius * (float)Math.Cos(angle);
                float y = center.Y + radius * (float)Math.Sin(angle);
                
                // Устанавливаем цвет и рисуем вершину
                GL.Color3(colors[i]);
                GL.Vertex3(x, y, center.Z);
            }
            
            GL.End();
        }

        // Рисует правильный многоугольник одним цветом
        // Это перегрузка функции для удобства - когда нужен один цвет для всех вершин
        public static void DrawPolygon(Vector3 center, float radius, Vector3 color, int sides, PrimitiveType mode)
        {
            // Создаем массив одинаковых цветов
            Vector3[] colors = new Vector3[sides];
            for (int i = 0; i < sides; i++)
            {
                colors[i] = color;
            }
            
            // Вызываем основную функцию рисования
            DrawPolygon(center, radius, colors, sides, mode);
        }
    }
}

