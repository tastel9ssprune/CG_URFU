using System;
using System.Drawing;

namespace Lab5.Utils
{
    // Вспомогательный класс для работы с изображениями
    // Здесь собраны функции для обработки пикселей
    public static class ImageHelper
    {
        // Вычисляет яркость пикселя
        // Яркость - это среднее значение красного, зеленого и синего каналов
        // pixel - цвет пикселя
        // Возвращает значение яркости от 0 до 255
        public static int CalculateBrightness(Color pixel)
        {
            return (pixel.R + pixel.G + pixel.B) / 3;
        }

        // Ограничивает значение в диапазоне от min до max
        // Используется чтобы цвета не выходили за пределы 0-255
        private static int Clamp(int value, int min, int max)
        {
            if (value < min) return min;
            if (value > max) return max;
            return value;
        }

        // Изменяет яркость цвета на заданное значение
        // color - исходный цвет
        // brightnessDelta - на сколько изменить яркость (может быть отрицательным)
        // Возвращает новый цвет с измененной яркостью
        public static Color AdjustBrightness(Color color, int brightnessDelta)
        {
            // Вычисляем новые значения каналов с учетом ограничений
            int r = Clamp(color.R + brightnessDelta, 0, 255);
            int g = Clamp(color.G + brightnessDelta, 0, 255);
            int b = Clamp(color.B + brightnessDelta, 0, 255);
            
            // Сохраняем альфа-канал (прозрачность) из исходного цвета
            return Color.FromArgb(color.A, r, g, b);
        }

        // Проверяет что координаты находятся в пределах изображения
        // x, y - координаты пикселя
        // width, height - размеры изображения
        // Возвращает true если координаты валидны
        public static bool IsValidPixel(int x, int y, int width, int height)
        {
            return x >= 0 && y >= 0 && x < width && y < height;
        }
    }
}

