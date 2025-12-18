using OpenTK;
using Lab2.Renderers;
using OpenTK.Graphics.OpenGL;

namespace Lab2.Shapes
{
    // Класс для работы с треугольником
    // Хранит координаты вершин и цвета
    public class Triangle
    {
        // Координаты вершин треугольника
        private Vector3[] vertices;
        
        // Цвета для каждой вершины
        private Vector3[] colors;
        
        // Глубина по оси Z (расстояние от камеры)
        private float depth;

        // Конструктор - создает треугольник с заданными параметрами
        public Triangle(float z = 4.0f)
        {
            depth = z;
            
            // Задаем координаты трех вершин треугольника
            vertices = new Vector3[3];
            vertices[0] = new Vector3(-1.0f, -1.0f, depth);
            vertices[1] = new Vector3(1.0f, -1.0f, depth);
            vertices[2] = new Vector3(0.0f, 1.0f, depth);
            
            // Задаем цвета для каждой вершины
            colors = new Vector3[3];
            colors[0] = new Vector3(1.0f, 1.0f, 0.0f);  // Желтый
            colors[1] = new Vector3(1.0f, 0.0f, 0.0f);  // Красный
            colors[2] = new Vector3(0.2f, 0.9f, 1.0f);  // Голубой
        }

        // Рисует треугольник на экране
        public void Render()
        {
            // Используем режим Triangles - рисуем заполненный треугольник
            ShapeRenderer.DrawShape(vertices, colors, 3, PrimitiveType.Triangles);
        }
    }
}

