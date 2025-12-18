using System;
using System.Drawing;
using System.Drawing.Drawing2D;

namespace GraphicsLab.Shapes
{
    // Класс для отрисовки линии (отрезка)
    // Реализует интерфейс IShape
    public class LineShape : IShape
    {
        // Начальная точка линии
        private readonly Point _start;
        // Конечная точка линии
        private readonly Point _end;
        // Толщина линии
        private readonly float _width;
        // Цвет линии
        private readonly Color _foreground;
        // Цвет фона для стирания
        private readonly Color _background;
        // Стиль линии (сплошная, пунктирная и т.д.)
        private readonly DashStyle _style;
        // Вид начала линии
        private readonly LineCap _startCap;
        // Вид конца линии
        private readonly LineCap _endCap;

        // Конструктор класса
        public LineShape(Point start, Point end, float width, Color foreground, Color background, 
            DashStyle style = DashStyle.Solid, LineCap startCap = LineCap.Flat, LineCap endCap = LineCap.Flat)
        {
            _start = start;
            _end = end;
            _width = width;
            _foreground = foreground;
            _background = background;
            _style = style;
            _startCap = startCap;
            _endCap = endCap;
        }

        // Реализация метода Render из интерфейса IShape
        // Отрисовывает линию заданным цветом и стилем
        public void Render(Graphics graphics)
        {
            using (var pen = new Pen(_foreground, _width))
            {
                pen.DashStyle = _style;
                pen.StartCap = _startCap;
                pen.EndCap = _endCap;
                graphics.DrawLine(pen, _start, _end);
            }
        }

        // Реализация метода Remove из интерфейса IShape
        // Стирает линию, рисуя её цветом фона
        public void Remove(Graphics graphics)
        {
            using (var pen = new Pen(_background, _width))
            {
                pen.DashStyle = _style;
                pen.StartCap = _startCap;
                pen.EndCap = _endCap;
                graphics.DrawLine(pen, _start, _end);
            }
        }
    }
}
