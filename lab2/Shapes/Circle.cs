using OpenTK;
using Lab2.Renderers;
using OpenTK.Graphics.OpenGL;

namespace Lab2.Shapes
{
    // Класс для работы с кругом
    // Круг рисуется как многоугольник с большим количеством сторон
    public class Circle
    {
        // Центр круга
        private Vector3 center;
        
        // Радиус круга
        private float radius;
        
        // Цвет круга
        private Vector3 color;
        
        // Количество сторон многоугольника (чем больше, тем круглее)
        private int sides;

        // Конструктор - создает круг с заданными параметрами
        public Circle(Vector3 center, float radius, Vector3 color, int sides = 50)
        {
            this.center = center;
            this.radius = radius;
            this.color = color;
            this.sides = sides;
        }

        // Рисует круг на экране
        public void Render()
        {
            // Используем режим Polygon для заливки
            // Круг рисуется как многоугольник с большим числом сторон
            ShapeRenderer.DrawPolygon(center, radius, color, sides, PrimitiveType.Polygon);
        }
    }
}

