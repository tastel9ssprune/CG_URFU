using System;
using System.Drawing;

namespace GraphicsLab.Shapes
{
    // Класс для отрисовки многоугольников
    // Поддерживает как правильные, так и произвольные многоугольники
    public class PolygonShape : IShape
    {
        // Массив вершин многоугольника
        private readonly PointF[] _vertices;
        // Толщина линии контура
        private readonly float _lineWidth;
        // Цвет контура
        private readonly Color _outlineColor;
        // Цвет заливки
        private readonly Color _fillColor;
        // Цвет для стирания
        private readonly Color _eraseColor;

        // Конструктор для произвольного многоугольника
        public PolygonShape(PointF[] vertices, float lineWidth, Color outlineColor, 
            Color fillColor, Color eraseColor)
        {
            _vertices = vertices;
            _lineWidth = lineWidth;
            _outlineColor = outlineColor;
            _fillColor = fillColor;
            _eraseColor = eraseColor;
        }

        // Статический метод для создания правильного многоугольника
        // Вычисляет координаты вершин на основе центра, количества сторон и радиуса
        public static PolygonShape CreateRegular(Point center, int vertexCount, int radius, 
            float lineWidth, Color outlineColor, Color fillColor, Color eraseColor)
        {
            var vertices = new PointF[vertexCount];
            // Вычисляем шаг угла: полный круг (2*PI) делим на количество вершин
            double angleIncrement = 2.0 * Math.PI / vertexCount;
            
            // Вычисляем координаты каждой вершины
            for (int i = 0; i < vertexCount; i++)
            {
                // Угол для текущей вершины
                double angle = i * angleIncrement;
                // Вычисляем координаты через тригонометрические функции
                vertices[i] = new PointF(
                    center.X + radius * (float)Math.Cos(angle),
                    center.Y + radius * (float)Math.Sin(angle)
                );
            }
            
            return new PolygonShape(vertices, lineWidth, outlineColor, fillColor, eraseColor);
        }

        // Метод для отрисовки многоугольника
        // Сначала заливает, затем рисует контур
        public void Render(Graphics graphics)
        {
            using (var fillBrush = new SolidBrush(_fillColor))
            using (var outlinePen = new Pen(_outlineColor, _lineWidth))
            {
                // Заливаем многоугольник
                graphics.FillPolygon(fillBrush, _vertices);
                // Рисуем контур
                graphics.DrawPolygon(outlinePen, _vertices);
            }
        }

        // Метод для стирания многоугольника
        // Заливает и обводит цветом фона
        public void Remove(Graphics graphics)
        {
            using (var eraseBrush = new SolidBrush(_eraseColor))
            using (var erasePen = new Pen(_eraseColor, _lineWidth))
            {
                graphics.FillPolygon(eraseBrush, _vertices);
                graphics.DrawPolygon(erasePen, _vertices);
            }
        }
    }
}
