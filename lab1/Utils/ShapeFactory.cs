using System.Drawing;
using GraphicsLab.Shapes;

namespace GraphicsLab
{
    // Фабрика для создания различных типов фигур
    // Использует паттерн Factory для упрощения создания объектов
    public static class ShapeFactory
    {
        // Создает линию (отрезок)
        public static IShape CreateLine(Point start, Point end, float width, 
            Color foreground, Color background)
        {
            return new LineShape(start, end, width, foreground, background);
        }

        // Создает эллипс с подписью
        public static IShape CreateEllipse(Point center, int radiusX, int radiusY, 
            Color borderColor, Color fillColor, Color eraseColor, string label, Color labelColor)
        {
            return new EllipseShape(center, radiusX, radiusY, borderColor, fillColor, eraseColor, label, labelColor);
        }

        // Создает правильный многоугольник
        // Вычисляет координаты вершин автоматически
        public static IShape CreateRegularPolygon(Point center, int sides, int radius, 
            float lineWidth, Color outlineColor, Color fillColor, Color eraseColor)
        {
            return PolygonShape.CreateRegular(center, sides, radius, lineWidth, outlineColor, fillColor, eraseColor);
        }

        // Создает произвольный многоугольник по заданным точкам
        public static IShape CreateCustomPolygon(PointF[] points, float lineWidth, 
            Color outlineColor, Color fillColor, Color eraseColor)
        {
            return new PolygonShape(points, lineWidth, outlineColor, fillColor, eraseColor);
        }
    }
}
