using System;
using System.Windows.Forms;

namespace GraphicsLab
{
    // Вспомогательный класс для создания элементов интерфейса
    // Упрощает создание кнопок с заданными параметрами
    public static class UIHelper
    {
        // Создает кнопку с заданными параметрами
        // caption - текст на кнопке
        // x, y - координаты расположения
        // width, height - размеры (по умолчанию 180x30)
        // handler - обработчик события нажатия (опционально)
        public static Button CreateControlButton(string caption, int x, int y, int width = 180, int height = 30, EventHandler handler = null)
        {
            var button = new Button
            {
                Text = caption,
                Location = new System.Drawing.Point(x, y),
                Size = new System.Drawing.Size(width, height)
            };
            
            // Если передан обработчик, подписываемся на событие Click
            if (handler != null)
            {
                button.Click += handler;
            }
            
            return button;
        }
    }
}
