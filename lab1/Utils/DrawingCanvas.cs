using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace GraphicsLab
{
    // Класс для управления областью рисования
    // Хранит список фигур и управляет их отрисовкой через событие Paint
    public class DrawingCanvas
    {
        // Панель для рисования
        private readonly Panel _panel;
        // Список всех нарисованных фигур
        private readonly List<IShape> _shapes;
        // Цвет фона области рисования
        private Color _backgroundColor;

        // Конструктор
        // Инициализирует панель и подписывается на событие Paint
        public DrawingCanvas(Panel panel)
        {
            _panel = panel;
            _shapes = new List<IShape>();
            _backgroundColor = panel.BackColor;
            InitializeCanvas();
        }

        // Инициализация canvas
        // Подписывается на событие Paint панели для перерисовки фигур
        private void InitializeCanvas()
        {
            _panel.Paint += OnPanelPaint;
        }

        // Обработчик события Paint
        // Вызывается каждый раз, когда панель нуждается в перерисовке
        // Отрисовывает все фигуры из списка
        private void OnPanelPaint(object sender, PaintEventArgs e)
        {
            var graphics = e.Graphics;
            // Включаем сглаживание для более качественной отрисовки
            graphics.SmoothingMode = SmoothingMode.AntiAlias;
            // Очищаем область цветом фона
            graphics.Clear(_backgroundColor);
            
            // Отрисовываем все фигуры из списка
            foreach (var shape in _shapes)
            {
                shape.Render(graphics);
            }
        }

        // Получить Graphics для совместимости со старым кодом
        // В новой реализации не используется, так как рисование через Paint
        public Graphics GetGraphics()
        {
            return _panel.CreateGraphics();
        }

        // Добавить фигуру в список и обновить отображение
        public void AddShape(IShape shape)
        {
            _shapes.Add(shape);
            Refresh();
        }

        // Удалить последнюю добавленную фигуру
        public void RemoveLastShape()
        {
            if (_shapes.Count > 0)
            {
                _shapes.RemoveAt(_shapes.Count - 1);
                Refresh();
            }
        }

        // Очистить список фигур
        public void ClearShapes()
        {
            _shapes.Clear();
            Refresh();
        }

        // Обновить отображение панели
        // Вызывает перерисовку через Invalidate и Update
        public void Refresh()
        {
            _panel.Invalidate();
            _panel.Update();
        }

        // Очистить область рисования
        // Устанавливает новый цвет фона и очищает список фигур
        public void Clear(Color backgroundColor)
        {
            _backgroundColor = backgroundColor;
            _shapes.Clear();
            Refresh();
        }

        // Освобождение ресурсов
        // Отписываемся от события Paint
        public void Dispose()
        {
            _panel.Paint -= OnPanelPaint;
        }
    }
}
