using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using OpenTK.Graphics.OpenGL;

namespace Lab7.Utils
{
    // Класс для загрузки текстур из файлов
    public static class TextureLoader
    {
        // Загружает текстуру из файла
        // filePath - путь к файлу изображения
        // Возвращает ID текстуры в OpenGL
        public static int LoadTexture(string filePath)
        {
            // Проверяем что файл существует
            if (!File.Exists(filePath))
                throw new FileNotFoundException($"Файл текстуры не найден: {filePath}");

            // Генерируем ID для новой текстуры
            int textureId = GL.GenTexture();
            GL.BindTexture(TextureTarget.Texture2D, textureId);

            // Загружаем изображение из файла
            using (Bitmap bitmap = new Bitmap(filePath))
            {
                // Переворачиваем изображение по вертикали
                // OpenGL использует другую систему координат для текстур
                bitmap.RotateFlip(RotateFlipType.RotateNoneFlipY);

                // Блокируем биты изображения для чтения
                BitmapData data = bitmap.LockBits(
                    new Rectangle(0, 0, bitmap.Width, bitmap.Height),
                    ImageLockMode.ReadOnly,
                    System.Drawing.Imaging.PixelFormat.Format32bppArgb
                );

                // Загружаем данные изображения в OpenGL
                GL.TexImage2D(
                    TextureTarget.Texture2D,
                    0,                          // Уровень детализации
                    PixelInternalFormat.Rgba,   // Внутренний формат
                    data.Width,                 // Ширина
                    data.Height,                // Высота
                    0,                          // Граница (должна быть 0)
                    OpenTK.Graphics.OpenGL.PixelFormat.Bgra,  // Формат пикселей
                    PixelType.UnsignedByte,     // Тип данных
                    data.Scan0                   // Указатель на данные
                );

                // Разблокируем биты
                bitmap.UnlockBits(data);
            }

            // Настраиваем параметры текстуры
            // Линейная фильтрация для сглаживания
            GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureMinFilter, (int)TextureMinFilter.Linear);
            GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureMagFilter, (int)TextureMagFilter.Linear);

            return textureId;
        }
    }
}

