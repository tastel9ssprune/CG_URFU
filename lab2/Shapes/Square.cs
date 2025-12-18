using OpenTK;
using Lab2.Renderers;
using OpenTK.Graphics.OpenGL;

namespace Lab2.Shapes
{
    // Класс для работы с квадратом
    // Квадрат - это четырехугольник с равными сторонами
    public class Square
    {
        // Координаты вершин квадрата
        private Vector3[] vertices;
        
        // Цвета для каждой вершины
        private Vector3[] colors;
        
        // Глубина по оси Z
        private float depth;

        // Конструктор - создает квадрат
        public Square(float z = 4.0f)
        {
            depth = z;
            
            // Задаем координаты четырех вершин квадрата
            vertices = new Vector3[4];
            vertices[0] = new Vector3(-1.0f, -1.0f, depth);
            vertices[1] = new Vector3(1.0f, -1.0f, depth);
            vertices[2] = new Vector3(1.0f, 1.0f, depth);
            vertices[3] = new Vector3(-1.0f, 1.0f, depth);
            
            // Задаем разные цвета для каждой вершины
            colors = new Vector3[4];
            colors[0] = new Vector3(1.0f, 1.0f, 0.0f);  // Желтый
            colors[1] = new Vector3(1.0f, 0.0f, 0.0f);   // Красный
            colors[2] = new Vector3(0.2f, 0.9f, 1.0f);    // Голубой
            colors[3] = new Vector3(0.0f, 1.0f, 0.0f);   // Зеленый
        }

        // Рисует квадрат контуром (линиями)
        public void Render()
        {
            // Используем LineLoop - рисуем замкнутую линию по вершинам
            ShapeRenderer.DrawShape(vertices, colors, 4, PrimitiveType.LineLoop);
        }
    }
}

