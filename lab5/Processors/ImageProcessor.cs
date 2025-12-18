using System;
using System.Drawing;

namespace Lab5.Processors
{
    // Класс для обработки изображений
    // Здесь собраны алгоритмы изменения изображений
    public class ImageProcessor
    {
        // Изменяет яркость всего изображения
        // originalImage - исходное изображение
        // brightnessDelta - на сколько изменить яркость (может быть отрицательным для затемнения)
        // Возвращает новое изображение с измененной яркостью
        public static Bitmap AdjustBrightness(Bitmap originalImage, int brightnessDelta)
        {
            // Создаем новое изображение того же размера
            Bitmap modifiedImage = new Bitmap(originalImage.Width, originalImage.Height);

            // Проходим по всем пикселям изображения
            for (int y = 0; y < originalImage.Height; y++)
            {
                for (int x = 0; x < originalImage.Width; x++)
                {
                    // Получаем цвет исходного пикселя
                    Color originalPixel = originalImage.GetPixel(x, y);
                    
                    // Изменяем яркость пикселя
                    Color modifiedPixel = Utils.ImageHelper.AdjustBrightness(originalPixel, brightnessDelta);
                    
                    // Устанавливаем новый цвет в модифицированное изображение
                    modifiedImage.SetPixel(x, y, modifiedPixel);
                }
            }

            return modifiedImage;
        }

        // Получает информацию о пикселе в заданных координатах
        // image - изображение
        // x, y - координаты пикселя
        // Возвращает строку с информацией о цвете и яркости
        public static string GetPixelInfo(Bitmap image, int x, int y)
        {
            // Проверяем что координаты валидны
            if (!Utils.ImageHelper.IsValidPixel(x, y, image.Width, image.Height))
            {
                return "Координаты вне изображения";
            }

            // Получаем цвет пикселя
            Color pixel = image.GetPixel(x, y);
            
            // Вычисляем яркость
            int brightness = Utils.ImageHelper.CalculateBrightness(pixel);
            
            // Формируем строку с информацией
            return $"Цвет: R={pixel.R}, G={pixel.G}, B={pixel.B}, Яркость: {brightness}";
        }
    }
}

