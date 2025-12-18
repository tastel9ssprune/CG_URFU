using System.Drawing;

namespace GraphicsLab.Shapes
{
    // Класс для отрисовки эллипса (овала)
    // Поддерживает заливку, контур и подпись
    public class EllipseShape : IShape
    {
        // Центр эллипса
        private readonly Point _center;
        // Радиус по оси X
        private readonly int _radiusX;
        // Радиус по оси Y
        private readonly int _radiusY;
        // Цвет контура
        private readonly Color _borderColor;
        // Цвет заливки
        private readonly Color _fillColor;
        // Цвет для стирания
        private readonly Color _eraseColor;
        // Текст подписи
        private readonly string _label;
        // Шрифт для подписи
        private readonly Font _labelFont;
        // Цвет текста подписи
        private readonly Color _labelColor;

        // Конструктор класса
        public EllipseShape(Point center, int radiusX, int radiusY, Color borderColor, 
            Color fillColor, Color eraseColor, string label, Color labelColor)
        {
            _center = center;
            _radiusX = radiusX;
            _radiusY = radiusY;
            _borderColor = borderColor;
            _fillColor = fillColor;
            _eraseColor = eraseColor;
            _label = label;
            _labelFont = new Font("Arial", 10);
            _labelColor = labelColor;
        }

        // Метод для отрисовки эллипса
        // Сначала заливает фигуру, затем рисует контур и добавляет подпись
        public void Render(Graphics graphics)
        {
            // Вычисляем прямоугольник, описывающий эллипс
            // DrawEllipse рисует от левого верхнего угла, поэтому вычитаем радиусы
            var bounds = new Rectangle(_center.X - _radiusX, _center.Y - _radiusY, 
                _radiusX * 2, _radiusY * 2);
            
            using (var fillBrush = new SolidBrush(_fillColor))
            using (var borderPen = new Pen(_borderColor))
            using (var textBrush = new SolidBrush(_labelColor))
            {
                // Заливаем эллипс
                graphics.FillEllipse(fillBrush, bounds);
                // Рисуем контур
                graphics.DrawEllipse(borderPen, bounds);
                // Добавляем подпись в центре
                graphics.DrawString(_label, _labelFont, textBrush, _center);
            }
        }

        // Метод для стирания эллипса
        // Заливает и обводит эллипс цветом фона
        public void Remove(Graphics graphics)
        {
            var bounds = new Rectangle(_center.X - _radiusX, _center.Y - _radiusY, 
                _radiusX * 2, _radiusY * 2);
            
            using (var eraseBrush = new SolidBrush(_eraseColor))
            using (var erasePen = new Pen(_eraseColor))
            {
                graphics.FillEllipse(eraseBrush, bounds);
                graphics.DrawEllipse(erasePen, bounds);
            }
        }
    }
}
