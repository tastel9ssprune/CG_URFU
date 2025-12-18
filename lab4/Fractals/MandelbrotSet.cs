using OpenTK.Graphics.OpenGL;

namespace Lab4.Fractals
{
    // Класс для рисования множества Мандельброта
    // Это фрактал который строится итерациями комплексных чисел
    public static class MandelbrotSet
    {
        // Рисует множество Мандельброта
        // width, height - размеры окна в пикселях
        // maxIterations - максимальное количество итераций (влияет на детализацию)
        public static void Render(int width, int height, int maxIterations)
        {
            // Начинаем рисовать точки
            GL.Begin(PrimitiveType.Points);

            // Проходим по всем пикселям экрана
            for (int px = 0; px < width; px++)
            {
                for (int py = 0; py < height; py++)
                {
                    // Преобразуем координаты пикселя в координаты комплексной плоскости
                    // Множество Мандельброта находится примерно в диапазоне от -2.47 до 1.0 по X
                    // и от -1.12 до 1.12 по Y
                    var x0 = (px / (double)width) * 3.47 - 2.47;
                    var y0 = (py / (double)height) * 2.24 - 1.12;

                    // Начальные значения для итерации
                    double x = 0.0;
                    double y = 0.0;

                    int iteration = 0;

                    // Итеративно вычисляем: z = z^2 + c
                    // где z начинается с 0, а c = (x0, y0)
                    // Если |z| > 2, то точка не принадлежит множеству
                    while (x * x + y * y <= 2 * 2 && iteration < maxIterations)
                    {
                        // Вычисляем z^2 + c
                        // z^2 = (x + iy)^2 = x^2 - y^2 + 2ixy
                        double xtemp = x * x - y * y + x0;
                        y = 2 * x * y + y0;
                        x = xtemp;
                        iteration++;
                    }

                    // Цвет зависит от количества итераций
                    // Чем больше итераций, тем темнее точка
                    var color = iteration / (double)maxIterations;
                    GL.Color3(color, color, color);
                    
                    // Преобразуем координаты пикселя в координаты OpenGL (-1 до 1)
                    GL.Vertex2((px / (double)width) * 2 - 1, (py / (double)height) * 2 - 1);
                }
            }

            GL.End();
        }
    }
}

