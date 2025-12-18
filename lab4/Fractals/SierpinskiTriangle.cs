using OpenTK;
using OpenTK.Graphics.OpenGL;

namespace Lab4.Fractals
{
    // Класс для рисования треугольника Серпинского
    // Это фрактал который строится рекурсивно делением треугольника на меньшие
    public static class SierpinskiTriangle
    {
        // Рисует треугольник Серпинского с заданной глубиной рекурсии
        // recursionDepth - сколько раз делить треугольник (чем больше, тем детальнее)
        public static void Render(int recursionDepth)
        {
            // Задаем координаты трех вершин начального треугольника
            Vector2 vertexA = new Vector2(-0.8f, -0.8f);  // Левая нижняя вершина
            Vector2 vertexB = new Vector2(0.8f, -0.8f);   // Правая нижняя вершина
            Vector2 vertexC = new Vector2(0.0f, 0.8f);     // Верхняя вершина

            // Начинаем рекурсивное рисование
            DrawSierpinski(vertexA, vertexB, vertexC, recursionDepth);
        }

        // Рекурсивная функция для рисования треугольника Серпинского
        // a, b, c - координаты вершин текущего треугольника
        // depth - оставшаяся глубина рекурсии
        private static void DrawSierpinski(Vector2 a, Vector2 b, Vector2 c, int depth)
        {
            // Если достигли максимальной глубины - рисуем треугольник
            if (depth == 0)
            {
                GL.Begin(PrimitiveType.LineLoop);
                
                // Рисуем три вершины разными цветами для красоты
                GL.Color3(0.9f, 0.0f, 0.0f);  // Красный
                GL.Vertex2(a);
                
                GL.Color3(0.0f, 0.0f, 0.9f);  // Синий
                GL.Vertex2(b);
                
                GL.Color3(0.0f, 0.9f, 0.0f);  // Зеленый
                GL.Vertex2(c);
                
                GL.End();
            }
            else
            {
                // Вычисляем середины сторон треугольника
                // Это точки для деления треугольника на три меньших
                Vector2 ab = (a + b) / 2;  // Середина стороны AB
                Vector2 bc = (b + c) / 2;  // Середина стороны BC
                Vector2 ca = (c + a) / 2;  // Середина стороны CA

                // Рекурсивно рисуем три меньших треугольника
                DrawSierpinski(a, ab, ca, depth - 1);  // Левый нижний
                DrawSierpinski(ab, b, bc, depth - 1);  // Правый нижний
                DrawSierpinski(ca, bc, c, depth - 1);   // Верхний
            }
        }
    }
}

